using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Model
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public int ShipperId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
