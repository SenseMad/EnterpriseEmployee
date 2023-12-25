namespace EnterpriseEmployee.Scripts
{
  /// <summary>
  /// Данные паспорта
  /// </summary>
  public class EmployeesPassportDetails
  {
    /// <summary>
    /// Id Сотрудника
    /// </summary>
    public int Id_Employee { get; set; }

    /// <summary>
    /// (Паспорт) серия
    /// </summary>
    public string Passport_Series { get; set; }

    /// <summary>
    /// (Паспорт) номер
    /// </summary>
    public string Passport_Number { get; set; }

    /// <summary>
    /// (Паспорт) Дата выдачи
    /// </summary>
    public string Date_Of_Issue { get; set; }

    /// <summary>
    /// (Паспорт) Выдавший орган
    /// </summary>
    public string Passport_Issued { get; set; }

    /// <summary>
    /// (Паспорт) Код подразделения
    /// </summary>
    public string Division_Code { get; set; }

    /// <summary>
    /// Город
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Улица
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    /// Дом
    /// </summary>
    public string House { get; set; }

    /// <summary>
    /// Квартира
    /// </summary>
    public string Flat { get; set; }

    //==================================================================

    public EmployeesPassportDetails() :
      this(-1, "", "", "", "", "", "", "", "", "")
    { }

    public EmployeesPassportDetails(string parPassport_Series, string parPassport_Number, string parDate_Of_Issue, string parPassport_Issued, string parDivision_Code, string parCity, string parStreet, string parHouse, 
      string parFlat) :
      this(-1, parPassport_Series, parPassport_Number, parDate_Of_Issue, parPassport_Issued, parDivision_Code, parCity, parStreet, parHouse, parFlat)
    { }

    public EmployeesPassportDetails(int parId_Employee, string parPassport_Series, string parPassport_Number, string parDate_Of_Issue, string parPassport_Issued, string parDivision_Code, string parCity, string parStreet, 
      string parHouse, string parFlat)
    {
      Id_Employee = parId_Employee;
      Passport_Series = parPassport_Series;
      Passport_Number = parPassport_Number;
      Date_Of_Issue = parDate_Of_Issue;
      Passport_Issued = parPassport_Issued;
      Division_Code = parDivision_Code;
      City = parCity;
      Street = parStreet;
      House = parHouse;
      Flat = parFlat;
    }

    //==================================================================
  }
}