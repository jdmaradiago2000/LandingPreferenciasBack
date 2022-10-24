using WebApi_LandingPreferencias.App_Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Data.SqlClient;

namespace WebApi_LandingPreferencias.DataAccess
{
    public class Conect
    {
        #region constantes
        private const string IRIS_LOG_EXCEPCIONES = "Nab_SP_Global_Log_Excepciones_Registro";
        #endregion
        #region Campos
        private static EncryptionHelper encripter = new EncryptionHelper();
        private SqlConnection conexion;
        private Dictionary<string, object> parameters = new Dictionary<string, object>();
        private Dictionary<string, SqlParameter> parametersOutput = new Dictionary<string, SqlParameter>();
        public Double timeQuery;
        public int numRows = 0;
        public int numColumns = 0;
        public bool error = false;
        public HttpContext context;
        private HttpContext httpContext;
        #endregion

        #region Propiedades
        public string CommandQuery { get; set; }
        #endregion

        #region Metodos
        public string GetCommand()
        {
            StringBuilder command = new StringBuilder();
            command.Append(this.CommandQuery);
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Append(parameter.Key + ": '" + parameter.Value + "'");
                    command.Append(", ");
                }
            }
            if (parametersOutput != null)
            {
                foreach (SqlParameter parameter in parametersOutput.Values)
                {
                    command.Append(parameter.ParameterName + ": '" + parameter.Value + "'");
                    command.Append(", ");
                }
            }
            return command.ToString();
        }


        public void AddParameters(string key, object value)
        {
            if (!parameters.ContainsKey(key))
            {
                if (value == null)
                {
                    value = DBNull.Value;
                }
                else if (value.GetType().ToString() == "System.String")
                {
                    string valueTrim = value.ToString().Trim();
                    value = (valueTrim == "") ? (DBNull.Value) : (value);
                }

                parameters.Add(key, value);
            }
        }

        public void AddParametersOutPut(string key, object value, SqlDbType type)
        {
            if (!parametersOutput.ContainsKey(key))
            {
                SqlParameter param = new SqlParameter(key, value);
                param.SqlDbType = type;
                param.Direction = ParameterDirection.Output;
                parametersOutput.Add(key, param);
            }
        }

        public void AddParametersOutPut(string key, object value, SqlDbType type, int longVarchar)
        {
            if (!parametersOutput.ContainsKey(key))
            {
                SqlParameter param = new SqlParameter(key, value);
                param.SqlDbType = type;
                param.Direction = ParameterDirection.Output;
                param.Size = longVarchar;
                parametersOutput.Add(key, param);
            }
        }

        public void AddParametersInputOutPut(string key, object value, SqlDbType type)
        {
            if (!parametersOutput.ContainsKey(key))
            {
                SqlParameter param = new SqlParameter(key, value);
                param.SqlDbType = type;
                param.Direction = ParameterDirection.InputOutput;
                parametersOutput.Add(key, param);
            }
        }

        public object GetValueParameterOut(string keyParameter)
        {
            object value = parametersOutput[keyParameter].Value;
            return value;
        }

        public void ExecTransac(Boolean IsStoreProcedure = true)
        {
            SqlDataReader data;
            try
            {
                SqlCommand command = new SqlCommand(CommandQuery, conexion);
                //verifica si es procedimiento almacenado
                if (IsStoreProcedure)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //Asigna parametros de consulta o de procedimiento
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> parameter in parameters)
                        {
                            string typeParam = parameter.Value.GetType().ToString();

                            if (typeParam == "System.String")
                            {
                                SqlParameter param = new SqlParameter("@" + parameter.Key, SqlDbType.VarChar);
                                param.Value = parameter.Value;
                                command.Parameters.Add(param);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@" + parameter.Key, parameter.Value);
                            }
                        }
                    }
                    //Asigna parametros Output
                    if (parametersOutput != null)
                    {
                        foreach (SqlParameter parameter in parametersOutput.Values)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                }
                //Time out de conexión a 0
                command.CommandTimeout = 0;
                //Abre conexión
                conexion.Open();
                //Ejecuta sentencia
                data = command.ExecuteReader();

                if (parametersOutput != null)
                {
                    foreach (SqlParameter item in parametersOutput.Values)
                    {
                        try
                        {
                            item.Value = command.Parameters[item.ParameterName].Value;
                        }
                        catch (ArgumentNullException ex) {
                            error = true;
                        }
                        catch (ArgumentException ex)
                        {
                            error = true;
                        }
                    }
                }

                if (data.RecordsAffected > 0)
                {
                    numRows = data.RecordsAffected;
                }
            }
            catch (SqlException ex)
            {
                conexion.Close();
                if (CommandQuery != IRIS_LOG_EXCEPCIONES)
                {
                    this.Reset();
                    error = true;
                    this.Exception_Log(ex);
                }
            }
            finally
            {
                conexion.Close();
            }
        }

        public DataTable GetDataTable(bool IsStoreProcedure = true)
        {
            DataTable dataTable = new DataTable();

            SqlCommand command = new SqlCommand(CommandQuery, conexion);
            try
            {
                //verifica si es procedimiento almacenado
                if (IsStoreProcedure)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //Asigna parametros de consulta o de procedimiento
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> parameter in parameters)
                        {
                            if (parameter.Value.GetType().ToString() == "System.String")
                            {
                                SqlParameter param = new SqlParameter("@" + parameter.Key, SqlDbType.VarChar);
                                param.Value = parameter.Value;
                                command.Parameters.Add(param);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@" + parameter.Key, parameter.Value);
                            }
                        }
                    }
                    if (parametersOutput != null)
                    {
                        foreach (SqlParameter parameter in parametersOutput.Values)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                }
                //Time out de conexión a 0
                command.CommandTimeout = 0;
                //Abre conexión
                conexion.Open();
                //Ejecuta sentencia
                SqlDataAdapter adapterData = new SqlDataAdapter();
                adapterData.SelectCommand = command;
                dataTable.Locale = CultureInfo.InvariantCulture;
                adapterData.Fill(dataTable);
                adapterData.Dispose();
            }
            catch (ArgumentNullException ex)
            {
                error = true;
                conexion.Close();
                if (CommandQuery != IRIS_LOG_EXCEPCIONES)
                {
                    this.Reset();
                    this.Exception_Log(ex);
                }
            }
            catch (ArgumentException ex)
            {
                error = true;
                conexion.Close();
                if (CommandQuery != IRIS_LOG_EXCEPCIONES)
                {
                    this.Reset();
                    this.Exception_Log(ex);
                }
            }
            catch (SqlException ex)
            {
                error = true;
                conexion.Close();
                if (CommandQuery != IRIS_LOG_EXCEPCIONES)
                {
                    this.Reset();
                    this.Exception_Log(ex);
                }
            }
            catch (OutOfMemoryException ex)
            {
                error = true;
                conexion.Close();
                if (CommandQuery != IRIS_LOG_EXCEPCIONES)
                {
                    this.Reset();
                    this.Exception_Log(ex);
                }
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
            }
            return dataTable;
        }

        public DataTable GetDataTable(string DataTablename, bool IsStoreProcedure = true)
        {
            DataTable dataTable = new DataTable(DataTablename);


            SqlCommand command = new SqlCommand(CommandQuery, conexion);
            try
            {
                //verifica si es procedimiento almacenado
                if (IsStoreProcedure)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //Asigna parametros de consulta o de procedimiento
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> parameter in parameters)
                        {
                            if (parameter.Value.GetType().ToString() == "System.String")
                            {
                                SqlParameter param = new SqlParameter("@" + parameter.Key, SqlDbType.VarChar);
                                param.Value = parameter.Value;
                                command.Parameters.Add(param);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@" + parameter.Key, parameter.Value);
                            }
                        }
                    }
                    if (parametersOutput != null)
                    {
                        foreach (SqlParameter parameter in parametersOutput.Values)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                }
                //Time out de conexión a 0
                command.CommandTimeout = 0;
                //Abre conexión
                conexion.Open();
                //Ejecuta sentencia
                SqlDataAdapter adapterData = new SqlDataAdapter();
                adapterData.SelectCommand = command;
                dataTable.Locale = CultureInfo.InvariantCulture;
                adapterData.Fill(dataTable);
                adapterData.Dispose();
            }
            catch (ArgumentNullException ex)
            {
                error = true;
                conexion.Close();
                if (CommandQuery != IRIS_LOG_EXCEPCIONES)
                {
                    this.Reset();
                    this.Exception_Log(ex);
                }
            }
            catch (ArgumentException ex)
            {
                error = true;
                conexion.Close();
                if (CommandQuery != IRIS_LOG_EXCEPCIONES)
                {
                    this.Reset();
                    this.Exception_Log(ex);
                }
            }
            catch (SqlException ex)
            {
                error = true;
                conexion.Close();
                if (CommandQuery != IRIS_LOG_EXCEPCIONES)
                {
                    this.Reset();
                    this.Exception_Log(ex);
                }
            }
            catch (OutOfMemoryException ex)
            {
                error = true;
                conexion.Close();
                if (CommandQuery != IRIS_LOG_EXCEPCIONES)
                {
                    this.Reset();
                    this.Exception_Log(ex);
                }
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
            }
            return dataTable;
        }

        public void Exception_Log(Exception ex)
        {
            try
            {
                SqlDataReader data;
                string usrID = context.User.Identity.Name;
                int codError = -1;
               /* if (ex is HttpException)
                {
                    HttpException checkException = (HttpException)ex;
                    codError = checkException.GetHttpCode();
                }*/

                string mensaje = ex.Message.ToString();
                AddParameters("host", context.Request.Host);
                AddParameters("url_path", context.Request.Host.Value);
                AddParameters("path", context.Request.Path);
                AddParameters("linea", 0);
                AddParameters("usr_Login", usrID);
                AddParameters("ip", "");
                AddParameters("equipo", "");
                AddParameters("script", this.CommandQuery);
                AddParameters("cod_excepcion", codError);
                AddParameters("mensaje", mensaje);
                AddParameters("notas", "");
                AddParameters("fec_evento", DateTime.Now);
                SqlCommand command = new SqlCommand(IRIS_LOG_EXCEPCIONES, conexion);
                command.CommandType = CommandType.StoredProcedure;
                foreach (KeyValuePair<string, object> item in parameters)
                {
                    if (item.Value.GetType().ToString() == "System.String")
                    {
                        SqlParameter param = new SqlParameter("@" + item.Key, SqlDbType.VarChar);
                        param.Value = item.Value;
                        command.Parameters.Add(param);
                    }
                    else
                    {
                        command.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }
                //Time out de conexión a 0
                command.CommandTimeout = 0;
                //Abre conexión
                conexion.Open();
                //Ejecuta sentencia
                data = command.ExecuteReader();
                data.Close();

            }
            catch (Exception exLog)
            {
               // new Logica.Log().saveLog(exLog.ToString());
               //throw;
            }
            finally
            {
                this.Reset();
                conexion.Close();
                conexion.Dispose();
            }
        }

        public void Reset()
        {
            if (parameters.Count > 0) { this.parameters.Clear(); }
            if (this.parametersOutput != null) { this.parametersOutput.Clear(); }
            this.numRows = 0;
            this.numColumns = 0;
            this.timeQuery = 0;
            this.parametersOutput = null;
        }

        #endregion

        #region Constructores
        public Conect(HttpContext context, IConfiguration config)
        {
                bool modeDeveloper = config.GetValue<string>("ModeDeveloper") == "true";
            bool modeTest = config.GetValue<string>("ModeTest") == "true";

            conexion = new SqlConnection(modeDeveloper ? encripter.Decrypt(ConnectionSources.GAMORADeveloper) : (modeTest ? encripter.Decrypt(ConnectionSources.GAMORATest) : encripter.Decrypt(ConnectionSources.GAMORA)));

            this.context = context;
        }

        public Conect()
        {
        }

        public static SqlConnection GetConexion(Conect conect)
        {
            return conect.conexion;
        }

        public string getCRMConnection()
        {
            string oradb = "data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.203.100.132)(PORT=1527))(CONNECT_DATA=(SERVICE_NAME=crmdb)));" +
               "User Id=GAMORAET;Password=Tele2021*!Ene#; Connection Timeout=60;";

            return oradb;
        }

        /* public Conect(string conexionName)
         {
             string conexionString = (context.GetGlobalResourceObject("SourcesConection", modeTest ? conexionName + "_Test" : conexionName) ?? "").ToString();
             if (!String.IsNullOrEmpty(conexionString))
             {
                 this.conexion = new SqlConnection(conexionString);
             }
             else
             {
                 throw new System.InvalidOperationException("El nombre de conexión " + conexionName + " no es válido.");
             }
         }*/

        public Conect(string ConectionString, bool ModeTest = true)
        {
            this.conexion = new SqlConnection(ConectionString);
        }

        public Conect(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }
        #endregion
    }
}