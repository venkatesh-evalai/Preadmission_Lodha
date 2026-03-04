namespace Preadmission_Lodha.Models
{
    public class OnlinePayment
    {
        public string testurl { get; set; }
        public int lead_Id { get; set; }
        public string hashString { get; set; }
        public string url { get; set; }
        public string response_status { get; set; }
        public string response_code { get; set; }
        public string response_message { get; set; }
        public int org_id { get; set; }
        public int academic_Id { get; set; }
        public int event_id { get; set; }
        public string hash { get; set; }
        public int payEnable { get; set; }
        public string merchent_id { get; set; }
        public string security_id { get; set; }
        public string checksum_key { get; set; }
        public int transId { get; set; }
        public string payment_status_Id { get; set; }
        public int status { get; set; }
        public int student_Id { get; set; }
        //############## TRACK N PAY PARAMETERS ######################
        public string provider { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string StudentName { get; set; }
        public string customerName { get; set; }
        public string org_Name { get; set; }
        public int org_Id { get; set; }
        public string mobileNumber { get; set; }
        public string zipCode { get; set; }
        public string splitInfo { get; set; }
        public string amount { get; set; }
        public string apiKey { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string mode { get; set; }
        public string order_id { get; set; }
        public string razorpayorderId { get; set; }
        public string phone { get; set; }
        public string return_url { get; set; }
        public string state { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        //public string zip_code { get; set; }
        public string salt { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public int is_split { get; set; }
        public string vendor_code1 { get; set; }
        public string vendor_code2 { get; set; }
        public string split_type1 { get; set; }
        public string split_type2 { get; set; }
        public string URLAuth1 { get; set; }
        public List<vendorCode> vendorCodes { get; set; }

        public string admission_No { get; set; } //new
                                                 // public string application_Form_No { get; set; }
        public List<CrudFeeReceiptModel> Receipts { get; set; }
    }
    public class vendorCode
    {
        public string vendor_code { get; set; }
        public string split_amount_fixed { get; set; }
    }
}
