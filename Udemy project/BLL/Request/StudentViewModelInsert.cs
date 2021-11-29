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
    public class StudentViewModelInsert
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class StudentValidator : AbstractValidator<StudentViewModelInsert>
    {
        private readonly IServiceProvider _serviceProvider;

        public StudentValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(25);
            RuleFor(x => x.Email).NotNull().NotEmpty().MinimumLength(8).MaximumLength(20).MustAsync(EmailExist).WithMessage("Email already exist");

        }

        private async Task<bool> EmailExist(string email, CancellationToken arg2)
        {
            if(string.IsNullOrEmpty(email))
            {
                return true;
            }
            var requiredService = _serviceProvider.GetRequiredService<IStudentService>();
            return await requiredService.EmailExist(email);
        }
    }
}
