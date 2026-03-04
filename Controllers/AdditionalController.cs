using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Preadmission_Lodha.Models;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace Preadmission_Lodha.Controllers
{
    public class UserDataModel
    {

        public int OrgId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string CountryCode { get; set; }
        public string IpAddress { get; set; }
        public string Type { get; set; }
        public string OTP { get; set; }
        public int No_Of_Child { get; set; }
        public List<ChildDataModel> Children { get; set; }
    }


    public class ChildDataModel
    {
        public string Name { get; set; }
        public int Class { get; set; }
    }
    public class TourRequestModel
    {
        public int OrgId { get; set; }
        public int AcademicId { get; set; }
        public DateTime ParentDate { get; set; }
        public DateTime AdminScheduledDate { get; set; }
        public int City { get; set; }
        public int FBSource { get; set; }
        public int NewsPaperSource { get; set; }
        public int SMSSource { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public int WebsiteSource { get; set; }
        public int WordOfMouthSource { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string ParentExpectations { get; set; }
        public string ReasonForChoosingSchool { get; set; }
        public string ZipCode { get; set; }
        public int StatusId { get; set; }
        public int IsActive { get; set; }
        public List<TourRequestChild> Children { get; set; }

        public DateTime DOB { get; set; }
        public int PalavaResident { get; set; }
        public int SiblingLWS { get; set; }
        public string SibGRNo { get; set; }
        public int GoogleSource { get; set; }
        public int InstaSource { get; set; }
        public int LinkdSource { get; set; }
        public int ParentRefSource { get; set; }
        public int FacultyRefSource { get; set; }

    }
    public class TourRequestChild
    {
        public int LeadId { get; set; }

    }
    public class UpdateSeatsRequest
    {
        public int OrgId { get; set; }
        public int AcademicId { get; set; }
        public int ClassId { get; set; }
        public int BranchId { get; set; }
        public int TotalSeats { get; set; }
        public DateTime AgeFrom { get; set; }
        public DateTime AgeTo { get; set; }
        public string Mode { get; set; }
    }
    public class NotificationAlert
    {
        public int OrgId { get; set; }
        public int AcademicId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int BranchId { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int IsActive { get; set; }


    }
    public class StudentDobRange
    {
        public int OrgId { get; set; }
        public int AcademicId { get; set; }
        public int ClassId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int IsActive { get; set; }
        public string mode { get; set; }
    }
    public class AdmOfficerAction
    {
        public int OrgId { get; set; }
        public int AcademicId { get; set; }
        public string mode { get; set; }
        public int StatusId { get; set; }
        public int LeadId { get; set; }

        public string Mobile { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public DateTime FeeExtendedDate { get; set; }
    }
    public class PurchaseOrder
    {
        public int orgId { get; set; }
        public int academicId { get; set; }
        public int mainId { get; set; }
        public int poId { get; set; }
        public int subId { get; set; }
        public int expenseId { get; set; }
        public int supplierId { get; set; }
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
        public string particular { get; set; }
        public DateTime poDate { get; set; }
        public string mode { get; set; }
        public int budgetId { get; set; }
        public int advance { get; set; }


    }
    public class Photo
    {
        public string PhotoName { get; set; }
        public string DestinationFilePath { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalController : ControllerBase
    {
        [HttpGet("getLeadDetails")]
        public DataTable getLeadDetails(string MobileNo, string mode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(commonCode.conStr))
                using (SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = MobileNo;
                    cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        [HttpPost("InsertUserData")]
        public CommonModal InsertUserData(UserDataModel userData)
        {

            CommonModal e1 = new CommonModal();
            try
            {
                using (SqlConnection conn = new SqlConnection(commonCode.conStr))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        string otp = GenerateNewRandom();
                        cmd.Parameters.Add("@ParentFirstName", SqlDbType.NVarChar).Value = userData.FirstName;
                        cmd.Parameters.Add("@ParentLastName", SqlDbType.NVarChar).Value = userData.LastName;
                        cmd.Parameters.Add("@ParentEmail", SqlDbType.NVarChar).Value = userData.EmailId;
                        cmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar).Value = userData.CountryCode;
                        cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = userData.MobileNo;
                        cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = userData.OrgId;
                        cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = userData.Type;
                        cmd.Parameters.Add("@OTP", SqlDbType.NVarChar).Value = otp;
                        cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "INSERT";

                        string n = cmd.ExecuteScalar()?.ToString();

                        if (n == "Inserted")
                        {
                            for (int i = 0; i < userData.No_Of_Child; i++)
                            {

                                InsertChildData(userData.OrgId, userData.MobileNo, userData.Children[i]);
                            }
                            string msg = $"Your One Time Password (OTP) for login is {otp}, provided by SITABEN SHAH MEMORIAL TRUST";
                            int status = SendOTPEmail(userData.EmailId, userData.OrgId, otp, userData.Type, "New User");
                            int status1 = SendOTPMobile(userData.OrgId, userData.MobileNo, msg);
                            if (status == 1 || status1 == 1)
                            {
                                e1.ResponseStatus = "True";
                                e1.ResponseCode = "200";
                                e1.ResponseMessage = "OTP sent successfully!!";
                                e1.data = new DataTable();
                                e1.data.Columns.Add("MobileNumber", typeof(string));
                                e1.data.Rows.Add(userData.MobileNo);
                            }
                            else
                            {
                                e1.ResponseStatus = "False";
                                e1.ResponseCode = "1027";
                                e1.ResponseMessage = "Error sending OTP";
                                e1.data = new DataTable();
                            }
                        }
                        else if (n == "RESENT")
                        {
                            string msg = $"Your One Time Password (OTP) for login is {otp}, provided by SITABEN SHAH MEMORIAL TRUST";
                            int OrgId = getOrgId(userData.MobileNo, "GETORG");
                            string EmailId = getMailId(OrgId, userData.MobileNo, "GETMAIL");
                            int status = SendOTPEmail(EmailId, OrgId, otp, userData.Type, "Existed User");
                            int status1 = SendOTPMobile(OrgId, userData.MobileNo, msg);
                            if (status == 1 || status1 == 1)
                            {
                                e1.ResponseStatus = "True";
                                e1.ResponseCode = "200";
                                e1.ResponseMessage = "OTP Resent";
                                e1.data = new DataTable();
                                e1.data.Columns.Add("MobileNumber", typeof(string));
                                e1.data.Rows.Add(userData.MobileNo);
                            }
                            else
                            {
                                e1.ResponseStatus = "False";
                                e1.ResponseCode = "1027";
                                e1.ResponseMessage = "Error sending OTP";
                                e1.data = new DataTable();
                            }
                        }
                        else if (n == "Updated")
                        {
                            string msg = $"Your One Time Password (OTP) for login is {otp}, provided by SITABEN SHAH MEMORIAL TRUST";
                            int OrgId = getOrgId(userData.MobileNo, "GETORG");
                            string EmailId = getMailId(OrgId, userData.MobileNo, "GETMAIL");
                            int status = SendOTPEmail(EmailId, OrgId, otp, userData.Type, "Existed User");
                            int status1 = SendOTPMobile(OrgId, userData.MobileNo, msg);
                            if (status == 1 || status1 == 1)
                            {
                                e1.ResponseStatus = "True";
                                e1.ResponseCode = "200";
                                e1.ResponseMessage = "OTP sent successfully!!";
                                e1.data = new DataTable();
                                e1.data.Columns.Add("MobileNumber", typeof(string));
                                e1.data.Rows.Add(userData.MobileNo);
                            }
                            else
                            {
                                e1.ResponseStatus = "False";
                                e1.ResponseCode = "1027";
                                e1.ResponseMessage = "Error sending OTP";
                                e1.data = new DataTable();
                            }
                        }
                        else
                        {

                            e1.ResponseStatus = "False";
                            e1.ResponseCode = "1027";
                            e1.ResponseMessage = n;
                            e1.data = new DataTable();
                            e1.data.Columns.Add("MobileNumber", typeof(string));
                            e1.data.Rows.Add(userData.MobileNo);

                        }

                        conn.Close();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                String msg = sqlEx.Message;
                e1.ResponseStatus = "False";
                e1.ResponseCode = "1027";
                e1.ResponseMessage = msg;

            }
            catch (Exception ex)

            {
                String msg = ex.Message;
                e1.ResponseStatus = "False";
                e1.ResponseCode = "1027";
                e1.ResponseMessage = msg;

            }

            return e1;
        }

        [HttpGet("getTourReqChildDetails")]
        public DataTable getTourReqChildDetails(int orgId, int leadId, string mode)
        {
            try
            {
                SqlConnection conn = new SqlConnection(commonCode.conStr);
                SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);

                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = orgId;
                cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = leadId;
                cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataTable Dt = ds.Tables[0];
                conn.Close();
                return Dt;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        [NonAction]
        private static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }

        [NonAction]
        private string getMailId(int OrgId, string MobileNo, string mode)
        {
            try
            {
                SqlConnection conn = new SqlConnection(commonCode.conStr);
                SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = OrgId;
                cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = MobileNo;
                cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;

                conn.Open();
                string n = cmd.ExecuteScalar().ToString();
                conn.Close();
                return n;
            }

            catch (Exception ex)
            {
                String msg = ex.Message;
                return $"An error occurred: {msg}";
            }
        }

        [NonAction]
        private int SendOTPEmail(string email, int org_Id, string otp, string type, string msg)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {


                    string mail = "", code = "";

                    if (org_Id == 210) { mail = "admission_lsg@lodhaworldschool.com"; code = "vtqujpxrxbrvghvo"; }
                    else if (org_Id == 211) { mail = "admission_thane@lodhaworldschool.com"; code = "qbvsvbsyqbdtnkgr"; }
                    else if (org_Id == 212) { mail = "admission_palava@lodhaworldschool.com"; code = "bqhlpnprmfojxiob"; }
                    else if (org_Id == 214) { mail = "admission_taloja@lodhaworldschool.com"; code = "ipqk qhxl eoiz pirp"; }
                    else if (org_Id == 213) { mail = "admissions@lodhaoakwoodschool.com"; code = "ytjnwukgnjsjpfiq"; }
                    else if (org_Id == 223) { mail = "admission_premier@lodhaworldschool.com"; code = "bcsciuavccudfmbl"; }

                    else { mail = "enquiryevalai@gmail.com"; code = "vgvj vqpj inov ntbo"; }

                    smtpClient.UseDefaultCredentials = false;
                    //smtpClient.Credentials = new NetworkCredential("enquiryevalai@gmail.com", "vgvj vqpj inov ntbo");
                    smtpClient.Credentials = new NetworkCredential(mail, code);
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(mail);
                    mailMessage.To.Add(email);
                    mailMessage.Subject = $"OTP for your {type}";
                    mailMessage.Body = $"Your OTP is: {otp}";//\n\nREMARK: {msg}";

                    smtpClient.Send(mailMessage);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending OTP email: {ex.Message}");
                return 0;
            }
        }

        [NonAction]
        private int getOrgId(string MobileNo, string mode) //added mode 
        {
            try
            {
                SqlConnection conn = new SqlConnection(commonCode.conStr);
                SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = MobileNo;
                cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;
                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();
                return Convert.ToInt32(result);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return -1;
            }
        }

        [NonAction]
        private int SendOTPMobile(int OrgId, string MobileNo, string msg)//added mode 
        {
            try
            {
                SqlConnection conn = new SqlConnection(commonCode.conStr);
                SqlCommand cmd = new SqlCommand("Pro_SendMobileMessage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@orgId", SqlDbType.Int).Value = OrgId;
                cmd.Parameters.Add("@customNumbers", SqlDbType.NVarChar).Value = MobileNo;
                cmd.Parameters.Add("@message", SqlDbType.NVarChar).Value = msg;
                cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "SEND_OTP_TO_CUSTOM_NUMBERS";

                conn.Open();
                string n = cmd.ExecuteScalar().ToString();
                conn.Close();
                return 1;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error sending OTP mobile: {ex.Message}");
                return 0;
            }
        }

        [NonAction]
        private void InsertChildData(int OrgId, string MobileNum, ChildDataModel childData)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(commonCode.conStr))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = OrgId;
                        cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = MobileNum;
                        cmd.Parameters.Add("@ChildName", SqlDbType.NVarChar).Value = childData.Name;
                        cmd.Parameters.Add("@ChildClass", SqlDbType.Int).Value = childData.Class;
                        cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "INSERTCHILD";

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Server Error (Child Data): {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred (Child Data): {ex.Message}");
            }
        }

        [HttpGet("getOtpStatus")]
        public CommonModal getOtpStatus(string OTP, string MobileNo, string mode)
        {

            CommonModal e1 = new CommonModal();
            try
            {
                SqlConnection conn = new SqlConnection(commonCode.conStr);
                SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OTP", SqlDbType.NVarChar).Value = OTP;
                cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = MobileNo;
                cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;

                conn.Open();
                string n = cmd.ExecuteScalar().ToString();
                conn.Close();
                if (n == "OTP IS VERIFIED")
                {
                    e1.ResponseStatus = "True";
                    e1.ResponseCode = "200";
                    e1.ResponseMessage = n;
                    e1.data = new DataTable();

                }
                else
                {
                    e1.ResponseStatus = "False";
                    e1.ResponseCode = "1021";
                    e1.ResponseMessage = n;
                    e1.data = new DataTable();

                }
                return e1;
            }

            catch (Exception ex)
            {
                String msg = ex.Message;
                e1.ResponseStatus = "False";
                e1.ResponseCode = "1027";
                e1.ResponseMessage = msg;
                return e1;
            }
        }


        [HttpGet("getStudentStatusList")]
        public DataTable getStudentStatusList(int academicId, int leadId, string mode)
        {
            try
            {
                SqlConnection conn = new SqlConnection(commonCode.conStr);

                SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academicId;
                cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = leadId;
                cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataTable Dt = ds.Tables[0];
                conn.Close();
                return Dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }

        }

        [HttpGet("getClusterDetails")]
        public DataTable getClusterDetails(string mode)
        {

            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            conn.Close();
            return Dt;

        }

        [HttpGet("getBranchDetails")]
        public DataTable getBranchDetails(int org_Id, int academic_Id, int class_Id, string mode)
        {

            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = org_Id;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academic_Id;
            cmd.Parameters.Add("@class_Id", SqlDbType.Int).Value = class_Id;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            conn.Close();
            return Dt;

        }

        [HttpGet("verifyClass")]
        public DataTable verifyClass(int org_Id, int class_Id, int academic_Id, DateTime date_Of_Birth, string mode)
        {


            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = org_Id;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academic_Id;
            cmd.Parameters.Add("@class_Id", SqlDbType.Int).Value = class_Id;
            cmd.Parameters.Add("@date_Of_Birth", SqlDbType.Date).Value = date_Of_Birth;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            conn.Close();
            return Dt;

        }
        [HttpGet("getGradeAvailSeatsCount")]
        public DataTable getGradeAvailSeatsCount(int OrgId, int AcademicId, int ClassId, int BranchId, string mode)
        {
            try
            {
                SqlConnection conn = new SqlConnection(commonCode.conStr);

                SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = OrgId;
                cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = AcademicId;
                cmd.Parameters.Add("@class_Id", SqlDbType.Int).Value = ClassId;
                cmd.Parameters.Add("@branch_Id", SqlDbType.Int).Value = BranchId;
                cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataTable Dt = ds.Tables[0];
                conn.Close();
                return Dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }

        }
        [HttpGet("getAdmEnquiryDetails")]
        public DataTable getAdmEnquiryDetails(int orgId, int academicId, int leadId, string mode)
        {
            try
            {
                SqlConnection conn = new SqlConnection(commonCode.conStr);

                SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = orgId;
                cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academicId;
                cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = leadId;
                cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataTable Dt = ds.Tables[0];
                conn.Close();
                return Dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }

        }

    }
}
