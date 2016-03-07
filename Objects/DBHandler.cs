using UltimateRoadTripMachineNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace JensenNS.Objects
{
  public class DBHandler
  {
    protected static SqlConnection _conn;

    protected static List<Object> GetAll(string table, Func<SqlDataReader, Object> MakeObject)
    {
      List<Object> output = new List<Object>(){};
      string query = "SELECT * FROM "+table+"";
      SqlDataReader rdr = DBHandler.DatabaseOperation(query);
      while(rdr.Read())
      {
        output.Add(MakeObject(rdr));
      }
      DBHandler.DatabaseCleanup(rdr, _conn);
      return output;
    }
    protected List<Object> GetList(string table, string query, Func<SqlDataReader, Object> MakeObject, SqlParameter parameter = null)
    {
      return GetList(table, query, MakeObject, new List<SqlParameter> { parameter });
    }
    protected List<Object> GetList(string table, string query, Func<SqlDataReader, Object> MakeObject, List<SqlParameter> parameters = null)
    {
      query = "SELECT * FROM " + table + " " + query;
      SqlDataReader rdr = DBHandler.DatabaseOperation(query, parameters);
      List<Object> output = new List<Object>{};
      while(rdr.Read())
      {
        output.Add(MakeObject(rdr));
      }
      return output;
    }
    protected static Object GetObjectFromDB(string table, string query, Func<SqlDataReader, Object> MakeObject, SqlParameter parameter = null)
    {
      query = "SELECT * FROM " + table + " " + query;
      SqlDataReader rdr = DBHandler.DatabaseOperation(query, parameter);
      Object output = null;
      while(rdr.Read())
      {
        output = MakeObject(rdr);
      }
      DBHandler.DatabaseCleanup(rdr, _conn);
      return output;
    }

    protected int Save(string table, List<string> columns, List<SqlParameter> parameters, int id = 0)
    {
      string query = "";

      if(id == 0)
      {
        string col = "(";
        string val = "(";
        foreach(string item in columns)
        {
          col += item + ", ";
          val += "@"+item + ", ";
        }
        col = col.Substring(0, col.Length-2) + ")";
        val = val.Substring(0, val.Length-2) + ")";
        query = "INSERT INTO " + table + " " + col + " OUTPUT INSERTED.id VALUES " + val + ";";
      } else {
        string update = "";
        foreach(string item in columns)
        {
          update += item + "=@" + item + ", ";
        }
        update = update.Substring(0, update.Length-2);
        query = "UPDATE " + table + " SET " + update + " WHERE id = @id";
        parameters.Add(new SqlParameter("@id", id));
      }
      SqlDataReader rdr = DBHandler.DatabaseOperation(query, parameters);
      while(rdr.Read() && id == 0)
      {
        id = rdr.GetInt32(0);
      }
      DBHandler.DatabaseCleanup(rdr, _conn);
      return id;
    }
    protected static void DeleteAll(string table)
    {
      string query = "DELETE FROM " + table;
      SqlDataReader rdr = DBHandler.DatabaseOperation(query);
      DBHandler.DatabaseCleanup(rdr, _conn);
    }
    protected static void Delete(string table, int id)
    {
      string query = "DELETE FROM "+table+" WHERE id = @id";
      SqlDataReader rdr = DBHandler.DatabaseOperation(query, new SqlParameter("@id", id));
      DBHandler.DatabaseCleanup(rdr, _conn);
    }
    protected static SqlDataReader DatabaseOperation(string query, SqlParameter parameter)
    {
      return DBHandler.DatabaseOperation(query, new List<SqlParameter> { parameter });
    }
    protected static SqlDataReader DatabaseOperation(string query, List<SqlParameter> parameters = null)
    {
      _conn = DB.Connection();
      _conn.Open();
      // Console.WriteLine(query);
      SqlCommand cmd = new SqlCommand(query, _conn);
      if(parameters != null)
      {
        foreach(SqlParameter param in parameters)
        {
          cmd.Parameters.Add(param);
        }
      }

      SqlDataReader rdr = cmd.ExecuteReader();

      return rdr;
    }
    protected static void DatabaseCleanup(SqlDataReader rdr, SqlConnection conn)
    {
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
  }
}
