using LandingPreferenciasBack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApi_LandingPreferencias.Models;

namespace WebApi_LandingPreferencias.DataAccess
{
    public class Questions
    {
        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;

        public Questions(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion


        public List<Question> GetQuestions()
        {
            List<Question> listaPreguntas = new List<Question>();
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Administrar_Preguntas_Landing_Preferencias";
            conect.AddParameters("TRANSACCION", 106);

            DataTable data = conect.GetDataTable();

            Nullable<int> dependenciaaux;
            foreach (DataRow row in data.Rows)
            {
                if (!string.IsNullOrEmpty(row["DEPENDENCIA"].ToString()))
                {
                    dependenciaaux = int.Parse(row["DEPENDENCIA"].ToString());
                }
                else
                {
                    dependenciaaux = null;
                }

                listaPreguntas.Add(new Question
                {
                    codigo_pregunta = int.Parse(row["codigo_pregunta"].ToString()),
                    etapa = int.Parse(row["etapa"].ToString()),
                    estado_pregunta = Convert.ToBoolean(row["estado_pregunta"].ToString()),
                    orden = int.Parse(row["orden"].ToString()),
                    descripcion = row["descripcion"].ToString(),
                    tipo_pregunta = row["tipo_pregunta"].ToString(),
                    dependencia = int.Parse(row["CODIGO_PREGUNTA"].ToString()),
                    codigo_respuesta = int.Parse(row["codigo_respuesta"].ToString()),
                    tipo_respuesta = row["tipo_respuesta"].ToString(),
                    respuesta_1 = row["respuesta_1"].ToString(),
                    respuesta_2 = row["respuesta_2"].ToString(),
                    respuesta_3 = row["respuesta_3"].ToString(),
                    respuesta_4 = row["respuesta_4"].ToString(),
                    respuesta_5 = row["respuesta_5"].ToString(),
                });
            }

            return listaPreguntas;
        }

        public List<Question> GetQuestionStageOriginal(int etapa)
        {
            List<Question> listaPreguntas = new List<Question>();
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Administrar_Preguntas_Landing_Preferencias";
            conect.AddParameters("TRANSACCION", 104);
            conect.AddParameters("ETAPA_", etapa);

            DataTable data = conect.GetDataTable();

            Nullable<int> dependenciaaux;
            foreach (DataRow row in data.Rows)
            {
                if (!string.IsNullOrEmpty(row["DEPENDENCIA"].ToString()))
                {
                    dependenciaaux = int.Parse(row["DEPENDENCIA"].ToString());
                }
                else
                {
                    dependenciaaux = null;
                }

                listaPreguntas.Add(new Question
                {
                    codigo_pregunta = int.Parse(row["CODIGO_PREGUNTA"].ToString()),
                    etapa = int.Parse(row["etapa"].ToString()),
                    estado_pregunta = Convert.ToBoolean(row["estado_pregunta"].ToString()),
                    orden = int.Parse(row["orden"].ToString()),
                    descripcion = row["descripcion"].ToString(),
                    tipo_pregunta = row["tipo_pregunta"].ToString(),
                    dependencia = int.Parse(row["CODIGO_PREGUNTA"].ToString()),
                    codigo_respuesta = int.Parse(row["codigo_respuesta"].ToString()),
                    tipo_respuesta = row["tipo_respuesta"].ToString(),
                    respuesta_1 = row["respuesta_1"].ToString(),
                    respuesta_2 = row["respuesta_2"].ToString(),
                    respuesta_3 = row["respuesta_3"].ToString(),
                    respuesta_4 = row["respuesta_4"].ToString(),
                    respuesta_5 = row["respuesta_5"].ToString(),
                });
            }

            return listaPreguntas;
        }


        public List<RespuestasPreguntas> GetQuestionStage(int etapa)
        {
            List<RespuestasPreguntas> listaPreguntas = new List<RespuestasPreguntas>();
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Administrar_Respuestas_Landing_Preferencias";
            conect.AddParameters("TRANSACCION", 10000);
            conect.AddParameters("ID", etapa);

            DataTable data = conect.GetDataTable();

            Nullable<int> dependenciaaux;

            if(data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    if (!string.IsNullOrEmpty(row["DEPENDENCIA"].ToString()))
                    {
                        dependenciaaux = int.Parse(row["DEPENDENCIA"].ToString());
                    }
                    else
                    {
                        dependenciaaux = null;
                    }

                    listaPreguntas.Add(new RespuestasPreguntas
                    {
                        id = 0,
                        codigo_pregunta = int.Parse(row["codigo_pregunta"].ToString()),
                        etapa = int.Parse(row["etapa"].ToString()),
                        estado_pregunta = Convert.ToBoolean(row["estado_pregunta"].ToString()),
                        orden = int.Parse(row["orden"].ToString()),
                        descripcion = row["descripcion"].ToString(),
                        tipo_pregunta = row["tipo_pregunta"].ToString(),
                        dependencia = int.Parse(row["codigo_pregunta"].ToString()),
                        codigo_respuesta = int.Parse(row["codigo_respuesta"].ToString()),
                        tipo_respuesta = row["tipo_respuesta"].ToString(),
                        respuesta_1 = row["respuesta_1"].ToString(),
                        respuesta_2 = row["respuesta_2"].ToString(),
                        respuesta_3 = row["respuesta_3"].ToString(),
                        respuesta_4 = row["respuesta_4"].ToString(),
                        respuesta_5 = row["respuesta_5"].ToString(),
                        respuesta_pregunta_1 = row["respuesta_pregunta_1"].ToString(),
                        respuesta_pregunta_2 = row["respuesta_pregunta_2"].ToString(),
                        respuesta_pregunta_3 = row["respuesta_pregunta_3"].ToString(),
                        respuesta_pregunta_4 = row["respuesta_pregunta_4"].ToString(),
                        respuesta_pregunta_5 = row["respuesta_pregunta_5"].ToString(),
                    });
                }

            }
            else
            {
                listaPreguntas.Add(new RespuestasPreguntas
                {
                    id = 0,
                    codigo_pregunta = 0,
                    etapa = 4,
                    estado_pregunta = true,
                    orden = 0,
                    descripcion = "",
                    tipo_pregunta = "",
                    dependencia = 0,
                    codigo_respuesta = 0,
                    tipo_respuesta = "",
                    respuesta_1 = "",
                    respuesta_2 = "",
                    respuesta_3 = "",
                    respuesta_4 = "",
                    respuesta_5 = "",
                    respuesta_pregunta_1 = "",
                    respuesta_pregunta_2 = "",
                    respuesta_pregunta_3 = "",
                    respuesta_pregunta_4 = "",
                    respuesta_pregunta_5 = "",
                });
            }

            return listaPreguntas;
        }


