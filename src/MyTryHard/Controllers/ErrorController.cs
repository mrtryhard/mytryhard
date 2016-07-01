using Microsoft.AspNetCore.Mvc;

namespace MyTryHard.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error/{errorCode:int?}")]
        public IActionResult Index(int? errorCode)
        {
            switch(errorCode)
            {
                case 500:
                    ViewBag.DetailedMessage = "La connection à la base de données a échouée.";
                    break;
                case 404:
                    ViewBag.DetailedMessage = "La page que vous avez demandé est introuvable.";
                    break;
                case 403:
                    ViewBag.DetailedMessage = "L'accès à cette section est interdite.";
                    break;
                default:
                    ViewBag.DetailedMessage = "Il s'agit d'une erreur inconnue. Contactez l'administrateur du site.";
                    break;
            }

            return View();
        }
    }
}
