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

namespace OmaColorDialog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            Binding variNappulaBind = new Binding();
            variNappulaBind.Source = this;
            variNappulaBind.Path = new PropertyPath("nappulaVari");
            variNappulaBind.Mode = BindingMode.OneWay;
            variNappulaBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            p1colorbutton.SetBinding(Button.ContentProperty, variNappulaBind);
            p2colorbutton.SetBinding(Button.ContentProperty, variNappulaBind);

            Binding variTaustaBind = new Binding();
            variTaustaBind.Source = this;
            variTaustaBind.Path = new PropertyPath("taustaVari");
            variTaustaBind.Mode = BindingMode.OneWay;
            variTaustaBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            taustaButton.SetBinding(Button.ContentProperty, variTaustaBind);

            Binding varipelialueBind = new Binding();
            varipelialueBind.Source = this;
            varipelialueBind.Path = new PropertyPath("pelialueVari");
            varipelialueBind.Mode = BindingMode.OneWay;
            varipelialueBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            pelialueButton.SetBinding(Button.ContentProperty, varipelialueBind);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {                    
            this.DialogResult = true;
        }

        /// <summary>
        /// taustan värin valitseminen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taustaButton_Click(object sender, RoutedEventArgs e)
        {
            var dialogVari = new System.Windows.Forms.ColorDialog();

            if (dialogVari.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor1 = Color.FromArgb(dialogVari.Color.A, dialogVari.Color.R, dialogVari.Color.G, dialogVari.Color.B);
                var brush1 = new SolidColorBrush(wpfColor1);
                taustaButton.Content = brush1.ToString();
                taustaVari = taustaButton.Content.ToString();
            }

        }

        /// <summary>
        /// p1 nappulan värin valitseminen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nappulaButton1_Click(object sender, RoutedEventArgs e)
        {
            var dialogVari = new System.Windows.Forms.ColorDialog();

            if (dialogVari.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor1 = Color.FromArgb(dialogVari.Color.A, dialogVari.Color.R, dialogVari.Color.G, dialogVari.Color.B);
                var brush1 = new SolidColorBrush(wpfColor1);
                p1colorbutton.Content = brush1.ToString();
                nappulaVari = p1colorbutton.Content.ToString();
            }
        }

        /// <summary>
        /// p2 nappulan värin valitseminen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nappulaButton2_Click(object sender, RoutedEventArgs e)
        {
            var dialogVari = new System.Windows.Forms.ColorDialog();

            if (dialogVari.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor1 = Color.FromArgb(dialogVari.Color.A, dialogVari.Color.R, dialogVari.Color.G, dialogVari.Color.B);
                var brush1 = new SolidColorBrush(wpfColor1);
                p2colorbutton.Content = brush1.ToString();
                nappulaVari = p2colorbutton.Content.ToString();
            }
        }

        /// <summary>
        /// pelialueen värin valitseminen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pelialueButton_Click(object sender, RoutedEventArgs e)
        {
            var dialogVari = new System.Windows.Forms.ColorDialog();

            if (dialogVari.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor1 = Color.FromArgb(dialogVari.Color.A, dialogVari.Color.R, dialogVari.Color.G, dialogVari.Color.B);
                var brush1 = new SolidColorBrush(wpfColor1);
                pelialueButton.Content = brush1.ToString();
                pelialueVari = pelialueButton.Content.ToString();
                
            }
        }

        public String taustaVari
        {
            get { return (String)GetValue(taustaVariProperty); }
            set { SetValue(taustaVariProperty, value); }
        }

        // Using a DependencyProperty as the backing store for taustaVari.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty taustaVariProperty =
            DependencyProperty.Register("taustaVari", typeof(String), typeof(MainWindow), new PropertyMetadata(null));


        public String nappulaVari
        {
            get { return (String)GetValue(nappulaVariProperty); }
            set { SetValue(nappulaVariProperty, value); }
        }

        // Using a DependencyProperty as the backing store for nappulaVari.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty nappulaVariProperty =
            DependencyProperty.Register("nappulaVari", typeof(String), typeof(MainWindow), new PropertyMetadata(null));



        public String pelialueVari
        {
            get { return (String)GetValue(pelialueVariProperty); }
            set { SetValue(pelialueVariProperty, value); }
        }

        // Using a DependencyProperty as the backing store for pelialueVari.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty pelialueVariProperty =
            DependencyProperty.Register("pelialueVari", typeof(String), typeof(MainWindow), new PropertyMetadata(null));



        public String pelaaja1Nimi
        {
            get { return (String)GetValue(pelaaja1NimiProperty); }
            set { SetValue(pelaaja1NimiProperty, value); }
        }

        // Using a DependencyProperty as the backing store for pelaaja1Nimi.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty pelaaja1NimiProperty =
            DependencyProperty.Register("pelaaja1Nimi", typeof(String), typeof(MainWindow), new PropertyMetadata(null));

        public String pelaaja2Nimi
        {
            get { return (String)GetValue(pelaaja2NimiProperty); }
            set { SetValue(pelaaja2NimiProperty, value); }
        }

        // Using a DependencyProperty as the backing store for pelaaja2Nimi.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty pelaaja2NimiProperty =
            DependencyProperty.Register("pelaaja2Nimi", typeof(String), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Muutetaan pelaajan1 nimi jos textikenttää on muokattu
        /// ja se ei ole null tai tyhjä
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P1nimi_LostFocus(object sender, RoutedEventArgs e)
        {
            if (P1nimi.Text != null && P1nimi.Text != "") pelaaja1Nimi = P1nimi.Text;
        }

        /// <summary>
        /// Muutetaan pelaajan2 nimi jos tekstikenttää on muokattu
        /// ja se ei ole null tai tyhjä
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P2nimi_LostFocus(object sender, RoutedEventArgs e)
        {
            if (P2nimi.Text != null && P2nimi.Text != "") pelaaja2Nimi = P2nimi.Text;
        }

    }
}
