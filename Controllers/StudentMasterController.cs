using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Preadmission_Lodha.Models;
using System.Data;

namespace Preadmission_Lodha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMasterController : ControllerBase
    {

        [HttpGet("getBloodGroup")]
        public List<bloodGroup> getBloodGroup()
        {
            SqlConnection con = new SqlConnection(commonCode.conStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("pro_CRUD_BloodGroup", con);
            cmd.CommandType = CommandType.StoredProcedure;
            List<bloodGroup> getAcademicYear = new List<bloodGroup>();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            con.Close();
            getAcademicYear = commonCode.ConvertDataTable<bloodGroup>(Dt);
            return getAcademicYear;
        }

        [HttpGet("getCountry")]
        public List<Address> getCountry()
        {
            SqlConnection con = new SqlConnection(commonCode.conStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("pro_CRUD_Country_Master", con);
            cmd.CommandType = CommandType.StoredProcedure;
            List<Address> getC = new List<Address>();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            con.Close();
            getC = commonCode.ConvertDataTable<Address>(Dt);
            return getC;
        }

        [HttpGet("getState")]
        public List<Address> getState(int countryId)
        {
            SqlConnection con = new SqlConnection(commonCode.conStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("pro_CRUD_State_Master", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@countryId", countryId));
            List<Address> getS = new List<Address>();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            con.Close();
            getS = commonCode.ConvertDataTable<Address>(Dt);
            return getS;
        }

        [HttpGet("getCity")]
        public List<Address> getCity(int countryId, int stateId)
        {
            SqlConnection con = new SqlConnection(commonCode.conStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("pro_CRUD_City_Master", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@countryId", countryId));
            cmd.Parameters.Add(new SqlParameter("@stateId", stateId));
            List<Address> getCty = new List<Address>();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            con.Close();
            getCty = commonCode.ConvertDataTable<Address>(Dt);
            return getCty;
        }

    }
}
