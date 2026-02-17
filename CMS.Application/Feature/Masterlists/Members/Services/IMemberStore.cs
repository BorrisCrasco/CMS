using Cms.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Members.Services
{
    public interface IMemberStore
    {
        Task Create(Member member, CancellationToken cancellation = default);
        Task<IEnumerable<VwMember>> Gets(CancellationToken cancellation = default);
        IQueryable<VwMember> Query();
        Task<VwMember?> Get(Guid id, CancellationToken cancellation = default);
        Task<Member?> GetForUpdate(Guid id, CancellationToken cancellation = default);
        Task<IEnumerable<Gender>> GetGenders(CancellationToken cancellation = default);

        Task<Gender?> GetGender(int id, CancellationToken cancellation = default);

    }
}
