using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace EnterpriseEmployee.Scripts.Database
{
  /// <summary>
  /// (База данных) Информация о сотруднике
  /// </summary>
  public static class DatabaseTableEmployeeInformation
  {
    /// <summary>
    /// Название таблицы
    /// </summary>
    public const string DATABASE_NAME = "EmployeeInformation";

    /// <summary>
    /// Поле ID Сотрудника
    /// </summary>
    public const string ID_EMPLOYEE = "Id_Employee";

    /// <summary>
    /// Поле Место рождения
    /// </summary>
    public const string PLACE_OF_BIRTH = "Place_Of_Birth";

    /// <summary>
    /// Поле Дата рождения
    /// </summary>
    public const string DATE_OF_BIRTH = "Date_Of_Birth";

    /// <summary>
    /// Поле Полных лет
    /// </summary>
    public const string FULL_YEARS = "Full_Years";

    /// <summary>
    /// Поле Мобильный телефон
    /// </summary>
    public const string MOBILE_PHONE = "Mobile_Phone";

    /// <summary>
    /// Поле Домашний телефон
    /// </summary>
    public const string HOME_PHONE = "Home_Phone";

    /// <summary>
    /// Поле Рабочий телефон
    /// </summary>
    public const string OFFICE_PHONE = "Office_Phone";

    /// <summary>
    /// Поле Email
    /// </summary>
    public const string EMAIL = "Email";

    /// <summary>
    /// Поле ИНН
    /// </summary>
    public const string INN = "INN";

    /// <summary>
    /// Поле № Мед. полиса
    /// </summary>
    public const string MEDICAL_POLICY = "Medical_Policy";

    /// <summary>
    /// Поле Национальность
    /// </summary>
    public const string NATIONALITY = "Nationality";

    /// <summary>
    /// Поле Гражданство
    /// </summary>
    public const string CITIZENSHIP = "Citizenship";

    //==================================================================

    /// <summary>
    /// Получение списка сотрудников
    /// </summary>
    public static List<EmployeeInformation> GetEmployeeInformations()
    {
      var retEmployeeInformation = new List<EmployeeInformation>();

      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"SELECT * FROM {DATABASE_NAME} ORDER BY {ID_EMPLOYEE}, {PLACE_OF_BIRTH}, {DATE_OF_BIRTH}, {FULL_YEARS}, {MOBILE_PHONE}, {HOME_PHONE}, {OFFICE_PHONE}, {EMAIL}, {INN}, {MEDICAL_POLICY}, " +
            $"{NATIONALITY}, {CITIZENSHIP}";

          using (SQLiteDataReader dataReader = command.ExecuteReader())
          {
            if (dataReader.HasRows)
            {
              while (dataReader.Read())
              {
                var employeeInformation = new EmployeeInformation
                (
                  Convert.ToInt32(dataReader[ID_EMPLOYEE]),
                  Convert.ToString(dataReader[PLACE_OF_BIRTH]),
                  Convert.ToString(dataReader[DATE_OF_BIRTH]),
                  Convert.ToString(dataReader[FULL_YEARS]),
                  Convert.ToString(dataReader[MOBILE_PHONE]),
                  Convert.ToString(dataReader[HOME_PHONE]),
                  Convert.ToString(dataReader[OFFICE_PHONE]),
                  Convert.ToString(dataReader[EMAIL]),
                  Convert.ToString(dataReader[INN]),
                  Convert.ToString(dataReader[MEDICAL_POLICY]),
                  Convert.ToString(dataReader[NATIONALITY]),
                  Convert.ToString(dataReader[CITIZENSHIP])

                );

                retEmployeeInformation.Add(employeeInformation);
              }

              dataReader.Close();
            }

            connect.Close();
          }
        }
      }
      catch { }

      return retEmployeeInformation;
    }

    /// <summary>
    /// Получение данных сотрудников
    /// </summary>
    public static EmployeeInformation GetEmployeeInformations(int parId)
    {
      var Data = new EmployeeInformation();

      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"SELECT * FROM {DATABASE_NAME} WHERE {ID_EMPLOYEE} = {parId}";

          using (SQLiteDataReader dataReader = command.ExecuteReader())
          {
            while (dataReader.Read())
            {
              var employeeInformation = new EmployeeInformation
              (
                  Convert.ToInt32(dataReader[ID_EMPLOYEE]),
                  Convert.ToString(dataReader[PLACE_OF_BIRTH]),
                  Convert.ToString(dataReader[DATE_OF_BIRTH]),
                  Convert.ToString(dataReader[FULL_YEARS]),
                  Convert.ToString(dataReader[MOBILE_PHONE]),
                  Convert.ToString(dataReader[HOME_PHONE]),
                  Convert.ToString(dataReader[OFFICE_PHONE]),
                  Convert.ToString(dataReader[EMAIL]),
                  Convert.ToString(dataReader[INN]),
                  Convert.ToString(dataReader[MEDICAL_POLICY]),
                  Convert.ToString(dataReader[NATIONALITY]),
                  Convert.ToString(dataReader[CITIZENSHIP])
              );

              Data = employeeInformation;
            }

            connect.Close();
          }
        }
      }
      catch { }

      return Data;
    }

    //==================================================================

    public static bool AddEmployeeInformation(EmployeeInformation parEmployeeInformation)
    {
      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"INSERT INTO {DATABASE_NAME} ({ID_EMPLOYEE}, {PLACE_OF_BIRTH}, {DATE_OF_BIRTH}, {FULL_YEARS}, {MOBILE_PHONE}, {HOME_PHONE}, {OFFICE_PHONE}, {EMAIL}, {INN}, {MEDICAL_POLICY}, " +
              $"{NATIONALITY}, {CITIZENSHIP})" +
                                $" VALUES('{parEmployeeInformation.Id_Employee}', '{parEmployeeInformation.Place_Of_Birth}', '{parEmployeeInformation.Date_Of_Birth}', '{parEmployeeInformation.Full_Years}', " +
                                $"'{parEmployeeInformation.Mobile_Phone}', '{parEmployeeInformation.Home_Phone}', '{parEmployeeInformation.Office_Phone}', '{parEmployeeInformation.Email}', '{parEmployeeInformation.INN}', " +
                                $"'{parEmployeeInformation.Medical_Policy}', '{parEmployeeInformation.Nationality}', '{parEmployeeInformation.Citizenship}')";

          command.Prepare();
          command.ExecuteNonQuery();

          connect.Close();
        }
      }
      catch { }

      return false;
    }

    //==================================================================

    public static bool UpdateEmployeeInformation(EmployeeInformation parEmployeeInformation)
    {
      using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
      {
        connect.Open();

        SQLiteCommand command = connect.CreateCommand();
        command.CommandText = $"UPDATE {DATABASE_NAME} SET {PLACE_OF_BIRTH}='{parEmployeeInformation.Place_Of_Birth}', {DATE_OF_BIRTH}='{parEmployeeInformation.Date_Of_Birth}', {FULL_YEARS}='{parEmployeeInformation.Full_Years}', " +
          $"{MOBILE_PHONE}='{parEmployeeInformation.Mobile_Phone}', {HOME_PHONE}='{parEmployeeInformation.Home_Phone}', {OFFICE_PHONE}='{parEmployeeInformation.Office_Phone}', {EMAIL}='{parEmployeeInformation.Email}', " +
          $"{INN}='{parEmployeeInformation.INN}', {MEDICAL_POLICY}='{parEmployeeInformation.Medical_Policy}', {NATIONALITY}='{parEmployeeInformation.Nationality}', {CITIZENSHIP}='{parEmployeeInformation.Citizenship}' " +
          $"WHERE {ID_EMPLOYEE} = '{parEmployeeInformation.Id_Employee}'";

        command.Prepare();
        command.ExecuteNonQuery();

        connect.Close();
      }

      return false;
    }

    //==================================================================
  }
}