﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Study.WPF.Pages
{
    /// <summary>
    /// Interaction logic for FancyStyleButton.xaml
    /// </summary>
    public partial class FancyStyleButton : Page
    {
        public FancyStyleButton()
        {
            InitializeComponent();
        }

        private void Test()
        {

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            var ddd = btnStart.IsMouseCaptureWithin;
        }
    }
}
