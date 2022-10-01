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
using System.Windows.Shapes;

namespace ianpintodealmeida_d7_avaliacao
{
    /// <summary>
    /// Interaction logic for LoginMessage.xaml
    /// </summary>
    public partial class LoginMessage : Window
    {
        public LoginMessage(string message)
        {
            InitializeComponent();
            DialogMessage.Text = message;
        }

        public void OnClickOutsideMessage(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
