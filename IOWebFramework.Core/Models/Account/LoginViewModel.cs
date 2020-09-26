using System.ComponentModel.DataAnnotations;

namespace IOWebFramework.Core.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Полето \"{0}\" е задължително")]
        [Display(Name = "Потребителско име от Активната Директория")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" е задължително")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола от Активната Директория")]
        public string Password { get; set; }

    }
}
