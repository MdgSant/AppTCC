using System;
using System.Collections.Generic;
using System.Text;
using Estudex0._1a.Models;
using System.Windows.Input;
using EstudeX.ViewModels;
using EstudeX.Services;
using Estudex0._1a.Models.Login;
using Estudex0._1a.Models.Utilizadores.Enum;
using Estudex0._1a.View.Utilizador;

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
                    Preferences.Set("UtilizadorTipo", uAutenticado.TipoUtilizador); // ✅ salva o tipo

                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    // ✅ redireciona para o Shell com perfil correto
                    var shell = new AppShell();
                    shell.AplicarPerfil();
                    Application.Current.MainPage = shell;
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
        private string nome = string.Empty;
        public string Nome
        {
            get { return nome; }
            set { nome = value; OnPropertyChanged(); }
        }

        private string cpf = string.Empty;
        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; OnPropertyChanged(); }
        }

        private string cidade = string.Empty;
        public string Cidade
        {
            get { return cidade; }
            set { cidade = value; OnPropertyChanged(); }
        }

        private string uf = string.Empty;
        public string Uf
        {
            get { return uf; }
            set { uf = value; OnPropertyChanged(); }
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

        private string tipoSelecionado = string.Empty;
        public string TipoSelecionado
        {
            get { return tipoSelecionado; }
            set { tipoSelecionado = value; OnPropertyChanged(); }
        }

        //CADASTRO
        public async Task RegistrarUtilizador()
        {
            try
            {
                if (string.IsNullOrEmpty(Nome) || string.IsNullOrEmpty(Cpf) ||
                    string.IsNullOrEmpty(Cidade) || string.IsNullOrEmpty(Uf) ||
                    string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Senha) ||
                    string.IsNullOrEmpty(TipoSelecionado))
                {
                    await Application.Current.MainPage.DisplayAlert("Atenção", "Preencha todos os campos!", "Ok");
                    return;
                }

                Utilizador u = new Utilizador();
                u.Nome = Nome;
                u.CPF = Cpf;
                u.Cidade = Cidade;
                u.UF = Uf;
                u.Email = Email;
                u.SenhaHash = Senha;
                u.IdTipoUtilizador = TipoSelecionado == "Professor" ? 2 : 1;

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