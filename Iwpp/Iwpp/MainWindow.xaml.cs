﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
        String nazwaPliku;
        String wysokoscw;
        String szerokoscw;
        String iloscZyl;
        String predkoscWylewania;
        String gestoscStaliWStanieStalym;
        String gestoscStaliWStanieCieklym;



        Boolean czyJestJuzPlik;

        //TODO DODATKOWA WALIDACJA


        public MainWindow()
        {
            InitializeComponent();
            daneZPlikow = new ObservableCollection<DaneZPliku>();
            data_grid.ItemsSource = daneZPlikow;
            przeplyw_obj_l_m.Background= new SolidColorBrush(Colors.LightBlue);
            przeplyw_obj_l_m.Background = new SolidColorBrush(Colors.LightBlue);
            skala_przeplywu.Background = new SolidColorBrush(Colors.LightBlue);
            przeplyw_jednym_wlewie_l_min.Background = new SolidColorBrush(Colors.LightBlue);
            przeplyw_obj_m_s.Background = new SolidColorBrush(Colors.LightBlue);
            przeplyw_jednym_wlewie_m_s.Background = new SolidColorBrush(Colors.LightBlue);
            przeplyw_obj_l_h.Background = new SolidColorBrush(Colors.LightBlue);

            wysokosc.Background = new SolidColorBrush(Colors.LightBlue);
            szerokosc.Background = new SolidColorBrush(Colors.LightBlue);
            ilosc_zyl.Background = new SolidColorBrush(Colors.LightBlue);
            predkosc_wlewania.Background = new SolidColorBrush(Colors.LightBlue);
            g_stali_stale.Background = new SolidColorBrush(Colors.LightBlue);
            g_stali_ciekle.Background = new SolidColorBrush(Colors.LightBlue);
            masowy_przeplyw_staly.Background = new SolidColorBrush(Colors.LightBlue);
            obj_przepl_stali.Background = new SolidColorBrush(Colors.LightBlue);
            skala_lewa.Background = new SolidColorBrush(Colors.LightBlue);
            predkosc_wlewania.Background = new SolidColorBrush(Colors.LightBlue);
            nazwa_pliku.Background = new SolidColorBrush(Colors.LightBlue);
            skalwa_prawa.Background = new SolidColorBrush(Colors.LightBlue);
           
        }

        private void zapiszDoPliku(String tabelowe, String dane)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (.xml)|.xml";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                String nazwaPliku = saveFileDialog.FileName;

                if (!File.Exists(nazwaPliku))
                {
                    File.WriteAllText(nazwaPliku, tabelowe + '\n' + dane);
                }
                else
                    File.AppendAllText(nazwaPliku, '\n' + dane);
            }
        }

        private void licz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //TODO PRAWIDLOWE WZORY

                Double wysokoscW = Double.Parse(wysokosc.Text);
                Double szerokoscS = Double.Parse(szerokosc.Text);
                Double wspolczynnikSkali = Double.Parse(skala_lewa.Text) / Double.Parse(skalwa_prawa.Text);
                Double predkoscWlewania = Double.Parse(predkosc_wlewania.Text);
                Double iloscZyl = Double.Parse(ilosc_zyl.Text);
                Double gestoscCiekla = Double.Parse(g_stali_ciekle.Text);
                Double gestoscStali = Double.Parse(g_stali_stale.Text);

                //POPRAWIONE
                Double skalaPrzeplywu = Math.Pow(wspolczynnikSkali, 5.0 / 2);
                //POPRAWIONE
                Double masowyPrzeplywStaliWSS = iloscZyl * wysokoscW * szerokoscS * (predkoscWlewania / 60) * 7600;
                //POPRAWIONE
                Double objetosciowyPrzeplywWSC = masowyPrzeplywStaliWSS / 7000;
                //POPRAWIONE
                Double przeplyw = skalaPrzeplywu * objetosciowyPrzeplywWSC;

                Double przeplywWLitrachNaminute = przeplyw * 60000;
                Double przeplywPrzezIloscZyl = przeplyw / iloscZyl;
                Double przeplywWLitrachNaGodzine = przeplyw * 3600000;
                Double przeplywWMetrachDlaJednejZyly = przeplyw / iloscZyl;
                Double przeplywWMetrachNaMinute = przeplyw * 0.001;
                Double przeplywWLitrachNaMinuteDlaEJdnejZyly = przeplywWLitrachNaminute / iloscZyl;

                przeplyw_obj_l_m.Text = przeplywWLitrachNaminute.ToString("F4");
                skala_przeplywu.Text = skalaPrzeplywu.ToString("F4");
                przeplyw_jednym_wlewie_l_min.Text = przeplywWLitrachNaMinuteDlaEJdnejZyly.ToString("F4");
                przeplyw_obj_m_s.Text = przeplyw.ToString("F4");
                przeplyw_jednym_wlewie_m_s.Text = przeplywPrzezIloscZyl.ToString("F4");
                przeplyw_obj_l_h.Text = przeplywWLitrachNaGodzine.ToString("F4");

                this.przeplywObj = przeplyw.ToString(); ;
                this.skalaPrzeplywu = skalaPrzeplywu.ToString(); ;
                this.przeplywWJednymLnaMin = przeplywPrzezIloscZyl.ToString(); ;
                this.przeplywMs = przeplywWLitrachNaminute.ToString(); ;
                this.przeplywWJednym = przeplywWMetrachDlaJednejZyly.ToString(); ;
                this.ObjwMin = przeplywWMetrachNaMinute.ToString();
                this.wysokoscw = wysokoscW.ToString();
                this.szerokoscw = szerokoscS.ToString();
                this.iloscZyl = iloscZyl.ToString();
                this.predkoscWylewania = predkoscWlewania.ToString();
                this.gestoscStaliWStanieStalym = gestoscStali.ToString();
                this.gestoscStaliWStanieCieklym = gestoscCiekla.ToString();

               

                DaneZPliku daneZPliku = new DaneZPliku();
                daneZPliku.NazwaPliku = this.nazwaPliku;
                daneZPliku.Wysokosc = this.wysokoscw;
                daneZPliku.Szerokosc = this.szerokoscw;
                daneZPliku.IloscZył = this.iloscZyl;
                daneZPliku.PredkoscWylewania = this.predkoscWylewania;
                daneZPliku.GęstoscStaliWStanieStalym = this.gestoscStaliWStanieStalym;
                daneZPliku.GęstoscStaliWStanieCieklym = this.gestoscStaliWStanieCieklym;
                daneZPliku.ObjetoscwMin = this.ObjwMin;
                daneZPliku.PrzeplywWJednym = this.przeplywMs;
                daneZPliku.PrzeplywObjetosciowy = this.przeplywObj;
                daneZPliku.SkalaPrzeplywu = this.skalaPrzeplywu;
                daneZPliku.PrzeplywWMs = this.przeplywMs;


                daneZPlikow.Add(daneZPliku);


                zapisz_btn.IsEnabled = true;
            }catch(Exception dfg)
            {

            }
        }

        private void zapisz_btn_Click(object sender, RoutedEventArgs e)
        {
            String przeplywObj = this.wysokosc.Text + " | " + this.szerokosc.Text + " | " + this.ilosc_zyl.Text + " | " + this.g_stali_ciekle.Text + " | " + this.g_stali_stale.Text + " | " + this.predkosc_wlewania.Text + " | " + this.masowy_przeplyw_staly.Text + " | " + this.obj_przepl_stali.Text + " | " +
                this.przeplywObj + " | " + this.skalaPrzeplywu + " | " + this.przeplywWJednymLnaMin + " | " + this.przeplywMs + " | " + this.przeplywWJednym + " | " + this.ObjwMin + " | ";
            String tabelowe = "Szerokość " + " | " + "Wysokość " + " | " + "Ilość żył " + " | " + "Gęstość stali w stanie stałym " + " | " + "Gęstość stali w stanie ciekłym " + " | " + "Prędkość wylewania " + " | " + "Masowy Przepływ stali " + " | " + "Obiętościowy przepływ stali " + " | " +
                "Przepływ Objetosciowy " + " | " + " Skala Przeplywu " + " | " + " Przepływ w jednym " + " | " + " Przepływ w m/s " + " | " + " przepływ w jednym " + " | " + " Objetosc w Min " + " | ";

            zapiszDoPliku(tabelowe, przeplywObj);
        }

        private void szerokosc_TextChanged(object sender, TextChangedEventArgs e)
        {
            oblicz();

            zapisz_btn.IsEnabled = false;
        }

        private void wysokosc_TextChanged(object sender, TextChangedEventArgs e)
        {

            oblicz();

            zapisz_btn.IsEnabled = false;

        }

        private void ilosc_zyl_TextChanged(object sender, TextChangedEventArgs e)
        {
            oblicz();

            zapisz_btn.IsEnabled = false;

        }

        private void predkosc_wlewania_TextChanged(object sender, TextChangedEventArgs e)
        {
            oblicz();

            zapisz_btn.IsEnabled = false;

        }

        private void g_stali_stale_TextChanged(object sender, TextChangedEventArgs e)
        {


            oblicz();
            zapisz_btn.IsEnabled = false;

        }

        private void g_stali_ciekle_TextChanged(object sender, TextChangedEventArgs e)
        {


            oblicz();



            zapisz_btn.IsEnabled = false;

        }
        private void oblicz()
        {
            try
            {
                Double wysokkoscW = Double.Parse(wysokosc.Text);
                Double szerokoscS = Double.Parse(szerokosc.Text);
                Double predkoscWlewania = Double.Parse(predkosc_wlewania.Text);
                Double iloscZyl = Double.Parse(ilosc_zyl.Text);
                Double gestoscCiekla = Double.Parse(g_stali_ciekle.Text);
                Double gestoscStali = Double.Parse(g_stali_stale.Text);


                //TODO ZMIANA WARTOSCI W PRZEPRLYWACH DYNAMICZNIE WG WZORU
                Double masowyPrzeplywStaliWSS = iloscZyl * wysokkoscW * szerokoscS * (predkoscWlewania / 60) * gestoscStali;


                if (!wysokkoscW.Equals(null) && !szerokoscS.Equals(null) && !predkoscWlewania.Equals(null) && !iloscZyl.Equals(null) && !gestoscCiekla.Equals(null) && !gestoscStali.Equals(null))
                {
                    masowy_przeplyw_staly.Text = masowyPrzeplywStaliWSS.ToString("F4");
                    obj_przepl_stali.Text = (masowyPrzeplywStaliWSS / gestoscCiekla).ToString("F4");
                }
            }
            catch (Exception e)
            {

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nazwa_pliku.Text.Equals(""))
            {
                zapisz_btn.IsEnabled = false;
            }
            else
            {
                zapisz_btn.IsEnabled = true;
                this.nazwaPliku = nazwa_pliku.Text;
            }
        }



        private void masowy_przeplyw_staly_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
