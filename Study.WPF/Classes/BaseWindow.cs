using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Study.WPF
{

    /// <summary>
    /// Define a base window which all other windows will inherent from.
    /// </summary>
    public class BaseWindow : Window
    {



        /// <summary>
        /// Constructor
        /// </summary>
        public BaseWindow() {
            //this.AllowsTransparency = true;
            this.Background = (SolidColorBrush) new BrushConverter().ConvertFrom("#f0f0f0"); // Brushes..ge.LightBlue;
            this.BorderBrush = Brushes.Red;
        }



        /// <summary>
        /// A property to check if the window is currently in design mode.
        /// </summary>
        public bool IsInDesignMode
        {
            get
            {
                return System.ComponentModel.DesignerProperties.GetIsInDesignMode(this);
            }
        }
    }
}
