using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

using BE = BusinessEntities;

namespace WebApp.Models
{

    public interface IApiClient<T> where T : BE.CoreEntity.IEntity
    {
        Task<List<T>> Search(string path);
    }

    public class ApiClient<T> : IApiClient<T> where T : BE.CoreEntity.IEntity
    {
        HttpClient client = new HttpClient();

        /// <summary>
        /// Search entities from web api
        /// </summary>
        /// <param name="path">Api path</param>
        /// <returns></returns>
        public async Task<List<T>> Search(string path)
        {
            IList<T> books = null;
            HttpResponseMessage response = await client.GetAsync(apiPath + path);
            if (response.IsSuccessStatusCode)
            {
                books = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<T>>(await response.Content.ReadAsStringAsync());
            }
            return books.ToList();
        }

        private static string apiPath = System.Configuration.ConfigurationManager.AppSettings["WebApiPath"].ToString();

    }
}