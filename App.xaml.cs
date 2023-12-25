using System.Windows;

namespace EnterpriseEmployee
{
  /// <summary>
  /// Логика взаимодействия для App.xaml
  /// </summary>
  public partial class App : Application
  {
    public static Window CurrentMainWindow
    {
      get { return Current.MainWindow; }
    }
  }
}