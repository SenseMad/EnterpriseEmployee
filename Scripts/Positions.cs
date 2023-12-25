namespace EnterpriseEmployee.Scripts
{
  /// <summary>
  /// Должности
  /// </summary>
  public class Positions
  {
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название должности
    /// </summary>
    public string Job_Title { get; set; }

    /// <summary>
    /// Оклад
    /// </summary>
    public string Salary { get; set; }

    //==================================================================

    public Positions() :
      this(-1, "", "")
    { }

    public Positions(string parJob_Title, string parSalary) :
      this(-1, parJob_Title, parSalary)
    { }

    public Positions(int parId, string parJob_Title, string parSalary)
    {
      Id = parId;
      Job_Title = parJob_Title;
      Salary = parSalary;
    }

    //==================================================================
  }
}