using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base
{
    public abstract class BaseEntity
    {

    }
    public abstract class BaseEntity<TKey> : BaseEntity
    {
        public TKey Id { get; set; }
    }
}
