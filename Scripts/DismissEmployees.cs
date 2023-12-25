namespace EnterpriseEmployee.Scripts
{
  public class DismissEmployees
  {
    /// <summary>
    /// Id Сотрудника
    /// </summary>
    public int Id_Employee { get; set; }

    /// <summary>
    /// Причина увольнения
    /// </summary>
    public string Reason_For_Dismissal { get; set; }

    /// <summary>
    /// Дата увольнения
    /// </summary>
    public string Date_Of_Dismissal { get; set; }

    /// <summary>
    /// № приказа на увольнение
    /// </summary>
    public string Number_Of_The_Dismissal_Order { get; set; }

    /// <summary>
    /// Дата приказа на увольнение
    /// </summary>
    public string Date_Of_The_Dismissal_Order { get; set; }

    //==================================================================

    public DismissEmployees() :
      this(-1, "", "", "", "")
    { }

    public DismissEmployees(string parReason_For_Dismissal, string parDate_Of_Dismissal, string parNumber_Of_The_Dismissal_Order, string parDate_Of_The_Dismissal_Order) :
      this(-1, parReason_For_Dismissal, parDate_Of_Dismissal, parNumber_Of_The_Dismissal_Order, parDate_Of_The_Dismissal_Order)
    { }

    public DismissEmployees(int parId_Employee, string parReason_For_Dismissal, string parDate_Of_Dismissal, string parNumber_Of_The_Dismissal_Order, string parDate_Of_The_Dismissal_Order)
    {
      Id_Employee = parId_Employee;
      Reason_For_Dismissal = parReason_For_Dismissal;
      Date_Of_Dismissal = parDate_Of_Dismissal;
      Number_Of_The_Dismissal_Order = parNumber_Of_The_Dismissal_Order;
      Date_Of_The_Dismissal_Order = parDate_Of_The_Dismissal_Order;
    }

    //==================================================================
  }
}