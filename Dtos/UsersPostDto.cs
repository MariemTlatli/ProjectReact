namespace UsersApi.Dtos
{
    public class UsersPostDto
    {
         public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleId { get; set; }
        public List<Permission> Permissions { get; set; } = new List<Permission>();
        
    }
}