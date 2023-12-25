using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

using EnterpriseEmployee.Scripts;
using EnterpriseEmployee.Scripts.Database;

namespace EnterpriseEmployee.Windows.AddEmployee
{
  /// <summary>
  /// Логика взаимодействия для AddEmployeePanel.xaml
  /// </summary>
  public partial class AddEmployeePanel : UserControl
  {
    public Employees Employees { get; private set; }

    public event Action<Employees> Apply;
    public event Action Back;

    public AddEmployeePanel()
    {
      InitializeComponent();
    }

    //==================================================================

    public void Open()
    {
      SurName.BorderBrush = new SolidColorBrush(Color.FromRgb(128, 128, 128));
      Name.BorderBrush = new SolidColorBrush(Color.FromRgb(128, 128, 128));

      SurName.Text = Name.Text = Patronymic.Text = "";
      Gender.Text = "Мужской";

      Employees = new Employees();

      Visibility = Visibility.Visible;
    }

    public void Close()
    {
      Visibility = Visibility.Collapsed;

      Employees = null;
    }

    //==================================================================

    private void ButtonBack_Click(object sender, RoutedEventArgs e)
    {
      Back?.Invoke();
      Close();
    }

    private void ButtonApply_Click(object sender, RoutedEventArgs e)
    {
      if(StringNull(SurName.Text) && StringNull(Name.Text))
      {
        AddEmployee();
        MessageBox.Show("Данные успешно сохранены!");
        //Apply?.Invoke(Employees);
        Close();
      }
      else
      {
        Warning(SurName.Text, SurName);
        Warning(Name.Text, Name);
      }
    }

    //==================================================================

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

    //==================================================================

    public void AddEmployee()
    {
      string surName = SurName.Text;
      string name = Name.Text;
      string patronymic = Patronymic.Text;
      string gender = Gender.Text;

      var employeeInformation = new Employees(surName, name, patronymic, gender, "В ожидании");

      DatabaseTableEmployee.AddEmployees(employeeInformation);
    }

    //==================================================================

    private void SurName_TextChanged(object sender, TextChangedEventArgs e) { Warning(SurName.Text, SurName); }
    private void Name_TextChanged(object sender, TextChangedEventArgs e) { Warning(Name.Text, Name); }

    //==================================================================
  }
}