﻿using Newtonsoft.Json;
using System.Net.Http;
using MoipCSharp.Models;
using MoipCSharp.Exception;
using System.Threading.Tasks;
using System.Net;
using System;

namespace MoipCSharp.Controllers
{
    public partial class ContasClassicasController : BaseController
    {
        #region Singleton Pattern
        private static readonly object syncObject = new object();
        private static ContasClassicasController instance = null;
        internal static ContasClassicasController Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new ContasClassicasController();
                    }
                }
                return instance;
            }
        }
        #endregion Singleton Pattern

        public async Task<HttpStatusCode> ContaExiste(string emailOuDocumento)
        {
            HttpResponseMessage response = await ClientInstance.GetAsync($"v2/accounts/exists?{(emailOuDocumento.Contains("@")?"email": "tax_document")}={emailOuDocumento}");
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                MoipException.APIException moipException = MoipException.DeserializeObject(content);
                throw new MoipException(moipException, "HTTP Response Not Success", content, (int)response.StatusCode);
            }
            return response.StatusCode;
        }
        public async Task<ContaClassicaResponse> Criar(ContaClassicaRequest body)
        {
            HttpResponseMessage response = await ClientInstance.PostAsync("v2/accounts", null);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                MoipException.APIException moipException = MoipException.DeserializeObject(content);
                throw new MoipException(moipException, "HTTP Response Not Success", content, (int)response.StatusCode);
            }
            try
            {
                return JsonConvert.DeserializeObject<ContaClassicaResponse>(await response.Content.ReadAsStringAsync());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ContaClassicaResponse> Consultar(string account_id)
        {
            HttpResponseMessage response = await ClientInstance.GetAsync($"v2/accounts/{account_id}");
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                MoipException.APIException moipException = MoipException.DeserializeObject(content);
                throw new MoipException(moipException, "HTTP Response Not Success", content, (int)response.StatusCode);
            }
            try
            {
                return JsonConvert.DeserializeObject<ContaClassicaResponse>(await response.Content.ReadAsStringAsync());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> SolicitarPermissoesAcessoUsuario(string response_type, string client_id, string redirect_uri, string scope)
        {
            HttpClient httpClient = ClientInstance;
            httpClient.BaseAddress = new Uri("https://connect-sandbox.moip.com.br/");
            HttpResponseMessage response = await httpClient.GetAsync($"oauth/authorize?response_type={response_type}&client_id={client_id}&redirect_uri={redirect_uri}&scope={scope}");
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                MoipException.APIException moipException = MoipException.DeserializeObject(content);
                throw new MoipException(moipException, "HTTP Response Not Success", content, (int)response.StatusCode);
            }
            try
            {
                return await response.Content.ReadAsStringAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<AccessTokenResponse> GerarAccessToken(string client_id, string client_secret, string redirect_uri, string grant_type, string code)
        {
            HttpClient httpClient = ClientInstance;
            httpClient.BaseAddress = new Uri("https://connect-sandbox.moip.com.br/");
            HttpResponseMessage response = await httpClient.PostAsync($"oauth/token?client_id={client_id}&client_secret={client_secret}&redirect_uri={redirect_uri}&grant_type{grant_type}&code={code}", null);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                MoipException.APIException moipException = MoipException.DeserializeObject(content);
                throw new MoipException(moipException, "HTTP Response Not Success", content, (int)response.StatusCode);
            }
            try
            {
                return JsonConvert.DeserializeObject<AccessTokenResponse>(await response.Content.ReadAsStringAsync());
            }
            catch (System.Exception ex)
            {
                throw new ArgumentException("Error message: " + ex.Message);
            }
        }
        public async Task<AccessTokenResponse> AtualizarAccessToken(string grant_type, string refresh_token)
        {
            HttpClient httpClient = ClientInstance;
            httpClient.BaseAddress = new Uri("https://connect-sandbox.moip.com.br/");
            HttpResponseMessage response = await httpClient.PostAsync($"oauth/token?grant_type={grant_type}&refresh_token={refresh_token}", null);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                MoipException.APIException moipException = MoipException.DeserializeObject(content);
                throw new MoipException(moipException, "HTTP Response Not Success", content, (int)response.StatusCode);
            }
            try
            {
                return JsonConvert.DeserializeObject<AccessTokenResponse>(await response.Content.ReadAsStringAsync());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ChavePublicaContaMoipResponse> ObterChavePublica()
        {
            HttpResponseMessage response = await ClientInstance.GetAsync("v2/keys");
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                MoipException.APIException moipException = MoipException.DeserializeObject(content);
                throw new MoipException(moipException, "HTTP Response Not Success", content, (int)response.StatusCode);
            }
            try
            {
                return JsonConvert.DeserializeObject<ChavePublicaContaMoipResponse>(await response.Content.ReadAsStringAsync());
            }
            catch (System.Exception ex)
            {
                throw new ArgumentException("Error message: " + ex.Message);
            }
        }
    }
}