        public List<RespuestasPreguntas> GetQuestionStageUpdated(string dato)
        {
            List<RespuestasPreguntas> listaPreguntas = new List<RespuestasPreguntas>();
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Administrar_Respuestas_Landing_Preferencias";
            conect.AddParameters("TRANSACCION", 10000);
            String[] result = dato.Split("-");
            conect.AddParameters("ID", 0);
            conect.AddParameters("CODIGO_CLIENTE", result[0]);
            conect.AddParameters("CODIGO_CUENTA", result[1]);

            DataTable data = conect.GetDataTable();

            Nullable<int> dependenciaaux;

            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    if (!string.IsNullOrEmpty(row["DEPENDENCIA"].ToString()))
                    {
                        dependenciaaux = int.Parse(row["DEPENDENCIA"].ToString());
                    }
                    else
                    {
                        dependenciaaux = null;
                    }

                    listaPreguntas.Add(new RespuestasPreguntas
                    {
                        id = 0,
                        codigo_pregunta = int.Parse(row["codigo_pregunta"].ToString()),
                        etapa = int.Parse(row["etapa"].ToString()),
                        estado_pregunta = Convert.ToBoolean(row["estado_pregunta"].ToString()),
                        orden = int.Parse(row["orden"].ToString()),
                        descripcion = row["descripcion"].ToString(),
                        tipo_pregunta = row["tipo_pregunta"].ToString(),
                        dependencia = int.Parse(row["codigo_pregunta"].ToString()),
                        codigo_respuesta = int.Parse(row["codigo_respuesta"].ToString()),
                        tipo_respuesta = row["tipo_respuesta"].ToString(),
                        respuesta_1 = row["respuesta_1"].ToString(),
                        respuesta_2 = row["respuesta_2"].ToString(),
                        respuesta_3 = row["respuesta_3"].ToString(),
                        respuesta_4 = row["respuesta_4"].ToString(),
                        respuesta_5 = row["respuesta_5"].ToString(),
                        respuesta_pregunta_1 = row["respuesta_pregunta_1"].ToString(),
                        respuesta_pregunta_2 = row["respuesta_pregunta_2"].ToString(),
                        respuesta_pregunta_3 = row["respuesta_pregunta_3"].ToString(),
                        respuesta_pregunta_4 = row["respuesta_pregunta_4"].ToString(),
                        respuesta_pregunta_5 = row["respuesta_pregunta_5"].ToString(),
                        contadora_anterior = row["contadora_anterior"].ToString(),
                        contadora_siguiente = row["contadora_siguiente"].ToString(),
                    });
                }

            }
            else
            {
                listaPreguntas.Add(new RespuestasPreguntas
                {
                    id = 0,
                    codigo_pregunta = 0,
                    etapa = 4,
                    estado_pregunta = true,
                    orden = 0,
                    descripcion = "",
                    tipo_pregunta = "",
                    dependencia = 0,
                    codigo_respuesta = 0,
                    tipo_respuesta = "",
                    respuesta_1 = "",
                    respuesta_2 = "",
                    respuesta_3 = "",
                    respuesta_4 = "",
                    respuesta_5 = "",
                    respuesta_pregunta_1 = "",
                    respuesta_pregunta_2 = "",
                    respuesta_pregunta_3 = "",
                    respuesta_pregunta_4 = "",
                    respuesta_pregunta_5 = "",
                });
            }

