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
    
    [Fact]
    public void Test_SaveRoadTripToDatabase()
    {
        RoadTrip newTrip = new RoadTrip("awesome adventure", "awesome adventure to somewhere");
        newTrip.Save();
        
         List<RoadTrip> allTrips = RoadTrip.GetAll();
         
         List<RoadTrip> testTrips = new List<RoadTrip>{newTrip};
         
        Assert.Equal(testTrips, allTrips);
        
    }
    
    [Fact]
    public void Test_Find_FindsRoadTripInDataBase()
    {
      //Arrange
      RoadTrip testRoadTrip = new RoadTrip("awsome adventure", "awesome adventure to somewhere");
      testRoadTrip.Save();

      //Act
      RoadTrip foundRoadTrip = RoadTrip.Find(testRoadTrip.GetId());

      //Assert
      Assert.Equal(testRoadTrip, foundRoadTrip);
    }
    
    [Fact]
    public void Test_UpdateRoadTripName()
    {
        RoadTrip newTrip = new RoadTrip("awesome adventure", "awesome adventure to somewhere");
        newTrip.Save();
        newTrip.SetName("Extreme");
        newTrip.Update();
        
        Assert.Equal(newTrip.GetName(), "Extreme");
    }
    
    [Fact]
    public void Test_DeleteRoadTrip()
    {
        RoadTrip firstTrip = new RoadTrip("awesome adventure", "awesome adventure to somewhere");
        firstTrip.Save();
        RoadTrip secondTrip = new RoadTrip("extreme adventure", "extreme adventure to somewhere");
        secondTrip.Save();
        
        secondTrip.Delete();
        List<RoadTrip> allTrips = RoadTrip.GetAll();
        List<RoadTrip> testTrips = new List<RoadTrip>{firstTrip};
        Assert.Equal(testTrips, allTrips);
    }
     
     public void Dispose()
     {
       RoadTrip.DeleteAll();
     }
  }
}
