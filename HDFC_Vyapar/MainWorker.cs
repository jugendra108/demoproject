using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFTP
{
    public class MainWorker
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async void UpdateFromSFTP()
        {
            HelperFunctions.LogMessage(log, enumMessageLevel.info, "In UpdateFromSFTP() Method");
            try
            {

                SFTPConnection _operation = new SFTPConnection();
                List<SFTPDataInfo> _data = new List<SFTPDataInfo>();
                _data = await _operation.GetDataFromSFTP();
            }
            catch (Exception ex)
            {
                HelperFunctions.LogMessage(log, enumMessageLevel.error, "Error Occured in Calling UpdateFromSFTP()" + ex.Message + ". \nInner exception"
                                + ex.InnerException + ". \nStack trace: " + ex.StackTrace);
            }
            finally
            {

            }
        }
    }
}
