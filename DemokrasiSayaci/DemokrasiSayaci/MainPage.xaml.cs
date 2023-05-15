using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DemokrasiSayaci
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            crateName = Preferences.Get("crateName", "-");
            if (crateName == "-" || crateName == null || crateName == "")
            {
                SetCrateName();
            }
            rteCount = Convert.ToInt32(Preferences.Get("rteCount", "0"));
            kkCount = Convert.ToInt32(Preferences.Get("kkCount", "0"));
            rteCounter.Text = rteCount.ToString();
            kkCounter.Text = kkCount.ToString();
            lblCrateName.Text = crateName;
        }

        public async void SetCrateName()
        {
            crateName = await DisplayPromptAsync("UYARI", "Lütfen Sandık Adı Girin:", "Onayla", "İptal");
            Preferences.Set("crateName", crateName);
            lblCrateName.Text = crateName;
            if (lblCrateName.Text == "-" || lblCrateName.Text == "")
            {
                SetCrateName();
            }
            else if (lblCrateName.Text == null)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else if (lblCrateName.Text.Length >= 49)
            {
                string newText = "";
                for (int i = 0; i < 49; i++)
                {
                    newText += lblCrateName.Text[i];
                }
                lblCrateName.Text = newText;
            }
        }




        int rteCount, kkCount;
        TimeSpan increaseDuration = TimeSpan.FromMilliseconds(175);
        TimeSpan decreaseDuration = TimeSpan.FromMilliseconds(350);
        string crateName;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void buttonRTE_Clicked(object sender, EventArgs e)
        {
            rteCount++;
            rteCounter.Text = rteCount.ToString();
            Vibration.Vibrate(increaseDuration);
            Preferences.Set("rteCount", rteCount.ToString());
        }

        private void buttonRTEDecrease_Clicked(object sender, EventArgs e)
        {
            if (rteCount > 0)
            {
                rteCount--;
                rteCounter.Text = rteCount.ToString();
                Vibration.Vibrate(decreaseDuration);
                Preferences.Set("rteCount", rteCount.ToString());
            }

        }

        //----------------------------------------------------------------------------------------------------------------

        private void buttonKK_Clicked(object sender, EventArgs e)
        {
            kkCount++;
            kkCounter.Text = kkCount.ToString();
            Vibration.Vibrate(increaseDuration);
            Preferences.Set("kkCount", kkCount.ToString());
        }

        private void buttonKKDecrease_Clicked(object sender, EventArgs e)
        {
            if (kkCount > 0)
            {
                kkCount--;
                kkCounter.Text = kkCount.ToString();
                Vibration.Vibrate(decreaseDuration);
                Preferences.Set("kkCount", kkCount.ToString());
            }

        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private async void btnClearData_Clicked(object sender, EventArgs e)
        {
            bool accept = await DisplayAlert("UYARI", "Bütün veriler silinecek!", "ONAYLA", "REDDET");
            if (accept)
            {
                Preferences.Set("crateName", "-");
                Preferences.Set("rteCount", "0");
                Preferences.Set("kkCount", "0");
                rteCount = Convert.ToInt32(Preferences.Get("rteCount", "0"));
                kkCount = Convert.ToInt32(Preferences.Get("kkCount", "0"));
                lblCrateName.Text = "-";
                crateName = "";
                rteCounter.Text = rteCount.ToString();
                kkCounter.Text = kkCount.ToString();
                SetCrateName();

            }

        }




    }
}
