using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;

namespace BookClub.Code.Classes
{
    public class GoodreadsObject
    {
        static readonly HttpClient client = new HttpClient();
        private readonly string key = ConfigurationManager.AppSettings["apiKey"];
        private readonly string secret = ConfigurationManager.AppSettings["apiSecret"];
        const string shelf = "books-i-own";

        public int id { get; set; }

        public GoodreadsObject(int ID)
        {
            id = ID;
        }

        public async Task<string> GetBookToRead ()
        {
            XDocument xml = new XDocument();

            try
            {
                string uri = ($"https://www.goodreads.com/review/list?v=2&key={key}&id={id}&shelf={shelf}&sort=random&per_page=200");
                var result = await client.GetAsync(uri);
                string shelves = await result.Content.ReadAsStringAsync();
                xml = XDocument.Parse(shelves);
                string book = xml.Elements().Descendants("book").Elements("title").Select(x => x.Value.ToString()).First();

                return book;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}
