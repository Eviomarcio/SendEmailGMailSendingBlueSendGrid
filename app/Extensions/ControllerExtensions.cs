using Microsoft.AspNetCore.Mvc;
using App.ViewModels;

namespace App.Extensions
{
    public static class ControllerExtensions
    {
        public static void ShowMessage(this Controller @this, string text, bool error = false)
        {
            @this.TempData["mensagem"] = MensagemViewModel.Serializar(
                text, error ? TipoMensagem.Erro : TipoMensagem.Informacao);
        }
    }
}