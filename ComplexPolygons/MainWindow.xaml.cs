using Microsoft.Maps.MapControl.WPF;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace ComplexPolygons
{
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void AddComplexPolygon_Clicked(object sender, RoutedEventArgs e)
        {
            //Clear any shapes in the map shape layer.
            MyMap.Children.Clear();

            //Create mock data
            var rings = new List<LocationCollection>(){
                new LocationCollection(){ new Location(40, -100), new Location(45, -100), new Location(45, -90), new Location(40, -90) },
                new LocationCollection(){ new Location(41, -99), new Location(43, -97), new Location(41, -95) },
                new LocationCollection(){ new Location(44, -91), new Location(43, -93), new Location(44, -95) }
            };

            //Create the colors for styling the complex polygon
            var fillColor = new SolidColorBrush(Color.FromArgb(150, 0, 255, 0));
            var strokeColor = new SolidColorBrush(Color.FromArgb(250,250, 0, 0));

            MapPolygon polygon = null;
            List<MapPolyline> outlines = null;

            //Create the complex polygon.
            CreatePolygon(rings, fillColor, strokeColor, 1, out polygon, out outlines);

            //If the polygon is not null add it to the map.
            if (polygon != null)
            {
                MyMap.Children.Add(polygon);
            }

            //If the outlines for the complex polygon are not null add them to the map.
            if (outlines != null)
            {
                foreach (var l in outlines)
                {
                    MyMap.Children.Add(l);
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates a complex polygon (polygon with holes and/or MultiPolygon). 
        /// </summary>
        /// <param name="rings">List of rings that create the polygons exterior and interior rings.</param>
        /// <param name="fillColor">Color to fill the poylgon. Set to null if outlines are only needed.</param>
        /// <param name="strokeColor">Color of outlines. Set to null if outlines are not needed.</param>
        /// <param name="width">Width of the outlines.</param>
        /// <param name="polygon">The generated polygon.</param>
        /// <param name="outlines">A list of generated polylines to out line the complex polygon rings.</param>
        public void CreatePolygon(List<LocationCollection> rings, Brush fillColor, Brush strokeColor, double width, out MapPolygon polygon, out List<MapPolyline> outlines)
        {
            outlines = new List<MapPolyline>();

            if (rings != null && rings.Count >= 0 && rings[0].Count >= 3)
            {
                //Get the first location in the first ring. This will be used as a base point for all rings.
                var baseLocation = rings[0][0];

                var exteriorRing = new LocationCollection();

                foreach (var r in rings)
                {
                    //Ensure ring is closed.
                    r.Add(r[0]);

                    if (r.Count >= 3)
                    {
                        //Add ring to main list of locations
                        foreach (var l in r)
                        {
                            exteriorRing.Add(l);
                        }

                        //Loop back to starting location
                        exteriorRing.Add(baseLocation);

                        if (strokeColor != null && width > 0)
                        {
                            //Create Polyline to outline ring
                            outlines.Add(new MapPolyline()
                            {
                                Stroke = strokeColor,
                                StrokeThickness = width,
                                StrokeLineJoin = PenLineJoin.Round,
                                StrokeEndLineCap = PenLineCap.Round,
                                StrokeStartLineCap = PenLineCap.Round,
                                Locations = r
                            });
                        }
                    }
                }

                if (fillColor != null && exteriorRing.Count > 3)
                {
                    polygon = new MapPolygon()
                    {
                        Locations = exteriorRing,
                        Fill = fillColor
                    };

                    return;
                }
            }

            polygon = null;
        }

        #endregion
    }
}