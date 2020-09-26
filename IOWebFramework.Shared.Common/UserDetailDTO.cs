using System.ComponentModel.DataAnnotations;
using static IOWebFramework.Shared.Common.MessageConstant;

namespace IOWebFramework.Shared.Common
{
    public class UserDetailDTO
    {
        [Display(Name = "Снимка")]
        public byte[] Photo { get; set; }

        [Display(Name = "Пълно име")]
        public string FullName { get; set; }

        [Display(Name = "Позиция")]
        public string Position { get; set; }

        [Display(Name = "Клон")]
        public string Branch { get; set; }

        [Display(Name = "Отдел")]
        public string Department { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        public string Telephone { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "ЕГН")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public string PID { get; set; }

        public bool IsSyncSuccessfully { get; set; } = true;
    }
}
