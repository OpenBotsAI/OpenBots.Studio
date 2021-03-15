﻿using OpenBots.NetCore.Core.Server.User;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace OpenBots.NetCore.Core.Server.API_Methods
{
    public class AuthMethods
    {
        public static RestClient GetAuthToken()
        {
            var settings = EnvironmentSettings.GetAgentSettings();

            string agentId = settings["AgentId"];
            string serverURL = settings["OpenBotsServerUrl"];

            if (string.IsNullOrEmpty(agentId))
                throw new Exception("Agent is not connected");

            string username = new RegistryManager().AgentUsername;
            string password = new RegistryManager().AgentPassword;

            if (username == null || password == null)
                throw new Exception("Agent credentials not found in registry");

            if (string.IsNullOrEmpty(serverURL))
                throw new Exception("Server URL not found");

            var client = new RestClient(serverURL);
            client.UserAgent = "";

            var request = new RestRequest("api/v1/auth/token", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { username, password });

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Status Code: {response.StatusCode} - Error Message: {response.ErrorMessage}");

            var deserializer = new JsonDeserializer();
            var output = deserializer.Deserialize<Dictionary<string, string>>(response);

            string token = output["token"];
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));

            return client;
        }
    }
}
