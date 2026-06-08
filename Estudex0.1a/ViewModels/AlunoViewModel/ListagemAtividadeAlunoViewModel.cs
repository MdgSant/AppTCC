using EstudeX.ViewModels;
using Estudex0._1a.Models;
using Estudex0._1a.Services.Aluno;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Estudex0._1a.ViewModels.AlunoViewModel
{
    public class ListagemAtividadeAlunoViewModel : BaseViewModel
    {
        private AlunoAtividadeService aService;
        public ObservableCollection<AtividadeStatus> Atividades { get; set; } = new();

        public ListagemAtividadeAlunoViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            aService = new AlunoAtividadeService(token);
        }

        public async Task ObterAtividades()
        {
            try
            {
                Atividades.Clear();
                int idAluno = Preferences.Get("UsuarioId", 1);

                var lista = await aService.GetAtividadesAsync();

                foreach (var a in lista)
                {
                    bool jaRespondeu = await aService.VerificarJaRespondeuAsync(idAluno, a.IdAtividade ?? 0);
                    Atividades.Add(new AtividadeStatus
                    {
                        Atividade = a,
                        JaRespondeu = jaRespondeu
                    });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message, "Ok");
            }
        }
    }
}
