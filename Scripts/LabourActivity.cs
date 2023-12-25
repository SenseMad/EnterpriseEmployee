namespace EnterpriseEmployee.Scripts
{
  /// <summary>
  /// Трудовая деятельность
  /// </summary>
  public class LabourActivity
  {
    /// <summary>
    /// Id Сотрудника
    /// </summary>
    public int Id_Employee { get; set; }

    /// <summary>
    /// Дата приема на работу
    /// </summary>
    public string Date_Of_Employment { get; set; }

    /// <summary>
    /// Должность
    /// </summary>
    public string Position { get; set; }

    /// <summary>
    /// Надбавка(%)
    /// </summary>
    public string Allowance { get; set; }

    /// <summary>
    /// Оклад с надбавкой
    /// </summary>
    public string Salary { get; set; }

    /// <summary>
    /// № Договора
    /// </summary>
    public string Contract_Number { get; set; }

    /// <summary>
    /// Дата договора
    /// </summary>
    public string Date_Of_Contract { get; set; }

    /// <summary>
    /// Тип места работы
    /// </summary>
    public string Type_Of_Workplace { get; set; }

    /// <summary>
    /// Стаж работы
    /// </summary>
    public string Work_Experience { get; set; }

    //==================================================================

    public LabourActivity() :
  this(-1, "", "", "", "", "", "", "", "")
    { }

    public LabourActivity(string parDate_Of_Employment, string parPosition, string parAllowance, string parSalary, string parContract_Number, string parDate_Of_Contract, string parType_Of_Workplace, string parWork_Experience) :
      this(-1, parDate_Of_Employment, parPosition, parAllowance, parSalary, parContract_Number, parDate_Of_Contract, parType_Of_Workplace, parWork_Experience)
    { }

    public LabourActivity(int parId_Employee, string parDate_Of_Employment, string parPosition, string parAllowance, string parSalary, string parContract_Number, string parDate_Of_Contract, string parType_Of_Workplace, 
      string parWork_Experience)
    {
      Id_Employee = parId_Employee;
      Date_Of_Employment = parDate_Of_Employment;
      Position = parPosition;
      Allowance = parAllowance;
      Salary = parSalary;
      Contract_Number = parContract_Number;
      Date_Of_Contract = parDate_Of_Contract;
      Type_Of_Workplace = parType_Of_Workplace;
      Work_Experience = parWork_Experience;
    }

    //==================================================================
  }
}