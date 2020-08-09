using ProgrammerNews.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerNews.Data
{
    public class ArticleDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ArticleDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Article>().Wait();
        }

        public async Task<List<Article>> GetArticlesAsync()
        {
            return await _database.Table<Article>().ToListAsync();
        }

        public async Task<int> SaveArticleAsync(Article article)
        {
            return await _database.InsertAsync(article);
        }
    }
}
