using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sdao.AppModel.API.Models
{
    /// <summary>
    /// ContainerViewModel
    /// </summary>
    public class ContainerViewModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public long categoryId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string containerName { get; set; }

        /// <summary>
        /// 自定义 json 格式
        /// </summary>
        public string json { get; set; }
    }
}
