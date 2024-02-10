﻿using Domain.Entities;

namespace Domain.Contracts
{
    public abstract class AuditableEntity : BaseEntity
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; private set; }
        public Guid LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Guid? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        protected AuditableEntity()
        {
            CreatedOn = DateTime.UtcNow;
            LastModifiedOn = DateTime.UtcNow;
            IsDeleted = false;
        }
    }
}
