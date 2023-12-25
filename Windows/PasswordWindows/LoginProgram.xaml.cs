using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace EnterpriseEmployee.Windows.PasswordWindows
{
  /// <summary>
  /// Логика взаимодействия для LoginProgram.xaml
  /// </summary>
  public partial class LoginProgram : Window
  {
    /// <summary>
    /// Логин
    /// </summary>
    private const string Login = "Admin";
    /// <summary>
    /// Пароль
    /// </summary>
    private const string Password = "Admin";

    private readonly MainWindow mainWindow = new MainWindow();
    public LoginProgram()
    {
      InitializeComponent();

      mainWindow.Owner = Owner;

      WindowsMouseLeft.MouseDown += (parSender, parArgs) => DragMove();
    }

    //==================================================================

    private void LogIn()
    {
      if (StringNull(LoginText.Text) && StringNull(PasswordText.Password))
      {
        if (LoginText.Text == Login && PasswordText.Password == Password)
        {
          InvalidLoginAndPassword.Visibility = Visibility.Collapsed;

          mainWindow.Show();
          Close();
        }
        else
        {
          InvalidLoginAndPassword.Visibility = Visibility.Visible;
        }
      }
      else
      {
        Warning(LoginText.Text, LoginText);
        PasswordText.BorderBrush = PasswordText.Password == "" ? new SolidColorBrush(Color.FromRgb(211, 71, 46)) : new SolidColorBrush(Color.FromRgb(128, 128, 128));
      }
    }

    /// <summary>
    /// Проверка на пустое поле
    /// </summary>
    private bool StringNull(string parText)
    {
      return !string.IsNullOrEmpty(parText);
    }

    /// <summary>
    /// Проверка на заполнение полей (TextBox)
    /// </summary>
    private void Warning(string text, TextBox textBox)
    {
      textBox.BorderBrush = text == "" ? new SolidColorBrush(Color.FromRgb(211, 71, 46)) : new SolidColorBrush(Color.FromRgb(128, 128, 128));
    }

    /// <summary>
    /// Проверка на заполнение полей (PasswordBox)
    /// </summary>
    private void Warning(string text, PasswordBox passwordBox)
    {
      passwordBox.BorderBrush = text == "" ? new SolidColorBrush(Color.FromRgb(211, 71, 46)) : new SolidColorBrush(Color.FromRgb(128, 128, 128));
    }

    //==================================================================

    private void Button_Enter_Click(object sender, RoutedEventArgs e)
    {
      LogIn();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    //==================================================================

    private void Login_TextChanged(object sender, TextChangedEventArgs e)
    {
      Warning(LoginText.Text, LoginText);
    }

    private void PasswordText_PasswordChanged(object sender, RoutedEventArgs e)
    {
      Warning(PasswordText.Password, PasswordText);
    }

    //==================================================================
  }
}