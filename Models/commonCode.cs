using System.Data;
using System.Reflection;

namespace Preadmission_Lodha.Models
{
    
    public class commonCode
    {
        public static string conStr;
        public static void Initialize(IConfiguration configuration)
        {
            conStr = configuration.GetConnectionString("DefaultConnection");
        }

        public static List<T> ConvertDataTable<T>(DataTable dt) where T : new()
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr) where T : new()
        {
            T obj = new T();
            Type type = typeof(T);

            foreach (DataColumn column in dr.Table.Columns)
            {
                PropertyInfo prop = type.GetProperty(column.ColumnName);

                if (prop != null && prop.CanWrite)
                {
                    object value = dr[column.ColumnName];

                    if (value == DBNull.Value)
                    {
                        prop.SetValue(obj, null);
                    }
                    else
                    {
                        try
                        {
                            Type targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            object safeValue = Convert.ChangeType(value, targetType);
                            prop.SetValue(obj, safeValue);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Property '{prop.Name}' conversion failed: {ex.Message}", ex);
                        }
                    }
                }
            }

            return obj;
        }
    }


    public class CommonModal
    {
        public string ResponseStatus { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public DataTable data { get; set; }
        public int data1 { get; set; }
        public string attachment_URL { get; set; }
    }

    public class CommonModalPO
    {
        public string ResponseStatus { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string data1 { get; set; }
        public List<int> particulars { get; set; }

    }

    public class CommonModal12
    {
        public string ResponseStatus { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public int data { get; set; }
    }
    public class CommonModal_ReportCard
    {
        public string ResponseStatus { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public CommonModal_sub_ReportCard data { get; set; }

    }
    public class CommonModal_sub_ReportCard
    {
        public DataTable result { get; set; }
        public string totalMark_Attain { get; set; }
        public string totalMax_Mark { get; set; }
        public string percentage_obtained { get; set; }
        public string overall_Grade { get; set; }
        public string is_Download { get; set; }
    }
    public class CommonModal_Diary
    {
        public string ResponseStatus { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public CommonModal_SubDiary data { get; set; }

    }

    public class CommonModal_MobileAppStudName
    {
        public DataTable admissionName { get; set; }
        public DataTable registrationName { get; set; }
    }
    public class CommonModal_SubDiary
    {
        public DataTable Jan { get; set; }
        public DataTable Feb { get; set; }
        public DataTable Mar { get; set; }
        public DataTable Apr { get; set; }
        public DataTable May { get; set; }
        public DataTable Jun { get; set; }
        public DataTable Jul { get; set; }
        public DataTable Aug { get; set; }
        public DataTable Sep { get; set; }
        public DataTable Oct { get; set; }
        public DataTable Nov { get; set; }
        public DataTable Dec { get; set; }

    }

    public class Month
    {
        public int month_Id { get; set; }
        public string month_Name { get; set; }
        public string short_Name { get; set; }
    }

    public class Address
    {
        public int country_Id { get; set; }
        public int state_Id { get; set; }
        public int city_Id { get; set; }

        public string country_Name { get; set; }
        public string state_Name { get; set; }
        public string city_Name { get; set; }
    }
    public class CommonModal_MobileAppChart
    {
        public DataTable marks { get; set; }
        public DataTable exam { get; set; }
        public DataTable subject { get; set; }
    }

    public class CommonModal_MobileLogin
    {
        public DataTable parentLogin { get; set; }
        public DataTable staffLogin { get; set; }
    }
    public class bloodGroup
    {
        public int Blood_group_id { get; set; }

        public string Blood_group_name { get; set; }
    }

    public class section
    {
        public int section_Id { get; set; }

        public string section_Name { get; set; }
    }

    public class boardOfStudy
    {
        public int boardId { get; set; }
        public string boardName { get; set; }
    }

    public class casteCommunity
    {
        public int community_Id { get; set; }
        public int caste_Id { get; set; }

        public string caste_Name { get; set; }
        public string community_name { get; set; }
    }

    public class banchyear
    {
        public int year_Id { get; set; }
        public int branch_Id { get; set; }

        public string year_name { get; set; }
        public string branch_Name { get; set; }
    }

    public class course
    {
        public int course_Id { get; set; }

        public string course_Name { get; set; }
    }

    public class degree
    {
        public int degree_Id { get; set; }
        public int CoCurricular_ID { get; set; }

        public string CoCurricular_Game_NAme { get; set; }
        public string degree_Name { get; set; }
    }

    public class feeCategory
    {
        public int fee_category_Id { get; set; }
        public int quota_Id { get; set; }
        public int semester_Id { get; set; }
        public int medium_Id { get; set; }
        public int exam_Id { get; set; }

        public string exam_Name { get; set; }
        public string medium_Name { get; set; }
        public string semester_name { get; set; }
        public string quota_Name { get; set; }
        public string fee_category_Name { get; set; }
    }

    public class OverallStaffNameModel
    {
        public DataTable data { get; set; }
    }
}
