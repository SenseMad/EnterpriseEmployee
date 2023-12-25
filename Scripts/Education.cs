namespace EnterpriseEmployee.Scripts
{
  /// <summary>
  /// Образование
  /// </summary>
  public class Education
  {
    /// <summary>
    /// Id Сотрудника
    /// </summary>
    public int Id_Employee { get; set; }

    /// <summary>
    /// Наименование документа об образовании
    /// </summary>
    public string Name_Document_On_Education { get; set; }

    /// <summary>
    /// Тип образования
    /// </summary>
    public string Type_Education { get; set; }

    /// <summary>
    /// Наименование учебного заведения
    /// </summary>
    public string Educational_Institution { get; set; }

    /// <summary>
    /// Специальность
    /// </summary>
    public string Specialty { get; set; }

    /// <summary>
    /// Квалификация
    /// </summary>
    public string Qualification { get; set; }

    /// <summary>
    /// Серия диплопа
    /// </summary>
    public string Diplom_Series { get; set; }

    /// <summary>
    /// Номер диплома
    /// </summary>
    public string Diplom_Number { get; set; }

    //==================================================================

    public Education() :
      this(-1, "", "", "", "", "", "", "")
    { }

    public Education(string parName_Document_On_Education, string parType_Education, string parEducational_Institution, string parSpecialty, string parQualification, string parDiplom_Series, string parDiplom_Number) :
      this(-1, parName_Document_On_Education, parType_Education, parEducational_Institution, parSpecialty, parQualification, parDiplom_Series, parDiplom_Number)
    { }

    public Education(int parId_Employee, string parName_Document_On_Education, string parType_Education, string parEducational_Institution, string parSpecialty, string parQualification, string parDiplom_Series, 
      string parDiplom_Number)
    {
      Id_Employee = parId_Employee;
      Name_Document_On_Education = parName_Document_On_Education;
      Type_Education = parType_Education;
      Educational_Institution = parEducational_Institution;
      Specialty = parSpecialty;
      Qualification = parQualification;
      Diplom_Series = parDiplom_Series;
      Diplom_Number = parDiplom_Number;
    }

    //==================================================================
  }
}