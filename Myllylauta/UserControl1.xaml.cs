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

namespace Myllylauta
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private List<Myllynappula.UserControl1> nappulaList = new List<Myllynappula.UserControl1>();

        public UserControl1()
        {
            InitializeComponent();

            Binding bindLabelContent = new Binding("p1Nimi");
            bindLabelContent.Source = this;
            bindLabelContent.Mode = BindingMode.TwoWay;
            bindLabelContent.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            P1nimi.SetBinding(ContentProperty, bindLabelContent);

            Binding bindLabelContent2 = new Binding("p2Nimi");
            bindLabelContent2.Source = this;
            bindLabelContent2.Mode = BindingMode.TwoWay;
            bindLabelContent2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            P2nimi.SetBinding(ContentProperty, bindLabelContent2);

            Binding bindKenttaColor = new Binding("kenttaColor");
            bindKenttaColor.Source = this;
            bindKenttaColor.Mode = BindingMode.TwoWay;
            bindKenttaColor.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            

        }

        public String p1Nimi
        {
            get { return (String)GetValue(p1NimiProperty); }
            set { SetValue(p1NimiProperty, value); }
        }

        // Using a DependencyProperty as the backing store for p1Nimi.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty p1NimiProperty =
            DependencyProperty.Register("p1Nimi", typeof(String), typeof(UserControl1), new PropertyMetadata("Pelaaja1"));



        public String p2Nimi
        {
            get { return (String)GetValue(p2NimiProperty); }
            set { SetValue(p2NimiProperty, value); }
        }

        // Using a DependencyProperty as the backing store for p2Nimi.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty p2NimiProperty =
            DependencyProperty.Register("p2Nimi", typeof(String), typeof(UserControl1), new PropertyMetadata("Pelaaja2"));

        /// <summary>
        /// Muutetaan canvaksien taustaväri
        /// </summary>
        /// <param name="vari"></param>
        public void setCanvasColor(Brush vari)
        {
            foreach(UIElement child in lautaGrid.Children)
            {
                if(child is OwnCanvas)
                {
                    OwnCanvas canvas = (OwnCanvas)child;
                    canvas.Background = vari;
                }
            }
        }

        public void setpelialueColor(Brush vari)
        {
            foreach (UIElement child in lautaGrid.Children)
            {
                if (child is Line)
                {
                    Line viiva = (Line)child;
                    viiva.Stroke = vari;
                }
                if(child is Ellipse)
                {
                    Ellipse ellipsi = (Ellipse)child;
                    ellipsi.Stroke = vari;
                }
            }
        }

        /// <summary>
        /// Metodi nappuloiden checked tilan tarkistusta varten
        /// </summary>
        /// <returns></returns>
        public bool onkoNappulaaChecked()
        {
            foreach (Myllynappula.UserControl1 btn in nappulaList)
            {
                if (btn.onkoChecked == true) return true;
            }

            return false;
        }

        /// <summary>
        /// Palautetaan bindattu nappula
        /// </summary>
        /// <param name="oma"></param>
        /// <returns></returns>
        public Myllynappula.UserControl1 bindNappula(OwnCanvas oma)
        {
            Myllynappula.UserControl1 uusinappula = new Myllynappula.UserControl1();

            uusinappula.nappulaColor = nappulaVari;

            Binding bindWidht = new Binding("HalfWidth");
            bindWidht.Source = oma;
            bindWidht.Mode = BindingMode.OneWay;
            bindWidht.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            uusinappula.SetBinding(WidthProperty, bindWidht);

            Binding bindHeight = new Binding("HalfHeight");
            bindHeight.Source = oma;
            bindHeight.Mode = BindingMode.OneWay;
            bindHeight.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            uusinappula.SetBinding(HeightProperty, bindHeight);

            Binding bindCanvTop = new Binding("OneFourthsHeight");
            bindCanvTop.Source = oma;
            bindCanvTop.Mode = BindingMode.OneWay;
            bindCanvTop.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            uusinappula.SetBinding(Canvas.TopProperty, bindCanvTop);

            Binding bindCanvLeft = new Binding("OneFourthsWidth");
            bindCanvLeft.Source = oma;
            bindCanvLeft.Mode = BindingMode.OneWay;
            bindCanvLeft.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            uusinappula.SetBinding(Canvas.LeftProperty, bindCanvLeft);
            

            return uusinappula;
        }

        /// <summary>
        /// Katsotaan onko laudalla tilaa uusille nappuloille
        /// </summary>
        /// <returns></returns>
        public bool onkoTilaa()
        {
            if (nappulaList.Count < 24) return true;
            else return false;
        }
        

        /// <summary>
        /// aloitetaan uusi peli, eli tyhjennetään pelialueesta nappulat ja asetetaan 
        /// oletusvärit
        /// </summary>
        public void uusiPeli()
        {
            nappulaVari = UserControl1.nappulaVariProperty.DefaultMetadata.DefaultValue.ToString();
            nappulaList.Clear();
            voittoAani.Stop();         
            foreach(UIElement child in lautaGrid.Children)
            {
                if(child is OwnCanvas)
                {
                    OwnCanvas canv = (OwnCanvas)child;
                    canv.Background = Brushes.White;
                    foreach (UIElement lapsi in canv.Children)
                    {
                        if (lapsi is Myllynappula.UserControl1)
                        {
                            Myllynappula.UserControl1 nappula = (Myllynappula.UserControl1)lapsi;
                            canv.Children.Remove(lapsi);

                            foreach (UIElement lapsi2 in canv.Children)
                            {
                                if (lapsi2 is CheckBox)
                                {
                                    CheckBox check = (CheckBox)lapsi2;
                                    check.IsChecked = false;
                                }
                            }
                            break;
                        }
                    }
                }               
            }
        }

        /// <summary>
        /// metodi nappulan poistamiseksi canvakselta. 
        /// Aluksi etitään poistettava nappula gridin sisältämistä canvaksista,
        /// sitten poistetaan kyseinen nappula, sitten etsitään checkboxi jonka päällä
        /// nappula oli ja unchekataan se. Lähdetään breakillä pois, jotta ei suoriteta
        /// turhaan foreach lauseita loppuun.
        /// </summary>
        public void removeChild()
        {
            foreach (UIElement child in lautaGrid.Children)
            {
                if(child is OwnCanvas){
                    OwnCanvas canv = (OwnCanvas)child;
                    foreach (UIElement lapsi in canv.Children)
                    {
                        if (lapsi is Myllynappula.UserControl1)
                        {
                            Myllynappula.UserControl1 nappula = (Myllynappula.UserControl1)lapsi;
                            if (nappula.onkoChecked == true)
                            {
                                canv.Children.Remove(lapsi);
                                nappulaList.Remove(nappula);
                                foreach (UIElement lapsi2 in canv.Children)
                                {
                                    if (lapsi2 is CheckBox)
                                    {
                                        CheckBox check = (CheckBox)lapsi2;
                                        check.IsChecked = false;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }               
            } 
        }


        /// <summary>
        /// Tämä property toimii välikätenä MainWindow luokan sekä Myllynappula luokan
        /// välillä. Näin MainWindowissa voidaan muokata nappuloiden väriä pelikentällä.
        /// </summary>
        public String nappulaVari
        {
            get { return (String)GetValue(nappulaVariProperty); }
            set { SetValue(nappulaVariProperty, value); }
        }

        // Using a DependencyProperty as the backing store for nappulaVari.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty nappulaVariProperty =
            DependencyProperty.Register("nappulaVari", typeof(String), typeof(UserControl1), new PropertyMetadata("Black")); 


        /// <summary>
        /// Tämä property kertoo onko lupaa lisätä uutta nappulaa pelialueelle vai ei.
        /// Laudalla olevat nappulapaikat(checkboxit) ovat bindattu tähän propertyyn, sillä
        /// nappuloita vain nappuloita lisätessä on lupa checkata tyhjiä nappulapaikkoja.
        /// Oletuksena false.
        /// </summary>
        public bool saakoLisata
        {
            get { return (bool)GetValue(saakoLisataProperty); }
            set { SetValue(saakoLisataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for saakoLisata.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty saakoLisataProperty =
            DependencyProperty.Register("saakoLisata", typeof(bool), typeof(UserControl1), new PropertyMetadata(false));


        /// <summary>
        /// Onko lupa siirtää nappuloita vai ei
        /// </summary>
        public bool saakoSiirtaa
        {
            get { return (bool)GetValue(saakoSiirtaaProperty); }
            set { SetValue(saakoSiirtaaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for saakoSiirtaa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty saakoSiirtaaProperty =
            DependencyProperty.Register("saakoSiirtaa", typeof(bool), typeof(UserControl1), new PropertyMetadata(false));

        /// <summary>
        /// Soitetaan voittoääni kun kentälle lisätään viides nappula
        /// </summary>
        private void soitaAani()
        {
            voittoAani.Volume = 100;
            voittoAani.Play();
        }

        /// <summary>
        /// Hoitaa toistaiseksi nappulan lisäämisen pelikentälle. Lisätään nappula
        /// vain sellaisille paikoille jotka ovat checked tämän clikkauksen jälkeen ja
        /// jotka eivät sisällä jo nappulaa. Tapahtuman pitäisi laueta vain checkboxia
        /// klikatessa. Varmisestaan myös, että vain yksi nappula on valittuna kerrallaan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lautaGrid_Click(object sender, RoutedEventArgs e)
        {

            //Tässä lisätään pelilaudalle uusi nappula ja bindataan se oikean kokoiseksi
            if (e.OriginalSource is CheckBox && !(e.Source is Myllynappula.UserControl1))
            {
                
                CheckBox check = (CheckBox)e.OriginalSource;
                OwnCanvas oma = (OwnCanvas)check.Parent;

                if (check.IsChecked == true && saakoLisata == true)
                {
                    Myllynappula.UserControl1 uusinappula = new Myllynappula.UserControl1();
                    uusinappula = bindNappula(oma);
                    if(saakoSiirtaa == true)
                    {
                        if (onkoNappulaaChecked() == true)
                        {
                            foreach (Myllynappula.UserControl1 btn in nappulaList)
                            {
                                if (btn.onkoChecked == true)
                                {
                                    uusinappula = btn;
                                    removeChild();
                                    break;
                                }
                            }
                        }
                    }
                    oma.Children.Add(uusinappula);
                    nappulaList.Add(uusinappula); // lisätään nappulat listaan, jotta voidaan seurata lisättyjen nappuloiden checked tilaa
                    if (nappulaList.Count == 5) soitaAani();
                    saakoLisata = false;    // laitetaan false, jotta ei voida lisäillä nappuloita milloin vain
                    saakoSiirtaa = false;
                }
            }

            //Tämä varmistaa, että vain yksi nappula voi olla valittuna/checkattuna kerrallaan.
            //Peruutetaan lisäämis tapahtuma jos painetaan Myllynappulasta
            if (e.Source is Myllynappula.UserControl1)
            {
                saakoLisata = false;
                saakoSiirtaa = false;
                Myllynappula.UserControl1 checkattuNappula = (Myllynappula.UserControl1)e.Source;
                foreach (Myllynappula.UserControl1 nappula in nappulaList)
                {
                    if (!nappula.Equals(checkattuNappula))
                    {
                        nappula.onkoChecked = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Oma canvas-luokka, joka lisää pari propertya avuksi. HalfWidth ja HalfHeight antavat puolitetun todellisen leveyden ja korkeuden ja näihin voi bindata xamlista
    /// OneFourthsWidth ja OneFourthsHeight auttavat sijoittamaan objektit oikealle kohdalle canvaksessa.
    /// </summary>
    public class OwnCanvas : Canvas
    {
        public OwnCanvas()
        {

            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            SizeChanged += OwnCanvas_SizeChanged;
        }

        void OwnCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            HalfWidth = ActualWidth / 2;
            HalfHeight = ActualHeight / 2;
            OneFourthsWidth = ActualWidth / 4;        // Käytetään tätä ellipsin sijoittamiseen keskelle canvasta,
            OneFourthsHeight = ActualHeight / 4;      // ellipsi on puolet canvaksen koosta, joten 1/4 canvaksen koosta sijoittaa ellipsit keskelle canvasta
        }



        public string kenttaColor
        {
            get { return (string)GetValue(kenttaColorProperty); }
            set { SetValue(kenttaColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for kenttaColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty kenttaColorProperty =
            DependencyProperty.Register("kenttaColor", typeof(string), typeof(OwnCanvas), new PropertyMetadata("#FFA86300"));



        public double HalfWidth
        {
            get { return (double)GetValue(HalfWidthProperty); }
            set { SetValue(HalfWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HalfWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HalfWidthProperty =
            DependencyProperty.Register("HalfWidth", typeof(double), typeof(OwnCanvas), new PropertyMetadata(0.0));


        public double HalfHeight
        {
            get { return (double)GetValue(HalfHeightProperty); }
            set { SetValue(HalfHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HalfHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HalfHeightProperty =
            DependencyProperty.Register("HalfHeight", typeof(double), typeof(OwnCanvas), new PropertyMetadata(0.0));

        public double OneFourthsWidth
        {
            get { return (double)GetValue(OneFourthsWidthProperty); }
            set { SetValue(OneFourthsWidthProperty, value); }
        }

        public static readonly DependencyProperty OneFourthsWidthProperty =
            DependencyProperty.Register("OneFourthsWidth", typeof(double), typeof(OwnCanvas), new PropertyMetadata(0.0));

        public double OneFourthsHeight
        {
            get { return (double)GetValue(OneFourthsHeightProperty); }
            set { SetValue(OneFourthsHeightProperty, value); }
        }

        public static readonly DependencyProperty OneFourthsHeightProperty =
            DependencyProperty.Register("OneFourthsHeight", typeof(double), typeof(OwnCanvas), new PropertyMetadata(0.0));
    }
}
