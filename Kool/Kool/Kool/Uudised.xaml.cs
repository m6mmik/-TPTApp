using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Supremes;
using Supremes.Nodes;

namespace Kool
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Uudised : ContentPage
	{
        int currentpage = 1;
        List<Uudis> uudisedList;
        ListView listView;
        WebView web;
        class Uudis
        {
            public Uudis(string pealkiri,string kuupäev, string link, Color Color)
            {
                this.Pealkiri = pealkiri;
                this.kuupäev = kuupäev;
                this.Color = Color;
                this.link = link;
            }

            public string Pealkiri { private set; get; }

            public string link { private set; get; }

            public string kuupäev { private set; get; }

            public Color Color { private set; get; }


        };

        public void createUudised()
        {
            
            uudisedList = new List<Uudis>
            {


            };
            string url = "https://www.tptlive.ee/category/uudised/page/" + currentpage.ToString();
            var doc = Dcsoup.Parse(new Uri(url), 5000);
            //Console.WriteLine(doc.Html);
            Elements uudised = doc.GetElementsByClass("blog-entry");

            string h = doc.Head.Html;

            Console.WriteLine(uudised.Count);
            for (int i = 0; i < uudised.Count; i++)
            {
                //Console.WriteLine(uudised[i]);
                uudised[i].GetElementsByClass("blog-info");
                uudised[i].GetElementsByClass("blog-date");

                string date = uudised[i].GetElementsByClass("blog-date").Text;
                Console.WriteLine(date);

                string title = uudised[i].GetElementsByClass("blog-content").Select("a").Attr("title");
                Console.WriteLine(title);

                string link = uudised[i].GetElementsByClass("blog-content").Select("a").Attr("href");
                Console.WriteLine(link);

                Random rnd = new Random();
                Color randomColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                uudisedList.Add(new Uudis(title, date, link, randomColor));


                Console.WriteLine(" ");
            }

            
            listView = new ListView
            {
              
                ItemsSource = uudisedList,

                
                ItemTemplate = new DataTemplate(() =>
                {
                    
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "Uudis");

                    Label titleLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "Pealkiri");

                    Label dateLabel = new Label();
                    dateLabel.SetBinding(Label.TextProperty, "kuupäev");

                    BoxView boxView = new BoxView();
                    boxView.SetBinding(BoxView.ColorProperty, "Color");

                    
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                                {
                                    boxView,
                                    new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        Spacing = 0,
                                        Children =
                                        {
                                            nameLabel,
                                            dateLabel
                                        }
                                        }
                                }
                        }
                    };
                })
            };

        }

        public Uudised ()
		{

            createUudised();

            Label header = new Label
            {
                Text = "Uudised",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Button next = new Button
            {
                Text = "Järgmine",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            next.Clicked += Next_Clicked;

            Button previous = new Button
            {
                Text = "Eelmine",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            previous.Clicked += Previous_Clicked;
            // Define some data.


            listView.ItemSelected += (sender, e) =>
            {
                var index = (listView.ItemsSource as List<Uudis>).IndexOf(e.SelectedItem as Uudis);
                web = new WebView()
                {

                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                web.Source = uudisedList[index].link;

                Button back = new Button
                {
                    Text = "Tagasi",
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    HorizontalOptions = LayoutOptions.Center
                };

                back.Clicked += Back_Clicked;

                Content = new StackLayout
                {
                    Children = {

                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Orientation = StackOrientation.Horizontal,

                        Children =
                        {

                            back,

                        }


                    },
                    web

                }
                };


            };



                Content = new StackLayout
            {
                Children = {

                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Orientation = StackOrientation.Horizontal,

                        Children =
                        {
                            
                            previous,
                            next
                        }

                        
                    },

                    header,
                    
                    listView
                }
            };
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Label header = new Label
            {
                Text = "Uudised",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Button next = new Button
            {
                Text = "Järgmine",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            next.Clicked += Next_Clicked;

            Button previous = new Button
            {
                Text = "Eelmine",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            previous.Clicked += Previous_Clicked;
            Content = new StackLayout
            {
                Children = {

                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Orientation = StackOrientation.Horizontal,

                        Children =
                        {

                            previous,
                            next
                        }


                    },

                    header,

                    listView
                }
            };
        }

        public void loadUudised()
        {
            uudisedList = new List<Uudis>
            {


            };
            string url = "https://www.tptlive.ee/category/uudised/page/" + currentpage.ToString();
            var doc = Dcsoup.Parse(new Uri(url), 5000);
            //Console.WriteLine(doc.Html);
            Elements uudised = doc.GetElementsByClass("blog-entry");

            string h = doc.Head.Html;

            Console.WriteLine(uudised.Count);
            for (int i = 0; i < uudised.Count; i++)
            {
                //Console.WriteLine(uudised[i]);
                uudised[i].GetElementsByClass("blog-info");
                uudised[i].GetElementsByClass("blog-date");

                string date = uudised[i].GetElementsByClass("blog-date").Text;
                Console.WriteLine(date);

                string title = uudised[i].GetElementsByClass("blog-content").Select("a").Attr("title");
                Console.WriteLine(title);

                string link = uudised[i].GetElementsByClass("blog-content").Select("a").Attr("href");
                Console.WriteLine(link);

                Random rnd = new Random();
                Color randomColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                uudisedList.Add(new Uudis(title, date, link, randomColor));


                Console.WriteLine(" ");
            }
            listView.ItemsSource = uudisedList;
        }

        private void Previous_Clicked(object sender, EventArgs e)
        {

            if(currentpage > 1)
            {
                currentpage--;
                uudisedList.Clear();
                loadUudised();
            }
            

        }

        private void Next_Clicked(object sender, EventArgs e)
        {
            //listView.ItemsSource = null;
            
            
            currentpage++;
            uudisedList.Clear();
            loadUudised();
            
           
            
            
        }
    }
}