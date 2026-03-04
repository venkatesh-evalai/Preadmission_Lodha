namespace Preadmission_Lodha.Models
{
    public class ClassMaster
    {
        public long SlNO { get; set; }
        public int org_Id { get; set; }
        public int academic_Id { get; set; }
        public int status { get; set; }
        public int classType_Id { get; set; }
        public string? academic_Year { get; set; }
        public int cls_Id { get; set; }
        public int class_Id { get; set; }
        public string? short_Name { get; set; }
        public string? class_Name { get; set; }
        public string? class_Description { get; set; }
        public int is_Active { get; set; }
        public string? user_Name { get; set; }
        public string? ip_Address { get; set; }
        public string? mode { get; set; }
        public string? category_Code { get; set; }
    }
}
