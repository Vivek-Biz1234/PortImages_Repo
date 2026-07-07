using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Auth.AuthEmployee.Interfaces;
using PORTIMAGES.Application.Auth.AuthUser.Interfaces;
using PORTIMAGES.Application.BrokerConsignee.Interfaces;
using PORTIMAGES.Application.Common.Interfaces;
using PORTIMAGES.Application.Menu.Interfaces;
using PORTIMAGES.Application.Products.Interfaces;
using PORTIMAGES.Application.Ship.Interfaces;
using PORTIMAGES.Application.User.Interfaces;
using PORTIMAGES.Common.Interfaces.Excel;
using PORTIMAGES.Infrastructure.Common.Repositories;
using PORTIMAGES.Infrastructure.Persistence;
using PORTIMAGES.Infrastructure.Repositories.Admin;
using PORTIMAGES.Infrastructure.Repositories.Auth.AuthEmployee;
using PORTIMAGES.Infrastructure.Repositories.Auth.AuthUser;
using PORTIMAGES.Infrastructure.Repositories.Auth.User;
using PORTIMAGES.Infrastructure.Repositories.User;
using PORTIMAGES.Infrastructure.Services.Excel;
namespace PORTIMAGES.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {           
            services.AddSingleton<IDapperRepository, DapperRepository>(); 
            services.AddScoped<IEmployeeRepository, EmployeeRepository>(); 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IMenuRepository,MenuRepository>();
            services.AddScoped<ITerminalRepository,TerminalRepository>();
            services.AddScoped<IInventoryStatusRepository, InventoryStatusRepository>();
            services.AddScoped<IUserMasterRepository, UserMasterRepository>();
            services.AddScoped<IEmployeeMasterRepository, EmployeeMasterRepository>();
            services.AddScoped<IVehicleStatusRepository, VehicleStatusRepository>();
            services.AddScoped<IINSDestinationRepository, INSDestinationRepository>();
            services.AddScoped<IINSStatusRepository, INSStatusRepository>();
            services.AddScoped<IINSOrganizationRepository, INSOrganizationRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IDropdownRepository, DropdownRepository>();
            services.AddScoped<IShipRepository, ShipRepository>();
            services.AddScoped<IShipTypeRepository, ShipTypeRepository>();
            services.AddScoped<IShipUseRepository, ShipUseRepository>();
            services.AddScoped<IPortRepository, PortRepository>();
            services.AddScoped<IShippingRepository, ShippingRepository>();
            services.AddScoped<IMakerRepository, MakerRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserProductRepository, UserProductRepository>();
            services.AddScoped<IInquiryRepository, InquiryRepository>();
            services.AddScoped<IProductFilesRepository, ProductFilesRepository>();
            services.AddScoped<IImageDownloadRepository, ImageDownloadRepository>();
            services.AddScoped<IMenusRepository, MenusRepository>();
            services.AddScoped<IConsigneeMasterRepository,ConsigneeMasterRepository>();
            services.AddScoped<IBrokerMasterRepository,BrokerMasterRepository>();
            services.AddScoped<IProductAdditionalInfoRepository, ProductAdditionalInfoRepository>();
            services.AddScoped<IImportTemplateService, ImportTemplateService>();
            services.AddScoped<IExcelImportValidatorService, ExcelImportValidatorService>();
            services.AddScoped<IExcelImportService, ExcelImportService>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
