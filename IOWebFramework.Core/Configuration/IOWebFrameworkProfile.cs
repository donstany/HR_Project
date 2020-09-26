using AutoMapper;
using IOWebFramework.Core.Models.Dossier;
using IOWebFramework.Infrastructure.Data.Models;
using IOWebFramework.Shared.Common;

namespace IOWebFramework.Core.Configuration
{
    public class IOWebFrameworkProfile : Profile
    {
        public IOWebFrameworkProfile()
        {
            CreateMap<Training, TrainingListViewModel>()
             .ForMember(dto => dto.TrainingName, conf => conf.MapFrom(ol => ol.TrainingName.Label))
             .ForMember(dto => dto.TrainingCenter, conf => conf.MapFrom(ol => ol.TrainingCenter.Label));

            CreateMap<Certificate, CertificateListViewModel>()
              .ForMember(dto => dto.CertificateNameIssuer, conf => conf.MapFrom(ol => ol.CertificateNameIssuer.Name))
              .ForMember(dto => dto.CertificateType, conf => conf.MapFrom(ol => ol.CertificateType.Label));
              

            CreateMap<Diploma, DiplomaListViewModel>()
             .ForMember(dto => dto.Degree, conf => conf.MapFrom(ol => ol.Degree.Label))
             .ForMember(dto => dto.EducationInstitution, conf => conf.MapFrom(ol => ol.EducationInstitution.Label))
             .ForMember(dto => dto.Specialty, conf => conf.MapFrom(ol => ol.DegreeId == 3 ? ol.SchoolProfile.Label : ol.Classifier.Name));
            
            CreateMap<Diploma, DiplomaViewModel>().ReverseMap();

            CreateMap<Employee, EmployeeMainInfoViewModel>().ReverseMap();
            
            CreateMap<Certificate, CertificateViewModel>().ReverseMap();

            CreateMap<Training, TrainingViewModel>().ReverseMap();
            CreateMap<Employee, UserDetailDTO>().ReverseMap();
        }
    }
}
