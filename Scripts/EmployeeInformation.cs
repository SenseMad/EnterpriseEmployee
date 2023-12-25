namespace EnterpriseEmployee.Scripts
{
  public class EmployeeInformation
  {
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id_Employee { get; set; }

    /// <summary>
    /// Место рождения
    /// </summary>
    public string Place_Of_Birth { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public string Date_Of_Birth { get; set; }

    /// <summary>
    /// Полных лет
    /// </summary>
    public string Full_Years { get; set; }

    /// <summary>
    /// Мобильный телефон
    /// </summary>
    public string Mobile_Phone { get; set; }

    /// <summary>
    /// Домашний телефон
    /// </summary>
    public string Home_Phone { get; set; }

    /// <summary>
    /// Рабочий телефон
    /// </summary>
    public string Office_Phone { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// ИНН
    /// </summary>
    public string INN { get; set; }

    /// <summary>
    /// № Мед. полиса
    /// </summary>
    public string Medical_Policy { get; set; }

    /// <summary>
    /// Национальность
    /// </summary>
    public string Nationality { get; set; }

    /// <summary>
    /// Гражданство
    /// </summary>
    public string Citizenship { get; set; }

    //==================================================================

    public EmployeeInformation() :
      this(-1, "", "", "", "", "", "", "", "", "", "", "")
    { }

    public EmployeeInformation(string parPlace_Of_Birth, string parDate_Of_Birth, string parFull_Years, string parMobile_Phone, string parHome_Phone, string parOffice_Phone, string parEmail, string parINN, 
      string parMedical_Policy, string parNationality, string parCitizenship) :
      this(-1, parPlace_Of_Birth, parDate_Of_Birth, parFull_Years, parMobile_Phone, parHome_Phone, parOffice_Phone, parEmail, parINN, parMedical_Policy, parNationality, parCitizenship)
    { }

    public EmployeeInformation(int parId_Employee, string parPlace_Of_Birth, string parDate_Of_Birth, string parFull_Years, string parMobile_Phone, string parHome_Phone, string parOffice_Phone, string parEmail, string parINN,
      string parMedical_Policy, string parNationality, string parCitizenship)
    {
      Id_Employee = parId_Employee;
      Place_Of_Birth = parPlace_Of_Birth;
      Date_Of_Birth = parDate_Of_Birth;
      Full_Years = parFull_Years;
      Mobile_Phone = parMobile_Phone;
      Home_Phone = parHome_Phone;
      Office_Phone = parOffice_Phone;
      Email = parEmail;
      INN = parINN;
      Medical_Policy = parMedical_Policy;
      Nationality = parNationality;
      Citizenship = parCitizenship;
    }

    //==================================================================
  }
}