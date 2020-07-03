using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Models
{
    public class Lov
    {
        public string lov_name { get; set; }
        public List<string> lov_values { get; set; }
    }

    public class LovList
    {
        public string overall_status { get; set; }
        public string overall_diagnostics { get; set; }
        public string database { get; set; }
        public List<Lov> lovs { get; set; }
        public List<string>EntitlementNotRequired { get; set; }
    }
}
