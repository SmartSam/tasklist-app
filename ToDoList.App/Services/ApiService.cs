using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList.Shared.Models;

namespace ToDoList.App.Services
{

    public class ApiService
    {
        public HttpClient _httpClient;

        public ApiService(HttpClient client)
        {
            _httpClient = client;
        }

        private async Task<string> requestNewToken()
        {
            try
            {
                var discovery = await HttpClientDiscoveryExtensions.GetDiscoveryDocumentAsync(
                    _httpClient, "http://localhost:5003");

                if (discovery.IsError)
                {
                    throw new ApplicationException($"Error: {discovery.Error}");
                }

                var tokenResponse = await HttpClientTokenRequestExtensions.RequestClientCredentialsTokenAsync(_httpClient, new ClientCredentialsTokenRequest
                {
                    Scope = "todolist-api",
                    ClientSecret = "thisismyclientspecificsecret",
                    Address = discovery.TokenEndpoint,
                    ClientId = "todolist-web"
                });

                if (tokenResponse.IsError)
                {
                    throw new ApplicationException($"Error: {tokenResponse.Error}");
                }

                return tokenResponse.AccessToken;
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Exception {e}");
            }
        }

        public async Task<List<ToDoItem>> GetItemsAsync()
        {
            var access_token = await requestNewToken();
            _httpClient.SetBearerToken(access_token);
            var response = await _httpClient.GetAsync("api/todoitem");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<ToDoItem>>(responseContent);
        }

        public async Task<ToDoItem> GetItemByIdAsync(int id)
        {
            var access_token = await requestNewToken();
            _httpClient.SetBearerToken(access_token);
            var response = await _httpClient.GetAsync($"api/todoitem/{id}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ToDoItem>(responseContent);
        }

        public async Task<ToDoItem> CreateItemAsync(ToDoItem toDoItem)
        {
            var access_token = await requestNewToken();
            _httpClient.SetBearerToken(access_token);
            var json =  JsonSerializer.Serialize<ToDoItem>(toDoItem);
            HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/todoitem/", contentPost);
            response.EnsureSuccessStatusCode();
            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ToDoItem>(responseContent);
        }

        public async Task<ToDoItem> UpdateItemAsync(ToDoItem toDoItem)
        {
            var access_token = await requestNewToken();
            _httpClient.SetBearerToken(access_token);
            var json = JsonSerializer.Serialize<ToDoItem>(toDoItem);
            HttpContent contentPut = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/todoitem/", contentPut);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ToDoItem>(responseContent);
        }
    }
}
