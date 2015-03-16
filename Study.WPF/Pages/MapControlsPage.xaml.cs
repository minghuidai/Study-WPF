using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;
using Microsoft.Maps.MapControl.WPF.Design;
using Study.BingMap.Query;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Study.WPF.Pages
{
 
    public partial class MapControlsPage : Page
    {

        private string _BingMapSessionKey;

        //LocationConverter locConverter = new LocationConverter();

        public MapControlsPage()
        {
            InitializeComponent();

            //_BingMapSessionKey = myMap.CredentialsProvider.SessionId;



            //myMap.CredentialsProvider.GetCredentials((c) =>
            //{
            //    _BingMapSessionKey = c.ApplicationId;
            //});

        }


        //private void AnimationLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBoxItem cbi = (ComboBoxItem)(((ComboBox)sender).SelectedItem);
        //    string v = cbi.Content as string;
        //    if (!string.IsNullOrEmpty(v) && myMap != null)
        //    {
        //        AnimationLevel newLevel = (AnimationLevel)Enum.Parse(typeof(AnimationLevel), v, true);
        //        myMap.AnimationLevel = newLevel;
        //    }
        //}

    }
}
