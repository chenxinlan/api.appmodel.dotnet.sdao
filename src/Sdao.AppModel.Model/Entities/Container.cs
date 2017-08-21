using System;
using System.ComponentModel.DataAnnotations;

namespace Sdao.AppModel.Model.Entities
{
    /// <summary>
    /// 容器表
    /// </summary>
    //[Table("Container")]
    public class Container: IEntityBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        [Required]
        public long id { get; set; }

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
        
        #region 必要的参数
        /// <summary>
        /// 是否删除：1删除，0未删除
        /// </summary>
        public int isdelete { get; set; }

        /// <summary>
        /// 创建用户编号
        /// </summary>

        public string createuserid { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createtime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 更新用户编号
        /// </summary>
        public string updateuserid { get; set; }
        
        /// <summary>
        /// 更新时间
        /// </summary>
        public string updatetime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        #endregion
    }
}
