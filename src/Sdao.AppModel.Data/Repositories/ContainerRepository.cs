using Sdao.AppModel.Data.Abstract;
using Sdao.AppModel.Model.Entities;

namespace Sdao.AppModel.Data.Repositories
{
    /// <summary>
    /// ContainerRepository
    /// </summary>
    public class ContainerRepository : EntityBaseRepository<Container>, IContainerRepository
    {
        /// <summary>
        /// ContainerRepository
        /// </summary>
        /// <param name="context">FastProductContext</param>
        public ContainerRepository(AppModelContext context)
            : base(context)
        { }
    }
}
