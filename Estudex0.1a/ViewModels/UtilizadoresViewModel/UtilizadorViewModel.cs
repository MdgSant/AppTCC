using System;
using System.Collections.Generic;
using System.Text;
using EstudeX0._1a.Services;
using Estudex0._1a.Models;
using EstudeX0._1a.ViewModel.UtilizadorViewModel;
using System.Windows.Input;
using EstudeX.ViewModels;

namespace Estudex0._1a.ViewModels.UtilizadoresViewModel
{
    public class UtilizadorViewModel : BaseViewModel
    {
        private UtilizadorService uService;
        public ICommand AutenticarCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand DirecionarCadastroCommand { get; set; }

        public UtilizadorViewModel()
        {
            uService = new UtilizadorService();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            AutenticarCommand = new Command(async () => await AutenticarUtilizador());
            RegistrarCommand = new Command(async () => await RegistrarUtilizador());
            DirecionarCadastroCommand = new Command(async () => await DirecionarParaCadastro());
        }


        private string email = string.Empty;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        private string senha = string.Empty;
        public string Senha
        {
            get { return senha; }
            set { senha = value; OnPropertyChanged(); }
        }

        //LOGIN
        public async Task AutenticarUtilizador()
        {
            try
            {
                LoginRequest u = new LoginRequest();
                u.Email = Email;
                u.Senha = Senha;

                LoginResponse uAutenticado = await uService.PostAutenticarUtilizadorAsync(u);

                if (!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    string mensagem = $"Bem-vindo(a) {uAutenticado.Nome}.";

                    Preferences.Set("UtilizadorId", uAutenticado.IdUtilizador);
                    Preferences.Set("UtilizadorNome", uAutenticado.Nome);
                    Preferences.Set("UtilizadorToken", uAutenticado.Token);

                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Dados incorretos :(", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        //CADASTRO
        public async Task RegistrarUtilizador()
        {
            try
            {
                LoginRequest u = new LoginRequest();
                u.Email = Email;
                u.Senha = Senha;

                Utilizador uRegistrado = await uService.PostRegistrarUtilizadorAsync(u);

                if (uRegistrado.IdUtilizador != 0)
                {
                    string mensagem = $"Utilizador Id {uRegistrado.IdUtilizador} registrado com sucesso.";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaCadastro()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CadastroView());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

    }
}