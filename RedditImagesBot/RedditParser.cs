using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RedditImagesBot
{
    internal class RedditParser
    {
        public static async Task<string> GetContent(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            //request.Accept = "application/xrds+xml";  
            HttpWebResponse response = (HttpWebResponse)request!.GetResponse();

            WebHeaderCollection header = response.Headers;

            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                string responseText = await reader.ReadToEndAsync();
                return responseText;
            }
        }

        public static async Task<string> GetTopOfTheDayPhotoUrl(string topicUrl)
        {
            string url = $"{topicUrl}top/?t=day";

            string response = await GetContent(url);
          
            string imageUrl = response
                .Split("https://i.imgur.com/")[1]
                .Split("\"")
                .First();

            imageUrl = $"https://i.imgur.com/{imageUrl}";

            return imageUrl;
        }
    }
}
