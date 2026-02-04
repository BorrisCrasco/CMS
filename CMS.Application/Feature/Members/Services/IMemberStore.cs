using Cms.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Members.Services
{
    public interface IMemberStore
    {
        Task Create(Member member, CancellationToken cancellation = default);
        Task<IEnumerable<Member>> Gets(CancellationToken cancellation = default);
        Task<Member?> Get(Guid id, CancellationToken cancellation = default);
        Task<Member?> GetForUpdate(Guid id, CancellationToken cancellation = default);
    }
}
