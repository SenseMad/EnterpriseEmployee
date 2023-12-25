using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Effects;

using EnterpriseEmployee.Scripts;
using EnterpriseEmployee.Scripts.Database;

namespace EnterpriseEmployee.Windows.InformationEmployee
{
  /// <summary>
  /// Логика взаимодействия для DismissEmployeesWindows.xaml
  /// </summary>
  public partial class DismissEmployeesWindows : Window
  {
    public event Action Back;

    public Employees Employees { get; set; }
    public string Status { get; set; }

    /// <summary>
    /// BlurEffect окна родителя
    /// </summary>
    public BlurEffect BlurEffect { get; }

    /// <summary>
    /// Начальное размытие
    /// </summary>
    public double LastBlurRadius { get; }

    public DismissEmployeesWindows(Window parOwner)
    {
      InitializeComponent();

      Owner = parOwner;
      ShowInTaskbar = false;

      if (Owner != null)
      {
        if (Owner.Effect is BlurEffect blurEffect)
        {
          BlurEffect = blurEffect;
          LastBlurRadius = BlurEffect.Radius;
          BlurEffect.Radius = 5;
        }
      }

      CurrentDay();
    }

    //==================================================================

    /// <summary>
    /// Текущая дата
    /// </summary>
    private void CurrentDay()
    {
      DateTime now = DateTime.Now;
      Date_Of_Dismissal.SelectedDate = now;
      Date_Of_The_Dismissal_Order.SelectedDate = now;
    }

    /// <summary>
    /// Очистка полей
    /// </summary>
    private void Clear()
    {
      Reason_For_Dismissal.Clear();
      Number_Of_The_Dismissal_Order.Clear();
    }

    private void AddDismissEmployees()
    {
      string reason_For_Dismissal = Reason_For_Dismissal.Text;
      string date_Of_Dismissal = Date_Of_Dismissal.Text;
      string number_Of_The_Dismissal_Order = Number_Of_The_Dismissal_Order.Text;
      string date_Of_The_Dismissal_Order = Date_Of_The_Dismissal_Order.Text;

      var dismissEmployees = new DismissEmployees(Employees.Id, reason_For_Dismissal, date_Of_Dismissal, number_Of_The_Dismissal_Order, date_Of_The_Dismissal_Order);

      DatabaseTableDismissEmployees.AddDismissEmployees(dismissEmployees);
    }

    /// <summary>
    /// Изменить ФИО сотрудника
    /// </summary>
    public void Edit_Employees()
    {
      Status = "Уволен";

      var employees = new Employees(Employees.Id, Employees.Surname, Employees.Name, Employees.Patronymic, Employees.Gender, Status);

      DatabaseTableEmployee.UpdateEmployees(employees);
    }

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
    /// Закрытие окна
    /// </summary>
    private void CloseWindows()
    {
      Back?.Invoke();
      CurrentDay();
      Clear();
      if (Owner != null)
        BlurEffect.Radius = LastBlurRadius;
      Close();
    }

    //==================================================================

    private void Button_Apply_Labour_Activity_Click(object sender, RoutedEventArgs e)
    {
      if (StringNull(Reason_For_Dismissal.Text) && StringNull(Number_Of_The_Dismissal_Order.Text))
      {
        AddDismissEmployees();
        Edit_Employees();
        MessageBox.Show("Сотрудник уволен!");
        CloseWindows();
      }
      else
      {
        Warning(Reason_For_Dismissal.Text, Reason_For_Dismissal);
        Warning(Number_Of_The_Dismissal_Order.Text, Number_Of_The_Dismissal_Order);
      }
    }

    private void Button_Close_Click(object sender, RoutedEventArgs e)
    {
      CurrentDay();
      Clear();
      if (Owner != null)
        BlurEffect.Radius = LastBlurRadius;
      Close();
    }

    //==================================================================
  }
}