namespace EpiTest.Business.UserRegistration.Interfaces
{
    public interface IUserRegistrationInformation
    {
        string ConfirmPassword { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string Username { get; set; }
    }
}