using System;
using System.Collections.Generic;
using System.Text;

namespace IOWebFramework.Core.Models.Cv
{
    public class CvViewModel
    {
        public string PhotoBase64 { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int PreviuosExperienceSummed { get; set; }
        public int PreviuosExperienceInIs { get; set; }
        public int PreviuosExperience { get; set; }
        public  List<EducationDto> EducationDtos { get; set; }
        public  List<TrainingDto> TrainingDtos { get; set; }
        public  List<CertificateDto> CertificateDtos { get; set; }
        public  List<ProjectDto> ProjectDtos { get; set; }
        public string FormatedPreviuosExperienceInIs { get; set; }
        public string FormatedPreviuosExperience { get; set; }
        public string FormatedPreviuosExperienceSummed { get; set; }

        public class EducationDto
        {
            public string EducationInstitutionName { get; set; }
            public string DegreeName { get; set; }
            public string SpecialtyName { get; set; }
        }
        public class TrainingDto
        {
            public string TrainingName { get; set; }
            public string TrainingCenterName { get; set; }
            public DateTime DateStart { get; set; }
            public DateTime? DateEnd { get; set; }
        }
        public class CertificateDto
        {
            public string CertificateName { get; set; }
            public string CertificateType { get; set; }
            public DateTime DateStart { get; set; }
            public DateTime? DateEnd { get; set; }
        }
        public class ProjectDto
        {
            public string ProjectFullName { get; set; }
            public string ProjectName { get; set; }
            public string ProjectRole { get; set; }
            public DateTime DateStart { get; set; }
            public DateTime? DateEnd { get; set; }
        }
    }
}
