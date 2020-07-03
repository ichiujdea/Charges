using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Models
{
    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
        public bool? templated { get; set; }
    }

    public class AdminVisibleInterfaces
    {
        public List<Link> links { get; set; }
    }

    public class Attributes
    {
        public bool disabled { get; set; }
        public bool hasSerialNumber { get; set; }
        public bool isSalesProduct { get; set; }
        public bool isServiceProduct { get; set; }
    }

    public class ItemStatus
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class C
    {
        public string billing_type { get; set; }
        public string inventory_category { get; set; }
        public ItemStatus item_status { get; set; }
        public bool track_in_install_base { get; set; }
        public string unit_of_measure { get; set; }
        public string user_item_type { get; set; }
        public string ebs_item_number { get; set; }
        public object ebs_item_id { get; set; }
    }

    public class CustomFields
    {
        public C c { get; set; }
    }

    public class Link2
    {
        public string rel { get; set; }
        public string href { get; set; }
        public bool? templated { get; set; }
    }

    public class Descriptions
    {
        public List<Link2> links { get; set; }
    }

    public class Folder
    {
        public int id { get; set; }
        public string lookupName { get; set; }
        public List<object> parents { get; set; }
    }

    public class Link3
    {
        public string rel { get; set; }
        public string href { get; set; }
        public bool? templated { get; set; }
    }

    public class Names
    {
        public List<Link3> links { get; set; }
    }

    public class Link4
    {
        public string rel { get; set; }
        public string href { get; set; }
        public bool? templated { get; set; }
    }

    public class Schedules
    {
        public List<Link4> links { get; set; }
    }

    public class Link5
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class SalesProduct
    {
        public int id { get; set; }
        public string lookupName { get; set; }
        public DateTime updatedTime { get; set; }
        public int acceptCount { get; set; }
        public AdminVisibleInterfaces adminVisibleInterfaces { get; set; }
        public Attributes attributes { get; set; }
        public CustomFields customFields { get; set; }
        public Descriptions descriptions { get; set; }
        public int displayOrder { get; set; }
        public Folder folder { get; set; }
        public string name { get; set; }
        public Names names { get; set; }
        public string partNumber { get; set; }
        public int respondCount { get; set; }
        public Schedules schedules { get; set; }
        public object serviceProduct { get; set; }
        public List<Link5> links { get; set; }
    }


    // This section holds the results of a ROQL item query. We iterate thru this to bring back the full SalesProduct Item above. 
    public class Item
    {
        public int id { get; set; }
        public string lookupName { get; set; }
        public DateTime updatedTime { get; set; }
    }

    public class ROQLProds
    {
        public List<Item> items { get; set; }
        public bool hasMore { get; set; }
    }

}
