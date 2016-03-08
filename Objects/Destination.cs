using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace UltimateRoadTripMachineNS.Objects
{
  public class Destination
  {
        private int _id;
        private string _name;
        private int _stop;

        private int _roadtrip_id;
        public Destination(string name, int stop, int roadtrip_id, int id = 0)
    {
        _id = id;
        _name = name;
        _stop = stop;
        _roadtrip_id = roadtrip_id;
    }

    public override bool Equals(System.Object otherDestination)
    {
        if (!(otherDestination is Destination))
        {
            return false;
        }
        else{
            Destination newDestination = (Destination) otherDestination;
            bool IdEquality = this.GetId() == newDestination.GetId();
            bool NameEquality = this.GetName() == newDestination.GetName();
            bool StopEquality = this.GetStop() == newDestination.GetStop();
            bool RoadTrip_IdEquality = this.GetRoadTripId() == newDestination.GetRoadTripId();

            return (IdEquality && NameEquality && StopEquality && RoadTrip_IdEquality);
          }
    }

    public int GetId()
    {
        return _id;
    }
    public string GetName()
    {
        return _name;
    }
    public void SetName(string name)
    {
        _name = name;
    }
    public int GetStop()
    {
        return _stop;
    }
    public void SetStop(int stop)
    {
        _stop = stop;
    }
    public int GetRoadTripId()
    {
        return _roadtrip_id;
    }
    public void SetRoadTripId(int roadtrip_id)
    {
        _roadtrip_id = roadtrip_id;
    }

    public static List<Destination> GetAll()
    {
        List<Destination> allDestinations = new List<Destination>{};

        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM destination;", conn);
        rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
            int id = rdr.GetInt32(0);
            string name = rdr.GetString(1);
            int roadtrip_id = rdr.GetInt32(2);
            int stop = rdr.GetInt32(3);

            Destination newDestination = new Destination(name, stop, roadtrip_id, id);
            allDestinations.Add(newDestination);
        }

        if (rdr != null)
        {
            rdr.Close();
        }
        if (conn != null)
        {
            conn.Close();
        }

        return allDestinations;

    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO destination (name, stop, roadtrip_id) OUTPUT INSERTED.id VALUES (@DestinationName, @DestinationStop, @DestinationRoadTripId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@DestinationName";
      nameParameter.Value = this.GetName();

      SqlParameter stopParameter = new SqlParameter();
      stopParameter.ParameterName = "@DestinationStop";
      stopParameter.Value = this.GetStop();

      SqlParameter roadtrip_idParameter = new SqlParameter();
      roadtrip_idParameter.ParameterName = "@DestinationRoadTripId";
      roadtrip_idParameter.Value = this.GetRoadTripId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(stopParameter);
      cmd.Parameters.Add(roadtrip_idParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }
    }
      public static Destination Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM destination WHERE id = @DestinationId;", conn);
      SqlParameter DestinationIdParameter = new SqlParameter();
      DestinationIdParameter.ParameterName = "@DestinationId";
      DestinationIdParameter.Value = id;
      cmd.Parameters.Add(DestinationIdParameter);
      rdr = cmd.ExecuteReader();

      int foundDestinationId = 0;
      string foundDestinationName = null;
      int foundDestinationStop = 0;
      int foundDestinationRoadTripId = 0;

      while(rdr.Read())
      {
        foundDestinationId = rdr.GetInt32(0);
        foundDestinationName = rdr.GetString(1);
          foundDestinationRoadTripId = rdr.GetInt32(2);
        foundDestinationStop = rdr.GetInt32(3);

      }
      Destination foundDestination = new Destination(foundDestinationName, foundDestinationStop, foundDestinationRoadTripId, foundDestinationId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundDestination;
    }

    public void Update()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE destination SET name = @NewName, stop = @NewStop, roadtrip_id = @NewRoadTripId OUTPUT INSERTED.name, INSERTED.stop, INSERTED.roadtrip_id WHERE id = @DestinationId;", conn);

      SqlParameter NewNameParameter = new SqlParameter();
      NewNameParameter.ParameterName= "@NewName";
      NewNameParameter.Value = this.GetName();
      cmd.Parameters.Add(NewNameParameter);

      SqlParameter DestinationIdParameter = new SqlParameter();
      DestinationIdParameter.ParameterName = "@DestinationId";
      DestinationIdParameter.Value = this.GetId();
      cmd.Parameters.Add(DestinationIdParameter);

      SqlParameter NewStopParameter = new SqlParameter();
      NewStopParameter.ParameterName = "@NewStop";
      NewStopParameter.Value = this.GetStop();
      cmd.Parameters.Add(NewStopParameter);


      SqlParameter NewRoadTripIdParameter = new SqlParameter();
      NewRoadTripIdParameter.ParameterName = "@NewRoadTripId";
      NewRoadTripIdParameter.Value = this.GetRoadTripId();
      cmd.Parameters.Add(NewRoadTripIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
       rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    // public void MoveUp()
    // {
    //   if(this.GetId() = 0)
    //   {
    //     return("You Are at the Starting Position of Your Trip");
    //   }
    //   else if
    //   {
    //     List<Destination> tripDestinations = Find(this.GetRoadTripId()).GetDestinations();
    //     update fruit a
    //      inner join fruit b on a.id <> b.id
    //        set a.color = b.color,
    //            a.name = b.name,
    //            a.calories = b.calories
    //      where a.id in (2,5) and b.id in (2,5)
    //   }
    // }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM destination WHERE id = @DestinationId", conn);

      SqlParameter DestinationIdParameter = new SqlParameter();
      DestinationIdParameter.ParameterName = "@DestinationId";
      DestinationIdParameter.Value = this.GetId();

      cmd.Parameters.Add(DestinationIdParameter);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM destination", conn);
      cmd.ExecuteNonQuery();
    }



  } // end class
} // end namespace