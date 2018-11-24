using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
// NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
namespace UtilityService
{
    public class Service1 : IService1
    {
        private static readonly HttpClient client = new HttpClient();

        public int SiteCompareTool(string url, string url2)
        {
            int score = 0;
            string[] topWordsSite1 = TopNWords(url, 100);
            string[] topWordsSite2 = TopNWords(url2, 100);
            foreach (string topWord in topWordsSite1)
            {
                if (topWordsSite2.Contains(topWord))
                {
                    int indexOnList = Array.IndexOf(topWordsSite2, topWord);
                    score += indexOnList;
                }
            }
            Uri site1 = new Uri(url);
            Uri site2 = new Uri(url2);
            for (int i = 0; i < site1.Segments.Length; i++)
            {
                if (site2.Segments.Length > i)
                {
                    if (site1.Segments.ElementAt(i) == site2.Segments.ElementAt(i))
                        score += 100;
                }
            }
            if (site1.Authority == site2.Authority)
                score += 100;
            return score;
        }

        string[] TopNWords(string url, int numberOfWords)
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            string content = reader.ReadToEnd();
            data.Close();
            reader.Close();

            string split = content.Split(new[] { "</head>" }, StringSplitOptions.None)[1];
            split = Regex.Replace(split, "<.*?>", String.Empty);
            split = WordFilter(split);
            string[] splitted = split.Split(' ');
            Dictionary<string, int> results = new Dictionary<string, int>();
            foreach (string element in splitted)
            {
                string trimmed = element.Trim();
                if (trimmed.Length > 0)
                {
                    if (results.ContainsKey(trimmed))
                        results[trimmed] += 1;
                    else
                        results.Add(trimmed, 1);
                }
            }
            return results.OrderByDescending(r => r.Value).Select(r => r.Key).Take(numberOfWords).ToArray();
        }

        public string WordFilter(string input)
        {
            List<string> stopWords = GetStopWordList();
            List<string> result = new List<string>();
            string[] words = input.Split(' ');
            foreach (string word in words)
            {
                if (!stopWords.Contains(word.Trim().ToLower()))
                {
                    result.Add(word);
                }
            }
            return string.Join(" ", result);
        }

        private List<string> GetStopWordList()
        {
            return new List<string>(){ "a",
    "about",
    "above",
    "after",
    "again",
    "against",
    "all",
    "am",
    "an",
    "and",
    "any",
    "are",
    "aren't",
    "as",
    "at",
    "be",
    "because",
    "been",
    "before",
    "being",
    "below",
    "between",
    "both",
    "but",
    "by",
    "can't",
    "cannot",
    "could",
    "couldn't",
    "did",
    "didn't",
    "do",
    "does",
    "doesn't",
    "doing",
    "don't",
    "down",
    "during",
    "each",
    "few",
    "for",
    "from",
    "further",
    "had",
    "hadn't",
    "has",
    "hasn't",
    "have",
    "haven't",
    "having",
    "he",
    "he'd",
    "he'll",
    "he's",
    "her",
    "here",
    "here's",
    "hers",
    "herself",
    "him",
    "himself",
    "his",
    "how",
    "how's",
    "i",
    "i'd",
    "i'll",
    "i'm",
    "i've",
    "if",
    "in",
    "into",
    "is",
    "isn't",
    "it",
    "it's",
    "its",
    "itself",
    "let's",
    "me",
    "more",
    "most",
    "mustn't",
    "my",
    "myself",
    "no",
    "nor",
    "not",
    "of",
    "off",
    "on",
    "once",
    "only",
    "or",
    "other",
    "ought",
    "our",
    "ours",
    "ourselves",
    "out",
    "over",
    "own",
    "same",
    "shan't",
    "she",
    "she'd",
    "she'll",
    "she's",
    "should",
    "shouldn't",
    "so",
    "some",
    "such",
    "than",
    "that",
    "that's",
    "the",
    "their",
    "theirs",
    "them",
    "themselves",
    "then",
    "there",
    "there's",
    "these",
    "they",
    "they'd",
    "they'll",
    "they're",
    "they've",
    "this",
    "those",
    "through",
    "to",
    "too",
    "under",
    "until",
    "up",
    "very",
    "was",
    "wasn't",
    "we",
    "we'd",
    "we'll",
    "we're",
    "we've",
    "were",
    "weren't",
    "what",
    "what's",
    "when",
    "when's",
    "where",
    "where's",
    "which",
    "while",
    "who",
    "who's",
    "whom",
    "why",
    "why's",
    "with",
    "won't",
    "would",
    "wouldn't",
    "you",
    "you'd",
    "you'll",
    "you're",
    "you've",
    "your",
    "yours",
    "yourself",
    "yourselves",
};
        }

        public string RemoveHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public string[] TopTenContentWords(string url)
        {
            WebClient channel = new WebClient(); // create a channel
            byte[] abc = channel.DownloadData(url);
            string converted = UTF8Encoding.UTF8.GetString(abc);
            string split = converted.Split(new[] { "</head>" }, StringSplitOptions.None)[1];
            split = RemoveHtml(split);
            split = WordFilter(split);
            string[] splitted = split.Split(' ');
            Dictionary<string, int> results = new Dictionary<string, int>();
            foreach (string element in splitted)
            {
                string trimmed = element.Trim();
                trimmed = trimmed.ToLower();
                if (trimmed.Length > 0)
                {
                    if (results.ContainsKey(trimmed))
                        results[trimmed] += 1;
                    else
                        results.Add(trimmed, 1);
                }
            }
            return results.OrderByDescending(r => r.Value).Select(r => r.Key).Take(10).ToArray();
        }

        async Task<string> GetResponse(string wordToStem)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
        {
           { "text", wordToStem }
        };

            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = await client.PostAsync("http://text-processing.com/api/sentiment/", content);

            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public string SentimentAnalysis(string wordToAnalyze)
        {
            string[] words = wordToAnalyze.Split(' ');
            Dictionary<string, int> results = new Dictionary<string, int>();
            foreach (string word in words)
            {
                string stem = GetResponse(word).Result;
                JObject jo = JObject.Parse(stem);
                IEnumerable<JProperty> props = jo.Properties();
                foreach (JProperty prop in props)
                {
                    if (prop.Name == "label")
                    {
                        string propertyValue = prop.Value.ToString();
                        if (results.ContainsKey(propertyValue))
                        {
                            results[propertyValue]++;
                        }
                        else
                        {
                            results.Add(propertyValue, 1);
                        }
                    }
                }
            }
            string toReturn = "";
            foreach (KeyValuePair<string, int> result in results)
            {
                toReturn += "Key: " + result.Key + " Value: " + result.Value;
            }
            return toReturn;
        }
    }
}