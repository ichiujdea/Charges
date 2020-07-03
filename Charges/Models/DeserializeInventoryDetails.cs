using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Charges.Models
{

    public class Serializer
    {
        public T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }


    }


    [XmlRoot(ElementName = "inventoryDetails", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
    public class InventoryDetails
    {
        [XmlElement(ElementName = "inventoryId", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string InventoryId { get; set; }
        [XmlElement(ElementName = "status", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Status { get; set; }
        [XmlElement(ElementName = "inventoryType", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string InventoryType { get; set; }
        [XmlElement(ElementName = "quantity", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Quantity { get; set; }
        [XmlElement(ElementName = "dmx_equipment_mis", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_mis { get; set; }
        [XmlElement(ElementName = "dmx_equipment_description", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_description { get; set; }
        [XmlElement(ElementName = "dmx_equipment_organization_code", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_organization_code { get; set; }
        [XmlElement(ElementName = "dmx_equipment_subinventory_code", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_subinventory_code { get; set; }
        [XmlElement(ElementName = "dmx_ebs_replaced_instance", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_ebs_replaced_instance { get; set; }
        [XmlElement(ElementName = "dmx_equipment_part_num", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_part_num { get; set; }
        [XmlElement(ElementName = "dmx_equipment_item_id", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_item_id { get; set; }
    }

    [XmlRoot(ElementName = "activityDetails", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
    public class ActivityDetails
    {
        [XmlElement(ElementName = "activityId", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string ActivityId { get; set; }
        [XmlElement(ElementName = "resourceId", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string ResourceId { get; set; }
        [XmlElement(ElementName = "resourceInternalId", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string ResourceInternalId { get; set; }
        [XmlElement(ElementName = "date", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Date { get; set; }
        [XmlElement(ElementName = "apptNumber", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string ApptNumber { get; set; }
        [XmlElement(ElementName = "customerNumber", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string CustomerNumber { get; set; }
    }

    [XmlRoot(ElementName = "inventoryChanges", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
    public class InventoryChanges
    {
        [XmlElement(ElementName = "status", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Status { get; set; }
        [XmlElement(ElementName = "inventoryType", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string InventoryType { get; set; }
        [XmlElement(ElementName = "quantity", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Quantity { get; set; }
        [XmlElement(ElementName = "dmx_equipment_mis", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_mis { get; set; }
        [XmlElement(ElementName = "dmx_equipment_description", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_description { get; set; }
        [XmlElement(ElementName = "dmx_equipment_organization_code", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_organization_code { get; set; }
        [XmlElement(ElementName = "dmx_equipment_subinventory_code", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_subinventory_code { get; set; }
        [XmlElement(ElementName = "dmx_ebs_replaced_instance", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_ebs_replaced_instance { get; set; }
        [XmlElement(ElementName = "dmx_equipment_part_num", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_part_num { get; set; }
        [XmlElement(ElementName = "dmx_equipment_item_id", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Dmx_equipment_item_id { get; set; }
    }

    [XmlRoot(ElementName = "items", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
    public class Items
    {
        [XmlElement(ElementName = "eventType", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string EventType { get; set; }
        [XmlElement(ElementName = "time", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Time { get; set; }
        [XmlElement(ElementName = "user", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string User { get; set; }
        [XmlElement(ElementName = "inventoryDetails", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public InventoryDetails InventoryDetails { get; set; }
        [XmlElement(ElementName = "activityDetails", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public ActivityDetails ActivityDetails { get; set; }
        [XmlElement(ElementName = "inventoryChanges", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public InventoryChanges InventoryChanges { get; set; }
    }

    [XmlRoot(ElementName = "response-wrapper", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
    public class Responsewrapper
    {
        [XmlElement(ElementName = "found", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string Found { get; set; }
        [XmlElement(ElementName = "nextPage", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public string NextPage { get; set; }
        [XmlElement(ElementName = "items", Namespace = "http://xmlns.oracle.com/cloud/adapter/REST/ExecuteSubs/types")]
        public Items Items { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

}
