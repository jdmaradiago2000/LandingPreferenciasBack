using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using WebApi_LandingPreferencias.Models;

namespace WebApi_LandingPreferencias.DataAccess
{
    public class DataRespuestas
    {
        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public DataRespuestas(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion

        public bool addRespuestasRegister(Respuestas respuestas)
        {
            if(respuestas.CODIGO_CLIENTE.ToString() != "0")
            {
                Conect conect = new Conect(context, config);
                conect.CommandQuery = "LP_SP_Administrar_Respuestas_Landing_Preferencias";
                conect.AddParameters("TRANSACCION", 1001);
                conect.AddParameters("ID", respuestas.ID);
                conect.AddParameters("CODIGO_CLIENTE", respuestas.CODIGO_CLIENTE);
                conect.AddParameters("CODIGO_CUENTA", respuestas.CODIGO_CUENTA);
                conect.AddParameters("CODIGO_PREGUNTA", respuestas.CODIGO_PREGUNTA);
                conect.AddParameters("CODIGO_RESPUESTA", respuestas.CODIGO_RESPUESTA);
                conect.AddParameters("RESPUESTA_1", respuestas.RESPUESTA_1);
                conect.AddParameters("RESPUESTA_2", respuestas.RESPUESTA_2);
                conect.AddParameters("RESPUESTA_3", respuestas.RESPUESTA_3);
                conect.AddParameters("RESPUESTA_4", respuestas.RESPUESTA_4);
                conect.AddParameters("RESPUESTA_5", respuestas.RESPUESTA_5);
                conect.AddParameters("FECHA_HORA", respuestas.FECHA_HORA);

                DataTable data = conect.GetDataTable();

                if (data != null && data.Rows.Count > 0)
                {
                    if (data.Rows[0].ToString().Contains("ERROR"))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return false;
            }           
        }

        public bool updateRespuestasRegister(Respuestas respuestas)
        {
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Administrar_Respuestas_Landing_Preferencias";
            conect.AddParameters("TRANSACCION", 1002);
            conect.AddParameters("ID", respuestas.ID);
            conect.AddParameters("CODIGO_CLIENTE", respuestas.CODIGO_CLIENTE);
            conect.AddParameters("CODIGO_CUENTA", respuestas.CODIGO_CUENTA);
            conect.AddParameters("CODIGO_PREGUNTA", respuestas.CODIGO_PREGUNTA);
            conect.AddParameters("CODIGO_RESPUESTA", respuestas.CODIGO_RESPUESTA);
            conect.AddParameters("RESPUESTA_1", respuestas.RESPUESTA_1);
            conect.AddParameters("RESPUESTA_2", respuestas.RESPUESTA_2);
            conect.AddParameters("RESPUESTA_3", respuestas.RESPUESTA_3);
            conect.AddParameters("RESPUESTA_4", respuestas.RESPUESTA_4);
            conect.AddParameters("RESPUESTA_5", respuestas.RESPUESTA_5);
            conect.AddParameters("FECHA_HORA", respuestas.FECHA_HORA);

            DataTable data = conect.GetDataTable();

            if (data != null && data.Rows.Count > 0)
            {
                if (data.Rows[0].ToString().Contains("ERROR"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public List<Respuestas> ObtenerRespuestasPorId(int ID)
        {
            List<Respuestas> listaRespuestas = new List<Respuestas>();
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Administrar_Respuestas_Landing_Preferencias";
            conect.AddParameters("TRANSACCION", 101);
            conect.AddParameters("ID", ID);

            DataTable data = conect.GetDataTable();

            foreach (DataRow row in data.Rows)
            {
                listaRespuestas.Add(new Respuestas
                {
                    ID = int.Parse(row["ID"].ToString()),
                    CODIGO_CLIENTE = row["CODIGO_CLIENTE"].ToString(),
                    CODIGO_CUENTA = row["CODIGO_CUENTA"].ToString(),
                    CODIGO_PREGUNTA = int.Parse(row["CODIGO_PREGUNTA"].ToString()),
                    CODIGO_RESPUESTA = int.Parse(row["CODIGO_RESPUESTA"].ToString()),
                    RESPUESTA_1 = row["RESPUESTA_1"].ToString(),
                    RESPUESTA_2 = row["RESPUESTA_2"].ToString(),
                    RESPUESTA_3 = row["RESPUESTA_3"].ToString(),
                    RESPUESTA_4 = row["RESPUESTA_4"].ToString(),
                    RESPUESTA_5 = row["RESPUESTA_5"].ToString(),
                    FECHA_HORA = Convert.ToDateTime(row["FECHA_HORA"].ToString()),
                });
            }

            return listaRespuestas;
        }

        public List<Respuestas> ObtenerRespuestasPorCodigoCliente(string CODIGO_CLIENTE)
        {
            List<Respuestas> listaRespuestas = new List<Respuestas>();
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Administrar_Respuestas_Landing_Preferencias";
            conect.AddParameters("TRANSACCION", 102);
            conect.AddParameters("CODIGO_CLIENTE", CODIGO_CLIENTE);

            DataTable data = conect.GetDataTable();

            foreach (DataRow row in data.Rows)
            {
                listaRespuestas.Add(new Respuestas
                {
                    ID = int.Parse(row["ID"].ToString()),
                    CODIGO_CLIENTE = row["CODIGO_CLIENTE"].ToString(),
                    CODIGO_CUENTA = row["CODIGO_CUENTA"].ToString(),
                    CODIGO_PREGUNTA = int.Parse(row["CODIGO_PREGUNTA"].ToString()),
                    CODIGO_RESPUESTA = int.Parse(row["CODIGO_RESPUESTA"].ToString()),
                    RESPUESTA_1 = row["RESPUESTA_1"].ToString(),
                    RESPUESTA_2 = row["RESPUESTA_2"].ToString(),
                    RESPUESTA_3 = row["RESPUESTA_3"].ToString(),
                    RESPUESTA_4 = row["RESPUESTA_4"].ToString(),
                    RESPUESTA_5 = row["RESPUESTA_5"].ToString(),
                    FECHA_HORA = Convert.ToDateTime(row["FECHA_HORA"].ToString()),
                });
            }

            return listaRespuestas;
        }

        public List<Respuestas> ObtenerRespuestasPorCodigoCliente(string CODIGO_CLIENTE, int ETAPA)
        {
            List<Respuestas> listaRespuestas = new List<Respuestas>();
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Administrar_Respuestas_Landing_Preferencias";
            conect.AddParameters("TRANSACCION", 103);
            conect.AddParameters("CODIGO_CLIENTE", CODIGO_CLIENTE);
            conect.AddParameters("ETAPA", ETAPA);

            DataTable data = conect.GetDataTable();

            foreach (DataRow row in data.Rows)
            {
                listaRespuestas.Add(new Respuestas
                {
                    ID = int.Parse(row["ID"].ToString()),
                    CODIGO_CLIENTE = row["CODIGO_CLIENTE"].ToString(),
                    CODIGO_CUENTA = row["CODIGO_CUENTA"].ToString(),
                    CODIGO_PREGUNTA = int.Parse(row["CODIGO_PREGUNTA"].ToString()),
                    CODIGO_RESPUESTA = int.Parse(row["CODIGO_RESPUESTA"].ToString()),
                    RESPUESTA_1 = row["RESPUESTA_1"].ToString(),
                    RESPUESTA_2 = row["RESPUESTA_2"].ToString(),
                    RESPUESTA_3 = row["RESPUESTA_3"].ToString(),
                    RESPUESTA_4 = row["RESPUESTA_4"].ToString(),
                    RESPUESTA_5 = row["RESPUESTA_5"].ToString(),
                    FECHA_HORA = Convert.ToDateTime(row["FECHA_HORA"].ToString()),
                });
            }

            return listaRespuestas;
        }

        public List<Respuestas> ObtenerRespuestasPorCodigoClienteYCodigoPregunta(string CODIGO_CLIENTE, int CODIGO_PREGUNTA)
        {
            List<Respuestas> listaRespuestas = new List<Respuestas>();
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Administrar_Respuestas_Landing_Preferencias";
            conect.AddParameters("TRANSACCION", 104);
            conect.AddParameters("CODIGO_CLIENTE", CODIGO_CLIENTE);
            conect.AddParameters("CODIGO_PREGUNTA", CODIGO_PREGUNTA);

            DataTable data = conect.GetDataTable();

            foreach (DataRow row in data.Rows)
            {
                listaRespuestas.Add(new Respuestas
                {
                    ID = int.Parse(row["ID"].ToString()),
                    CODIGO_CLIENTE = row["CODIGO_CLIENTE"].ToString(),
                    CODIGO_CUENTA = row["CODIGO_CUENTA"].ToString(),
                    CODIGO_PREGUNTA = int.Parse(row["CODIGO_PREGUNTA"].ToString()),
                    CODIGO_RESPUESTA = int.Parse(row["CODIGO_RESPUESTA"].ToString()),
                    RESPUESTA_1 = row["RESPUESTA_1"].ToString(),
                    RESPUESTA_2 = row["RESPUESTA_2"].ToString(),
                    RESPUESTA_3 = row["RESPUESTA_3"].ToString(),
                    RESPUESTA_4 = row["RESPUESTA_4"].ToString(),
                    RESPUESTA_5 = row["RESPUESTA_5"].ToString(),
                    FECHA_HORA = Convert.ToDateTime(row["FECHA_HORA"].ToString()),
                });
            }

            return listaRespuestas;
        }
    }
}