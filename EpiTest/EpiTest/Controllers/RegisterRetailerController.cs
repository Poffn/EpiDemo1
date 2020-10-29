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

namespace EpiTest.Controllers
{
    /// <summary>
    /// Used to register a user for first time
    /// </summary>
    public class RegisterRetailerController : Controller
    {
        const string AdminRoleName = "WebAdmins";
        public const string ErrorKey = "CreateError";

        

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
            //if (ModelState.IsValid)
            //{
            //    UIUserCreateStatus status;
            //    IEnumerable<string> errors = Enumerable.Empty<string>();
            //    var result = UIUserProvider.CreateUser(model.Username, model.Password, model.Email, null, null, true, out status, out errors);
            //    if (status == UIUserCreateStatus.Success)
            //    {
            //        UIRoleProvider.CreateRole(AdminRoleName);
            //        UIRoleProvider.AddUserToRoles(result.Username, new string[] { AdminRoleName });

            //        if (ProfileManager.Enabled)
            //        {
            //            var profile = EPiServerProfile.Wrap(ProfileBase.Create(result.Username));
            //            profile.Email = model.Email;
            //            profile.Save();
            //        }

            //        AdministratorRegistrationPage.IsEnabled = false;
            //        SetFullAccessToWebAdmin();
            //        var resFromSignIn = UISignInManager.SignIn(UIUserProvider.Name, model.Username, model.Password);
            //        if (resFromSignIn)
            //        {
            //            return Redirect(UrlResolver.Current.GetUrl(ContentReference.StartPage));
            //        }
            //    }
            //    AddErrors(errors);
            //}
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
        UIUserProvider UIUserProvider
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UIUserProvider>();
            }
        }
        UIRoleProvider UIRoleProvider
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UIRoleProvider>();
            }
        }
        UISignInManager UISignInManager
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UISignInManager>();
            }
        }

    }
}
