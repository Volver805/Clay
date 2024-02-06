using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Clay.Domain.Entities
{
    public class BaseEntity  
    {
        [JsonIgnore]
        public int ID { get; set; }
    }
}
