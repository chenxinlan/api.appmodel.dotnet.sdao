using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Sdao.AppModel.API.Models;
using Sdao.AppModel.Data.Abstract;
using Sdao.AppModel.Model.Entities;

namespace Sdao.AppModel.API.Controllers
{
    /// <summary>
    /// ����������
    /// </summary>
    [Route("api/[controller]")]
    public class ContainerController : Controller
    {
        /// <summary>
        /// container�ӿ�
        /// </summary>
        private IContainerRepository _containerRepository;

      
        /// <summary>
        /// ��־
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="containerRepository">//ʵ������(�ӿ�==>ʵ����)</param>
        /// <param name="loggerFactory">loggerFactory</param>
        public ContainerController(IContainerRepository containerRepository, ILoggerFactory loggerFactory)
        {
            _containerRepository = containerRepository;
            _logger = loggerFactory.CreateLogger<ContainerController>();
        }

        /// <summary>
        /// ��ȡ����Container��Ϣ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (null == _containerRepository || _containerRepository.Count() == 0)
                {
                    return NoContent();
                }

                IEnumerable<Container> _containers = _containerRepository.GetAll().Where(t => t.isdelete == 0).ToList();

                if (_containers == null || _containers.Count() == 0)
                {
                    return NoContent();
                }

                var viewModel = Mapper.Map<IEnumerable<Container>, IEnumerable<ContainerViewModel>>(_containers);

                return Ok(viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// ����Id��ȡ����
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}", Name = "GetContainerById")]
        public IActionResult GetContainerById(long Id)
        {
            try
            {
                if (null == _containerRepository || _containerRepository.Count() == 0)
                {
                    return NoContent();
                }

                var _container = _containerRepository.GetSingle(Id);

                if (_container == null || _container.isdelete==1)
                {
                    return NotFound(Id);
                }
                var viewModel = Mapper.Map<Container, ContainerViewModel>(_container);
                return Ok(viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

           
        }


        /// <summary>
        /// ���ݷ���id��ȡ��������
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("GetContainersByCategoryId/{categoryId:long}")]
        public IActionResult GetContainersByCategoryId(long categoryId)
        {
            try
            {
                if (null == _containerRepository || _containerRepository.Count() == 0)
                {
                    return NoContent();
                }
                

                var _containers = _containerRepository.FindBy(t => t.categoryId == categoryId && t.isdelete == 0);
                if (_containers != null && _containers.Count() > 0)
                {
                    var viewModel = Mapper.Map<IEnumerable<Container>,IEnumerable<ContainerViewModel>>(_containers);
                    return Ok(viewModel);
                }
                else {
                    return NotFound(categoryId);
                }
                
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            
        }

        /// <summary>
        /// ����Token��ȡContainers
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet("GetContainersByToken")]
        [ApiExplorerSettings(GroupName = "v2")]
        public IActionResult GetContainersByToken()
        {
            try
            {
                if (null == _containerRepository || _containerRepository.Count() == 0)
                {
                    return NoContent();
                }

                string userId = "1000";

                var _containers = _containerRepository.FindBy(t => t.createuserid == userId && t.isdelete == 0);

                if (_containers == null || _containers.Count() == 0)
                {
                    return NoContent();
                }

                var viewModel = Mapper.Map<IEnumerable<Container>, IEnumerable<ContainerViewModel>>(_containers);

                return Ok(viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// ����Ӫ��Ա����Container
        /// </summary>
        /// <param name="containerPostModel"></param>
        /// <returns></returns>
        [HttpPost("CreateByToken")]
        [ApiExplorerSettings(GroupName = "v2")]
        public IActionResult CreateByToken([FromBody]ContainerPostModel containerPostModel)
        {
            try
            {
                if (containerPostModel == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (containerPostModel.categoryId == -1) return BadRequest("categoryId ����Ϊ��");

                string userId = "1000";
                
                Container container = Mapper.Map<ContainerPostModel, Container>(containerPostModel);
                container.createuserid = userId;
                container.updateuserid = userId;
                container.isdelete = 0;

                _containerRepository.Add(container);
                _containerRepository.Commit();

                var containerViewModel = Mapper.Map<Container, ContainerViewModel>(container);

                CreatedAtRouteResult result = CreatedAtRoute("GetContainerById", new { controller = "Container", Id = container.id }, containerViewModel);

                return result;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// �޸�Container
        /// </summary>
        /// <param name="containerPutModel"></param>
        /// <returns></returns>
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v2")]
        public IActionResult Put([FromBody] ContainerPutModel containerPutModel)
        {

            try
            {
                if (containerPutModel == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var container = _containerRepository.GetSingle(containerPutModel.id);


                if (container == null || container.isdelete == 1)
                {
                    return NotFound(containerPutModel.id);
                }
                else
                {
                    string userId = "1000";
                    if(container.categoryId!=-1) container.categoryId = containerPutModel.categoryId;
                    container.containerName = containerPutModel.containerName;
                    container.json = containerPutModel.json;
                    container.updateuserid = userId;
                    _containerRepository.Update(container);
                    _containerRepository.Commit();

                    var containerViewModel = Mapper.Map<Container, ContainerViewModel>(container);

                    return Ok(containerViewModel);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// ͨ��Idɾ��property
        /// </summary>
        /// <param name="id">���</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ApiExplorerSettings(GroupName = "v2")]
        public IActionResult Delete(long id)
        {

            try
            {
                var _container = _containerRepository.GetSingle(id);
                if (_container == null || _container.isdelete == 1)
                {
                    return NotFound(id);
                }
                else
                {

                    _container.isdelete = 1;
                    _containerRepository.Commit();

                    return Ok(true);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}