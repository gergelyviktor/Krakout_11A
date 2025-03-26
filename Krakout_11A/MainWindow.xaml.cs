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
using System.Windows.Controls;

namespace Krakout_11A {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        double xSeb = 5;
        double ySeb = 5;
        int pontok = 0;
        int teglakSzamaSoronkent = 10;
        int sorokSzama = 5;
        public MainWindow() {
            InitializeComponent();
            // sorok megjelenítése
            for (int j = 0; j < sorokSzama; j++) {
                // egy sornyi tégla
                for (int i = 0; i < teglakSzamaSoronkent; i++) {
                    var tegla = new Image();
                    tegla.Source = new BitmapImage(new Uri("/kepek/tegla.jpg", UriKind.Relative));
                    tegla.Width = 90;
                    tegla.Height = 20;
                    tegla.Stretch = Stretch.Fill;
                    jatekter.Children.Add(tegla);
                    Canvas.SetLeft(tegla, i * 100);
                    Canvas.SetTop(tegla, j * 30);
                }
            }

            //
            var ido = new DispatcherTimer();
            ido.Interval = TimeSpan.FromMilliseconds(1);
            ido.Tick += idoLepes;
            ido.Start();
        }

        private void idoLepes(object sender, EventArgs e) {
            // 1. balról és jobbról forduljon vissza
            if (Canvas.GetLeft(labda) > 950 || Canvas.GetLeft(labda) < 0)
                xSeb = xSeb * -1;
            // 2. fentről forduljon vissza
            if (Canvas.GetTop(labda) < 0)
                ySeb = ySeb * -1;
            // 2.1 alul menjen ki és rakja vissza az ütő fölé a labdát
            if (Canvas.GetTop(labda) > 600) {
                Canvas.SetLeft(labda, 450);
                Canvas.SetTop(labda, 200);
                ySeb = 5;
            }
            // 3. ütőmozgatás, de úgy, hogy ne menjen ki az ütő a szélén
            var egerPozicio = Mouse.GetPosition(jatekter).X;
            if (egerPozicio > 0 && egerPozicio < 950) {
                // ütő mozgatás
                Canvas.SetLeft(uto, egerPozicio);
            }
            // 4. ütközésvizsgálat a labda és az ütő között
            var utoX = Canvas.GetLeft(uto);
            var utoY = Canvas.GetTop(uto);
            var labdaX = Canvas.GetLeft(labda);
            var labdaY = Canvas.GetTop(labda);
            if (labdaX + labda.Width > utoX
                && labdaX < utoX + uto.Width
                && labdaY + labda.Height > utoY
                && labdaY < utoY + uto.Height
                ) {
                ySeb *= -1.3;
                pontszam.Content = ++pontok;
            }
            // 5. ütközésvizsgálat a labda és a tégla között
            foreach (var tegla in jatekter.Children.OfType<Image>()) {
                var teglaX = Canvas.GetLeft(tegla);
                var teglaY = Canvas.GetTop(tegla);
                if (labdaX + labda.Width > teglaX
                    && labdaX < teglaX + tegla.Width
                    && labdaY + labda.Height > teglaY
                    && labdaY < teglaY + tegla.Height
                    ) {
                    ySeb *= -1;
                    jatekter.Children.Remove(tegla);
                    pontszam.Content = ++pontok;
                    break;
                }
            }
            // labda mozgatás
            Canvas.SetLeft(labda, Canvas.GetLeft(labda) + xSeb);
            Canvas.SetTop(labda, Canvas.GetTop(labda) + ySeb);
        }
    }
}
