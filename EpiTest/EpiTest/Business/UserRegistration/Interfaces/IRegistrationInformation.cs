namespace EpiTest.Business.UserRegistration.Interfaces
{
    public interface IUserCreationInformation
    {
        string ConfirmPassword { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string Username { get; set; }
    }
}