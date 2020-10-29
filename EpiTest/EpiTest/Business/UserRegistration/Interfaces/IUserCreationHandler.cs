using System.Collections.Generic;

namespace EpiTest.Business.UserRegistration.Interfaces { 

    interface IUserCreationHandler
    {
        void CreateUser(IUserCreationInformation userInformation,string role, out bool success, out IEnumerable<string> errors);


    }
}