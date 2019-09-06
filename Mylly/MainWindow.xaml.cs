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

namespace Mylly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Command nappuloiden lisäykselle
            CommandBinding customCommandBinding = new CommandBinding(CustomRoutedCommand, ExecutedSelect, CanExecuteSelect);
            this.CommandBindings.Add(customCommandBinding);
            //Command nappuloiden poistamiselle
            CommandBinding customCommandBindingPoista = new CommandBinding(CustomRoutedCommandPoista, RemoveNappulaExecuted, RemoveNappulaCanExecute);
            this.CommandBindings.Add(customCommandBindingPoista);
            //Command nappuloiden siirrolle
            CommandBinding customCommandBindingSiirra = new CommandBinding(CustomRoutedCommandSiirra, SiirraNappulaExecuted, SiirraNappulaCanExecute);
            this.CommandBindings.Add(customCommandBindingSiirra);
        }

        public static RoutedCommand CustomRoutedCommand = new RoutedCommand();
        public static RoutedCommand CustomRoutedCommandPoista = new RoutedCommand();
        public static RoutedCommand CustomRoutedCommandSiirra = new RoutedCommand();

        /// <summary>
        /// katotaan onko lupaa siirtää nappulaa, mikäli laudalla on tilaa ja
        /// nappula on valittuna, voidaan siirtää
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SiirraNappulaCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (peliAlue.onkoTilaa() == true && peliAlue.onkoNappulaaChecked() == true) e.CanExecute = true;

        }

        /// <summary>
        /// Siirretään nappula
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SiirraNappulaExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            peliAlue.saakoSiirtaa = true;
            peliAlue.saakoLisata = true;
        }

        /// <summary>
        /// aktivoidaan poistamiseen liittyvät controllit mikäli mikään nappula on checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RemoveNappulaCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = peliAlue.onkoNappulaaChecked();
        }

        /// <summary>
        /// Annetaan lupa poistaa nappula, katsotaan mikä nappula on checked
        /// ja kutsutaan Myllylaudan removeChild metodia nappulan poistamista varten.
        /// Poistetaan nappula myös nappuloiden listauksesta, jota käytetään edellä
        /// mainittujen checkedien tarkistamiseen. Lähdetään breakillä pois, jotta
        /// ei suoriteta turhaan ylimääräisiä silmukoita. Ohjelma myös kaatuu ilman breikkiä.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RemoveNappulaExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            peliAlue.removeChild();
        }

        /// <summary>
        /// Tarkistetaan onko laudalla tilaa uusille nappuloille, jos on,
        /// canExecute on true, muuten false ja tällöin lisaaNappula otetaan pois
        /// käytöstä
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CanExecuteSelect(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = peliAlue.onkoTilaa();
        }

        /// <summary>
        /// annetaan lupa lisata nappula pelikentälle 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExecutedSelect(object sender, ExecutedRoutedEventArgs e)
        {         
            peliAlue.saakoLisata = true;
        }


        /// <summary>
        /// avataan about box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutBox_Click(object sender, RoutedEventArgs e)
        {
            AboutBox1 tietoja = new AboutBox1();
            tietoja.ShowDialog();
        }

        /// <summary>
        /// Avataan avustuksesta linkki joka selittää miten peliä pelataan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Avustus_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://fi.wikipedia.org/wiki/Mylly_(peli)");
        }


        /// <summary>
        /// Tulostus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(peliAlue, "Tulostusalue");
            }
        }

        /// <summary>
        /// Avataan itse luotu ohjelma joka sisältää kaksi väridialogia ja 
        /// pelaajien nimet. Nimet ja värit muokattavissa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSettings_Click(object sender, RoutedEventArgs e)
        {

            OmaColorDialog.MainWindow omaVariDialog = new OmaColorDialog.MainWindow();       
            if(omaVariDialog.ShowDialog() == true)
            {
                if(omaVariDialog.nappulaVari != null)
                {
                    peliAlue.nappulaVari = omaVariDialog.nappulaVari.ToString();                 
                }
                if(omaVariDialog.taustaVari != null)
                {
                    BrushConverter conv = new BrushConverter();
                    Brush vari = (Brush)conv.ConvertFromString(omaVariDialog.taustaVari);
                    peliAlue.setCanvasColor(vari);
                }
                if(omaVariDialog.pelialueVari != null)
                {
                    //TODO: bindaa omacolordialogin värit pelialueen checkboxeihin ja viivoihin
                    BrushConverter conv = new BrushConverter();
                    Brush vari = (Brush)conv.ConvertFromString(omaVariDialog.pelialueVari);
                    peliAlue.setpelialueColor(vari);
                }
                if(omaVariDialog.pelaaja1Nimi != null && omaVariDialog.pelaaja1Nimi != "") peliAlue.p1Nimi = omaVariDialog.pelaaja1Nimi;
                if (omaVariDialog.pelaaja2Nimi != null && omaVariDialog.pelaaja2Nimi != "") peliAlue.p2Nimi = omaVariDialog.pelaaja2Nimi;

            }
            
        }

        /// <summary>
        /// aloitetaan uusi peli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuNewGame_Click(object sender, RoutedEventArgs e)
        {
            peliAlue.uusiPeli();
        }
    }
}
