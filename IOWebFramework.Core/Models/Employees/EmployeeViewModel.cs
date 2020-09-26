using IOWebFramework.Shared.Common;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static IOWebFramework.Shared.Common.MessageConstant;

namespace IOWebFramework.Core.Models.Employees
{
    public class EmployeeViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        //[JsonProperty("personId")]
        //[DisplayName("Име")]
        ////[Range(1, int.MaxValue, ErrorMessage = FieldIsMandatory)]
        //public int PersonId { get; set; }

        [JsonProperty("personName")]
        public string PersonName { get; set; }

        [JsonProperty("td")]
        [DisplayName("ТД номер")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public string Td { get; set; }

        [JsonProperty("fileContentId")]
        [DisplayName("Прикачени")]
        public string FileContentId { get; set; }

        [JsonProperty("hireDate")]
        [DisplayName("Дата на назначаване")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public DateTime HireDate { get; set; }

        [JsonProperty("fireDate")]
        [DisplayName("Дата на напускане")]
        public DateTime? FireDate { get; set; }

        [JsonProperty("previuosExperienceSummed")]
        [DisplayName("Предходен трудов стаж - общо")]
        public int? PreviuosExperienceSummed { get; set; }

        [JsonProperty("previuosExperienceInIs")]
        [DisplayName("Предходен трудов стаж в ИО")]
        public int? PreviuosExperienceInIO { get; set; }

        [JsonProperty("previuosExperienceOutIs")]
        [DisplayName("Предходен трудов стаж извън ИО")]
        public int? PreviuosExperienceOutIO { get; set; }

        [DisplayName("Години")]
        public int ExpirienceInIOYearsId { get; set; }

        [DisplayName("Mесеци")]
        public int ExpirienceInIOMonthsId { get; set; }

        [DisplayName("Дни")]
        public int ExpirienceInIODaysId { get; set; }

        [DisplayName("Години")]
        public int ExpirienceOutIOYearsId { get; set; }

        [DisplayName("Mесеци")]
        public int ExpirienceOutIOMonthsId { get; set; }

        [DisplayName("Дни")]
        public int ExpirienceOutIODaysId { get; set; }

        [JsonProperty("previuosExperience")]
        [DisplayName("Предходен трудов стаж")]
        public int? PreviuosExperience { get; set; }

        [JsonProperty("branchId")]
        [DisplayName("Клон")]
        [Range(1, int.MaxValue, ErrorMessage = FieldIsMandatory)]
        public int BranchId { get; set; } = 1;

        [JsonProperty("branchName")]
        public string BranchName { get; set; }

        [JsonProperty("positionId")]
        [DisplayName("Позиция")]
        [Range(1, int.MaxValue, ErrorMessage = FieldIsMandatory)]
        public int PositionId { get; set; } = 1;

        [JsonProperty("positionName")]
        public string PositionName { get; set; }

        [JsonProperty("departmentId")]
        [DisplayName("Отдел")]
        [Range(1, int.MaxValue, ErrorMessage = FieldIsMandatory)]
        public int DepartmentId { get; set; } = 1;

        [JsonProperty("departmentName")]
        public string DepartmentName { get; set; }

        [JsonProperty("personalId")]
        //[Required(ErrorMessage = FieldIsMandatory)]
        [DisplayName("ЕГН")]
        public string PID { get; set; }

        [JsonProperty("isActive")]
        [DisplayName("Активен")]
        public bool IsActive { get; set; }

        [JsonProperty("userDetailDTO")]
        public UserDetailDTO UserDetailDTO { get; set; } = new UserDetailDTO();

        [JsonProperty("photoBase64")]
        [DisplayName("Снимка")]
        public string PhotoBase64 { get; set; }

        [JsonProperty("branch")]
        [DisplayName("Клон")]
        public string Branch { get; set; }

        [JsonProperty("department")]
        [DisplayName("Отдел")]
        public string Department { get; set; }

        [JsonProperty("position")]
        [DisplayName("Позиция")]
        public string Position { get; set; }

        [JsonProperty("address")]
        [DisplayName("Адрес")]
        public string Address { get; set; }

        [JsonProperty("telephone")]
        [DisplayName("Телефон")]
        public string Telephone { get; set; }

        [JsonProperty("email")]
        [DisplayName("Еmail")]
        public string Email { get; set; }
        public bool IsEditMode { get; set; } = false;
  
        [JsonProperty("isExistingActiveEmployee")]
        public bool IsExistingActiveEmployee { get; set; } = false;
        
        public (int Year, int Moth, int Day) SplitDate(int totalDays = 0)
        {
            int year = 0;
            int month = 0;
            int day = 0;
            if (totalDays == 0)
            {
                return (year, month, day);
            }

            year = totalDays % 360;
            month = totalDays % 12;
            day = totalDays - 0;
            return (year, month, day);
        }

    }
}
