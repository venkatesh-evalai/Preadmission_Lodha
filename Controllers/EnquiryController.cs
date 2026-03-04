using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Preadmission_Lodha.Models;
using System.Data;

namespace Preadmission_Lodha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {

        [HttpPost("crudEnquirylodha")]
        public CommonModal crudEnquirylodha(CRUDEnquiry1 a)
        {

            CommonModal e1 = new CommonModal();
            SqlConnection conn = new SqlConnection(commonCode.conStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Pro_admissionEnquiry", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@org_Id", SqlDbType.Int).Value = a.org_Id;
            cmd.Parameters.Add("@academic_Id", SqlDbType.Int).Value = a.academic_Id;
            //     cmd.Parameters.Add("@enqiryId", SqlDbType.Int).Value = a.enquiry_No;
            cmd.Parameters.Add("@lead_Id", SqlDbType.Int).Value = a.lead_id;
            cmd.Parameters.Add("@classId", SqlDbType.Int).Value = a.class_Id;
            cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = a.stud_First_Name;
            cmd.Parameters.Add("@middleName", SqlDbType.NVarChar).Value = a.stud_Middle_Name;
            cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = a.stud_Last_Name;
            cmd.Parameters.Add("@gender", SqlDbType.NVarChar).Value = a.gender;
            //cmd.Parameters.Add("@date_Of_Birth", SqlDbType.DateTime).Value = a.date_Of_Birth != DateTime.MinValue.Date ? a.date_Of_Birth : DateTime.Now;
            cmd.Parameters.Add("@date_Of_Birth", SqlDbType.Date).Value =
a.date_Of_Birth != DateTime.MinValue ? a.date_Of_Birth.Date : DateTime.Now.Date;
            cmd.Parameters.Add("@place_Of_Birth", SqlDbType.NVarChar).Value = a.place_Of_Birth;
            cmd.Parameters.Add("@nationality_no", SqlDbType.NVarChar).Value = a.nationality;
            cmd.Parameters.Add("@religion_no", SqlDbType.NVarChar).Value = a.religion;
            cmd.Parameters.Add("@mother_Tongue", SqlDbType.NVarChar).Value = a.mother_Tongue;
            cmd.Parameters.Add("@caste_Id", SqlDbType.Int).Value = a.caste_Id;
            cmd.Parameters.Add("@caste", SqlDbType.NVarChar).Value = a.caste;
            cmd.Parameters.Add("@previously_applied", SqlDbType.Bit).Value = a.previously_applied;
            cmd.Parameters.Add("@previous_application_date", SqlDbType.Date).Value = a.previous_application_date;
            cmd.Parameters.Add("@last_school", SqlDbType.NVarChar).Value = a.last_School;
            cmd.Parameters.Add("@previous_marks", SqlDbType.NVarChar).Value = a.previous_marks;
            cmd.Parameters.Add("@previous_udise", SqlDbType.NVarChar).Value = a.previous_udise;
            cmd.Parameters.Add("@aadhar_no", SqlDbType.NVarChar).Value = a.aadhar_no;
            cmd.Parameters.Add("@specialability", SqlDbType.NVarChar).Value = a.specialability;
            cmd.Parameters.Add("@specialInterest", SqlDbType.NVarChar).Value = a.specialInterest;
            cmd.Parameters.Add("@mother_Name", SqlDbType.NVarChar).Value = a.mother_Name;
            cmd.Parameters.Add("@mother_Mobile", SqlDbType.NVarChar).Value = a.mother_Mobile;
            cmd.Parameters.Add("@mother_Email", SqlDbType.NVarChar).Value = a.mother_Email;
            cmd.Parameters.Add("@mother_Education", SqlDbType.NVarChar).Value = a.mother_Edu_Qualification;
            cmd.Parameters.Add("@mother_Occupation", SqlDbType.NVarChar).Value = a.mother_Occupation;
            cmd.Parameters.Add("@motherorganization", SqlDbType.NVarChar).Value = a.motherorganization;
            cmd.Parameters.Add("@motherdesignation", SqlDbType.NVarChar).Value = a.motherdesignation;
            cmd.Parameters.Add("@father_Name", SqlDbType.NVarChar).Value = a.father_Name;
            cmd.Parameters.Add("@fahter_Mobile", SqlDbType.NVarChar).Value = a.father_Mobile;
            cmd.Parameters.Add("@father_Email", SqlDbType.NVarChar).Value = a.father_Email;
            cmd.Parameters.Add("@father_Education", SqlDbType.NVarChar).Value = a.father_Edu_Qualification;
            cmd.Parameters.Add("@father_Occupation", SqlDbType.NVarChar).Value = a.father_Occupation;
            cmd.Parameters.Add("@fatherorganization", SqlDbType.NVarChar).Value = a.fatherorganization;
            cmd.Parameters.Add("@fatherdesigntion", SqlDbType.NVarChar).Value = a.fatherdesigntion;
            cmd.Parameters.Add("@current_Address", SqlDbType.NVarChar).Value = a.current_Address;
            cmd.Parameters.Add("@c_Location", SqlDbType.NVarChar).Value = a.c_Location;
            cmd.Parameters.Add("@c_landmark", SqlDbType.NVarChar).Value = a.c_landmark;
            cmd.Parameters.Add("@c_State", SqlDbType.Int).Value = a.c_State;
            cmd.Parameters.Add("@c_City", SqlDbType.Int).Value = a.c_City;
            cmd.Parameters.Add("@c_ZipCode", SqlDbType.Int).Value = a.c_ZipCode;
            cmd.Parameters.Add("@c_residential_status", SqlDbType.NVarChar).Value = a.c_residential_status;
            cmd.Parameters.Add("@guardian_Name", SqlDbType.NVarChar).Value = a.guardian_Name;
            cmd.Parameters.Add("@relationship", SqlDbType.NVarChar).Value = a.relationship;
            cmd.Parameters.Add("@guardian_type", SqlDbType.NVarChar).Value = a.guardian_type;
            cmd.Parameters.Add("@guardian_Mobile", SqlDbType.NVarChar).Value = a.guardian_Mobile;
            cmd.Parameters.Add("@guardian_address", SqlDbType.NVarChar).Value = a.guardian_address;
            cmd.Parameters.Add("@have_sibling", SqlDbType.Bit).Value = a.have_sibling;
            cmd.Parameters.Add("@siblingname1", SqlDbType.NVarChar).Value = a.siblingname1;
            cmd.Parameters.Add("@siblingclass1", SqlDbType.NVarChar).Value = a.siblingclass1;
            cmd.Parameters.Add("@have_reference", SqlDbType.Bit).Value = a.have_reference;
            cmd.Parameters.Add("@referencename", SqlDbType.NVarChar).Value = a.referencename;
            cmd.Parameters.Add("@referenceclass", SqlDbType.NVarChar).Value = a.referenceclass;
            cmd.Parameters.Add("@reference_relation", SqlDbType.NVarChar).Value = a.reference_relation;
            cmd.Parameters.Add("@student_height", SqlDbType.NVarChar).Value = a.student_height;
            cmd.Parameters.Add("@student_weight", SqlDbType.NVarChar).Value = a.student_weight;
            cmd.Parameters.Add("@blood_group_id", SqlDbType.Int).Value = a.Blood_group_id;
            cmd.Parameters.Add("@polio_immunization", SqlDbType.Bit).Value = a.polio_immunization;
            cmd.Parameters.Add("@dpt_immunization", SqlDbType.Bit).Value = a.dpt_immunization;
            cmd.Parameters.Add("@mmr_immunization", SqlDbType.Bit).Value = a.mmr_immunization;
            cmd.Parameters.Add("@cholera_immunization", SqlDbType.Bit).Value = a.cholera_immunization;
            cmd.Parameters.Add("@long_term_medication", SqlDbType.NVarChar).Value = a.long_term_medication;
            cmd.Parameters.Add("@health_specific_information", SqlDbType.NVarChar).Value = a.health_specific_information;
            cmd.Parameters.Add("@allergies", SqlDbType.NVarChar).Value = a.allergies;
            cmd.Parameters.Add("@learning_disability", SqlDbType.NVarChar).Value = a.learning_disability;
            cmd.Parameters.Add("@special_health_instructions", SqlDbType.NVarChar).Value = a.special_health_instructions;
            cmd.Parameters.Add("@past_hospitalization", SqlDbType.NVarChar).Value = a.past_hospitalization;
            cmd.Parameters.Add("@tearms_accepted", SqlDbType.Bit).Value = a.tearms_accepted;
            cmd.Parameters.Add("@applicant_name", SqlDbType.NVarChar).Value = a.applicant_name;
            cmd.Parameters.Add("@parent_name", SqlDbType.NVarChar).Value = a.parent_name;
            cmd.Parameters.Add("@declaration_place", SqlDbType.NVarChar).Value = a.declaration_place;
            cmd.Parameters.Add("@created_date", SqlDbType.DateTime).Value = a.created_date;
            cmd.Parameters.Add("@lodha_resident", SqlDbType.Int).Value = a.lodha_resident;
            cmd.Parameters.Add("@appstep", SqlDbType.Int).Value = a.app_step;
            cmd.Parameters.Add("@f_code", SqlDbType.NVarChar).Value = a.f_code;
            cmd.Parameters.Add("@m_code", SqlDbType.NVarChar).Value = a.m_code;
            cmd.Parameters.Add("@g_code", SqlDbType.NVarChar).Value = a.g_code;
            cmd.Parameters.Add("@cluster", SqlDbType.Int).Value = a.cluster;
            cmd.Parameters.Add("@branch_Id", SqlDbType.Int).Value = a.branch_Id;
            cmd.Parameters.Add("@districtofbirth", SqlDbType.NVarChar).Value = a.districtofbirth;
            cmd.Parameters.Add("@Talukaofbirth", SqlDbType.NVarChar).Value = a.Talukaofbirth;
            cmd.Parameters.Add("@country_Name_ad", SqlDbType.NVarChar).Value = a.country_Name_ad;

            cmd.Parameters.Add("@name_of_preschool", SqlDbType.NVarChar).Value = a.name_of_preschool;
            cmd.Parameters.Add("@board_of_Study", SqlDbType.Int).Value = a.board_of_Study;
            cmd.Parameters.Add("@grade_in_Previous", SqlDbType.NVarChar).Value = a.grade_in_Previous;
            cmd.Parameters.Add("@year_of_completion", SqlDbType.Int).Value = a.year_of_completion;

            cmd.Parameters.Add("@english", SqlDbType.Int).Value = a.english;
            cmd.Parameters.Add("@biology", SqlDbType.Int).Value = a.biology;
            cmd.Parameters.Add("@business_studies", SqlDbType.Int).Value = a.business_studies;
            cmd.Parameters.Add("@computer_science", SqlDbType.Int).Value = a.computer_science;
            cmd.Parameters.Add("@math", SqlDbType.Int).Value = a.math;
            cmd.Parameters.Add("@physics", SqlDbType.Int).Value = a.physics;
            cmd.Parameters.Add("@economics", SqlDbType.Int).Value = a.economics;
            cmd.Parameters.Add("@chemistry", SqlDbType.Int).Value = a.chemistry;
            cmd.Parameters.Add("@accounts", SqlDbType.Int).Value = a.accounts;
            cmd.Parameters.Add("@psychology", SqlDbType.Int).Value = a.psychology;

            cmd.Parameters.Add("@religionother", SqlDbType.NVarChar).Value = a.religionother;
            cmd.Parameters.Add("@sub_caste", SqlDbType.NVarChar).Value = a.sub_caste;
            cmd.Parameters.Add("@persue_reason", SqlDbType.NVarChar).Value = a.persue_reason;
            cmd.Parameters.Add("@guarduian_Name2", SqlDbType.NVarChar).Value = a.guarduian_Name2;
            cmd.Parameters.Add("@relationShip2", SqlDbType.NVarChar).Value = a.relationShip2;
            cmd.Parameters.Add("@guardian_Mobile2", SqlDbType.NVarChar).Value = a.guardian_Mobile2;
            cmd.Parameters.Add("@guardian_type2", SqlDbType.NVarChar).Value = a.guardian_type2;
            cmd.Parameters.Add("@guardian_address2", SqlDbType.NVarChar).Value = a.guardian_address2;

            cmd.Parameters.Add("@fathermonthlyincome", SqlDbType.Decimal).Value = a.father_monthly_income;
            cmd.Parameters.Add("@mothermonthlyincome", SqlDbType.Decimal).Value = a.mother_monthly_income;

            cmd.Parameters.Add("@mother_Org_sector", SqlDbType.NVarChar).Value = a.mother_Org_sector;
            cmd.Parameters.Add("@father_Org_sector", SqlDbType.NVarChar).Value = a.father_Org_sector;

            cmd.Parameters.Add("@inter_Math", SqlDbType.Int).Value = a.inter_Math;
            cmd.Parameters.Add("@spanish", SqlDbType.Int).Value = a.spanish;
            cmd.Parameters.Add("@hindi", SqlDbType.Int).Value = a.hindi;
            cmd.Parameters.Add("@evm", SqlDbType.Int).Value = a.evm;

            cmd.Parameters.Add("@apaar_No", SqlDbType.NVarChar).Value = a.apaar_No;
            cmd.Parameters.Add("@pen_No", SqlDbType.NVarChar).Value = a.pen_No;
            cmd.Parameters.Add("@known_from", SqlDbType.NVarChar).Value = a.known_from;

            cmd.Parameters.Add("@mode", SqlDbType.NVarChar).Value = "INSERTPeaddmisiionlodha";

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataTable Dt = ds.Tables[0];
            DataTable Dt1 = ds.Tables[1];
            DataTable Dt2 = ds.Tables[2];
            DataTable Dt3 = ds.Tables[3];
            conn.Close();
            if (Dt.Rows.Count > 0)
            {
                // e1.ResponseStatus = Dt1.Rows[0]["status1"].ToString();
                e1.ResponseMessage = Dt3.Rows[0]["MSG"].ToString();
                e1.data = Dt2;
            }
            else
            {
                e1.ResponseStatus = "False";
                e1.ResponseMessage = "Failed";
            }
            return e1;


        }

    }
}
