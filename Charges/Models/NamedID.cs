using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Models
{
    public class NamedIDLink
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class NamedID
    {
        public int id { get; set; }
        public string lookupName { get; set; }
        public int DisplayOrder { get; set; }
        public string Name { get; set; }
        public List<NamedIDLink> links { get; set; }
    }
}
