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
        public List<int> ArticleIds { get; set; }

        readonly int PageCount = 15;

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<List<Article>> PerformFeedPaging()
        {
            List<int> newPageIds = ArticleIds.GetRange(0, PageCount);
            ArticleIds.RemoveRange(0, PageCount);

            List<Article> newArticles = await GetArticlesFromIds(newPageIds);
            return newArticles;
        }

        private async Task<List<Article>> GetArticlesFromIds(List<int> ids)
        {
            Uri itemBaseUri = new Uri(string.Format(Constants.itemBaseUrl, string.Empty));
            List<Article> articles = new List<Article>();

            foreach (int articleId in ids)
            {
                try
                {
                    HttpResponseMessage articleResponse = await client.GetAsync($"{itemBaseUri}/{articleId}.json");

                    if (articleResponse.StatusCode == HttpStatusCode.OK)
                    {
                        string jsonContent = await articleResponse.Content.ReadAsStringAsync();
                        Article article = JsonConvert.DeserializeObject<Article>(jsonContent);
                        articles.Add(article);
                    }
                }
                catch(Exception exc)
                {

                }
            }

            return articles;
        }

        public async Task<List<Article>> GetTopStories()
        {
            List<Article> topArticles = new List<Article>();
            Uri topStoryIdsUri = new Uri(string.Format(Constants.topStoriesUri, string.Empty));
            
            try
            {
                HttpResponseMessage idResponse = await client.GetAsync(topStoryIdsUri);

                if(idResponse.StatusCode == HttpStatusCode.OK)
                {
                    string idsJsonContent = await idResponse.Content.ReadAsStringAsync();
                    ArticleIds = JsonConvert.DeserializeObject<List<int>>(idsJsonContent);

                    List<int> firstPageIds = ArticleIds.GetRange(0, PageCount);
                    ArticleIds.RemoveRange(0, PageCount);

                    topArticles = await GetArticlesFromIds(firstPageIds);

                }
            }
            catch(Exception exc)
            { 
            }

            return topArticles;
        }
    }
}
