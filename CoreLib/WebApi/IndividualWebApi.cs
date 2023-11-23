using CoreLib.Interfaces;
using CoreLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreLib.WebApi
{
    public class IndividualWebApi
    {
        static readonly HttpClient client = new HttpClient();
        static string controllerPath = "https://localhost:7186/Individual";
        public async Task<Individual?> Get(int id)
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync(controllerPath+$"/{id}");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<Individual>();

                return result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
        public async Task<List<Individual>?> GetAll()
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync(controllerPath);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<List<Individual>>();

                return result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
        public async Task<Individual?> Create(Individual individual)
        {
            try
            {
                using HttpResponseMessage response = await client.PostAsJsonAsync(controllerPath, individual);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<Individual>();

                return result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
        public async Task<Individual?> Update(Individual individual)
        {
            try
            {
                using HttpResponseMessage response = await client.PutAsJsonAsync(controllerPath, individual);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<Individual>();

                return result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
        public async Task<Individual?> Delete(Individual individual)
        {
            try
            {
                using HttpResponseMessage response = await client.DeleteAsync(controllerPath+$"/{individual.Id}");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<Individual>();

                return result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}
