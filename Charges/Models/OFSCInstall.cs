using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Models
{
    public class OFSCInstallItem
    {
        public int inventoryId { get; set; }
        public string status { get; set; }
        public string inventoryType { get; set; }
        public int quantity { get; set; }
        public string resourceId { get; set; }
        public int resourceInternalId { get; set; }
        public int activityId { get; set; }
        public string dmx_equipment_part_num { get; set; }
        public string dmx_equipment_description { get; set; }
        public string dmx_equipment_subinventory_code { get; set; }
        public string dmx_equipment_organization_code { get; set; }
        public string dmx_equipment_item_id { get; set; }
    }

    public class OFSCInstallLink
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class OFSCInstall
    {
        public int totalResults { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public List<OFSCInstallItem> items { get; set; }
        public List<OFSCInstallLink> links { get; set; }
    }
}
