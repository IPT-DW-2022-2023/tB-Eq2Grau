using Eq2Grau.Models;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Security.Cryptography.Xml;

namespace Eq2Grau.Controllers {
    public class HomeController : Controller {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger=logger;
        }


      /*
       * Algoritmo
       * 1 - solicitar ao utilizador os parâmetros da equação (A, B, C)
             1.1 - enviar os dados para o Servidor
         2 - será q posso fazer o cálculo das raízes?
             2.1 - o parâmetro A=/=0?
                   2.1.1 - o delta (b2-4ac) >= 0
                           x1 = (-b + sqrt(delta))/(2a)
                           x2 = (-b - sqrt(delta))/2/a
                   2.1.2 - o delta (b2-4ac) < 0
                           x1 = -b/(2a) + sqrt(-delta))/(2a) i
                           x2 = -b/2/a  - sqrt(-delta))/2/a  i      
                   2.1.3 - enviar a resposta (x1 e x2) para o utilizador
             2.2 - (o parâmetro A=0)
                   notificar o utilizador que escrever um valor errado
       */

      public IActionResult Index(int A, int B, int C) {
            // vars auxiliares
            double delta = B*B-4*A*C;
            string x1 = "", x2 = "";

            // validar se há condições para efetuar o cálculo
            if (A!=0) {
                if (delta>=0) {
                    //  x1 = (-b + sqrt(delta))/(2a)
                    //  x2 = (-b - sqrt(delta))/2/a
                    x1=(-B+Math.Sqrt(delta))/(2*A)+"";
                    x2=(-B-Math.Sqrt(delta))/2/A+"";
                }
                else {
                    // solução complexa
                    x1=-B/(2*A)+" + "+Math.Sqrt(-delta)/(2*A)+" i";
                    x2=-B/(2*A)+" - "+Math.Sqrt(-delta)/(2*A)+" i";
                }
                // preparar os dados a serem enviados para o browser
                ViewBag.X1 = x1;
                ViewBag.X2 = x2;
            }



            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId=Activity.Current?.Id??HttpContext.TraceIdentifier });
        }
    }
}