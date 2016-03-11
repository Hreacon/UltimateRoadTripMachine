using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace UltimateRoadTripMachineNS.Objects
{
  public class RoadTrip
  {
    private int _id;
    private string _name;
    private string _description;
    public RoadTrip(string name, string description, int id = 0)
    {
      _id = id;
      _name = name;
      _description = description;
    }

    public override bool Equals(System.Object otherTrip)
    {
      if (!(otherTrip is RoadTrip))
      {
        return false;
      }
      else{
        RoadTrip newTrip = (RoadTrip) otherTrip;
        bool IdEquality = this.GetId() == newTrip.GetId();
        bool NameEquality = this.GetName() == newTrip.GetName();
        bool DescriptionEquality = this.GetDescription() == newTrip.GetDescription();

        return (IdEquality && NameEquality && DescriptionEquality);
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
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string description)
    {
      _description = description;
    }

    public static List<RoadTrip> GetAll()
    {
      List<RoadTrip> allTrips = new List<RoadTrip>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM roadtrips;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);

        RoadTrip newTrip = new RoadTrip(name, description, id);
        if(newTrip.GetDestinations().Count > 0)
          allTrips.Add(newTrip);
      }

      if (rdr != null)
      {
          rdr.Close();
      }
      if (conn != null)
      {
          conn.Close();
      }

      return allTrips;

    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO roadtrips (name, description) OUTPUT INSERTED.id VALUES (@RoadTripName, @RoadTripDescription);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@RoadTripName";
      nameParameter.Value = this.GetName();

      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@RoadTripDescription";
      descriptionParameter.Value = this.GetDescription();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(descriptionParameter);

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

      public static RoadTrip Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM roadtrips WHERE id = @RoadTripId;", conn);
      SqlParameter roadTripIdParameter = new SqlParameter();
      roadTripIdParameter.ParameterName = "@RoadTripId";
      roadTripIdParameter.Value = id;
      cmd.Parameters.Add(roadTripIdParameter);
      rdr = cmd.ExecuteReader();

      int foundRoadTripId = 0;
      string foundRoadTripName = null;
      string foundRoadTripDescription = null;

      while(rdr.Read())
      {
        foundRoadTripId = rdr.GetInt32(0);
        foundRoadTripName = rdr.GetString(1);
        foundRoadTripDescription = rdr.GetString(2);
      }
      RoadTrip foundRoadTrip = new RoadTrip(foundRoadTripName, foundRoadTripDescription, foundRoadTripId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundRoadTrip;
    }

    public void Update()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE roadtrips SET name = @NewName, description = @NewDescription OUTPUT INSERTED.name, INSERTED.description WHERE id = @RoadTripId;", conn);

      SqlParameter NewNameParameter = new SqlParameter();
      NewNameParameter.ParameterName= "@NewName";
      NewNameParameter.Value = this.GetName();
      cmd.Parameters.Add(NewNameParameter);

      SqlParameter RoadTripIdParameter = new SqlParameter();
      RoadTripIdParameter.ParameterName = "@RoadTripId";
      RoadTripIdParameter.Value = this.GetId();
      cmd.Parameters.Add(RoadTripIdParameter);

      SqlParameter NewDescriptionParameter = new SqlParameter();
      NewDescriptionParameter.ParameterName = "@NewDescription";
      NewDescriptionParameter.Value = this.GetDescription();
      cmd.Parameters.Add(NewDescriptionParameter);
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

    public List<Destination> GetDestinations()
    {
      Console.WriteLine("RoadTrip.GetDestinations(): " + GetId());
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM destinations WHERE roadtrip_id = @RoadTripId ORDER BY stop;", conn);
      SqlParameter roadtripIdParameter = new SqlParameter();
      roadtripIdParameter.ParameterName = "@RoadTripId";
      roadtripIdParameter.Value = this.GetId();
      cmd.Parameters.Add(roadtripIdParameter);
      rdr = cmd.ExecuteReader();

      List<Destination> destinations = new List<Destination> {};
      while(rdr.Read())
      {
        int DestinationId = rdr.GetInt32(0);
        string DestinationName = rdr.GetString(1);
        int DestinationRoadtrip_id = rdr.GetInt32(2);
        int DestinationStop = rdr.GetInt32(3);
        Console.WriteLine("Getting Destination: " + DestinationName);
        Destination newDestination = new Destination(DestinationName, DestinationRoadtrip_id, DestinationStop, DestinationId);
        destinations.Add(newDestination);
        Console.WriteLine("Destination Count" + destinations.Count);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return destinations;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM roadtrips WHERE id = @RoadTripId", conn);

      SqlParameter roadTripIdParameter = new SqlParameter();
      roadTripIdParameter.ParameterName = "@RoadTripId";
      roadTripIdParameter.Value = this.GetId();

      cmd.Parameters.Add(roadTripIdParameter);
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
      SqlCommand cmd = new SqlCommand("DELETE FROM roadtrips", conn);
      cmd.ExecuteNonQuery();
    }
  } // end class
} // end namespace
