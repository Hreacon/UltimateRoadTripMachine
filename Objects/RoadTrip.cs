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
            
            return (NameEquality && DescriptionEquality && IdEquality);
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
        
        SqlCommand cmd = new SqlCommand("SELECT * FROM roadtrip;", conn);
        rdr = cmd.ExecuteReader();
        
        while(rdr.Read())
        {
            int id = rdr.GetInt32(0);
            string name = rdr.GetString(1);
            string description = rdr.GetString(2);
            
            RoadTrip newTrip = new RoadTrip(name, description, id);
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
    
  } // end class
} // end namespace
