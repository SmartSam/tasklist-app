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

        public async Task<List<ToDoItem>> GetItemsAsync()
        {
            var response = await _httpClient.GetAsync("api/todoitem");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<ToDoItem>>(responseContent);
        }

        public async Task<ToDoItem> GetItemByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/todoitem/{id}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ToDoItem>(responseContent);
        }

        public async Task<ToDoItem> CreateItemAsync(ToDoItem toDoItem)
        {
            var json =  JsonSerializer.Serialize<ToDoItem>(toDoItem);
            HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/todoitem/", contentPost);
            response.EnsureSuccessStatusCode();
            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ToDoItem>(responseContent);
        }

        public async Task<ToDoItem> UpdateItemAsync(ToDoItem toDoItem)
        {
            var json = JsonSerializer.Serialize<ToDoItem>(toDoItem);
            HttpContent contentPut = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/todoitem/", contentPut);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ToDoItem>(responseContent);
        }
    }
}
