using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EpiTest.Business.UserRegistration.Interfaces;
using System.Collections.Generic;

namespace EpiTest.Business.UserRegistration.Handlers
{
    public class AdminRegistrationHandler : UserRegistrationHandler, IUserCreationHandler
    {
        public override string Role => "WebAdmins";

        public override void CreateUser(IUserRegistrationInformation userInformation, out bool success, out IEnumerable<string> errors)
        {

            base.CreateUser(userInformation, out success, out errors);
            if (success)
            {
                AdministratorRegistrationPage.IsEnabled = false;
                SetFullAccessToWebAdmin();

            }
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