using EPiServer.Personalization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Security;
using EpiTest.Business.UserRegistration.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Profile;

namespace EpiTest.Business.UserRegistration.Handlers
{
    public partial class UserRegistrationHandler : IUserCreationHandler
    {
        public virtual string Role => string.Empty;
        Injected<UIUserProvider> UIUserProvider;
        Injected<UIRoleProvider> UIRoleProvider;

        private void AddUserToRole(string username)
        {
            if (!UIRoleProvider.Service.RoleExists(Role))
            {
                UIRoleProvider.Service.CreateRole(Role);
            }

            UIRoleProvider.Service.AddUserToRoles(username, new string[] { Role });
        }

        public virtual void CreateUser(IUserRegistrationInformation userInformation, out bool success, out IEnumerable<string> errors)
        {
            success = false;
            errors = null;
            UIUserCreateStatus createUserStatus;
            IEnumerable<string> createUserErrors = System.Linq.Enumerable.Empty<string>();

            var result = UIUserProvider.Service.CreateUser(userInformation.Username, userInformation.Password, userInformation.Email, null, null, true, out createUserStatus, out createUserErrors);
            if (createUserStatus == UIUserCreateStatus.Success)
            {
                success = true;

                AddUserToRole(result.Username);

                if (ProfileManager.Enabled)
                {
                    var profile = EPiServerProfile.Wrap(ProfileBase.Create(result.Username));
                    profile.Email = userInformation.Email;
                    profile.Save();
                }

                return;

            }
            errors = createUserErrors;
        }
    }
}