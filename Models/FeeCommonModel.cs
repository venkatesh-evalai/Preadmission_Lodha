namespace Preadmission_Lodha.Models
{
    public class CrudFeeReceiptModel
    {
        public int org_Id { get; set; }
        public int? transactionId { get; set; }
        public int academic_Id { get; set; }
        public string? msg { get; set; }
        public string? academic_Year { get; set; }
        public string? class_Name { get; set; }
        public int? NewOrOld { get; set; }
        public int? quota_id { get; set; }
        public int? quota_Id { get; set; }
        public int? quota_Id1 { get; set; }
        public int? class_Id { get; set; }
        public int? section_Id { get; set; }
        public string? section_Name { get; set; }
        public int category_Id { get; set; }
        public int subCategory_Id { get; set; }
        public int? install1 { get; set; }
        public int? install2 { get; set; }
        public int? install3 { get; set; }
        public int? install4 { get; set; }
        public int duration_Id { get; set; }
        public int type_Id { get; set; }
        public int structure_Id { get; set; }
        public int? term { get; set; }
        public int student_Id { get; set; }
        public int receipt_Id { get; set; }
        public int month_Id { get; set; }
        public int? install_Id { get; set; }
        public string? orderid { get; set; }
        public int? transId { get; set; }
        public string? category_Name { get; set; }
        public string? StudentName { get; set; }
        public string? subCategory_Name { get; set; }
        public string? duration_Name { get; set; }
        public string? type_Name { get; set; }
        public string? student_Code { get; set; }
        public string? student_Name { get; set; }
        public string? receipt_Code { get; set; }
        public string? receipt_Mode { get; set; }
        public string? cheque_Number { get; set; }
        public DateTime cheque_Date { get; set; }
        public DateTime due_Date { get; set; }
        public string? reference_Code { get; set; }
        public DateTime payment_Date { get; set; }
        public string? bank_Name { get; set; }
        public string? branch_Name { get; set; }
        public Decimal structure_Amount { get; set; }
        public Decimal advance_Amount { get; set; }
        public Decimal credit_Amount { get; set; }
        public Decimal discount_Amount { get; set; }
        public Decimal changedDiscountAmount { get; set; }

        public Decimal receipt_Amount { get; set; }
        public Decimal balance_Amount { get; set; }
        public Decimal payable_Amount { get; set; }
        public DateTime receipt_Date { get; set; }
        public Decimal bal_CreditAmount { get; set; }
        public string? receipt_Remark { get; set; }
        public Decimal fine_Amount { get; set; }
        public Decimal additional_Charge { get; set; }
        public int receipt_Cancel { get; set; }
        public DateTime cancel_Date { get; set; }
        public int? status { get; set; }
        public string? user_Name { get; set; }
        public string? ip_Address { get; set; }
        public string? mode { get; set; }
        public string? dd_Number { get; set; }
        public DateTime dd_Date { get; set; }
        public string? admission_No { get; set; }
        public string? father_Name { get; set; }
        public int amount { get; set; }
        public string? Payment_Status { get; set; }
        public string? mobileNumber { get; set; }
        public string? zipCode { get; set; }
        public string? URLAuth1 { get; set; }
        public string? description { get; set; }
        public string? email { get; set; }
        public string? city { get; set; }
        public Guid order_paymentid { get; set; }
        public DateTime Installment_to_date { get; set; }
        public int? trans_id { get; set; }
        public string? order_id { get; set; }

    }

    public class CrudFeeDiscountModel
    {
        public int? org_Id { get; set; }
        public int? academic_Id { get; set; }
        public string? admission_No { get; set; }
        public int? class_Id { get; set; }
        public int? quota_id { get; set; }
        public int? NewOrOld { get; set; }
        public int? branch_Id { get; set; }
        public int? receipt { get; set; }
        public string? class_Name { get; set; }
        public string? section_Name { get; set; }
        public int? section_Id { get; set; }
        public int? student_Id { get; set; }
        public int? category_Id { get; set; }
        public int? subCategory_Id { get; set; }
        public int? supplementry_SubCategory { get; set; }
        public DateTime due_Date { get; set; }
        public int? duration_Id { get; set; }
        public int? type_Id { get; set; }
        public int? structure_Id { get; set; }
        public int? discount_Id { get; set; }
        public int? supplementry_Id { get; set; }
        public int? orgDiscountAmount { get; set; }
        public int? installmentHide { get; set; }
        public int? install_Id { get; set; }

        public string? category_Name { get; set; }
        public string? subCategory_Name { get; set; }
        public string? duration_Name { get; set; }
        public string? type_Name { get; set; }
        public string? student_Name { get; set; }
        public string? SupplementrySubCategory_Name { get; set; }
        public string? supplementry_Reason { get; set; }
        public Decimal structure_Amount { get; set; }
        public Decimal receipt_Amount { get; set; }
        public Decimal balance_Amount { get; set; }
        public Decimal supplementry_Amount { get; set; }
        public int? discount_Type { get; set; }
        public Decimal discount_Amount { get; set; }
        public Decimal changedDiscountAmount { get; set; }
        public DateTime discount_Date { get; set; }
        public DateTime created_Date { get; set; }
        public string? discount_Reason { get; set; }
        public int? status { get; set; }
        public int? month_Id { get; set; }
        public string? month_Name { get; set; }
        public decimal amount { get; set; }
        public decimal discount { get; set; }
        public string? user_Name { get; set; }
        public string? ip_Address { get; set; }
        public string? mode { get; set; }
        public int? mode_hide { get; set; }
    }

    public class getFeeReceiptCodeModel
    {
        public int receipt_Code { get; set; }
        public int? view_Id { get; set; }
        public int? quota_id { get; set; }
    }

    public class crudAccountsPostingModel
    {
        public int? org_Id { get; set; }
        public int? transCode { get; set; }
        public int? accountsCode { get; set; }
        public int? student_Id { get; set; }
        public string? transType { get; set; }
        public string? receipt_Code { get; set; }
        public Decimal credit_Amount { get; set; }
        public Decimal debit_Amount { get; set; }
        public DateTime receipt_Date { get; set; }
        public int? typeId { get; set; }//new
        public int? academicId { get; set; }//new
        public int? directOrIndirect { get; set; }//new

    }

    public class crudAccountsPostingModel1
    {
        public int? org_Id { get; set; }
        public int? transCode { get; set; }
        public int? accountsCode { get; set; }
        public int? student_Id { get; set; }
        public string? transType { get; set; }
        public string? receipt_Code { get; set; }
        public Decimal credit_Amount { get; set; }
        public Decimal debit_Amount { get; set; }
        public DateTime receipt_Date { get; set; }
        public int? typeId { get; set; }//new
        public int? academicId { get; set; }//new
        public int? directOrIndirect { get; set; }//new
        public int? structure_Id { get; set; }//new
        public int? class_Id { get; set; }//new

    }

}
