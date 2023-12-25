using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace EnterpriseEmployee.Scripts.Database
{
  /// <summary>
  /// (База данных) Паспорт
  /// </summary>
  public static class DatabaseTableEmployeesPassportDetails
  {
    /// <summary>
    /// Название таблицы
    /// </summary>
    public const string DATABASE_NAME = "EmployeesPassportDetails";

    /// <summary>
    /// Поле ID Сотрудника
    /// </summary>
    public const string ID_EMPLOYEE = "Id_Employee";

    /// <summary>
    /// Поле Паспорт Серия
    /// </summary>
    public const string PASSPORT_SERIES = "Passport_Series";

    /// <summary>
    /// Поле Паспорт Номер
    /// </summary>
    public const string PASSPORT_NUMBER = "Passport_Number";

    /// <summary>
    /// Поле Дата выдачи
    /// </summary>
    public const string DATE_OF_ISSUE = "Date_Of_Issue";

    /// <summary>
    /// Поле Выдавший орган
    /// </summary>
    public const string PASSPORT_ISSUED = "Passport_Issued";

    /// <summary>
    /// Поле Код подразделения
    /// </summary>
    public const string DIVISION_CODE = "Division_Code";

    /// <summary>
    /// Поле Город
    /// </summary>
    public const string CITY = "City";

    /// <summary>
    /// Поле Улица
    /// </summary>
    public const string STREET = "Street";

    /// <summary>
    /// Поле Дом
    /// </summary>
    public const string HOUSE = "House";

    /// <summary>
    /// Поле Квартира
    /// </summary>
    public const string FLAT = "Flat";

    //==================================================================

    /// <summary>
    /// Получение данных паспорта о сотрудниках
    /// </summary>
    public static EmployeesPassportDetails GetEmployeesPassportDetails(int parId_Employee)
    {
      var Data = new EmployeesPassportDetails();

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
              var employeesPassportDetails = new EmployeesPassportDetails
              (
                  Convert.ToInt32(dataReader[ID_EMPLOYEE]),
                  Convert.ToString(dataReader[PASSPORT_SERIES]),
                  Convert.ToString(dataReader[PASSPORT_NUMBER]),
                  Convert.ToString(dataReader[DATE_OF_ISSUE]),
                  Convert.ToString(dataReader[PASSPORT_ISSUED]),
                  Convert.ToString(dataReader[DIVISION_CODE]),
                  Convert.ToString(dataReader[CITY]),
                  Convert.ToString(dataReader[STREET]),
                  Convert.ToString(dataReader[HOUSE]),
                  Convert.ToString(dataReader[FLAT])
              );

              Data = employeesPassportDetails;
            }

            connect.Close();
          }
        }
      }
      catch { }

      return Data;
    }

    //==================================================================

    /// <summary>
    /// Добавить данные паспорта
    /// </summary>
    public static bool AddEmployeesPassportDetails(EmployeesPassportDetails parEmployeesPassportDetails)
    {
      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"INSERT INTO {DATABASE_NAME} ({ID_EMPLOYEE}, {PASSPORT_SERIES}, {PASSPORT_NUMBER}, {DATE_OF_ISSUE}, {PASSPORT_ISSUED}, {DIVISION_CODE}, {CITY}, {STREET}, {HOUSE}, {FLAT})" +
                                $" VALUES('{parEmployeesPassportDetails.Id_Employee}', '{parEmployeesPassportDetails.Passport_Series}', '{parEmployeesPassportDetails.Passport_Number}', " +
                                $"'{parEmployeesPassportDetails.Date_Of_Issue}', '{parEmployeesPassportDetails.Passport_Issued}', '{parEmployeesPassportDetails.Division_Code}', '{parEmployeesPassportDetails.City}', " +
                                $"'{parEmployeesPassportDetails.Street}', '{parEmployeesPassportDetails.House}', '{parEmployeesPassportDetails.Flat}')";

          command.Prepare();
          command.ExecuteNonQuery();

          connect.Close();
        }
      }
      catch { }

      return false;
    }

    //==================================================================

    public static bool UpdateEmployeesPassportDetails(EmployeesPassportDetails parEmployeesPassportDetails)
    {
      using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
      {
        connect.Open();

        SQLiteCommand command = connect.CreateCommand();
        command.CommandText = $"UPDATE {DATABASE_NAME} SET {PASSPORT_SERIES}='{parEmployeesPassportDetails.Passport_Series}', {PASSPORT_NUMBER}='{parEmployeesPassportDetails.Passport_Number}', " +
          $"{DATE_OF_ISSUE}='{parEmployeesPassportDetails.Date_Of_Issue}', {PASSPORT_ISSUED}='{parEmployeesPassportDetails.Passport_Issued}', {DIVISION_CODE}='{parEmployeesPassportDetails.Division_Code}', " +
          $"{CITY}='{parEmployeesPassportDetails.City}', {STREET}='{parEmployeesPassportDetails.Street}', {HOUSE}='{parEmployeesPassportDetails.House}', {FLAT}='{parEmployeesPassportDetails.Flat}' " +
          $"WHERE {ID_EMPLOYEE} = '{parEmployeesPassportDetails.Id_Employee}'";

        command.Prepare();
        command.ExecuteNonQuery();

        connect.Close();
      }

      return false;
    }

    //==================================================================
  }
}