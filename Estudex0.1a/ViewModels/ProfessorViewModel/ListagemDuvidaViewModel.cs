using Estudex0._1a.Models;
using Estudex0._1a.Services.Professor;
using AppRpgEtec.ViewModels;
using System.Collections.ObjectModel;

namespace Estudex0._1a.ViewModels.ProfessorViewModel
{
    public class ListagemDuvidaViewModel : BaseViewModel
    {
        private DuvidaService dService;

        public ObservableCollection<Duvida> Duvidas { get; set; }

        public ListagemDuvidaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            dService = new DuvidaService(token);
            Duvidas = new ObservableCollection<Duvida>();
            _ = ObterDuvidas();
        }

        public async Task ObterDuvidas()
        {
            try
            {
                Duvidas = await dService.GetDuvidasAsync();
                OnPropertyChanged(nameof(Duvidas));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}