using DLL.Context;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL
{
    public static class DLLDependency
    {
        public static void  ALLDependency(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Defaultconnection"),
            serverOptions => serverOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository,DepartmentRepository>();
            services.AddTransient<INewIDepRepo, NewIDepRepo>();
            services.AddTransient<INewStuRepo, NewStuRepo>();

        }
    }
}
