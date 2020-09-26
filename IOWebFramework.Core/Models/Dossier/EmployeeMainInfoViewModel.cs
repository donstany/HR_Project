using System.ComponentModel.DataAnnotations;

namespace IOWebFramework.Core.Models.Dossier
{
    public class EmployeeMainInfoViewModel
    {
        [Display(Name = "Трудов Договор")]
        public string Td { get; set; }

        [Display(Name = "Предишен опит сумарно")]
        public int? PreviuosExperienceSummed { get; set; }

        [Display(Name = "Предишен опит в ИО")]
        public int? PreviuosExperienceInIs { get; set; }

        [Display(Name = "Предишен опит извън ИО")]
        public int? PreviuosExperience { get; set; }

        [Display(Name = "Клон")]
        public string Branch { get; set; }
     
        [Display(Name = "Позиция")]
        public string Position { get; set; }

        [Display(Name = "Отдел")]
        public string Department { get; set; }

        [Display(Name = "Снимка")]
        public byte[] Photo { get; set; }

        [Display(Name = "Предишен опит в ИО")]
        public string FormatedPreviuosExperienceInIs { get; set; }

        [Display(Name = "Предишен опит извън ИО")]
        public string FormatedPreviuosExperience { get; set; }

        [Display(Name = "Предишен опит сумарно")]
        public string FormatedPreviuosExperienceSummed { get; set; }
    }
}
