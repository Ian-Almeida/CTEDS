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
using ianpintodealmeida_d7_avaliacao.Models;

namespace ianpintodealmeida_d7_avaliacao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Context _context;
        private UserModel _loginModel;
        public MainWindow(Context context)
        {
            _context = context;
            _loginModel = new UserModel();
            InitializeComponent();
            FormPanel.DataContext = _loginModel;

            if (VerificaSeAdminExiste())
            {
                CadastrarAdmBtn.Visibility = Visibility.Hidden;
            }
        }

        private void OnPasswordChange(object sender, RoutedEventArgs e)
        {
            _loginModel.Password = PasswordField.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dataBaseItem = _context.Users.FirstOrDefault(obj => obj.Email == _loginModel.Email && obj.Password == _loginModel.Password);
            LoginMessage dialog;

            if (dataBaseItem != null)
            {
                // Show popup sucess
                dialog = new LoginMessage("Usuário autenticado!") { Owner = this };
            } else
            {
                dialog = new LoginMessage("Credenciais inválidas!") { Owner = this };
            }

            dialog.ShowDialog();
        }

        private void CadastrarAdmin_Click(object sender, RoutedEventArgs e)
        {
            _context.Add(new UserModel
            {
                Email = "admin@email.com",
                Password = "admin123",
                UserId = new Guid(),
            });
            _context.SaveChanges();
            CadastrarAdmBtn.Visibility = Visibility.Hidden;
        }

        private bool VerificaSeAdminExiste()
        {
            var adminItem = _context.Users.FirstOrDefault(obj => obj.Email == "admin@email.com" && obj.Password == "admin123");
            return adminItem != null;
        }
    }
}
