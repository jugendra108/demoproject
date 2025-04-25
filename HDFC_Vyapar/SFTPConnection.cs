using HDFC_SFTP_FileProcessor;
using log4net;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WinSCP;
using System.Data.OleDb;
//using System.Data.OracleClient;
//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using System.Reflection;
using DataTable = System.Data.DataTable;
using Dapper;
using System.Linq;
using System.Globalization;
using System.Threading;
using ExcelDataReader;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace SFTP
{
    public class SFTPConnection
    {
        static ConnectionFactory con = new ConnectionFactory();
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string host = Convert.ToString(ConfigurationManager.AppSettings["Host"].ToString());
        private int port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());
        private string userName = HDFC_SFTP_FileProcessor.EncryptDecrypt.DecryptQueryString(Convert.ToString(ConfigurationManager.AppSettings["UserName"].ToString()));
        private string secureString = HDFC_SFTP_FileProcessor.EncryptDecrypt.DecryptQueryString(Convert.ToString(ConfigurationManager.AppSettings["SecureString"].ToString()));
     
        static FileList objFile = new FileList();

        //SMTP email appconfig for QA
        private string smtpServer = Convert.ToString(ConfigurationManager.AppSettings["smtpServer"].ToString());
        private int smtpport = Convert.ToInt32(ConfigurationManager.AppSettings["smtpport"].ToString());
        private string SMTPusername = Convert.ToString(ConfigurationManager.AppSettings["SMTPusername"].ToString());
        private string SMTPpassword = Convert.ToString(ConfigurationManager.AppSettings["SMTPpassword"].ToString());
        private string senderEmail = Convert.ToString(ConfigurationManager.AppSettings["senderEmail"].ToString());
        private string SSlEnableTrue_False = ConfigurationManager.AppSettings["SSlEnableTrue_False"].ToString();
        private int MailDelayTime = Convert.ToInt32(ConfigurationManager.AppSettings["MailDelayTime"].ToString());
        private Boolean enablesslstatus;

        public async Task<List<SFTPDataInfo>> GetDataFromSFTP()
        {
            try
            {
                HelperFunctions.LogMessage(log, enumMessageLevel.info, "DIGI Code Session closed for " + host + " at " + DateTime.Now.ToString("h:mm:ss tt"));
                return null;
            }
            catch (Exception ex)
            {
                HelperFunctions.LogMessage(log, enumMessageLevel.error, "Error Occured in GetDataFromSFTP() :: " + ex.Message);
                throw ex;
            }

        }


    }
}
