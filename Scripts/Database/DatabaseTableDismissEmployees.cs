using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace EnterpriseEmployee.Scripts.Database
{
  public static class DatabaseTableDismissEmployees
  {
    /// <summary>
    /// Название таблицы
    /// </summary>
    public const string DATABASE_NAME = "DismissEmployees";

    /// <summary>
    /// Поле ID Сотрудника
    /// </summary>
    public const string ID_EMPLOYEE = "Id_Employee";

    /// <summary>
    /// Поле Причина увольнения
    /// </summary>
    public const string REASON_FOR_DISMISSAL = "Reason_For_Dismissal";

    /// <summary>
    /// Поле Дата увольнения
    /// </summary>
    public const string DATE_OF_DISMASSAL = "Date_Of_Dismissal";

    /// <summary>
    /// Поле № приказа на увольнение
    /// </summary>
    public const string NUMBER_OF_THE_DISMISSAL_ORDER = "Number_Of_The_Dismissal_Order";

    /// <summary>
    /// Поле Дата приказа на увольнение
    /// </summary>
    public const string DATE_OF_THE_DISMISSAL_ORDER = "Date_Of_The_Dismissal_Order";

    //==================================================================



    //==================================================================

    public static bool AddDismissEmployees(DismissEmployees parDismissEmployees)
    {
      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"INSERT INTO {DATABASE_NAME} ({ID_EMPLOYEE}, {REASON_FOR_DISMISSAL}, {DATE_OF_DISMASSAL}, {NUMBER_OF_THE_DISMISSAL_ORDER}, {DATE_OF_THE_DISMISSAL_ORDER})" +
                                $" VALUES('{parDismissEmployees.Id_Employee}', '{parDismissEmployees.Reason_For_Dismissal}', '{parDismissEmployees.Date_Of_Dismissal}', " +
                                $"'{parDismissEmployees.Number_Of_The_Dismissal_Order}', '{parDismissEmployees.Date_Of_The_Dismissal_Order}')";

          command.Prepare();
          command.ExecuteNonQuery();

          connect.Close();
        }
      }
      catch { }

      return false;
    }

    /// <summary>
    /// Получение данных о уволенных сотрудниках
    /// </summary>
    public static DismissEmployees GetDismissEmployees(int parId_Employee)
    {
      var Data = new DismissEmployees();

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
              var dismissEmployees = new DismissEmployees
              (
                  Convert.ToInt32(dataReader[ID_EMPLOYEE]),
                  Convert.ToString(dataReader[REASON_FOR_DISMISSAL]),
                  Convert.ToString(dataReader[DATE_OF_DISMASSAL]),
                  Convert.ToString(dataReader[NUMBER_OF_THE_DISMISSAL_ORDER]),
                  Convert.ToString(dataReader[DATE_OF_THE_DISMISSAL_ORDER])
              );

              Data = dismissEmployees;
            }

            connect.Close();
          }
        }
      }
      catch { }

      return Data;
    }

    //==================================================================



    //==================================================================
  }
}