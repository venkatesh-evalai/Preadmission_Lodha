using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Preadmission_Lodha.Models;
using System.Data;
using System.Text;

namespace Preadmission_Lodha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeCommonController : ControllerBase
    {
        [HttpPost("crudFeeDiscountMaster")]
        public bool crudFeeDiscountMaster(CrudFeeDiscountModel feeDisc)
        {
            DateTime dt = DateTime.Now;
            if (Convert.ToDateTime(feeDisc.discount_Date) != DateTime.MinValue)
            {
                dt = Convert.ToDateTime(feeDisc.discount_Date);
            }

            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("2021_Pro_CRUD_FeeDiscountMaster", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@orgId", SqlDbType.Int).Value = feeDisc.org_Id;
            cmd.Parameters.Add("@academicId", SqlDbType.Int).Value = feeDisc.academic_Id;
            //new code start
            cmd.Parameters.Add("@categoryId", SqlDbType.Int).Value = feeDisc.category_Id;
            cmd.Parameters.Add("@subCategoryId", SqlDbType.Int).Value = feeDisc.subCategory_Id;
            //new code end
            cmd.Parameters.Add("@class_Id", SqlDbType.Int).Value = feeDisc.class_Id;
            cmd.Parameters.Add("@NewOrOld", SqlDbType.Int).Value = feeDisc.NewOrOld;
            cmd.Parameters.Add("@quota_id", SqlDbType.Int).Value = feeDisc.quota_id;
            cmd.Parameters.Add("@durationId", SqlDbType.Int).Value = feeDisc.duration_Id;
            cmd.Parameters.Add("@typeId", SqlDbType.Int).Value = feeDisc.type_Id;
            cmd.Parameters.Add("@structureId", SqlDbType.Int).Value = feeDisc.structure_Id;
            cmd.Parameters.Add("@studentId", SqlDbType.Int).Value = feeDisc.student_Id;
            cmd.Parameters.Add("@discountId", SqlDbType.Int).Value = feeDisc.discount_Id;
            cmd.Parameters.Add("@discountType", SqlDbType.Int).Value = feeDisc.discount_Type;
            cmd.Parameters.Add("@discountAmount", SqlDbType.Decimal).Value = feeDisc.discount_Amount;
            cmd.Parameters.Add("@discountDate", SqlDbType.DateTime).Value = dt;
            cmd.Parameters.Add("@discountReason", SqlDbType.NVarChar).Value = feeDisc.discount_Reason;
            cmd.Parameters.Add("@structure_Amount", SqlDbType.NVarChar).Value = feeDisc.structure_Amount;
            cmd.Parameters.Add("@receipt", SqlDbType.Int).Value = feeDisc.receipt != null ? feeDisc.receipt : 0;
            cmd.Parameters.Add("@status", SqlDbType.Int).Value = feeDisc.status;
            cmd.Parameters.Add("@month_Id", SqlDbType.Int).Value = feeDisc.month_Id;
            cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = feeDisc.user_Name;
            cmd.Parameters.Add("@ipAddress", SqlDbType.NVarChar).Value = feeDisc.ip_Address;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = feeDisc.mode;
            conn.Open();
            int n = (int)cmd.ExecuteNonQuery();
            conn.Close();
            if (n > 0) return true;
            else return false;
        }

        [HttpGet("getFeeReceiptCode")]
        public List<getFeeReceiptCodeModel> getFeeReceiptCode(int orgId, string mode)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("2021_Pro_CRUD_FeeReceiptMaster", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@orgId", SqlDbType.Int).Value = orgId;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;
            List<getFeeReceiptCodeModel> FeeReceiptCode = new List<getFeeReceiptCodeModel>();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("receipt_Code", typeof(Int32));
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["receipt_Code"] = Convert.ToInt32(dr["receipt_Code"]);
                dt.Rows.Add(drow);
            }
            conn.Close();
            FeeReceiptCode = commonCode.ConvertDataTable<getFeeReceiptCodeModel>(dt);
            return FeeReceiptCode;
        }

        [HttpPost]
        public bool crudMonthlyFeeReceiptMaster(CrudFeeReceiptModel feeRcpt)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("2021_Pro_CRUD_FeeReceiptMaster", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 150;
            cmd.Parameters.Add("@orgId", SqlDbType.Int).Value = feeRcpt.org_Id;
            cmd.Parameters.Add("@academicId", SqlDbType.Int).Value = feeRcpt.academic_Id;
            cmd.Parameters.Add("@categoryId", SqlDbType.Int).Value = feeRcpt.category_Id;
            cmd.Parameters.Add("@subCategoryId", SqlDbType.Int).Value = feeRcpt.subCategory_Id;
            cmd.Parameters.Add("@class_Id", SqlDbType.Int).Value = feeRcpt.class_Id;
            cmd.Parameters.Add("@NewOrOld", SqlDbType.Int).Value = feeRcpt.NewOrOld;
            cmd.Parameters.Add("@quota_id", SqlDbType.Int).Value = feeRcpt.quota_id;
            cmd.Parameters.Add("@durationId", SqlDbType.Int).Value = feeRcpt.duration_Id;
            cmd.Parameters.Add("@typeId", SqlDbType.Int).Value = feeRcpt.type_Id;
            cmd.Parameters.Add("@structureId", SqlDbType.Int).Value = feeRcpt.structure_Id;
            cmd.Parameters.Add("@studentId", SqlDbType.Int).Value = feeRcpt.student_Id;
            cmd.Parameters.Add("@receiptId", SqlDbType.Int).Value = feeRcpt.receipt_Id;
            cmd.Parameters.Add("@studentCode", SqlDbType.NVarChar).Value = feeRcpt.student_Code;
            cmd.Parameters.Add("@receiptCode", SqlDbType.NVarChar).Value = feeRcpt.receipt_Code;
            cmd.Parameters.Add("@receiptMode", SqlDbType.Int).Value = feeRcpt.receipt_Mode;
            cmd.Parameters.Add("@chequeNumber", SqlDbType.NVarChar).Value = feeRcpt.cheque_Number;
            if (feeRcpt.cheque_Date != DateTime.MinValue) { cmd.Parameters.Add("@chequeDate", SqlDbType.DateTime).Value = feeRcpt.cheque_Date; }
            cmd.Parameters.Add("@ddNumber", SqlDbType.NVarChar).Value = feeRcpt.dd_Number;
            if (feeRcpt.dd_Date != DateTime.MinValue) { cmd.Parameters.Add("@ddDate", SqlDbType.DateTime).Value = feeRcpt.dd_Date; }
            cmd.Parameters.Add("@referenceCode", SqlDbType.NVarChar).Value = feeRcpt.reference_Code;
            if (feeRcpt.payment_Date != DateTime.MinValue) { cmd.Parameters.Add("@paymentDate", SqlDbType.DateTime).Value = feeRcpt.payment_Date; }
            cmd.Parameters.Add("@bankName", SqlDbType.NVarChar).Value = feeRcpt.bank_Name;
            cmd.Parameters.Add("@branchName", SqlDbType.NVarChar).Value = feeRcpt.branch_Name;
            cmd.Parameters.Add("@monthId", SqlDbType.Int).Value = feeRcpt.month_Id;
            cmd.Parameters.Add("@receiptAmount", SqlDbType.NVarChar).Value = feeRcpt.payable_Amount;
            cmd.Parameters.Add("@bal_CreditAmount", SqlDbType.Decimal).Value = feeRcpt.bal_CreditAmount;
            cmd.Parameters.Add("@balanceAmount", SqlDbType.Decimal).Value = feeRcpt.balance_Amount;
            cmd.Parameters.Add("@structureAmount", SqlDbType.Decimal).Value = feeRcpt.structure_Amount;
            cmd.Parameters.Add("@discountAmount", SqlDbType.Decimal).Value = feeRcpt.discount_Amount;
            cmd.Parameters.Add("@paidAmount", SqlDbType.Decimal).Value = feeRcpt.receipt_Amount;

            cmd.Parameters.Add("@receiptDate", SqlDbType.DateTime).Value = feeRcpt.receipt_Date == DateTime.MinValue ? DateTime.Today : feeRcpt.receipt_Date;

            cmd.Parameters.Add("@receiptRemark", SqlDbType.NVarChar).Value = feeRcpt.receipt_Remark;
            cmd.Parameters.Add("@fineAmount", SqlDbType.Decimal).Value = feeRcpt.fine_Amount;
            cmd.Parameters.Add("@additionalCharge", SqlDbType.Decimal).Value = feeRcpt.additional_Charge;
            cmd.Parameters.Add("@receiptCancel", SqlDbType.Int).Value = feeRcpt.receipt_Cancel;
            if (feeRcpt.cancel_Date != DateTime.MinValue) { cmd.Parameters.Add("@cancelDate", SqlDbType.DateTime).Value = feeRcpt.cancel_Date; }
            cmd.Parameters.Add("@status", SqlDbType.Int).Value = feeRcpt.status;
            cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = feeRcpt.user_Name;
            cmd.Parameters.Add("@trans_id", SqlDbType.Int).Value = feeRcpt.trans_id;
            cmd.Parameters.Add("@order_id", SqlDbType.NVarChar).Value = feeRcpt.order_id;
            cmd.Parameters.Add("@ipAddress", SqlDbType.NVarChar).Value = feeRcpt.ip_Address;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = feeRcpt.mode;
            conn.Open();
            int n = (int)cmd.ExecuteNonQuery();

            LogTextFile(feeRcpt.org_Id, feeRcpt.org_Id, feeRcpt.academic_Id, feeRcpt.category_Id, feeRcpt.subCategory_Id, feeRcpt.duration_Id, feeRcpt.type_Id, feeRcpt.structure_Id,
                       feeRcpt.student_Id, feeRcpt.receipt_Id, feeRcpt.student_Code, feeRcpt.receipt_Code, feeRcpt.receipt_Mode, feeRcpt.cheque_Number, feeRcpt.cheque_Date, feeRcpt.dd_Date,
                       feeRcpt.payment_Date, feeRcpt.bank_Name, feeRcpt.branch_Name, feeRcpt.month_Id, feeRcpt.payable_Amount, feeRcpt.bal_CreditAmount, feeRcpt.balance_Amount,
                       feeRcpt.structure_Amount, feeRcpt.discount_Amount, feeRcpt.receipt_Amount, feeRcpt.receipt_Date, feeRcpt.receipt_Remark, feeRcpt.fine_Amount, feeRcpt.additional_Charge,
                       feeRcpt.receipt_Cancel, feeRcpt.cancel_Date, feeRcpt.status??0, feeRcpt.user_Name, feeRcpt.ip_Address, feeRcpt.mode, "crudMonthlyFeeReceiptMaster", "Pro_CRUD_FeeReceiptMaster");

            conn.Close();
            if (n > 0) return true;
            else return false;
        }


        [HttpPost]
        public void LogTextFile(int orgId, int org_Id, int academic_Id, int category_Id, int subCategory_Id, int duration_Id, int type_Id, int structure_Id, int student_Id, int receipt_Id, string student_Code,
           string receipt_Code, string receipt_Mode, string cheque_Number, DateTime cheque_Date, DateTime dd_Date, DateTime payment_Date, string bank_Name, string branch_Name,
           int month_Id, decimal payable_Amount, decimal bal_CreditAmount, decimal balance_Amount, decimal structure_Amount, decimal discount_Amount, decimal receipt_Amount,
           DateTime receipt_Date, string receipt_Remark, decimal fine_Amount, decimal additional_Charge, int receipt_Cancel, DateTime cancel_Date, int status, string user_Name,
           string ip_Address, string mode, string Api, string sp)
        {

            var testValue = 0;

            DateTime now = DateTime.Now;
            var d = now.Day;
            var m = now.Month;
            var y = now.Year;

            var HH = now.Hour;
            var MM = now.Minute;
            var SS = now.Second;

            var fileN = orgId + "_" + y + m + d + ".txt";
            // string fileName = @"D:\Temp\" + fileN;
            string fileName = @"C:\websites\valaischool.com\Excel\LogFile\" + fileN;
            FileInfo fi = new FileInfo(fileName);

            try
            {

                // Check if file already exists. If yes, delete it.     
                if (System.IO.File.Exists(fileName))
                {
                    string appendText = "\n" + "=================================================" + Environment.NewLine;
                    System.IO.File.AppendAllText(fileName, appendText);

                    string API = "\n" + "API Name=" + Api;
                    System.IO.File.AppendAllText(fileName, API);

                    string SP = "\n" + "SP Name=" + sp;
                    System.IO.File.AppendAllText(fileName, SP);


                    string orgID = Convert.ToString(org_Id);
                    string Orgid = "\n" + "org_Id=" + orgID;
                    System.IO.File.AppendAllText(fileName, Orgid);


                    string academicId = Convert.ToString(academic_Id);
                    string acadId = "," + "academic_Id=" + academicId;
                    System.IO.File.AppendAllText(fileName, acadId);

                    string categoryId = Convert.ToString(category_Id);
                    string categoryID = "," + "category_Id=" + categoryId;
                    System.IO.File.AppendAllText(fileName, categoryID);


                    string subCategoryId = Convert.ToString(subCategory_Id);
                    string subCategoryID = "," + "subCategory_Id=" + subCategoryId;
                    System.IO.File.AppendAllText(fileName, subCategoryID);


                    string durationId = Convert.ToString(duration_Id);
                    string durationID = "," + "duration_Id=" + durationId;
                    System.IO.File.AppendAllText(fileName, durationID);


                    string typeId = Convert.ToString(type_Id);
                    string typeID = "," + "type_Id=" + typeId;
                    System.IO.File.AppendAllText(fileName, typeID);

                    string structureId = Convert.ToString(structure_Id);
                    string structureID = "," + "structure_Id=" + structureId;
                    System.IO.File.AppendAllText(fileName, categoryID);

                    string studentId = Convert.ToString(student_Id);
                    string studentID = "," + "student_Id=" + studentId;
                    System.IO.File.AppendAllText(fileName, studentID);


                    string receiptId = Convert.ToString(receipt_Id);
                    string receiptID = "," + "receipt_Id=" + receiptId;
                    System.IO.File.AppendAllText(fileName, receiptID);

                    string studentCode = "," + "student_Code=" + student_Code;
                    System.IO.File.AppendAllText(fileName, studentCode);

                    string receiptCode = "," + "receipt_Code=" + receipt_Code;
                    System.IO.File.AppendAllText(fileName, receiptCode);



                    string receiptMode = Convert.ToString(receipt_Mode);
                    string receiptmode = "," + "receipt_Mode=" + receiptMode;
                    System.IO.File.AppendAllText(fileName, receiptmode);

                    string chequeNumber = "," + "cheque_Number=" + cheque_Number;
                    System.IO.File.AppendAllText(fileName, chequeNumber);

                    string chequeDate = Convert.ToString(cheque_Date);
                    string chequedate = "," + "cheque_Date=" + chequeDate;
                    System.IO.File.AppendAllText(fileName, chequedate);

                    string ddDate = Convert.ToString(dd_Date);
                    string dddate = "," + "dd_Date=" + ddDate;
                    System.IO.File.AppendAllText(fileName, dddate);

                    string paymentDate = Convert.ToString(payment_Date);
                    string paymentdate = "," + "payment_Date=" + paymentDate;
                    System.IO.File.AppendAllText(fileName, paymentdate);



                    string bankName = "," + "bank_Name=" + bank_Name;
                    System.IO.File.AppendAllText(fileName, bankName);


                    string branchName = "," + "branch_Name=" + branch_Name;
                    System.IO.File.AppendAllText(fileName, branchName);

                    string monthId = Convert.ToString(month_Id);
                    string monthID = "," + "month_Id=" + monthId;
                    System.IO.File.AppendAllText(fileName, monthID);

                    string payableAmount = Convert.ToString(payable_Amount);
                    string payableamount = "," + "payable_Amount=" + payableAmount;
                    System.IO.File.AppendAllText(fileName, payableamount);

                    string balCreditAmount = Convert.ToString(bal_CreditAmount);
                    string balcreditAmount = "," + "bal_CreditAmount=" + balCreditAmount;
                    System.IO.File.AppendAllText(fileName, balcreditAmount);

                    string balanceAmount = Convert.ToString(balance_Amount);
                    string balanceamount = "," + "balance_Amount=" + balanceAmount;
                    System.IO.File.AppendAllText(fileName, balanceamount);

                    string structureAmount = Convert.ToString(structure_Amount);
                    string structureamount = "," + "structure_Amount=" + structureAmount;
                    System.IO.File.AppendAllText(fileName, structureamount);

                    string discountAmount = Convert.ToString(discount_Amount);
                    string discountamount = "," + "discount_Amount=" + discountAmount;
                    System.IO.File.AppendAllText(fileName, discountamount);

                    string receiptAmount = Convert.ToString(receipt_Amount);
                    string receiptamount = "," + "receipt_Amount=" + receiptAmount;
                    System.IO.File.AppendAllText(fileName, receiptamount);

                    string receiptDate = Convert.ToString(receipt_Date);
                    string receiptdate = "\n" + "receipt_Date=" + receiptDate;
                    System.IO.File.AppendAllText(fileName, receiptdate);


                    string receiptRemark = "," + "receipt_Remark=" + receipt_Remark;
                    System.IO.File.AppendAllText(fileName, receiptRemark);

                    string fineAmount = Convert.ToString(fine_Amount);
                    string fineamount = "," + "fine_Amount=" + fineAmount;
                    System.IO.File.AppendAllText(fileName, fineamount);

                    string additionalCharge = Convert.ToString(additional_Charge);
                    string additionalcharge = "," + "additional_Charge=" + additionalCharge;
                    System.IO.File.AppendAllText(fileName, additionalcharge);


                    string receiptCancel = Convert.ToString(receipt_Cancel);
                    string receiptcancel = "," + "receipt_Cancel=" + receiptCancel;
                    System.IO.File.AppendAllText(fileName, receiptcancel);


                    string cancelDate = Convert.ToString(cancel_Date);
                    string canceldate = "," + "cancel_Date=" + cancelDate;
                    System.IO.File.AppendAllText(fileName, canceldate);

                    string statu = Convert.ToString(status);
                    string tatus = "," + "status=" + statu;
                    System.IO.File.AppendAllText(fileName, tatus);


                    string userName = "," + "user_Name=" + user_Name;
                    System.IO.File.AppendAllText(fileName, userName);

                    string ipAddress = "," + "ip_Address=" + ip_Address;
                    System.IO.File.AppendAllText(fileName, ipAddress);

                    string Mode = "," + "mode=" + mode;
                    System.IO.File.AppendAllText(fileName, Mode);

                }
                else
                {

                    // Create a new file     
                    using (FileStream fs = fi.Create())
                    {
                        Byte[] txt = new UTF8Encoding(true).GetBytes("Log File creation");
                        fs.Write(txt, 0, txt.Length);
                        testValue = 1;
                    }
                }
                // Write file contents on console.     
                using (StreamReader sr = System.IO.File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }

                if (testValue == 1)
                {
                    LogTextFile(orgId, org_Id, academic_Id, category_Id, subCategory_Id, duration_Id, type_Id, structure_Id, student_Id, receipt_Id, student_Code,
                     receipt_Code, receipt_Mode, cheque_Number, cheque_Date, dd_Date, payment_Date, bank_Name, branch_Name,
                     month_Id, payable_Amount, bal_CreditAmount, balance_Amount, structure_Amount, discount_Amount, receipt_Amount,
                     receipt_Date, receipt_Remark, fine_Amount, additional_Charge, receipt_Cancel, cancel_Date, status, user_Name,
                     ip_Address, mode, Api, sp);
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }

        [HttpGet("getFeeReceiptDetails")]
        public List<CrudFeeReceiptModel> getFeeReceiptDetails(int orgId, int academicId, int studentId, int install1, int install2, int install3, int install4, string mode)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("2021_Pro_CRUD_FeeReceiptMaster", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@orgId", SqlDbType.Int).Value = orgId;
            cmd.Parameters.Add("@academicId", SqlDbType.Int).Value = academicId;
            cmd.Parameters.Add("@studentId", SqlDbType.Int).Value = studentId;
            cmd.Parameters.Add("@install1", SqlDbType.Int).Value = install1;
            cmd.Parameters.Add("@install2", SqlDbType.Int).Value = install2;
            cmd.Parameters.Add("@install3", SqlDbType.Int).Value = install3;
            cmd.Parameters.Add("@install4", SqlDbType.Int).Value = install4;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;
            List<CrudFeeReceiptModel> data = new List<CrudFeeReceiptModel>();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            conn.Close();
            data = commonCode.ConvertDataTable<CrudFeeReceiptModel>(Dt);
            return data;
        }

        [HttpPost("updateCreditNote")]
        public bool updateCreditNote(CrudFeeReceiptModel feeRcpt)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("2021_Pro_CRUD_FeeReceiptMaster", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@orgId", SqlDbType.Int).Value = feeRcpt.org_Id;
            cmd.Parameters.Add("@studentId", SqlDbType.Int).Value = feeRcpt.student_Id;
            cmd.Parameters.Add("@bal_CreditAmount", SqlDbType.Decimal).Value = feeRcpt.bal_CreditAmount;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = feeRcpt.mode;
            conn.Open();
            int n = (int)cmd.ExecuteNonQuery();
            conn.Close();
            if (n > 0) return true;
            else return false;
        }

        [HttpPost("crudAccountsPostingNew1")]
        public bool crudAccountsPostingNew1(crudAccountsPostingModel1 AccPo)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("Pro_CRUD_2024_AccountsPosting_Insertion", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@orgId", SqlDbType.Int).Value = AccPo.org_Id;
            if (AccPo.receipt_Date != DateTime.MinValue)
            {
                cmd.Parameters.Add("@transDate", SqlDbType.DateTime).Value = AccPo.receipt_Date;
            }
            cmd.Parameters.Add("@transCode", SqlDbType.Int).Value = AccPo.transCode;
            cmd.Parameters.Add("@tranType", SqlDbType.NVarChar).Value = AccPo.transType;
            cmd.Parameters.Add("@accountsCode", SqlDbType.Int).Value = AccPo.accountsCode;
            cmd.Parameters.Add("@creditAmount", SqlDbType.Decimal).Value = AccPo.credit_Amount;
            cmd.Parameters.Add("@debitAmount", SqlDbType.Decimal).Value = AccPo.debit_Amount;
            cmd.Parameters.Add("@paymentCode", SqlDbType.Int).Value = Convert.ToInt32(AccPo.receipt_Code);
            cmd.Parameters.Add("@customerCode", SqlDbType.Int).Value = AccPo.student_Id;
            cmd.Parameters.Add("@academicId", SqlDbType.Int).Value = AccPo.academicId;
            cmd.Parameters.Add("@typeId", SqlDbType.Int).Value = AccPo.typeId;//new
            cmd.Parameters.Add("@directOrIndirect", SqlDbType.Int).Value = AccPo.directOrIndirect;//new

            cmd.Parameters.Add("@structure_Id", SqlDbType.Int).Value = AccPo.structure_Id;//new by Tamil
            cmd.Parameters.Add("@class_Id", SqlDbType.Int).Value = AccPo.class_Id;//new by Tamil
            conn.Open();
            int n = (int)cmd.ExecuteNonQuery();
            conn.Close();
            if (n > 0) return true;
            else return false;
        }

        [HttpPost("crudAccountsPosting")]
        public bool crudAccountsPosting(crudAccountsPostingModel AccPo)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("Pro_CRUD_AccountsPosting_Insertion", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@orgId", SqlDbType.Int).Value = AccPo.org_Id;
            if (AccPo.receipt_Date != DateTime.MinValue)
            {
                cmd.Parameters.Add("@transDate", SqlDbType.DateTime).Value = AccPo.receipt_Date;
            }
            cmd.Parameters.Add("@transCode", SqlDbType.Int).Value = AccPo.transCode;
            cmd.Parameters.Add("@tranType", SqlDbType.NVarChar).Value = AccPo.transType;
            cmd.Parameters.Add("@accountsCode", SqlDbType.Int).Value = AccPo.accountsCode;
            cmd.Parameters.Add("@creditAmount", SqlDbType.NVarChar).Value = AccPo.credit_Amount;
            cmd.Parameters.Add("@debitAmount", SqlDbType.NVarChar).Value = AccPo.debit_Amount;
            cmd.Parameters.Add("@paymentCode", SqlDbType.NVarChar).Value = AccPo.receipt_Code;
            cmd.Parameters.Add("@customerCode", SqlDbType.Int).Value = AccPo.student_Id;
            conn.Open();
            int n = (int)cmd.ExecuteNonQuery();
            conn.Close();
            if (n > 0) return true;
            else return false;
        }

        [HttpPost("crudAccountsPostingNew")]
        public bool crudAccountsPostingNew(crudAccountsPostingModel AccPo)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            SqlCommand cmd = new SqlCommand("Pro_CRUD_2024_AccountsPosting_Insertion", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@orgId", SqlDbType.Int).Value = AccPo.org_Id;
            if (AccPo.receipt_Date != DateTime.MinValue)
            {
                cmd.Parameters.Add("@transDate", SqlDbType.DateTime).Value = AccPo.receipt_Date;
            }
            cmd.Parameters.Add("@transCode", SqlDbType.Int).Value = AccPo.transCode;
            cmd.Parameters.Add("@tranType", SqlDbType.NVarChar).Value = AccPo.transType;
            cmd.Parameters.Add("@accountsCode", SqlDbType.Int).Value = AccPo.accountsCode;
            cmd.Parameters.Add("@creditAmount", SqlDbType.Decimal).Value = AccPo.credit_Amount;
            cmd.Parameters.Add("@debitAmount", SqlDbType.Decimal).Value = AccPo.debit_Amount;
            cmd.Parameters.Add("@paymentCode", SqlDbType.Int).Value = Convert.ToInt32(AccPo.receipt_Code);
            cmd.Parameters.Add("@customerCode", SqlDbType.Int).Value = AccPo.student_Id;
            cmd.Parameters.Add("@academicId", SqlDbType.Int).Value = AccPo.academicId;
            cmd.Parameters.Add("@typeId", SqlDbType.Int).Value = AccPo.typeId;//new
            cmd.Parameters.Add("@directOrIndirect", SqlDbType.Int).Value = AccPo.directOrIndirect;//new
            conn.Open();
            int n = (int)cmd.ExecuteNonQuery();
            conn.Close();
            if (n > 0) return true;
            else return false;
        }
    }
}
