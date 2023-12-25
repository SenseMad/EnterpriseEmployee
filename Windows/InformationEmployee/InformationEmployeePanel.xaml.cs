using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Controls;
using EasyDox;

using EnterpriseEmployee.Scripts;
using EnterpriseEmployee.Scripts.Database;

namespace EnterpriseEmployee.Windows.InformationEmployee
{
  /// <summary>
  /// Логика взаимодействия для InformationEmployeePanel.xaml
  /// </summary>
  public partial class InformationEmployeePanel : UserControl
  {
    public DismissEmployeesWindows DismissEmployeesWindows { get; private set; }

    public Employees Employees { get; private set; }
    public EmployeeInformation EmployeeInformation { get; private set; }
    public EmployeesPassportDetails EmployeesPassportDetails { get; private set; }
    public Education Education { get; private set; }
    public LabourActivity LabourActivity { get; private set; }
    public Positions Positions { get; private set; }
    public DismissEmployees DismissEmployees { get; private set; }

    public List<Positions> PositionsList { get; private set; }

    //public event Action<Employees> Apply;
    public event Action Back;

    //==================================================================

    public InformationEmployeePanel()
    {
      InitializeComponent();

      EmployeeInformation = new EmployeeInformation();
      EmployeesPassportDetails = new EmployeesPassportDetails();
      Education = new Education();
      LabourActivity = new LabourActivity();
      Positions = new Positions();
      DismissEmployees = new DismissEmployees();

      PositionsList = DatabaseTablePositions.GetPositions();

      for (int i = 0; i < PositionsList.Count; i++)
      {
        Position.Items.Add(PositionsList[i].Job_Title);
      }
    }

    //==================================================================

    public void Open(Employees parEmployees)
    {
      Employees = parEmployees;
      EmployeeInformation = DatabaseTableEmployeeInformation.GetEmployeeInformations(Employees.Id);
      EmployeesPassportDetails = DatabaseTableEmployeesPassportDetails.GetEmployeesPassportDetails(Employees.Id);
      Education = DatabaseTableEducation.GetEducation(Employees.Id);
      LabourActivity = DatabaseTableLabourActivity.GetLabourActivity(Employees.Id);

      Deselecting();
      EnableTheEditButton();

      Visibility = Visibility.Visible;
      Loading();

      CurrentDay();
      OffButton();

      Position.SelectedIndex = 0;
    }

    public void Close()
    {
      Deselecting();

      Employees = null;
      EmployeeInformation = null;
      EmployeesPassportDetails = null;
      Education = null;
      LabourActivity = null;

      TabControl.SelectedIndex = 0;

      Visibility = Visibility.Collapsed;
    }

    /// <summary>
    /// Загрузка информации при открытии
    /// </summary>
    private void Loading()
    {
      Loading_EmployeeInformation();
      Loading_EmployeesPassportDetails();
      Loading_Education();
      Loading_LabourActivity();
    }

    /// <summary>
    /// Текущая дата
    /// </summary>
    private void CurrentDay()
    {
       if (!StringNull(DateOfBirth.Text))
        DateOfBirth.Text = DateTime.Now.ToShortDateString();
      if (!StringNull(Date_Of_Issue.Text))
        Date_Of_Issue.Text = DateTime.Now.ToShortDateString();
      if (!StringNull(Date_Of_Employment.Text))
        Date_Of_Employment.Text = DateTime.Now.ToShortDateString();
      if (!StringNull(Date_Of_Contract.Text))
        Date_Of_Contract.Text = DateTime.Now.ToShortDateString();
    }

    private void ResetButtons(Button button, Button button2)
    {
      button.Visibility = Visibility.Visible;
      button2.Visibility = Visibility.Collapsed;
    }

    private void EnableTheEditButton()
    {
      // Информация о сотруднике
      if (StringNull(EmployeeInformation.Place_Of_Birth) && StringNull(EmployeeInformation.Date_Of_Birth) && StringNull(EmployeeInformation.Full_Years) && StringNull(EmployeeInformation.INN) &&
       StringNull(EmployeeInformation.Nationality) && StringNull(EmployeeInformation.Citizenship))
      {
        Button_Apply_Employee_Information.Visibility = Visibility.Collapsed;
        Button_Edit_Employee_Information.Visibility = Visibility.Visible;
      }
      else
        ResetButtons(Button_Apply_Employee_Information, Button_Edit_Employee_Information);

      // Паспорт
      if (StringNull(EmployeesPassportDetails.Passport_Series) && StringNull(EmployeesPassportDetails.Passport_Number) && StringNull(EmployeesPassportDetails.Date_Of_Issue) &&
       StringNull(EmployeesPassportDetails.Passport_Issued) && StringNull(EmployeesPassportDetails.Division_Code) && StringNull(EmployeesPassportDetails.City) && StringNull(EmployeesPassportDetails.Street) &&
       StringNull(EmployeesPassportDetails.House) && StringNull(EmployeesPassportDetails.Flat))
      {
        Button_Apply_Employee_Passport.Visibility = Visibility.Collapsed;
        Button_Edit_Employee_Passport.Visibility = Visibility.Visible;
      }
      else
        ResetButtons(Button_Apply_Employee_Passport, Button_Edit_Employee_Passport);

      // Образование
      if (StringNull(Education.Name_Document_On_Education) && StringNull(Education.Type_Education) && StringNull(Education.Educational_Institution) && StringNull(Education.Specialty) && StringNull(Education.Qualification))
      {
        Button_Apply_Education.Visibility = Visibility.Collapsed;
        Button_Edit_Education.Visibility = Visibility.Visible;
      }
      else
        ResetButtons(Button_Apply_Education, Button_Edit_Education);

      // Трудовая деятельность
      if (StringNull(LabourActivity.Date_Of_Employment) && StringNull(LabourActivity.Position) && StringNull(LabourActivity.Salary) && StringNull(LabourActivity.Contract_Number) && 
        StringNull(LabourActivity.Date_Of_Contract) && StringNull(LabourActivity.Type_Of_Workplace))
      {
        Button_Apply_Labour_Activity.Visibility = Visibility.Collapsed;
        Button_Edit_Labour_Activity.Visibility = Visibility.Visible;
      }
      else
        ResetButtons(Button_Apply_Labour_Activity, Button_Edit_Labour_Activity);
    }

