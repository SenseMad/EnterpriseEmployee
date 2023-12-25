using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;

namespace EnterpriseEmployee.Scripts.Database
{
  public static class DatabaseTableEmployee
  {
    /// <summary>
    /// Название таблицы
    /// </summary>
    public const string DATABASE_NAME = "Employees";

    /// <summary>
    /// Поле ID
    /// </summary>
    public const string FILED_ID = "Id";

    /// <summary>
    /// Поле Фамилия
    /// </summary>
    public const string SUR_NAME = "SurName";

    /// <summary>
    /// Поле Имя
    /// </summary>
    public const string NAME = "Name";

    /// <summary>
    /// Поле Отчество
    /// </summary>
    public const string PATRONYMIC = "Patronymic";

    /// <summary>
    /// Поле Пол
    /// </summary>
    public const string GENDER = "Gender";

    /// <summary>
    /// Поле Статус
    /// </summary>
    public const string STATUS = "Status";

    //==================================================================

    /// <summary>
    /// Добавить нового сотрудника
    /// </summary>
    public static bool AddEmployees(Employees parEmployees)
    {
      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"INSERT INTO {DATABASE_NAME} ({SUR_NAME}, {NAME}, {PATRONYMIC}, {GENDER}, {STATUS})" +
                                $" VALUES('{parEmployees.Surname}', '{parEmployees.Name}', '{parEmployees.Patronymic}', '{parEmployees.Gender}', '{parEmployees.Status}')";

          command.Prepare();
          command.ExecuteNonQuery();

          connect.Close();
        }
      }
      catch
      {
        MessageBox.Show("При добавлении нового пользователя произошла ошибка!");
      }

      return false;
    }

    //==================================================================

    /// <summary>
    /// Получение списка сотрудников
    /// </summary>
    public static List<Employees> GetEmployees()
    {
      var retEmployees = new List<Employees>();

      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"SELECT * FROM {DATABASE_NAME} ORDER BY {FILED_ID}, {SUR_NAME}, {NAME}, {PATRONYMIC}, {GENDER}, {STATUS}";

          using (SQLiteDataReader dataReader = command.ExecuteReader())
          {
            if (dataReader.HasRows)
            {
              while (dataReader.Read())
              {
                var employees = new Employees
                (
                  Convert.ToInt32(dataReader[FILED_ID]),
                  Convert.ToString(dataReader[SUR_NAME]),
                  Convert.ToString(dataReader[NAME]),
                  Convert.ToString(dataReader[PATRONYMIC]),
                  Convert.ToString(dataReader[GENDER]),
                  Convert.ToString(dataReader[STATUS])
                );

                retEmployees.Add(employees);
              }

              dataReader.Close();
            }

            connect.Close();
          }
        }
      }
      catch { }

      return retEmployees;
    }

    //==================================================================

    public static bool UpdateEmployees(Employees parEmployees)
    {
      using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
      {
        connect.Open();

        SQLiteCommand command = connect.CreateCommand();
        command.CommandText = $"UPDATE {DATABASE_NAME} SET {SUR_NAME}='{parEmployees.Surname}', {NAME}='{parEmployees.Name}', {PATRONYMIC}='{parEmployees.Patronymic}', {GENDER}='{parEmployees.Gender}', " +
          $"{STATUS}='{parEmployees.Status}' WHERE {FILED_ID} = '{parEmployees.Id}'";

        command.Prepare();
        command.ExecuteNonQuery();

        connect.Close();
      }

      return false;
    }

    //==================================================================
  }
}