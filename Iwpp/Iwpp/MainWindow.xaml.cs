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

namespace Iwpp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void licz_Click(object sender, RoutedEventArgs e)
        { 
            
           

            Double wysokkoscW = Double.Parse(wysokosc.Text);
            Double szerokoscS= Double.Parse(szerokosc.Text);
            Double gestoscCiekla = Double.Parse(g_stali_ciekle.Text);
            Double gestoscStali = Double.Parse(g_stali_stale.Text);
            Double wspolczynnikSkali = Double.Parse(skala_lewa.Text) / Double.Parse(skalwa_prawa.Text);
            Double predkoscWlewania = Double.Parse(predkosc_wlewania.Text);


            Double przeplyw;

            przeplyw = wysokkoscW * szerokoscS * (gestoscCiekla / gestoscStali) * wspolczynnikSkali * 60 / 1000;

            przeplyw_obj_l_m.Text = przeplyw.ToString();

                //Q = A * v * (gęstość cieczy w stanie ciekłym / gęstość stali) * współczynnik skali liniowej * 60 / 1000
        }
    }
}