    /// <summary>
    /// Отключеник кнопки увольнения сотрудника
    /// </summary>
    private void OffButton()
    {
      if (Employees.Status == "Уволен")
      {
        Dismiss.Visibility = Visibility.Collapsed;
        ButtonInformation.Visibility = Visibility.Visible;
      }
      else if (Employees.Status == "Работающий")
      {
        Dismiss.Visibility = Visibility.Visible;
        ButtonInformation.Visibility = Visibility.Collapsed;
      }
      else if (Employees.Status == "В ожидании")
      {
        Dismiss.Visibility = Visibility.Collapsed;
        ButtonInformation.Visibility = Visibility.Collapsed;
      }
    }

    //==================================================================

    #region Загрузка данных при открытии меню

    /// <summary>
    /// Информация о сотруднике
    /// </summary>
    private void Loading_EmployeeInformation()
    {
      SurName.Text = $"{Employees.Surname}";
      Name.Text = $"{Employees.Name}";
      Patronymic.Text = $"{Employees.Patronymic}";
      Gender.Text = $"{Employees.Gender}";
      Status.Text = $"{Employees.Status}";

      PlaceOfBirth.Text = $"{EmployeeInformation.Place_Of_Birth}";
      DateOfBirth.Text = $"{EmployeeInformation.Date_Of_Birth}";
      FullYears.Text = $"{EmployeeInformation.Full_Years}";
      Mobile_Phone.Text = $"{EmployeeInformation.Mobile_Phone}";
      Home_Phone.Text = $"{EmployeeInformation.Home_Phone}";
      Office_Phone.Text = $"{EmployeeInformation.Office_Phone}";
      Email.Text = $"{EmployeeInformation.Email}";
      INN.Text = $"{EmployeeInformation.INN}";
      MedicalPolicy.Text = $"{EmployeeInformation.Medical_Policy}";
      Nationality.Text = $"{EmployeeInformation.Nationality}";
      Citizenship.Text = $"{EmployeeInformation.Citizenship}";
    }

    /// <summary>
    /// Данные паспорта
    /// </summary>
    private void Loading_EmployeesPassportDetails()
    {
      Passport_Series.Text = $"{EmployeesPassportDetails.Passport_Series}";
      Passport_Number.Text = $"{EmployeesPassportDetails.Passport_Number}";
      Date_Of_Issue.Text = $"{EmployeesPassportDetails.Date_Of_Issue}";
      Passport_Issued.Text = $"{EmployeesPassportDetails.Passport_Issued}";
      Division_Code.Text = $"{EmployeesPassportDetails.Division_Code}";
      City.Text = $"{EmployeesPassportDetails.City}";
      Street.Text = $"{EmployeesPassportDetails.Street}";
      House.Text = $"{EmployeesPassportDetails.House}";
      Flat.Text = $"{EmployeesPassportDetails.Flat}";
    }

    /// <summary>
    /// Данные об образовании
    /// </summary>
    private void Loading_Education()
    {
      Name_Document_On_Education.Text = $"{Education.Name_Document_On_Education}";
      Type_Education.Text = $"{Education.Type_Education}";
      Educational_Institution.Text = $"{Education.Educational_Institution}";
      Specialty.Text = $"{Education.Specialty}";
      Qualification.Text = $"{Education.Qualification}";
      Diplom_Series.Text = $"{Education.Diplom_Series}";
      Diplom_Number.Text = $"{Education.Diplom_Number}";
    }

    /// <summary>
    /// Данные о трудовой деятельности
    /// </summary>
    private void Loading_LabourActivity()
    {
      Date_Of_Employment.Text = $"{LabourActivity.Date_Of_Employment}";
      Position.Text = $"{LabourActivity.Position}";
      Allowance.Text = $"{LabourActivity.Allowance}";
      Salary_Allowance.Text = $"{LabourActivity.Salary}";
      Contract_Number.Text = $"{LabourActivity.Contract_Number}";
      Date_Of_Contract.Text = $"{LabourActivity.Date_Of_Contract}";
      Type_Of_Workplace.Text = $"{LabourActivity.Type_Of_Workplace}";
      Work_Experience.Text = $"{LabourActivity.Work_Experience}";
    }

    #endregion

    #region Добавление новой информации

    /// <summary>
    /// Добавление информации о сотруднике
    /// </summary>
    public void Add_EmployeeInformation()
    {
      string placeOfBirth = PlaceOfBirth.Text;
      string dateOfBirth = DateOfBirth.Text;
      string fullYears = FullYears.Text;
      string mobile_Phone = Mobile_Phone.Text;
      string home_Phone = Home_Phone.Text;
      string office_Phone = Office_Phone.Text;
      string email = Email.Text;
      string inn = INN.Text;
      string medicalPolicy = MedicalPolicy.Text;
      string nationality = Nationality.Text;
      string citizenship = Citizenship.Text;

      var employeeInformation = new EmployeeInformation(Employees.Id, placeOfBirth, dateOfBirth, fullYears, mobile_Phone, home_Phone, office_Phone, email, inn, medicalPolicy, nationality, citizenship);

      DatabaseTableEmployeeInformation.AddEmployeeInformation(employeeInformation);
    }

