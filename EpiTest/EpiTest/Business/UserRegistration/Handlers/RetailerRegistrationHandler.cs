using EpiTest.Business.UserRegistration.Interfaces;

namespace EpiTest.Business.UserRegistration.Handlers
{
    public class RetailerRegistrationHandler : UserRegistrationHandler, IUserCreationHandler
    {
        public override string Role => "Retailer";
    }
}