using Estudex0._1a.Models;
using Estudex0._1a.Services.Professor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using EstudeX.ViewModels;

namespace Estudex0._1a.ViewModels.ProfessorViewModel
{
    public class ListagemAtividadeViewModel : BaseViewModel
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
        }

        public async Task ObterAtividades()
        {
            try
            {
                Atividades.Clear(); // ✅ limpa sem substituir a referência
                var lista = await aService.GetAtividadesAsync();
                foreach (var a in lista) Atividades.Add(a);
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
