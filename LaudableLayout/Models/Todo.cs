using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace LaudableLayout.Models
{
    public class Todo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Done { get; set; }
    }

    public class TodoManager
    {
        public static HttpBaseProtocolFilter RootFilter { get; private set; }

        public static List<Todo> GetTodoes()
        {
            
            var todoes = new List<Todo>();
            RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = Windows.Web.Http.Filters.HttpCacheReadBehavior.MostRecent;
            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;
            using (var client = new HttpClient(RootFilter))
            {         
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(new Uri("http://localhost:50970/api/todoes"));
                });
                task.Wait();

                todoes = JsonConvert.DeserializeObject<List<Todo>>(response);

                return todoes;
            }
        }
    }
}
