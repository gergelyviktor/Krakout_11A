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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Krakout_11A {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        int xSeb = 5;
        int ySeb = 5;
        int labdaSzelesseg = 30;
        int utoSzelesseg = 100;
        public MainWindow() {
            InitializeComponent();
            var ido = new DispatcherTimer();
            ido.Interval = TimeSpan.FromMilliseconds(1);
            ido.Tick += idoLepes;
            ido.Start();
        }

        private void idoLepes(object sender, EventArgs e) {
            // 1. balról és jobbról forduljon vissza
            if (Canvas.GetLeft(labda) > 950 || Canvas.GetLeft(labda) < 0) 
                xSeb = xSeb * -1;
            // 2. fentről és lentről is forduljon vissza
            if (Canvas.GetTop(labda) > 560 || Canvas.GetTop(labda) < 0)
                ySeb = ySeb * -1;
            // mozgatás
            Canvas.SetLeft(labda, Canvas.GetLeft(labda) + xSeb);
            Canvas.SetTop(labda, Canvas.GetTop(labda) + ySeb);
            // ütő mozgatás

        }
    }
}
