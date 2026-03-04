namespace Preadmission_Lodha.Models
{
    public class PaymentModel1
    {
        public int event_id { get; set; }
        public int transaction_Id { get; set; }
        public int org_Id { get; set; }
        public int academic_Id { get; set; }
        // public int ad_id { get; set; }
        // public int category_id { get; set; }
        //public int subcategory_id { get; set; }
        public string mobile_no { get; set; }

        public int amount { get; set; }
        public string Payment_Status { get; set; }
        public string email_id { get; set; }
        public int transaction_Amount { get; set; }
    }

}
