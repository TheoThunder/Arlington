using Data.Repositories.Abstract;
using Data.Repositories.Static;
using Ninject.Modules;

namespace Web.Infrastructure.Ninject
{
    public class StaticRepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Data.Repositories.Abstract.IAppointmentSheet>().To<Data.Repositories.Static.StaticAppointmentSheetRepository>();
            Bind<Data.Repositories.Abstract.ILeadRepository>().To<Data.Repositories.Static.StaticLeadRepository>();
            Bind<Data.Repositories.Abstract.IUserRepository>().To<Data.Repositories.Static.StaticUserRepository>();
            Bind<Data.Repositories.Abstract.IZoneRepository>().To<Data.Repositories.Static.StaticZoneRepository>();
            Bind<Data.Repositories.Abstract.ICardRepository>().To<Data.Repositories.Static.StaticCardRepository>();
            Bind<Data.Repositories.Abstract.IRoleRepository>().To<Data.Repositories.Static.StaticRoleRepository>();
            Bind<Data.Repositories.Abstract.IAccountRepository>().To<Data.Repositories.Static.StaticAccountRepository>();
            Bind<Data.Repositories.Abstract.IEquipmentRepository>().To<Data.Repositories.Static.StaticEquipmentRepository>();
            Bind<Data.Repositories.Abstract.ILeadAccessRepository>().To<Data.Repositories.Static.StaticLeadAccessRepository>();
            
            

        }
    }
}