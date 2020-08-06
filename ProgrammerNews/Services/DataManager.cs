using ProgrammerNews.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerNews.Services
{
    public class DataManager
    {
        RestService RestService;

        public DataManager()
        {
            RestService = new RestService();
        }

        public Task<List<Article>> GetTopStories()
        {
            return RestService.GetTopStories();
        }
    }
}
