using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Preadmission_Lodha.Models;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Preadmission_Lodha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasteController : ControllerBase
    {
        [HttpGet("getCasteDetails")]
        public List<Caste> getCasteDetails(int org_Id, string mode)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("pro_CRUD_Caste_Master", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = org_Id;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;
            List<Caste> data = new List<Caste>();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            conn.Close();
            data = commonCode.ConvertDataTable<Caste>(Dt);
            return data;
        }

    }
}
