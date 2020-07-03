using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Models
{
    public class BillTo
    {
        public int? cust_account_id { get; set; }
        public string account_number { get; set; }
        public string customer_name { get; set; }
        public long cust_acct_site_id { get; set; }
        public long party_site_id { get; set; }
        public string party_site_number { get; set; }
        public int location_id { get; set; }
        public string address1 { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string postal_plus4_code { get; set; }
        public string state { get; set; }
        public string county { get; set; }
        public string country { get; set; }
    }

    public class BillToAddresses
    {
        public List<BillTo> BillTo { get; set; }
        public string overall_status { get; set; }
        public string overall_diagnostics { get; set; }
        public string database { get; set; }
    }
}
