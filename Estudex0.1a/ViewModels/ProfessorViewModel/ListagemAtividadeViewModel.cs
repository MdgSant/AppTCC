using Estudex0._1a.Models;
using Estudex0._1a.Services.Professor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Estudex0._1a.ViewModels.ProfessorViewModel
{
    public class ListagemAtividadeViewModel : ObservableObject
    {
        // 1
        private ProfessorService aService;

        // 2
        public ObservableCollection<Atividade> Atividades { get; set; }

        // 3
        public ListagemAtividadeViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            aService = new ProfessorService(token);
            Atividades = new ObservableCollection<Atividade>();

            _ = ObterAtividades();
        }

        public async Task ObterAtividades()
        {
            try
            {
                Atividades = await aService.GetAtividadesAsync();
                OnPropertyChanged(nameof(Atividades));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
