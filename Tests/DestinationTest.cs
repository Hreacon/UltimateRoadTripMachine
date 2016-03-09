using Xunit;
using UltimateRoadTripMachineNS.Objects;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace UltimateRoadTripMachineNS
{
  public class DestinationTest : IDisposable
{
   public DestinationTest()
   {
     DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=road_trip_test;Integrated Security=SSPI;";
   }

   [Fact]
  public void Test_DatabaseEmptyAtFirstDestination()
  {
    int result = Destination.GetAll().Count;

    Assert.Equal(0, result);
  }

  [Fact]
  public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSameDestination()
  {

    Destination firstDestination = new Destination("multnomah falls", 1);
    Destination secondDestination = new Destination("multnomah falls", 1);

    Assert.Equal(firstDestination, secondDestination);
  }

  [Fact]
  public void Test_SaveDestinationToDatabaseDestination()
  {
      Destination newDestination = new Destination("multnomah falls", 1, 1);
      newDestination.Save();

       List<Destination> allDestinations = Destination.GetAll();

       List<Destination> testDestinations= new List<Destination>{newDestination};

      Assert.Equal(testDestinations, allDestinations);

  }

  [Fact]
  public void Test_Find_FindsDestinationInDataBaseDestination()
  {
    //Arrange
    Destination testDestination = new Destination("multnomah falls", 1);
    testDestination.Save();

    //Act
    Destination foundDestination = Destination.Find(testDestination.GetId());

    //Assert
    Assert.Equal(testDestination, foundDestination);
  }

  // [Fact]
  // public void Test_CreateNewDestinationAndSetStop()
  // {
  //   Destination newDestination = new Destination("multnomah falls", 1)
  // }

  [Fact]
  public void Test_MoveUp_SwapsThisDestinationWithPrevious()
  {
  }

  [Fact]
  public void Test_SearchForLinksFromTerm()
  {
  List<string> urls = Scrubber.Search("poptarts");

  Assert.Equal(2, urls.Count);
  }


  [Fact]
  public void Test_UpdateDestinationNameDestination()
  {
      Destination newDestination = new Destination("multnomah falls", 1);
      newDestination.Save();
      newDestination.SetName("paradise falls");
      newDestination.Update();

      Assert.Equal(newDestination.GetName(), "paradise falls");
  }


  [Fact]
  public void Test_DeleteDestinationDestination()
  {
      Destination firstDestination = new Destination("multnomah falls", 1, 1);
      firstDestination.Save();
      Destination secondDestination = new Destination("paradise falls", 2, 1);
      secondDestination.Save();

      secondDestination.Delete();
      List<Destination> allDestinations = Destination.GetAll();
      List<Destination> testDestinations = new List<Destination>{firstDestination};
      Assert.Equal(testDestinations, allDestinations);
  }

   public void Dispose()
   {
     Destination.DeleteAll();
     RoadTrip.DeleteAll();
   }
 }
}
