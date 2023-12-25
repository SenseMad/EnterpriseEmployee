using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace EnterpriseEmployee.Scripts.Database
{
  /// <summary>
  /// (База данных) Информация о трудовой деятельности
  /// </summary>
  public static class DatabaseTableLabourActivity
  {
    /// <summary>
    /// Название таблицы
    /// </summary>
    public const string DATABASE_NAME = "LabourActivity";

    /// <summary>
    /// Поле ID Сотрудника
    /// </summary>
    public const string ID_EMPLOYEE = "Id_Employee";

    /// <summary>
    /// Поле Дата приема на работу
    /// </summary>
    public const string DATE_OF_EMPLOYMENT = "Date_Of_Employment";

    /// <summary>
    /// Поле Должность
    /// </summary>
    public const string POSITION = "Position";

    /// <summary>
    /// Поле Надбавка(%)
    /// </summary>
    public const string ALLOWANCE = "Allowance";

    /// <summary>
    /// Поле Оклад с надбавкой
    /// </summary>
    public const string SALARY = "Salary";

    /// <summary>
    /// Поле № Договора
    /// </summary>
    public const string CONTRACT_NUMBER = "Contract_Number";

    /// <summary>
    /// Поле Дата договора
    /// </summary>
    public const string DATE_OF_CONTRACT = "Date_Of_Contract";

    /// <summary>
    /// Поле Тип места работы
    /// </summary>
    public const string TYPY_OF_WORKPLACE = "Type_Of_Workplace";

    /// <summary>
    /// Поле Стаж работы
    /// </summary>
    public const string WORK_EXPERIENCE = "Work_Experience";

    //==================================================================

    /// <summary>
    /// Получение данных о трудовой деятельности сотрудника
    /// </summary>
    public static LabourActivity GetLabourActivity(int parId_Employee)
    {
      var Data = new LabourActivity();

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
              var labourActivity = new LabourActivity
              (
                  Convert.ToInt32(dataReader[ID_EMPLOYEE]),
                  Convert.ToString(dataReader[DATE_OF_EMPLOYMENT]),
                  Convert.ToString(dataReader[POSITION]),
                  Convert.ToString(dataReader[ALLOWANCE]),
                  Convert.ToString(dataReader[SALARY]),
                  Convert.ToString(dataReader[CONTRACT_NUMBER]),
                  Convert.ToString(dataReader[DATE_OF_CONTRACT]),
                  Convert.ToString(dataReader[TYPY_OF_WORKPLACE]),
                  Convert.ToString(dataReader[WORK_EXPERIENCE])
              );

              Data = labourActivity;
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
    /// Добавить данные о трудовой деятельности
    /// </summary>
    public static bool AddLabourActivity(LabourActivity parLabourActivity)
    {
      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"INSERT INTO {DATABASE_NAME} ({ID_EMPLOYEE}, {DATE_OF_EMPLOYMENT}, {POSITION}, {ALLOWANCE}, {SALARY}, {CONTRACT_NUMBER}, {DATE_OF_CONTRACT}, {TYPY_OF_WORKPLACE}, {WORK_EXPERIENCE})" +
                                $" VALUES('{parLabourActivity.Id_Employee}', '{parLabourActivity.Date_Of_Employment}', '{parLabourActivity.Position}', '{parLabourActivity.Allowance}', '{parLabourActivity.Salary}', " +
                                $"'{parLabourActivity.Contract_Number}', '{parLabourActivity.Date_Of_Contract}', '{parLabourActivity.Type_Of_Workplace}', '{parLabourActivity.Work_Experience}')";

          command.Prepare();
          command.ExecuteNonQuery();

          connect.Close();
        }
      }
      catch { }

      return false;
    }

    //==================================================================

    public static bool UpdateLabourActivity(LabourActivity parLabourActivity)
    {
      using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
      {
        connect.Open();

        SQLiteCommand command = connect.CreateCommand();
        command.CommandText = $"UPDATE {DATABASE_NAME} SET {DATE_OF_EMPLOYMENT}='{parLabourActivity.Date_Of_Employment}', {POSITION}='{parLabourActivity.Position}', {ALLOWANCE}='{parLabourActivity.Allowance}', " +
          $"{SALARY}='{parLabourActivity.Salary}', {CONTRACT_NUMBER}='{parLabourActivity.Contract_Number}', {DATE_OF_CONTRACT}='{parLabourActivity.Date_Of_Contract}', " +
          $"{TYPY_OF_WORKPLACE}='{parLabourActivity.Type_Of_Workplace}', {WORK_EXPERIENCE}='{parLabourActivity.Work_Experience}' WHERE {ID_EMPLOYEE} = '{parLabourActivity.Id_Employee}'";

        command.Prepare();
        command.ExecuteNonQuery();

        connect.Close();
      }

      return false;
    }

    //==================================================================
  }
}