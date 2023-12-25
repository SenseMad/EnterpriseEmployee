using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using EnterpriseEmployee.Scripts;
using EnterpriseEmployee.Windows.InformationEmployee;
using EnterpriseEmployee.Windows.ListOfEmployees;

namespace EnterpriseEmployee.Objects.Employee
{
  /// <summary>
  /// Логика взаимодействия для EmployeesView.xaml
  /// </summary>
  public partial class EmployeesView : UserControl
  {
    public Employees Employees { get; private set; }

    /// <summary>
    /// Событие: Нажатие на кнопку
    /// </summary>
    public event Action<Employees> Click;

    //==================================================================

    public EmployeesView(Employees parEmployees)
    {
      InitializeComponent();

      Employees = parEmployees;
      Update();
    }

    //==================================================================

    public void Update()
    {
      FIO_Text.Text = $"{Employees.Surname} {Employees.Name} {Employees.Patronymic}";
    }

    //==================================================================

    private void ButtonEmployee_Click(object sender, RoutedEventArgs e)
    {
      Click?.Invoke(Employees);
    }

    //==================================================================
  }
}