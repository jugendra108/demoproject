
using HDFC_SFTP_FileProcessor;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace HDFC_SFTP_FileProcessor
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        public IDbConnection GetConnection
        {
            get
            {
                string[] SplitconnList = connectionString.Split(';');
                string UserId = SplitconnList[1];
                int UserIdstartIndex = 8;
                int UserIdendIndex = UserId.Length - 8;
                string EnUserId = UserId.Substring(UserIdstartIndex, UserIdendIndex);
                string DEUserId = EncryptDecrypt.DecryptQueryString(EnUserId);

                string Password = SplitconnList[2];
                int PassStartIndex = 9;
                int PassEndIndex = Password.Length - 9;
                string EnPassword = Password.Substring(PassStartIndex, PassEndIndex);
                string DEPassword = EncryptDecrypt.DecryptQueryString(EnPassword);

                string ServiceConnString = SplitconnList[0].ToString() + ";" + "User ID=" + DEUserId + ";" + "Password=" + DEPassword + ";" + SplitconnList[3].ToString() + ";" + SplitconnList[4].ToString() + ";" + "";


                var conn = new OracleConnection(ServiceConnString);
                return conn;

            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ConnectionFactory() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}
