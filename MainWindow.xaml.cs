using System.Windows;

namespace EnterpriseEmployee
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      myTabControlStyle.Close += Close;
      myTabControlStyle.Minimize += () => WindowState = WindowState.Minimized;

      WindowsMouseLeft.MouseDown += (parSender, parArgs) => DragMove();
    }

    //==================================================================
  }
}