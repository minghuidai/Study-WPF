using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Study.WPF
{

    /// <summary>
    /// Defines the commands used for bing map controls
    /// </summary>
    public static class MapCommands
    {
        public static RoutedCommand ZoomIn = new RoutedCommand();
        public static RoutedCommand ZoomOut = new RoutedCommand();
        public static RoutedCommand SetRoadMode = new RoutedCommand();
        public static RoutedCommand SetAerialMode = new RoutedCommand();

        static MapCommands()
        {
            //d = new MapZoomInCommandBinding();
        }
 
    }



    /// <summary>
    /// Zoom In command binding
    /// </summary>
    public sealed class MapZoomInCommandBinding : CommandBinding
    {

        public MapZoomInCommandBinding() : base(MapCommands.ZoomIn, OnExecute, OnCanExecute)
        { }

        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Map element = sender as Map;
            element.ZoomLevel += 1;
        }

        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }

    public sealed class MapZoomOutCommandBinding : CommandBinding
    {

        public MapZoomOutCommandBinding()
            : base(MapCommands.ZoomOut, OnExecute, OnCanExecute)
        { }

        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Map element = sender as Map;
            element.ZoomLevel -= 1;
        }

        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }

    public sealed class SetRoadModeCommandBinding : CommandBinding
    {

        public SetRoadModeCommandBinding()
            : base(MapCommands.SetRoadMode, OnExecute, OnCanExecute)
        { }

        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Map element = sender as Map;
            element.Mode = new RoadMode();
        }

        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Map element = sender as Map;
            e.CanExecute = element.Mode.GetType() != typeof(RoadMode);
        }

    }

    public sealed class SetAerialModeCommandBinding : CommandBinding
    {

        public SetAerialModeCommandBinding()
            : base(MapCommands.SetAerialMode, OnExecute, OnCanExecute)
        { }

        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Map element = sender as Map;
            element.Mode = new AerialMode();
        }

        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Map element = sender as Map;
            e.CanExecute = element.Mode.GetType() != typeof(AerialMode);
        }

    }


}
