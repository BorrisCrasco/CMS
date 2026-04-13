using CMS.Application.Feature.Authentications.Menus.Dtos;
using CMS.Application.Feature.Authentications.Roles.Dtos;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Menus.Request
{
    public class GetMenu : IRequest<IResult<MenuDto>>
    {

        public Guid Id { get; set; }
    }
}
