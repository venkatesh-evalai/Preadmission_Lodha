namespace Preadmission_Lodha.Models
{
    public class AcademicYear
    {
        public int org_Id { get; set; }
        public int academic_Id { get; set; }
        public int academic_Yrs_from { get; set; }
        public DateTime from_Date { get; set; }
        public string? from_Date1 { get; set; }
        public int academic_Yrs_To { get; set; }
        public DateTime to_Date { get; set; }
        public string? to_Date1 { get; set; }
        public int is_Active { get; set; }
        public string? mode { get; set; }
        public string? ip_Address { get; set; }
        public string? user_Name { get; set; }

        //new code
        public string? academic_Start_Year { get; set; }
        public string? academic_End_Year { get; set; }
        public string? academic_Year { get; set; }
        public int pre_Academic_Id { get; set; }
        public string? pre_Academic_Year { get; set; }
    }
}
