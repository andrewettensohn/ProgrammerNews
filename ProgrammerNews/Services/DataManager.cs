using ProgrammerNews.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerNews.Services
{
    public class DataManager
    {
        public RestService RestService { private set; get; }
        public int ArticleListCount { get; set; }

        public DataManager()
        {
            RestService = new RestService();
        }

        public async Task<List<Article>> GetTopStories()
        {
            List<Article> articles = await RestService.GetTopStories();
            ArticleListCount = RestService.ArticleIds.Count;
            return articles;
        }

        public async Task<List<Article>> PerformFeedPaging()
        {
            List<Article> articles = await RestService.PerformFeedPaging();
            ArticleListCount = RestService.ArticleIds.Count;
            return articles;
        }
    }
}
