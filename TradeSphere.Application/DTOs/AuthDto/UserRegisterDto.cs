namespace TradeSphere.Application.DTOs.AuthDto
{
    public class UserRegisterDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password do not match.")]
        public string PasswordConfirmed { get; set; }
        [Required]

        public string PhoneNumber { get; set; }
    }
}
