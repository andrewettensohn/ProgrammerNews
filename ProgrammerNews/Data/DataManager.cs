using ProgrammerNews.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerNews.Data
{
    public class DataManager
    {
        public RestService RestService { private set; get; }
        static ArticleDatabase database;
        public static ArticleDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ArticleDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }
        public int ArticleListCount { get; set; }

        public DataManager()
        {
            RestService = new RestService();
        }

        public async Task<int> SaveArticleAsync(Article article)
        {
            return await Database.SaveArticleAsync(article);
        }

        public async Task<List<Article>> GetSavedArticles()
        {
            return await Database.GetArticlesAsync();
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
