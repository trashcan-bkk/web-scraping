using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace web_scraper.Controllers
{
    public class MainController : ControllerBase 
    {
        [HttpGet("web-scraping")]
        public async Task<List<string>> Main(string source, string tagString, string classString) 
        {
            // Insert info for scraping like Url, tag, class

            // source = $"https://www.jdsports.co.th/brand/fila/";
            // tagString = "span";
            // classString = "itemTitle";

            // Gather tag and class into a string
            string query = string.Format("//{0}[@class='{1}']", tagString, classString);
            
            // Create a list for returning data back 
            List<string> dataList = new List<string>();

            // Connect to the path url and retrieve the data
            HttpClient hc = new HttpClient();
            HttpResponseMessage result = await hc.GetAsync(source);
            Stream stream = await result.Content.ReadAsStreamAsync();
            HtmlDocument doc = new HtmlDocument();
            doc.Load(stream);
            var data = doc.DocumentNode.SelectNodes(query);

            // Loop the data and display into a list 
            foreach (var item in data)
            {
                dataList.Add(item.InnerText);
            }

            if (dataList == null)
            {
                return null;
            }
            return dataList;
        }

        [HttpGet("web-scraping/export")]
        public void Export() 
        {
        }
    }
}