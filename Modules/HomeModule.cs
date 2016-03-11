using Nancy;
using UltimateRoadTripMachineNS.Objects;
using System.Collections.Generic;
using System;
using System.Net;
using System.IO;

namespace UltimateRoadTripMachineNS
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Console.WriteLine("Return View");
        return View["index.cshtml"];
      };
      Get["/roadTrip/{id}"] = x => {
        Console.WriteLine("View Road Trip");
        return View["viewRoadTrip.cshtml", RoadTrip.Find(int.Parse(x.id))];
      };
      Get["/getStop/{id}"] = x => {
        Console.WriteLine("View Stop");
        return View["destination.cshtml", Destination.Find(int.Parse(x.id))];
      };
      Get["/getAllRoadTrips"] = _ => {
        return View["viewAllRoadTrips.cshtml", RoadTrip.GetAll()];
      };
      Get["/deleteDestination/{id}"] = parameters => {
        Console.WriteLine("Deleteing: " + parameters.id);
        Destination selectedDestination = Destination.Find(parameters.id);
        selectedDestination.Delete();
        return View["empty.cshtml"];
      };

      Get["/moveUp/{id}"] = parameters => {
        Console.WriteLine("Move Up: " + parameters.id);
        Destination selectedDestination = Destination.Find(parameters.id);
        selectedDestination.MoveUp();
        return View["empty.cshtml"];
      };

      Get["/moveDown/{id}"] = parameters => {
        Console.WriteLine("Move Down: " + parameters.id);
        Destination selectedDestination = Destination.Find(parameters.id);
        Console.WriteLine("Destination Found, RoadTripId: " + selectedDestination.GetRoadTripId());
        selectedDestination.MoveDown();
        return View["empty.cshtml"];
      };

      Post["/nameTrip"] = _ => {
        RoadTrip newTrip = RoadTrip.Find(int.Parse(Request.Form["id"]));
        newTrip.SetName(Request.Form["name"]);
        newTrip.Update();
        return View["empty.cshtml"];
      };

      // AJAX ROUTE ONLY RETURNS A PARTIAL HTML VIEW

      Post["/addStop"] = _ => {
        Dictionary<string,object> model = new Dictionary<string,object>(){}; // instantiate model
        int roadTripId = 0;
        try {
          roadTripId = int.Parse(Request.Form["roadTripId"]); // get roadtrip id
        } catch (Exception e) {}
        string destinationName = Request.Form["command"]; // get new destination name
        RoadTrip rtrip;
        if(roadTripId == 0) // there is no road trip yet
        {
          rtrip = new RoadTrip("The awesome " + destinationName + " Road Trip!", ""); // so make a road trip
          rtrip.Save(); // save road trip to db
          roadTripId = rtrip.GetId();
        } else rtrip = RoadTrip.Find(roadTripId);
        Destination newStop = new Destination(destinationName, roadTripId); // make a new destination
        newStop.Save(); // save the new stop to the database
        if(newStop.GetStop() == 1) // if theres only one stop in the road trip so far
        {
          Console.WriteLine("First Stop");
          model.Add("map", Scrubber.GetMapOnLocation(newStop.GetName())); // show the map with only one location
        } else { // there are already multiple stops in the trip
          Console.WriteLine("Not First Stop");
          model.Add("map", Scrubber.GetMapDirections(rtrip.GetDestinations()[rtrip.GetDestinations().Count-2].GetName(), newStop.GetName())); // show direciton map
        }
        model.Add("images", Scrubber.Search(newStop.GetName(), 6));
        model.Add("roadTripId", roadTripId);
        model.Add("destination", newStop);
        Console.WriteLine(model);
        Console.WriteLine("Return View");
        return View["stop.cshtml", model];
      };
    }
  }
}
