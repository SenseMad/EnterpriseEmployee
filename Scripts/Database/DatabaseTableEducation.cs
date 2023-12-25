using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace EnterpriseEmployee.Scripts.Database
{
  /// <summary>
  /// (База данных) Информация об образовании
  /// </summary>
  public static class DatabaseTableEducation
  {
    /// <summary>
    /// Название таблицы
    /// </summary>
    public const string DATABASE_NAME = "Education";

    /// <summary>
    /// Поле ID Сотрудника
    /// </summary>
    public const string ID_EMPLOYEE = "Id_Employee";

    /// <summary>
    /// Поле Наименование документа об образовании
    /// </summary>
    public const string NAME_DOCUMENT_ON_EDUCATION = "Name_Document_On_Education";

    /// <summary>
    /// Поле Тип образования
    /// </summary>
    public const string TYPE_EDUCATION = "Type_Education";

    /// <summary>
    /// Поле Наименование учебного заведения
    /// </summary>
    public const string EDUCATIONAL_INSTITUTION = "Educational_Institution";

    /// <summary>
    /// Поле Специальность
    /// </summary>
    public const string SPECIALTY = "Specialty";

    /// <summary>
    /// Поле Квалификация
    /// </summary>
    public const string QUALIFICATION = "Qualification";

    /// <summary>
    /// Поле Серия диплопа
    /// </summary>
    public const string DIPLOM_SERIES = "Diplom_Series";

    /// <summary>
    /// Поле Номер диплома
    /// </summary>
    public const string DIPLOM_NUMBER = "Diplom_Number";

    //==================================================================

    /// <summary>
    /// Получение данных сотрудников
    /// </summary>
    public static Education GetEducation(int parId_Employee)
    {
      var Data = new Education();

      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"SELECT * FROM {DATABASE_NAME} WHERE {ID_EMPLOYEE} = {parId_Employee}";

          using (SQLiteDataReader dataReader = command.ExecuteReader())
          {
            while (dataReader.Read())
            {
              var education = new Education
              (
                  Convert.ToInt32(dataReader[ID_EMPLOYEE]),
                  Convert.ToString(dataReader[NAME_DOCUMENT_ON_EDUCATION]),
                  Convert.ToString(dataReader[TYPE_EDUCATION]),
                  Convert.ToString(dataReader[EDUCATIONAL_INSTITUTION]),
                  Convert.ToString(dataReader[SPECIALTY]),
                  Convert.ToString(dataReader[QUALIFICATION]),
                  Convert.ToString(dataReader[DIPLOM_SERIES]),
                  Convert.ToString(dataReader[DIPLOM_NUMBER])
              );

              Data = education;
            }

            connect.Close();
          }
        }
      }
      catch { }

      return Data;
    }

    //==================================================================

    public static bool AddEmployeeInformation(Education parEducation)
    {
      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"INSERT INTO {DATABASE_NAME} ({ID_EMPLOYEE}, {NAME_DOCUMENT_ON_EDUCATION}, {TYPE_EDUCATION}, {EDUCATIONAL_INSTITUTION}, {SPECIALTY}, {QUALIFICATION}, {DIPLOM_SERIES}, {DIPLOM_NUMBER})" +
                                $" VALUES('{parEducation.Id_Employee}', '{parEducation.Name_Document_On_Education}', '{parEducation.Type_Education}', '{parEducation.Educational_Institution}', '{parEducation.Specialty}', " +
                                $"'{parEducation.Qualification}', '{parEducation.Diplom_Series}', '{parEducation.Diplom_Series}')";

          command.Prepare();
          command.ExecuteNonQuery();

          connect.Close();
        }
      }
      catch { }

      return false;
    }

    //==================================================================

    public static bool UpdateEducation(Education parEducation)
    {
      using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
      {
        connect.Open();

        SQLiteCommand command = connect.CreateCommand();
        command.CommandText = $"UPDATE {DATABASE_NAME} SET {NAME_DOCUMENT_ON_EDUCATION}='{parEducation.Name_Document_On_Education}', {TYPE_EDUCATION}='{parEducation.Type_Education}', " +
          $"{EDUCATIONAL_INSTITUTION}='{parEducation.Educational_Institution}', {SPECIALTY}='{parEducation.Specialty}', {QUALIFICATION}='{parEducation.Qualification}', {DIPLOM_SERIES}='{parEducation.Diplom_Series}', " +
          $"{DIPLOM_NUMBER}='{parEducation.Diplom_Number}' WHERE {ID_EMPLOYEE} = '{parEducation.Id_Employee}'";

        command.Prepare();
        command.ExecuteNonQuery();

        connect.Close();
      }

      return false;
    }

    //==================================================================
  }
}