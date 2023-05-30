using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Iwpp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<DaneZPliku> daneZPlikow { get; set; }

        String przeplywObj;
        String skalaPrzeplywu;
        String przeplywWJednymLnaMin;
        String przeplywMs;
        String przeplywWJednym;
        String ObjwMin;

        String gestoscStala;
        String gestoscCiekla;

        Boolean czyJestJuzPlik;

        //TODO DODATKOWA WALIDACJA


        public MainWindow()
        {
            InitializeComponent();
            daneZPlikow = new ObservableCollection<DaneZPliku>();
            data_grid.ItemsSource = daneZPlikow;
        }

        private void zapiszDoPliku(String tabelowe, String dane)
        {
            String nazwaPliku = nazwa_pliku.Text+".xml";

            if (!File.Exists(nazwaPliku)){
                File.WriteAllText(nazwaPliku, tabelowe + '\n' + dane);
            }
            else
            File.AppendAllText(nazwaPliku, '\n' + dane);

        }

    

   
        private void licz_Click(object sender, RoutedEventArgs e)
        {

            //TODO PRAWIDLOWE WZORY


            Double wysokkoscW = Double.Parse(wysokosc.Text);
            Double szerokoscS= Double.Parse(szerokosc.Text);
            Double wspolczynnikSkali = Double.Parse(skala_lewa.Text) / Double.Parse(skalwa_prawa.Text);
            Double predkoscWlewania = Double.Parse(predkosc_wlewania.Text);
            Double iloscZyl= Double.Parse(ilosc_zyl.Text);


            Double przeplyw;
           

            Double skalaPrzeplywu=(predkoscWlewania*wspolczynnikSkali)*wspolczynnikSkali*wspolczynnikSkali;


            Double gestoscCiekla = Double.Parse(g_stali_ciekle.Text);

            Double gestoscStali = Double.Parse(g_stali_stale.Text);


            przeplyw = wysokkoscW * szerokoscS * (gestoscCiekla / gestoscStali) * wspolczynnikSkali * predkoscWlewania * 60 / 1000;


            Double przeplywPrzezIloscZyl = przeplyw / iloscZyl;
            Double przeplywWMetrach = przeplyw * 0.001 / 60*gestoscCiekla;
            Double przeplywWMetrachDlaJednejZyly = przeplywWMetrach / iloscZyl;
            Double przeplywWMetrachNaMinute = przeplyw * 0.001 * gestoscCiekla;

            

            przeplyw_obj_l_m.Text = przeplyw.ToString("G4");
            skala_przeplywu.Text = skalaPrzeplywu.ToString();
            przeplyw_jednym_wlewie_l_min.Text = przeplywPrzezIloscZyl.ToString("G4");
            przeplyw_obj_m_s.Text = przeplywWMetrach.ToString("G4");
            przeplyw_jednym_wlewie_m_s.Text= przeplywWMetrachDlaJednejZyly.ToString("G4");
            przeplyw_obj_m_min.Text= przeplywWMetrachNaMinute.ToString("G4");

            this.przeplywObj= przeplyw.ToString(); ;
            this.skalaPrzeplywu=skalaPrzeplywu.ToString(); ;
            this.przeplywWJednymLnaMin=przeplywPrzezIloscZyl.ToString(); ;
            this.przeplywMs= przeplywWMetrach.ToString(); ;
            this.przeplywWJednym=przeplywWMetrachDlaJednejZyly.ToString(); ;
            this.ObjwMin = przeplywWMetrachNaMinute.ToString(); 


            //Q = A * v * (gęstość cieczy w stanie ciekłym / gęstość stali) * współczynnik skali liniowej * 60 / 1000


            DaneZPliku daneZPliku = new DaneZPliku();
            daneZPliku.ObjetoscwMin = this.ObjwMin;
            daneZPliku.PrzeplywWJednym = this.przeplywMs;
            daneZPliku.PrzeplywObjetosciowy = this.przeplywObj;
            daneZPliku.SkalaPrzeplywu = this.skalaPrzeplywu;
            daneZPliku.PrzeplywWMs = this.przeplywMs;

            daneZPlikow.Add(daneZPliku);
               

            zapisz_btn.IsEnabled=true;
        }

        private void zapisz_btn_Click(object sender, RoutedEventArgs e)
        {
            String przeplywObj =
                this.przeplywObj + " | " + this.skalaPrzeplywu + " | " + this.przeplywWJednymLnaMin + " | " + this.przeplywMs + " | " + this.przeplywWJednym + " | " + this.ObjwMin + " | ";
            String tabelowe=" Przepływ Objetosciowy "+ " | "+ " Skala Przeplywu " + " | " + " Przepływ w jednym " + " | " + " Przepływ w m/s " + " | " + " przepływ w jednym " + " | " + " Objetosc w Min " + " | ";

            zapiszDoPliku(tabelowe,przeplywObj);
        }

        private void szerokosc_TextChanged(object sender, TextChangedEventArgs e)
        {

            //TODO ZMIANA WARTOSCI W PRZEPRLYWACH DYNAMICZNIE WG WZORU

            masowy_przeplyw_staly.Text = "Wartosc";
            obj_przepl_stali.Text = "Wartosc";

            zapisz_btn.IsEnabled = false;
        }

        private void wysokosc_TextChanged(object sender, TextChangedEventArgs e)
        {

            //TODO ZMIANA WARTOSCI W PRZEPRLYWACH DYNAMICZNIE WG WZORU


            masowy_przeplyw_staly.Text = "Wartosc2";
            obj_przepl_stali.Text = "Wartosc2";

            zapisz_btn.IsEnabled = false;

        }

        private void ilosc_zyl_TextChanged(object sender, TextChangedEventArgs e)
        {

            //TODO ZMIANA WARTOSCI W PRZEPRLYWACH DYNAMICZNIE WG WZORU



            masowy_przeplyw_staly.Text = "Wartosc3";
            obj_przepl_stali.Text = "Wartosc3";
            zapisz_btn.IsEnabled = false;

        }

        private void predkosc_wlewania_TextChanged(object sender, TextChangedEventArgs e)
        {

            //TODO ZMIANA WARTOSCI W PRZEPRLYWACH DYNAMICZNIE WG WZORU


            masowy_przeplyw_staly.Text = "Wartosc4";
            obj_przepl_stali.Text = "Wartosc4";
            zapisz_btn.IsEnabled = false;

        }

        private void g_stali_stale_TextChanged(object sender, TextChangedEventArgs e)
        {

            //TODO ZMIANA WARTOSCI W PRZEPRLYWACH DYNAMICZNIE WG WZORU


            masowy_przeplyw_staly.Text = "Wartosc6";
            obj_przepl_stali.Text = "Wartosc6";
            zapisz_btn.IsEnabled = false;

        }

        private void g_stali_ciekle_TextChanged(object sender, TextChangedEventArgs e)
        {


            //TODO ZMIANA WARTOSCI W PRZEPRLYWACH DYNAMICZNIE WG WZORU


            masowy_przeplyw_staly.Text = "Wartosc7";
            obj_przepl_stali.Text = "Wartosc7";

            zapisz_btn.IsEnabled = false;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nazwa_pliku.Text.Equals(""))
            {
                zapisz_btn.IsEnabled = false;
            }
            else zapisz_btn.IsEnabled=true;
        }
    }
}
