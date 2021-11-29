using BLL.Service;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Request
{
    public class DepartmentViewModelInsert
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class DepartmentValidator : AbstractValidator<DepartmentViewModelInsert>
    {
        private readonly IServiceProvider _serviceProvider;

        public DepartmentValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(25).MustAsync(NameExist).WithMessage("Name already exist");
            RuleFor(x => x.Code).NotNull().NotEmpty().MinimumLength(3).MaximumLength(20).MustAsync(CodeExist).WithMessage("Code already exist");

        }

        private async  Task<bool> CodeExist(string Code, CancellationToken arg2)
        {
            if (String.IsNullOrEmpty(Code))
            {
                return true;
            }
            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            return await requiredService.CodeExist(Code);
        }

        private async Task<bool> NameExist(string Name, CancellationToken arg2)
        {
            if (String.IsNullOrEmpty(Name))
            {
                return true;
            }
            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            return await requiredService.NameExist(Name);
        }
    }
}
