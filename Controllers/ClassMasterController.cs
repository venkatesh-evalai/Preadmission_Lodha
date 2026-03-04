using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Preadmission_Lodha.Models;
using System.Data;

namespace Preadmission_Lodha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassMasterController : ControllerBase
    {
        [HttpGet("getUserClass")]
        public List<ClassMaster> getUserClass(int org_Id, int academic_Id, int staff_Id, int user_Type_Id, int user_Role_Id, String? mode = "GET_USER_CLASS")
        {
            if (mode != "GETCLASS_Classteacher")
            {
                mode = "GET_USER_CLASS";
            }

            SqlConnection conn = new SqlConnection(commonCode.conStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Pro_CRUD_Class_Master", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = mode;
            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = org_Id;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = academic_Id;
            cmd.Parameters.Add("@staff_Id", SqlDbType.Int).Value = staff_Id;
            cmd.Parameters.Add("@user_Type_Id", SqlDbType.Int).Value = user_Type_Id;
            cmd.Parameters.Add("@user_Role_Id", SqlDbType.Int).Value = user_Role_Id;
            List<ClassMaster> data = new List<ClassMaster>();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            conn.Close();
            data = commonCode.ConvertDataTable<ClassMaster>(Dt);
            return data;
        }
    }
}
