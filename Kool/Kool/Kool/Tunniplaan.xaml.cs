using Supremes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kool
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Tunniplaan : ContentPage
	{
		public Tunniplaan ()
		{
            InitializeComponent();
            var doc = Dcsoup.Parse(new Uri("https://tpt.siseveeb.ee/veebivormid/tunniplaan"), 5000);

        }
	}
}