            return listaPreguntas;
        }


        public List<RespuestasPreguntas> GetQuestionPrevious(string datos)
        {
            List<RespuestasPreguntas> listaPreguntas = new List<RespuestasPreguntas>();
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Administrar_Respuestas_Landing_Preferencias";
            conect.AddParameters("TRANSACCION", 10002);
            conect.AddParameters("ID", 0);
            String[] result = datos.Split("-");
            conect.AddParameters("CODIGO_PREGUNTA", result[0]);
            conect.AddParameters("CODIGO_CLIENTE", result[1]);
            conect.AddParameters("CODIGO_CUENTA", result[2]);

            DataTable data = conect.GetDataTable();

            Nullable<int> dependenciaaux;
            foreach (DataRow row in data.Rows)
            {
                if (!string.IsNullOrEmpty(row["DEPENDENCIA"].ToString()))
                {
                    dependenciaaux = int.Parse(row["DEPENDENCIA"].ToString());
                }
                else
                {
                    dependenciaaux = null;
                }

                listaPreguntas.Add(new RespuestasPreguntas
                {
                    id = int.Parse(row["id"].ToString()),
                    codigo_pregunta = int.Parse(row["codigo_pregunta"].ToString()),
                    etapa = int.Parse(row["etapa"].ToString()),
                    estado_pregunta = Convert.ToBoolean(row["estado_pregunta"].ToString()),
                    orden = int.Parse(row["orden"].ToString()),
                    descripcion = row["descripcion"].ToString(),
                    tipo_pregunta = row["tipo_pregunta"].ToString(),
                    dependencia = int.Parse(row["codigo_pregunta"].ToString()),
                    codigo_respuesta = int.Parse(row["codigo_respuesta"].ToString()),
                    tipo_respuesta = row["tipo_respuesta"].ToString(),
                    respuesta_1 = row["respuesta_1"].ToString(),
                    respuesta_2 = row["respuesta_2"].ToString(),
                    respuesta_3 = row["respuesta_3"].ToString(),
                    respuesta_4 = row["respuesta_4"].ToString(),
                    respuesta_5 = row["respuesta_5"].ToString(),
                    respuesta_pregunta_1 = row["respuesta_pregunta_1"].ToString(),
                    respuesta_pregunta_2 = row["respuesta_pregunta_2"].ToString(),
                    respuesta_pregunta_3 = row["respuesta_pregunta_3"].ToString(),
                    respuesta_pregunta_4 = row["respuesta_pregunta_4"].ToString(),
                    respuesta_pregunta_5 = row["respuesta_pregunta_5"].ToString(),
                    contadora_anterior = row["contadora_anterior"].ToString(),
                    contadora_siguiente = row["contadora_siguiente"].ToString(),
                });;
            }

            return listaPreguntas;
        }

        public List<RespuestasPreguntas> GetLastQuestionPrevious(string datos)
        {
            List<RespuestasPreguntas> listaPreguntas = new List<RespuestasPreguntas>();
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Administrar_Respuestas_Landing_Preferencias";
            conect.AddParameters("TRANSACCION", 10001);
            conect.AddParameters("ID", 0);
            String[] result = datos.Split("-");
            conect.AddParameters("CODIGO_PREGUNTA", result[0]);
            conect.AddParameters("CODIGO_CLIENTE", result[1]);
            conect.AddParameters("CODIGO_CUENTA", result[2]);

            DataTable data = conect.GetDataTable();

            Nullable<int> dependenciaaux;
            foreach (DataRow row in data.Rows)
            {
                if (!string.IsNullOrEmpty(row["DEPENDENCIA"].ToString()))
                {
                    dependenciaaux = int.Parse(row["DEPENDENCIA"].ToString());
                }
                else
                {
                    dependenciaaux = null;
                }

                listaPreguntas.Add(new RespuestasPreguntas
                {
                    id = int.Parse(row["id"].ToString()),
                    codigo_pregunta = int.Parse(row["codigo_pregunta"].ToString()),
                    etapa = int.Parse(row["etapa"].ToString()),
                    estado_pregunta = Convert.ToBoolean(row["estado_pregunta"].ToString()),
                    orden = int.Parse(row["orden"].ToString()),
                    descripcion = row["descripcion"].ToString(),
                    tipo_pregunta = row["tipo_pregunta"].ToString(),
                    dependencia = int.Parse(row["codigo_pregunta"].ToString()),
                    codigo_respuesta = int.Parse(row["codigo_respuesta"].ToString()),
                    tipo_respuesta = row["tipo_respuesta"].ToString(),
                    respuesta_1 = row["respuesta_1"].ToString(),
                    respuesta_2 = row["respuesta_2"].ToString(),
                    respuesta_3 = row["respuesta_3"].ToString(),
                    respuesta_4 = row["respuesta_4"].ToString(),
                    respuesta_5 = row["respuesta_5"].ToString(),
                    respuesta_pregunta_1 = row["respuesta_pregunta_1"].ToString(),
                    respuesta_pregunta_2 = row["respuesta_pregunta_2"].ToString(),
                    respuesta_pregunta_3 = row["respuesta_pregunta_3"].ToString(),
                    respuesta_pregunta_4 = row["respuesta_pregunta_4"].ToString(),
                    respuesta_pregunta_5 = row["respuesta_pregunta_5"].ToString(),
                    contadora_anterior = row["contadora_anterior"].ToString(),
                    contadora_siguiente = row["contadora_siguiente"].ToString(),
                }); ;
            }

            return listaPreguntas;
        }
    }
}