    /// <summary>
    /// Добавление информации паспорта
    /// </summary>
    public void Add_EmployeesPassportDetails()
    {
      string passport_Series = Passport_Series.Text;
      string passport_Number = Passport_Number.Text;
      string date_Of_Issue = Date_Of_Issue.Text;
      string passport_Issued = Passport_Issued.Text;
      string division_Code = Division_Code.Text;
      string city = City.Text;
      string street = Street.Text;
      string house = House.Text;
      string flat = Flat.Text;

      var employeesPassportDetails = new EmployeesPassportDetails(Employees.Id, passport_Series, passport_Number, date_Of_Issue, passport_Issued, division_Code, city, street, house, flat);

      DatabaseTableEmployeesPassportDetails.AddEmployeesPassportDetails(employeesPassportDetails);
    }

    /// <summary>
    /// Добавить информацию об образовании
    /// </summary>
    public void Add_Education()
    {
      string name_Document_On_Education = Name_Document_On_Education.Text;
      string type_Education = Type_Education.Text;
      string educational_Institution = Educational_Institution.Text;
      string specialty = Specialty.Text;
      string qualification = Qualification.Text;
      string diplom_Series = Diplom_Series.Text;
      string diplom_Number = Diplom_Number.Text;

      var education = new Education(Employees.Id, name_Document_On_Education, type_Education, educational_Institution, specialty, qualification, diplom_Series, diplom_Number);

      DatabaseTableEducation.AddEmployeeInformation(education);
    }

    /// <summary>
    /// Добавить информацию о трудовой деятельности
    /// </summary>
    public void Add_LabourActivity()
    {
      string date_Of_Employment = Date_Of_Employment.Text;
      string position = Position.Text;
      string allowance = Allowance.Text;
      string salary_Allowance = Salary_Allowance.Text;
      string contract_Number = Contract_Number.Text;
      string date_Of_Contract = Date_Of_Contract.Text;
      string type_Of_Workplace = Type_Of_Workplace.Text;
      string work_Experience = Work_Experience.Text;

      var labourActivity = new LabourActivity(Employees.Id, date_Of_Employment, position, allowance, salary_Allowance, contract_Number, date_Of_Contract, type_Of_Workplace, work_Experience);

      DatabaseTableLabourActivity.AddLabourActivity(labourActivity);
    }

    #endregion

    #region Изменение информации

    /// <summary>
    /// Изменить общие данные сотрудника
    /// </summary>
    public void Edit_EmployeeInformation()
    {
      string placeOfBirth = PlaceOfBirth.Text;
      string dateOfBirth = DateOfBirth.Text;
      string fullYears = FullYears.Text;
      string mobile_Phone = Mobile_Phone.Text;
      string home_Phone = Home_Phone.Text;
      string office_Phone = Office_Phone.Text;
      string email = Email.Text;
      string inn = INN.Text;
      string medicalPolicy = MedicalPolicy.Text;
      string nationality = Nationality.Text;
      string citizenship = Citizenship.Text;

      var employeeInformation = new EmployeeInformation(Employees.Id, placeOfBirth, dateOfBirth, fullYears, mobile_Phone, home_Phone, office_Phone, email, inn, medicalPolicy, nationality, citizenship);

      DatabaseTableEmployeeInformation.UpdateEmployeeInformation(employeeInformation);
    }

    /// <summary>
    /// Изменить ФИО сотрудника
    /// </summary>
    public void Edit_Employees()
    {
      string surName = SurName.Text;
      string name = Name.Text;
      string patronymic = Patronymic.Text;
      string gender = Gender.Text;
      string status = Status.Text;

      var employees = new Employees(Employees.Id, surName, name, patronymic, gender, status);

      DatabaseTableEmployee.UpdateEmployees(employees);
    }

    /// <summary>
    /// Изменить данные паспорта
    /// </summary>
    public void Edit_EmployeesPassportDetails()
    {
      string passport_Series = Passport_Series.Text;
      string passport_Number = Passport_Number.Text;
      string date_Of_Issue = Date_Of_Issue.Text;
      string passport_Issued = Passport_Issued.Text;
      string division_Code = Division_Code.Text;
      string city = City.Text;
      string street = Street.Text;
      string house = House.Text;
      string flat = Flat.Text;

      var employeesPassportDetails = new EmployeesPassportDetails(Employees.Id, passport_Series, passport_Number, date_Of_Issue, passport_Issued, division_Code, city, street, house, flat);

      DatabaseTableEmployeesPassportDetails.UpdateEmployeesPassportDetails(employeesPassportDetails);
    }

    /// <summary>
    /// Изменить данные образования
    /// </summary>
    public void Edit_Education()
    {
      string name_Document_On_Education = Name_Document_On_Education.Text;
      string type_Education = Type_Education.Text;
      string educational_Institution = Educational_Institution.Text;
      string specialty = Specialty.Text;
      string qualification = Qualification.Text;
      string diplom_Series = Diplom_Series.Text;
      string diplom_Number = Diplom_Number.Text;

      var education = new Education(Employees.Id, name_Document_On_Education, type_Education, educational_Institution, specialty, qualification, diplom_Series, diplom_Number);

      DatabaseTableEducation.UpdateEducation(education);
    }

    /// <summary>
    /// Изменить данные трудовой деятельности
    /// </summary>
    public void Edit_LabourActivity()
    {
      string date_Of_Employment = Date_Of_Employment.Text;
      string position = Position.Text;
      string allowance = Allowance.Text;
      string salary_Allowance = Salary_Allowance.Text;
      string contract_Number = Contract_Number.Text;
      string date_Of_Contract = Date_Of_Contract.Text;
      string type_Of_Workplace = Type_Of_Workplace.Text;
      string work_Experience = Work_Experience.Text;

      var labourActivity = new LabourActivity(Employees.Id, date_Of_Employment, position, allowance, salary_Allowance, contract_Number, date_Of_Contract, type_Of_Workplace, work_Experience);

      DatabaseTableLabourActivity.UpdateLabourActivity(labourActivity);
    }

