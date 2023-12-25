namespace EnterpriseEmployee.Scripts
{
  public class Employees
  {
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string Patronymic { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public string Status { get; set; }

    //==================================================================

    public Employees() :
      this(-1, "", "", "", "", "")
    { }

    public Employees(string parSurname, string parName, string parPatronymic, string parGender, string parStatus) :
      this(-1, parSurname, parName, parPatronymic, parGender, parStatus)
    { }

    public Employees(int parId, string parSurname, string parName, string parPatronymic, string parGender, string parStatus)
    {
      Id = parId;
      Surname = parSurname;
      Name = parName;
      Patronymic = parPatronymic;
      Gender = parGender;
      Status = parStatus;
    }

    //==================================================================
  }
}