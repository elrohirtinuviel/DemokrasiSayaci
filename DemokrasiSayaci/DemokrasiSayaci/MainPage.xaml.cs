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
            SetValues();
            if (crateName == "-" || crateName == null || crateName == "")
            {
                SetCrateName();
            }
            else if(currentVote == 0 && (rteCount == 0 || kkCount == 0))
            {
                SetTotalVote();
            }           

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
            else
            {
                SetTotalVote();
            }
            
        }

        public async void SetTotalVote()
        {
            try
            {
                totalVotes = Convert.ToInt32(await DisplayPromptAsync("UYARI", "Lütfen Kullanılan Oy Sayısını Giriniz:", "Onayla", "İptal"));
                Preferences.Set("totalVotes", totalVotes.ToString());
                currentVote = totalVotes;
                Preferences.Set("currentVote", currentVote.ToString());
                lblVotesLeft.Text = currentVote.ToString();
            }
            catch
            {
                SetTotalVote();
            }
            if (lblVotesLeft.Text == "0" || lblVotesLeft.Text == "")
            {
                SetTotalVote();
            }
            else if (lblVotesLeft.Text == null)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            

        }

        public void SetValues()
        {
            crateName = Preferences.Get("crateName", "");
            rteCount = Convert.ToInt32(Preferences.Get("rteCount", "0"));
            kkCount = Convert.ToInt32(Preferences.Get("kkCount", "0"));
            vibrate = Convert.ToBoolean(Preferences.Get("vibrate", "true"));
            invalidVotes = Convert.ToInt32(Preferences.Get("invalidVotes", "0"));
            currentVote = Convert.ToInt32(Preferences.Get("currentVote", "0"));          
            totalVotes = Convert.ToInt32(Preferences.Get("totalVotes", "0"));
            rteCounter.Text = rteCount.ToString();
            kkCounter.Text = kkCount.ToString();
            lblCrateName.Text = crateName;
            lblInvalidVotes.Text = invalidVotes.ToString();
            lblVotesLeft.Text = currentVote.ToString();
        }



        int rteCount, kkCount;
        TimeSpan increaseDuration = TimeSpan.FromMilliseconds(175);
        TimeSpan decreaseDuration = TimeSpan.FromMilliseconds(350);
        string crateName;
        bool vibrate;
        int totalVotes;
        int currentVote;
        int invalidVotes;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void buttonRTE_Clicked(object sender, EventArgs e)
        {
            if (rteCount < totalVotes && currentVote > 0)
            {
                rteCount++;
                rteCounter.Text = rteCount.ToString();
                if (vibrate)
                {
                    Vibration.Vibrate(increaseDuration);
                }
                Preferences.Set("rteCount", rteCount.ToString());
                currentVote--;
                Preferences.Set("currentVote", currentVote.ToString());
                lblVotesLeft.Text = currentVote.ToString();
                if(rteCount > kkCount)
                {
                    rteCounter.TextColor = Color.Green;
                    kkCounter.TextColor = Color.White;
                }
                else if (kkCount == rteCount)
                {
                    kkCounter.TextColor = Color.White;
                    rteCounter.TextColor = Color.White;
                }
                else
                {
                    rteCounter.TextColor = Color.White;
                }
            }
            
        }

        private void buttonRTEDecrease_Clicked(object sender, EventArgs e)
        {
            if (rteCount > 0)
            {
                rteCount--;
                rteCounter.Text = rteCount.ToString();
                if (vibrate)
                {
                    Vibration.Vibrate(decreaseDuration);
                }
                Preferences.Set("rteCount", rteCount.ToString());
                currentVote++;
                Preferences.Set("currentVote", currentVote.ToString());
                lblVotesLeft.Text = currentVote.ToString();
                if (rteCount > kkCount)
                {
                    rteCounter.TextColor = Color.Green;
                    kkCounter.TextColor = Color.White;
                }
                else if (kkCount == rteCount)
                {
                    kkCounter.TextColor = Color.White;
                    rteCounter.TextColor = Color.White;
                }
                else
                {
                    rteCounter.TextColor = Color.White;
                    kkCounter.TextColor = Color.Green;
                }
            }

        }

        //----------------------------------------------------------------------------------------------------------------

        private void buttonKK_Clicked(object sender, EventArgs e)
        {
            if(kkCount < totalVotes && currentVote > 0)
            {
                kkCount++;
                kkCounter.Text = kkCount.ToString();
                if (vibrate)
                {
                    Vibration.Vibrate(increaseDuration);
                }
                Preferences.Set("kkCount", kkCount.ToString());
                currentVote--;
                Preferences.Set("currentVote", currentVote.ToString());
                lblVotesLeft.Text = currentVote.ToString();
                if (kkCount > rteCount)
                {
                    kkCounter.TextColor = Color.Green;
                    rteCounter.TextColor = Color.White;
                }
                else if(kkCount == rteCount)
                {
                    kkCounter.TextColor = Color.White;
                    rteCounter.TextColor = Color.White;
                }
                else
                {
                    kkCounter.TextColor = Color.White;
                }
            }
           
        }

        private void buttonKKDecrease_Clicked(object sender, EventArgs e)
        {
            if (kkCount > 0)
            {
                kkCount--;
                kkCounter.Text = kkCount.ToString();
                if (vibrate)
                {
                    Vibration.Vibrate(decreaseDuration);
                }
                Preferences.Set("kkCount", kkCount.ToString());
                currentVote++;
                Preferences.Set("currentVote", currentVote.ToString());
                lblVotesLeft.Text = currentVote.ToString();
                if (kkCount > rteCount)
                {
                    kkCounter.TextColor = Color.Green;
                    rteCounter.TextColor = Color.White;
                }
                else if (kkCount == rteCount)
                {
                    kkCounter.TextColor = Color.White;
                    rteCounter.TextColor = Color.White;
                }
                else
                {
                    kkCounter.TextColor = Color.White;
                    rteCounter.TextColor= Color.Green;
                }
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
                Preferences.Set("totalVotes", "0");
                Preferences.Set("currentVote", "0");
                Preferences.Set("invalidVotes", "0");
                rteCount = Convert.ToInt32(Preferences.Get("rteCount", "0"));
                kkCount = Convert.ToInt32(Preferences.Get("kkCount", "0"));
                lblCrateName.Text = "-";
                lblVotesLeft.Text = "0";
                lblInvalidVotes.Text = "0";
                crateName = "";
                currentVote = 0;
                rteCounter.Text = rteCount.ToString();
                kkCounter.Text = kkCount.ToString();
                SetValues();
                SetCrateName();
                //SetValues();

            }

        }

        private void btnInvalidVoteIncrease_Clicked(object sender, EventArgs e)
        {
            if(currentVote > 0)
            {
                invalidVotes++;
                Preferences.Set("invalidVotes", invalidVotes.ToString());
                lblInvalidVotes.Text = invalidVotes.ToString();
                currentVote--;
                Preferences.Set("currentVote", currentVote.ToString());
                lblVotesLeft.Text = currentVote.ToString();
            }
           
        }

        private void btnInvalidVoteDecrease_Clicked(object sender, EventArgs e)
        {
            if (invalidVotes > 0)
            {
                invalidVotes--;
                Preferences.Set("invalidVotes", invalidVotes.ToString());
                lblInvalidVotes.Text = invalidVotes.ToString();
                currentVote++;
                Preferences.Set("currentVote", currentVote.ToString());
                lblVotesLeft.Text = currentVote.ToString();
            }
        }

        private void btnVibrate_Clicked(object sender, EventArgs e)
        {
            vibrate = !vibrate;
            Preferences.Set("vibrate", vibrate.ToString());
            
        }

    }
}
