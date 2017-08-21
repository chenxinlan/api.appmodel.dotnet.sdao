using AutoMapper;
using Sdao.AppModel.Model.Entities;

namespace Sdao.AppModel.API.Models.Mappings
{
    /// <summary>
    /// 领域对象与DTO（数据传输对象）之间的转换,数据库查询结果映射至实体.
    /// </summary>
    public class DomainToViewModelMappingProfile : Profile
    {
        /// <summary>
        /// constructor
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Container, ContainerViewModel>();

            CreateMap<ContainerPostModel, Container>();

            CreateMap<ContainerPutModel, Container>();
        }
    }
}
