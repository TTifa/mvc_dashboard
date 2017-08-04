using System.ComponentModel.DataAnnotations;

namespace Ttifa.Web
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }

    public class LoginUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Role { get; set; }
    }

    public class RegisterUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string NickName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}