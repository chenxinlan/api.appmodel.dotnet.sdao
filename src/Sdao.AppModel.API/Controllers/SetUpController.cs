using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Sdao.AppModel.Data;

namespace Sdao.AppModel.API.Controllers.V3
{
    /// <summary>
    /// 数据库初始化生成
    /// </summary>
    [Route("api/[controller]")]
    //[ApiExplorerSettings(GroupName = "v3")]
    public class SetUpController : Controller
    {
        /// <summary>
        /// 上下文DbContext
        /// </summary>
        private AppModelContext _ctx;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ctx"></param>
        public SetUpController(AppModelContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// 初始化:dotnet ef migrations add "initial"  如果没有migrations文件夹,则重新新生成
        /// </summary>
        /// <returns></returns>
        [Route("InitializeAsync")]
        [HttpGet]
        public async Task<IActionResult> InitializeAsync()
        {
            try
            {
                await _ctx.Database.MigrateAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}