using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using Study.BingMap.Query;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Study.WPF.Pages
{
 
    public partial class MapTestPage : Page
    {

        LocationConverter locConverter = new LocationConverter();
        bool _PickingLocation = false;
        LocationCollection _PolygonPoints = null;

        public MapTestPage()
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

        private void btnTileLayer_Click(object sender, RoutedEventArgs e)
        {
            AddTileOverlay();
        }



        private void AddTileOverlay()
        {

            // Create a new map layer to add the tile overlay to.
            var tileLayer = new MapTileLayer();

            // The source of the overlay.
            TileSource tileSource = new TileSource();
            tileSource.UriFormat = "{UriScheme}://ecn.t0.tiles.virtualearth.net/tiles/r{quadkey}.jpeg?g=129&mkt=en-us&shading=hill&stl=H";

            // Add the tile overlay to the map layer
            tileLayer.TileSource = tileSource;




            // Add the map layer to the map
            if (!myMap.Children.Contains(tileLayer))
            {
                myMap.Children.Add(tileLayer);
            }
            tileLayer.Opacity = 0.2;
        }



        private void test()
        {
            //Microsoft.Maps.MapControl.WPF.Core.
        }



        private void btnDrawBoundries_Click(object sender, RoutedEventArgs e)
        {
            var locations = new LocationCollection();
            locations.Add(new Location(34.064914703369141, -84.093452453613281));

            MapPolygon p = new MapPolygon()
            {


            };

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

        private void btnDrawBoundry_Click(object sender, RoutedEventArgs e)
        {
            string place = txtPlace.Text;
            string level = txtLevel.Text;

            var bingservice = new BingMapQuery("Ai6zQ5AwxFAZKY3DtRmKAPJHZVlK4h_e01jNbblWGbagsXzwH0nf5vYrTEr13kBd");

            var locations = bingservice.GetBoundries(place, level);

            if (locations != null)
            {
                var polygon = new MapPolygon();
                polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
                polygon.StrokeThickness = 2;
                polygon.Opacity = 0.7;
                polygon.Locations = locations;
                myMap.Children.Add(polygon);

                myMap.SetView(locations[0], myMap.ZoomLevel);
            }


        }

        private void btReset_Click(object sender, RoutedEventArgs e)
        {
            myMap.Children.Clear();
        }

    }
}
