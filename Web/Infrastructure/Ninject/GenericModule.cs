using Ninject.Modules;
using Web.Service;
using Web.Service.Abstract;

namespace Web.Infrastructure.Ninject
{
    /// <summary>
    /// This guy doesn't care what database is used.
    /// </summary>
    public class GenericModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILeadProfileService>().To<LeadProfileService>();
        }
    }
}