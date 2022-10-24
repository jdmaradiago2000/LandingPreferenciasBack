using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_LandingPreferencias.DataAccess
{
    public class Inf_General
    {

        public bool validateDocument(string numberDocument, out Models.IdsUser idsUser)
        {
            string valor;

            idsUser = new Models.IdsUser();
            OracleConnection conn = new OracleConnection();
            try
            {
                conn.ConnectionString = new Conect().getCRMConnection();
                conn.Open();
                string sql = " SELECT s.payment_type,cer.id_type,cer.id_number,c.cust_id,c.cust_name,c.field4 id_segmento_cust " +
                    " , ac.acct_id,ac.acct_code,ac.acct_name " +
                    " ,s.subs_id,s.subs_name,s.service_number,s.ex_field19 id_segmento_sub " +
                    " , s.offering_id, dnd.promotional_flag, dnd.institutional_flag, dnd.campaigns_flag,dnd.create_time " +
                    " FROM cust_user.inf_customer c " +
                    " INNER JOIN party_user.inf_party_certificate cer ON cer.party_id = c.party_id " +
                    " INNER JOIN party_user.inf_account ac ON ac.party_id = cer.party_id " +
                    " INNER JOIN subs_user.inf_subscriber s ON s.owner_party_role_id = c.cust_id " +
                    " LEFT JOIN subs_user.inf_subscriber_dnd_ctz dnd on dnd.subs_id = s.subs_id " +
                    " INNER JOIN PARTY_USER.INF_PAYMENT_RELATION pr ON ac.acct_id = pr.acct_id AND s.subs_id = pr.pay_obj_id " +
                    " AND ac.acct_code = pr.acct_code " +
                    " WHERE s.payment_type = 1 and cer.ID_NUMBER = '" + numberDocument + "' " +
                    " GROUP BY s.payment_type,cer.id_type,cer.id_number,c.cust_id,c.cust_name,c.field4 " +
                    " ,s.subs_id,ac.acct_id,ac.acct_code,ac.acct_name " +
                    " ,s.subs_name,s.service_number,s.ex_field19, s.offering_id, dnd.promotional_flag, dnd.institutional_flag, dnd.campaigns_flag, dnd.create_time ";

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 120;

                OracleDataReader dr = cmd.ExecuteReader(); // C#

                DataTable dataTable = new DataTable();
                dataTable.Load(dr);

                if (dataTable != null && dataTable.Rows.Count >0)
                {
                    idsUser.Cust_id = Int64.Parse(dataTable.Rows[0][3].ToString());
                    idsUser.Acct_id = Int64.Parse(dataTable.Rows[0]["acct_id"].ToString());
                    idsUser.Estado = true;
                    //valor = dataTable.Rows[0][3].ToString();
                }
                else
                {
                    return false;
                }

                return idsUser.Estado;
            }
            catch (OracleException e)
            {
                return false;
            }
            catch (FormatException e)
            {
                return false;
            }
            finally
            {
                conn.Close();   // C#
                conn.Dispose(); // C#
            }
        }


        public bool validateMobileNumber(string mobileNumber, string custID)
        {
            OracleConnection conn = new OracleConnection();
            int networkType = 0;

            try
            {
                conn.ConnectionString = new Conect().getCRMConnection();
                conn.Open();
                string sql = " SELECT s.payment_type,cer.id_type,cer.id_number,c.cust_id,c.cust_name,c.field4 id_segmento_cust " +
                 " , ac.acct_id,ac.acct_code,ac.acct_name " +
                 " ,s.subs_id,s.subs_name,s.service_number,s.ex_field19 id_segmento_sub " +
                 " , s.offering_id, dnd.promotional_flag, dnd.institutional_flag, dnd.campaigns_flag,dnd.create_time " +
                 " FROM cust_user.inf_customer c " +
                 " INNER JOIN party_user.inf_party_certificate cer ON cer.party_id = c.party_id " +
                 " INNER JOIN party_user.inf_account ac ON ac.party_id = cer.party_id " +
                 " INNER JOIN subs_user.inf_subscriber s ON s.owner_party_role_id = c.cust_id " +
                 " LEFT JOIN subs_user.inf_subscriber_dnd_ctz dnd on dnd.subs_id = s.subs_id " +
                 " INNER JOIN PARTY_USER.INF_PAYMENT_RELATION pr ON ac.acct_id = pr.acct_id AND s.subs_id = pr.pay_obj_id " +
                 " AND ac.acct_code = pr.acct_code " +
                 " WHERE s.payment_type = 1 and c.cust_id = '" + custID + "' and s.service_number = '" + mobileNumber + "' " +
                 " GROUP BY s.payment_type,cer.id_type,cer.id_number,c.cust_id,c.cust_name,c.field4 " +
                 " ,s.subs_id,ac.acct_id,ac.acct_code,ac.acct_name " +
                 " ,s.subs_name,s.service_number,s.ex_field19, s.offering_id, dnd.promotional_flag, dnd.institutional_flag, dnd.campaigns_flag, dnd.create_time ";


                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 120;

                OracleDataReader dr = cmd.ExecuteReader(); // C#

                DataTable dataTable = new DataTable();
                dataTable.Load(dr);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    /*networkType = dataTable.Rows[0].Field<int>(1);

                    if (networkType == 1)
                    {
                        return getMobileNumber(mobileNumber);
                    }
                    else
                    {
                        return getFixedNumber(mobileNumber);
                    }*/
                    return true;
                }

                return true;
            }
            catch (OracleException e)
            {
                Console.WriteLine("Error" + e);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e);

            }
            finally
            {
                conn.Close();   // C#
                conn.Dispose(); // C#
            }
            return false;
        }

        public bool getMobileNumber(string mobileNumber)
        {
            OracleConnection conn = new OracleConnection();
            try
            {
                conn.ConnectionString = new Conect().getCRMConnection();
                conn.Open();
                string sql = " SELECT ID_NUMBER, CUST_ID " +
                    " FROM BC_SUB_IDEN  " +
                    " WHERE SUB_IDEN_TYPE = 1 AND SUB_IDENTITY = " + mobileNumber;

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 120;

                OracleDataReader dr = cmd.ExecuteReader(); // C#

                DataTable dataTable = new DataTable();
                dataTable.Load(dr);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch (OracleException e)
            {
                Console.WriteLine("Error" + e);

            }
            finally
            {
                conn.Close();   // C#
                conn.Dispose(); // C#
            }
            return false;
        }

        public bool getFixedNumber(string mobileNumber)
        {
            OracleConnection conn = new OracleConnection();
            try
            {
                conn.ConnectionString = new Conect().getCRMConnection();
                conn.Open();
                string sql = " SELECT ID_NUMBER, CUST_ID " +
                    " FROM BC_CONTACT  " +
                    " WHERE MOBILE_PHONE = " + mobileNumber;

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 120;

                OracleDataReader dr = cmd.ExecuteReader(); // C#

                DataTable dataTable = new DataTable();
                dataTable.Load(dr);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch (OracleException e)
            {
                Console.WriteLine("Error" + e);

            }
            finally
            {
                conn.Close();   // C#
                conn.Dispose(); // C#
            }
            return false;
        }
    }
}
