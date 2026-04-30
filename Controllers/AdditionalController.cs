using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Preadmission_Lodha.Models;
using Razorpay.Api;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using static Preadmission_Lodha.Controllers.PaymentController;

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

        private readonly IWebHostEnvironment _env;
        public AdditionalController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public static PaymentModel1 det;
        public string[] output = new string[3];
        public static string chesksumValue;
        public static string chesksumKey;
        public static int transId;
        public string testurl;
        public static string salt;

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
                    else if (org_Id == 214) { mail = "admission_taloja@lodhaworldschool.com"; code =  "demf uobr xuxi iufs"; }
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


        [HttpPost("InsertTourRequestData")]
        public CommonModal InsertTourRequestData(TourRequestModel userData)
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


                        cmd.Parameters.Add("@ParentFirstName", SqlDbType.NVarChar).Value = userData.FirstName;
                        cmd.Parameters.Add("@ParentLastName", SqlDbType.NVarChar).Value = userData.LastName;
                        cmd.Parameters.Add("@ParentEmail", SqlDbType.NVarChar).Value = userData.Email;
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = userData.Address;
                        cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = userData.Mobile;
                        cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = userData.OrgId;
                        cmd.Parameters.Add("@ParentExpectation", SqlDbType.NVarChar).Value = userData.ParentExpectations;
                        cmd.Parameters.Add("@ReasonForChoosingSchool", SqlDbType.NVarChar).Value = userData.ReasonForChoosingSchool;
                        cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar).Value = userData.ZipCode;
                        cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = userData.AcademicId;
                        cmd.Parameters.Add("@Country", SqlDbType.Int).Value = userData.Country;
                        cmd.Parameters.Add("@City", SqlDbType.Int).Value = userData.City;
                        cmd.Parameters.Add("@State", SqlDbType.Int).Value = userData.State;
                        cmd.Parameters.Add("@FBSource", SqlDbType.Int).Value = userData.FBSource;
                        cmd.Parameters.Add("@NewsPaperSource", SqlDbType.Int).Value = userData.NewsPaperSource;
                        cmd.Parameters.Add("@SMSSource", SqlDbType.Int).Value = userData.SMSSource;
                        cmd.Parameters.Add("@WebsiteSource", SqlDbType.Int).Value = userData.WebsiteSource;
                        cmd.Parameters.Add("@WordOfMouthSource", SqlDbType.Int).Value = userData.WordOfMouthSource;
                        cmd.Parameters.Add("@ParentDate", SqlDbType.Date).Value = userData.ParentDate;
                        //cmd.Parameters.Add("@statusId", SqlDbType.Int).Value = userData.StatusId;
                        //cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = userData.IsActive;
                        cmd.Parameters.Add("@GoogleSource", SqlDbType.Int).Value = userData.GoogleSource;
                        cmd.Parameters.Add("@InstaSource", SqlDbType.Int).Value = userData.InstaSource;
                        cmd.Parameters.Add("@LinkdSource", SqlDbType.Int).Value = userData.LinkdSource;
                        cmd.Parameters.Add("@ParentRefSource", SqlDbType.Int).Value = userData.ParentRefSource;
                        cmd.Parameters.Add("@FacultyRefSource", SqlDbType.Int).Value = userData.FacultyRefSource;
                        cmd.Parameters.Add("@PalavaResident", SqlDbType.Int).Value = userData.PalavaResident;
                        cmd.Parameters.Add("@SiblingLWS", SqlDbType.Int).Value = userData.SiblingLWS;
                        cmd.Parameters.Add("@SibGRNo", SqlDbType.NVarChar).Value = userData.SibGRNo;
                        cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = userData.DOB;
                        cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "INSERTTOURREQUEST";

                        string n = cmd.ExecuteScalar()?.ToString();

                        if (n == "Inserted")
                        {

                            bool result = InsertTourRequestChild(conn, userData.Mobile, userData.OrgId, userData.Children, userData.ParentDate, userData.AcademicId);

                            if (result == true)
                            {
                                e1.ResponseStatus = "True";
                                e1.ResponseCode = "200";
                                e1.ResponseMessage = "Tour Request Details are Registered";
                                e1.data = new DataTable();
                                e1.data.Columns.Add("MobileNumber", typeof(string));
                                e1.data.Rows.Add(userData.Mobile);
                            }
                            else
                            {
                                e1.ResponseStatus = "False";
                                e1.ResponseCode = "1027";
                                e1.ResponseMessage = "Error in Registering the Details";
                                e1.data = new DataTable();
                                e1.data.Columns.Add("MobileNumber", typeof(string));
                                e1.data.Rows.Add(userData.Mobile);
                            }

                        }
                        else
                        {

                            e1.ResponseStatus = "False";
                            e1.ResponseCode = "1027";
                            e1.ResponseMessage = n;
                            e1.data = new DataTable();
                            e1.data.Columns.Add("MobileNumber", typeof(string));
                            e1.data.Rows.Add(userData.Mobile);

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

        [NonAction]
        private bool InsertTourRequestChild(SqlConnection connection, string mobile, int orgId, List<TourRequestChild> children, DateTime ParentDate, int academic_Id)
        {
            try
            {
                foreach (TourRequestChild child in children)
                {
                    using (SqlCommand cmd = new SqlCommand("pre_Admission_Pro", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = child.LeadId;
                        cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = orgId;
                        cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = mobile;
                        cmd.Parameters.Add("@ParentDate", SqlDbType.Date).Value = ParentDate;
                        cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academic_Id;
                        cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "INSERTTOURREQUESTCHILD";

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred (Table B): {ex.Message}");


                return false;
            }
        }

        [NonAction]
        private void LogToFile(string filePath, string message)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }

        [HttpPost("getArray")]
        public string getArray(PaymentModel1 things)
        {
            try
            {
                string logFileName = $"RazorpayLogsapplicationfee_{things.org_Id}_{DateTime.Now:ddMMyyyy}_{things.event_id}.txt";

                // Combine the file name with the virtual directory path
                string logFilePath = Path.Combine(_env.ContentRootPath,$"Group/Logs/{logFileName}");
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);
                LogToFile(logFilePath, "getArray Method Started.");




                int n = 0;
                det = things;
                OnlinePayment retData = new OnlinePayment();
                SqlConnection conn = new SqlConnection(commonCode.conStr);
                SqlCommand cmd = new SqlCommand("Pro_onlinePayment_event", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = things.org_Id;
                cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = things.academic_Id;
                cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = things.event_id;
                cmd.Parameters.Add("@transaction_Amount", SqlDbType.Int).Value = things.transaction_Amount;
                //cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = things.email_id;


                cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "GET_MERCHANT_DETAILS2";
                conn.Open();
                List<OnlinePayment> data = new List<OnlinePayment>();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataTable Dt = ds.Tables[0];
                data = commonCode.ConvertDataTable<OnlinePayment>(Dt);
                if (things.org_Id == 212)
                {
                    //data[0].apiKey = "rzp_live_qQE7inemMZTwMV";
                    //data[0].salt = "iyl6WFYskRBRWKqHyBmdEL6I";
                    //data[0].provider = "RAZORPAY"; // Example: Use "RAZORPAY" or other if required
                    data[0].apiKey = "rzp_test_W7w5DfMGU7APNC";
                    data[0].salt = "5E2xLFCISLt3m4R8buRRCSlq";
                    data[0].provider = "RAZORPAY";
                }


                if (data[0].provider == "TRACKNPAY")
                {
                    // chesksumKey = data[0].checksum_key;
                    transId = data[0].transId;

                    things.transaction_Id = transId;



                    det = things;

                    // Conver the string to the checksum value
                    chesksumValue = Generatehash512(data[0].salt + "|" + "address_line_1" + "|" + things.event_id + "|" + things.transaction_Amount + "|" +
                                    data[0].apiKey + "|city|IND|INR|" + data[0].description + "|" + data[0].email + "|" + data[0].mode + "|" + data[0].customerName.TrimEnd() + "|" + data[0].transId + "|" +
                                    data[0].mobileNumber + "|https://api.valaischool.com/api/Additional/return_responseTracknPay|state|udf1|udf2|udf3|udf4|udf5|000000");

                    //string msg = data[0].merchent_id + "|" + data[0].transId + "|NA|" + things[0].amount + "|NA|NA|NA|INR|NA|R|" + data[0].security_id + "|NA|NA|F|NA|NA|NA|NA|NA|NA|NA|http://localhost:1675/api/Payment/return_response" + "|" + chesksumValue;
                    string url = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
                    salt = data[0].salt;
                    //  chesksumValue = Generatehash512(data[0].salt + "|" + things[0].amount + "|" + data[0].apiKey + "|city|IND|INR|" + data[0].email + "|LIVE|" + data[0].StudentName + "|" + data[0].order_id + "|" + data[0].mobileNumber + "|http://localhost:59904/api/Payment/Return|" + data[0].zipCode);
                    RemotePost remotepost = new RemotePost();
                    remotepost.Url = "https://biz.traknpay.in/v2/getpaymentrequest?api_key=" + data[0].apiKey +
                        "&return_url=https://api.valaischool.com/api/Additional/return_responseTracknPay&mode=" + data[0].mode +
                        "&order_id=" + data[0].transId + "&amount=" + things.transaction_Amount + "&name=" + data[0].customerName.TrimEnd() +
                        "&currency=INR&description=" + data[0].description +
                        "&address_line_1=" + "address_line_1" + "&address_line_2=" + things.event_id + "&phone=" + data[0].mobileNumber +
                        "&email=" + data[0].email +
                        "&city=city&state=state&country=IND&zip_code=000000&udf1=udf1&udf2=udf2&udf3=udf3&udf4=udf4&udf5=udf5&hash=" + chesksumValue;

                    //  remotepost.Url = "https://biz.traknpay.in/v2/getpaymentrequest?api_key=" + data[0].apiKey + "&return_url=https://localhost:44313/api/Payment/return_responseTracknPay&mode=" + data[0].mode + "&order_id=" + data[0].transId + "&amount=" + "60" + "&name=" + "iuyrtuy" + "&currency=INR&description=" + (string.IsNullOrEmpty("rrrr") ? "fee" : "fee") + "&address_line_1=address_line_1&address_line_2=address_line_2&phone=" + (string.IsNullOrEmpty("000000044") ? "9898989898" : "84787474") + "&email=" + (string.IsNullOrEmpty("") ? "demo@evali.com" : "") + "&city=" + (string.IsNullOrEmpty("lfkhg") ? "patna" : "uiyf") + "&state=bihar&country=IND&zip_code=" + (string.IsNullOrEmpty("iufrhuiyhf") ? "800001" : "09487t97") + "&udf1=" + "8748565" + "&udf2=udf2&udf3=udf3&udf4=udf4&udf5=udf5&hash=" + chesksumValue;

                    testurl = remotepost.Url.ToString();

                    //// data[0].mode = "TEST";

                    //string hash = (data[0].salt + "|address_line_1|address_line_2|" + things.transaction_Amount + "|" + data[0].apiKey + "|" + (string.IsNullOrEmpty(data[0].city) ? "patna" : data[0].city) + "|IND|INR|" + (string.IsNullOrEmpty(data[0].description) ? "fee" : data[0].description) + "|" + (string.IsNullOrEmpty(data[0].email) ? "demo@evali.com" : data[0].email) + "|" + data[0].mode + "|" + data[0].customerName.Trim() + "|" + data[0].transId + "|" + (string.IsNullOrEmpty(data[0].mobileNumber) ? "9898989898" : data[0].mobileNumber) + "|https://localhost:44313/api/Payment/return_responseTracknPay|bihar|" + data[0].amount + "|udf2|udf3|udf4|udf5|" + (string.IsNullOrEmpty(data[0].zipCode) ? "800001" : data[0].zipCode));

                    //string h1 = hash.Trim();

                    //chesksumValue = Generatehash51211(h1).ToUpper();

                    //string url = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
                    //salt = data[0].salt;
                    //RemotePost remotepost = new RemotePost();


                    //remotepost.Url = "https://biz.traknpay.in/v2/getpaymentrequest?api_key=" + data[0].apiKey + "&return_url=https://localhost:44313/api/LodhaEvent/return_responseTracknPay&mode=" + data[0].mode + "&order_id=" + data[0].transId + "&amount=" + things.transaction_Amount + "&name=" + data[0].customerName + "&currency=INR&description=" + (string.IsNullOrEmpty(data[0].description) ? "fee" : data[0].description) + "&address_line_1=address_line_1&address_line_2=address_line_2&phone=" + (string.IsNullOrEmpty(data[0].mobileNumber) ? "9898989898" : data[0].mobileNumber) + "&email=" + (string.IsNullOrEmpty(data[0].email) ? "demo@evali.com" : data[0].email) + "&city=" + (string.IsNullOrEmpty(data[0].city) ? "patna" : data[0].city) + "&state=bihar&country=IND&zip_code=" + (string.IsNullOrEmpty(data[0].zipCode) ? "800001" : data[0].zipCode) + "&udf1=" + data[0].amount + "&udf2=udf2&udf3=udf3&udf4=udf4&udf5=udf5&hash=" + chesksumValue;

                    //testurl = remotepost.Url.ToString();

                    //enterMsg(things[0].org_Id, things[0].academic_Id, things[0].student_Id, chesksumValue, url, testurl);
                    return testurl;
                }

                else if (data[0].provider == "RAZORPAY")
                {


                    decimal totalAmount = 0;
                    transId = getTransId(things.org_Id, things.academic_Id, things.event_id);
                    //transId = data[0].transId;
                    //things.transaction_Id = transId;

                    totalAmount = things.transaction_Amount;


                    // Populate retData
                    retData.address_line_1 = "address_line_1";
                    retData.address_line_2 = "address_line_2";
                    retData.amount = totalAmount.ToString();
                    retData.apiKey = data[0].apiKey;
                    retData.city = data[0].city;
                    retData.country = "IND";
                    retData.currency = "INR";
                    retData.description = data[0].description;
                    retData.email = data[0].email;
                    retData.mobileNumber = data[0].mobileNumber;
                    retData.mode = data[0].mode;
                    retData.return_url = "https://login.valaischool.com/";
                    retData.salt = data[0].salt;
                    retData.StudentName = data[0].customerName;
                    retData.transId = transId;
                    retData.zipCode = data[0].zipCode;
                    retData.org_Id = things.org_Id;
                    retData.academic_Id = things.academic_Id;
                    retData.student_Id = things.event_id;
                    retData.provider = data[0].provider;
                    retData.admission_No = data[0].admission_No;

                    retData.udf1 = "udf1";
                    LogToFile(logFilePath, $"Received data(RAZORPAY): org_Id={things.org_Id}, academic_Id={things.academic_Id}, student_Id={things.event_id},admission_No = {data[0].admission_No}");

                    decimal OrderAmount;

                    try
                    {
                        OrderAmount = Convert.ToDecimal(totalAmount);
                    }
                    catch (FormatException fe)
                    {
                        throw new Exception("Invalid amount format: " + totalAmount, fe);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while converting the amount: " + ex.Message, ex);
                    }

                    try
                    {
                        string orderId = CreateOrder(totalAmount, retData.currency, retData.apiKey, retData.salt);
                        retData.order_id = orderId;
                    }
                    catch (Exception ex)
                    {
                        LogErrorToDatabase(ex.Message, ex.StackTrace, nameof(getArray), things.org_Id, things.academic_Id, things.event_id, transId);
                        return "fail";
                    }
                    //update pending status for fee 12-09-2025
                    string orderId1 = retData.order_id;
                    SqlConnection conn1 = new SqlConnection(commonCode.conStr);
                    SqlCommand cmd1 = new SqlCommand("Pro_admissionEnquiry175", conn1);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@org_Id", SqlDbType.Int).Value = things.org_Id;
                    cmd1.Parameters.Add("@academic_Id", SqlDbType.Int).Value = things.academic_Id;
                    cmd1.Parameters.Add("@enqiryId", SqlDbType.Int).Value = things.event_id;
                    cmd1.Parameters.Add("@transId", SqlDbType.Int).Value = transId;
                    cmd1.Parameters.Add("@order_id", SqlDbType.NVarChar).Value = orderId1;
                    cmd1.Parameters.Add("@applicationcFee", SqlDbType.Decimal).Value = totalAmount;
                    cmd1.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "UPDATEFeePending";

                    conn1.Open();
                    object result = cmd1.ExecuteScalar();
                    int n1;
                    if (result != null && int.TryParse(result.ToString(), out int count))
                    {
                        n1 = count;
                    }
                    else
                    {
                        n1 = 0;
                    }
                    conn1.Close();
                    if (n1 > 0)
                    { LogToFile(logFilePath, $"Updated pending status for org_Id={things.org_Id}, academic_Id={things.academic_Id}, student_Id={things.event_id},admission_No = {data[0].admission_No}"); }
                    else
                    { LogToFile(logFilePath, $"Failed to Updated pending status for org_Id={things.org_Id}, academic_Id={things.academic_Id}, student_Id={things.event_id},admission_No = {data[0].admission_No}"); }
                    //update pending status for fee    12-09-2025 
                    return JsonSerializer.Serialize(JsonSerializer.Serialize(retData));
                }
                return "Unsupported provider.";
            }
            catch (Exception e)
            {
                LogErrorToDatabase(e.Message, e.StackTrace, nameof(getArray), things.org_Id, things.academic_Id, things.event_id, 0);

                return "fail";
            }
        }

        [NonAction]
        public string Generatehash512(string text)
        {

            byte[] message = System.Text.Encoding.UTF8.GetBytes(text);

            System.Text.UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex.ToUpper();

        }

        [NonAction]
        public void LogErrorToDatabase(string errorMessage, string stackTrace, string methodName, int orgId, int academicId, int studentId, int transactionId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(commonCode.conStr))
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO ErrorLogs (ErrorMessage, StackTrace, MethodName, Timestamp, OrgId, AcademicId, StudentId, TransactionId) " +
                        "VALUES (@ErrorMessage, @StackTrace, @MethodName, @Timestamp, @OrgId, @AcademicId, @StudentId, @TransactionId)", conn);

                    cmd.Parameters.AddWithValue("@ErrorMessage", errorMessage);
                    cmd.Parameters.AddWithValue("@StackTrace", (object)stackTrace ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MethodName", methodName);
                    cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now); // Track when the error occurred
                    cmd.Parameters.AddWithValue("@OrgId", orgId);
                    cmd.Parameters.AddWithValue("@AcademicId", academicId);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@TransactionId", transactionId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Optional: Log to a monitoring system or simply output to debug
                Debug.WriteLine($"Failed to log error to database. Error: {ex.Message}");
            }
        }


        [HttpGet("getTransId")]
        private int getTransId(int orgId, int academicId, int studentId)
        {
            int transId = 0;
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Pro_onlinePayment_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = orgId;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academicId;
            cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = studentId;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "GETTRANSID";

            transId = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return (int)transId;
        }

        [NonAction]
        public String CreateOrder(Decimal Amount, String Currency, string mkey, string msecret)
        {
            try
            {
                RazorpayClient client = new RazorpayClient(mkey, msecret);
                Dictionary<String, Object> options = new Dictionary<String, Object>();
                options.Add("amount", Amount * 100);  // order amount should be in paise
                options.Add("currency", Currency);
                Order OrderReponse = client.Order.Create(options);
                var orderid = OrderReponse.Attributes["id"].ToString();
                return orderid;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpPost("return_responsRazorpay")]
        public IActionResult return_responsRazorpay()
        {
            var request = Request.Form;
            int org_Id = int.Parse(request["org_Id"]);
            int student_Id = int.Parse(request["student_Id"]);// Assuming 'student_Name' is part of the form data
                                                                   //string logFilePath = $@"E:\Valaischool\Backendcode\School_04-11-2024\Group\Logs\RazorpayLogs_{DateTime.Now:ddMMyyyy}_{student_Id}.txt";
            string logFileName = $"RazorpayLogsapplicationfee_{org_Id}_{DateTime.Now:ddMMyyyy}_{student_Id}.txt";

            // Combine the file name with the virtual directory path
            string logFilePath = Path.Combine(_env.ContentRootPath,$"Group/Logs/{logFileName}");
            try
            {
                // Log request details
                LogToFile(logFilePath, "Processing Razorpay response...");

                // Retrieve values from the request
                string amount = request["amount"];
                string currency = request["currency"];
                string order_id = request["order_id"];
                string payment_datetime = request["payment_datetime"];
                string response_code = request["response_code"];
                string responseMessage = request["response_message"];
                string transaction_id = request["transaction_id"];
                string provider = request["provider"];
                //int org_Id = int.Parse(request["org_Id"]);
                int academic_Id = int.Parse(request["academic_Id"]);
                //int student_Id = int.Parse(request["student_Id"]);

                LogToFile(logFilePath, $"Received data: OrderID={order_id}, Amount={amount}, ResponseCode={response_code}");

                // Validate provider
                if (provider != "RAZORPAY")
                {
                    LogToFile(logFilePath, "Invalid payment provider.");
                    return BadRequest("Invalid payment provider.");
                }

                // Validate transaction ID
                string transId = Convert.ToString(getTransId(org_Id, academic_Id, student_Id));
                if (order_id != transId)
                {
                    LogToFile(logFilePath, $"Received data: org_Id={org_Id}, academic_Id={academic_Id}, Invalid transaction ID");

                    // LogToFile(logFilePath, "Invalid transaction ID.");
                    return BadRequest("Invalid transaction ID.");
                }

                using (SqlConnection conn = new SqlConnection(commonCode.conStr))
                {
                    SqlCommand cmd = new SqlCommand("Pro_onlinePayment_event", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@transaction_Id", order_id);
                    cmd.Parameters.AddWithValue("@payment_trans_Id", transaction_id);
                    cmd.Parameters.AddWithValue("@payment_Vendor", "RAZORPAY");
                    cmd.Parameters.AddWithValue("@merchant_id", order_id);
                    cmd.Parameters.AddWithValue("@currency_Code", currency);
                    cmd.Parameters.AddWithValue("@payment_status_Id", response_code);
                    cmd.Parameters.AddWithValue("@transaction_status", responseMessage);
                    cmd.Parameters.AddWithValue("@transaction_amount", Convert.ToDecimal(amount));
                    cmd.Parameters.AddWithValue("@transaction_Date", Convert.ToDateTime(payment_datetime));
                    cmd.Parameters.AddWithValue("@mode", "INSERT_PAYMENT_TRANSACTION2");

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();

                    LogToFile(logFilePath, "Stored procedure executed successfully.");

                    var data = commonCode.ConvertDataTable<OnlinePayment>(ds.Tables[0]);

                    // Get email and other info
                    DataTable dt2 = getMailId2(data[0].org_Id, data[0].academic_Id, data[0].lead_Id, "GETMAIL3");
                    string email2 = dt2.Rows[0]["emailId"].ToString();
                    string orgName = dt2.Rows[0]["orgName"].ToString();
                    string leadName = dt2.Rows[0]["childName"].ToString();
                    int classId = Convert.ToInt32(dt2.Rows[0]["class_Id"]);
                    string className = dt2.Rows[0]["class_Name"].ToString();
                    string applicationNumber = dt2.Rows[0]["application_Form_No"].ToString();

                    LogToFile(logFilePath, $"Received data: email={email2}, orgName={orgName}, leadName={leadName}, classId={classId}, className={className}, applicationNumber={applicationNumber}");
                    // Create the link based on organization
                    string baseLink = data[0].org_Id != 213
                        ? "https://applyonline.lodhaworldschool.com/dashboard?status="
                        : "https://applyonline.lodhaoakwoodschool.com/dashboard?status=";

                    // Handle successful and unsuccessful responses
                    if (response_code == "0")
                    {
                        CRUDEnquirylodha(data[0].org_Id, data[0].academic_Id, data[0].event_id, Convert.ToDecimal(amount), order_id);
                        LogToFile(logFilePath, $"Received data: org_Id={data[0].org_Id}, academic_Id={data[0].academic_Id}, event_id={data[0].event_id}, Amount={Convert.ToDecimal(amount)},applicationNumber={applicationNumber}");

                        SendAdminStatusEmail(email2, data[0].org_Id, DateTime.Today, DateTime.Now.TimeOfDay, orgName, leadName, 0, classId, className, applicationNumber);
                        LogToFile(logFilePath, "Payment processed successfully.");
                    }
                    else
                    {
                        // CRUDEnquirylodha(data[0].org_Id, data[0].academic_Id, data[0].event_id, 0);
                        LogToFile(logFilePath, "Payment failed.");
                    }

                    // Redirect to the payment status page
                    LogToFile(logFilePath, "Redirecting to status page.");
                    return Redirect((baseLink + response_code));
                }
            }
            catch (Exception ex)
            {
                LogToFile(logFilePath, $"Error occurred: {ex.Message} | StackTrace: {ex.StackTrace}");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("getMailId2")]
        public DataTable getMailId2(int OrgId, int AcademicId, int LeadId, string mode)
        {

            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = OrgId;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = AcademicId;
            cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = LeadId;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;


            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            conn.Close();
            return Dt;
        }

        [HttpPost("CRUDEnquirylodha")]
        public CommonModal12 CRUDEnquirylodha(int org_Id, int academic_Id, int enquiry_No, decimal Application_Fee, string order_Id)
        {
            CommonModal12 e1 = new CommonModal12();
            try
            {

                SqlConnection conn = new SqlConnection(commonCode.conStr);
                SqlCommand cmd = new SqlCommand("Pro_admissionEnquiry175", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = org_Id;
                cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academic_Id;
                cmd.Parameters.Add("@transId", SqlDbType.Int).Value = order_Id;
                cmd.Parameters.Add("@enqiryId", SqlDbType.Int).Value = enquiry_No;
                cmd.Parameters.Add("@applicationcFee", SqlDbType.Decimal).Value = Application_Fee;
                cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "UPDATE2";

                conn.Open();
                int n = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                if (n > 0)
                {

                    //by goutham

                    FeeCommonController f = new FeeCommonController();
                    ////
                    crudAccountsPostingModel ap = new crudAccountsPostingModel();
                    ap.org_Id = org_Id;
                    ap.receipt_Date = DateTime.Now;
                    ap.transCode = 1;
                    ap.transType = "Receipt";
                    ap.accountsCode = 9;
                    ap.credit_Amount = Application_Fee;//feeArray[0].amount;
                    ap.debit_Amount = 0;
                    ap.typeId = 7657;
                    ap.academicId = academic_Id;
                    ap.directOrIndirect = 1;

                    ap.receipt_Code = order_Id;
                    ap.student_Id = enquiry_No;
                    var acc = f.crudAccountsPostingNew(ap);
                    //end
                    e1.ResponseStatus = "True";
                    e1.ResponseCode = "1";
                    e1.ResponseMessage = "Inserted Successfully";
                    e1.data = n;
                    return e1;
                }
                else
                {
                    e1.ResponseStatus = "False";
                    e1.ResponseCode = "0";
                    e1.ResponseMessage = "Insertion failed";
                    LogErrorToDatabase("Insertion failed in CRUDEnquirylodha", "", nameof(CRUDEnquirylodha), org_Id, academic_Id, enquiry_No, 0);

                    return e1;
                }

            }
            catch (Exception ex)
            {
                e1.ResponseStatus = "False";
                e1.ResponseCode = "0";
                e1.ResponseMessage = ex.ToString();
                LogErrorToDatabase(ex.Message, ex.StackTrace, nameof(CRUDEnquirylodha), org_Id, academic_Id, enquiry_No, 0);

                return e1;
            }
        }

        [NonAction]
        private int SendAdminStatusEmail(string email, int org_Id, DateTime visitDate, TimeSpan visitTime, string orgName, string leadName, int status_Id, int classId, string className, string applicationNumber)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                //userData.StatusId == 14 || userData.StatusId == 13 || userData.StatusId == 9 || userData.StatusId == 12 || userData.StatusId == 3

                {
                    string mail = "", code = "";

                    if (org_Id == 210) { mail = "admission_lsg@lodhaworldschool.com"; code = "vtqujpxrxbrvghvo"; }
                    else if (org_Id == 211) { mail = "admission_thane@lodhaworldschool.com"; code = "qbvsvbsyqbdtnkgr"; }
                    else if (org_Id == 212) { mail = "admission_palava@lodhaworldschool.com"; code = "bqhlpnprmfojxiob"; }
                    else if (org_Id == 214) { mail = "admission_taloja@lodhaworldschool.com"; code =  "demf uobr xuxi iufs"; }
                    else if (org_Id == 213) { mail = "admissions@lodhaoakwoodschool.com"; code = "ytjnwukgnjsjpfiq"; }
                    else if (org_Id == 223) { mail = "admission_premier@lodhaworldschool.com"; code = "bcsciuavccudfmbl"; }


                    string formattedVisitDate = visitDate.ToString("MMMM dd, yyyy");

                    // Format the time in 12-hour format with AM/PM
                    string formattedVisitTime = DateTime.Today.Add(visitTime).ToString(@"hh\:mm tt");

                    // Combine the formatted date and time
                    string formattedDateTime = $"{formattedVisitDate} at {formattedVisitTime}";



                    smtpClient.UseDefaultCredentials = false;
                    //smtpClient.Credentials = new NetworkCredential("enquiryevalai@gmail.com", "vgvj vqpj inov ntbo");
                    smtpClient.Credentials = new NetworkCredential(mail, code);
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(mail);
                    mailMessage.To.Add(email);

                    string body = $@"
<html>
<head>
    <style>
        /* Add any CSS styling here */
    </style>
</head>
<body>
    <p>Dear Parent/Guardian,</p>
    <p>Greetings from {orgName}!</p>";
                    if (status_Id == 14 && org_Id != 213)
                    {
                        if (classId == 14 || classId == 15 || classId == 16) //classId == 1 || 
                        {
                            mailMessage.Subject = $"Interaction & Document Submission scheduled for {formattedDateTime}";
                            //                            body += $@"
                            //<p>Thank you for submitting your application at {orgName}. We would like to inform you that the entrance evaluation and document submission for your ward has been scheduled for {formattedDateTime}.</p>
                            //<p>The entrance evaluation is an important step in our shared journey towards excellence, ensuring that each child is positioned for success within our academic community.</p>
                            //<p><strong>Details of the entrance evaluation:</strong></p>
                            //<ol>
                            // <li>The evaluation will be conducted in the subjects of Math, English, and Hindi/ Science or
                            //as per the Class selected.</li>
                            //<li>The time allotted for each paper will be 1hrs to 2.5hrs depending on the class.</li>
                            //<li>The student must keep a pen or pencil and an eraser ready with him/her.</li>
                            //</ol>
                            //<p>We also invite you to visit the school for document verification and submission along with your
                            //ward.</p>
                            //<p><strong>Please ensure that you bring along the following documents for verification:</strong></p>
                            //<ul>
                            //    <li>Printout of the Admission Form</li>
                            //    <li>Original Birth Certificate / Copy of Birth Certificate attested by gazetted officer / Copy of Birth Certificate with notary stamp / Birth Certificate with QR code (Any one)</li>
                            //    <li>Copy of Aadhar Card of the student</li>
                            //    <li>Copies of Aadhar Card of both parents</li>
                            //    <li>Proof of Residence
                            //        <ul>
                            //            <li>For Owners – Copy of Index 2 or an Electricity Bill</li>
                            //            <li>For Tenants – Registered copy of the Rent Agreement</li>
                            //        </ul>
                            //    </li>
                            //    <li>Original Blood group report by a certified pathologist, Vaccination Card, or Discharge
                            //Summary of Mother with the child’s blood group mentioned</li>
                            //    <li>Copy of Full-Year result of AY 2023-24</li>
                            //    <li>Copy of 1st Term result of AY 2024-25</li>
                            //    <li>Copy of Full Year Marksheet of AY 2024-25 (To be submitted on or before 31st March 2025)</li>
                            //    <li>Original School Leaving Certificate (To be submitted on or before 31st March 2025)</li>
                            //    <li>Recent passport-size photograph of the student (2 copies with white background)</li>
                            //    <li>Recent passport-size photographs of both parents (1 copy each with white background)</li>
                            //    <li>Copy of the student’s caste certificate (if applicable). Please note that the Father’s caste certificate will not be applicable.</li>
                            //</ul>
                            //<p>We look forward to welcoming you on campus and being a partner in your child’s journey towards excellence.</p>
                            //<p>Warm Regards,<br><br>Admissions Team<br>{orgName}</p>";
                            body += $@"
                            <p>We are pleased to invite you and your child to the next stage of our admissions process at {orgName}. Your scheduled interaction with our Counsellor / Coordinator, along with document verification and submission, will take place on {formattedDateTime}. We request that both parents attend this session, accompanied by your child.</p>
                            <p>The interaction with the counsellor is an important step in our shared journey towards excellence, ensuring that each child is positioned for success within our academic community.</p>
                            <p>Please ensure that you bring along the following documents for verification:</p>
                            <ul>
                                <li>Printout of the Admission Form</li>
                                <li>Original Birth Certificate / Copy of Birth Certificate attested by gazetted officer / Copy of Birth Certificate with notary stamp / Birth Certificate with QR code (Any one)</li>
                                <li>Copy of Aadhar Card of the student</li>
                                <li>Copies of Aadhar Card of both parents</li>
                                <li>Proof of Residence
                                    <ul>
                                        <li>For Owners – Copy of Index 2 or an Electricity Bill</li>
                                        <li>For Tenants – Registered copy of the Rent Agreement</li>
                                    </ul>
                                </li>
                                <li>Original Blood Group Report by a certified pathologist</li>
                                <li>Copy of Palava Smart Card (only added for LSG, CBG, Taloja campuses)</li>
                                <li>Recent passport-size photographs (white background):
                                    <ul>
                                        <li>Student – 2 copies</li>
                                        <li>Parent – 1 copy each</li>
                                    </ul>
                                </li>
                                <li>Copy of the student’s caste certificate (If belonging to SC/ST/OBC/SBC/NT etc.). Please note that the father’s caste certificate will not be accepted.</li>
                            </ul>
                            <p>We look forward to welcoming you on campus and being a partner in your child’s journey towards excellence.</p>
                            <p>Warm Regards,<br><br>Admissions Team<br>{orgName}</p>";

                        }
                        else
                        {
                            mailMessage.Subject = $"Entrance Evaluation & Document Submission Scheduled for {formattedDateTime}";
                            body += $@"
<p>Thank you for submitting your application at {orgName}. We would like to inform you that the entrance evaluation and document submission for your ward has been scheduled for {formattedDateTime}.</p>
<p>The entrance evaluation is an important step in our shared journey towards excellence, ensuring that each child is positioned for success within our academic community.</p>
<p><strong>Details of the entrance evaluation:</strong></p>
<ol>
 <li>The evaluation will be conducted in the subjects of Math, English, and Hindi/ Science or
as per the Class selected.</li>
<li>The time allotted for each paper will be 1hrs to 2.5hrs depending on the class.</li>
<li>The student must keep a pen or pencil and an eraser ready with him/her.</li>
</ol>
<p>We also invite you to visit the school for document verification and submission along with your
ward.</p>
<p><strong>Please ensure that you bring along the following documents for verification:</strong></p>
<ul>
    <li>Printout of the Admission Form</li>
    <li>Original Birth Certificate / Copy of Birth Certificate attested by gazetted officer / Copy of Birth Certificate with notary stamp / Birth Certificate with QR code (Any one)</li>
    <li>Copy of Aadhar Card of the student</li>
    <li>Copies of Aadhar Card of both parents</li>
    <li>Proof of Residence
        <ul>
            <li>For Owners – Copy of Index 2 or an Electricity Bill</li>
            <li>For Tenants – Registered copy of the Rent Agreement</li>
        </ul>
    </li>
    <li>Original Blood group report by a certified pathologist, Vaccination Card, or Discharge
Summary of Mother with the child’s blood group mentioned</li>
    <li>Copy of Full-Year result of AY 2023-24</li>
    <li>Copy of 1st Term result of AY 2024-25</li>
    <li>Copy of Full Year Marksheet of AY 2024-25 (To be submitted on or before 31st March 2025)</li>
    <li>Original School Leaving Certificate (To be submitted on or before 31st March 2025)</li>
    <li>Recent passport-size photograph of the student (2 copies with white background)</li>
    <li>Recent passport-size photographs of both parents (1 copy each with white background)</li>
    <li>Copy of the student’s caste certificate (if applicable). Please note that the Father’s caste certificate will not be applicable.</li>
</ul>
<p>We look forward to welcoming you on campus and being a partner in your child’s journey towards excellence.</p>
<p>Warm Regards,<br><br>Admissions Team<br>{orgName}</p>";
                            //                            body += $@"
                            //<p>Thank you for submitting your application at {orgName}. We would like to inform you that the entrance evaluation and document submission for your ward has been scheduled for {formattedDateTime}.</p>
                            //<p>The entrance evaluation is an important step in our shared journey towards excellence, ensuring that each child is positioned for success within our academic community.</p>
                            //<p><strong>Details of the entrance evaluation:</strong></p>
                            //<ol>
                            //    <li>The evaluation will be conducted in the subjects of Math, English, and Hindi.</li>
                            //    <li>Each paper will carry a total of 20 marks.</li>
                            //    <li>The time allotted for each paper will be 20 minutes.</li>
                            //    <li>The student must keep a pen or pencil and an eraser ready with him/her.</li>
                            //</ol>
                            //<p>We also invite you to visit the school for document verification and submission along with your ward.</p>
                            //<p>Please bring the following documents for verification:</p>
                            //<ol>
                            //    <li>Printout of the Admission Form</li>
                            //    <li>Original Birth Certificate / Copy of Birth Certificate attested by gazetted officer / Copy of Birth Certificate with notary stamp / Birth Certificate with QR code (Any one)</li>
                            //    <li>Copy of the Student’s Aadhar Card</li>
                            //    <li>Copies of both parents’ Aadhar Cards</li>
                            //    <li>Proof of Residence:
                            //        <ul>
                            //            <li>If Owner – Copy of Index 2 or Electricity Bill</li>
                            //            <li>If Tenant – Registered copy of the Rent Agreement</li>
                            //        </ul>
                            //    </li>
                            //    <li>Original Blood group report by a certified pathologist, Vaccination Card, or Discharge Summary of Mother with the child’s blood group mentioned</li>
                            //    <li>Copy of Full-Year result of AY 2023-24</li>
                            //    <li>Copy of 1st Term result of AY 2024-25</li>
                            //    <li>Copy of Full Year Marksheet of AY 2024-25 (To be submitted on or before 31st March 2025)</li>
                            //    <li>Original School Leaving Certificate (To be submitted on or before 31st March 2025)</li>
                            //    <li>Recent passport-size photograph of the student (2 copies with white background)</li>
                            //    <li>Recent passport-size photographs of both parents (1 copy each with white background)</li>
                            //    <li>Copy of the student’s caste certificate (if applicable). Please note that the Father’s caste certificate will not be applicable.</li>
                            //</ol>
                            //<p>We look forward to welcoming you and seeing your child excel during this stage of the admissions process.</p>
                            //<p>Warm Regards,<br><br>Admissions Team<br>{orgName}</p>";


                        }
                    }
                    if (status_Id == 14 && org_Id == 213) //scheduled
                    {
                        if (classId == 1 || classId == 14 || classId == 15 || classId == 16)
                        {
                            mailMessage.Subject = $"{orgName} | Principal Interaction on {formattedDateTime}";
                            body += $@"
                    <p>Thank you for submitting your application at {orgName}</p>
                    <p>We are pleased to invite you for the interaction of your child with the teacher & interaction of parents with the principal on {formattedDateTime} (reporting). It will take approximately 1 hour. Request both parents to be present for this interaction along with the child.</p>
                    <p>A detailed mail regarding the interaction process will be shared by our Admission Manager.</p>
                    <p>Please ensure you arrive on time so we can begin promptly. If you have any questions or need further assistance, feel free to reach out to us on <a href=""{mail}"">{mail}</a></p>
                    <p>Looking forward to seeing you!</p>

                    <p>Regards,<br><br>Admissions Team</p>";
                        }
                        else
                        {
                            mailMessage.Subject = $"{orgName} | Entrance Evaluation  on {formattedDateTime}";
                            body += $@"
 <p>Thank you for submitting your application at {orgName}</p>
                    <p>We are pleased to invite you for the interaction round with the principal on {formattedDateTime} (reporting). </p>
                    <p>A detailed mail regarding the Entrance Exam process will be shared by our Admission Manager. </p>
                    <p>It will take approximately 1 hour. Request both parents to be present for this interaction along with the child.</p>
                    <p>Please ensure you arrive on time so we can begin promptly. If you have any questions or need further assistance, feel free to reach out to us on <a href=""{mail}"">{mail}</a></p>
                    <p>Looking forward to seeing you!</p>
<p>Regards,<br><br>Admissions Team</p>";
                        }
                    }
                    if (status_Id == 13 && org_Id != 213)
                    {

                        if (classId == 1 || classId == 14 || classId == 15 || classId == 16)
                        {
                            mailMessage.Subject = $"Interaction & Document Submission is Rescheduled for {formattedDateTime}";
                            body += $@"
<p>We are pleased to invite you and your child to the next stage of our admissions process at {orgName}. Your scheduled interaction with our Counsellor / Coordinator, along with document verification and submission, will take place on {formattedDateTime}. We request that both parents attend this session, accompanied by your child.</p>
<p>The interaction with the counsellor is an important step in our shared journey towards excellence, ensuring that each child is positioned for success within our academic community.</p>
<p>Please ensure that you bring along the following documents for verification:</p>
<ul>
    <li>Printout of the Admission Form</li>
    <li>Original Birth Certificate / Copy of Birth Certificate attested by gazetted officer / Copy of Birth Certificate with notary stamp / Birth Certificate with QR code (Any one)</li>
    <li>Copy of Aadhar Card of the student</li>
    <li>Copies of Aadhar Card of both parents</li>
    <li>Proof of Residence
        <ul>
            <li>For Owners – Copy of Index 2 or an Electricity Bill</li>
            <li>For Tenants – Registered copy of the Rent Agreement</li>
        </ul>
    </li>
    <li>Original Blood Group Report by a certified pathologist</li>
    <li>Copy of Palava Smart Card (only added for LSG, CBG, Taloja campuses)</li>
    <li>Recent passport-size photographs (white background):
        <ul>
            <li>Student – 2 copies</li>
            <li>Parent – 1 copy each</li>
        </ul>
    </li>
    <li>Copy of the student’s caste certificate (If belonging to SC/ST/OBC/SBC/NT etc.). Please note that the father’s caste certificate will not be accepted.</li>
</ul>
<p>We look forward to welcoming you on campus and being a partner in your child’s journey towards excellence.</p>
<p>Warm Regards,<br><br>Admissions Team<br>{orgName}</p>";

                        }
                        else
                        {
                            mailMessage.Subject = $"Entrance Evaluation & Document Submission is ReScheduled for {formattedDateTime}";
                            body += $@"
<p>Thank you for submitting your application at {orgName}. We would like to inform you that the entrance evaluation and document submission for your ward has been scheduled for {formattedDateTime}.</p>
<p>The entrance evaluation is an important step in our shared journey towards excellence, ensuring that each child is positioned for success within our academic community.</p>
<p><strong>Details of the entrance evaluation:</strong></p>
<ol>
    <li>The evaluation will be conducted in the subjects of Math, English, and Hindi.</li>
    <li>Each paper will carry a total of 20 marks.</li>
    <li>The time allotted for each paper will be 20 minutes.</li>
    <li>The student must keep a pen or pencil and an eraser ready with him/her.</li>
</ol>
<p>We also invite you to visit the school for document verification and submission along with your ward.</p>
<p>Please bring the following documents for verification:</p>
<ol>
    <li>Printout of the Admission Form</li>
    <li>Original Birth Certificate / Copy of Birth Certificate attested by gazetted officer / Copy of Birth Certificate with notary stamp / Birth Certificate with QR code (Any one)</li>
    <li>Copy of the Student’s Aadhar Card</li>
    <li>Copies of both parents’ Aadhar Cards</li>
    <li>Proof of Residence:
        <ul>
            <li>If Owner – Copy of Index 2 or Electricity Bill</li>
            <li>If Tenant – Registered copy of the Rent Agreement</li>
        </ul>
    </li>
    <li>Original Blood group report by a certified pathologist, Vaccination Card, or Discharge Summary of Mother with the child’s blood group mentioned</li>
    <li>Copy of Full-Year result of AY 2023-24</li>
    <li>Copy of 1st Term result of AY 2024-25</li>
    <li>Copy of Full Year Marksheet of AY 2024-25 (To be submitted on or before 31st March 2025)</li>
    <li>Original School Leaving Certificate (To be submitted on or before 31st March 2025)</li>
    <li>Recent passport-size photograph of the student (2 copies with white background)</li>
    <li>Recent passport-size photographs of both parents (1 copy each with white background)</li>
    <li>Copy of the student’s caste certificate (if applicable). Please note that the Father’s caste certificate will not be applicable.</li>
</ol>
<p>We look forward to welcoming you and seeing your child excel during this stage of the admissions process.</p>
<p>Warm Regards,<br><br>Admissions Team<br>{orgName}</p>";

                        }
                    }
                    if (status_Id == 13 && org_Id == 213)
                    {
                        if (classId == 1 || classId == 14 || classId == 15 || classId == 16)
                        {
                            mailMessage.Subject = $"{orgName} | Principal Interaction Rescheduled on {formattedDateTime}";
                            body += $@"
<p>Thank you for submitting your application at {orgName}</p>
<p>We are pleased to invite you that interaction of the child with the teacher & interaction of parents with the principal is Rescheduled on {formattedDateTime} (reporting). It will take approximately 1 hour. Request both parents to be present for this interaction along with the child.</p>
<p>Regards,<br><br>Admissions Team</p>";
                        }
                        else
                        {
                            mailMessage.Subject = $"{orgName} | Entrance Evaluation Rescheduled on {formattedDateTime}";
                            body += $@"
<p>Thank you for submitting your application at {orgName}</p>
<p>We are pleased to inform you that the Entrance Evaluation and interaction of the child with the teacher & interaction of parents with the principal is Rescheduled on {formattedDateTime} (reporting). It will take approximately 1 hour. Request both parents to be present for this interaction along with the child.</p>
<p>Regards,<br><br>Admissions Team</p>";
                        }

                    }
                    if (status_Id == 9 && org_Id != 213)
                    {
                        mailMessage.Subject = $" Your Admission application for {orgName} is Confirmed , here are the next steps!";
                        body += $@"
<p>We are delighted to inform you that {leadName}’s admission has been successfully confirmed for {className}. This marks the beginning of an exciting journey as we work together to nurture your child’s growth and leadership potential.</p>
<p>To complete the process, please log in to the admission portal by visiting <a href='https://applyonline.lodhaworldschool.com'>https://applyonline.lodhaworldschool.com</a>. From there, kindly proceed by clicking on the payment link. Your child’s admission will be confirmed upon receipt of the admission fee along with the first installment of the annual fees.</p>
<p>Please note that the payment link will remain active for the next 3 days. If the payment is not made within this time frame, the application will expire.</p>
<p>Should you have any queries, feel free to contact us at {mail}.</p>
<p>We look forward to welcoming and supporting {leadName}’s journey toward excellence.</p>
<p>Warm Regards,<br><br>Admissions Team<br>{orgName}</p>";
                    }
                    if (status_Id == 9 && org_Id == 213)
                    {
                        mailMessage.Subject = $" Your Admission application for {orgName} is Confirmed , here are the next steps!";
                        body += $@"
<p>We are delighted to inform you that <b>{leadName}’s</b> admission has been successfully confirmed for <b>Grade {className}</b>. This marks the beginning of an exciting journey as we work together to nurture your child’s growth and leadership potential.</p>

<p>To complete the process, please log in to the admission portal by visiting <a href=""https://applyonline.lodhaoakwoodschool.com"">https://applyonline.lodhaoakwoodschool.com</a>. From there, kindly proceed by clicking on the payment link. Your child’s admission will be confirmed upon receipt of the admission fee along with the first installment of the annual fees.</p>

<p>Please note that the payment link will remain active for the next 3 days. If the payment is not made within this time frame, the application will expire.</p>

<p>Please ensure that you send payment transaction details through email only on <a href=""mailto:admissions@lodhaoakwoodschool.com"">admissions@lodhaoakwoodschool.com</a>.</p>

<p>Kindly fill up the below-mentioned details of the child and attach transaction details:</p>

<ul>
    <li>DATE OF PAYMENT</li>
    <li>STUDENT NAME</li>
    <li>STD</li>
    <li>AMOUNT</li>
    <li>UTR NUMBER</li>
</ul>

<p>We look forward to welcoming and supporting <b>{leadName}’s</b> journey toward excellence.</p>

<p>Warm Regards,<br><br>Admissions Team<br>Lodha Oakwood School</p>";
                    }
                    if (status_Id == 12 && org_Id != 213)
                    {
                        mailMessage.Subject = $"Thank you for your application to Lodha World School";
                        body += $@"
<p>Thank you for considering Lodha World School for your child’s education and for taking the time to participate in our admissions process.</p>
<p>After careful consideration, we believe that our current environment may not be the best fit for your child’s unique strengths and needs. Our aim is to ensure every learner thrives in an environment where they feel fully supported and engaged, and we are mindful of making decisions that are in the best interest of all students.</p>
<p>We understand that this may be disappointing, but please know that this decision is in no way a reflection of your child’s abilities or potential. We remain confident they will continue to flourish on their educational journey.</p>
<p>Should you have any questions or wish to receive feedback on the process, please feel free to reach out to us at <a href='https://applyonline.lodhaworldschool.com'>https://applyonline.lodhaworldschool.com</a>.</p>
<p>We value your interest in Lodha World School and wish your family all the best for the future.</p>
<p>Warm Regards,<br><br>Admissions Team<br>{orgName}</p>";

                    }
                    if (status_Id == 12 && org_Id == 213)
                    {
                        mailMessage.Subject = $"Thank you for your application to Lodha Oakwood School";
                        body += $@"
<p>Thank you for considering Lodha Oakwood School for your child’s education and for taking the time to participate in our admissions process.</p>
<p>After careful consideration, we believe that our current environment may not be the best fit for your child’s unique strengths and needs. Our aim is to ensure every learner thrives in an environment where they feel fully supported and engaged, and we are mindful of making decisions that are in the best interest of all students.</p>
<p>We understand that this may be disappointing, but please know that this decision is in no way a reflection of your child’s abilities or potential. We remain confident they will continue to flourish on their educational journey.</p>
<p>Should you have any questions or wish to receive feedback on the process, please feel free to reach out to us at <a href='mailto:admissions@lodhaoakwoodschool.com'>admissions@lodhaoakwoodschool.com</a>.</p>
<p>We value your interest in Lodha Oakwood School and wish your family all the best for the future.</p>
<p>Warm Regards,<br><br>Admissions Team<br>Lodha Oakwood School</p>";


                    }
                    if (status_Id == 3 && org_Id != 213)
                    {
                        mailMessage.Subject = $"Seat Confirmed – Welcome to Lodha World School!";
                        body += $@"
             <p>We are delighted to welcome you and your child to {orgName}!</p>
<p>At Lodha World School, we believe that every child is born with unique abilities, and when nurtured with love, respect, and trust, those abilities flourish. Here, your child will experience an education that fosters collaboration, creativity, experimentation, and innovation – without comparison or limitation.</p>
<p>As part of our mission to create the Leaders of Tomorrow, we are committed to nurturing global citizens who are passionate about bettering the world around them. Join us in this exciting journey as we prepare our young learners to take flight, soaring to new heights with curiosity, ambition, and joy in their hearts.</p>
<p>We value your feedback! To help us continue improving our admissions process and overall experience, kindly take a moment to share your thoughts by clicking this link - <a href=""https://forms.gle/J9XWerEVLYDmVwLD7"">Feedback Form</a>.</p>
<p>We will be sharing your child’s unique ID, email, and other details shortly. Information on procuring books and uniforms will also be provided closer to the start of the school year.</p>
<p>We look forward to embarking on this journey toward excellence together!</p>
<p>Warm Regards,<br><br>Admissions Team<br>{orgName}</p>";
                    }
                    if (status_Id == 3 && org_Id == 213)
                    {
                        mailMessage.Subject = $"Seat Confirmed – Welcome to Lodha Oakwood School!";
                        body += $@"
<p>We are delighted to welcome you and your child to Lodha Oakwood School!</p>
<p>At Lodha Oakwood School, we believe that every child is born with unique abilities and aim to maximize that potential, whether in academics or beyond. Here, your child will experience an education that fosters collaboration, creativity, experimentation, and innovation – without comparison or limitation.</p>
<p>As part of our mission to deliver the highest level of academic excellence, we are committed to nurturing global citizens who are passionate about bettering the world around them. Join us in this exciting journey as we prepare our young learners to take flight, soaring to new heights with curiosity, ambition, and joy in their hearts.</p>
<p>We value your feedback! To help us continue improving our admissions process and overall experience, kindly take a moment to share your thoughts by clicking this link - <a href=""https://forms.gle/NeuzLYbLMbRwRfkcA"">https://forms.gle/NeuzLYbLMbRwRfkcA</a></p>
<p>We will be sharing your child’s unique ID, email, and other details shortly. Information on procuring books and uniforms will also be provided closer to the start of the school year.</p>
<p>We look forward to embarking on this journey toward excellence together!</p>
<p>Warm Regards,<br><br>Admissions Team<br>Lodha Oakwood School</p>";
                    }

                    if (status_Id == 0 && org_Id != 213)
                    {
                        mailMessage.Subject = $"Application Received-{orgName}";
                        body += $@"
<p>Thank you for choosing {orgName}. We are pleased to confirm the receipt of your application for Grade {className}, and appreciate your interest in becoming a part of our vibrant learning community.</p>
<p>Your application number is {applicationNumber}. Kindly keep this reference for any future communication.</p>
<p>Our admissions team will review your application and reach out to you within the next 2 working days to guide you through the next steps. Please ensure to check your email regularly for further updates.</p>
<p>We look forward to supporting your child on their journey towards excellence.</p>
<p>Warm Regards,<br><br>Admissions Team<br><br>{orgName}</p>";
                    }
                    if (status_Id == 0 && org_Id == 213)
                    {
                        mailMessage.Subject = $"Application Received-{orgName}";
                        body += $@"
<p>Thank you for choosing {orgName}. We are pleased to confirm the receipt of your application for Grade {className}, and appreciate your interest in becoming a part of our vibrant learning community.</p>
<p>Your application number is {applicationNumber}. Kindly keep this reference for any future communication.</p>
<p>Our admissions team will review your application and reach out to you within the next 2 working days to guide you through the next steps. Please ensure to check your email regularly for further updates.</p>
<p>We look forward to supporting your child on their journey towards excellence.</p>
<p>Warm Regards,<br><br>Admissions Team<br>{orgName}</p>";
                    }
                    if (status_Id == 1000 && org_Id != 213)
                    {
                        mailMessage.Subject = $"Your application for {orgName} is not submitted!";
                        body += $@"

<p>Thank you for your attempt to register to the Lodha.</p>
<p>Unfortunately, your transaction has failed.</p>
<p>Request you to attempt again.</p>
 <p>Regards,<br><br>Admissions Team</p>";
                    }
                    if (status_Id == 1000 && org_Id == 213)
                    {
                        mailMessage.Subject = $"Your application for {orgName} is not submitted!";
                        body += $@"

<p>Thank you for your attempt to register to the Lodha.</p>
<p>Unfortunately, your transaction has failed.</p>
<p>Request you to attempt again.</p>
 <p>Regards,<br><br>Admissions Team</p>";
                    }
                    if (status_Id == 1006 && org_Id != 213)
                    {
                        mailMessage.Subject = $"Your application for {orgName} is not submitted!";
                        body += $@"

<p>Thank you for your attempt to register to the Lodha.</p>
<p>Unfortunately, we have not received an update from the payment gateway.</p>
<p>In case the transaction was successful at your end, please share a screenshot of the same.</p>
 <p>Regards,<br><br>Admissions Team</p>";
                    }
                    if (status_Id == 1006 && org_Id == 213)
                    {
                        mailMessage.Subject = $"Your application for {orgName} is not submitted!";
                        body += $@"

<p>Thank you for your attempt to register to the Lodha.</p>
<p>Unfortunately, we have not received an update from the payment gateway.</p>
<p>In case the transaction was successful at your end, please share a screenshot of the same.</p>
 <p>Regards,<br><br>Admissions Team</p>";
                    }

                    body += @"
<img src=""cid:logo"" alt=""Logo"" />
</body>
</html>";

                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = body;


                    if (org_Id != 213)
                    {
                        string imagePath = "Group/210/maillogo/logo.png";
                        string fullPath = Path.Combine(_env.ContentRootPath, imagePath);
                        LinkedResource logoResource = new LinkedResource(fullPath, MediaTypeNames.Image.Jpeg);
                        logoResource.ContentId = "logo";

                        AlternateView alternateView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                        alternateView.LinkedResources.Add(logoResource);
                        mailMessage.AlternateViews.Add(alternateView);

                        smtpClient.Send(mailMessage);
                    }
                    else if (org_Id == 213)
                    {
                        string imagePath = "Group/210/maillogo/logooak.jpg";
                        string fullPath = Path.Combine(_env.ContentRootPath,imagePath);
                        LinkedResource logoResource = new LinkedResource(fullPath, MediaTypeNames.Image.Jpeg);
                        logoResource.ContentId = "logo";

                        AlternateView alternateView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                        alternateView.LinkedResources.Add(logoResource);
                        mailMessage.AlternateViews.Add(alternateView);

                        smtpClient.Send(mailMessage);
                    }

                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending  email: {ex.Message}");
                return 0;
            }
        }

        [HttpPost("InsertUserData2")]
        public CommonModal InsertUserData2(UserDataModel userData)
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
                        cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "INSERT2";

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
                            int OrgId = 213;
                            string EmailId = getMailId(OrgId, userData.MobileNo, "GETMAILOAK");
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
                            int OrgId = 213;
                            string EmailId = getMailId(OrgId, userData.MobileNo, "GETMAILOAK");
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

        [HttpGet("AppCancellationRequest")]
        public string AppCancellationRequest(int org_Id, int student_Id, int academic_Id, string mode)
        {

            SqlConnection conn = new SqlConnection(commonCode.conStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = org_Id;
            cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = student_Id;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academic_Id;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;
            string n = cmd.ExecuteScalar().ToString();
            conn.Close();
            return n;
        }
    }
}
