using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    [DataContract]
    public sealed class CustomerDto
    {
        [DataMember]
        public string Name { get; set; }
    }
}