    #endregion

    //==================================================================

    /// <summary>
    /// Проверка на символы
    /// </summary>
    /// <param name="parAddText">Текст</param>
    private bool IsInt(string parAddText)
    {
      return char.IsDigit(parAddText, 0);
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
      textBox.BorderBrush = text == "" ? new SolidColorBrush(Color.FromRgb(211, 71, 46)) : SolidColorBrush();
    }

    /// <summary>
    /// Снятие выделения с полей
    /// </summary>
    private void Deselecting()
    {
      #region Общая информация

      SurName.BorderBrush = SolidColorBrush();
      Name.BorderBrush = SolidColorBrush();
      PlaceOfBirth.BorderBrush = SolidColorBrush();
      FullYears.BorderBrush = SolidColorBrush();
      INN.BorderBrush = SolidColorBrush();
      Nationality.BorderBrush = SolidColorBrush();
      Citizenship.BorderBrush = SolidColorBrush();

      #endregion

      #region Паспорт

      Passport_Series.BorderBrush = SolidColorBrush();
      Passport_Number.BorderBrush = SolidColorBrush();
      Passport_Issued.BorderBrush = SolidColorBrush();
      Division_Code.BorderBrush = SolidColorBrush();
      City.BorderBrush = SolidColorBrush();
      Street.BorderBrush = SolidColorBrush();
      House.BorderBrush = SolidColorBrush();
      Flat.BorderBrush = SolidColorBrush();

      #endregion

      #region Образование

      Name_Document_On_Education.BorderBrush = SolidColorBrush();
      Type_Education.BorderBrush = SolidColorBrush();
      Educational_Institution.BorderBrush = SolidColorBrush();
      Specialty.BorderBrush = SolidColorBrush();
      Qualification.BorderBrush = SolidColorBrush();

      #endregion

      #region Трудовая деятельность

      Salary.BorderBrush = SolidColorBrush();
      Salary_Allowance.BorderBrush = SolidColorBrush();
      Contract_Number.BorderBrush = SolidColorBrush();
      Type_Of_Workplace.BorderBrush = SolidColorBrush();

      #endregion
    }

    private SolidColorBrush SolidColorBrush()
    {
      return new SolidColorBrush(Color.FromRgb(128, 128, 128));
    }

    //==================================================================

    private void ButtonBack_Click(object sender, RoutedEventArgs e)
    {
      Back?.Invoke();
      Close();
    }

    #region Обновление данных

    /// <summary>
    /// Обновить ФИО сотрудника
    /// </summary>
    private void Update_Employees()
    {
      Employees.Surname = SurName.Text;
      Employees.Name = Name.Text;
      Employees.Patronymic = Patronymic.Text;
    }

    /// <summary>
    /// Обновить общие данные сотрудника
    /// </summary>
    private void Update_EmployeesInformation()
    {
      EmployeeInformation.Place_Of_Birth = PlaceOfBirth.Text;
      EmployeeInformation.Date_Of_Birth = DateOfBirth.Text;
      EmployeeInformation.Full_Years = FullYears.Text;
      EmployeeInformation.Mobile_Phone = Mobile_Phone.Text;
      EmployeeInformation.Home_Phone = Home_Phone.Text;
      EmployeeInformation.Office_Phone = Office_Phone.Text;
      EmployeeInformation.Email = Email.Text;
      EmployeeInformation.INN = INN.Text;
      EmployeeInformation.Medical_Policy = MedicalPolicy.Text;
      EmployeeInformation.Nationality = Nationality.Text;
      EmployeeInformation.Citizenship = Citizenship.Text;
    }

    /// <summary>
    /// Обновить паспортные данные
    /// </summary>
    private void Update_EmployeePassport()
    {
      EmployeesPassportDetails.Passport_Series = Passport_Series.Text;
      EmployeesPassportDetails.Passport_Number = Passport_Number.Text;
      EmployeesPassportDetails.Date_Of_Issue = Date_Of_Issue.Text;
      EmployeesPassportDetails.Passport_Issued = Passport_Issued.Text;
      EmployeesPassportDetails.Division_Code = Division_Code.Text;
      EmployeesPassportDetails.City = City.Text;
      EmployeesPassportDetails.Street = Street.Text;
      EmployeesPassportDetails.House = House.Text;
      EmployeesPassportDetails.Flat = Flat.Text;
    }

    /// <summary>
    /// Обновить информацию об образовании
    /// </summary>
    private void Update_Education()
    {
      Education.Name_Document_On_Education = Name_Document_On_Education.Text;
      Education.Type_Education = Type_Education.Text;
      Education.Educational_Institution = Educational_Institution.Text;
      Education.Specialty = Specialty.Text;
      Education.Qualification = Qualification.Text;
      Education.Diplom_Series = Diplom_Series.Text;
      Education.Diplom_Number = Diplom_Number.Text;
    }

    /// <summary>
    /// Обновить информацию о трудовой деятельности
    /// </summary>
    private void Update_LabourActivity()
    {
      LabourActivity.Date_Of_Employment = Date_Of_Employment.Text;
      LabourActivity.Position = Position.Text;
      LabourActivity.Allowance = Allowance.Text;
      LabourActivity.Salary = Salary.Text;
      LabourActivity.Contract_Number = Contract_Number.Text;
      LabourActivity.Date_Of_Contract = Date_Of_Contract.Text;
      LabourActivity.Type_Of_Workplace = Type_Of_Workplace.Text;
      LabourActivity.Work_Experience = Work_Experience.Text;
    }

