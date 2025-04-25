using Dapper;
using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using SFTP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDFC_SFTP_FileProcessor
{
    public class SFTPEnrolled
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ConnectionFactory con = new ConnectionFactory();
        public  List<DataMigration> CallMigrationProcess(int requestid,DataMigration dataMigration,int RecProcessCount)
        {
            DataTable dt = new DataTable();            
            List<DataMigration> result = new List<DataMigration>();
            try
            {
                int RecCount = 0;
                do
                {
                    var conn = con.GetConnection;
                    OracleConnection oracleconnection = (OracleConnection)conn;
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("pn_request", requestid);
                    parameters.Add("pn_RecProcessCount", RecProcessCount);
                    parameters.Add("pn_datamigration_id", dataMigration.Id);
                    parameters.Add("pn_entity", dataMigration.EntityId);
                    parameters.Add("pn_error", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 10);        
                    result = oracleconnection.Query<DataMigration>("pkg_hdfc_data_Vyapar_AllTypes.p_migration_RecProcessCount", param: parameters,
                                                               commandTimeout: 0, commandType: CommandType.StoredProcedure).ToList();
                    RecCount = parameters.Get<Int32>("pn_error");

                    HelperFunctions.LogMessage(log, enumMessageLevel.info, "Vyapar PendingRecords->EntityId: " + dataMigration.EntityId + ", Pending Record Founds For Processing: " + RecCount);
                    parameters = new DynamicParameters();
                    parameters.Add("pn_request", requestid);
                    parameters.Add("pn_datamigration_id", dataMigration.Id);
                    parameters.Add("pn_entity", dataMigration.EntityId);
                    parameters.Add("pn_error", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 10);
                    parameters.Add("pv_err_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
                    oracleconnection.Execute("pkg_hdfc_data_Vyapar_AllTypes.p_migration_process", param: parameters,commandTimeout: 0, commandType: CommandType.StoredProcedure);
                    int ErrorNumber = parameters.Get<Int32>("pn_error");
                    string ErrorMessage = parameters.Get<string>("pv_err_msg");
                    HelperFunctions.LogMessage(log, enumMessageLevel.info, "Enrolled CallMigrationProcess->EntityId: " + dataMigration.EntityId + ", ErrorNumber: " + ErrorNumber + ", ErrorMessage: " + ErrorMessage);
                } while (RecCount > 0);

                return result;

            }
            catch (Exception ex)
            {
                HelperFunctions.LogMessage(log, enumMessageLevel.error, "Enrolled CallMigrationProcess->EntityId: " + dataMigration.EntityId + ", Error: " + ex.Message);
                return null;
            }
        }
        public List<ResponseDataVyapar> processdata(int requestid, DataMigration dataMigration)
        {
            HelperFunctions.LogMessage(log, enumMessageLevel.info, "Enrolled ProcessData Parameters::- " + requestid + "::" + dataMigration.Id + "::" + dataMigration.EntityId);
            try
            {
                var connn = con.GetConnection;
            OracleConnection oracleconnectionn = (OracleConnection)connn;
            DynamicParameters parameterss = new DynamicParameters();
            // parameterss.Add("pn_RecProcessCount", RecProcessCount);
            parameterss.Add("pn_id", dataMigration.Id);
            parameterss.Add("pn_entityid", dataMigration.EntityId);
            parameterss.Add("pn_request", requestid);
            // parameterss.Add("v_cursor", direction: ParameterDirection.Output, size: 5215585);
            //parameterss.Add("pn_error", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 10);     
              var   result1 = oracleconnectionn.Query<ResponseDataVyapar>("PKG_Vyapar_MIGRATION_ALLTYPES.P_MIGRATION_PROCESS", param: parameterss,
                                                         commandTimeout: 0, commandType: CommandType.StoredProcedure).ToList();         
            return result1;
            }
            catch (Exception ex)
            {
                HelperFunctions.LogMessage(log, enumMessageLevel.error, "Error in Vyapar UpdateDataMigrationStatus :- " + ex.Message);
                return null;
            }
        }
        public int UpdateDataMigrationStatus(int id, int? count = null)
        {
            try
            {
                var conn = con.GetConnection;
                OracleConnection oracleconnection = (OracleConnection)conn;
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("v_totalCount", count > 0 ? count : null);
                parameters.Add("v_dataMigrationId", id);
                parameters.Add("v_isProcessed", count > 0 ? 1 : -1);
                oracleconnection.Query<DataMigration>("USP_UPDATEDATAMIGRATIONSTATUS_Vyapar", param: parameters,
                                                  commandType: CommandType.StoredProcedure);

                if (count > 0)
                    return 1;
                else
                    return -1;

            }
            catch (Exception ex)
            {
                HelperFunctions.LogMessage(log, enumMessageLevel.error, "Error in Vyapar UpdateDataMigrationStatus :- " + ex.Message);
                return -1;
            }
        }
    }

}

