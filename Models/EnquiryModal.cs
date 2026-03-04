namespace Preadmission_Lodha.Models
{
    public class CRUDEnquiry1
    {
        public int? lodha_resident { get; set; }
        public int? cluster { get; set; }
        public int? app_step { get; set; }
        public string? f_code { get; set; }
        public string? m_code { get; set; }
        public string? g_code { get; set; }
        public string? security_key { get; set; }
        public string? application_Form_No { get; set; }
        public string? admission_Form_No { get; set; }
        public int? enquirer_relationship { get; set; }
        public string? father_Office_Address { get; set; }
        public string? dd_Number { get; set; }
        public DateTime? dd_Date { get; set; }
        public string? cheque_Number { get; set; }
        public DateTime? cheque_Date { get; set; }
        public DateTime? payment_Date { get; set; }
        public string? reference_Code { get; set; }
        public string? bank_Name { get; set; }
        public string? branch_Name { get; set; }

        public string? receipt_Mode { get; set; }
        public string? special_Interest { get; set; }
        public string? tc_No { get; set; }


        public string? nationality_no { get; set; }
        public string? religion_no { get; set; }
        public string? mothertongue_no { get; set; }
        public string? syllabus_followed { get; set; }
        public string? special_Expextation { get; set; }
        public string? father_office_contact { get; set; }
        public string? mother_office_address { get; set; }
        public string? mother_office_contact { get; set; }
        public string? old_School_Address { get; set; }
        public string? others_text { get; set; }
        public string? follow_up_1 { get; set; }
        public string? follow_up_2 { get; set; }
        public string? counselor_name { get; set; }

        public int? transport_applicable { get; set; }
        public int? hostel_applicable { get; set; }

        public decimal father_monthly_income { get; set; }
        public decimal mother_monthly_income { get; set; }

        public DateTime? admission_date { get; set; }
        public int? org_Id { get; set; }
        public int? branch_Id { get; set; }
        public int? academic_Id { get; set; }
        public int? enquiry_No { get; set; }
        public int? class_Id { get; set; }
        public string? stud_First_Name { get; set; }
        public string? stud_Middle_Name { get; set; }
        public string? stud_Last_Name { get; set; }
        public string? gender { get; set; }
        public DateTime date_Of_Birth { get; set; }
        public int? Priority { get; set; }
        public int? Stage { get; set; }
        public int? Status { get; set; }
        public int? newer_status { get; set; }
        public DateTime? close_date { get; set; }
        public string? admissionNumber { get; set; }
        public string? father_Name { get; set; }
        public string? father_Edu_Qualification { get; set; }
        public string? father_Occupation { get; set; }
        public string? father_Mobile { get; set; }
        public string? father_Email { get; set; }
        public string? mother_Name { get; set; }
        public string? mother_Edu_Qualification { get; set; }
        public string? mother_Occupation { get; set; }
        public string? mother_Mobile { get; set; }
        public string? mother_Email { get; set; }
        public string? guardian_Name { get; set; }
        public string? guardian_Mobile { get; set; }
        public string? guardian_Email { get; set; }
        public string? relationship { get; set; }
        public string? reason_To_Change { get; set; }
        public string? last_School { get; set; }
        public string? known_By { get; set; }
        public int? sister { get; set; }
        public int? brother { get; set; }
        public int? kisok { get; set; }
        public int? staff { get; set; }
        public int? sib_Age { get; set; }
        public string? sib_Class { get; set; }
        public string? sib_School { get; set; }
        public string? admission_Query { get; set; }
        public int? p_State { get; set; }
        public int? p_City { get; set; }
        public int? p_Country { get; set; }
        public string? p_Location { get; set; }
        public string? p_ZipCode { get; set; }
        public int? c_State { get; set; }
        public int? c_City { get; set; }
        public int? c_Country { get; set; }

        public decimal Application_Fee { get; set; }
        public int? Paid { get; set; }

        public int? Adm_Payment_Mode { get; set; }
        public int? App_Payment_Mode { get; set; }

        public decimal Admission_Fee { get; set; }
        public int? Adm_Fee_Paid { get; set; }
        public DateTime? App_Fee_Date { get; set; }
        public DateTime? Adm_Fee_Date { get; set; }

        public string? c_Location { get; set; }
        public string? c_ZipCode { get; set; }
        public string? permnt_Address { get; set; }
        public string? current_Address { get; set; }
        public int? currentIsPermanent { get; set; }
        public string? country_Name { get; set; }
        public string? state_Name { get; set; }
        public string? city_Name { get; set; }
        public string? IP_address { get; set; }
        public string? username { get; set; }
        public string? mode { get; set; }
        public string? Language1 { get; set; }
        public int? Age { get; set; }
        public string? Category { get; set; }
        public string? stud_mob_no { get; set; }
        public string? stud_email_id { get; set; }
        public string? aadhar_no { get; set; }
        public Boolean? physically_handicapped { get; set; }
        public string? physically_handicapped_details { get; set; }
        public string? bank_acc_no { get; set; }
        public string? ifsc_code { get; set; }
        public string? correspondence_address { get; set; }
        public string? guardian_address { get; set; }
        public string? classX_exam_details { get; set; }
        public string? classXI_exam_details { get; set; }
        public string? classGNM_exam_details { get; set; }
        public string? other_exam_details { get; set; }
        public string? reg_karnataka_council_details { get; set; }
        public string? work_experience { get; set; }
        public string? docs_path { get; set; }
        public string? classXII_exam_details { get; set; }
        public string? blood_group { get; set; }

        public int? physically_handicapped1 { get; set; }
        // public string? stud_mob_no { get; set; }

        public int? caste_Id { get; set; }
        public int? subcaste_Id { get; set; }

        public int? Blood_group_id { get; set; }
        public string? aadharCardNo { get; set; }
        public string? emailId { get; set; }
        public int? isPhysical { get; set; }

        public string? Phydetails { get; set; }

        public string? BankName { get; set; }
        public string? Bankbranch { get; set; }
        public string? AccoNo { get; set; }
        public string? IFSCCode { get; set; }

        public string? age1 { get; set; }
        public string? age2 { get; set; }
        public string? age3 { get; set; }
        public string? age4 { get; set; }
        public string? age5 { get; set; }
        public string? age6 { get; set; }
        public string? age7 { get; set; }
        public string? doctor_Name { get; set; }

        public string? yrs_Of_Complt1 { get; set; }
        public string? yrs_Of_Complt2 { get; set; }
        public string? yrs_Of_Complt3 { get; set; }
        public string? yrs_Of_Complt4 { get; set; }
        public string? yrs_Of_Complt5 { get; set; }
        public string? standard1 { get; set; }
        public string? standard2 { get; set; }

        //new code for
        public string? GNMName { get; set; }
        public string? GNMNameUniv { get; set; }
        public string? GNMyearofpassing { get; set; }
        public string? GNMmaxMars { get; set; }
        public string? GNMTotalMarkSecured { get; set; }
        public string? GNMPerofMark { get; set; }


        public string? CousrseReg { get; set; }
        public string? Regnos { get; set; }
        public string? DateofReg { get; set; }
        public string? ValidTill { get; set; }
        public string? TnaiMemno { get; set; }


        public string? Expdesignation1 { get; set; }
        public string? ExpnameifIns1 { get; set; }
        public string? ExpFrom1 { get; set; }
        public string? ExpTo1 { get; set; }
        public string? ExpTotalDu1 { get; set; }

        public string? Expdesignation2 { get; set; }
        public string? ExpnameifIns2 { get; set; }
        public string? ExpFrom2 { get; set; }
        public string? ExpTo2 { get; set; }
        public string? ExpTotalDu2 { get; set; }

        public string? Expdesignation3 { get; set; }
        public string? ExpnameifIns3 { get; set; }
        public string? ExpFrom3 { get; set; }
        public string? ExpTo3 { get; set; }
        public string? ExpTotalDu3 { get; set; }

        public string? Expdesignation4 { get; set; }
        public string? ExpnameifIns4 { get; set; }
        public string? ExpFrom4 { get; set; }
        public string? ExpTo4 { get; set; }
        public string? ExpTotalDu4 { get; set; }

        public string? Expdesignation5 { get; set; }
        public string? ExpnameifIns5 { get; set; }
        public string? ExpFrom5 { get; set; }
        public string? ExpTo5 { get; set; }
        public string? ExpTotalDu5 { get; set; }



        public string? place_Of_Birth { get; set; }
        public string? nationality { get; set; }
        public string? religion { get; set; }
        public string? mother_Tongue { get; set; }
        public string? caste { get; set; }
        public string? specialability { get; set; }

        public string? motherorganization { get; set; }
        public string? motherdesignation { get; set; }
        public string? fatherorganization { get; set; }
        public string? fatherdesigntion { get; set; }
        public string? guarduian_Name { get; set; }
        public string? siblingname1 { get; set; }
        public string? siblingclass1 { get; set; }

        public DateTime? previous_application_date { get; set; }
        public Boolean? previously_applied { get; set; }
        public string? previous_marks { get; set; }
        public string? previous_udise { get; set; }
        public string? guardian_type { get; set; }
        public string? c_landmark { get; set; }
        public string? c_residential_status { get; set; }
        public Boolean? have_sibling { get; set; }
        public Boolean? have_reference { get; set; }
        public string? referencename { get; set; }
        public string? referenceclass { get; set; }
        public string? reference_relation { get; set; }
        public string? student_height { get; set; }
        public string? student_weight { get; set; }
        public Boolean? polio_immunization { get; set; }
        public Boolean? dpt_immunization { get; set; }
        public Boolean? mmr_immunization { get; set; }
        public Boolean? cholera_immunization { get; set; }
        public string? long_term_medication { get; set; }
        public string? health_specific_information { get; set; }
        public string? allergies { get; set; }
        public string? learning_disability { get; set; }
        public string? special_health_instructions { get; set; }
        public string? past_hospitalization { get; set; }
        public string? photo_name { get; set; }
        public string? photo_uri { get; set; }
        public Boolean? tearms_accepted { get; set; }
        public string? applicant_name { get; set; }
        public string? parent_name { get; set; }
        public string? declaration_place { get; set; }
        public int? completed_step { get; set; }

        public DateTime? created_date { get; set; }

        public string? specialInterest { get; set; }
        public int? lead_id { get; set; }
        public string? districtofbirth { get; set; }
        public string? Talukaofbirth { get; set; }
        public string? country_Name_ad { get; set; }


        //new 04042025
        public string? name_of_preschool { get; set; }
        public int? board_of_Study { get; set; }
        public string? grade_in_Previous { get; set; }
        public int? year_of_completion { get; set; }
        public int? english { get; set; }
        public int? biology { get; set; }
        public int? business_studies { get; set; }
        public int? computer_science { get; set; }
        public int? math { get; set; }
        public int? physics { get; set; }
        public int? economics { get; set; }
        public int? chemistry { get; set; }
        public int? accounts { get; set; }
        public int? psychology { get; set; }

        public string? religionother { get; set; }
        public string? sub_caste { get; set; }
        public string? persue_reason { get; set; }
        public string? guarduian_Name2 { get; set; }
        public string? relationShip2 { get; set; }
        public string? guardian_Mobile2 { get; set; }
        public string? guardian_type2 { get; set; }
        public string? guardian_address2 { get; set; }

        public string? mother_Org_sector { get; set; }
        public string? father_Org_sector { get; set; }
        public int? inter_Math { get; set; }
        public int? spanish { get; set; }
        public int? hindi { get; set; }
        public int? evm { get; set; }
        public string? apaar_No { get; set; }
        public string? pen_No { get; set; }
        public string? known_from { get; set; }
    }
}
