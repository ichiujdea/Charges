using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Models
{
    public class ReportResults
    {
        public int count { get; set; }
        public string name { get; set; }
        public List<string> columnNames { get; set; }
        public List<List<string>> rows { get; set; }
    }
}
