using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Models
{
    public class AssignedTo
    {
        public object account { get; set; }
        public object staffGroup { get; set; }
    }

    public class Banner
    {
        public object importanceFlag { get; set; }
        public object text { get; set; }
        public object updatedByAccount { get; set; }
        public object updatedTime { get; set; }
    }

    public class IncidentLink
    {
        public string rel { get; set; }
        public string href { get; set; }
        public bool? templated { get; set; }
    }

    public class BilledMinutes
    {
        public List<IncidentLink> links { get; set; }
    }

    public class IncidentLink2
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class Category
    {
        public List<IncidentLink2> links { get; set; }
    }

    public class IncidentLink3
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class CreatedByAccount
    {
        public List<IncidentLink3> links { get; set; }
    }

    public class IncidentLink4
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class OFSCLocked
    {
        public List<IncidentLink4> links { get; set; }
    }

    public class IncidentLink5
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class WorkZone
    {
        public List<IncidentLink5> links { get; set; }
    }

    public class MoodCustom
    {
        public object Access_Hours { get; set; }
        public bool Amplifier_Photo { get; set; }
        public object Deposit_Waiver_No { get; set; }
        public bool Dish_Photo { get; set; }
        public object EBS_Update_Date { get; set; }
        public object Field_Service_Resource { get; set; }
        public object General_Followup_Date { get; set; }
        public object Incident_Child_1 { get; set; }
        public object Incident_Child_2 { get; set; }
        public object Incident_Child_3 { get; set; }
        public object Incident_Child_4 { get; set; }
        public object Incident_Child_5 { get; set; }
        public object Install_Rev_Forecast_Date { get; set; }
        public object Install_Revenue_Amt { get; set; }
        public object Install_Revenue_Equipment_Amt { get; set; }
        public OFSCLocked OFSC_Locked { get; set; }
        public object PO_Amount { get; set; }
        public object PO_Number { get; set; }
        public object Parent_Incident_ID { get; set; }
        public object Project_Owner { get; set; }
        public object Skill_1 { get; set; }
        public object Skill_2 { get; set; }
        public object Skill_3 { get; set; }
        public object Skill_4 { get; set; }
        public object Skill_5 { get; set; }
        public bool Store_Stamp { get; set; }
        public object SubCo_PO_Amount { get; set; }
        public object SubCo_iSup_PO { get; set; }
        public bool Test_Pin_Photo { get; set; }
        public object Third_Party_Srv_Deadline_Date { get; set; }
        public bool Visual_Photo { get; set; }
        public WorkZone Work_Zone { get; set; }
        public object acct_verified { get; set; }
    }

    public class Incidents
    {
        public object Add_Time_Reason { get; set; }
        public string Market { get; set; }
        public object OFSC_Activity_ID { get; set; }
    }

    public class Integration
    {
        public bool FS_Create_Sent_Flag { get; set; }
        public object OFSC_Install_Deinstall { get; set; }
        public object OIC_Time { get; set; }
        public string Unsubmitted_Charges { get; set; }
        public object cc_email_addresses { get; set; }
        public bool charges_started_flag { get; set; }
        public string ebs_token { get; set; }
        public object inc_emailcontactat { get; set; }
    }

    public class IncidentType
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class Priority
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class IncidentC
    {
        public string business_process { get; set; }
        public object incident_closed_permanent { get; set; }
        public object group_email_box { get; set; }
        public DateTime incident_create_date { get; set; }
        public object reopen_date { get; set; }
        public IncidentType incident_type { get; set; }
        public object lio_locale { get; set; }
        public object ln_status { get; set; }
        public object last_status_update_date { get; set; }
        public Priority priority { get; set; }
        public object rush_market { get; set; }
        public object ebs_sales_order_update_date { get; set; }
        public object fs_client_damage_labor { get; set; }
        public object fs_device_type { get; set; }
        public object fs_dish_realignment_labor { get; set; }
        public object fs_dtoc_billable_labor { get; set; }
        public object fs_dtoc_warranty_labor { get; set; }
        public object fs_install_support_labor { get; set; }
        public object fs_issue_area { get; set; }
        public object fs_platform { get; set; }
        public object fs_site_survey_labor { get; set; }
        public object fs_source_labor { get; set; }
        public object fs_system_labor { get; set; }
        public object fs_tech_sales_labor { get; set; }
        public object fs_travel_time { get; set; }
        public object fs_work_order_signee { get; set; }
        public bool capture_drawing_updates { get; set; }
        public object fs_actual_travel_time { get; set; }
        public object additional_tech_time { get; set; }
        public bool confirm_manager_training { get; set; }
        public bool confirm_music_working { get; set; }
        public object fs_appointment_date_tz { get; set; }
        public object union_shop { get; set; }
        public object fs_appointment_date { get; set; }
        public bool confirm_video_player_connected { get; set; }
        public string created_by_group { get; set; }
        public bool do_not_route { get; set; }
        public bool confirm_video_working { get; set; }
        public object fs_duration { get; set; }
        public object estimated_hours { get; set; }
        public bool fs_work_order_flag { get; set; }
        public object multiday_duration { get; set; }
        public object service_eta_date { get; set; }
        public object preferrred_technician { get; set; }
        public object fs_projected_start_tz { get; set; }
        public object fs_projected_end_tz { get; set; }
        public object rollout_document { get; set; }
        public bool send_sales_order_detail_flag { get; set; }
        public object service_window_start { get; set; }
        public object service_window_end { get; set; }
        public DateTime sla_window_start { get; set; }
        public DateTime sla_window_end { get; set; }
        public object fs_start_time_tz { get; set; }
        public object fs_start_time { get; set; }
        public object fs_end_time_tz { get; set; }
        public object fs_end_time { get; set; }
        public object tech_resource_id { get; set; }
        public object tech_resource_name { get; set; }
        public object fs_time_of_assignment_tz { get; set; }
        public object fs_time_of_assignment { get; set; }
        public object fs_time_of_booking_tz { get; set; }
        public object fs_time_of_booking { get; set; }
        public object fs_workorder_record_type { get; set; }
    }

    public class IncidentCustomFields
    {
        public MoodCustom MoodCustom { get; set; }
        public Incidents Incidents { get; set; }
        public Integration Integration { get; set; }
        public IncidentC c { get; set; }
    }

    public class Link6
    {
        public string rel { get; set; }
        public string href { get; set; }
        public bool? templated { get; set; }
    }

    public class FileAttachments
    {
        public List<Link6> links { get; set; }
    }

    public class Link7
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class Interface
    {
        public List<Link7> links { get; set; }
    }

    public class Language
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class Link8
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class Mailbox
    {
        public List<Link8> links { get; set; }
    }

    public class Link9
    {
        public string rel { get; set; }
        public string href { get; set; }
        public bool? templated { get; set; }
    }

    public class MilestoneInstances
    {
        public List<Link9> links { get; set; }
    }

    public class Link10
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class Organization
    {
        public List<Link10> links { get; set; }
    }

    public class Link11
    {
        public string rel { get; set; }
        public string href { get; set; }
        public bool? templated { get; set; }
    }

    public class OtherContacts
    {
        public List<Link11> links { get; set; }
    }

    public class Link12
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class PrimaryContact
    {
        public List<Link12> links { get; set; }
    }

    public class Link13
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class Product
    {
        public List<Link13> links { get; set; }
    }

    public class Queue
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class ResponseEmailAddressType
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class Severity
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class NameOfSLA
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class StateOfSLA
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class SLAInstance
    {
        public string activeDate { get; set; }
        public string expireDate { get; set; }
        public int id { get; set; }
        public NameOfSLA nameOfSLA { get; set; }
        public int remainingFromChat { get; set; }
        public int remainingFromCSR { get; set; }
        public int remainingFromEmail { get; set; }
        public int remainingFromWeb { get; set; }
        public int remainingTotal { get; set; }
        public int sLASet { get; set; }
        public StateOfSLA stateOfSLA { get; set; }
    }

    public class Parent
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class Source
    {
        public int id { get; set; }
        public string lookupName { get; set; }
        public List<Parent> parents { get; set; }
    }

    public class Status
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class StatusType
    {
        public int id { get; set; }
        public string lookupName { get; set; }
    }

    public class StatusWithType
    {
        public Status status { get; set; }
        public StatusType statusType { get; set; }
    }

    public class Link14
    {
        public string rel { get; set; }
        public string href { get; set; }
        public bool? templated { get; set; }
    }

    public class Threads
    {
        public List<Link14> links { get; set; }
    }

    public class Link15
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string mediaType { get; set; }
    }

    public class Incident
    {
        public int id { get; set; }
        public string lookupName { get; set; }
        public DateTime createdTime { get; set; }
        public DateTime updatedTime { get; set; }
        public object asset { get; set; }
        public AssignedTo assignedTo { get; set; }
        public Banner banner { get; set; }
        public BilledMinutes billedMinutes { get; set; }
        public Category category { get; set; }
        public object channel { get; set; }
        public object chatQueue { get; set; }
        public object closedTime { get; set; }
        public CreatedByAccount createdByAccount { get; set; }
        public IncidentCustomFields customFields { get; set; }
        public object disposition { get; set; }
        public FileAttachments fileAttachments { get; set; }
        public DateTime initialResponseDueTime { get; set; }
        public object initialSolutionTime { get; set; }
        public Interface @interface { get; set; }
        public Language language { get; set; }
        public object lastResponseTime { get; set; }
        public object lastSurveyScore { get; set; }
        public Mailbox mailbox { get; set; }
        public object mailing { get; set; }
        public MilestoneInstances milestoneInstances { get; set; }
        public Organization organization { get; set; }
        public OtherContacts otherContacts { get; set; }
        public PrimaryContact primaryContact { get; set; }
        public Product product { get; set; }
        public Queue queue { get; set; }
        public string referenceNumber { get; set; }
        public object resolutionInterval { get; set; }
        public ResponseEmailAddressType responseEmailAddressType { get; set; }
        public object responseInterval { get; set; }
        public Severity severity { get; set; }
        public SLAInstance sLAInstance { get; set; }
        public int smartSenseCustomer { get; set; }
        public int smartSenseStaff { get; set; }
        public Source source { get; set; }
        public StatusWithType statusWithType { get; set; }
        public string subject { get; set; }
        public Threads threads { get; set; }
        public List<Link15> links { get; set; }
    }
}
