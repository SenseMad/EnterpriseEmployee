using System;
using System.Windows.Controls;

namespace EnterpriseEmployee.Style.TabControl
{
  public partial class TabControlStyle : UserControl
  {
    public event Action Minimize;
    public event Action Close;

    public TabControlStyle()
    {
      InitializeComponent();
    }

    private void ButtonMinimize_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Minimize?.Invoke();
    }

    private void ButtonClose_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Close?.Invoke();
    }
  }
}