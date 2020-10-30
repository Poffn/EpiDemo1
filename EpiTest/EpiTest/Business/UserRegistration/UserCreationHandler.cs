using EPiServer.Personalization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Security;
using EpiTest.Business.UserRegistration.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Profile;

namespace EpiTest.Business.UserRegistration.Handlers
{
    public class UserCreationHandler : IUserCreationHandler
    {
        Injected<UIUserProvider> UIUserProvider;
        Injected<UIRoleProvider> UIRoleProvider;

        private void AddUserToRole(string username,string role)
        {
            if (!UIRoleProvider.Service.RoleExists(role))
            {
                UIRoleProvider.Service.CreateRole(role);
            }

            UIRoleProvider.Service.AddUserToRoles(username, new string[] { role });
        }

        public virtual void CreateUser(IUserCreationInformation userInformation,string role, out bool success, out IEnumerable<string> errors)
        {
            success = false;
            UIUserCreateStatus createUserStatus;

            var result = UIUserProvider.Service.CreateUser(userInformation.Username, userInformation.Password, userInformation.Email, null, null, true, out createUserStatus, out errors);
            if (createUserStatus == UIUserCreateStatus.Success)
            {
                success = true;

                AddUserToRole(result.Username, role);

                if (ProfileManager.Enabled)
                {
                    var profile = EPiServerProfile.Wrap(ProfileBase.Create(result.Username));
                    profile.Email = userInformation.Email;
                    profile.Save();
                }

                return;

            }
        }
    }
}