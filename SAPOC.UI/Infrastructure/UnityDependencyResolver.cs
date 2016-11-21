using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAPOC.UI.Infrastructure
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        IUnityContainer unityContainer;
        public UnityDependencyResolver(IUnityContainer _unityContainer)
        {
            unityContainer = _unityContainer;
        }

        #region Override Methods Of 'IDependencyResolver'

        public object GetService(Type serviceType)
        {
            try
            {
                return unityContainer.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return unityContainer.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }

        #endregion
    }
}