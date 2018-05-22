using Supremes;
using System;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kool
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Animatsioon : ContentPage
	{
        Timer timer;
		public Animatsioon ()
		{
            InitializeComponent();

            startButton.Clicked += StartButton_Clicked;

            character.Text = "A";
        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {
            character.Text = "N";
            timer = new Timer(1);
            
            
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            character.Text = character.Text + "a";
        }
    }
}