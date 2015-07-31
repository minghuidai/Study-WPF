using System;
using System.Collections.Generic;
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
using Study.WPF.Controls;

namespace Study.WPF.Windows
{
    /// <summary>
    /// Interaction logic for CornerRaduisWindow.xaml
    /// </summary>
    public partial class CornerRaduisWindow : DialogShell
    {
        public CornerRaduisWindow()
        {
            InitializeComponent();

            //this.WindowStyle = System.Windows.WindowStyle.None;
            //this.Background = new SolidColorBrush(Colors.Transparent);
            //BorderThickness = new Thickness(1);
            //AllowsTransparency = true;



             //WindowStyle="None" Background="Transparent" BorderThickness="1" BorderBrush="Transparent" AllowsTransparency="False" 
        }

    }
}
