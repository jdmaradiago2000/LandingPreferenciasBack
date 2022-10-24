using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_LandingPreferencias.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace WebApi_LandingPreferencias.DataAccess
{
    public class DataToken
    {
        public async Task<ResponseGenerateToken> generarToken(RequesGenetateToken token)
        {

            try
            {
                var httpClient = new HttpClient();


                httpClient.BaseAddress = new Uri("http://localhost:8080/");
                httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                //HTTP POST

                string json = JsonConvert.SerializeObject(token, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var postTask = await httpClient.PostAsync("token/generarTokenParametric", stringContent);

                if (postTask.IsSuccessStatusCode)
                {
                    var contents = await postTask.Content.ReadAsStringAsync();
                    ResponseGenerateToken responseService = JsonConvert.DeserializeObject<ResponseGenerateToken>(contents);
                    return responseService;
                }
                return null;                
            }
            catch (Exception ex)
            {
                return null;
            }
        }




        public async Task<Boolean> validarToken(RequesValidateToken token)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://localhost:8080/");
                httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                

                string json = JsonConvert.SerializeObject(token, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var postTask = await httpClient.PostAsync("token/validarTokenParametric", stringContent);

                if (postTask.IsSuccessStatusCode)
                {
                    var contents = await postTask.Content.ReadAsStringAsync();


                    if (bool.Parse(contents))
                    {
                        return true;
                    }
                    
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<Boolean> desactivarTokens(DesactiveToken token)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://localhost:8080/");
                httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);


                string json = JsonConvert.SerializeObject(token, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var postTask = await httpClient.PostAsync("token/desactivarToken", stringContent);

                if (postTask.IsSuccessStatusCode)
                {
                    var contents = await postTask.Content.ReadAsStringAsync();

                    if (bool.Parse(contents))
                    {
                        return true;
                    }

                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }






        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }




    }
}
