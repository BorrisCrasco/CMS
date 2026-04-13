using Lipip.Atomic.EntityFramework.Common.Authentications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Common.Audit
{
    public class AuditService(ICurrentUser currentUser) : IAuditService
    {

        public void ApplyAudit(DbContext context)
        {
            var entries = context.ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted);

            var now = DateTime.UtcNow;
            var user = currentUser.Username ?? "SYSTEM";

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                var type = entity.GetType();

                var createdDateProp = type.GetProperty("CreatedDate");
                var createdByProp = type.GetProperty("CreatedBy");
                var updatedDateProp = type.GetProperty("UpdatedDate");
                var updatedByProp = type.GetProperty("UpdatedBy");
                var isDeletedProp = type.GetProperty("IsDeleted");
                var deletedDateProp = type.GetProperty("DeletedDate");
                var deletedByProp = type.GetProperty("DeletedBy");

                if (entry.State == EntityState.Added)
                {
                    createdDateProp?.SetValue(entity, now);
                    createdByProp?.SetValue(entity, user);
                }

                if (entry.State == EntityState.Modified)
                {
                    updatedDateProp?.SetValue(entity, now);
                    updatedByProp?.SetValue(entity, user);
                }

                if (entry.State == EntityState.Deleted && isDeletedProp != null)
                {
                    entry.State = EntityState.Modified;

                    isDeletedProp.SetValue(entity, true);
                    deletedDateProp?.SetValue(entity, now);
                    deletedByProp?.SetValue(entity, user);
                }
            }
        }
    }
}
