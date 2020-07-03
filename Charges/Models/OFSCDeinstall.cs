using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Models
{
    public class OFSCDeinstallItem
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
        public string dmx_equipment_mis { get; set; }
        public string dmx_ebs_item_instance { get; set; }
        public string dmx_inventory_cust_siteid { get; set; }
        public string dmx_return_to_truck { get; set; }
        public string dmx_inventory_activity_type { get; set; }
        public string dmx_equipment_asset_id { get; set; }
        public string serialNumber { get; set; }
    }

    public class OFSCDeinstallLink
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class OFSCDeinstall
    {
        public int totalResults { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public List<OFSCDeinstallItem> items { get; set; }
        public List<OFSCDeinstallLink> links { get; set; }
    }
}
