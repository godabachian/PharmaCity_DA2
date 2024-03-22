using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PharmaCity.BusinessLogic;
using PharmaCity.BusinessLogic.Tools;
using PharmaCity.DataAccess;
using PharmaCity.DataAccess.Context;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;
using PharmaCity.IMechanism;
using PharmaCity.MechanismExternal;
using System;

namespace PharmaCity.Factory
{
    public class FactoryService
    {
        public readonly IServiceCollection _serviceCollection;

        public FactoryService(IServiceCollection serviceCollection)
        {
            this._serviceCollection = serviceCollection;
        }

        public void AddDbContextService()
        {
            var connection = "Data Source=localhost; Initial Catalog=PharmaCityDb; Integrated Security=True";

            _serviceCollection.AddDbContext<PharmaCityDbContext>(options => options.UseSqlServer(connection));
        }

        public void AddServices()
        {
            _serviceCollection.AddScoped<IUserService, UserService>();
            _serviceCollection.AddScoped<IUserRepository, UserRepository>();

            _serviceCollection.AddScoped<IMedicineService, MedicineService>();
            _serviceCollection.AddScoped<IMedicineRepository, MedicineRepository>();

            _serviceCollection.AddScoped<IGuidService, GuidService>();

            _serviceCollection.AddScoped<IInvitationService, InvitationService>();
            _serviceCollection.AddScoped<IInvitationRepository, InvitationRepository>();

            _serviceCollection.AddScoped<ISessionService, SessionService>();

            _serviceCollection.AddScoped<IPharmacyRepository, PharmacyRepository>();
            _serviceCollection.AddScoped<IPharmacyService, PharmacyService>();

            _serviceCollection.AddScoped<IStockRequestRepository, StockRequestRepository>();
            _serviceCollection.AddScoped<IStockRequestService, StockRequestService>();

            _serviceCollection.AddScoped<IShoppingRepository, ShoppingRepository>();
            _serviceCollection.AddScoped<IShoppingService, ShoppingService>();

            _serviceCollection.AddScoped<IPetitionRepository, PetitionRepository>();

            _serviceCollection.AddScoped<IExportService, ExportService>();
            _serviceCollection.AddScoped<IConcreteMechanism, MechanismJSON>();
        }

        
    }
}
