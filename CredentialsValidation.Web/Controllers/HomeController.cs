using CredentialsValidation.Abstractions;
using CredentialsValidation.Shared;
using CredentialsValidation.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CredentialsValidation.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyConfiguration _myConfiguration;


        public HomeController(ILogger<HomeController> logger, IOptions<MyConfiguration> myConfiguration)
        {
            _logger = logger;
            _myConfiguration = myConfiguration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: Home/Validate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ValidateAsync(IFormCollection collection)
        {
            ResponseEnvelope responseEnvelope = new ResponseEnvelope();

            try
            {
                Credentials credentialsObj = new Credentials
                {
                    EMail = collection["EMail"],
                    Password = collection["Password"]
                };

                ServiceHelper serviceHelper = new ServiceHelper(_myConfiguration.WebAPIBaseURL);
                responseEnvelope = await serviceHelper.ValidateCredentialsAsync(credentialsObj);
            }
            catch
            {
                // Log the exception
            }

            return Json(responseEnvelope);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
