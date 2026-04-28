using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Preadmission_Lodha.Models;
using Razorpay.Api;
using SixLabors.ImageSharp;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using static Preadmission_Lodha.Models.PaymentModel;

namespace Preadmission_Lodha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public PaymentController( IWebHostEnvironment env)
        {
            _env = env;
        }


        public static string chesksumValue;
        public static string chesksumKey;
        public static int transId;
        public string testurl;
        public static string salt;
        public string[] output = new string[3];

        public List<CrudFeeReceiptModel> det = new List<CrudFeeReceiptModel>();
        public static List<CrudFeeReceiptModel> det1 = new List<CrudFeeReceiptModel>();


        public class RemotePost
        {
            private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();


            public string Url = "";
            public string Method = "post";
            public string FormName = "form1";

            public void Add(string name, string value)
            {
                Inputs.Add(name, value);
            }

            public string BuildPostHtml()
            {
                var sb = new StringBuilder();
                sb.Append("<html><head>");
                sb.AppendFormat("</head><body onload=\"document.{0}.submit()\">", FormName);
                sb.AppendFormat("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url);
                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    sb.AppendFormat("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]);
                }
                sb.Append("</form>");
                sb.Append("</body></html>");
                return sb.ToString();
            }

            public void Post(HttpResponse response)
            {
                response.Clear();
                response.ContentType = "text/html; charset=utf-8";
                response.WriteAsync(BuildPostHtml()).GetAwaiter().GetResult();
            }

            public void Post()
            {
                throw new InvalidOperationException("ASP.NET Core does not support System.Web.HttpContext.Current. Use Post(HttpResponse response) instead.");
            }
        }
        public class VendorInfo
        {
            public string vendor_code { get; set; }
            public decimal split_amount_fixed { get; set; }
        }

        public class SplitInfo
        {
            public List<VendorInfo> vendors { get; set; }
        }
        [HttpPost("getArray1")]
        public string getArray1(List<CrudFeeReceiptModel> things)
        {
            try
            {

                DataTable Dt = new DataTable();
                int n = 0;
                OnlinePayment retData = new OnlinePayment();

                List<OnlinePayment> data = new List<OnlinePayment>();
                foreach (var t in things)
                {
                    SqlConnection conn = new SqlConnection(commonCode.conStr);

                    SqlCommand cmd = new SqlCommand("Pro_2021_onlinePayment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = t.org_Id;
                    cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = t.academic_Id;
                    cmd.Parameters.Add("@student_Id", SqlDbType.Int).Value = t.student_Id;
                    cmd.Parameters.Add("@transaction_amount", SqlDbType.Decimal).Value = t.amount;
                    cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "GET_MERCHANT_DETAILS";
                    conn.Open();

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    //Dt.Rows.Add(ds.Tables[0].Rows);
                    if (Dt.Rows.Count > 0)
                    {

                        for (int z = 0; z < ds.Tables[0].Rows.Count; z++)
                        {
                            Dt.Rows.Add(
                                ds.Tables[0].Rows[z].ItemArray[0],
                                ds.Tables[0].Rows[z].ItemArray[1],
                                ds.Tables[0].Rows[z].ItemArray[2],
                                ds.Tables[0].Rows[z].ItemArray[3],
                                ds.Tables[0].Rows[z].ItemArray[4],
                                ds.Tables[0].Rows[z].ItemArray[5],
                                ds.Tables[0].Rows[z].ItemArray[6],
                                ds.Tables[0].Rows[z].ItemArray[7],
                                ds.Tables[0].Rows[z].ItemArray[8],
                                ds.Tables[0].Rows[z].ItemArray[9],
                                ds.Tables[0].Rows[z].ItemArray[10],
                                ds.Tables[0].Rows[z].ItemArray[11],
                                ds.Tables[0].Rows[z].ItemArray[12],
                                ds.Tables[0].Rows[z].ItemArray[13],
                                ds.Tables[0].Rows[z].ItemArray[14],
                                ds.Tables[0].Rows[z].ItemArray[15],
                                ds.Tables[0].Rows[z].ItemArray[16],
                                ds.Tables[0].Rows[z].ItemArray[17],
                                ds.Tables[0].Rows[z].ItemArray[18],
                                ds.Tables[0].Rows[z].ItemArray[19]
                                );
                        }
                    }
                    else
                    {
                        Dt = ds.Tables[0];
                    }
                    conn.Close();
                }

                data = commonCode.ConvertDataTable<OnlinePayment>(Dt);
                // if (things[0].org_Id == 212)
                //{
                //    data[0].apiKey = "rzp_test_Bfjv3KifmtSU01";
                //    data[0].salt = "brQ9h8ZDDBjo5wwj5ytMBvh8";
                //    data[0].provider = "RAZORPAY"; // Example: Use "RAZORPAY" or other if required
                //}

                string logFileName = $"RazorpayLogspreinstallment_{things[0].org_Id}_{DateTime.Now:ddMMyyyy}_{things[0].student_Id}.txt";

                // Combine the file name with the virtual directory path
                string logFilePath = Path.Combine(_env.ContentRootPath,$"Group/Logs/{logFileName}");
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);
                LogToFile(logFilePath, "Processing Razorpay started...(getArray1)");


                det1.Clear();

                decimal? totalAmount = 0;
                string Udate1 = "";
                decimal splitAmount1 = 0;
                decimal splitAmount2 = 0;
                if (data[0].provider == "TRACKNPAY" && data[0].is_split == 1)
                {
                    transId = data[0].transId;
                    for (int i = 0; i < things.Count; i++)
                    {

                        things[i].transactionId = transId;
                        if (things[i].type_Name.ToUpper().Contains(data[0].split_type2))
                        {
                            splitAmount2 = splitAmount2 + things[i].amount;
                        }
                        else
                        {
                            splitAmount1 = splitAmount1 + things[i].amount;
                        }
                        totalAmount = totalAmount + things[i].amount;
                        if (string.IsNullOrWhiteSpace(Udate1))
                        {
                            Udate1 = Udate1 + things[i].amount + "-" + things[i].transactionId;
                        }
                        else
                        {
                            Udate1 = Udate1 + "*" + things[i].amount + "-" + things[i].transactionId;
                        }

                    }
                    try
                    {

                        n = det.Count;
                        det.AddRange(things);
                    }
                    catch (Exception e)
                    {
                        det = things;
                    }


                    // data[0].mode = "TEST";


                    var vendor1 = new VendorInfo
                    {
                        vendor_code = data[0].vendor_code1,
                        split_amount_fixed = splitAmount1
                    };

                    var vendor2 = new VendorInfo
                    {
                        vendor_code = data[0].vendor_code2,
                        split_amount_fixed = splitAmount2
                    };

                    // Creating a SplitInfo object
                    var splitInfo = new SplitInfo
                    {
                        vendors = new List<VendorInfo> { vendor1, vendor2 }
                    };

                    // Serializing SplitInfo object to JSON
                    string json = JsonSerializer.Serialize(splitInfo);
                    string hash = (data[0].salt + "|address_line_1|address_line_2|" + totalAmount + "|" + data[0].apiKey + "|" + (string.IsNullOrEmpty(things[0].city) ? "patna" : things[0].city) + "|IND|INR|" + (string.IsNullOrEmpty(things[0].description) ? "fee" : things[0].description) + "|" + (string.IsNullOrEmpty(things[0].email) ? "demo@evali.com" : things[0].email) + "|" + data[0].mode + "|" + things[0].student_Name.Trim() + "|" + data[0].transId + "|" + (string.IsNullOrEmpty(things[0].mobileNumber) ? "9898989898" : things[0].mobileNumber) + "|https://api.valaischool.com/api/Payment/return_responseTracknPay1|" + json + "|bihar|" + Udate1 + "|udf2|udf3|udf4|udf5|" + (string.IsNullOrEmpty(things[0].zipCode) ? "800001" : things[0].zipCode));

                    string h1 = hash.Trim();

                    chesksumValue = Generatehash51211(h1).ToUpper();

                    string url = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
                    salt = data[0].salt;
                    RemotePost remotepost = new RemotePost();
                    remotepost.Url = "https://pgbiz.omniware.in/v2/getpaymentrequest?api_key=" + data[0].apiKey + "&return_url=https://api.valaischool.com/api/Payment/return_responseTracknPay1&mode=" + data[0].mode + "&order_id=" + data[0].transId + "&amount=" + totalAmount + "&name=" + things[0].student_Name + "&currency=INR&description=" + (string.IsNullOrEmpty(things[0].description) ? "fee" : things[0].description) + "&address_line_1=address_line_1&address_line_2=address_line_2&phone=" + (string.IsNullOrEmpty(things[0].mobileNumber) ? "9898989898" : things[0].mobileNumber) + "&email=" + (string.IsNullOrEmpty(things[0].email) ? "demo@evali.com" : things[0].email) + "&city=" + (string.IsNullOrEmpty(things[0].city) ? "patna" : things[0].city) + "&split_info=" + json + "&state=bihar&country=IND&zip_code=" + (string.IsNullOrEmpty(things[0].zipCode) ? "800001" : things[0].zipCode) + "&udf1=" + Udate1 + "&udf2=udf2&udf3=udf3&udf4=udf4&udf5=udf5&hash=" + chesksumValue;

                    testurl = remotepost.Url.ToString();

                    string msg = msg = data[0].merchent_id + "|" + data[0].transId + "|NA|" + totalAmount + "|NA|NA|NA|INR|NA|R|" + data[0].security_id + "|" + Udate1 + "|NA|F|NA|NA|NA|NA|NA|NA|NA|https://api.valaischool.com/api/Payment/return_responseTracknPay1" + "|" + chesksumValue;


                    enterMsg(things[0].org_Id, things[0].academic_Id, things[0].student_Id, msg, url, testurl);
                    return testurl;
                }
                else if (data[0].provider == "TRACKNPAY")
                {
                    // && things[0].org_Id !=175
                    transId = data[0].transId;
                    for (int i = 0; i < things.Count; i++)
                    {
                        things[i].transactionId = transId;
                        totalAmount = totalAmount + things[i].amount;
                        if (string.IsNullOrWhiteSpace(Udate1))
                        {
                            Udate1 = Udate1 + things[i].amount + "-" + things[i].transactionId;
                        }
                        else
                        {
                            Udate1 = Udate1 + "*" + things[i].amount + "-" + things[i].transactionId;
                        }

                    }
                    try
                    {

                        n = det1.Count;
                        det1.AddRange(things);
                    }
                    catch (Exception e)
                    {
                        det1 = things;
                    }


                    // data[0].mode = "TEST";

                    string hash = (data[0].salt + "|address_line_1|address_line_2|" + totalAmount + "|" + data[0].apiKey + "|" + (string.IsNullOrEmpty(things[0].city) ? "patna" : things[0].city) + "|IND|INR|" + (string.IsNullOrEmpty(things[0].description) ? "fee" : things[0].description) + "|" + (string.IsNullOrEmpty(things[0].email) ? "demo@evali.com" : things[0].email) + "|" + data[0].mode + "|" + things[0].student_Name.Trim() + "|" + data[0].transId + "|" + (string.IsNullOrEmpty(things[0].mobileNumber) ? "9898989898" : things[0].mobileNumber) + "|https://api.valaischool.com/api/Payment/return_responseTracknPay1|bihar|" + Udate1 + "|udf2|udf3|udf4|udf5|" + (string.IsNullOrEmpty(things[0].zipCode) ? "800001" : things[0].zipCode));

                    string h1 = hash.Trim();

                    chesksumValue = Generatehash51211(h1).ToUpper();

                    string url = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
                    salt = data[0].salt;
                    RemotePost remotepost = new RemotePost();


                    remotepost.Url = "https://biz.traknpay.in/v2/getpaymentrequest?api_key=" + data[0].apiKey + "&return_url=https://api.valaischool.com/api/Payment/return_responseTracknPay1&mode=" + data[0].mode + "&order_id=" + data[0].transId + "&amount=" + totalAmount + "&name=" + things[0].student_Name + "&currency=INR&description=" + (string.IsNullOrEmpty(things[0].description) ? "fee" : things[0].description) + "&address_line_1=address_line_1&address_line_2=address_line_2&phone=" + (string.IsNullOrEmpty(things[0].mobileNumber) ? "9898989898" : things[0].mobileNumber) + "&email=" + (string.IsNullOrEmpty(things[0].email) ? "demo@evali.com" : things[0].email) + "&city=" + (string.IsNullOrEmpty(things[0].city) ? "patna" : things[0].city) + "&state=bihar&country=IND&zip_code=" + (string.IsNullOrEmpty(things[0].zipCode) ? "800001" : things[0].zipCode) + "&udf1=" + Udate1 + "&udf2=udf2&udf3=udf3&udf4=udf4&udf5=udf5&hash=" + chesksumValue;

                    testurl = remotepost.Url.ToString();
                    string msg = data[0].merchent_id + "|" + data[0].transId + "|NA|" + totalAmount + "|NA|NA|NA|INR|NA|R|" + data[0].security_id + "|" + Udate1 + "|NA|F|NA|NA|NA|NA|NA|NA|NA|https://api.valaischool.com/api/Payment/return_responseTracknPay1" + "|" + chesksumValue;

                    enterMsg(things[0].org_Id, things[0].academic_Id, things[0].student_Id, msg, url, testurl);
                    return testurl;
                }

                else if (data[0].provider == "BILLDESK")
                {
                    string URLAuth = things[0].URLAuth1;                                                                      // string URLAuth = "https://uat1.billdesk.com"; //paymant gateway URL
                    string postString = string.Format("msg={0}", things[0].msg); // values 
                    const string contentType = "application/x-www-form-urlencoded";
                    //System.Net.ServicePointManager.Expect100Continue = false;
                    System.Net.ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls;
                    CookieContainer cookies = new CookieContainer();
                    HttpWebRequest webRequest = WebRequest.Create(URLAuth) as HttpWebRequest;
                    webRequest.Method = "POST";
                    webRequest.ContentType = contentType;
                    webRequest.CookieContainer = cookies;
                    webRequest.Accept = "text/plain";
                    webRequest.ContentLength = postString.Length;

                    // ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
                    // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    //  ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    webRequest.Headers.Add("Origin", "https://www.valaischool.com");
                    webRequest.Referer = "https://www.valaischool.com/api/Payment/getPayment"; // Referrer URL to call the patment gatewat API
                    webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
                    byte[] bytes = new ASCIIEncoding().GetBytes(postString);
                    Stream requestWriter = webRequest.GetRequestStream();
                    requestWriter.Write(bytes, 0, bytes.Length);
                    requestWriter.Close();
                    //HttpWebResponse myResp = webRequest.GetResponse() as HttpWebResponse;
                    //var url = myResp.ResponseUri.AbsoluteUri; //Get the Response URL 

                    var url = URLAuth + "?" + postString;
                    StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                    string responseData = responseReader.ReadToEnd();
                    responseReader.Close();
                    webRequest.GetResponse().Close();
                    output[0] = responseData;
                    return url;




                }

                else if (data[0].provider == "RAZORPAY")
                {
                    // && things[0].org_Id !=175
                    //transId = data[0].transId;


                    for (int i = 0; i < things.Count; i++)
                    {
                        things[i].transactionId = data[data.Count - 1].transId;
                        things[i].transId = data[data.Count - 1].transId;

                        totalAmount = totalAmount + things[i].amount;

                        if (string.IsNullOrWhiteSpace(Udate1))
                        {
                            Udate1 = Udate1 + things[i].amount + "-" + things[i].transactionId;
                        }
                        else
                        {
                            Udate1 = Udate1 + "*" + things[i].amount + "-" + things[i].transactionId;
                        }
                        //if (things[0].org_Id == 223)
                        //{
                        //    //data[0].apiKey = "rzp_live_qQE7inemMZTwMV";
                        //    //data[0].salt = "iyl6WFYskRBRWKqHyBmdEL6I";
                        //    //data[0].provider = "RAZORPAY"; // Example: Use "RAZORPAY" or other if required
                        //    data[0].apiKey = "rzp_test_W7w5DfMGU7APNC";
                        //    data[0].salt = "5E2xLFCISLt3m4R8buRRCSlq";
                        //    data[0].provider = "RAZORPAY";

                        //}

                    }
                    //det1.Clear();
                    //try
                    //{

                    //    n = things.Count;
                    //    det1.AddRange(things);
                    //}
                    //catch (Exception e)
                    //{
                    //    det1 = things;
                    //}


                    try
                    {

                        if (det1 == null)
                        {
                            det1 = new List<CrudFeeReceiptModel>();
                        }

                        // Add items from 'things' to 'det1'
                        det1.AddRange(things);

                        // Database Insertion
                        using (SqlConnection conn = new SqlConnection(commonCode.conStr))
                        {
                            conn.Open();


                            try
                            {

                                var order_paymentid = Guid.NewGuid();
                                foreach (var item in things)
                                {
                                    using (SqlCommand cmd = new SqlCommand("SP_ManageStudentTransaction", conn))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@org_Id", item.org_Id);
                                        cmd.Parameters.AddWithValue("@transactionId", item.transactionId);
                                        cmd.Parameters.AddWithValue("@academic_Id", item.academic_Id);
                                        cmd.Parameters.AddWithValue("@msg", item.msg ?? (object)DBNull.Value);
                                        cmd.Parameters.AddWithValue("@academic_Year", item.academic_Year);
                                        cmd.Parameters.AddWithValue("@class_Name", item.class_Name);
                                        cmd.Parameters.AddWithValue("@NewOrOld", item.NewOrOld);
                                        cmd.Parameters.AddWithValue("@quota_id", item.quota_Id);
                                        cmd.Parameters.AddWithValue("@quota_Id1", item.quota_Id1);
                                        cmd.Parameters.AddWithValue("@class_Id", item.class_Id);
                                        cmd.Parameters.AddWithValue("@section_Id", item.section_Id);
                                        cmd.Parameters.AddWithValue("@section_Name", item.section_Name);
                                        cmd.Parameters.AddWithValue("@category_Id", item.category_Id);
                                        cmd.Parameters.AddWithValue("@subCategory_Id", item.subCategory_Id);
                                        cmd.Parameters.AddWithValue("@install1", item.install1);
                                        cmd.Parameters.AddWithValue("@install2", item.install2);
                                        cmd.Parameters.AddWithValue("@install3", item.install3);
                                        cmd.Parameters.AddWithValue("@install4", item.install4);
                                        cmd.Parameters.AddWithValue("@duration_Id", item.duration_Id);
                                        cmd.Parameters.AddWithValue("@type_Id", item.type_Id);
                                        cmd.Parameters.AddWithValue("@structure_Id", item.structure_Id);
                                        cmd.Parameters.AddWithValue("@term", item.term);
                                        cmd.Parameters.AddWithValue("@student_Id", item.student_Id);
                                        cmd.Parameters.AddWithValue("@receipt_Id", item.receipt_Id);
                                        cmd.Parameters.AddWithValue("@month_Id", item.month_Id);
                                        cmd.Parameters.AddWithValue("@install_Id", item.install_Id);
                                        cmd.Parameters.AddWithValue("@orderid", item.orderid);
                                        cmd.Parameters.AddWithValue("@transId", item.transId);
                                        cmd.Parameters.AddWithValue("@category_Name", item.category_Name);
                                        cmd.Parameters.AddWithValue("@StudentName", item.StudentName);
                                        cmd.Parameters.AddWithValue("@subCategory_Name", item.subCategory_Name);
                                        cmd.Parameters.AddWithValue("@duration_Name", item.duration_Name);
                                        cmd.Parameters.AddWithValue("@type_Name", item.type_Name);
                                        cmd.Parameters.AddWithValue("@student_Code", item.student_Code);
                                        cmd.Parameters.AddWithValue("@student_Name", item.student_Name);
                                        cmd.Parameters.AddWithValue("@receipt_Code", item.receipt_Code);
                                        cmd.Parameters.AddWithValue("@receipt_Mode", item.receipt_Mode);
                                        cmd.Parameters.AddWithValue("@cheque_Number", item.cheque_Number ?? (object)DBNull.Value);
                                        cmd.Parameters.AddWithValue("@reference_Code", item.reference_Code ?? (object)DBNull.Value);
                                        cmd.Parameters.AddWithValue("@bank_Name", item.bank_Name);
                                        cmd.Parameters.AddWithValue("@branch_Name", item.branch_Name);
                                        cmd.Parameters.AddWithValue("@structure_Amount", item.structure_Amount);
                                        cmd.Parameters.AddWithValue("@advance_Amount", item.advance_Amount);
                                        cmd.Parameters.AddWithValue("@credit_Amount", item.credit_Amount);
                                        cmd.Parameters.AddWithValue("@discount_Amount", item.discount_Amount);
                                        cmd.Parameters.AddWithValue("@changedDiscountAmount", item.changedDiscountAmount);
                                        cmd.Parameters.AddWithValue("@receipt_Amount", item.receipt_Amount);
                                        cmd.Parameters.AddWithValue("@balance_Amount", item.balance_Amount);
                                        cmd.Parameters.AddWithValue("@payable_Amount", item.payable_Amount);

                                        cmd.Parameters.Add("@cheque_Date", SqlDbType.Date).Value = item.cheque_Date != DateTime.MinValue ? Convert.ToDateTime(item.cheque_Date).Date : DateTime.Now.Date;
                                        cmd.Parameters.Add("@due_Date", SqlDbType.Date).Value = item.due_Date != DateTime.MinValue ? Convert.ToDateTime(item.due_Date).Date : DateTime.Now.Date;
                                        cmd.Parameters.Add("@payment_Date", SqlDbType.Date).Value = item.payment_Date != DateTime.MinValue ? Convert.ToDateTime(item.payment_Date).Date : DateTime.Now.Date;
                                        cmd.Parameters.Add("@cancel_Date", SqlDbType.Date).Value = item.cancel_Date != DateTime.MinValue ? Convert.ToDateTime(item.cancel_Date).Date : DateTime.Now.Date;
                                        cmd.Parameters.Add("@dd_Date", SqlDbType.Date).Value = item.dd_Date != DateTime.MinValue ? Convert.ToDateTime(item.dd_Date).Date : DateTime.Now.Date;
                                        cmd.Parameters.Add("@receipt_Date", SqlDbType.Date).Value = item.receipt_Date != DateTime.MinValue ? Convert.ToDateTime(item.receipt_Date).Date : DateTime.Now.Date;



                                        cmd.Parameters.AddWithValue("@bal_CreditAmount", item.bal_CreditAmount);
                                        cmd.Parameters.AddWithValue("@receipt_Remark", item.receipt_Remark ?? (object)DBNull.Value);
                                        cmd.Parameters.AddWithValue("@fine_Amount", item.fine_Amount);
                                        cmd.Parameters.AddWithValue("@additional_Charge", item.additional_Charge);
                                        cmd.Parameters.AddWithValue("@receipt_Cancel", item.receipt_Cancel);
                                        cmd.Parameters.AddWithValue("@status", item.status);
                                        cmd.Parameters.AddWithValue("@user_Name", item.user_Name);
                                        cmd.Parameters.AddWithValue("@ip_Address", item.ip_Address);
                                        cmd.Parameters.AddWithValue("@mode", item.mode);
                                        cmd.Parameters.AddWithValue("@admission_No", item.admission_No);
                                        cmd.Parameters.AddWithValue("@father_Name", item.father_Name);
                                        cmd.Parameters.AddWithValue("@amount", item.amount);
                                        cmd.Parameters.AddWithValue("@Payment_Status", item.Payment_Status);
                                        cmd.Parameters.AddWithValue("@mobileNumber", item.mobileNumber);
                                        cmd.Parameters.AddWithValue("@zipCode", item.zipCode);
                                        cmd.Parameters.AddWithValue("@URLAuth1", item.URLAuth1);
                                        cmd.Parameters.AddWithValue("@description", item.description);
                                        cmd.Parameters.AddWithValue("@email", item.email);
                                        cmd.Parameters.AddWithValue("@city", item.city);
                                        cmd.Parameters.AddWithValue("@order_paymentid", order_paymentid);
                                        cmd.Parameters.AddWithValue("@fee_Method", "Preadmission Fee");
                                        cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = "INSERT";
                                        cmd.ExecuteNonQuery();
                                    }
                                }


                            }
                            catch
                            {

                                throw;
                            }

                        }


                    }
                    catch (Exception e)
                    {
                        det1 = things;
                    }





                    //data[0].salt + "|address_line_1|address_line_2|" + things[0].amount + "|" + data[0].apiKey + "|" + things[0].city + "|IND|INR|" + things[0].description + "|" + things[0].email + "|" + things[0].mode + "|" + things[0].student_Name + "|" + things[0].transId + "|" + things[0].mobileNumber + "|http://api.addod.in/api/Payment/return_responseTracknPay|bihar|udf1|udf2|udf3|udf4|udf5|" + things[0].zipCode;
                    retData.address_line_1 = "address_line_1";
                    retData.address_line_2 = "address_line_2";
                    retData.amount = totalAmount.ToString();
                    retData.apiKey = data[0].apiKey; //KEY
                    retData.city = data[0].city;
                    retData.country = "IND";
                    retData.currency = "INR";
                    retData.description = data[0].description;
                    retData.email = data[0].email;
                    retData.mobileNumber = data[0].mobileNumber;
                    retData.mode = data[0].mode;
                    //retData.order_id = data[0].transId.ToString();
                    retData.return_url = "https://login.valaischool.com/";
                    retData.salt = data[0].salt; //PASSWORD                
                    retData.StudentName = data[0].StudentName;
                    retData.transId = data[data.Count - 1].transId;
                    retData.zipCode = data[0].zipCode;
                    retData.org_Id = things[0].org_Id;
                    retData.academic_Id = things[0].academic_Id;
                    retData.student_Id = things[0].student_Id;
                    retData.provider = data[0].provider;
                    retData.admission_No = data[0].admission_No;

                    retData.udf1 = Udate1;
                    LogToFile(logFilePath, $"Received data: Udf={Udate1}");
                    det1 = things;
                    retData.Receipts = det1;
                    decimal totalamt = 0;
                    foreach (var pc in det1)
                    {
                        totalamt += (pc.payable_Amount == null ? 0 : pc.payable_Amount);
                    }

                    string url = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";

                    //decimal OrderAmount = Convert.ToDecimal(retData.amount);
                    decimal OrderAmount;

                    try
                    {
                        OrderAmount = Convert.ToDecimal(totalAmount);
                        LogToFile(logFilePath, $"Received data: OrderAmount={OrderAmount}");

                    }
                    catch (FormatException fe)
                    {
                        // Log the error or handle it
                        throw new Exception("Invalid amount format: " + totalAmount, fe);
                        LogToFile(logFilePath, $"Invalid amount format: {totalAmount} ");

                    }
                    catch (Exception ex)
                    {
                        // Handle any other exception
                        throw new Exception("An error occurred while converting the amount: " + ex.Message, ex);
                        LogToFile(logFilePath, $"An error occurred while converting the amount ");

                    }

                    string orderId = CreateOrder(OrderAmount, retData.currency, retData.apiKey, retData.salt);

                    retData.order_id = orderId;
                    LogToFile(logFilePath, $"Received data: orderId={orderId}");
                    if (totalamt == 0) { return "Payable Amount is 0"; }
                    FeeEntrypreadmissionwebonline_pending(det1, data[data.Count - 1].transId.ToString(), things[0].student_Id, retData.order_id);
                    LogToFile(logFilePath, $"Redirecting to Payment gatewayy....");
                    return JsonSerializer.Serialize(JsonSerializer.Serialize(retData));

                }

                else
                {

                    // chesksumKey = data[0].checksum_key;
                    transId = data[0].transId;
                    for (int i = 0; i < things.Count; i++)
                    {
                        things[i].transactionId = transId;

                    }
                    try
                    {
                        n = det.Count;
                        det.AddRange(things);
                    }
                    catch (Exception e)
                    {
                        det = things;
                    }
                    retData.address_line_1 = "address_line_1";
                    retData.address_line_2 = "address_line_2";
                    retData.amount = things[0].amount.ToString();
                    //  retData.apiKey = data[0].apiKey;
                    retData.city = data[0].city;
                    retData.country = "IND";
                    retData.currency = "INR";
                    retData.description = data[0].description;
                    retData.email = data[0].email;
                    retData.mobileNumber = data[0].mobileNumber;
                    retData.mode = data[0].mode;
                    retData.order_id = data[0].transId.ToString();
                    retData.return_url = "https://login.valaischool.com/";
                    //  retData.salt = data[0].salt;
                    retData.state = "state";
                    retData.StudentName = data[0].StudentName;
                    retData.transId = data[0].transId;
                    retData.udf1 = "udf1";
                    retData.udf2 = "udf2";
                    retData.udf3 = "udf3";
                    retData.udf4 = "udf4";
                    retData.udf5 = "udf5";
                    retData.zipCode = data[0].zipCode;

                    retData.merchent_id = data[0].merchent_id;
                    retData.security_id = data[0].security_id;
                    retData.checksum_key = data[0].checksum_key;



                    chesksumValue = BitConverter.ToString(hmacSHA256(data[0].merchent_id + "|" + data[0].transId + "|NA|" + things[0].amount + "|NA|NA|NA|INR|NA|R|" + data[0].security_id + "|NA|NA|F|NA|NA|NA|NA|NA|NA|NA|https://api.valaischool.com/api/Payment/return_response", data[0].checksum_key)).Replace("-", "").ToUpper();
                    string msg = data[0].merchent_id + "|" + data[0].transId + "|NA|" + things[0].amount + "|NA|NA|NA|INR|NA|R|" + data[0].security_id + "|NA|NA|F|NA|NA|NA|NA|NA|NA|NA|https://api.valaischool.com/api/Payment/return_response" + "|" + chesksumValue;
                    string url = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
                    //makepayment pay1 = new makepayment();
                    things[0].msg = msg;


                    string URLAuth1 = data[0].URLAuth1;
                    things[0].URLAuth1 = URLAuth1;

                    testurl = getPayment(things);
                    retData.testurl = testurl;// payment gatewat method
                    enterMsg(things[0].org_Id, things[0].academic_Id, things[0].student_Id, msg, url, testurl);

                    return retData.testurl;


                }


            }
            catch (Exception e)
            {
                return "fail";
            }

        }

        [HttpPost("getPayment")]
        public string getPayment(List<CrudFeeReceiptModel> things)
        {
            //NOTE: Billdest API will accept only when the API is Call from the proper Refeffer URL Which we given to the BILLDESK
            // string URLAuth = "https://uat.billdesk.com/pgidsk/PGIMerchantPayment"; //paymant gateway URL
            string URLAuth = things[0].URLAuth1;                                                                      // string URLAuth = "https://uat1.billdesk.com"; //paymant gateway URL
            string postString = string.Format("msg={0}", things[0].msg); // values 
            const string contentType = "application/x-www-form-urlencoded";
            //System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls;
            CookieContainer cookies = new CookieContainer();
            HttpWebRequest webRequest = WebRequest.Create(URLAuth) as HttpWebRequest;
            webRequest.Method = "POST";
            webRequest.ContentType = contentType;
            webRequest.CookieContainer = cookies;
            webRequest.Accept = "text/plain";
            webRequest.ContentLength = postString.Length;

            // ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //  ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            webRequest.Headers.Add("Origin", "https://www.valaischool.com");
            webRequest.Referer = "https://www.valaischool.com/api/Payment/getPayment"; // Referrer URL to call the patment gatewat API
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            byte[] bytes = new ASCIIEncoding().GetBytes(postString);
            Stream requestWriter = webRequest.GetRequestStream();
            requestWriter.Write(bytes, 0, bytes.Length);
            requestWriter.Close();
            //HttpWebResponse myResp = webRequest.GetResponse() as HttpWebResponse;
            //var url = myResp.ResponseUri.AbsoluteUri; //Get the Response URL 

            var url = URLAuth + "?" + postString;
            StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
            string responseData = responseReader.ReadToEnd();
            responseReader.Close();
            webRequest.GetResponse().Close();
            output[0] = responseData;
            return url;
        }
        [NonAction]
        private void LogToFile(string filePath, string message)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }

        [NonAction]
        public string Generatehash51211(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }

        [NonAction]
        public void enterMsg(int orgId, int academicId, int studentId, string msg, string url, string testUrl)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("Pro_onlinePayment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = orgId;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academicId;
            cmd.Parameters.Add("@student_Id", SqlDbType.Int).Value = studentId;
            cmd.Parameters.Add("@msg", SqlDbType.NVarChar).Value = msg;
            cmd.Parameters.Add("@url", SqlDbType.NVarChar).Value = url;
            cmd.Parameters.Add("@testUrl", SqlDbType.NVarChar).Value = testUrl;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "GET_MERCHANT_DETAILS_MSG";
            conn.Open();
            int n = cmd.ExecuteNonQuery();
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

        [NonAction]
        private byte[] hmacSHA256(string v, string checksum_key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(checksum_key)))
            {
                try
                {
                    return hmac.ComputeHash(Encoding.ASCII.GetBytes(v));
                }
                catch (Exception e)
                {
                    byte[] r = null;
                    return r;
                }
            }
        }

        [HttpPost("FeeEntrypreadmissionwebonline_pending")]
        public string FeeEntrypreadmissionwebonline_pending(List<CrudFeeReceiptModel> feeArray, string refCode, int studentId, string order_id)
        {
            string logFileName = $"RazorpayLogsnormalfeecore_{feeArray[0].org_Id}_{DateTime.Now:ddMMyyyy}_{feeArray[0].student_Id}.txt";

            // Combine the file name with the virtual directory path
            //string logFilePath = HttpContext.Current.Server.MapPath($"~/Group/{feeArray[0].org_Id}/Logs/{logFileName}");

            string basePath = Path.Combine(_env.ContentRootPath,"Group/" + feeArray[0].org_Id.ToString() + "/Logs");

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            string logFilePath = Path.Combine(basePath, logFileName);


            LogToFile(logFilePath, "FeeEntry Method Started in core.");

            LogToFile(logFilePath, $"Debug: refCode value before validation: '{refCode}'");

            if (string.IsNullOrWhiteSpace(refCode))
            {
                LogToFile(logFilePath, "Error: Reference Code is required.");
                return "Error: Reference Code is required.";
            }



            FeeCommonController feco = new FeeCommonController();


            decimal totalPayableAmt = 0;
            FeeCommonController f = new FeeCommonController();

            int receiptCode = 0;
            List<getFeeReceiptCodeModel> receipt = feco.getFeeReceiptCode(feeArray[0].org_Id, "RECEIPT_NO");
            receiptCode = receipt[0].receipt_Code;
            LogToFile(logFilePath, $"Received data(receiptCode): org_Id={feeArray[0].org_Id}, academic_Id={feeArray[0].academic_Id}, student_Id={feeArray[0].student_Id},receiptCode = {receipt[0].receipt_Code}");

            foreach (var n in feeArray)
            {
                // if ((n.student_Id == studentId) && (n.Payment_Status == "success"))
                if (n.student_Id == feeArray[0].student_Id)

                {
                    LogToFile(logFilePath, $"Received data(Payment_Status == success): org_Id={feeArray[0].org_Id}, academic_Id={feeArray[0].academic_Id}, student_Id={feeArray[0].student_Id}");
                    var t = n;

                    //if (t.org_Id != null && t.student_Id == studentId)
                    if (t.org_Id != null && t.student_Id == feeArray[0].student_Id)

                    {
                        LogToFile(logFilePath, $"Received data(t.payable_Amount should be greater than zero): org_Id={feeArray[0].org_Id}, payable_Amount={t.payable_Amount}, student_Id={feeArray[0].student_Id}");

                        if (t.payable_Amount > 0)
                        {
                            LogToFile(logFilePath, $"Received data(t.payable_Amount should be greater than zero & it is inside method): org_Id={feeArray[0].org_Id}, payable_Amount={t.payable_Amount}, student_Id={feeArray[0].student_Id}");
                            if (t.changedDiscountAmount > 0)
                            {
                                LogToFile(logFilePath, $"Received data(t.changedDiscountAmount should be greater than zero & it is inside method): org_Id={feeArray[0].org_Id}, changedDiscountAmount={t.changedDiscountAmount}, student_Id={feeArray[0].student_Id}");

                                CrudFeeDiscountModel s = new CrudFeeDiscountModel();
                                s.org_Id = t.org_Id;
                                s.academic_Id = t.academic_Id;
                                //new code start
                                s.category_Id = t.category_Id != null ? t.category_Id : 0;
                                s.subCategory_Id = t.subCategory_Id != null ? t.subCategory_Id : 0;
                                //new code end
                                s.class_Id = t.class_Id;
                                s.NewOrOld = t.NewOrOld;
                                s.quota_id = (t.quota_Id == null ? 0 : t.quota_Id);
                                s.duration_Id = t.duration_Id;
                                s.type_Id = t.type_Id;
                                s.structure_Id = t.structure_Id;
                                s.student_Id = t.student_Id;
                                s.discount_Id = 0;
                                s.discount_Type = 1;
                                s.discount_Amount = t.changedDiscountAmount;
                                s.discount_Date = DateTime.Now;
                                s.discount_Reason = "Single payment";
                                s.structure_Amount = t.structure_Amount;
                                s.receipt = receiptCode;
                                s.status = 1;
                                s.month_Id = t.month_Id;
                                s.user_Name = t.user_Name != null ? t.user_Name : "";
                                s.ip_Address = t.ip_Address != null ? t.ip_Address : "";
                                s.mode = t.duration_Id > 2 ? "CREATE1" : "CREATEMONTHLY1";
                                var a = f.crudFeeDiscountMaster(s);
                            }
                            CrudFeeReceiptModel r = new CrudFeeReceiptModel();
                            LogToFile(logFilePath, $"Received data(receipt_Amount enters & it is inside method): org_Id={feeArray[0].org_Id}, student_Id={feeArray[0].student_Id}");

                            r.org_Id = t.org_Id;
                            LogToFile(logFilePath, $"Received data(org_Id): org_Id={t.org_Id}");
                            r.academic_Id = t.academic_Id;
                            LogToFile(logFilePath, $"Received data(academic_Id): org_Id={t.academic_Id}");
                            //new code start
                            r.category_Id = t.category_Id != null ? t.category_Id : 0;
                            LogToFile(logFilePath, $"Received data(category_Id): org_Id={t.category_Id}");
                            r.subCategory_Id = t.subCategory_Id != null ? t.subCategory_Id : 0;
                            LogToFile(logFilePath, $"Received data(subCategory_Id): org_Id={t.subCategory_Id}");
                            //new code end
                            r.class_Id = t.class_Id;
                            LogToFile(logFilePath, $"Received data(class_Id): org_Id={t.class_Id}");
                            r.quota_id = (t.quota_Id == null ? 0 : t.quota_Id);
                            LogToFile(logFilePath, $"Received data(quota_id): org_Id={(t.quota_Id == null ? 0 : t.quota_Id)}");
                            r.NewOrOld = t.NewOrOld;
                            LogToFile(logFilePath, $"Received data(NewOrOld): org_Id={t.NewOrOld}");
                            r.duration_Id = t.duration_Id;
                            LogToFile(logFilePath, $"Received data(duration_Id): org_Id={t.duration_Id}");
                            r.type_Id = t.type_Id;
                            LogToFile(logFilePath, $"Received data(type_Id): org_Id={t.type_Id}");
                            r.structure_Id = t.structure_Id;
                            LogToFile(logFilePath, $"Received data(structure_Id): org_Id={t.structure_Id}");
                            r.student_Id = t.student_Id;
                            LogToFile(logFilePath, $"Received data(student_Id): org_Id={t.student_Id}");
                            r.receipt_Id = t.receipt_Id;
                            LogToFile(logFilePath, $"Received data(receipt_Id): org_Id={t.receipt_Id}");
                            r.student_Code = t.student_Code;
                            LogToFile(logFilePath, $"Received data(student_Code): org_Id={t.student_Code}");
                            r.receipt_Code = receiptCode.ToString();
                            r.receipt_Mode = "3";
                            r.cheque_Number = t.cheque_Number != null ? t.cheque_Number : "";
                            r.cheque_Date = t.cheque_Date != DateTime.MinValue ? t.cheque_Date : DateTime.Now;
                            r.dd_Number = t.dd_Number != null ? t.dd_Number : "";
                            r.dd_Date = t.dd_Date != null ? t.dd_Date : DateTime.Now;
                            r.reference_Code = refCode != null ? refCode : "";
                            r.payment_Date = t.payment_Date != DateTime.MinValue ? t.payment_Date : DateTime.Now;
                            r.bank_Name = t.bank_Name != null ? t.bank_Name : "";
                            r.branch_Name = t.branch_Name != null ? t.branch_Name : "";
                            r.month_Id = t.month_Id;
                            LogToFile(logFilePath, $"Received data(month_Id): org_Id={t.month_Id}");
                            r.payable_Amount = t.payable_Amount;
                            LogToFile(logFilePath, $"Received data(payable_Amount): org_Id={t.payable_Amount}");
                            r.bal_CreditAmount = t.bal_CreditAmount;
                            LogToFile(logFilePath, $"Received data(bal_CreditAmount): org_Id={t.bal_CreditAmount}");
                            r.balance_Amount = t.balance_Amount;
                            LogToFile(logFilePath, $"Received data(balance_Amount): balance_Amount={t.balance_Amount}");
                            r.structure_Amount = t.structure_Amount;
                            LogToFile(logFilePath, $"Received data(structure_Amount): structure_Amount={t.structure_Amount}");
                            r.discount_Amount = t.discount_Amount;
                            LogToFile(logFilePath, $"Received data(discount_Amount): discount_Amount={t.discount_Amount}");
                            r.receipt_Amount = t.payable_Amount;
                            LogToFile(logFilePath, $"Received data(payable_Amount): payable_Amount={t.payable_Amount}");
                            r.receipt_Date = DateTime.Now;
                            LogToFile(logFilePath, $"Received data(receipt_Date): receipt_Date={r.receipt_Date}");
                            r.receipt_Remark = t.receipt_Remark != null ? t.receipt_Remark : "";
                            LogToFile(logFilePath, $"Received data(receipt_Remark): receipt_Remark={t.receipt_Remark}");
                            r.fine_Amount = t.fine_Amount;
                            LogToFile(logFilePath, $"Received data(fine_Amount): fine_Amount={t.fine_Amount}");
                            r.trans_id = t.transId;
                            r.order_id = order_id;
                            r.additional_Charge = t.additional_Charge;
                            LogToFile(logFilePath, $"Received data(additional_Charge): additional_Charge={t.additional_Charge}");

                            r.receipt_Cancel = 2;//for pending
                            LogToFile(logFilePath, $"Received data(receipt_Cancel): receipt_Cancel={t.receipt_Cancel}");
                            r.cancel_Date = t.cancel_Date != DateTime.MinValue ? t.cancel_Date : DateTime.Now;
                            LogToFile(logFilePath, $"Received data(cancel_Date): cancel_Date={t.cancel_Date}");
                            r.status = 5;//for pending
                            r.user_Name = t.user_Name != null ? t.user_Name : "";
                            LogToFile(logFilePath, $"Received data(user_Name): user_Name={t.user_Name}");
                            r.ip_Address = t.ip_Address != null ? t.ip_Address : "";
                            LogToFile(logFilePath, $"Received data(ip_Address): ip_Address={t.ip_Address}");
                            r.mode = t.month_Id == 0 ? "CREATE1" : "MONTHLYCREATE1";
                            LogToFile(logFilePath, $"Received data(month_Id): month_Id={t.month_Id}");

                            LogToFile(logFilePath, $"Received data(crudMonthlyFeeReceiptMaster): org_Id={r.org_Id}, academic_Id={r.academic_Id}, category_Id={r.category_Id}, " +
$"subCategory_Id={r.subCategory_Id}, class_Id={r.class_Id}, NewOrOld={r.NewOrOld}, quota_id={r.quota_id}, " +
$"duration_Id={r.duration_Id}, type_Id={r.type_Id}, structure_Id={r.structure_Id}, student_Id={r.student_Id}, " +
$"receipt_Id={r.receipt_Id}, student_Code={r.student_Code}, receipt_Code={r.receipt_Code}, " +
$"receipt_Mode={r.receipt_Mode}, cheque_Number={r.cheque_Number}, cheque_Date={r.cheque_Date}, dd_Number={r.dd_Number}, " +
$"dd_Date={r.dd_Date}, reference_Code={r.reference_Code}, payment_Date={r.payment_Date}, bank_Name={r.bank_Name}, " +
$"branch_Name={r.branch_Name}, month_Id={r.month_Id}, payable_Amount={r.payable_Amount}, bal_CreditAmount={r.bal_CreditAmount}, " +
$"balance_Amount={r.balance_Amount}, structure_Amount={r.structure_Amount}, discount_Amount={r.discount_Amount}, " +
$"receipt_Amount={r.receipt_Amount}, receipt_Date={r.receipt_Date}, receipt_Remark={r.receipt_Remark}, fine_Amount={r.fine_Amount}, " +
$"additional_Charge={r.additional_Charge}, receipt_Cancel={r.receipt_Cancel}, cancel_Date={r.cancel_Date}, status={r.status}, " +
$"user_Name={r.user_Name}, ip_Address={r.ip_Address}, mode={r.mode}");



                            try
                            {
                                var b = f.crudMonthlyFeeReceiptMaster(r);
                                totalPayableAmt += (t.payable_Amount == null ? 0 : t.payable_Amount);

                                // totalPayableAmt += t.payable_Amount;

                                LogToFile(logFilePath, $"Saved receipt for structure_Id={t.structure_Id}, payable_Amount={t.payable_Amount}");
                            }
                            catch (Exception ex)
                            {
                                LogToFile(logFilePath, $"❌ Error in crudMonthlyFeeReceiptMaster: {ex.Message}\nStack: {ex.StackTrace}");
                            }


                            LogToFile(logFilePath, $"Received data(totalPayableAmt): totalPayableAmt={totalPayableAmt}");
                            LogToFile(logFilePath, $"Received data(Details of amount): org_Id={t.org_Id},academic_Id={t.academic_Id},student_Id={t.student_Id}," +
                                $"class_Id={t.class_Id},quota_Id={t.quota_Id},NewOrOld={t.NewOrOld},duration_Id={t.duration_Id}, " +
                                $"type_Id={t.type_Id},structure_Id={t.structure_Id},receipt_Id={t.receipt_Id},month_Id={t.month_Id}," +
                                $"receipt_Amount={t.receipt_Amount},payable_Amount={t.payable_Amount},user_Name={t.user_Name},receipt_Id={t.receipt_Id},mode={r.mode}");
                            //acc 
                            crudAccountsPostingModel1 ap2 = new crudAccountsPostingModel1();
                            ap2.org_Id = feeArray[0].org_Id;
                            ap2.receipt_Date = DateTime.Now;
                            ap2.transCode = 1;
                            ap2.transType = "Receipt-pending";
                            ap2.accountsCode = 9;
                            ap2.credit_Amount = t.payable_Amount;//feeArray[0].amount;
                            ap2.debit_Amount = 0;
                            ap2.typeId = t.type_Id;
                            ap2.academicId = t.academic_Id;
                            ap2.directOrIndirect = 1;

                            ap2.receipt_Code = receiptCode.ToString();
                            ap2.student_Id = feeArray[0].student_Id;
                            ap2.structure_Id = t.structure_Id;  //Tamil
                            ap2.class_Id = t.class_Id;  //Tamil
                            var acc2 = f.crudAccountsPostingNew1(ap2);

                            //acc
                        }
                    }

                }
            }
            CreditNoteController c = new CreditNoteController();
            List<CreditNote> creditAmt = c.getStudentDetail(feeArray[0].org_Id, feeArray[0].student_Id, "FEECOLLECTIONCREDITNOTE");
            decimal creditAmount = creditAmt[0].advance_Amount;
            decimal balCreditAmount = 0;
            if (creditAmount > 0)
            {
                if (creditAmount < totalPayableAmt)
                {
                    balCreditAmount = 0;
                }
                else if (creditAmount > totalPayableAmt)
                {
                    balCreditAmount = (creditAmount - totalPayableAmt);
                }
                CrudFeeReceiptModel p = new CrudFeeReceiptModel();
                p.org_Id = feeArray[0].org_Id;
                p.student_Id = feeArray[0].student_Id;
                p.bal_CreditAmount = balCreditAmount;
                p.mode = "UPDATECREDITAMOUNT";
                var b = f.updateCreditNote(p);
            }
            crudAccountsPostingModel ap = new crudAccountsPostingModel();
            ap.org_Id = feeArray[0].org_Id;
            ap.receipt_Date = DateTime.Now;
            ap.transCode = 1;
            ap.transType = "Receipt-pending";
            ap.accountsCode = 9;
            ap.credit_Amount = feeArray[0].amount;
            ap.debit_Amount = 0;
            ap.receipt_Code = receiptCode.ToString();
            ap.student_Id = feeArray[0].student_Id;
            var acc = f.crudAccountsPosting(ap);
            //for (int i = feeArray.Count - 1; i >= 0; i--)
            //{
            //    if (feeArray[i].student_Id == studentId)
            //    {
            //        feeArray.RemoveAt(i);
            //    }
            //}
            return 0.ToString();
        }


        [HttpPost("return_responsRazorpay1")]
        public IActionResult return_responsRazorpay1()
        {
            int status;
            var x = Request.Form;
            int student_Id = int.Parse(x["student_Id"]);
            int org_Id = int.Parse(x["org_Id"]);
            // string logFilePath = $@"E:\Valaischool\Backendcode\School_04-11-2024\Group\Logs\RazorpayLogspreinstallment_{DateTime.Now:ddMMyyyy}_{student_Id}.txt";
            string logFileName = $"RazorpayLogspreinstallment_{org_Id}_{DateTime.Now:ddMMyyyy}_{student_Id}.txt";

            // Combine the file name with the virtual directory path
            string logFilePath = Path.Combine(_env.ContentRootPath, $"Group/Logs/{logFileName}");
            try
            {

                // Log request details
                LogToFile(logFilePath, "Processing Razorpay response...(return_responsRazorpay1)");

                List<string> tids = new List<string>();

                var Udf = Convert.ToString(x["udf1"]);

                Console.WriteLine(Udf);
                var splitPerTran = Udf.Split('*');
                foreach (var items in splitPerTran)
                {
                    var Tid = items.Split('-')[1];
                    tids.Add(Tid);
                }
                LogToFile(logFilePath, $"Received data: Udf={Udf}");

                string amount = Convert.ToString(x["amount"]);
                string currency = Convert.ToString(x["currency"]);
                string order_id = Convert.ToString(x["order_id"]);
                string payment_datetime = Convert.ToString(x["payment_datetime"]);
                string response_code = Convert.ToString(x["response_code"]);
                string responseMessage = Convert.ToString(x["response_message"]);
                string transaction_id = Convert.ToString(x["transaction_id"]);
                string hashValue = x["hash"];
                string provider = x["provider"];
                //int org_Id = int.Parse(x["org_Id"]);
                int academic_Id = int.Parse(x["academic_Id"]);
                //int student_Id = int.Parse(x["student_Id"]);
                int transac = Convert.ToInt32(x["order_id"]);//ewlly

                LogToFile(logFilePath, $"Received data: OrderID={order_id}, Amount={amount}, ResponseCode={response_code},transaction_id={transaction_id}");
                if (provider == "RAZORPAY")

                {
                    string transId = Convert.ToString(getTransId(org_Id, academic_Id, student_Id));

                    if (tids.Contains(transId.ToString()))
                    {
                        SqlConnection conn = new SqlConnection(commonCode.conStr);
                        SqlCommand cmd = new SqlCommand("Pro_2021_onlinePayment", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@msg", SqlDbType.NVarChar).Value = chesksumValue;
                        cmd.Parameters.Add("@transaction_Id", SqlDbType.NVarChar).Value = order_id; //created in sql
                        cmd.Parameters.Add("@payment_trans_Id", SqlDbType.NVarChar).Value = transaction_id;
                        cmd.Parameters.Add("@payment_Vendor", SqlDbType.NVarChar).Value = "RAZORPAY";
                        cmd.Parameters.Add("@merchant_id", SqlDbType.NVarChar).Value = order_id;
                        cmd.Parameters.Add("@currency_Code", SqlDbType.NVarChar).Value = currency;
                        cmd.Parameters.Add("@payment_status_Id", SqlDbType.NVarChar).Value = response_code;
                        cmd.Parameters.Add("@transaction_status", SqlDbType.NVarChar).Value = responseMessage;
                        cmd.Parameters.Add("@transaction_amount", SqlDbType.Decimal).Value = Convert.ToDecimal(amount);
                        cmd.Parameters.Add("@transaction_Date", SqlDbType.DateTime).Value = payment_datetime;
                        cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "INSERT_PAYMENT_TRANSACTION";
                        conn.Open();
                        List<OnlinePayment> data = new List<OnlinePayment>();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        DataTable Dt = ds.Tables[0];
                        conn.Close();
                        LogToFile(logFilePath, "Stored procedure executed successfully.");
                        data = commonCode.ConvertDataTable<OnlinePayment>(Dt);

                        DataTable dt2 = getMailId2(data[0].org_Id, data[0].academic_Id, data[0].student_Id, "GETMAIL4");

                        string email2 = dt2.Rows[0]["emailId"].ToString();
                        string orgName = dt2.Rows[0]["orgName"].ToString();
                        string leadName = dt2.Rows[0]["childName"].ToString();
                        int classId = Convert.ToInt32(dt2.Rows[0]["class_Id"]);
                        string className = dt2.Rows[0]["class_Name"].ToString();
                        string applicationNumber = dt2.Rows[0]["application_Form_No"].ToString();
                        DateTime today = DateTime.Today;
                        TimeSpan currentTime = DateTime.Now.TimeOfDay;
                        LogToFile(logFilePath, $"Received data: email={email2}, orgName={orgName}, leadName={leadName}, classId={classId}, className={className}, applicationNumber={applicationNumber}");


                        //newlly
                        List<CrudFeeReceiptModel> det1 = StudentTransactions(org_Id, transac, student_Id);


                        // det[0].reference_Code = values[2].ToString();
                        if (transaction_id != "" && response_code == "0")
                        {
                            foreach (var v in det1)
                            {




                                LogToFile(logFilePath, $"thingsdet item: " +
            $"org_Id={v.org_Id}, " +
            $"transactionId={v.transactionId}, " +
            $"academic_Id={v.academic_Id}, " +
            $"academic_Year={v.academic_Year}, " +
            $"class_Name={v.class_Name}, " +
            $"NewOrOld={v.NewOrOld}, " +
            $"quota_id={v.quota_id}, " +
            $"class_Id={v.class_Id}, " +
            $"section_Id={v.section_Id}, " +
            $"section_Name={v.section_Name}, " +
            $"category_Id={v.category_Id}, " +
            $"subCategory_Id={v.subCategory_Id}, " +
            $"install1={v.install1}, " +
            $"install2={v.install2}, " +
            $"install3={v.install3}, " +
            $"install4={v.install4}, " +
            $"duration_Id={v.duration_Id}, " +
            $"type_Id={v.type_Id}, " +
            $"structure_Id={v.structure_Id}, " +
            $"term={v.term}, " +
            $"student_Id={v.student_Id}, " +
            $"receipt_Id={v.receipt_Id}, " +
            $"month_Id={v.month_Id}, " +
            $"install_Id={v.install_Id}, " +
            $"transId={v.transId}, " +
            $"category_Name={v.category_Name}, " +
            $"StudentName={v.StudentName}, " +
            $"subCategory_Name={v.subCategory_Name}, " +
            $"duration_Name={v.duration_Name}, " +
            $"type_Name={v.type_Name}, " +
            $"student_Code={v.student_Code}, " +
            $"receipt_Code={v.receipt_Code}, " +
            $"receipt_Mode={v.receipt_Mode}, " +
            $"cheque_Number={v.cheque_Number}, " +
            $"cheque_Date={v.cheque_Date}, " +
            $"due_Date={v.due_Date}, " +
            $"reference_Code={v.reference_Code}, " +
            $"payment_Date={v.payment_Date}, " +
            $"bank_Name={v.bank_Name}, " +
            $"branch_Name={v.branch_Name}, " +
            $"structure_Amount={v.structure_Amount}, " +
            $"advance_Amount={v.advance_Amount}, " +
            $"credit_Amount={v.credit_Amount}, " +
            $"discount_Amount={v.discount_Amount}, " +
            $"changedDiscountAmount={v.changedDiscountAmount}, " +
            $"receipt_Amount={v.receipt_Amount}, " +
            $"balance_Amount={v.balance_Amount}, " +
            $"payable_Amount={v.payable_Amount}, " +
            $"receipt_Date={v.receipt_Date}, " +
            $"bal_CreditAmount={v.bal_CreditAmount}, " +
            $"receipt_Remark={v.receipt_Remark}, " +
            $"fine_Amount={v.fine_Amount}, " +
            $"additional_Charge={v.additional_Charge}, " +
            $"receipt_Cancel={v.receipt_Cancel}, " +
            $"cancel_Date={v.cancel_Date}, " +
            $"status={v.status}, " +
            $"user_Name={v.user_Name}, " +
            $"ip_Address={v.ip_Address}, " +
            $"mode={v.mode}, " +
            $"dd_Number={v.dd_Number}, " +
            $"dd_Date={v.dd_Date}, " +
            $"admission_No={v.admission_No}, " +
            $"father_Name={v.father_Name}, " +
            $"amount={v.amount}, " +
            $"Payment_Status={v.Payment_Status}, " +
            $"mobileNumber={v.mobileNumber}, " +
            $"zipCode={v.zipCode}, " +
            $"URLAuth1={v.URLAuth1}, " +
            $"description={v.description}, " +
            $"email={v.email}, " +
            $"city={v.city}");


                                if (v.student_Id == data[0].student_Id)//&& tids.Contains(v.transactionId.ToString()))
                                {
                                    v.Payment_Status = "success";
                                    v.order_id = transaction_id;
                                    v.trans_id = transac;
                                    LogToFile(logFilePath, "det Payment success.");
                                }
                            }
                            FeeEntrypre(det1, Convert.ToString(order_id), data[0].student_Id);
                            LogToFile(logFilePath, $"Received data(FeeEntrypre): org_Id={data[0].org_Id}, academic_Id={data[0].academic_Id}, order_id={Convert.ToString(order_id)}, student_Id={data[0].student_Id}");
                            UpdatePreAdmissionAdminStatus(data[0].org_Id, data[0].student_Id, data[0].academic_Id);
                            LogToFile(logFilePath, $"Received data(UpdatePreAdmissionAdminStatus): org_Id={data[0].org_Id}, academic_Id={data[0].academic_Id}, student_Id={data[0].student_Id}");

                            SendSeatBookedStatusEmail(email2, data[0].org_Id, today, currentTime, orgName, leadName, 0, classId, className, applicationNumber);
                            LogToFile(logFilePath, $"Received data(SendSeatBookedStatusEmail): org_Id={data[0].org_Id}, academic_Id={data[0].academic_Id}, applicationNumber={applicationNumber}");

                            // status = 1;// success payment
                        }
                        else
                        {

                            for (int i = det1.Count - 1; i >= 0; i--)
                            {
                                if (det1[i].student_Id == data[0].student_Id)
                                {
                                    det1.RemoveAt(i);
                                }
                            }
                            LogToFile(logFilePath, "failure payment.");
                            //status = 0; // failure payment
                        }
                    }
                    else
                    {
                        SqlConnection conn = new SqlConnection(commonCode.conStr);
                        SqlCommand cmd = new SqlCommand("Pro_2021_onlinePayment", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@msg", SqlDbType.NVarChar).Value = chesksumValue;
                        cmd.Parameters.Add("@transaction_Id", SqlDbType.NVarChar).Value = order_id; //created in sql
                        cmd.Parameters.Add("@payment_trans_Id", SqlDbType.NVarChar).Value = transaction_id;
                        cmd.Parameters.Add("@payment_Vendor", SqlDbType.NVarChar).Value = "RAZORPAY1";
                        cmd.Parameters.Add("@merchant_id", SqlDbType.NVarChar).Value = order_id;
                        cmd.Parameters.Add("@currency_Code", SqlDbType.NVarChar).Value = currency;
                        cmd.Parameters.Add("@payment_status_Id", SqlDbType.NVarChar).Value = response_code;
                        cmd.Parameters.Add("@transaction_status", SqlDbType.NVarChar).Value = responseMessage;
                        cmd.Parameters.Add("@transaction_amount", SqlDbType.Decimal).Value = Convert.ToDecimal(amount);
                        cmd.Parameters.Add("@transaction_Date", SqlDbType.DateTime).Value = payment_datetime;
                        cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "INSERT_PAYMENT_TRANSACTION";
                        conn.Open();
                        List<OnlinePayment> data = new List<OnlinePayment>();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        DataTable Dt = ds.Tables[0];
                        conn.Close();
                        LogToFile(logFilePath, "transId is not Contain in tids - contact developer.");
                        //data = commonCode.ConvertDataTable<OnlinePayment>(Dt);
                        status = 3; // No matched transaction and failure payment 
                    }

                }

                var response = new HttpResponseMessage();

                string body = @"<body>
<table>
    <tr><td>Amount</td><td> @@amount@@</td></tr>
    <tr><td>Trans Id</td><td> @@TransId@@</td></tr>
    <tr><td>Trans Status</td><td> @@Status@@</td></tr>
    <tr><td>You will be automatically redirected to the website in 10 seconds, if not kindly click </td><td><a href='@@redirectUrl@@'>Back to Website</a></td></tr>
</table>
</body>
<script>
setTimeout(function(){ 
    window.location.href='@@redirectUrl@@'
}, 10000);
</script>";

                // Determine the redirect URL based on org_Id
                string redirectUrl = org_Id == 213
                    ? "https://applyonline.lodhaoakwoodschool.com/dashboard"
                    : "https://applyonline.lodhaworldschool.com/dashboard";

                // Replace placeholders in the email body
                body = body.Replace("@@amount@@", Convert.ToDecimal(amount).ToString());
                body = body.Replace("@@TransId@@", Convert.ToString(transaction_id));
                body = body.Replace("@@Status@@", responseMessage);
                body = body.Replace("@@redirectUrl@@", redirectUrl);

                return Content(body, "text/html");
            }
            catch (Exception ex)
            {
                LogToFile(logFilePath, $"Error occurred: {ex.Message} | StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getTransId")]
        private int getTransId(int orgId, int academicId, int studentId)
        {
            int transId = 0;
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Pro_2021_onlinePayment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = orgId;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academicId;
            cmd.Parameters.Add("@student_Id", SqlDbType.Int).Value = studentId;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "GETTRANSID";

            transId = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return (int)transId;
        }

        [HttpGet("getMailId2")]
        public DataTable getMailId2(int OrgId, int AcademicId, int StudentId, string mode)
        {

            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = OrgId;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = AcademicId;
            cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = StudentId;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;


            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            conn.Close();
            return Dt;
        }

        [HttpGet("StudentTransactions")]
        public List<CrudFeeReceiptModel> StudentTransactions(int org_Id, int transactionId, int student_Id)
        {
            try
            {
                SqlConnection con = new SqlConnection(commonCode.conStr);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_ManageStudentTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = org_Id;
                cmd.Parameters.Add("@transactionId", SqlDbType.Int).Value = transactionId;
                cmd.Parameters.Add("@student_Id", SqlDbType.Int).Value = student_Id;
                cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = "GET";
                List<CrudFeeReceiptModel> getdetail = new List<CrudFeeReceiptModel>();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataTable Dt = ds.Tables[0];
                con.Close();
                getdetail = commonCode.ConvertDataTable<CrudFeeReceiptModel>(Dt);
                return getdetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [NonAction]
        public string FeeEntrypre(List<CrudFeeReceiptModel> feeArray, string refCode, int studentId)
        {
            FeeCommonController feco = new FeeCommonController();

            decimal totalPayableAmt = 0;
            FeeCommonController f = new FeeCommonController();
            int receiptCode = 0;
            List<getFeeReceiptCodeModel> receipt = feco.getFeeReceiptCode(feeArray[0].org_Id, "RECEIPT_NO");
            receiptCode = receipt[0].receipt_Code;
            foreach (var n in feeArray)
            {
                if ((n.student_Id == studentId) && (n.Payment_Status == "success"))
                {
                    var t = n;

                    if (t.org_Id != null && t.student_Id == studentId)
                    {
                        if (t.payable_Amount > 0)
                        {
                            CrudFeeDiscountModel s = new CrudFeeDiscountModel();
                            s.org_Id = t.org_Id;
                            s.academic_Id = t.academic_Id;
                            s.category_Id = t.category_Id;
                            s.subCategory_Id = t.subCategory_Id;
                            s.duration_Id = t.duration_Id;
                            s.type_Id = t.type_Id;
                            s.structure_Id = t.structure_Id;
                            s.student_Id = t.student_Id;
                            s.discount_Id = 0;
                            s.discount_Type = 1;
                            s.discount_Amount = t.changedDiscountAmount;
                            s.discount_Date = DateTime.Now;
                            s.discount_Reason = "Single payment";
                            s.structure_Amount = t.structure_Amount;
                            s.receipt = receiptCode;
                            s.status = 1;
                            s.month_Id = t.month_Id;
                            s.user_Name = t.user_Name != null ? t.user_Name : "";
                            s.ip_Address = t.ip_Address != null ? t.ip_Address : "";
                            s.mode = t.duration_Id > 2 ? "CREATE" : "CREATEMONTHLY";
                            var a = f.crudFeeDiscountMaster(s);
                            CrudFeeReceiptModel r = new CrudFeeReceiptModel();
                            r.org_Id = t.org_Id;
                            r.academic_Id = t.academic_Id;
                            r.class_Id = t.class_Id;
                            r.quota_id = t.quota_id;
                            r.NewOrOld = t.NewOrOld;
                            r.duration_Id = t.duration_Id;
                            r.type_Id = t.type_Id;
                            r.structure_Id = t.structure_Id;
                            r.student_Id = t.student_Id;
                            r.receipt_Id = t.receipt_Id;
                            r.student_Code = t.student_Code;
                            r.receipt_Code = receiptCode.ToString();
                            r.receipt_Mode = "3";
                            r.cheque_Number = t.cheque_Number != null ? t.cheque_Number : "";
                            r.cheque_Date = t.cheque_Date != DateTime.MinValue ? t.cheque_Date : DateTime.Now;
                            r.dd_Number = t.dd_Number != null ? t.dd_Number : "";
                            r.dd_Date = t.dd_Date != null ? t.dd_Date : DateTime.Now;
                            r.reference_Code = refCode != null ? refCode : "";
                            r.payment_Date = t.payment_Date != DateTime.MinValue ? t.payment_Date : DateTime.Now;
                            r.bank_Name = t.bank_Name != null ? t.bank_Name : "";
                            r.branch_Name = t.branch_Name != null ? t.branch_Name : "";
                            r.month_Id = t.month_Id;
                            r.payable_Amount = t.payable_Amount;
                            r.bal_CreditAmount = t.bal_CreditAmount;
                            r.balance_Amount = t.balance_Amount;
                            r.structure_Amount = t.structure_Amount;
                            r.discount_Amount = t.discount_Amount;
                            r.receipt_Amount = t.payable_Amount;
                            r.receipt_Date = DateTime.Now;
                            r.receipt_Remark = t.receipt_Remark != null ? t.receipt_Remark : "";
                            r.fine_Amount = t.fine_Amount;
                            r.additional_Charge = t.additional_Charge;
                            r.receipt_Cancel = t.receipt_Cancel;
                            r.cancel_Date = t.cancel_Date != DateTime.MinValue ? t.cancel_Date : DateTime.Now;
                            r.status = 1;
                            r.user_Name = t.user_Name != null ? t.user_Name : "";
                            r.ip_Address = t.ip_Address != null ? t.ip_Address : "";
                            r.mode = t.month_Id == 0 ? "CREATE" : "MONTHLYCREATE";
                            r.trans_id = t.trans_id;
                            r.order_id = t.order_id;
                            var b = f.crudMonthlyFeeReceiptMaster(r);
                            totalPayableAmt = totalPayableAmt + t.receipt_Amount;

                            //new code
                            crudAccountsPostingModel1 ap2 = new crudAccountsPostingModel1();
                            ap2.org_Id = feeArray[0].org_Id;
                            ap2.receipt_Date = DateTime.Now;
                            ap2.transCode = 1;
                            ap2.transType = "Receipt";
                            ap2.accountsCode = 9;
                            ap2.credit_Amount = totalPayableAmt;//feeArray[0].amount;
                            ap2.debit_Amount = 0;
                            ap2.typeId = t.type_Id;
                            ap2.academicId = t.academic_Id;
                            ap2.directOrIndirect = 1;

                            ap2.receipt_Code = receiptCode.ToString();
                            ap2.student_Id = feeArray[0].student_Id;
                            ap2.structure_Id = t.structure_Id;  //Tamil
                            ap2.class_Id = t.class_Id;  //Tamil
                            var acc2 = f.crudAccountsPostingNew1(ap2);
                        }
                    }

                }
            }

            CreditNoteController c = new CreditNoteController();
            List<CreditNote> creditAmt = c.getStudentDetail(feeArray[0].org_Id, feeArray[0].student_Id, "FEECOLLECTIONCREDITNOTE");
            decimal creditAmount = creditAmt[0].advance_Amount;
            decimal balCreditAmount = 0;
            if (creditAmount > 0)
            {
                if (creditAmount < totalPayableAmt)
                {
                    balCreditAmount = 0;
                }
                else if (creditAmount > totalPayableAmt)
                {
                    balCreditAmount = (creditAmount - totalPayableAmt);
                }
                CrudFeeReceiptModel p = new CrudFeeReceiptModel();
                p.org_Id = feeArray[0].org_Id;
                p.student_Id = feeArray[0].student_Id;
                p.bal_CreditAmount = balCreditAmount;
                p.mode = "UPDATECREDITAMOUNT";
                var b = f.updateCreditNote(p);
            }
            crudAccountsPostingModel ap = new crudAccountsPostingModel();
            ap.org_Id = feeArray[0].org_Id;
            ap.receipt_Date = DateTime.Now;
            ap.transCode = 1;
            ap.transType = "Receipt";
            ap.accountsCode = 9;
            ap.credit_Amount = feeArray[0].amount;
            ap.debit_Amount = 0;
            ap.receipt_Code = receiptCode.ToString();
            ap.student_Id = feeArray[0].student_Id;
            var acc = f.crudAccountsPosting(ap);
            for (int i = feeArray.Count - 1; i >= 0; i--)
            {
                if (feeArray[i].student_Id == studentId)
                {
                    feeArray.RemoveAt(i);
                }
            }
            return 0.ToString();
        }

        [HttpPost]
        public string UpdatePreAdmissionAdminStatus(int org_Id, int student_Id, int academic_Id)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("pre_Admission_Pro", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = org_Id;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academic_Id;
            cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = student_Id;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "INSERTAPPADMOFFACTION2";

            conn.Open();
            string n = cmd.ExecuteNonQuery().ToString();
            conn.Close();
            return n;
        }
        //ravi end

        ///end
        [NonAction]
        private int SendSeatBookedStatusEmail(string email, int org_Id, DateTime visitDate, TimeSpan visitTime, string orgName, string leadName, int status_Id, int classId, string className, string applicationNumber)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                //userData.StatusId == 14 || userData.StatusId == 13 || userData.StatusId == 9 || userData.StatusId == 12 || userData.StatusId == 3

                {
                    string mail = "", code = "";

                    if (org_Id == 210) { mail = "admission_lsg@lodhaworldschool.com"; code = "xnyfofrvrrrhmakf"; }
                    else if (org_Id == 211) { mail = "admission_thane@lodhaworldschool.com"; code = "qbvsvbsyqbdtnkgr"; }
                    else if (org_Id == 212) { mail = "admission_palava@lodhaworldschool.com"; code = "bqhlpnprmfojxiob"; }
                    else if (org_Id == 214) { mail = "admission_taloja@lodhaworldschool.com"; code =  "demf uobr xuxi iufs"; }
                    else if (org_Id == 213) { mail = "admissions@lodhaoakwoodschool.com"; code = "ytjnwukgnjsjpfiq"; }
                    else if (org_Id == 223) { mail = "admission_premier@lodhaworldschool.com"; code = "bcsciuavccudfmbl"; }



                    smtpClient.UseDefaultCredentials = false;
                    //smtpClient.Credentials = new NetworkCredential("enquiryevalai@gmail.com", "vgvj vqpj inov ntbo");
                    smtpClient.Credentials = new NetworkCredential(mail, code);
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;

                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
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

                    if (org_Id != 213)
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
                    if (org_Id == 213)
                    {
                        mailMessage.Subject = $"Seat Confirmed – Welcome to Lodha Oakwood School!";
                        body += $@"
<p>We are delighted to welcome you and your child to Lodha Oakwood School!</p>
<p>At Lodha Oakwood School, we believe that every child is born with unique abilities and aim to maximize that potential, whether in academics or beyond. Here, your child will experience an education that fosters collaboration, creativity, experimentation, and innovation – without comparison or limitation.</p>
<p>As part of our mission to deliver the highest level of academic excellence, we are committed to nurturing global citizens who are passionate about bettering the world around them. Join us in this exciting journey as we prepare our young learners to take flight, soaring to new heights with curiosity, ambition, and joy in their hearts.</p>
<p>We value your feedback! To help us continue improving our admissions process and overall experience, kindly take a moment to share your thoughts by clicking this link - <a href=""https://forms.gle/NeuzLYbLMbRwRfkcA""</a>.</p>
<p>We will be sharing your child’s unique ID, email, and other details shortly. Information on procuring books and uniforms will also be provided closer to the start of the school year.</p>
<p>We look forward to embarking on this journey toward excellence together!</p>
<p>Warm Regards,<br><br>Admissions Team<br>Lodha Oakwood School</p>";
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
                // Log the error details
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return 0; // Email sending failed
            }
        }

    }
}
