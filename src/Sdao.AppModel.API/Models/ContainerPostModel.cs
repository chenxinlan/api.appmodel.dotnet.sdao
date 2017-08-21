using System.ComponentModel.DataAnnotations;

namespace Sdao.AppModel.API.Models
{
    /// <summary>
    /// ContainerPostModel
    /// </summary>
    public class ContainerPostModel
    {
        /// <summary>
        /// 分类id
        /// </summary>
        [Required]
        public long categoryId { get; set; } = -1;

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
