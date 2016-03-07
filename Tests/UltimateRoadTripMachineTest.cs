using Xunit;
using UltimateRoadTripMachineNS.Objects;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace UltimateRoadTripMachineNS
{
  public class RoadTripTest : IDisposable
  {
     public RoadTripTest()
     {
       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=road_trip_test;Integrated Security=SSPI;";
     }
     
     [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = RoadTrip.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {

      RoadTrip firstRoadTrip = new RoadTrip("awesome adventure", "awesome adventure to somewhere");
      RoadTrip secondRoadTrip = new RoadTrip("awesome adventure", "awesome adventure to somewhere");

      Assert.Equal(firstRoadTrip, secondRoadTrip);
    }
     
     public void Dispose()
     {
    //    RoadTrip.DeleteAll();
     }
  }
}
