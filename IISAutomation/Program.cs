using Microsoft.Web.Administration;
using System;
using System.Linq;
using System.Net.Http;

namespace IISAutomation
{
    class Program
    {
        private const string ADDRESS = "https://gist.github.com/CharlTruter/2107864";
        private const string WEBSITE_NAME = "Default Web Site";

        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(ADDRESS).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    using (ServerManager manager = new ServerManager())
                    {
                        // Get the Site object
                        Site site = manager.Sites.Where(q => q.Name.Equals(WEBSITE_NAME)).FirstOrDefault();

                        // If the site does not exist, throw an exception
                        if (site == null)
                        {
                            throw new Exception("The specified site was not found!");
                        }

                        // Stop the site
                        site.Stop();
                        site.Start();
                    }
                }
            }
        }
    }
}
