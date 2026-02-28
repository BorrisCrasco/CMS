using CMS.Application.Feature.Authentications.Users.Request;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Users.Validation
{
    public class LoginUserValidation : AbstractValidator<LoginUser>
    {
        public LoginUserValidation()
        {

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required!");

        }
    }
}
