using AutoMapper;

namespace Sdao.AppModel.API.Models.Mappings
{
    /// <summary>
    /// AutoMapperConfiguration
    /// </summary>
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// Configure
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
            });
        }
    }
}
