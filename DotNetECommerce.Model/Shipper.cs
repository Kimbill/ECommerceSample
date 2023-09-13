using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Model
{
    public class Shipper : BaseEntity
    {
        public string ShipperName { get; set; }
        public string PhoneNumber { get; set; }
        //public string ShipperEmail { get; set; }
    }
}
