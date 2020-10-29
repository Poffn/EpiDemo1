using System.Collections.Generic;

namespace EpiTest.Business.UserRegistration.Interfaces { 

    interface IUserCreationHandler
    {
        string Role { get; }
        void CreateUser(IUserRegistrationInformation userInformation, out bool success, out IEnumerable<string> errors);


    }
}