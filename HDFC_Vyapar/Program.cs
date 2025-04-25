
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFTP
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            HelperFunctions.LogMessage(log, enumMessageLevel.info, "DIGI Code Service Progress in On heelo jugendra hai");
            MainWorker mainWorker = new MainWorker();
            HelperFunctions.LogMessage(log, enumMessageLevel.info, "Main worker Object created");
            try
            {
                mainWorker.UpdateFromSFTP();
            }
            catch (Exception ex)
            {
                HelperFunctions.LogMessage(log, enumMessageLevel.error, "Exception message:" + ex.Message + ". \nInner exception: "
                    + ex.InnerException + ". \nStack trace:" + ex.StackTrace);
            }
            finally
            {
                
                 //
            }
            Console.ReadKey();
        }
    }
}
