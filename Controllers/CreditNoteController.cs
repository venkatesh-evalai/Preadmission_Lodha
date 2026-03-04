using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Preadmission_Lodha.Models;
using System.Data;

namespace Preadmission_Lodha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditNoteController : ControllerBase
    {

        [HttpGet("getStudentDetail")]
        public List<CreditNote> getStudentDetail(int org_Id, int student_Id, string mode)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Pro_2021_CRUD_FeeCreditNote", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@orgId", SqlDbType.Int).Value = org_Id;
            cmd.Parameters.Add("@studentId ", SqlDbType.Int).Value = student_Id;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;
            List<CreditNote> data = new List<CreditNote>();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            conn.Close();
            data = commonCode.ConvertDataTable<CreditNote>(Dt);
            return data;

        }
    }
}
