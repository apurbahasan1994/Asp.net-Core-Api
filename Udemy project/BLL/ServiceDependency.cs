using BLL.Request;
using BLL.Service;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public static class ServiceDependency
    {
        public static void AllDependency(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDepartmentService,DepartmentService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<INewDepService, NewDepService>();
            services.AddTransient<INewStudService, NewStuService>();
            FluentValidationService(services, configuration);

        }

        private static void FluentValidationService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IValidator<DepartmentViewModelInsert>, DepartmentValidator>();
            services.AddTransient<IValidator<StudentViewModelInsert>, StudentValidator>();
           
        }
    }
}
