using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace Sdao.AppModel.API.Convention
{
    /// <summary>
    /// // ApiExplorerGroupPerVersionConvention.cs
    /// </summary>
    public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
    {
        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="controller"></param>
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace; // e.g. "Controllers.V1"
            var apiVersion = controllerNamespace.Split('.').Last().ToLower();

            if (apiVersion != "controllers") controller.ApiExplorer.GroupName = apiVersion;
        }
    }
}
