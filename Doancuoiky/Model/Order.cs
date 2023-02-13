using System;
using System.Collections.Generic;
using System.Text;

namespace Doancuoiky.Model
{
    public class Order
    {
        public string OrderId { get; set; }
        public string Username { get; set; }

        public decimal TotalCost { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
