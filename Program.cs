
using http_request_training;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace HTTP_Request
{

    class Program
    {
        static async Task Main(string[] args) 
        {
            using (var httpClient = new HttpClient())
            {

                // GET
                var result = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
                var json = await result.Content.ReadAsStringAsync();

                var posts = JsonConvert.DeserializeObject<List<Post>>(json);
                var selectedPost = posts.FirstOrDefault(p => p.Id == 4);

                // POST
                var postContent = new Post()
                {
                    Id = 4,
                    UserId = 4,
                    UserName = "Pawel",
                    Title = "My Title"
                };

                var postJsonContent = new StringContent(JsonConvert.SerializeObject(postContent));
                var postResult = await httpClient.PostAsync("https://jsonplaceholder.typicode.com/posts", postJsonContent);

                using (var postRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://jsonplaceholder.typicode.com/posts"))
                {
                    postRequestMessage.Headers.Add("content-type", "application/json");
                    postRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(postJsonContent));

                    var post2Result = await httpClient.SendAsync(postRequestMessage);
                }

                // Adding query params
                var queryParams = HttpUtility.ParseQueryString("https://jsonplaceholder.typicode.com/posts");
                queryParams["postId"] = "1";
                queryParams["someParam"] = "someValue";

                var formattedParams = queryParams.ToString();

                Console.WriteLine(formattedParams);
            };
            
        }
    }
}