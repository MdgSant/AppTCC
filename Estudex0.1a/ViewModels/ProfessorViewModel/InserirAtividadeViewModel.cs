using AppRpgEtec.ViewModels;
using Estudex0._1a.Models;
using Estudex0._1a.Services.Professor;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Estudex0._1a.ViewModels.ProfessorViewModel
{
    public class InserirAtividadeViewModel : BaseViewModel
    {
        private int idAtividade;
        private string titulo;
        private int pontuacaoMaxima;
        private NivelDificuldade nivelDificuldadeSelecionado;
        private Utilizador orientadorSelecionado;

        public ObservableCollection<NivelDificuldade> ListaNiveisDificuldade { get; set; } = new();
        public ObservableCollection<Utilizador> ListaOrientadores { get; set; } = new();

        public string Titulo
        {
            get => titulo;
            set { titulo = value; OnPropertyChanged(); }
        }
        public int PontuacaoMaxima
        {
            get => pontuacaoMaxima;
            set { pontuacaoMaxima = value; OnPropertyChanged(); }
        }
        public NivelDificuldade NivelDificuldadeSelecionado
        {
            get => nivelDificuldadeSelecionado;
            set { nivelDificuldadeSelecionado = value; OnPropertyChanged(); }
        }
        public Utilizador OrientadorSelecionado
        {
            get => orientadorSelecionado;
            set { orientadorSelecionado = value; OnPropertyChanged(); }
        }

        public Command SalvarAtividadeCommand { get; set; }

        private ProfessorService aService;

        public InserirAtividadeViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            aService = new ProfessorService(token);
            SalvarAtividadeCommand = new Command(async () => await SalvarAtividade());
        }

        public async Task InicializarAsync()
        {
            await CarregarListas();
        }

        private async Task CarregarListas()
        {
            try
            {
                var niveis = await aService.GetNiveisDificuldadeAsync();
                Console.WriteLine("Niveis recebidos: " + niveis?.Count);

                var orientadores = await aService.GetOrientadoresAsync();
                Console.WriteLine("Orientadores recebidos: " + orientadores?.Count);

                foreach (var n in niveis) ListaNiveisDificuldade.Add(n);
                foreach (var o in orientadores) ListaOrientadores.Add(o);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Erro CarregarListas", ex.Message + " | " + ex.InnerException, "Ok");
            }
        }

        public async Task SalvarAtividade()
        {
            try
            {
                if (string.IsNullOrEmpty(Titulo) || NivelDificuldadeSelecionado == null || OrientadorSelecionado == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Atenção", "Preencha todos os campos!", "Ok");
                    return;
                }

                Atividade model = new Atividade()
                {
                    Titulo = this.titulo,
                    PontuacaoMaxima = this.pontuacaoMaxima,
                    IdOrientador = OrientadorSelecionado.IdUtilizador,
                    NivelDificuldade = NivelDificuldadeSelecionado
                };
                var atividade = await aService.PostAtividadeAsync(model);

                await Shell.Current.GoToAsync($"InserirPerguntasView?idAtividade={atividade.IdAtividade}");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
