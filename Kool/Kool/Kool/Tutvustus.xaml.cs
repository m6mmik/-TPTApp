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
	public partial class Tutvustus : ContentPage
	{
        List<Info> infoList;
        ListView listView;
        WebView web;
        class Info
        {
            public Info(string pealkiri, string link)
            {
                this.Pealkiri = pealkiri;
                this.link = link;
            }

            public string Pealkiri { private set; get; }

            public string link { private set; get; }

          


        };

        public Tutvustus ()
		{
			InitializeComponent ();


            infoList = new List<Info>
            {


            };

            string url = "";
            var doc = Dcsoup.Parse(new Uri("https://www.tptlive.ee/opilasele/"), 5000);

            Elements content = doc.GetElementsByClass("inner-content-wrapper");

            string h = doc.Head.Html;


            Elements informatsioon = content[0].Children;

            for (int i = 1; i < informatsioon.Count - 1; i++)
            {

                Console.WriteLine(informatsioon[i].Children[0].Children[0].Attr("href"));
                Console.WriteLine(informatsioon[i].Children[0].Children[0].Text);
                string link = informatsioon[i].Children[0].Children[0].Attr("href");
                string pealkiri = informatsioon[i].Children[0].Children[0].Text;
                infoList.Add(new Info(pealkiri, link));

            }


            web = new WebView
            {
                Source = "www.google.ee"
            };

            listView = new ListView
            {
               
                ItemsSource = infoList,
                
                
                ItemTemplate = new DataTemplate(() =>
                {
                    

                    Label titleLabel = new Label();
                    titleLabel.SetBinding(Label.TextProperty, "Pealkiri");

                    
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                                {
                                  
                                    new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        Spacing = 0,
                                        Children =
                                        {
                                            titleLabel
                                        }
                                        }
                                }
                        }
                    };
                })
            };

            listView.ItemSelected += (sender, e) => {
                var index = (listView.ItemsSource as List<Info>).IndexOf(e.SelectedItem as Info);
                web = new WebView() {
                    
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                web.Source = infoList[index].link;

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

            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(100, GridUnitType.Absolute) }
                },
                ColumnDefinitions =
                {
                new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) }
                }
            };
            grid.Children.Add(listView);
            grid.Children.Add(web);

            

            Content = new StackLayout
            {
                Children = {

                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Orientation = StackOrientation.Horizontal,

                        Children =
                        {

                            

                        }


                    },

                    listView
                }
            };


        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Content = new StackLayout
            {
                Children = {

                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Orientation = StackOrientation.Horizontal,

                        Children =
                        {



                        }


                    },

                    listView
                }
            };
        }
    }
}