using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Sdao.AppModel.Data;

namespace Sdao.AppModel.API.Controllers.V3
{
    /// <summary>
    /// ���ݿ��ʼ������
    /// </summary>
    [Route("api/[controller]")]
    //[ApiExplorerSettings(GroupName = "v3")]
    public class SetUpController : Controller
    {
        /// <summary>
        /// ������DbContext
        /// </summary>
        private AppModelContext _ctx;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="ctx"></param>
        public SetUpController(AppModelContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// ��ʼ��:dotnet ef migrations add "initial"  ���û��migrations�ļ���,������������
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