    #endregion

    #region Кнопки добавить информацию

    private void Button_Apply_Employee_Information_Click(object sender, RoutedEventArgs e)
    {
      if (CheckingEmployeeInformation())
      {
        Add_EmployeeInformation();
        Update_EmployeesInformation();

        if (Employees.Surname != SurName.Text || Employees.Name != Name.Text || Employees.Patronymic != Patronymic.Text)
        {
          Edit_Employees();
          Update_Employees();
        }

        MessageBox.Show("Данные успешно сохранены!");
        Button_Apply_Employee_Information.Visibility = Visibility.Collapsed;
        Button_Edit_Employee_Information.Visibility = Visibility.Visible;
      }
    }

    private void Button_Apply_Employee_Passport_Click(object sender, RoutedEventArgs e)
    {
      if(CheckingEmployee_Passport())
      {
        Add_EmployeesPassportDetails();
        Update_EmployeePassport();

        MessageBox.Show("Данные успешно сохранены!");
        Button_Apply_Employee_Passport.Visibility = Visibility.Collapsed;
        Button_Edit_Employee_Passport.Visibility = Visibility.Visible;
      }
    }

    private void Button_Apply_Education_Click(object sender, RoutedEventArgs e)
    {
      if (CheckingEducation())
      {
        Add_Education();
        Update_Education();

        MessageBox.Show("Данные успешно сохранены!");
        Button_Apply_Education.Visibility = Visibility.Collapsed;
        Button_Edit_Education.Visibility = Visibility.Visible;
      }
    }

    private void Button_Apply_Labour_Activity_Click(object sender, RoutedEventArgs e)
    {
      if (CheckingLabour_Activity())
      {
        Add_LabourActivity();
        Update_LabourActivity();

        if (Employees.Status != Status.Text)
        {
          Edit_Employees();
          Employees.Status = Status.Text;
        }

        MessageBox.Show("Данные успешно сохранены!");
        Button_Apply_Labour_Activity.Visibility = Visibility.Collapsed;
        Button_Edit_Labour_Activity.Visibility = Visibility.Visible;
      }
    }

    #endregion

    #region Кнопки изменить информацию

    private void Button_Edit_Employee_Information_Click(object sender, RoutedEventArgs e)
    {
      if (CheckingEmployeeInformation())
      {
        if(EmployeeInformation.Place_Of_Birth != PlaceOfBirth.Text || EmployeeInformation.Date_Of_Birth != DateOfBirth.Text || EmployeeInformation.Full_Years != FullYears.Text || 
          EmployeeInformation.Mobile_Phone != Mobile_Phone.Text || EmployeeInformation.Home_Phone != Home_Phone.Text || EmployeeInformation.Office_Phone != Office_Phone.Text || EmployeeInformation.Email != Email.Text || 
          EmployeeInformation.INN != INN.Text || EmployeeInformation.Medical_Policy != MedicalPolicy.Text || EmployeeInformation.Nationality != Nationality.Text || EmployeeInformation.Citizenship != Citizenship.Text)
        {
          Edit_EmployeeInformation();
          Update_EmployeesInformation();

          if (Employees.Surname != SurName.Text || Employees.Name != Name.Text || Employees.Patronymic != Patronymic.Text)
          {
            Edit_Employees();
            Update_Employees();
          }

          MessageBox.Show("Данные успешно изменены!");
          return;
        }

        if (Employees.Surname != SurName.Text || Employees.Name != Name.Text || Employees.Patronymic != Patronymic.Text)
        {
          Edit_Employees();
          Update_Employees();

          MessageBox.Show("Данные успешно изменены!");
        }
      }
    }

    private void Button_Edit_Employee_Passport_Click(object sender, RoutedEventArgs e)
    {
      if (CheckingEmployee_Passport())
      {
        if (EmployeesPassportDetails.Passport_Series != Passport_Series.Text || EmployeesPassportDetails.Passport_Number != Passport_Number.Text || EmployeesPassportDetails.Date_Of_Issue != Date_Of_Issue.Text || 
          EmployeesPassportDetails.Passport_Issued != Passport_Issued.Text || EmployeesPassportDetails.Division_Code != Division_Code.Text || EmployeesPassportDetails.City != City.Text || 
          EmployeesPassportDetails.Street != Street.Text || EmployeesPassportDetails.House != House.Text || EmployeesPassportDetails.Flat != Flat.Text)
        {
          Edit_EmployeesPassportDetails();
          Update_EmployeePassport();

          MessageBox.Show("Данные успешно изменены!");
        }
      }
    }

    private void Button_Edit_Education_Click(object sender, RoutedEventArgs e)
    {
      if (CheckingEducation())
      {
        if(Education.Name_Document_On_Education != Name_Document_On_Education.Text || Education.Type_Education != Type_Education.Text || Education.Educational_Institution != Educational_Institution.Text || 
          Education.Specialty != Specialty.Text || Education.Qualification != Qualification.Text || Education.Diplom_Series != Diplom_Series.Text || Education.Diplom_Number != Diplom_Number.Text)
        {
          Edit_Education();
          Update_Education();

          MessageBox.Show("Данные успешно изменены!");
        }
      }
    }

