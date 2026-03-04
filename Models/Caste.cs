namespace Preadmission_Lodha.Models
{
    public class Caste
    {
        public int org_Id { get; set; }
        public int caste_Id { get; set; }
        public int subcaste_Id { get; set; }
        public string caste_Code { get; set; }
        public string subcaste_Code { get; set; }
        public string caste_Name { get; set; }
        public string subcaste_Name { get; set; }
        public int scheduled_Tribe { get; set; }
        public int is_Active { get; set; }
        public string user_Name { get; set; }
        public string ip_Address { get; set; }
        public string mode { get; set; }
        public long SINO { get; set; }
    }
}
