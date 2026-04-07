using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Domain.Abstractions
{
    public abstract class SoftDeletableEntity : Entity
    {
        public bool IsDeleted { get; private set; }

        public DateTime? DeletedOn { get; private set; }

        public void SoftDelete()
        {
            if (IsDeleted) return;

            IsDeleted = true;
            DeletedOn = DateTime.Now;
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletedOn = null;
        }
    }
}
