using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Models
{
    public class Error
    {
        public int line_number { get; set; }
        public string error_message { get; set; }
    }

    public class Order
    {
        public int line_number { get; set; }
        public int order_number { get; set; }
    }

    public class ChargeSubmission
    {
        public string overall_status { get; set; }
        public string overall_diagnostics { get; set; }
        public string database { get; set; }
        public List<Error> Errors { get; set; }
        public List<Order> Orders { get; set; }
    }
}
