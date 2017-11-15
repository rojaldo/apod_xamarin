using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApodApp
{
	public partial class AboutPage : ContentPage
	{
        private string url;

        public AboutPage()
		{
			InitializeComponent();
		}

        public void DatePicked(object sender, DateChangedEventArgs e)
        {
            
            var date = datepicker.Date.ToString("yyyy-MM-dd");
            this.url = "https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY" + "&date=" + date;
            label_title.Text = date;
            ProcessRequest();
        }

        private async void ProcessRequest()
        {
            var data = await GetRequest(url);
            label_title.Text = data.title;
            label_description.Text = data.explanation;
            image_main.Source = new UriImageSource { CachingEnabled = false, Uri = data.url }; ;

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
