using System.Collections.Generic;
using System.Windows.Controls;

using EnterpriseEmployee.Scripts;

namespace EnterpriseEmployee.Objects.Employee
{
  /// <summary>
  /// Логика взаимодействия для EmployeesListView.xaml
  /// </summary>
  public partial class EmployeesListView : UserControl
  {
    public EmployeesListView()
    {
      InitializeComponent();
    }

    //==================================================================

    public EmployeesView this[int parIndex]
    {
      get => (EmployeesView)Employees.Children[parIndex];
      set => Employees.Children[parIndex] = value;
    }

    //==================================================================

    public EmployeesView AddEmployee(Employees parEmployees)
    {
      var employeesView = new EmployeesView(parEmployees);
      Employees.Children.Add(employeesView);
      return employeesView;
    }

    public EmployeesView[] AddEmployees(List<Employees> parEmployees)
    {
      EmployeesView[] employeesViews = new EmployeesView[parEmployees.Count];
      int i = 0;
      foreach (var employees in parEmployees)
      {
        employeesViews[i++] = AddEmployee(employees);
      }

      return employeesViews;
    }

    //==================================================================

    public void Clear()
    {
      Employees.Children.Clear();
    }

    //==================================================================
  }
}