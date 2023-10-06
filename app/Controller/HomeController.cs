using System.Text;
using app.Interface;
using App.Extensions;
//using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;

        public HomeController(IEmailService emailService)
        {
            this._emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail()
        {
            var html = new StringBuilder();
            var destinationEmail = "eviomarcio.silva@gmail.com";
            html.Append("<h1>Teste de Serviço de Envio de E-mail</h1>");
            html.Append("<p>Este é um teste do serviço de envio de e-mails usando ASP.NET Core.</p>");
            await _emailService.SendEmailAsync(destinationEmail, "Teste de Serviço de E-mail", string.Empty, html.ToString());
            this.ShowMessage($"Uma mensagem foi enviada para o e-mail {destinationEmail}.");

        return RedirectToAction(nameof(Index));
        }
    }
}