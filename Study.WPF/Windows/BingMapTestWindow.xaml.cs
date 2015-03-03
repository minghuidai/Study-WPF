﻿using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Study.WPF.Windows
{
    /// <summary>
    /// Interaction logic for BingMapTestWindow.xaml
    /// </summary>


    public partial class BingMapTestWindow : Window
    {
        LocationConverter locConverter = new LocationConverter();
        bool _PickingLocation = false;
        LocationCollection _PolygonPoints = null;

        public BingMapTestWindow()
        {
            InitializeComponent();

            myMap.Focus();

            // Displays the current latitude and longitude as the map animates.
            myMap.ViewChangeOnFrame += new EventHandler<MapEventArgs>(viewMap_ViewChangeOnFrame);
            // The default animation level: navigate between different map locations.
            //viewMap.AnimationLevel = AnimationLevel.Full;

        }


        private void viewMap_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            // Gets the map object that raised this event.
            Map map = sender as Map;
            // Determine if we have a valid map object.
            if (map != null)
            {
                // Gets the center of the current map view for this particular frame.
                Location mapCenter = map.Center;

                // Updates the latitude and longitude values, in real time,
                // as the map animates to the new location.
                txtLatitude.Text = string.Format(CultureInfo.InvariantCulture,
                  "{0:F5}", mapCenter.Latitude);
                txtLongitude.Text = string.Format(CultureInfo.InvariantCulture,
                    "{0:F5}", mapCenter.Longitude);
            }
        }



        private void ChangeMapView_Click(object sender, RoutedEventArgs e)
        {
            // Parse the information of the button's Tag property
            string[] tagInfo = ((Button)sender).Tag.ToString().Split(' ');
            Location center = (Location)locConverter.ConvertFrom(tagInfo[0]);
            double zoom = System.Convert.ToDouble(tagInfo[1]);

            // Set the map view
            myMap.SetView(center, zoom);

        }


        private void AnimationLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)(((ComboBox)sender).SelectedItem);
            string v = cbi.Content as string;
            if (!string.IsNullOrEmpty(v) && myMap != null)
            {
                AnimationLevel newLevel = (AnimationLevel)Enum.Parse(typeof(AnimationLevel), v, true);
                myMap.AnimationLevel = newLevel;
            }
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var polygon = new MapPolygon();
            polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
            polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            polygon.StrokeThickness = 5;
            polygon.Opacity = 0.7;

            var locations = new LocationCollection();
            //locations.Add(new Location(43.64508171979899, -79.3930435180664));
            //locations.Add(new Location(43.64508171979899, -79.3930435180664));
            //locations.Add(new Location(43.63946054004705, -79.36819553375244));
            //locations.Add(new Location(43.652720712083266, -79.37201499938965));
            //locations.Add(new Location(43.65793702655821, -79.39111232757568));
            //locations.Add(new Location(43.64927396999741, -79.37222957611084));
            //locations.Add(new Location(43.64486433588385, -79.3791389465332));

            locations.Add(new Location(47.6424, -122.3219));
            locations.Add(new Location(47.8424, -122.1747));
            locations.Add(new Location(47.5814, -122.1747));
            //locations.Add(new Location(47.67856, -122.130994));


            polygon.Locations = locations;

            myMap.Children.Add(polygon);

            myMap.Center = locations[0];


        }


        private void myMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Disables the default mouse click action.
            e.Handled = true;

            if (_PickingLocation)
            {
                // Determin the location to place the pushpin at on the map.

                //Get the mouse click coordinates
                Point mousePosition = e.GetPosition(this);
                //Convert the mouse coordinates to a locatoin on the map
                Location pinLocation = myMap.ViewportPointToLocation(mousePosition);

                _PolygonPoints.Add(pinLocation);

            }


        }


        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Disables the default mouse double-click action.
            e.Handled = true;

            // Determin the location to place the pushpin at on the map.

            //Get the mouse click coordinates
            Point mousePosition = e.GetPosition(this);
            //Convert the mouse coordinates to a locatoin on the map
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);

            // The pushpin to add to the map.
            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;

            // Adds the pushpin to the map.
            myMap.Children.Add(pin);

        }


        private void btnPickLocation_Click(object sender, RoutedEventArgs e)
        {
            _PickingLocation = !_PickingLocation;

            if (_PickingLocation)
            {
                btnPickLocation.Content = "Stop";
                _PolygonPoints = new LocationCollection();
            }
            else
            {
                btnPickLocation.Content = "Pick";
            }
        }


        private void btnCreatePolygon_Click(object sender, RoutedEventArgs e)
        {
            if (_PickingLocation) return;

            if (_PolygonPoints == null || _PolygonPoints.Count < 3)
            {
                MessageBox.Show("You must pick up at least 3 locations to create a polygon.");
                return;
            }

            var polygon = new MapPolygon();
            polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
            polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            polygon.StrokeThickness = 5;
            polygon.Opacity = 0.7;
            polygon.Locations = _PolygonPoints;
            myMap.Children.Add(polygon);

            _PolygonPoints = null;
        }

        private void btnCheckLocationInPolygon_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}