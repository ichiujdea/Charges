using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Models
{
    class Pricing
    {
        public string return_status { get; set; }
        public string error_message { get; set; }
        public string database { get; set; }
        public decimal list_price { get; set; }
        public decimal selling_price { get; set; }
        public string price_list { get; set; }
        public string currency_code { get; set; }
        public string contract_number { get; set; }
        public string contract_number_modifier { get; set; }
        public int list_header_id { get; set; }
        public string coverage_term_name { get; set; }
        public string contract_line_id { get; set; }
        public int contract_id { get; set; }
    }
}
