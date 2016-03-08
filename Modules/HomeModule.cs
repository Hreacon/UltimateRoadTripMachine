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
      Post["/start"] = _ => { // start of a road trip. Reset the form to include a road trip id. Then show a map
        // create new road trip based on start destination and some keywords
        string firstStop = Request.Form["command"];
        RoadTrip newTrip = new RoadTrip("The awesome " + firstStop + " Road Trip!", "");
        newTrip.Save();
        Destination newStop = new Destination(firstStop, 1, newTrip.GetId()); // TODO depreciated stop constructor
        newStop.Save();
        Dictionary<string,object> model = new Dictionary<string,object>(){};
        model.Add("map", Scrubber.GetMapOnLocation(newStop.GetName()));
        model.Add("roadTripId", newTrip.GetId());
        return View["stop.cshtml", model];
      }; 
      Post["/addStop"] = _ => {
        Dictionary<string,object> model = new Dictionary<string,object>(){};
        // get roadtrip id
        int roadTripId = int.Parse(Request.Form["roadtripid"]);
        string destinationName = Request.Form["command"];
        List<Destination> destinations = RoadTrip.Find(roadTripId).GetDestinations();
        Destination newStop = new Destination(destinationName, destinations.Count+1, roadTripId);
        // get the previous destination
        model.Add("map", Scrubber.GetMapDirections(destinations[destinations.Count-1].GetName(), newStop.GetName()));
        model.Add("images", Scrubber.Scrub(newStop.GetName()));
        model.Add("roadTripId", roadTripId);        
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
