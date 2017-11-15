using System;

using Xamarin.Forms;

namespace ApodApp
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page apodPage, aboutPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    apodPage = new NavigationPage(new ApodPage())
                    {
                        Title = "Apod"
                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "Another day"
                    };
                    aboutPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    apodPage = new ApodPage()
                    {
                        Title = "Today"
                    };

                    aboutPage = new AboutPage()
                    {
                        Title = "Another day"
                    };
                    break;
            }

            Children.Add(apodPage);
            Children.Add(aboutPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
