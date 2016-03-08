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
        return View["index.cshtml"];
      };
      Post["/map"] = _ => {
        string command = Request.Form["start"];
        Console.WriteLine("Retrieving Map Location: "+ command);
        return View["map.cshtml", Scrubber.GetMapOnLocation(command)];
      };
      Post["/mapDirections"] = _ => {
        string start = Request.Form["start"];
        string end = Request.Form["destination"];
        Console.WriteLine("Retrieving Map Directions: '"+ start + "', '" + end +"'");
        return View["map.cshtml", Scrubber.GetMapDirections(start, end)];
      };
      Post["/iframe"] = _ => {
        string command = Request.Form["command"];
        Console.WriteLine("iframe route, command string: " + command);
        Uri model = new Uri(command);
        return View["map.cshtml", model.AbsoluteUri];
      };
      // Post["/start"] = _ => { // start of a road trip. Reset the form to include a road trip id. Then show a map
      //   // create new road trip based on start destination and some keywords
      //   string firstStop = Request.Form["command"];
      //   RoadTrip newTrip = new RoadTrip("The awesome " + firstStop + " Road Trip!", "");
      //   newTrip.Save();
      //   Destination newStop = new Destination(firstStop, 1, newTrip.GetId()); // TODO depreciated stop constructor
      //   newStop.Save();
      //   Dictionary<string,object> model = new Dictionary<string,object>(){};
      //   model.Add("map", Scrubber.GetMapOnLocation(newStop.GetName()));
      //   model.Add("roadTripId", newTrip.GetId());
      //   return View["stop.cshtml", model];
      // }; 
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
          model.Add("map", Scrubber.GetMapOnLocation(newStop.GetName())); // show the map with only one location
        } else { // there are already multiple stops in the trip
          model.Add("map", Scrubber.GetMapDirections(rtrip.GetDestinations()[rtrip.GetDestinations().Count-1].GetName(), newStop.GetName())); // show direciton map
        }
        model.Add("images", Scrubber.Scrub(newStop.GetName(), 6));
        model.Add("roadTripId", roadTripId);
        Console.WriteLine(model);
        return View["stop.cshtml", model];
      };
      Post["/getPage"] = _ => {
        Console.WriteLine("Get Page");
        string command = Request.Form["command"];
        List<string> images = Scrubber.Scrub(command);
        Dictionary<string, object> model = new Dictionary<string, object>(){};
        model.Add("list", images);
        string binguri = "http://www.bing.com/images/search?q="; // this is for testing, it is not needed for production
        model.Add("bing", Scrubber.GetPageContent(binguri + command));
        return View["list.cshtml", model];
      };
    }
  }
}