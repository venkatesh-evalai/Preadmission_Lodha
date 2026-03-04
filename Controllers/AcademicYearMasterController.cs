using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Preadmission_Lodha.Models;
using System.Data;

namespace Preadmission_Lodha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearMasterController : ControllerBase
    {
        [HttpGet("getAcademicYearMaster")]
        public List<AcademicYear> getAcademicYearMaster(int org_Id, int academic_Id, string mode)
        {
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("2021_pro_CRUD_AcademicYear", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = org_Id;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academic_Id;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;
            List<AcademicYear> getAcademicDetails = new List<AcademicYear>();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            conn.Close();
            getAcademicDetails = commonCode.ConvertDataTable<AcademicYear>(Dt);
            return getAcademicDetails;
        }

    }
}
