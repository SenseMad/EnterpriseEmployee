using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace EnterpriseEmployee.Scripts.Database
{
  public static class DatabaseTablePositions
  {
    /// <summary>
    /// Название таблицы
    /// </summary>
    public const string DATABASE_NAME = "Positions";

    /// <summary>
    /// Поле ID
    /// </summary>
    public const string ID = "Id";

    /// <summary>
    /// Поле Название должности
    /// </summary>
    public const string JOB_TITLE = "Job_Title";

    /// <summary>
    /// Поле Оклад
    /// </summary>
    public const string SALARY = "Salary";

    //==================================================================

    /// <summary>
    /// Получение списка должностей
    /// </summary>
    public static List<Positions> GetPositions()
    {
      var retPositions = new List<Positions>();

      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"SELECT * FROM {DATABASE_NAME} ORDER BY {ID}, {JOB_TITLE}, {SALARY}";

          using (SQLiteDataReader dataReader = command.ExecuteReader())
          {
            if (dataReader.HasRows)
            {
              while (dataReader.Read())
              {
                var positions = new Positions
                (
                  Convert.ToInt32(dataReader[ID]),
                  Convert.ToString(dataReader[JOB_TITLE]),
                  Convert.ToString(dataReader[SALARY])

                );

                retPositions.Add(positions);
              }

              dataReader.Close();
            }

            connect.Close();
          }
        }
      }
      catch { }

      return retPositions;
    }

    /// <summary>
    /// Получение данных о должностях
    /// </summary>
    public static Positions GetPositions(int parId)
    {
      var Data = new Positions();

      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"SELECT * FROM {DATABASE_NAME} WHERE {ID} = {parId}";

          using (SQLiteDataReader dataReader = command.ExecuteReader())
          {
            while (dataReader.Read())
            {
              var positions = new Positions
              (
                  Convert.ToInt32(dataReader[ID]),
                  Convert.ToString(dataReader[JOB_TITLE]),
                  Convert.ToString(dataReader[SALARY])
              );

              Data = positions;
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



    //==================================================================
  }
}