using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LedCSharp;

namespace LcdControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DoLogic();
        }

        public void DoLogic()
        {
            //Server server = new Server(13373);
            //server.AcceptLoop();

            LogitechGSDK.LogiLedInit();
            LogitechGSDK.LogiLedPulseLighting(0, 100, 0, LogitechGSDK.LOGI_LED_DURATION_INFINITE, 20);
        }
    }
}
