using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Preadmission_Lodha.Models;
using System.Data;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace Preadmission_Lodha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionFormController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public AdmissionFormController(IWebHostEnvironment env)
        {
            _env = env;
        }
        string error = "";

        [HttpGet("AdmissonformforSchool")]
        public CommonModal AdmissonformforSchool(int org_Id, int academic_Id, int leadid)
        {
            CommonModal e1 = new CommonModal();
            try
            {

                //int studid = student_Id;
                AdditionalController additional = new AdditionalController();



                string path = "";
                string path1 = "";
                string getFolder1 = "";
                string getFolder2 = "";
                XFont font = new XFont("Times New Roman", 11, XFontStyle.Regular);
                XFont font2 = new XFont("Times New Roman", 10, XFontStyle.Regular);
                XFont font1 = new XFont("Times New Roman", 11, XFontStyle.Bold);
                XFont font5 = new XFont("Times New Roman", 15, XFontStyle.Bold);
                XFont font6 = new XFont("Times New Roman", 9, XFontStyle.Regular);
                XFont font7 = new XFont("Times New Roman", 22, XFontStyle.Bold);
                XFont font8 = new XFont("Times New Roman", 17, XFontStyle.Regular);
                DataTable report = additional.getAdmEnquiryDetails(org_Id, academic_Id, leadid, "GETADMISSIONEnquiry1");
                string stud_First_Name = "";
                string stud_Middle_Name = "";
                string stud_Last_Name = "";
                string parent_name = ""; string father_Email = "";
                string date_Of_Birth = ""; string gender = ""; string place_Of_Birth = ""; string nationality = ""; string country_Name = ""; string mother_Tongue = ""; string religion = ""; string caste = ""; string subcaste = ""; string father_Mobile = "";
                string Schoolcampus = ""; string Category = ""; string Grade = ""; string aadhar_no = ""; string PreviouslyLWS = ""; string PresentSchoolAttended = ""; string PresentSchoolGrade = ""; string UDISENumber = "";
                string Academicstrengths = ""; string Otherinterests = ""; string mother_Name = ""; string mother_Edu_Qualification = ""; string mother_Occupation = ""; string motherorganization = ""; string mother_Mobile = ""; string father_Name = "";
                string mother_Email = ""; string father_Edu_Qualification = ""; string father_Occupation = ""; string fatherdesigntion = ""; string c_residential_status = ""; string current_Address = ""; string Clustername = ""; string Address1 = "";
                string Address2 = ""; string Pincode = ""; string guarduian_Name = ""; string guarduian_Name1 = ""; string relationship = "";
                string relationship1 = ""; string guardian_Mobile = ""; string guardian_Mobile1 = ""; string guardian_type = ""; string guardian_type1 = ""
                 ; string guardian_address = ""; string guardian_address1 = ""; string fatherorganization = ""; string parentLastname = ""; string Talukaofbirth = ""; string districtofbirth = "";
                string religionother = ""; string previousdateofapp = ""; string FlatnoWings = ""; string student_height = ""; string motherdesignation = "";
                string student_weight = ""; string Blood_group_name = ""; string Doesyourchildsuffer = "";
                string Doesyourchildhaveanylearningdisability = "";
                string Hasyourchildreceivedanyspecialinstruction = "";
                string Hasyourchildbeenhospitalizedforanyillness = "";
                string Anyotherhealthspecificinformation = "";
                string ImmunizationReceived = "";
                string Receivinganylongmedi = "";
                string siblinginfo = "";
                string siblingname1 = "";
                string SiblingGrade = "";
                string DoyouaddanothSibling = "";
                string anyothrefestuLodhaSchool = "";
                string SecondSiblingName = "";
                string SecondSiblingGrade = "";
                string ReferenceName = "";
                string ReferenceGrade = "";
                string Referencerelationwithstudent = "";
                string applicant_name = "";
                string declarationPlace = "";
                string declarationDate = "";
                string application_Form_No = "";
                string academic_Year = "";



                string dateOfBirth = "";

                int class_Id = 0;
                int branch_Id = 0;
                string branch_Name = "";

                string lastschool = "";
                string siblingage = "";
                string sib_Class = "";
                string siblingname2 = "";
                string siblingclass2 = "";
                string allergies = "";
                string longtermmedication = "";
                bool have_sibling = true;

                string name_of_preschool = "";
                int board_of_Study = 0;
                string grade_in_Previous = "";
                int year_of_completion = 0;
                int english = 0;
                int biology = 0;
                int business_studies = 0;
                int computer_science = 0;
                int math = 0;
                int physics = 0;
                int economics = 0;
                int chemistry = 0;
                int accounts = 0;
                int psychology = 0;

                string persue_reason = "";
                string state_Name = "";

                string mother_Org_sector = "";
                string father_Org_sector = "";

                int inter_Math = 0;
                int spanish = 0;
                int hindi = 0;
                int evm = 0;

                string apaar_No = "";
                string pen_No = "";
                string known_from = "";

                foreach (DataRow row in report.Rows)
                {
                    class_Id = row.Field<int>("class_Id");
                    branch_Id = row.Field<int>("branch_Id");
                    branch_Name = row.Field<string>("branch_Name") ?? "";

                    academic_Year = row.Field<string>("academic_Year") ?? "";
                    application_Form_No = row.Field<string>("application_Form_No") ?? "";
                    stud_First_Name = row.Field<string>("stud_First_Name") ?? "";
                    stud_Middle_Name = row.Field<string>("stud_Middle_Name") ?? "";
                    stud_Last_Name = row.Field<string>("stud_Last_Name") ?? "";
                    parent_name = row.Field<string>("parent_name") ?? "";
                    parentLastname = row.Field<string>("parentLastname") ?? "";
                    father_Email = row.Field<string>("father_Email") ?? "";
                    date_Of_Birth = row.Field<DateTime>("date_Of_Birth").ToString("dd-MM-yyyy") ?? "";

                    gender = row.Field<string>("gender") ?? "";
                    place_Of_Birth = row.Field<string>("place_Of_Birth") ?? "";
                    districtofbirth = row.Field<string>("districtofbirth") ?? "";
                    Talukaofbirth = row.Field<string>("Talukaofbirth") ?? "";

                    nationality = row.Field<string>("nationality") ?? "";
                    country_Name = row.Field<string>("country_Name") ?? "";
                    mother_Tongue = row.Field<string>("mother_Tongue") ?? "";
                    religion = row.Field<string>("religion") ?? "";
                    religionother = row.Field<string>("religionother") ?? "";
                    caste = row.Field<string>("caste") ?? "";
                    subcaste = row.Field<string>("subcaste") ?? "";
                    father_Mobile = row.Field<string>("father_Mobile") ?? "";
                    Schoolcampus = row.Field<string>("Schoolcampus") ?? "";
                    Category = row.Field<string>("Category") ?? "";
                    Grade = row.Field<string>("Grade") ?? "";
                    PreviouslyLWS = row.Field<string>("PreviouslyLWS") ?? "";
                    previousdateofapp = row.Field<DateTime>("previousdateofapp").ToString("dd-MM-yyyy") ?? "";
                    PresentSchoolAttended = row.Field<string>("PresentSchoolAttended") ?? "";
                    PresentSchoolGrade = row.Field<string>("PresentSchoolGrade") ?? "";

                    aadhar_no = row.Field<string>("aadhar_no") ?? "";
                    UDISENumber = row.Field<string>("UDISENumber") ?? "";
                    Academicstrengths = row.Field<string>("Academicstrengths") ?? "";
                    Otherinterests = row.Field<string>("Otherinterests") ?? "";
                    mother_Name = row.Field<string>("mother_Name") ?? "";
                    mother_Edu_Qualification = row.Field<string>("mother_Edu_Qualification") ?? "";
                    motherdesignation = row.Field<string>("motherdesignation") ?? "";

                    mother_Occupation = row.Field<string>("mother_Occupation") ?? "";
                    motherorganization = row.Field<string>("motherorganization") ?? "";
                    mother_Mobile = row.Field<string>("mother_Mobile") ?? "";
                    mother_Email = row.Field<string>("mother_Email") ?? "";
                    father_Name = row.Field<string>("father_Name") ?? "";
                    father_Edu_Qualification = row.Field<string>("father_Edu_Qualification") ?? "";
                    father_Occupation = row.Field<string>("father_Occupation") ?? "";
                    fatherdesigntion = row.Field<string>("fatherdesigntion") ?? "";
                    fatherorganization = row.Field<string>("fatherorganization") ?? "";
                    father_Mobile = row.Field<string>("father_Mobile") ?? "";
                    father_Email = row.Field<string>("father_Email") ?? "";
                    c_residential_status = row.Field<string>("c_residential_status") ?? "";
                    FlatnoWings = row.Field<string>("FlatnoWings") ?? "";
                    current_Address = row.Field<string>("current_Address") ?? "";
                    Clustername = row.Field<string>("Clustername") ?? "";
                    Address1 = row.Field<string>("Address1") ?? "";
                    Address2 = row.Field<string>("Address2") ?? "";
                    Pincode = row.Field<string>("Pincode") ?? "";
                    guarduian_Name = row.Field<string>("guarduian_Name") ?? "";
                    relationship = row.Field<string>("relationship") ?? "";
                    guardian_Mobile = row.Field<string>("guardian_Mobile") ?? "";
                    guardian_type = row.Field<string>("guardian_type") ?? "";
                    guardian_address = row.Field<string>("guardian_address") ?? "";

                    guarduian_Name1 = row.Field<string>("guarduian_Name1") ?? "";
                    relationship1 = row.Field<string>("relationship1") ?? "";
                    guardian_Mobile1 = row.Field<string>("guardian_Mobile1") ?? "";
                    guardian_type1 = row.Field<string>("guardian_type1") ?? "";
                    guardian_address1 = row.Field<string>("guardian_address1") ?? "";

                    student_height = row.Field<string>("student_height") ?? "";
                    student_weight = row.Field<string>("student_weight") ?? "";
                    Blood_group_name = row.Field<string>("Blood_group_name") ?? "";
                    ImmunizationReceived = row.Field<string>("ImmunizationReceived") ?? "";
                    Receivinganylongmedi = row.Field<string>("Receivinganylongmedi") ?? "";
                    Doesyourchildsuffer = row.Field<string>("Doesyourchildsuffer") ?? "";
                    Doesyourchildhaveanylearningdisability = row.Field<string>("Doesyourchildhaveanylearningdisability") ?? "";
                    Hasyourchildreceivedanyspecialinstruction = row.Field<string>("Hasyourchildreceivedanyspecialinstruction") ?? "";
                    Hasyourchildbeenhospitalizedforanyillness = row.Field<string>("Hasyourchildbeenhospitalizedforanyillness") ?? "";
                    Anyotherhealthspecificinformation = row.Field<string>("Anyotherhealthspecificinformation") ?? "";

                    siblinginfo = row.Field<string>("siblinginfo") ?? "";
                    siblingname1 = row.Field<string>("siblingname1") ?? "";
                    SiblingGrade = row.Field<string>("SiblingGrade") ?? "";
                    DoyouaddanothSibling = row.Field<string>("DoyouaddanothSibling") ?? "";
                    anyothrefestuLodhaSchool = row.Field<string>("anyothrefestuLodhaSchool") ?? "";
                    SecondSiblingName = row.Field<string>("SecondSiblingName") ?? "";
                    SecondSiblingGrade = row.Field<string>("SecondSiblingGrade") ?? "";
                    ReferenceName = row.Field<string>("ReferenceName") ?? "";
                    ReferenceGrade = row.Field<string>("ReferenceGrade") ?? "";
                    Referencerelationwithstudent = row.Field<string>("Referencerelationwithstudent") ?? "";
                    applicant_name = row.Field<string>("applicant_name") ?? "";
                    declarationPlace = row.Field<string>("declarationPlace") ?? "";
                    declarationDate = row.Field<DateTime>("declarationDate").ToString("dd-MM-yyyy") ?? "";//row.Field<string>("declarationDate") ?? "";

                    lastschool = row.Field<string>("last_School") ?? "";
                    siblingage = row.Field<string>("sib_Age") ?? "";
                    sib_Class = row.Field<string>("siblingclass1") ?? "";
                    siblingname2 = row.Field<string>("siblingname2") ?? "";
                    siblingclass2 = row.Field<string>("siblingclass2") ?? "";

                    allergies = row.Field<string>("allergies") ?? "";
                    longtermmedication = row.Field<string>("long_term_medication") ?? "";
                    have_sibling = row.Field<bool>("have_sibling");

                    name_of_preschool = row.Field<string>("name_of_preschool") ?? "";
                    board_of_Study = row.Field<int>("board_of_Study");
                    grade_in_Previous = row.Field<string>("grade_in_Previous") ?? "";
                    year_of_completion = row.Field<int>("year_of_completion");

                    english = row.Field<int>("english");
                    biology = row.Field<int>("biology");
                    business_studies = row.Field<int>("business_studies");
                    computer_science = row.Field<int>("computer_science");
                    math = row.Field<int>("math");
                    physics = row.Field<int>("physics");
                    economics = row.Field<int>("economics");
                    chemistry = row.Field<int>("chemistry");
                    accounts = row.Field<int>("accounts");
                    psychology = row.Field<int>("psychology");

                    persue_reason = row.Field<string>("persue_reason") ?? "";
                    state_Name = row.Field<string>("state_Name") ?? "";

                    mother_Org_sector = row.Field<string>("mother_Org_sector") ?? "";
                    father_Org_sector = row.Field<string>("father_Org_sector") ?? "";

                    inter_Math = row.Field<int>("inter_Math");
                    spanish = row.Field<int>("spanish");
                    hindi = row.Field<int>("hindi");
                    evm = row.Field<int>("evm");

                    apaar_No = row.Field<string>("apaar_No") ?? "";
                    pen_No = row.Field<string>("pen_No") ?? "";
                    known_from = row.Field<string>("known_from") ?? "";
                }

                PdfSharpCore.Pdf.PdfDocument PDFNewDoc = new PdfSharpCore.Pdf.PdfDocument();
                getFolder1 = Path.Combine(_env.ContentRootPath, "Group/AdmissionForm/Commonfile/commonfile2.pdf");
                if (org_Id == 210 || org_Id == 211 || org_Id == 212 || org_Id == 214 || org_Id == 223)
                {
                    path = Path.Combine(_env.ContentRootPath, "Group/AdmissionForm/ApplicationFormAY2024-25.pdf");
                }
                else if (org_Id == 213 && (class_Id == 11 || class_Id == 12))
                {
                    path = Path.Combine(_env.ContentRootPath, "Group/AdmissionForm/LOS Admission Form edit_AS_.pdf");
                }
                else if (org_Id == 213 && (class_Id >= 8 && class_Id <= 10))
                {
                    path = Path.Combine(_env.ContentRootPath, "Group/AdmissionForm/LOS Admission Form 8-10.pdf");
                }
                else
                {
                    //path = HttpContext.Current.Server.MapPath("~/Group/AdmissionForm/08.10.2024_1923849500_LOS Admission Form edit_AS.pdf");
                    path = Path.Combine(_env.ContentRootPath, "Group/AdmissionForm/LOSAdmissionForm.pdf");
                }
                PdfSharpCore.Pdf.PdfDocument PDFDoc = PdfSharpCore.Pdf.IO.PdfReader.Open(path, PdfDocumentOpenMode.Import);
                //PdfSharpCore.Pdf.PdfDocument PDFNewDoc = new PdfSharpCore.Pdf.PdfDocument();


                // Console.WriteLine($"Name: {stud_First_Name}");
                var dirfordelete = "";
                var savefilefor2 = "";
                double maxWidth = 180;
                double maxWidth1 = 210;
                double maxWidth2 = 250;
                double maxWidth3 = 200;
                double maxWidth4 = 50;
                //gfx.DrawString("Rahul Niwas Patil fffff vvvv", font, XBrushes.Black,
                //     new XRect(Math.Max(45 - (gfx.MeasureString("Rahul Niwas Patil fffff vvvv", font).Width - maxWidth) / 2, 45), 175, maxWidth, pp.Height), XStringFormats.TopLeft);


                string filename = "AdmissionForm" + "-" + leadid + ".pdf";
                savefilefor2 = Path.Combine(_env.ContentRootPath,"Group/AdmissionForm/output/");
                //dirfordelete = HttpContext.Current.Server.MapPath("~/Group/ReportCard/" + org_Id + "/ReportCardOutput/");

                void DrawWrappedText(XGraphics g, string inputtext, XFont inputfont, XBrush brush, XRect rect, double inputmaxWidth, int maxLines, double lineHeight)
                {
                    var words = inputtext.Split(' ');
                    string currentLine = "";
                    double y = rect.Y;
                    int linesDrawn = 0;
                    bool isFirstLine = true;

                    foreach (var word in words)
                    {
                        string testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
                        var size = g.MeasureString(testLine, inputfont);
                        double lineX = isFirstLine ? rect.X : 56D;
                        double allowedWidth = inputmaxWidth - (lineX - rect.X);
                        if (size.Width > allowedWidth)
                        {
                            g.DrawString(currentLine, inputfont, brush, new XRect(lineX, y, allowedWidth, rect.Height), XStringFormats.TopLeft);

                            y += lineHeight;
                            linesDrawn++;
                            if (linesDrawn >= maxLines)
                                throw new ArgumentOutOfRangeException("Max lines exceeded while wrapping text.");

                            currentLine = word;
                            isFirstLine = false;
                        }
                        else
                        {
                            currentLine = testLine;
                        }
                    }

                    // Draw the last line
                    if (!string.IsNullOrWhiteSpace(currentLine) && linesDrawn < maxLines)
                    {
                        double lineStartX = isFirstLine ? rect.X : 56D;
                        double allowedWidth = inputmaxWidth - (lineStartX - rect.X);

                        g.DrawString(currentLine, font, brush, new XRect(lineStartX, y, allowedWidth, rect.Height), XStringFormats.TopLeft);
                    }
                }


                if (org_Id == 210 || org_Id == 211 || org_Id == 212 || org_Id == 214 || org_Id == 223)
                {
                    PdfPage pp = PDFNewDoc.AddPage(PDFDoc.Pages[0]);
                    PdfPage pp1 = PDFNewDoc.AddPage(PDFDoc.Pages[1]);
                    PdfPage pp2 = PDFNewDoc.AddPage(PDFDoc.Pages[2]);
                    PdfPage pp3 = PDFNewDoc.AddPage(PDFDoc.Pages[3]);
                    //PdfPage pp4 = PDFNewDoc.AddPage(PDFDoc.Pages[4]);

                    XGraphics gfx = XGraphics.FromPdfPage(pp);
                    XGraphics gfx1 = XGraphics.FromPdfPage(pp1);
                    XGraphics gfx2 = XGraphics.FromPdfPage(pp2);
                    XGraphics gfx3 = XGraphics.FromPdfPage(pp3);
                    // XGraphics gfx4 = XGraphics.FromPdfPage(pp4);

                    gfx.DrawString(academic_Year.ToString(), font7, XBrushes.Black, new XRect(278, 41, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(80, 85, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //   gfx.DrawString(stud_First_Name.ToString(), font, XBrushes.Black, new XRect(65, 175, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(stud_First_Name.ToString(), font, XBrushes.Black,
                        new XRect(Math.Max(45 - (gfx.MeasureString(stud_First_Name.ToString(), font).Width - maxWidth) / 2, 45), 175, maxWidth, pp.Height), XStringFormats.TopLeft);


                    //      gfx.DrawString(stud_Middle_Name.ToString(), font, XBrushes.Black, new XRect(230, 175, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(stud_Middle_Name.ToString(), font, XBrushes.Black,
                       new XRect(Math.Max(210 - (gfx.MeasureString(stud_Middle_Name.ToString(), font).Width - maxWidth) / 2, 210), 175, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //    gfx.DrawString(stud_Last_Name.ToString(), font, XBrushes.Black, new XRect(390, 175, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(stud_Last_Name.ToString(), font, XBrushes.Black,
                         new XRect(Math.Max(370 - (gfx.MeasureString(stud_Last_Name.ToString(), font).Width - maxWidth) / 2, 370), 175, maxWidth, pp.Height), XStringFormats.TopLeft);


                    //   gfx.DrawString(parent_name.ToString(), font, XBrushes.Black, new XRect(65, 220, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(parent_name.ToString(), font, XBrushes.Black,
                new XRect(Math.Max(45 - (gfx.MeasureString(parent_name.ToString(), font).Width - maxWidth) / 2, 45), 220, maxWidth, pp.Height), XStringFormats.TopLeft);


                    //   gfx.DrawString(parentLastname.ToString(), font, XBrushes.Black, new XRect(230, 220, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(parentLastname.ToString(), font, XBrushes.Black,
                 new XRect(Math.Max(210 - (gfx.MeasureString(parentLastname.ToString(), font).Width - maxWidth) / 2, 210), 220, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //   gfx.DrawString(father_Email.ToString(), font, XBrushes.Black, new XRect(390, 220, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Email.ToString(), font, XBrushes.Black,
                       new XRect(Math.Max(370 - (gfx.MeasureString(father_Email.ToString(), font).Width - maxWidth) / 2, 370), 220, maxWidth, pp.Height), XStringFormats.TopLeft);


                    // gfx.DrawString(date_Of_Birth.ToString(), font, XBrushes.Black, new XRect(110, 265, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(date_Of_Birth.ToString(), font, XBrushes.Black,
                new XRect(Math.Max(45 - (gfx.MeasureString(date_Of_Birth.ToString(), font).Width - maxWidth) / 2, 45), 265, maxWidth, pp.Height), XStringFormats.TopLeft);


                    //if (gender == "Male")
                    //{
                    //    gfx.DrawString(gender.ToString(), font, XBrushes.Black, new XRect(285, 265, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //}
                    //else
                    //{
                    //    gfx.DrawString(gender.ToString(), font, XBrushes.Black, new XRect(280, 265, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //}
                    gfx.DrawString(gender.ToString(), font, XBrushes.Black,
            new XRect(Math.Max(210 - (gfx.MeasureString(gender.ToString(), font).Width - maxWidth) / 2, 210), 265, maxWidth, pp.Height), XStringFormats.TopLeft);

                    // gfx.DrawString(gender.ToString(), font, XBrushes.Black, new XRect(285, 265, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //   gfx.DrawString(place_Of_Birth.ToString(), font, XBrushes.Black, new XRect(390, 265, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(place_Of_Birth.ToString(), font, XBrushes.Black,
                   new XRect(Math.Max(370 - (gfx.MeasureString(place_Of_Birth.ToString(), font).Width - maxWidth) / 2, 370), 265, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //    gfx.DrawString(districtofbirth.ToString(), font, XBrushes.Black, new XRect(110, 310, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(districtofbirth.ToString(), font, XBrushes.Black,
       new XRect(Math.Max(45 - (gfx.MeasureString(districtofbirth.ToString(), font).Width - maxWidth) / 2, 45), 310, maxWidth, pp.Height), XStringFormats.TopLeft);

                    // gfx.DrawString(Talukaofbirth.ToString(), font, XBrushes.Black, new XRect(230, 310, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(Talukaofbirth.ToString(), font, XBrushes.Black,
            new XRect(Math.Max(210 - (gfx.MeasureString(Talukaofbirth.ToString(), font).Width - maxWidth) / 2, 210), 310, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //  gfx.DrawString(nationality.ToString(), font, XBrushes.Black, new XRect(450, 310, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(nationality.ToString(), font, XBrushes.Black,
                      new XRect(Math.Max(370 - (gfx.MeasureString(nationality.ToString(), font).Width - maxWidth) / 2, 370), 310, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //    gfx.DrawString(country_Name.ToString(), font, XBrushes.Black, new XRect(65, 355, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(country_Name.ToString(), font, XBrushes.Black,
                   new XRect(Math.Max(45 - (gfx.MeasureString(country_Name.ToString(), font).Width - maxWidth) / 2, 45), 355, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //      gfx.DrawString(mother_Tongue.ToString(), font, XBrushes.Black, new XRect(230, 355, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Tongue.ToString(), font, XBrushes.Black,
                        new XRect(Math.Max(210 - (gfx.MeasureString(mother_Tongue.ToString(), font).Width - maxWidth) / 2, 210), 355, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //    gfx.DrawString(religion.ToString(), font, XBrushes.Black, new XRect(390, 355, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(religion.ToString(), font, XBrushes.Black,
                      new XRect(Math.Max(370 - (gfx.MeasureString(religion.ToString(), font).Width - maxWidth) / 2, 370), 355, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //     gfx.DrawString(religionother.ToString(), font, XBrushes.Black, new XRect(65, 400, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(religion.ToString(), font, XBrushes.Black,
                      new XRect(Math.Max(45 - (gfx.MeasureString(religion.ToString(), font).Width - maxWidth) / 2, 45), 400, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //     gfx.DrawString(caste.ToString(), font, XBrushes.Black, new XRect(230, 400, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(caste.ToString(), font, XBrushes.Black,
                 new XRect(Math.Max(210 - (gfx.MeasureString(caste.ToString(), font).Width - maxWidth) / 2, 210), 400, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //     gfx.DrawString(subcaste.ToString(), font, XBrushes.Black, new XRect(390, 400, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(subcaste.ToString(), font, XBrushes.Black,
                 new XRect(Math.Max(370 - (gfx.MeasureString(subcaste.ToString(), font).Width - maxWidth) / 2, 370), 400, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //   gfx.DrawString(father_Mobile.ToString(), font, XBrushes.Black, new XRect(105, 445, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Mobile.ToString(), font, XBrushes.Black,
                new XRect(Math.Max(45 - (gfx.MeasureString(father_Mobile.ToString(), font).Width - maxWidth) / 2, 45), 445, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //    gfx.DrawString(Schoolcampus.ToString(), font6, XBrushes.Black, new XRect(230, 445, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(Schoolcampus.ToString(), font, XBrushes.Black,
             new XRect(Math.Max(210 - (gfx.MeasureString(Schoolcampus.ToString(), font).Width - maxWidth) / 2, 210), 445, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //   gfx.DrawString(Category.ToString(), font, XBrushes.Black, new XRect(442, 445, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(Category.ToString(), font, XBrushes.Black,
             new XRect(Math.Max(370 - (gfx.MeasureString(Category.ToString(), font).Width - maxWidth) / 2, 370), 445, maxWidth, pp.Height), XStringFormats.TopLeft);


                    //   gfx.DrawString(Grade.ToString(), font, XBrushes.Black, new XRect(65, 490, pp.Width, pp.Height), XStringFormats.TopLeft);
                    /*gfx.DrawString(Grade.ToString(), font, XBrushes.Black,
       new XRect(Math.Max(45 - (gfx.MeasureString(Grade.ToString(), font).Width - maxWidth) / 2, 45), 490, maxWidth, pp.Height), XStringFormats.TopLeft);*/

                    if ((class_Id == 11 || class_Id == 12) && (branch_Id != 0))
                    {
                        gfx.DrawString(Grade.ToString() + " - " + branch_Name.ToString(), font, XBrushes.Black,
           new XRect(Math.Max(30 - (gfx.MeasureString(Grade.ToString(), font).Width - maxWidth) / 2, 30), 490, maxWidth, pp.Height), XStringFormats.TopLeft);
                    }
                    else
                    {
                        gfx.DrawString(Grade.ToString(), font, XBrushes.Black,
           new XRect(Math.Max(45 - (gfx.MeasureString(Grade.ToString(), font).Width - maxWidth) / 2, 45), 490, maxWidth, pp.Height), XStringFormats.TopLeft);
                    }


                    //  gfx.DrawString(PreviouslyLWS.ToString(), font, XBrushes.Black, new XRect(154, 535, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(PreviouslyLWS.ToString(), font, XBrushes.Black,
        new XRect(Math.Max(70 - (gfx.MeasureString(PreviouslyLWS.ToString(), font).Width - maxWidth1) / 2, 70), 535, maxWidth1, pp.Height), XStringFormats.TopLeft);



                    if (previousdateofapp == "01-01-0001" || previousdateofapp == "01-01-1900")
                    {
                        gfx.DrawString(" ", font, XBrushes.Black, new XRect(400, 535, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    else
                    {
                        //  gfx.DrawString(previousdateofapp.ToString(), font, XBrushes.Black, new XRect(400, 535, pp.Width, pp.Height), XStringFormats.TopLeft);

                        gfx.DrawString(previousdateofapp.ToString(), font, XBrushes.Black,
       new XRect(Math.Max(315 - (gfx.MeasureString(previousdateofapp.ToString(), font).Width - maxWidth1) / 2, 315), 535, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    }
                    // gfx.DrawString(previousdateofapp.ToString(), font, XBrushes.Black, new XRect(400, 535, pp.Width, pp.Height), XStringFormats.TopLeft);

                    // gfx.DrawString(PresentSchoolAttended.ToString(), font, XBrushes.Black, new XRect(65, 580, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(PresentSchoolAttended.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(70 - (gfx.MeasureString(PresentSchoolAttended.ToString(), font).Width - maxWidth1) / 2, 70), 580, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    // gfx.DrawString(PresentSchoolGrade.ToString(), font, XBrushes.Black, new XRect(310, 580, pp.Width, pp.Height), XStringFormats.TopLeft);
                    // gfx.DrawString(PresentSchoolGrade.ToString(), font, XBrushes.Black,
                    //new XRect(Math.Max(100 - (gfx.MeasureString(PresentSchoolGrade.ToString(), font).Width - maxWidth1) / 2, 100), 580, maxWidth1, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(PresentSchoolGrade.ToString(), font, XBrushes.Black, new XRect(360, 580, pp.Width, pp.Height), XStringFormats.TopLeft);


                    //  gfx.DrawString(aadhar_no.ToString(), font, XBrushes.Black, new XRect(140, 625, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(aadhar_no.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(70 - (gfx.MeasureString(aadhar_no.ToString(), font).Width - maxWidth1) / 2, 70), 625, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    // gfx.DrawString(UDISENumber.ToString(), font, XBrushes.Black, new XRect(310, 625, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //         gfx.DrawString(UDISENumber.ToString(), font, XBrushes.Black,
                    //new XRect(Math.Max(315 - (gfx.MeasureString(UDISENumber.ToString(), font).Width - maxWidth1) / 2, 315), 625, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    if (pen_No != "" && apaar_No != "")
                    {
                        gfx.DrawString(apaar_No.ToString() + " / " + pen_No.ToString(), font, XBrushes.Black,
               new XRect(Math.Max(250 - (gfx.MeasureString(UDISENumber.ToString(), font).Width - maxWidth1) / 2, 250), 625, maxWidth1, pp.Height), XStringFormats.TopCenter);
                    }
                    else if (apaar_No != "" && pen_No == "")
                    {
                        gfx.DrawString(apaar_No.ToString(), font, XBrushes.Black,
               new XRect(Math.Max(250 - (gfx.MeasureString(UDISENumber.ToString(), font).Width - maxWidth1) / 2, 250), 625, maxWidth1, pp.Height), XStringFormats.TopLeft);
                    }
                    else if (apaar_No == "" && pen_No != "")
                    {
                        gfx.DrawString(pen_No.ToString(), font, XBrushes.Black,
               new XRect(Math.Max(250 - (gfx.MeasureString(UDISENumber.ToString(), font).Width - maxWidth1) / 2, 250), 625, maxWidth1, pp.Height), XStringFormats.TopLeft);
                    }

                    //   gfx.DrawString(Academicstrengths.ToString(), font, XBrushes.Black, new XRect(230, 667, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(Academicstrengths.ToString(), font, XBrushes.Black,
              new XRect(Math.Max(125 - (gfx.MeasureString(Academicstrengths.ToString(), font).Width - maxWidth2) / 2, 125), 667, maxWidth2, pp.Height), XStringFormats.TopLeft);

                    //     gfx.DrawString(Otherinterests.ToString(), font, XBrushes.Black, new XRect(230, 710, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(Otherinterests.ToString(), font, XBrushes.Black,
        new XRect(Math.Max(155 - (gfx.MeasureString(Otherinterests.ToString(), font).Width - maxWidth2) / 2, 155), 710, maxWidth2, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(known_from.ToString(), font, XBrushes.Black,
new XRect(Math.Max(155 - (gfx.MeasureString(Otherinterests.ToString(), font).Width - maxWidth2) / 2, 155), 748, maxWidth2, pp.Height), XStringFormats.TopCenter);

                    //page 2 
                    //   gfx1.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(80, 59, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(academic_Year.ToString(), font7, XBrushes.Black, new XRect(278, 19, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //   gfx1.DrawString(mother_Name.ToString(), font, XBrushes.Black, new XRect(70, 140, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(mother_Name.ToString(), font, XBrushes.Black,
              new XRect(Math.Max(100 - (gfx1.MeasureString(mother_Name.ToString(), font).Width - maxWidth1) / 2, 100), 140, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //  gfx1.DrawString(mother_Edu_Qualification.ToString(), font, XBrushes.Black, new XRect(320, 140, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(mother_Edu_Qualification.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(345 - (gfx1.MeasureString(mother_Edu_Qualification.ToString(), font).Width - maxWidth1) / 2, 345), 140, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //  gfx1.DrawString(mother_Occupation.ToString(), font, XBrushes.Black, new XRect(70, 185, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(mother_Occupation.ToString(), font, XBrushes.Black,
           new XRect(Math.Max(45 - (gfx1.MeasureString(mother_Occupation.ToString(), font).Width - maxWidth) / 2, 45), 185, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //    gfx1.DrawString(motherdesignation.ToString(), font, XBrushes.Black, new XRect(160, 185, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(motherdesignation.ToString(), font, XBrushes.Black,
          new XRect(Math.Max(210 - (gfx1.MeasureString(motherdesignation.ToString(), font).Width - maxWidth) / 2, 210), 185, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //    gfx1.DrawString(motherorganization.ToString(), font, XBrushes.Black, new XRect(390, 185, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(motherorganization.ToString(), font, XBrushes.Black,
       new XRect(Math.Max(370 - (gfx1.MeasureString(motherorganization.ToString(), font).Width - maxWidth) / 2, 370), 185, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //    gfx1.DrawString(mother_Mobile.ToString(), font, XBrushes.Black, new XRect(146, 230, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(mother_Mobile.ToString(), font, XBrushes.Black,
            new XRect(Math.Max(70 - (gfx1.MeasureString(mother_Mobile.ToString(), font).Width - maxWidth1) / 2, 70), 230, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //   gfx1.DrawString(mother_Email.ToString(), font, XBrushes.Black, new XRect(320, 230, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(mother_Email.ToString(), font, XBrushes.Black,
                    new XRect(Math.Max(315 - (gfx1.MeasureString(mother_Email.ToString(), font).Width - maxWidth1) / 2, 315), 230, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //  gfx1.DrawString(father_Name.ToString(), font, XBrushes.Black, new XRect(70, 275, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(father_Name.ToString(), font, XBrushes.Black,
      new XRect(Math.Max(70 - (gfx1.MeasureString(father_Name.ToString(), font).Width - maxWidth1) / 2, 70), 275, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //   gfx1.DrawString(father_Edu_Qualification.ToString(), font, XBrushes.Black, new XRect(320, 275, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(father_Edu_Qualification.ToString(), font, XBrushes.Black,
    new XRect(Math.Max(315 - (gfx1.MeasureString(father_Edu_Qualification.ToString(), font).Width - maxWidth1) / 2, 315), 275, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //  gfx1.DrawString(father_Occupation.ToString(), font, XBrushes.Black, new XRect(70, 320, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(father_Occupation.ToString(), font, XBrushes.Black,
           new XRect(Math.Max(45 - (gfx1.MeasureString(father_Occupation.ToString(), font).Width - maxWidth) / 2, 45), 320, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //   gfx1.DrawString(fatherdesigntion.ToString(), font, XBrushes.Black, new XRect(230, 320, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(fatherdesigntion.ToString(), font, XBrushes.Black,
                 new XRect(Math.Max(210 - (gfx1.MeasureString(fatherdesigntion.ToString(), font).Width - maxWidth) / 2, 210), 320, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //  gfx1.DrawString(fatherorganization.ToString(), font, XBrushes.Black, new XRect(390, 320, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(fatherorganization.ToString(), font, XBrushes.Black,
               new XRect(Math.Max(370 - (gfx1.MeasureString(fatherorganization.ToString(), font).Width - maxWidth) / 2, 370), 320, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //gfx1.DrawString(father_Mobile.ToString(), font, XBrushes.Black, new XRect(147, 365, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(father_Mobile.ToString(), font, XBrushes.Black,
               new XRect(Math.Max(70 - (gfx1.MeasureString(father_Mobile.ToString(), font).Width - maxWidth1) / 2, 70), 365, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //       gfx1.DrawString(father_Email.ToString(), font, XBrushes.Black, new XRect(320, 365, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(father_Email.ToString(), font, XBrushes.Black,
               new XRect(Math.Max(315 - (gfx1.MeasureString(father_Email.ToString(), font).Width - maxWidth1) / 2, 315), 365, maxWidth1, pp.Height), XStringFormats.TopLeft);


                    gfx1.DrawString(c_residential_status.ToString(), font, XBrushes.Black, new XRect(240, 413, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(FlatnoWings.ToString(), font, XBrushes.Black, new XRect(240, 440, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(Clustername.ToString(), font, XBrushes.Black, new XRect(220, 463, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(Address1.ToString(), font, XBrushes.Black, new XRect(260, 490, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(Address2.ToString(), font, XBrushes.Black, new XRect(265, 516, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(Pincode.ToString(), font, XBrushes.Black, new XRect(200, 546, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //    gfx1.DrawString(guarduian_Name.ToString(), font, XBrushes.Black, new XRect(95, 618, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guarduian_Name.ToString(), font, XBrushes.Black,
             new XRect(Math.Max(43 - (gfx1.MeasureString(guarduian_Name.ToString(), font).Width - maxWidth) / 2, 43), 618, maxWidth, pp.Height), XStringFormats.TopLeft);

                    // gfx1.DrawString(relationship.ToString(), font, XBrushes.Black, new XRect(250, 618, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(relationship.ToString(), font, XBrushes.Black,
          new XRect(Math.Max(210 - (gfx1.MeasureString(relationship.ToString(), font).Width - maxWidth) / 2, 210), 618, maxWidth, pp.Height), XStringFormats.TopLeft);


                    //    gfx1.DrawString(guardian_Mobile.ToString(), font, XBrushes.Black, new XRect(420, 618, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_Mobile.ToString(), font, XBrushes.Black,
           new XRect(Math.Max(370 - (gfx1.MeasureString(guardian_Mobile.ToString(), font).Width - maxWidth) / 2, 370), 618, maxWidth, pp.Height), XStringFormats.TopLeft);


                    //       gfx1.DrawString(guardian_type.ToString(), font, XBrushes.Black, new XRect(110, 659, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_type.ToString(), font, XBrushes.Black,
          new XRect(Math.Max(42 - (gfx1.MeasureString(guardian_type.ToString(), font).Width - maxWidth) / 2, 42), 659, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //   gfx1.DrawString(guardian_address.ToString(), font6, XBrushes.Black, new XRect(210, 659, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_address.ToString(), font, XBrushes.Black,
                     new XRect(Math.Max(230 - (gfx1.MeasureString(guardian_address.ToString(), font).Width - maxWidth3) / 2, 230), 659, maxWidth3, pp.Height), XStringFormats.TopLeft);

                    //   gfx1.DrawString(guarduian_Name1.ToString(), font, XBrushes.Black, new XRect(90, 700, pp.Width, pp.Height), XStringFormats.TopLeft);
                  
                    //page 3

                    //        gfx2.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(80, 59, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(academic_Year.ToString(), font7, XBrushes.Black, new XRect(278, 19, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //   gfx2.DrawString(student_height.ToString(), font, XBrushes.Black, new XRect(120, 140, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(student_height.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(45 - (gfx2.MeasureString(student_height.ToString(), font).Width - maxWidth) / 2, 45), 140, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //  gfx2.DrawString(student_weight.ToString(), font, XBrushes.Black, new XRect(180, 140, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(student_weight.ToString(), font, XBrushes.Black,
 new XRect(Math.Max(210 - (gfx2.MeasureString(student_weight.ToString(), font).Width - maxWidth) / 2, 210), 140, maxWidth, pp.Height), XStringFormats.TopLeft);

                    //  gfx2.DrawString(Blood_group_name.ToString(), font, XBrushes.Black, new XRect(450, 140, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(Blood_group_name.ToString(), font, XBrushes.Black,
             new XRect(Math.Max(370 - (gfx2.MeasureString(Blood_group_name.ToString(), font).Width - maxWidth) / 2, 370), 140, maxWidth, pp.Height), XStringFormats.TopLeft);


                    //    gfx2.DrawString(ImmunizationReceived.ToString(), font, XBrushes.Black, new XRect(100, 190, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(ImmunizationReceived.ToString(), font, XBrushes.Black,
           new XRect(Math.Max(70 - (gfx2.MeasureString(ImmunizationReceived.ToString(), font).Width - maxWidth1) / 2, 70), 190, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //  gfx2.DrawString(Receivinganylongmedi.ToString(), font, XBrushes.Black, new XRect(350, 190, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(Receivinganylongmedi.ToString(), font, XBrushes.Black,
              new XRect(Math.Max(315 - (gfx2.MeasureString(Receivinganylongmedi.ToString(), font).Width - maxWidth1) / 2, 315), 190, maxWidth1, pp.Height), XStringFormats.TopLeft);


                    //   gfx2.DrawString(Doesyourchildsuffer.ToString(), font, XBrushes.Black, new XRect(120, 241, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(Doesyourchildsuffer.ToString(), font, XBrushes.Black,
           new XRect(Math.Max(155 - (gfx2.MeasureString(Doesyourchildsuffer.ToString(), font).Width - maxWidth2) / 2, 155), 241, maxWidth2, pp.Height), XStringFormats.TopLeft);


                    //     gfx2.DrawString(Doesyourchildhaveanylearningdisability.ToString(), font, XBrushes.Black, new XRect(80, 290, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(Doesyourchildhaveanylearningdisability.ToString(), font, XBrushes.Black,
           new XRect(Math.Max(70 - (gfx2.MeasureString(Doesyourchildhaveanylearningdisability.ToString(), font).Width - maxWidth1) / 2, 70), 290, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //  gfx2.DrawString(Hasyourchildreceivedanyspecialinstruction.ToString(), font, XBrushes.Black, new XRect(315, 290, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(Hasyourchildreceivedanyspecialinstruction.ToString(), font, XBrushes.Black,
              new XRect(Math.Max(315 - (gfx2.MeasureString(Hasyourchildreceivedanyspecialinstruction.ToString(), font).Width - maxWidth1) / 2, 315), 290, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //    gfx2.DrawString(Hasyourchildbeenhospitalizedforanyillness.ToString(), font, XBrushes.Black, new XRect(100, 350, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(Hasyourchildbeenhospitalizedforanyillness.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(155 - (gfx2.MeasureString(Hasyourchildbeenhospitalizedforanyillness.ToString(), font).Width - maxWidth2) / 2, 155), 350, maxWidth2, pp.Height), XStringFormats.TopLeft);

                    //      gfx2.DrawString(Anyotherhealthspecificinformation.ToString(), font, XBrushes.Black, new XRect(240, 426, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(Anyotherhealthspecificinformation.ToString(), font, XBrushes.Black,
                         new XRect(Math.Max(270 - (gfx2.MeasureString(Anyotherhealthspecificinformation.ToString(), font).Width - maxWidth) / 2, 270), 430, maxWidth, pp.Height), XStringFormats.TopLeft);


                    // page 4

                    //    gfx3.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(80, 59, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(academic_Year.ToString(), font7, XBrushes.Black, new XRect(278, 19, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //     gfx3.DrawString(siblinginfo.ToString(), font, XBrushes.Black, new XRect(70, 140, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(siblinginfo.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(155 - (gfx3.MeasureString(siblinginfo.ToString(), font).Width - maxWidth2) / 2, 155), 140, maxWidth2, pp.Height), XStringFormats.TopLeft);

                    //    gfx3.DrawString(siblingname1.ToString(), font, XBrushes.Black, new XRect(80, 185, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(siblingname1.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(70 - (gfx3.MeasureString(siblingname1.ToString(), font).Width - maxWidth1) / 2, 70), 185, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //   gfx3.DrawString(SiblingGrade.ToString(), font, XBrushes.Black, new XRect(315, 185, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(SiblingGrade.ToString(), font, XBrushes.Black,
          new XRect(Math.Max(315 - (gfx3.MeasureString(SiblingGrade.ToString(), font).Width - maxWidth1) / 2, 315), 185, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    gfx3.DrawString(DoyouaddanothSibling.ToString(), font, XBrushes.Black, new XRect(80, 245, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(DoyouaddanothSibling.ToString(), font, XBrushes.Black,
          new XRect(Math.Max(70 - (gfx3.MeasureString(DoyouaddanothSibling.ToString(), font).Width - maxWidth1) / 2, 70), 245, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //  gfx3.DrawString(anyothrefestuLodhaSchool.ToString(), font, XBrushes.Black, new XRect(315, 245, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(anyothrefestuLodhaSchool.ToString(), font, XBrushes.Black,
new XRect(Math.Max(315 - (gfx3.MeasureString(anyothrefestuLodhaSchool.ToString(), font).Width - maxWidth1) / 2, 315), 245, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //   gfx3.DrawString(SecondSiblingName.ToString(), font, XBrushes.Black, new XRect(80, 310, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(SecondSiblingName.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(70 - (gfx3.MeasureString(SecondSiblingName.ToString(), font).Width - maxWidth1) / 2, 70), 310, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //   gfx3.DrawString(SecondSiblingGrade.ToString(), font, XBrushes.Black, new XRect(315, 310, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(SecondSiblingGrade.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(315 - (gfx3.MeasureString(SecondSiblingGrade.ToString(), font).Width - maxWidth1) / 2, 315), 310, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //  gfx3.DrawString(ReferenceName.ToString(), font, XBrushes.Black, new XRect(80, 370, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(ReferenceName.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(70 - (gfx3.MeasureString(ReferenceName.ToString(), font).Width - maxWidth1) / 2, 70), 370, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    // gfx3.DrawString(ReferenceGrade.ToString(), font, XBrushes.Black, new XRect(315, 370, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(ReferenceGrade.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(315 - (gfx3.MeasureString(ReferenceGrade.ToString(), font).Width - maxWidth1) / 2, 315), 370, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    // gfx3.DrawString(Referencerelationwithstudent.ToString(), font, XBrushes.Black, new XRect(80, 435, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(Referencerelationwithstudent.ToString(), font, XBrushes.Black,
    new XRect(Math.Max(70 - (gfx3.MeasureString(Referencerelationwithstudent.ToString(), font).Width - maxWidth1) / 2, 70), 435, maxWidth1, pp.Height), XStringFormats.TopLeft);


                    //  gfx3.DrawString(applicant_name.ToString(), font, XBrushes.Black, new XRect(80, 698, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(applicant_name.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(70 - (gfx3.MeasureString(applicant_name.ToString(), font).Width - maxWidth1) / 2, 70), 698, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //  gfx3.DrawString(parent_name.ToString(), font, XBrushes.Black, new XRect(315, 706, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(parent_name.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(315 - (gfx3.MeasureString(parent_name.ToString(), font).Width - maxWidth1) / 2, 315), 706, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    //  gfx3.DrawString(declarationPlace.ToString(), font, XBrushes.Black, new XRect(80, 785, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(declarationPlace.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(70 - (gfx3.MeasureString(declarationPlace.ToString(), font).Width - maxWidth1) / 2, 70), 785, maxWidth1, pp.Height), XStringFormats.TopLeft);

                    // gfx3.DrawString(declarationDate.ToString(), font, XBrushes.Black, new XRect(315, 762, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(declarationDate.ToString(), font, XBrushes.Black,
         new XRect(Math.Max(315 - (gfx3.MeasureString(declarationDate.ToString(), font).Width - maxWidth1) / 2, 315), 762, maxWidth1, pp.Height), XStringFormats.TopLeft);

                }


                else if (org_Id == 213 && (class_Id == 11 || class_Id == 12))
                {
                    PdfPage pp = PDFNewDoc.AddPage(PDFDoc.Pages[0]);
                    PdfPage pp1 = PDFNewDoc.AddPage(PDFDoc.Pages[1]);
                    PdfPage pp2 = PDFNewDoc.AddPage(PDFDoc.Pages[2]);
                    PdfPage pp3 = PDFNewDoc.AddPage(PDFDoc.Pages[3]);
                    PdfPage pp4 = PDFNewDoc.AddPage(PDFDoc.Pages[4]);



                    XGraphics gfx = XGraphics.FromPdfPage(pp);
                    XGraphics gfx1 = XGraphics.FromPdfPage(pp1);
                    XGraphics gfx2 = XGraphics.FromPdfPage(pp2);
                    XGraphics gfx3 = XGraphics.FromPdfPage(pp3);
                    XGraphics gfx4 = XGraphics.FromPdfPage(pp4);
                    //gfx.DrawString(academic_Year.ToString(), font8, XBrushes.Black, new XRect(278, 73, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(249, 120, pp.Width, pp.Height), XStringFormats.TopLeft);

                    XFont font9 = new XFont("Arial", 40, XFontStyle.Bold);

                    gfx.DrawString(stud_First_Name.ToString() + " " + stud_Middle_Name.ToString() + " " + stud_Last_Name.ToString(), font, XBrushes.Black, new XRect(150, 163, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(parent_name.ToString() + " " + parentLastname.ToString(), font, XBrushes.Black, new XRect(150, 191, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString(father_Mobile.ToString(), font, XBrushes.Black, new XRect(482, 192, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(date_Of_Birth.ToString(), font, XBrushes.Black, new XRect(300, 191, pp.Width, pp.Height), XStringFormats.TopLeft);


                    if (gender == "Male")
                    {
                        gfx.DrawString(".", font9, XBrushes.Black, new XRect(123, 166, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    else
                    {
                        gfx.DrawString(".", font9, XBrushes.Black, new XRect(171, 166, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }

                    gfx.DrawString(place_Of_Birth.ToString(), font, XBrushes.Black, new XRect(480, 191, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(districtofbirth.ToString(), font, XBrushes.Black, new XRect(135, 219, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(state_Name.ToString(), font, XBrushes.Black, new XRect(305, 219, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(nationality.ToString(), font, XBrushes.Black, new XRect(470, 219, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(country_Name.ToString(), font, XBrushes.Black, new XRect(130, 247, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Tongue.ToString(), font, XBrushes.Black, new XRect(310, 247, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(religion.ToString(), font, XBrushes.Black, new XRect(460, 247, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(religionother.ToString(), font, XBrushes.Black, new XRect(130, 303, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(caste.ToString(), font, XBrushes.Black, new XRect(125, 275, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString(subcaste.ToString(), font, XBrushes.Black, new XRect(467, 303, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(subcaste.ToString(), font, XBrushes.Black, new XRect(365, 275, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(Schoolcampus.ToString(), font6, XBrushes.Black, new XRect(130, 332, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(Category.ToString(), font, XBrushes.Black, new XRect(190, 304, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(Grade.ToString(), font, XBrushes.Black, new XRect(405, 304, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(PreviouslyLWS.ToString(), font, XBrushes.Black, new XRect(170, 360, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //if (previousdateofapp == "01-01-0001" || previousdateofapp == "01-01-1900")
                    //{
                    //    gfx.DrawString(" ", font, XBrushes.Black, new XRect(370, 360, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //}
                    //else
                    //{
                    //    gfx.DrawString(previousdateofapp.ToString(), font, XBrushes.Black, new XRect(370, 360, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //}
                    //   gfx.DrawString(previousdateofapp.ToString(), font, XBrushes.Black, new XRect(370, 360, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(PresentSchoolAttended.ToString(), font6, XBrushes.Black, new XRect(157, 389, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString(PresentSchoolGrade.ToString(), font, XBrushes.Black, new XRect(490, 389, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(aadhar_no.ToString(), font, XBrushes.Black, new XRect(180, 332, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString(UDISENumber.ToString(), font, XBrushes.Black, new XRect(460, 416, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(UDISENumber.ToString(), font, XBrushes.Black, new XRect(461, 332, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(Academicstrengths.ToString(), font, XBrushes.Black, new XRect(60, 388, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString("Otherinterests.ToString()", font, XBrushes.Black, new XRect(300, 417, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(Otherinterests.ToString(), font, XBrushes.Black, new XRect(300, 417, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(mother_Name.ToString(), font, XBrushes.Black, new XRect(130, 471, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Edu_Qualification.ToString(), font, XBrushes.Black, new XRect(440, 471, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(mother_Mobile.ToString(), font, XBrushes.Black, new XRect(130, 495, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Email.ToString(), font, XBrushes.Black, new XRect(300, 495, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(mother_Occupation.ToString(), font, XBrushes.Black, new XRect(110, 522, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(motherdesignation.ToString(), font, XBrushes.Black, new XRect(350, 522, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(motherorganization.ToString(), font, XBrushes.Black, new XRect(140, 548, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Org_sector.ToString(), font, XBrushes.Black, new XRect(450, 548, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx.DrawString(father_Name.ToString(), font, XBrushes.Black, new XRect(130, 573, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Edu_Qualification.ToString(), font, XBrushes.Black, new XRect(440, 573, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(father_Mobile.ToString(), font, XBrushes.Black, new XRect(130, 599, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Email.ToString(), font, XBrushes.Black, new XRect(300, 599, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(father_Occupation.ToString(), font, XBrushes.Black, new XRect(110, 625, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(fatherdesigntion.ToString(), font, XBrushes.Black, new XRect(350, 625, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(fatherorganization.ToString(), font, XBrushes.Black, new XRect(145, 650, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Org_sector.ToString(), font, XBrushes.Black, new XRect(450, 650, pp.Width, pp.Height), XStringFormats.TopLeft);

                    DrawWrappedText(gfx, Address1.ToString() + " " + Pincode.ToString() + " .", font, XBrushes.Black, new XRect(153, 674, pp.Width, pp.Height), 400, 2, 22);




                    //page 2 

                    // gfx1.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(239, 100, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(academic_Year.ToString(), font8, XBrushes.Black, new XRect(278, 73, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(name_of_preschool.ToString(), font, XBrushes.Black, new XRect(140, 165, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(92, 166, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(138, 166, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(181, 166, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(213, 166, pp.Width, pp.Height), XStringFormats.TopLeft);

                    if (board_of_Study == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(92, 166, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    else if (board_of_Study == 3) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(138, 166, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    else if (board_of_Study == 2) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(181, 166, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    else { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(213, 166, pp.Width, pp.Height), XStringFormats.TopLeft); }

                    gfx1.DrawString(grade_in_Previous.ToString(), font, XBrushes.Black, new XRect(153, 219, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(year_of_completion.ToString(), font, XBrushes.Black, new XRect(453, 219, pp.Width, pp.Height), XStringFormats.TopLeft);

                    if (english == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 278, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    if (math == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 294, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    if (chemistry == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 309, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    if (biology == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 325, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    if (physics == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 341, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    if (accounts == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 356, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    if (business_studies == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 372, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    if (economics == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 388, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    if (psychology == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 404, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    if (computer_science == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 420, pp.Width, pp.Height), XStringFormats.TopLeft); }

                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 278, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 294, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 309, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 325, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 341, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 356, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 372, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 388, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 404, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(56, 420, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(guarduian_Name.ToString(), font, XBrushes.Black, new XRect(153, 499, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(relationship.ToString(), font, XBrushes.Black, new XRect(143, 527, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_Mobile.ToString(), font, XBrushes.Black, new XRect(460, 527, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString("9638524710", font, XBrushes.Black, new XRect(460, 247, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(guardian_type.ToString(), font, XBrushes.Black, new XRect(130, 554, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_address.ToString(), font6, XBrushes.Black, new XRect(59, 582, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString("address", font6, XBrushes.Black, new XRect(59, 274, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(guarduian_Name1.ToString(), font, XBrushes.Black, new XRect(150, 180, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(relationship1.ToString(), font, XBrushes.Black, new XRect(220, 208, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(guardian_Mobile1.ToString(), font, XBrushes.Black, new XRect(460, 208, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(guardian_type1.ToString(), font, XBrushes.Black, new XRect(130, 236, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(guardian_address1.ToString(), font6, XBrushes.Black, new XRect(59, 263, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(student_height.ToString(), font, XBrushes.Black, new XRect(100, 315, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(student_weight.ToString(), font, XBrushes.Black, new XRect(280, 315, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(Blood_group_name.ToString(), font, XBrushes.Black, new XRect(480, 315, pp.Width, pp.Height), XStringFormats.TopLeft);

                    /*if (class_Id == 11)
                    {
                        gfx1.DrawString("X", font, XBrushes.Black, new XRect(210, 516, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    else if (class_Id == 12)
                    {
                        gfx1.DrawString("XI", font, XBrushes.Black, new XRect(210, 516, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }*/

                    /*if (academic_Id == 11)
                    {
                        gfx1.DrawString("2025", font, XBrushes.Black, new XRect(140, 544, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    else if (academic_Id == 10)
                    {
                        gfx1.DrawString("2024", font, XBrushes.Black, new XRect(140, 544, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }*/
                    /*gfx1.DrawString(ImmunizationReceived.ToString(), font, XBrushes.Black, new XRect(153, 450, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(Receivinganylongmedi.ToString(), font, XBrushes.Black, new XRect(249, 477, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(Doesyourchildsuffer.ToString(), font, XBrushes.Black, new XRect(60, 526, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx1.DrawString(Doesyourchildhaveanylearningdisability.ToString(), font, XBrushes.Black, new XRect(60, 581, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(Hasyourchildreceivedanyspecialinstruction.ToString(), font, XBrushes.Black, new XRect(60, 632, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(Hasyourchildbeenhospitalizedforanyillness.ToString(), font, XBrushes.Black, new XRect(60, 685, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(Anyotherhealthspecificinformation.ToString(), font, XBrushes.Black, new XRect(60, 738, pp.Width, pp.Height), XStringFormats.TopLeft);
*/

                    //page 3






                    //gfx2.DrawString(".", font9, XBrushes.Black, new XRect(56, 431, pp.Width, pp.Height), XStringFormats.TopLeft);

                    if (longtermmedication != "No" || longtermmedication != "")
                    {
                        gfx2.DrawString(".", font9, XBrushes.Black, new XRect(56, 338, pp.Width, pp.Height), XStringFormats.TopLeft);
                        gfx2.DrawString("", font, XBrushes.Black, new XRect(145, 384, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    else
                    {
                        gfx2.DrawString(".", font9, XBrushes.Black, new XRect(102, 338, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    /*gfx2.DrawString(".", font9, XBrushes.Black, new XRect(56, 492, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(".", font9, XBrushes.Black, new XRect(102, 492, pp.Width, pp.Height), XStringFormats.TopLeft);*/
                    if (allergies != "No" || allergies != "")
                    {
                        gfx2.DrawString(".", font9, XBrushes.Black, new XRect(56, 399, pp.Width, pp.Height), XStringFormats.TopLeft);
                        gfx2.DrawString(allergies, font, XBrushes.Black, new XRect(145, 443, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    else
                    {
                        gfx2.DrawString(".", font9, XBrushes.Black, new XRect(56, 399, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    gfx2.DrawString(father_Name.ToString(), font, XBrushes.Black, new XRect(90, 484, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(father_Mobile, font, XBrushes.Black, new XRect(120, 503, pp.Width, pp.Height), XStringFormats.TopLeft);


                    XRect rec1 = new XRect(56, 590.6, pp.Width, pp.Height);


                    DrawWrappedText(gfx2, persue_reason.ToString(), font, XBrushes.Black, rec1, 500, 3, 27.7D);
                    gfx2.DrawString("", font, XBrushes.Black, new XRect(56, 674, pp.Width, pp.Height), XStringFormats.TopLeft); //additional Information

                    //Academicstrengths
                    /*gfx2.DrawString(applicant_name.ToString(), font, XBrushes.Black, new XRect(130, 490, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(parent_name.ToString(), font, XBrushes.Black, new XRect(125, 517, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(declarationPlace.ToString(), font, XBrushes.Black, new XRect(140, 545, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(declarationDate.ToString(), font, XBrushes.Black, new XRect(420, 545, pp.Width, pp.Height), XStringFormats.TopLeft);*/
                    if (have_sibling == true)
                    {
                        gfx3.DrawString("Yes", font, XBrushes.Black, new XRect(240, 165, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    else
                    {
                        gfx3.DrawString("No", font, XBrushes.Black, new XRect(240, 165, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    gfx3.DrawString(siblingname1, font, XBrushes.Black, new XRect(150, 191, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx3.DrawString(anyothrefestuLodhaSchool, font, XBrushes.Black, new XRect(310, 247, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx3.DrawString(siblingage, font, XBrushes.Black, new XRect(430, 191, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString("", font, XBrushes.Black, new XRect(240, 218, pp.Width, pp.Height), XStringFormats.TopLeft); //Add another sibling

                    gfx3.DrawString(siblingname2, font, XBrushes.Black, new XRect(150, 274, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(siblingclass2, font, XBrushes.Black, new XRect(433, 274, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(ReferenceName, font, XBrushes.Black, new XRect(130, 302, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(ReferenceGrade.ToString(), font, XBrushes.Black, new XRect(410, 302, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx3.DrawString(Referencerelationwithstudent.ToString(), font, XBrushes.Black, new XRect(190, 330, pp.Width, pp.Height), XStringFormats.TopLeft);

                    /*gfx3.DrawString(academic_Year.ToString(), font8, XBrushes.Black, new XRect(278, 73, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx4.DrawString(academic_Year.ToString(), font8, XBrushes.Black, new XRect(278, 73, pp.Width, pp.Height), XStringFormats.TopLeft);*/


                }

                else if (org_Id == 213 && (class_Id >= 8 && class_Id <= 10))
                {
                    PdfPage pp = PDFNewDoc.AddPage(PDFDoc.Pages[0]);
                    PdfPage pp1 = PDFNewDoc.AddPage(PDFDoc.Pages[1]);
                    PdfPage pp2 = PDFNewDoc.AddPage(PDFDoc.Pages[2]);
                    PdfPage pp3 = PDFNewDoc.AddPage(PDFDoc.Pages[3]);
                    PdfPage pp4 = PDFNewDoc.AddPage(PDFDoc.Pages[4]);



                    XGraphics gfx = XGraphics.FromPdfPage(pp);
                    XGraphics gfx1 = XGraphics.FromPdfPage(pp1);
                    XGraphics gfx2 = XGraphics.FromPdfPage(pp2);
                    XGraphics gfx3 = XGraphics.FromPdfPage(pp3);
                    XGraphics gfx4 = XGraphics.FromPdfPage(pp4);

                    XFont font9 = new XFont("Arial", 40, XFontStyle.Bold);
                    XFont font10 = new XFont("Arial", 16, XFontStyle.Bold);

                    gfx.DrawString(academic_Year.ToString(), font10, XBrushes.Black, new XRect(287, 72, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(249, 100, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(stud_First_Name.ToString() + " " + stud_Middle_Name.ToString() + " " + stud_Last_Name.ToString(), font, XBrushes.Black, new XRect(140, 160, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(parent_name.ToString() + " " + parentLastname.ToString(), font, XBrushes.Black, new XRect(150, 191, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString(father_Mobile.ToString(), font, XBrushes.Black, new XRect(482, 192, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(date_Of_Birth.ToString(), font, XBrushes.Black, new XRect(310, 189, pp.Width, pp.Height), XStringFormats.TopLeft);


                    if (gender == "Male")
                    {
                        gfx.DrawString(".", font9, XBrushes.Black, new XRect(123, 163, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    else
                    {
                        gfx.DrawString(".", font9, XBrushes.Black, new XRect(171, 163, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }

                    gfx.DrawString(place_Of_Birth.ToString(), font, XBrushes.Black, new XRect(480, 189, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString("districtofbirth", font, XBrushes.Black, new XRect(125, 218, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(districtofbirth.ToString(), font, XBrushes.Black, new XRect(125, 216, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString("Talukaofbirth", font, XBrushes.Black, new XRect(305, 218, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(state_Name.ToString(), font, XBrushes.Black, new XRect(305, 216, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(nationality.ToString(), font, XBrushes.Black, new XRect(470, 216, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString("Country", font, XBrushes.Black, new XRect(130, 247, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(country_Name.ToString(), font, XBrushes.Black, new XRect(130, 245, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString("mother_Tongue", font, XBrushes.Black, new XRect(310, 247, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Tongue.ToString(), font, XBrushes.Black, new XRect(310, 245, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(religion.ToString(), font, XBrushes.Black, new XRect(470, 245, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(religionother.ToString(), font, XBrushes.Black, new XRect(130, 303, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(caste.ToString(), font, XBrushes.Black, new XRect(100, 273, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString("subcaste", font, XBrushes.Black, new XRect(317, 275, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(subcaste.ToString(), font, XBrushes.Black, new XRect(317, 273, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx.DrawString(Category.ToString(), font, XBrushes.Black, new XRect(130, 300, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx.DrawString(Grade.ToString(), font, XBrushes.Black, new XRect(425, 300, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(PreviouslyLWS.ToString(), font, XBrushes.Black, new XRect(190, 361, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString(aadhar_no.ToString(), font, XBrushes.Black, new XRect(190, 361, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString("UDISENumber", font, XBrushes.Black, new XRect(460, 361, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString(UDISENumber.ToString(), font, XBrushes.Black, new XRect(460, 361, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(previousdateofapp.ToString(), font, XBrushes.Black, new XRect(370, 360, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(aadhar_no.ToString(), font, XBrushes.Black, new XRect(190, 330, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(UDISENumber.ToString(), font, XBrushes.Black, new XRect(495, 330, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(Academicstrengths.ToString(), font, XBrushes.Black, new XRect(60, 385, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString("Otherinterests.ToString()", font, XBrushes.Black, new XRect(300, 445, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(Otherinterests.ToString(), font, XBrushes.Black, new XRect(300, 414, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(mother_Name.ToString(), font, XBrushes.Black, new XRect(130, 469, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Edu_Qualification.ToString(), font, XBrushes.Black, new XRect(440, 468, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString("9344089944", font, XBrushes.Black, new XRect(130, 536, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Mobile.ToString(), font, XBrushes.Black, new XRect(130, 495, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Email.ToString(), font, XBrushes.Black, new XRect(300, 495, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(mother_Occupation.ToString(), font, XBrushes.Black, new XRect(115, 520, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(motherdesignation.ToString(), font, XBrushes.Black, new XRect(340, 520, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(motherorganization.ToString(), font, XBrushes.Black, new XRect(140, 545, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Org_sector, font, XBrushes.Black, new XRect(440, 545, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(father_Name.ToString(), font, XBrushes.Black, new XRect(130, 571, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Edu_Qualification.ToString(), font, XBrushes.Black, new XRect(440, 571, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString("9344089944", font, XBrushes.Black, new XRect(130, 638, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Mobile.ToString(), font, XBrushes.Black, new XRect(130, 596, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Email.ToString(), font, XBrushes.Black, new XRect(300, 596, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(father_Occupation.ToString(), font, XBrushes.Black, new XRect(115, 622, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(fatherdesigntion.ToString(), font, XBrushes.Black, new XRect(340, 622, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(fatherorganization.ToString(), font, XBrushes.Black, new XRect(145, 648, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Org_sector, font, XBrushes.Black, new XRect(440, 648, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(Address1.ToString() + " " + Pincode.ToString(), font, XBrushes.Black, new XRect(60, 696, pp.Width, pp.Height), XStringFormats.TopLeft);



                    //page 2 

                    // gfx1.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(239, 100, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(academic_Year.ToString(), font10, XBrushes.Black, new XRect(287, 72, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx1.DrawString("name_of_preschool.ToString()", font, XBrushes.Black, new XRect(153, 160, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(name_of_preschool.ToString(), font, XBrushes.Black, new XRect(153, 160, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(92, 163, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(138, 163, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(181, 163, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString(".", font9, XBrushes.Black, new XRect(214, 163, pp.Width, pp.Height), XStringFormats.TopLeft);

                    if (board_of_Study == 1) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(92, 163, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    else if (board_of_Study == 3) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(138, 163, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    else if (board_of_Study == 2) { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(181, 163, pp.Width, pp.Height), XStringFormats.TopLeft); }
                    else { gfx1.DrawString(".", font9, XBrushes.Black, new XRect(214, 163, pp.Width, pp.Height), XStringFormats.TopLeft); }

                    //gfx1.DrawString("grade_in_Previous.ToString()", font, XBrushes.Black, new XRect(153, 216, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(grade_in_Previous.ToString(), font, XBrushes.Black, new XRect(153, 216, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString("2020", font, XBrushes.Black, new XRect(460, 216, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(year_of_completion.ToString(), font, XBrushes.Black, new XRect(460, 216, pp.Width, pp.Height), XStringFormats.TopLeft);

                    if (inter_Math == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(209, 289, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    if (math == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(344, 289, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }

                    if (hindi == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(57, 333, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    if (spanish == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(209, 333, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }

                    if (economics == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(57, 377, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    if (business_studies == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(209, 377, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    if (psychology == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(344, 377, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    if (accounts == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(450, 377, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }

                    if (evm == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(57, 421, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    if (physics == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(209, 421, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    if (chemistry == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(344, 421, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    if (biology == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(450, 421, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }

                    if (computer_science == 1)
                    {
                        gfx1.DrawString(".", font9, XBrushes.Black, new XRect(57, 465, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }

                    gfx1.DrawString(guarduian_Name.ToString(), font, XBrushes.Black, new XRect(153, 558, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(relationship.ToString(), font, XBrushes.Black, new XRect(143, 585, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString("9582447788", font, XBrushes.Black, new XRect(460, 227, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_Mobile.ToString(), font, XBrushes.Black, new XRect(460, 585, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(guardian_type.ToString(), font, XBrushes.Black, new XRect(130, 614, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_address.ToString(), font, XBrushes.Black, new XRect(59, 641, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx1.DrawString("guarduian_Name1.ToString()", font, XBrushes.Black, new XRect(150, 308, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guarduian_Name1.ToString(), font, XBrushes.Black, new XRect(150, 664, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString("relationship1.ToString()", font, XBrushes.Black, new XRect(220, 336, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(relationship1.ToString(), font, XBrushes.Black, new XRect(220, 692, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString("guardian_Mobile1.ToString()", font, XBrushes.Black, new XRect(460, 336, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_Mobile1.ToString(), font, XBrushes.Black, new XRect(460, 692, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx1.DrawString("guardian_type1.ToString()", font, XBrushes.Black, new XRect(130, 364, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_type1.ToString(), font, XBrushes.Black, new XRect(130, 720, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString("guardian_address1.ToString()", font, XBrushes.Black, new XRect(59, 390, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_address1.ToString(), font, XBrushes.Black, new XRect(59, 745, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx2.DrawString(student_height.ToString(), font, XBrushes.Black, new XRect(100, 153, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(student_weight.ToString(), font, XBrushes.Black, new XRect(280, 153, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(Blood_group_name.ToString(), font, XBrushes.Black, new XRect(480, 153, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx2.DrawString(ImmunizationReceived.ToString(), font, XBrushes.Black, new XRect(153, 181, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(Receivinganylongmedi.ToString(), font, XBrushes.Black, new XRect(249, 211, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(allergies.ToString(), font, XBrushes.Black, new XRect(60, 261, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx1.DrawString(Doesyourchildsuffer.ToString(), font, XBrushes.Black, new XRect(60, 526, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx2.DrawString(Doesyourchildhaveanylearningdisability.ToString(), font, XBrushes.Black, new XRect(60, 321, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(Hasyourchildreceivedanyspecialinstruction.ToString(), font, XBrushes.Black, new XRect(60, 373, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(Hasyourchildbeenhospitalizedforanyillness.ToString(), font, XBrushes.Black, new XRect(60, 430, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(Anyotherhealthspecificinformation.ToString(), font, XBrushes.Black, new XRect(60, 484, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(father_Name.ToString(), font, XBrushes.Black, new XRect(120, 604, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(father_Mobile.ToString(), font, XBrushes.Black, new XRect(130, 634, pp.Width, pp.Height), XStringFormats.TopLeft);


                    //page 3

                    //  gfx2.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(239, 100, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(academic_Year.ToString(), font10, XBrushes.Black, new XRect(287, 72, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx3.DrawString(siblinginfo.ToString(), font, XBrushes.Black, new XRect(200, 161, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx3.DrawString(siblingname1.ToString(), font, XBrushes.Black, new XRect(140, 188, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(SiblingGrade.ToString(), font, XBrushes.Black, new XRect(412, 188, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx3.DrawString(DoyouaddanothSibling.ToString(), font, XBrushes.Black, new XRect(240, 216, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(anyothrefestuLodhaSchool.ToString(), font, XBrushes.Black, new XRect(310, 245, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx3.DrawString(SecondSiblingName.ToString(), font, XBrushes.Black, new XRect(150, 272, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(SecondSiblingGrade.ToString(), font, XBrushes.Black, new XRect(433, 272, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx3.DrawString(ReferenceName.ToString(), font, XBrushes.Black, new XRect(130, 300, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(ReferenceGrade.ToString(), font, XBrushes.Black, new XRect(410, 300, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx3.DrawString(Referencerelationwithstudent.ToString(), font, XBrushes.Black, new XRect(190, 328, pp.Width, pp.Height), XStringFormats.TopLeft);


                    //gfx3.DrawString(applicant_name.ToString(), font, XBrushes.Black, new XRect(130, 490, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx3.DrawString(parent_name.ToString(), font, XBrushes.Black, new XRect(125, 517, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx3.DrawString(declarationPlace.ToString(), font, XBrushes.Black, new XRect(140, 545, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx3.DrawString(declarationDate.ToString(), font, XBrushes.Black, new XRect(270, 525, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx3.DrawString(academic_Year.ToString(), font10, XBrushes.Black, new XRect(287, 72, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx4.DrawString(academic_Year.ToString(), font10, XBrushes.Black, new XRect(287, 72, pp.Width, pp.Height), XStringFormats.TopLeft);


                }

                else
                {
                    PdfPage pp = PDFNewDoc.AddPage(PDFDoc.Pages[0]);
                    PdfPage pp1 = PDFNewDoc.AddPage(PDFDoc.Pages[1]);
                    PdfPage pp2 = PDFNewDoc.AddPage(PDFDoc.Pages[2]);
                    PdfPage pp3 = PDFNewDoc.AddPage(PDFDoc.Pages[3]);
                    PdfPage pp4 = PDFNewDoc.AddPage(PDFDoc.Pages[4]);



                    XGraphics gfx = XGraphics.FromPdfPage(pp);
                    XGraphics gfx1 = XGraphics.FromPdfPage(pp1);
                    XGraphics gfx2 = XGraphics.FromPdfPage(pp2);
                    XGraphics gfx3 = XGraphics.FromPdfPage(pp3);
                    XGraphics gfx4 = XGraphics.FromPdfPage(pp4);

                    XFont font9 = new XFont("Arial", 40, XFontStyle.Bold);
                    XFont font10 = new XFont("Arial", 16, XFontStyle.Bold);

                    gfx.DrawString(academic_Year.ToString(), font10, XBrushes.Black, new XRect(287, 72, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(249, 100, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(stud_First_Name.ToString() + " " + stud_Middle_Name.ToString() + " " + stud_Last_Name.ToString(), font, XBrushes.Black, new XRect(140, 163, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(parent_name.ToString() + " " + parentLastname.ToString(), font, XBrushes.Black, new XRect(150, 191, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString(father_Mobile.ToString(), font, XBrushes.Black, new XRect(482, 192, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(date_Of_Birth.ToString(), font, XBrushes.Black, new XRect(310, 192, pp.Width, pp.Height), XStringFormats.TopLeft);


                    if (gender == "Male")
                    {
                        gfx.DrawString(".", font9, XBrushes.Black, new XRect(123, 166, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }
                    else
                    {
                        gfx.DrawString(".", font9, XBrushes.Black, new XRect(171, 166, pp.Width, pp.Height), XStringFormats.TopLeft);
                    }

                    gfx.DrawString(place_Of_Birth.ToString(), font, XBrushes.Black, new XRect(480, 192, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString("districtofbirth", font, XBrushes.Black, new XRect(125, 218, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(districtofbirth.ToString(), font, XBrushes.Black, new XRect(125, 218, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString("Talukaofbirth", font, XBrushes.Black, new XRect(305, 218, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(state_Name.ToString(), font, XBrushes.Black, new XRect(305, 218, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(nationality.ToString(), font, XBrushes.Black, new XRect(470, 218, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString("Country", font, XBrushes.Black, new XRect(130, 247, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(country_Name.ToString(), font, XBrushes.Black, new XRect(130, 247, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString("mother_Tongue", font, XBrushes.Black, new XRect(310, 247, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Tongue.ToString(), font, XBrushes.Black, new XRect(310, 247, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(religion.ToString(), font, XBrushes.Black, new XRect(470, 247, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(religionother.ToString(), font, XBrushes.Black, new XRect(130, 303, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(caste.ToString(), font, XBrushes.Black, new XRect(100, 275, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString("subcaste", font, XBrushes.Black, new XRect(317, 275, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(subcaste.ToString(), font, XBrushes.Black, new XRect(317, 275, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx.DrawString(Category.ToString(), font, XBrushes.Black, new XRect(130, 302, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx.DrawString(Grade.ToString(), font, XBrushes.Black, new XRect(425, 302, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString(PreviouslyLWS.ToString(), font, XBrushes.Black, new XRect(190, 361, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(aadhar_no.ToString(), font, XBrushes.Black, new XRect(190, 361, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx.DrawString("UDISENumber", font, XBrushes.Black, new XRect(460, 361, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(UDISENumber.ToString(), font, XBrushes.Black, new XRect(460, 361, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //if (previousdateofapp == "01-01-0001" || previousdateofapp == "01-01-1900")
                    //{
                    //    gfx.DrawString(" ", font, XBrushes.Black, new XRect(370, 360, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //}
                    //else
                    //{
                    //    gfx.DrawString(previousdateofapp.ToString(), font, XBrushes.Black, new XRect(370, 360, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //}
                    //   gfx.DrawString(previousdateofapp.ToString(), font, XBrushes.Black, new XRect(370, 360, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(PresentSchoolAttended.ToString(), font6, XBrushes.Black, new XRect(160, 333, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(PresentSchoolGrade.ToString(), font, XBrushes.Black, new XRect(495, 333, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(Academicstrengths.ToString(), font, XBrushes.Black, new XRect(60, 415, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString("Otherinterests.ToString()", font, XBrushes.Black, new XRect(300, 445, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(Otherinterests.ToString(), font, XBrushes.Black, new XRect(300, 445, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(mother_Name.ToString(), font, XBrushes.Black, new XRect(130, 510, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Edu_Qualification.ToString(), font, XBrushes.Black, new XRect(410, 509, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString("9344089944", font, XBrushes.Black, new XRect(130, 536, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Mobile.ToString(), font, XBrushes.Black, new XRect(130, 536, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Email.ToString(), font, XBrushes.Black, new XRect(300, 536, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(mother_Occupation.ToString(), font, XBrushes.Black, new XRect(115, 561, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(motherdesignation.ToString(), font, XBrushes.Black, new XRect(340, 561, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(motherorganization.ToString(), font, XBrushes.Black, new XRect(140, 586, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(mother_Org_sector, font, XBrushes.Black, new XRect(440, 586, pp.Width, pp.Height), XStringFormats.TopLeft);



                    gfx.DrawString(father_Name.ToString(), font, XBrushes.Black, new XRect(130, 612, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Edu_Qualification.ToString(), font, XBrushes.Black, new XRect(440, 612, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx.DrawString("9344089944", font, XBrushes.Black, new XRect(130, 638, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Mobile.ToString(), font, XBrushes.Black, new XRect(130, 638, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Email.ToString(), font, XBrushes.Black, new XRect(300, 638, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(father_Occupation.ToString(), font, XBrushes.Black, new XRect(115, 663, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(fatherdesigntion.ToString(), font, XBrushes.Black, new XRect(340, 663, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(fatherorganization.ToString(), font, XBrushes.Black, new XRect(145, 690, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx.DrawString(father_Org_sector, font, XBrushes.Black, new XRect(440, 690, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx.DrawString(Address1.ToString() + " " + Pincode.ToString(), font, XBrushes.Black, new XRect(60, 737, pp.Width, pp.Height), XStringFormats.TopLeft);



                    //page 2 

                    // gfx1.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(239, 100, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(academic_Year.ToString(), font10, XBrushes.Black, new XRect(287, 72, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx1.DrawString(guarduian_Name.ToString(), font, XBrushes.Black, new XRect(153, 200, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(relationship.ToString(), font, XBrushes.Black, new XRect(143, 227, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString("9582447788", font, XBrushes.Black, new XRect(460, 227, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_Mobile.ToString(), font, XBrushes.Black, new XRect(460, 227, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(guardian_type.ToString(), font, XBrushes.Black, new XRect(130, 256, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_address.ToString(), font, XBrushes.Black, new XRect(59, 281, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx1.DrawString("guarduian_Name1.ToString()", font, XBrushes.Black, new XRect(150, 308, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guarduian_Name1.ToString(), font, XBrushes.Black, new XRect(150, 308, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString("relationship1.ToString()", font, XBrushes.Black, new XRect(220, 336, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(relationship1.ToString(), font, XBrushes.Black, new XRect(220, 336, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString("guardian_Mobile1.ToString()", font, XBrushes.Black, new XRect(460, 336, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_Mobile1.ToString(), font, XBrushes.Black, new XRect(460, 336, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx1.DrawString("guardian_type1.ToString()", font, XBrushes.Black, new XRect(130, 364, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_type1.ToString(), font, XBrushes.Black, new XRect(130, 364, pp.Width, pp.Height), XStringFormats.TopLeft);
                    //gfx1.DrawString("guardian_address1.ToString()", font, XBrushes.Black, new XRect(59, 390, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(guardian_address1.ToString(), font, XBrushes.Black, new XRect(59, 390, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx1.DrawString(student_height.ToString(), font, XBrushes.Black, new XRect(100, 434, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(student_weight.ToString(), font, XBrushes.Black, new XRect(280, 434, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(Blood_group_name.ToString(), font, XBrushes.Black, new XRect(480, 434, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx1.DrawString(ImmunizationReceived.ToString(), font, XBrushes.Black, new XRect(153, 461, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(Receivinganylongmedi.ToString(), font, XBrushes.Black, new XRect(249, 488, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(allergies.ToString(), font, XBrushes.Black, new XRect(60, 540, pp.Width, pp.Height), XStringFormats.TopLeft);

                    //gfx1.DrawString(Doesyourchildsuffer.ToString(), font, XBrushes.Black, new XRect(60, 526, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx1.DrawString(Doesyourchildhaveanylearningdisability.ToString(), font, XBrushes.Black, new XRect(60, 594, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx1.DrawString(Hasyourchildreceivedanyspecialinstruction.ToString(), font, XBrushes.Black, new XRect(60, 645, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(Hasyourchildbeenhospitalizedforanyillness.ToString(), font, XBrushes.Black, new XRect(60, 698, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx1.DrawString(Anyotherhealthspecificinformation.ToString(), font, XBrushes.Black, new XRect(60, 751, pp.Width, pp.Height), XStringFormats.TopLeft);


                    //page 3

                    //  gfx2.DrawString(application_Form_No.ToString(), font5, XBrushes.Black, new XRect(239, 100, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(academic_Year.ToString(), font10, XBrushes.Black, new XRect(287, 72, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(siblinginfo.ToString(), font, XBrushes.Black, new XRect(200, 163, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(siblingname1.ToString(), font, XBrushes.Black, new XRect(140, 190, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(SiblingGrade.ToString(), font, XBrushes.Black, new XRect(412, 190, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(DoyouaddanothSibling.ToString(), font, XBrushes.Black, new XRect(240, 218, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(anyothrefestuLodhaSchool.ToString(), font, XBrushes.Black, new XRect(310, 247, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(SecondSiblingName.ToString(), font, XBrushes.Black, new XRect(150, 274, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(SecondSiblingGrade.ToString(), font, XBrushes.Black, new XRect(433, 274, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(ReferenceName.ToString(), font, XBrushes.Black, new XRect(130, 302, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(ReferenceGrade.ToString(), font, XBrushes.Black, new XRect(410, 302, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(Referencerelationwithstudent.ToString(), font, XBrushes.Black, new XRect(190, 330, pp.Width, pp.Height), XStringFormats.TopLeft);


                    gfx2.DrawString(applicant_name.ToString(), font, XBrushes.Black, new XRect(130, 490, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(parent_name.ToString(), font, XBrushes.Black, new XRect(125, 517, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx2.DrawString(declarationPlace.ToString(), font, XBrushes.Black, new XRect(140, 545, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx2.DrawString(declarationDate.ToString(), font, XBrushes.Black, new XRect(420, 545, pp.Width, pp.Height), XStringFormats.TopLeft);

                    gfx3.DrawString(academic_Year.ToString(), font10, XBrushes.Black, new XRect(287, 72, pp.Width, pp.Height), XStringFormats.TopLeft);
                    gfx4.DrawString(academic_Year.ToString(), font10, XBrushes.Black, new XRect(287, 72, pp.Width, pp.Height), XStringFormats.TopLeft);


                }

                PDFNewDoc.Save(savefilefor2 + filename);

                e1.ResponseStatus = "True";
                e1.ResponseCode = "0";
                e1.ResponseMessage = "ReportCardGenerated";
                return e1;



            }
            catch (Exception e) { error = e.ToString(); }

            e1.ResponseStatus = "False";
            e1.ResponseCode = "1012";
            e1.ResponseMessage = error;
            return e1;


        }

    }
}
