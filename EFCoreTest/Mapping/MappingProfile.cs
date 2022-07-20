using AutoMapper;
using Model;
using Service.Input;
using Service.Output;

namespace Device.Api.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Mapping profile
        /// </summary>
        public MappingProfile()
        {
            //Inputs

            // Guide
            CreateMap<Company, CompanyOutput>();
            CreateMap<CompanyInput, Company>();
            CreateMap<CompanyInput, CompanyInputAim>();
        }
    }
}
