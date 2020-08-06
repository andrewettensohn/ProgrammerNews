using Newtonsoft.Json;
using ProgrammerNews.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerNews.Services
{
    public class RestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<List<Article>> GetTopStories()
        {
            List<Article> topArticles = new List<Article>();
            Uri topStoryIdsUri = new Uri(string.Format(Constants.topStoriesUri, string.Empty));
            Uri itemBaseUri = new Uri(string.Format(Constants.itemBaseUrl, string.Empty));

            try
            {
                HttpResponseMessage idResponse = await client.GetAsync(topStoryIdsUri);

                if(idResponse.StatusCode == HttpStatusCode.OK)
                {
                    string idsJsonContent = await idResponse.Content.ReadAsStringAsync();
                    List<int> articleIds = JsonConvert.DeserializeObject<List<int>>(idsJsonContent);

                    articleIds = articleIds.GetRange(0, 30);

                    foreach (int articleId in articleIds)
                    {
                        HttpResponseMessage articleResponse = await client.GetAsync($"{itemBaseUri}/{articleId}.json");

                        if(articleResponse.StatusCode == HttpStatusCode.OK)
                        {
                            string jsonContent = await articleResponse.Content.ReadAsStringAsync();
                            Article article = JsonConvert.DeserializeObject<Article>(jsonContent);
                            topArticles.Add(article);
                        }
                    }
                }
            }
            catch(Exception exc)
            { 
            }

            return topArticles;
        }
    }
}
