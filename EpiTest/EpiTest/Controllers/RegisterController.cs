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
using EpiTest.Business.UserRegistration.Interfaces;

namespace EpiTest.Controllers
{
    /// <summary>
    /// Used to register a user for first time
    /// </summary>
    public class RegisterController : Controller
    {
        public const string Role = "WebAdmins";
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
        public ActionResult Index(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool success;
                IEnumerable<string> errors = Enumerable.Empty<string>();
                UserCreationHandler.Service.CreateUser(model,Role, out success, out errors);
                if (success)
                {
                    AdministratorRegistrationPage.IsEnabled = false;
                    SetFullAccessToWebAdmin();

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

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!AdministratorRegistrationPage.IsEnabled)
            {
                filterContext.Result = new HttpNotFoundResult();
                return;
            }
            base.OnAuthorization(filterContext);
        }
        private void SetFullAccessToWebAdmin()
        {
            var securityrep = ServiceLocator.Current.GetInstance<IContentSecurityRepository>();
            var permissions = securityrep.Get(ContentReference.RootPage).CreateWritableClone() as IContentSecurityDescriptor;
            permissions.AddEntry(new AccessControlEntry(Role, AccessLevel.FullAccess));
            securityrep.Save(ContentReference.RootPage, permissions, SecuritySaveType.Replace);
        }

    }
}
