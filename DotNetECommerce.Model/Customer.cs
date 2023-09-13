using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Model
{
    public class Customer : BaseEntity
    {
        public string CustomerName { get; set; }
        public string Location { get; set; }
    }
}
