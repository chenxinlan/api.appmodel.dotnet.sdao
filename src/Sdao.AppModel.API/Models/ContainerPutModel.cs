using System.ComponentModel.DataAnnotations;

namespace Sdao.AppModel.API.Models
{
    /// <summary>
    /// ContainerPutModel
    /// </summary>
    public class ContainerPutModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        public long id { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        [Required]
        public long categoryId { get; set; }

        /// <summary>
        /// 容器名称
        /// </summary>
        [Required]
        public string containerName { get; set; }

        /// <summary>
        /// 自定义json格式：可以写样式等:标题加粗处理
        /// </summary>
        public string json { get; set; }
    }
}
