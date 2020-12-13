﻿using Newtonsoft.Json;
using OpenBots.Core.Server.Models;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace OpenBots.Core.Server.API_Methods
{
    public class AssetMethods
    {
        public static Asset GetAsset(RestClient client, string filter)
        {           
            var request = new RestRequest("api/v1/Assets", Method.GET);
            request.AddParameter("$filter", filter);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Status Code: {response.StatusCode} - Error Message: {response.ErrorMessage}");

            var deserializer = new JsonDeserializer();
            var output = deserializer.Deserialize<Dictionary<string, string>>(response);
            var items = output["items"];
            return JsonConvert.DeserializeObject<List<Asset>>(items).FirstOrDefault();
        }       

        public static void PutAsset(RestClient client, Asset asset)
        {
            var request = new RestRequest("api/v1/Assets/{id}", Method.PUT);
            request.AddUrlSegment("id", asset.Id.ToString());           
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(asset);

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Status Code: {response.StatusCode} - Error Message: {response.ErrorMessage}");
        }

        public static void DownloadFileAsset(RestClient client, Guid? assetID, string directoryPath, string fileName)
        {
            var request = new RestRequest("api/v1/assets/{id}/Export", Method.GET);
            request.AddUrlSegment("id", assetID.ToString());
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Status Code: {response.StatusCode} - Error Message: {response.ErrorMessage}");

            byte[] file = response.RawBytes;
            File.WriteAllBytes(Path.Combine(directoryPath, fileName), file);
        }

        public static void UpdateFileAsset(RestClient client, Asset asset, string filePath)
        {
            var request = new RestRequest("api/v1/Assets/{id}/Update", Method.PUT);
            request.AddUrlSegment("id", asset.Id.ToString());
            request.RequestFormat = DataFormat.Json;

            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFile("File", filePath.Trim());

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Status Code: {response.StatusCode} - Error Message: {response.ErrorMessage}");
        }

        public static void AppendAsset(RestClient client, Guid? assetId, string appendText)
        {
            var request = new RestRequest("api/v1/Assets/{id}/Append", Method.PUT);
            request.AddUrlSegment("id", assetId.ToString());
            request.AddQueryParameter("value", appendText);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Status Code: {response.StatusCode} - Error Message: {response.ErrorMessage}");
        }
      
        public static void IncrementAsset(RestClient client, Guid? assetId)
        {
            var request = new RestRequest("api/v1/Assets/{id}/Increment", Method.PUT);
            request.AddUrlSegment("id", assetId.ToString());
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Status Code: {response.StatusCode} - Error Message: {response.ErrorMessage}");
        }

        public static void DecrementAsset(RestClient client, Guid? assetId)
        {
            var request = new RestRequest("api/v1/Assets/{id}/Decrement", Method.PUT);
            request.AddUrlSegment("id", assetId.ToString());
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Status Code: {response.StatusCode} - Error Message: {response.ErrorMessage}");
        }

        public static void AddAsset(RestClient client, Guid? assetId, string value)
        {
            var request = new RestRequest("api/v1/Assets/{id}/Add", Method.PUT);
            request.AddUrlSegment("id", assetId.ToString());
            request.AddQueryParameter("value", value);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Status Code: {response.StatusCode} - Error Message: {response.ErrorMessage}");
        }

        public static void SubtractAsset(RestClient client, Guid? assetId, string value)
        {
            var request = new RestRequest("api/v1/Assets/{id}/Subtract", Method.PUT);
            request.AddUrlSegment("id", assetId.ToString());
            request.AddQueryParameter("value", value);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Status Code: {response.StatusCode} - Error Message: {response.ErrorMessage}");
        }
    }
}
