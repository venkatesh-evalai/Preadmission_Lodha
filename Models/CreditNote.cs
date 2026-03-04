namespace Preadmission_Lodha.Models
{
    public class CreditNote
    {
        public int receipt_Id { get; set; }
        public int orgId { get; set; }
        public int academic_Id { get; set; }
        public int receiptMode { get; set; }
        public string chequeNumber { get; set; }
        public string referenceCode { get; set; }
        public string ddNumber { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string branch_Name { get; set; }
        public int receipt_Code { get; set; }
        public long SI_No { get; set; }
        public int studentId { get; set; }
        public int student_Id { get; set; }
        public int class_Id { get; set; }
        public int section_Id { get; set; }
        public int cancel_Status { get; set; }
        public decimal amount { get; set; }
        public decimal receipt_Amount { get; set; }
        public decimal advance_Amount { get; set; }
        public DateTime receiptDate { get; set; }
        public DateTime receipt_Date { get; set; }
        public DateTime chequeDate { get; set; }
        public DateTime paymentDate { get; set; }
        public DateTime ddDate { get; set; }
        public string class_Name { get; set; }
        public string PaymentName { get; set; }
        public string receipt_Mode { get; set; }
        public string reference_Code { get; set; }
        public string bank_Name { get; set; }
        public string Bank_branch_Name { get; set; }
        public string section_Name { get; set; }
        public string admission_No { get; set; }
        public string student_Name { get; set; }
        public string user_Name { get; set; }
        public string ip_Address { get; set; }
        public string mode { get; set; }
        public int quota_id { get; set; }
        public string quota_name { get; set; }
        public string father_Name { get; set; }
        public int newer_status { get; set; }
        public string mother_Name { get; set; }
        //public string admission_No { get; set; }
    }
}
