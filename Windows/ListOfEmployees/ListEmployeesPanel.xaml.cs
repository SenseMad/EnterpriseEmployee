using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;

using EnterpriseEmployee.Scripts;
using EnterpriseEmployee.Scripts.Database;
using EnterpriseEmployee.Objects.Employee;
using System.IO;

namespace EnterpriseEmployee.Windows.ListOfEmployees
{
  /// <summary>
  /// Логика взаимодействия для ListEmployeesPanel.xaml
  /// </summary>
  public partial class ListEmployeesPanel : UserControl
  {
    /// <summary>
    /// Статус сотрудников
    /// </summary>
    private string StatusEmployees { get; set; } = "Работающий";
    private string GenderEmployee { get; set; }

    /// <summary>
    /// Массив кнопок
    /// </summary>
    private readonly List<Button> ButtonsList = new List<Button>();

    /// <summary>
    /// Индект выделенной кнопки
    /// </summary>
    private int CurrentIndex { get; set; } = 0;
    
    public ListEmployeesPanel()
    {
      InitializeComponent();

      IsVisibleChanged += OnIsVisibleChanged;

      myInformationEmployeePanel.Back += () => Update();
      myAddEmployee.Back += () => Update();
    }

    //==================================================================

    private void OnIsVisibleChanged(object parSender, DependencyPropertyChangedEventArgs parE)
    {
      ButtonsList.Add(ButtonWorkingEmployees);
      ButtonsList.Add(ButtonEmployeesPending);
      ButtonsList.Add(ButtonShowDismissedEmployees);

      ButtonSelection(0);

      Update();
    }

    //==================================================================

    public void Update()
    {
      List<Employees> employees = DatabaseTableEmployee.GetEmployees();
      Update(SortByParameters(employees));
    }

    private void Update(List<Employees> parEmployees)
    {
      Employees.Clear();
      EmployeesView[] employeesViews = Employees.AddEmployees(parEmployees);

      foreach (var employeesView in employeesViews)
      {
        employeesView.Click += (employees) =>
        {
          myInformationEmployeePanel.Open(employees);
        };
      }
    }

    private List<Employees> SortByParameters(List<Employees> parEmployees)
    {
      return parEmployees.FindAll(parEmployee =>
      {
        if (!string.IsNullOrEmpty(GenderEmployee) && !parEmployee.Gender.ToUpper().Contains(GenderEmployee.ToUpper()))
          return false;

        if (!string.IsNullOrEmpty(StatusEmployees) && !parEmployee.Status.ToUpper().Contains(StatusEmployees.ToUpper()))
          return false;

        if (!string.IsNullOrEmpty(TextSearchEmployees.Text) && !parEmployee.Name.ToUpper().Contains(TextSearchEmployees.Text.ToUpper()) &&
            !parEmployee.Surname.ToUpper().Contains(TextSearchEmployees.Text.ToUpper()) &&
            !parEmployee.Patronymic.ToUpper().Contains(TextSearchEmployees.Text.ToUpper()))
          return false;

        return true;
      });
    }

    //==================================================================

    private void ButtonSearchEmployee_Click(object sender, RoutedEventArgs e)
    {
      Update();
    }

    private void ButtonWorkingEmployees_Click(object sender, RoutedEventArgs e)
    {
      StatusEmployees = "Работающий";

      ButtonSelection(0);

      Update();
    }

    private void ButtonEmployeesPending_Click(object sender, RoutedEventArgs e)
    {
      StatusEmployees = "В ожидании";

      ButtonSelection(1);

      Update();
    }

    private void ButtonShowDismissedEmployees_Click(object sender, RoutedEventArgs e)
    {
      StatusEmployees = "Уволен";

      ButtonSelection(2);

      Update();
    }

    private void ButtonAddEmployee_Click(object sender, RoutedEventArgs e)
    {
      myAddEmployee.Open();
    }

    //==================================================================

    /// <summary>
    /// Выделение кнопки
    /// </summary>
    /// <param name="parIndex">Индекс кнопки</param>
    private void ButtonSelection(int parIndex)
    {
      ButtonsList[CurrentIndex].IsEnabled = true;

      CurrentIndex = parIndex;

      ButtonsList[CurrentIndex].IsEnabled = false;
    }

    //==================================================================

    private void TextSearchEmployees_TextChanged(object sender, TextChangedEventArgs e)
    {
      Update();
    }

    //==================================================================

    private void AllGender_Checked(object sender, RoutedEventArgs e)
    {
      GenderEmployee = ""; 
      Update();
    }

    private void MaleGender_Checked(object sender, RoutedEventArgs e)
    {
      GenderEmployee = "Мужской"; 
      Update();
    }

    private void FemaleGender_Checked(object sender, RoutedEventArgs e)
    {
      GenderEmployee = "Женский"; 
      Update();
    }

    private void ButtonPrintedReport_Click(object sender, RoutedEventArgs e)
    {
      List<Employees> employees = DatabaseTableEmployee.GetEmployees();

      using (System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog())
      {
        saveFileDialog.Title = "Список сотрудников";
        saveFileDialog.Filter = "TXT: (*.txt)|*.txt";

        if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.Default))
          {
            sw.WriteLine("[Работающие]\n");
            TextOutput(sw, employees, "Работающий");

            sw.WriteLine("\n[В ожидании]\n");
            TextOutput(sw, employees, "В ожидании");

            sw.WriteLine("\n[Уволены]\n");
            TextOutput(sw, employees, "Уволен");
          }
        }
      }
    }

    /// <summary>
    /// Вывод списка сотрудников
    /// </summary>
    private void TextOutput(StreamWriter streamWriter, List<Employees> employees, string status)
    {
      for (int i = 0; i < employees.Count; i++)
      {
        if (employees[i].Status == status)
        {
          streamWriter.WriteLine($"{employees[i].Surname} {employees[i].Name} {employees[i].Patronymic}");
        }
      }
    }

    //==================================================================
  }
}