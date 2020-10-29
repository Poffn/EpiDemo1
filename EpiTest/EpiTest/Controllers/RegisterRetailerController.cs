using EpiTest.Models;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Security;
using EPiServer.Web.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Profile;
using EPiServer.Security;
using EPiServer.DataAbstraction;
using EPiServer.Personalization;
using System.EnterpriseServices;
using EpiTest.Business.UserRegistration.Interfaces;

namespace EpiTest.Controllers
{
    /// <summary>
    /// Used to register a user for first time
    /// </summary>
    public class RegisterRetailerController : Controller
    {
        public const string ErrorKey = "CreateError";

        Injected<IUserCreationHandler> UserCreationHandler;
        Injected<UIUserProvider> UIUserProvider;
        Injected<UISignInManager> UISignInManager;

        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index(RegisterRetailerViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool success;
                IEnumerable<string> errors = Enumerable.Empty<string>();
                UserCreationHandler.Service.CreateUser(model, out success, out errors);
                if (success)
                {

                    var resFromSignIn = UISignInManager.Service.SignIn(UIUserProvider.Service.Name, model.Username, model.Password);
                    if (resFromSignIn)
                    {
                        return Redirect(UrlResolver.Current.GetUrl(ContentReference.StartPage));
                    }
                }
                AddErrors(errors);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(ErrorKey, error);
            }
        }

    }
}
