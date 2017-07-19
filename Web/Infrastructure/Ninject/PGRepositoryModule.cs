using Data.Repositories.Abstract;
using Data.Repositories.Postgres;
using Ninject.Modules;

namespace Web.Infrastructure.Ninject
{
    public class PGRepositoryModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<Data.Repositories.Abstract.ILeadRepository>().To<Data.Repositories.Static.StaticLeadRepository>();
            Bind<Data.Repositories.Abstract.ILeadRepository>().To<Data.Repositories.Postgres.PGLeadRepository>();
            Bind<IUserRepository>().To<PGUserRepository>();
            Bind<Data.Repositories.Abstract.IAppointmentSheet>().To<Data.Repositories.Postgres.PGAppointmentSheetRepository>();
            Bind<Data.Repositories.Abstract.ICardRepository>().To<Data.Repositories.Postgres.PGCardRepository>();
            Bind<Data.Repositories.Abstract.IRoleRepository>().To<Data.Repositories.Static.StaticRoleRepository>();
            Bind<Data.Repositories.Abstract.IAccountRepository>().To<Data.Repositories.Postgres.PGAccountRepository>();
            Bind<Data.Repositories.Abstract.ILeadAccessRepository>().To<Data.Repositories.Static.StaticLeadAccessRepository>();
            Bind<Data.Repositories.Abstract.IEquipmentRepository>().To<Data.Repositories.Postgres.PGEquipmentRepository>();
            Bind<Data.Repositories.Abstract.IThresholdRepository>().To<Data.Repositories.Postgres.PGThresholdRepository>();
            Bind<Data.Repositories.Abstract.IZoneRepository>().To<Data.Repositories.Postgres.PGZoneRepository>();
            Bind<Data.Repositories.Abstract.IGenericUsageRepositoryInterface>().To<Data.Repositories.Postgres.PGIncodeQueriesRepository>();
            Bind<Data.Repositories.Abstract.ICalendarEventRepository>().To<Data.Repositories.Postgres.PGEventRepository>();
            Bind<Data.Repositories.Abstract.ITicketRepository>().To<Data.Repositories.Postgres.PGTicketRepository>();
            Bind<Data.Repositories.Abstract.IUserZoneRepository>().To<Data.Repositories.Postgres.PGUserZoneRepository>();
            Bind<Data.Repositories.Abstract.ITicketHistoryRepository>().To<Data.Repositories.Postgres.PGTicketHistoryRepository>();
            Bind<Data.Repositories.Abstract.ITimeSlotRepository>().To<Data.Repositories.Postgres.PGTimeSlotRepository>();
            // These are for Reports
            Bind<Data.Repositories.Abstract.IAAPSReportRepository>().To<Data.Repositories.Postgres.PGAAPSReportRepository>();
            Bind<Data.Repositories.Abstract.ISAPSReportRepository>().To<Data.Repositories.Postgres.PGSAPSReportRepository>();
            Bind<Data.Repositories.Abstract.ITicketReportRepository>().To<Data.Repositories.Postgres.PGTicketReportRepository>();
            Bind<Data.Repositories.Abstract.IGroupPPReportRepository>().To<Data.Repositories.Postgres.PGGroupPPReportRepository>();
            Bind<Data.Repositories.Abstract.IMonthlyVolumeReportRepository>().To<Data.Repositories.Postgres.PGMonthlyVolumeReportRepository>();
            // This one is for dashboard
            Bind<Data.Repositories.Abstract.IDashboardRepository>().To<Data.Repositories.Postgres.PGDashboardRepository>();

            // This one is for PhoneSystem
            Bind<Data.Repositories.Abstract.IPhoneUserRepository>().To<Data.Repositories.Postgres.PGPhoneUserRepository>();
        }
    }
}