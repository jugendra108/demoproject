using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SFTP
{
    public class SFTPConfig
    {
        public string host { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int port { get; set; }
        public string fileLocation { get; set; }

    }

    public class SFTPDataInfo
    {
        public DateTime FileSentDateTime { get; set; }
        public string Contents { get; set; }
        public string FileName { get; set; }
    }
    public class FileUploadresult
    {
        public string Error { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ResponseData> Data { get; set; }
    }

    public class ResponseData
    {
        public string RowNo { get; set; }
        public string CaseNumber { get; set; }
        public string CaseType { get; set; }
        public string TID { get; set; }
        public string MID { get; set; }
        public string StateOffice { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class ResponseDataVyapar
    {
        public string Fields { get; set; }
        //public string RowNo { get; set; }
        public string MID { get; set; }
        public string TID { get; set; }
        public string TIDType { get; set; }
        public string DBAName { get; set; }
        public string SerialNumber { get; set; }
        public string MachineName { get; set; }
        public string TerminalIndicatorFlag { get; set; }
        public string VendorName { get; set; }
        public string Source { get; set; }
        public string Medium { get; set; }
        public string CallCategory { get; set; }
        public string CallType { get; set; }
        public string CallSub_Type { get; set; }
        public string Description { get; set; }
        public string ContactPersonMobileNo { get; set; }
        public string AssignToRole { get; set; }
        public string UDF_1 { get; set; }
        public string UDF_2 { get; set; }
        public string UDF_3 { get; set; }
        public string UDF_4 { get; set; }
        public string UDF_5 { get; set; }
        //public string Comments { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class FileList
    {
        public List<string> TIDEnrolledQueue { get; set; }
        public List<string> TIDDeactivateQueue { get; set; }
        public List<string> TIDLifecycleUpdatesQueue { get; set; }

        public List<string> TIDBQRQueue { get; set; }
        public List<string> TIDReactivationQueue { get; set; }
        public FileList()
        {
            TIDEnrolledQueue = new List<string>();
            TIDDeactivateQueue = new List<string>();
            TIDLifecycleUpdatesQueue = new List<string>();
            TIDBQRQueue = new List<string>();
            TIDReactivationQueue = new List<string>();
        }
    }

    public class DataMigration
    {
        public int v_largetableid { get; set; }
        public int Id { get; set; }
        public string ERRORMESSAGE { get; set; }
        public string CASENUMBER { get; set; }
        public int CASETYPE1 { get; set; }
        public int TID { get; set; }
        public int MID { get; set; }
        public string STATEOFFICE { get; set; }
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public string FileName { get; set; }
        public string Fileextension { get; set; }
        public string UploadFilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public int count { get; set; }
    }

    public class Constant
    {
        public const string CULTURE_SPECIFIC_JS = "CultureSpecificJSFormats";
        public const string CURRENT_LANGUAGE = "CurrentLanguage";



        public const string HashValue = "hashValue";
        public const string Password = "password";
        public const string Md5Salt = "T9`C$M*8fuCAv=8#$aZ'RC2xT)E:[#";



        public const string Login = "Login";
        public const string ChangePassword = "ChangePassword";
    }

    public class DataMigrationBAU
    {
        public int v_largetableid { get; set; }

    }

    public class ResponseBQR
    {

        public string TerminalNo { get; set; }
        public string Mecode { get; set; }
        public string mVISAId { get; set; }
        public string RegistrationMobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string MobileNo3 { get; set; }
        public string EmailId { get; set; }
        public string MerchantName { get; set; }
        public string MerchantAddress { get; set; }
        public string MerchantCity { get; set; }
        public string MerchantPINNumber { get; set; }
        public string MPAN { get; set; }
        public string MerchantCategoryCode { get; set; }
        public string MerchantDBAName { get; set; }
        public string MerchantContactPerson { get; set; }
        public string MerchantContactpersonphone { get; set; }
        public string MerchantCurrencyCode { get; set; }
        public string CanRefundFlag { get; set; }
        public string CanAcceptTipFlag { get; set; }
        public string CanAcceptSurcharge { get; set; }
        public string AggregatorURL { get; set; }
        public string RUPAYPAN { get; set; }
        public string IFSCCODE { get; set; }
        public string AccountNumber { get; set; }
        public string VPA { get; set; }
        public string Uploadstatus { get; set; }



    }
    public class ResponseData_Reactivation
    {
        public string MECode { get; set; }
        public string MEName { get; set; }
        public string TID { get; set; }
        public string Reason { get; set; }
        public string IMPDate { get; set; }
        public string UserCode { get; set; }
        public string AuthorizedBy { get; set; }
        public string AuthorizedOn { get; set; }
        public string Status { get; set; }
        public string ChangeRefNo { get; set; }
        public string ApprovalRemark { get; set; }
        public string Uploadstatus { get; set; }
    }
    public class EventNotificationModel
    {
        public int EventId { get; set; }
        public int EntityId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }        
        public DateTime CreatedOn { get; set; }

    }
    public enum EnumNotificationEventType
    {
        VyaparRequestFileEventId = 198,
        VyaparResponseFileEventId = 199,
        VyaparRequestTemplateId = 834,
        VyaparResponseTemplateId = 835,
    }
    public class SmtpEmailModel
    {
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
