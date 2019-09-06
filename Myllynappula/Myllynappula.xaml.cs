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

namespace Myllynappula
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            
            //Bindataan onkoChecked IsChecked propertyyn, jotta päästään käsiksi
            //IsCheckediin muissakin luokissa. Tähän olisi varmaan parempikin tapa,
            //mutta tähän hätään en keksinyt parempaa.
            Binding checkBind = new Binding();
            checkBind.Source = this;
            checkBind.Path = new PropertyPath("onkoChecked");
            checkBind.Mode = BindingMode.TwoWay;
            checkBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            nappula.SetBinding(CheckBox.IsCheckedProperty, checkBind);

            Binding variBind = new Binding();
            variBind.Source = this;
            variBind.Path = new PropertyPath("nappulaColor");
            variBind.Mode = BindingMode.OneWay;
            variBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            nappula.SetBinding(CheckBox.ForegroundProperty, variBind);
        }

        /// <summary>
        /// bindattuna IsChecked propertyyn, jotta siihen päästään käsiksi
        /// </summary>
        public bool onkoChecked
        {
            get { return (bool)GetValue(onkoCheckedProperty); }
            set { SetValue(onkoCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for onkoChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty onkoCheckedProperty =
            DependencyProperty.Register("onkoChecked", typeof(bool), typeof(UserControl1), new PropertyMetadata(false));


        /// <summary>
        /// säädetään nappulan väriä tällä. On bindattuna nappulan checkboxin foregroundiin
        /// </summary>
        public String nappulaColor
        {
            get { return (String)GetValue(nappulaColorProperty); }
            set { SetValue(nappulaColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for nappulaColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty nappulaColorProperty =
            DependencyProperty.Register("nappulaColor", typeof(String), typeof(UserControl1), new PropertyMetadata("Black"));



    }
}
