using log4net;
using System;

namespace SFTP
{
    public class HelperFunctions
    {
        public static void LogMessage(ILog log, enumMessageLevel messageLevel, String message)
        {
            string timestamp = DateTime.Now.ToString() + " >> ";
            if (messageLevel == enumMessageLevel.debug)
            {
                log.Debug(message);
                Console.WriteLine(timestamp + "DEBUG >> " + message);
            }
            else if (messageLevel == enumMessageLevel.info)
            {
                log.Info(message);
                Console.WriteLine(timestamp + "INFO >> " + message);
            }
            else if (messageLevel == enumMessageLevel.warn)
            {
                log.Warn(message);
                Console.WriteLine(timestamp + "WARN >> " + message);
            }
            else if (messageLevel == enumMessageLevel.error)
            {
                log.Error(message);
                Console.WriteLine(timestamp + "ERROR >> " + message);
            }
        }
    }

    public enum enumMessageLevel
    {
        debug,
        info,
        warn,
        error
    }
}