    private void Button_Edit_Labour_Activity_Click(object sender, RoutedEventArgs e)
    {
      if (CheckingLabour_Activity())
      {
        if(LabourActivity.Date_Of_Employment != Date_Of_Employment.Text || LabourActivity.Position != Position.Text || LabourActivity.Allowance != Allowance.Text || LabourActivity.Salary != Salary_Allowance.Text || 
          LabourActivity.Contract_Number != Contract_Number.Text || LabourActivity.Date_Of_Contract != Date_Of_Contract.Text || LabourActivity.Type_Of_Workplace != Type_Of_Workplace.Text || 
          LabourActivity.Work_Experience != Work_Experience.Text)
        {
          Edit_LabourActivity();
          Update_LabourActivity();

          if (Employees.Status != Status.Text)
          {
            Edit_Employees();
            Employees.Status = Status.Text;
          }

          MessageBox.Show("Данные успешно изменены!");
          return;
        }

        if (Employees.Status != Status.Text)
        {
          Edit_Employees();
          Employees.Status = Status.Text;

          MessageBox.Show("Данные успешно изменены!");
        }
      }
    }

    #endregion

    //==================================================================

    #region Проверка на символы

    #region Общие сведения

    private void SurName_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = IsInt(parE.Text); }
    private void Name_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = IsInt(parE.Text); }
    private void Patronymic_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = IsInt(parE.Text); }
    private void DateOfBirth_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }   
    private void FullYears_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Mobile_Phone_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Home_Phone_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Office_Phone_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void INN_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void MedicalPolicy_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Nationality_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = IsInt(parE.Text); }
    private void Citizenship_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = IsInt(parE.Text); }

    #endregion

    #region Паспортные данные

    private void Passport_Series_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Passport_Number_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Date_Of_Issue_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Passport_Issued_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = IsInt(parE.Text); }
    private void Division_Code_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }

    #endregion

    #region Образование

    private void Name_Document_On_Education_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = IsInt(parE.Text); }
    private void Type_Education_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = IsInt(parE.Text); }
    private void Educational_Institution_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = IsInt(parE.Text); }
    private void Diplom_Series_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Diplom_Number_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }

    #endregion

    #region Трудовая деятельность

    private void Date_Of_Employment_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Contract_Number_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Allowance_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Work_Experience_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }
    private void Date_Of_Contract_PreviewTextInput(object parSender, TextCompositionEventArgs parE) { parE.Handled = !IsInt(parE.Text); }

    #endregion

    #endregion

    #region Проверка на заполненность полей

    #region Общие сведения

    private void SurName_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(SurName.Text)) Warning(SurName.Text, SurName); }
    private void Name_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Name.Text)) Warning(Name.Text, Name); }
    private void PlaceOfBirth_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(PlaceOfBirth.Text)) Warning(PlaceOfBirth.Text, PlaceOfBirth); }
    private void FullYears_TextChanged(object sender, TextChangedEventArgs e) { if(StringNull(FullYears.Text)) Warning(FullYears.Text, FullYears); }
    private void INN_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(INN.Text)) Warning(INN.Text, INN); }
    private void Nationality_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Nationality.Text)) Warning(Nationality.Text, Nationality); }
    private void Citizenship_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Citizenship.Text)) Warning(Citizenship.Text, Citizenship); }
    
    #endregion

    #region Паспортные данные

    private void Passport_Series_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Passport_Series.Text)) Warning(Passport_Series.Text, Passport_Series); }
    private void Passport_Number_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Passport_Number.Text)) Warning(Passport_Number.Text, Passport_Number); }
    private void Passport_Issued_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Passport_Issued.Text)) Warning(Passport_Issued.Text, Passport_Issued); }
    private void Division_Code_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Division_Code.Text)) Warning(Division_Code.Text, Division_Code); }
    private void City_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(City.Text)) Warning(City.Text, City); }
    private void Street_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Street.Text)) Warning(Street.Text, Street); }
    private void House_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(House.Text)) Warning(House.Text, House); }
    private void Flat_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Flat.Text)) Warning(Flat.Text, Flat); }

    #endregion

    #region Образование

    private void Name_Document_On_Education_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Name_Document_On_Education.Text)) Warning(Name_Document_On_Education.Text, Name_Document_On_Education); }
    private void Type_Education_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Type_Education.Text)) Warning(Type_Education.Text, Type_Education); }
    private void Educational_Institution_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Educational_Institution.Text)) Warning(Educational_Institution.Text, Educational_Institution); }
    private void Specialty_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Specialty.Text)) Warning(Specialty.Text, Specialty); }
    private void Qualification_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Qualification.Text)) Warning(Qualification.Text, Qualification); }

    #endregion
    
    #region Трудовая деятельность

    private void Contract_Number_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Contract_Number.Text)) Warning(Contract_Number.Text, Contract_Number); }
    private void Type_Of_Workplace_TextChanged(object sender, TextChangedEventArgs e) { if (StringNull(Type_Of_Workplace.Text)) Warning(Type_Of_Workplace.Text, Type_Of_Workplace); }

    #endregion

    #endregion

    private void Position_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      Positions = DatabaseTablePositions.GetPositions(Position.SelectedIndex + 1);
      Salary.Text = Positions.Salary;
    }

    private void Salary_TextChanged(object sender, TextChangedEventArgs e)
    {
      Warning(Salary.Text, Salary);

      if (Salary.Text == "") { return; }

      if (Allowance.Text != "")
      {
        double CurrentSalary = Convert.ToDouble(Salary.Text); // Зарплата

        double CurrentAllowance = CurrentSalary * (Convert.ToDouble(Allowance.Text) / 100); // Надбавка(%)

        double TotalSalary = CurrentSalary + CurrentAllowance; // Общая зарплата

        Salary_Allowance.Text = TotalSalary.ToString();
      }
      else
      {
        Salary_Allowance.Text = Salary.Text;
      }
    }

    private void Allowance_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (Salary.Text == "") { return; }

      if (Allowance.Text != "")
      {
        double CurrentSalary = Convert.ToDouble(Salary.Text); // Зарплата

        double CurrentAllowance = CurrentSalary * (Convert.ToDouble(Allowance.Text) / 100); // Надбавка(%)

        double TotalSalary = CurrentSalary + CurrentAllowance; // Общая зарплата

        Salary_Allowance.Text = TotalSalary.ToString();
      }
      else
      {
        Salary_Allowance.Text = Salary.Text;
      }
    }

    private void Dismiss_Click(object sender, RoutedEventArgs e)
    {
      DismissEmployeesWindows = new DismissEmployeesWindows(Application.Current.MainWindow)
      {
        Employees = Employees,
      };

      DismissEmployeesWindows.Date_Of_Dismissal.Visibility = Visibility.Visible;
      DismissEmployeesWindows.Date_Of_Dismissal_Inform.Visibility = Visibility.Collapsed;

      DismissEmployeesWindows.Date_Of_The_Dismissal_Order.Visibility = Visibility.Visible;
      DismissEmployeesWindows.Date_Of_The_Dismissal_Order_Inform.Visibility = Visibility.Collapsed;

      DismissEmployeesWindows.Number_Of_The_Dismissal_Order.IsReadOnly = false;
      DismissEmployeesWindows.Reason_For_Dismissal.IsReadOnly = false;

      DismissEmployeesWindows.Button_Apply_Labour_Activity.Visibility = Visibility.Visible;
      DismissEmployeesWindows.Button_Cancel.Visibility = Visibility.Visible;
      DismissEmployeesWindows.Button_Close.Visibility = Visibility.Collapsed;
      DismissEmployeesWindows.TitleText.Text = "Увольнение сотрудника";

      DismissEmployeesWindows.Back += () =>
      {
        Employees.Status = DismissEmployeesWindows.Status;
        Status.Text = $"{Employees.Status}";

        if (Employees.Status == "Уволен")
        {
          Dismiss.Visibility = Visibility.Collapsed;
          ButtonInformation.Visibility = Visibility.Visible;
        }
      };

      DismissEmployeesWindows.ShowDialog();
    }

    private void ButtonInformation_Click(object sender, RoutedEventArgs e)
    {
      DismissEmployees = DatabaseTableDismissEmployees.GetDismissEmployees(Employees.Id);

      DismissEmployeesWindows = new DismissEmployeesWindows(Application.Current.MainWindow);

      DismissEmployeesWindows.Date_Of_Dismissal.Visibility = Visibility.Collapsed;
      DismissEmployeesWindows.Date_Of_Dismissal_Inform.Visibility = Visibility.Visible;

      DismissEmployeesWindows.Date_Of_The_Dismissal_Order.Visibility = Visibility.Collapsed;
      DismissEmployeesWindows.Date_Of_The_Dismissal_Order_Inform.Visibility = Visibility.Visible;

      DismissEmployeesWindows.Reason_For_Dismissal.Text = DismissEmployees.Reason_For_Dismissal;
      DismissEmployeesWindows.Reason_For_Dismissal.IsReadOnly = true;
      DismissEmployeesWindows.Date_Of_Dismissal_Inform.Text = DismissEmployees.Date_Of_Dismissal;
      DismissEmployeesWindows.Number_Of_The_Dismissal_Order.Text = DismissEmployees.Number_Of_The_Dismissal_Order;
      DismissEmployeesWindows.Number_Of_The_Dismissal_Order.IsReadOnly = true;
      DismissEmployeesWindows.Date_Of_The_Dismissal_Order_Inform.Text = DismissEmployees.Date_Of_The_Dismissal_Order;

      DismissEmployeesWindows.Button_Apply_Labour_Activity.Visibility = Visibility.Collapsed;
      DismissEmployeesWindows.Button_Cancel.Visibility = Visibility.Collapsed;
      DismissEmployeesWindows.Button_Close.Visibility = Visibility.Visible;

      DismissEmployeesWindows.TitleText.Text = "Информация о сотруднике";

      DismissEmployeesWindows.ShowDialog();
    }

    /// <summary>
    /// Проверка на заполненность полей информации о сотруднике
    /// </summary>
    private bool CheckingEmployeeInformation()
    {
      if (StringNull(PlaceOfBirth.Text) && StringNull(DateOfBirth.Text) && StringNull(FullYears.Text) && StringNull(INN.Text) && StringNull(Nationality.Text) && StringNull(Citizenship.Text) && StringNull(SurName.Text) &&
        StringNull(Name.Text) && Convert.ToInt32(FullYears.Text) >= 18)
      {
        return true;
      }
      else
      {
        FullYears.BorderBrush = Convert.ToInt32(FullYears.Text) < 18 || FullYears.Text == "" ? new SolidColorBrush(Color.FromRgb(211, 71, 46)) : SolidColorBrush();

        Warning(PlaceOfBirth.Text, PlaceOfBirth);
        Warning(INN.Text, INN);
        Warning(Nationality.Text, Nationality);
        Warning(Citizenship.Text, Citizenship);
        Warning(SurName.Text, SurName);
        Warning(Name.Text, Name);
      }

      return false;
    }

    /// <summary>
    /// Проверка на заполненность полей паспорта
    /// </summary>
    private bool CheckingEmployee_Passport()
    {
      if (StringNull(Passport_Series.Text) && StringNull(Passport_Number.Text) && StringNull(Date_Of_Issue.Text) && StringNull(Passport_Issued.Text) && StringNull(Division_Code.Text) && StringNull(City.Text) && 
        StringNull(Street.Text) && StringNull(House.Text) && StringNull(Flat.Text))
      {
        return true;
      }
      else
      {
        Warning(Passport_Series.Text, Passport_Series);
        Warning(Passport_Number.Text, Passport_Number);
        Warning(Passport_Issued.Text, Passport_Issued);
        Warning(Division_Code.Text, Division_Code);
        Warning(City.Text, City);
        Warning(Street.Text, Street);
        Warning(House.Text, House);
        Warning(Flat.Text, Flat);
      }

      return false;
    }

    /// <summary>
    /// Проверка на заполненность полей об образовании
    /// </summary>
    private bool CheckingEducation()
    {
      if (StringNull(Name_Document_On_Education.Text) && StringNull(Type_Education.Text) && StringNull(Educational_Institution.Text) && StringNull(Specialty.Text) && StringNull(Qualification.Text) && 
        StringNull(Diplom_Series.Text) && StringNull(Diplom_Number.Text))
      {
        return true;
      }
      else
      {
        Warning(Name_Document_On_Education.Text, Name_Document_On_Education);
        Warning(Type_Education.Text, Type_Education);
        Warning(Educational_Institution.Text, Educational_Institution);
        Warning(Specialty.Text, Specialty);
        Warning(Qualification.Text, Qualification);
        Warning(Diplom_Series.Text, Diplom_Series);
        Warning(Diplom_Number.Text, Qualification);
      }

      return false;
    }

    /// <summary>
    /// Проверка на заполненность полей о трудовой деятельности
    /// </summary>
    private bool CheckingLabour_Activity()
    {
      if (StringNull(Date_Of_Employment.Text) && StringNull(Position.Text) && StringNull(Salary_Allowance.Text) && StringNull(Contract_Number.Text) && StringNull(Date_Of_Contract.Text) && StringNull(Type_Of_Workplace.Text))
      {
        return true;
      }
      else
      {
        Warning(Salary_Allowance.Text, Salary_Allowance);
        Warning(Contract_Number.Text, Contract_Number);
        Warning(Type_Of_Workplace.Text, Type_Of_Workplace);
      }

      return false;
    }

    /// <summary>
    /// Проверка на заполненность всех полей
    /// </summary>
    /// <returns></returns>
    private bool CheckingAll()
    {
      if (CheckingEmployeeInformation() && CheckingEmployee_Passport() && CheckingEducation() && CheckingLabour_Activity())
      {
        return true;
      }
      else
      {
        FullYears.BorderBrush = Convert.ToInt32(FullYears.Text) < 18 || FullYears.Text == "" ? new SolidColorBrush(Color.FromRgb(211, 71, 46)) : SolidColorBrush();

        Warning(PlaceOfBirth.Text, PlaceOfBirth);
        Warning(INN.Text, INN);
        Warning(Nationality.Text, Nationality);
        Warning(Citizenship.Text, Citizenship);
        Warning(SurName.Text, SurName);
        Warning(Name.Text, Name);

        Warning(Passport_Series.Text, Passport_Series);
        Warning(Passport_Number.Text, Passport_Number);
        Warning(Passport_Issued.Text, Passport_Issued);
        Warning(Division_Code.Text, Division_Code);
        Warning(City.Text, City);
        Warning(Street.Text, Street);
        Warning(House.Text, House);
        Warning(Flat.Text, Flat);

        Warning(Name_Document_On_Education.Text, Name_Document_On_Education);
        Warning(Type_Education.Text, Type_Education);
        Warning(Educational_Institution.Text, Educational_Institution);
        Warning(Specialty.Text, Specialty);
        Warning(Qualification.Text, Qualification);

        Warning(Salary_Allowance.Text, Salary_Allowance);
        Warning(Contract_Number.Text, Contract_Number);
        Warning(Type_Of_Workplace.Text, Type_Of_Workplace);
      }

      return false;
    }

    private void EmploymentContract_Click(object sender, RoutedEventArgs e)
    {
      if (CheckingAll())
      {
        Dogovor();
      }
    }

    /// <summary>
    /// Трудовой договор
    /// </summary>
    private void Dogovor()
    {
      string fio = $"{Employees.Surname} {Employees.Name} {Employees.Patronymic}";
      string passport_Series = EmployeesPassportDetails.Passport_Series;
      string passport_Number = EmployeesPassportDetails.Passport_Number;
      string passport_Issued = EmployeesPassportDetails.Passport_Issued;
      string placeOfBirth = EmployeeInformation.Place_Of_Birth;
      string position = Position.Text;
      string salary_Allowance = LabourActivity.Salary;
      string mobile_phone = EmployeeInformation.Mobile_Phone;

      var fieldValues = new Dictionary<string, string>
      {
        {"Текущая дата",  DateTime.Now.ToShortDateString()},
        {"ФИО",  fio},
        {"Паспорт серия",  passport_Series},
        {"Паспорт номер",  passport_Number},
        {"Паспорт выдан",  passport_Issued},
        {"Адрес проживания",  placeOfBirth},
        {"Должность",  position},
        {"Приступить к работе с",  DateTime.Now.ToShortDateString()},
        {"Оклад",  salary_Allowance},
        {"Моб. телефон",  mobile_phone}
      };

      using(System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog())
      {
        saveFileDialog.Title = "Сохранение документа";
        saveFileDialog.Filter = "Word: (*.docx)|*.docx|(*.doc)|*.doc";

        if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          var engine = new Engine();

          engine.Merge("Трудовой договор.docx", fieldValues, saveFileDialog.FileName);
        }
      }
    }

    /// <summary>
    /// Расчет количества лет
    /// </summary>
    private void DateOfBirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
      if (DateOfBirth.Text != "")
      {
        DateTime dateBirthDay = new DateTime(DateOfBirth.SelectedDate.Value.Year, DateOfBirth.SelectedDate.Value.Month, DateOfBirth.SelectedDate.Value.Day);

        DateTime dateNow = DateTime.Now;

        int year = dateNow.Year - dateBirthDay.Year;

        if (dateNow.Month < dateBirthDay.Month || (dateNow.Month == dateBirthDay.Month && dateNow.Day < dateBirthDay.Day))
          year--;

        FullYears.Text = year.ToString();
      }
    }

    //==================================================================
  }
}