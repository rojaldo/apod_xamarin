using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApodApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ApodPage : ContentPage
	{
		public ApodPage ()
		{
			InitializeComponent ();
            ProcessRequest();
        }

        private async void ProcessRequest()
        {
            var data = await GetRequest("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY");
            label_title.Text = data.title;
            label_description.Text = data.explanation;
            image_main.Source = new UriImageSource { CachingEnabled = false, Uri = data.url };

        }

        private async Task<dynamic> GetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
            dynamic data = null;
            Console.Out.WriteLine("Start request!");
            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response Body: \r\n {0}", content);
                        data = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                        return data;
                    }
                    else
                    {
                        Console.Out.WriteLine("Response Body: Error!!!");
                        return null;
                    }
                }
            }
        }
    }
}