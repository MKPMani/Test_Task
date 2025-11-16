using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace User.Core.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
