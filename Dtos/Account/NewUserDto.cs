namespace UsersApi.Dtos.Account
{
    public class NewUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; } // Include the phone number here
    }

}