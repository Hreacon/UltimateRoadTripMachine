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
      Get["/test/"] = _ => {
        
        return View["test.cshtml"];
      };
      Post["/map"] = _ => {
        string command = Request.Form["start"];
        Console.WriteLine("Retrieving Map Locations: "+ command);
        return View["map.cshtml", Scrubber.GetMapOnLocation(command)];
      };
      Post["/mapDirections"] = _ => {
        string start = Request.Form["start"];
        string end = Request.Form["end"];
        Console.WriteLine("Retrieving Map Directions: "+ start + " " + end);
        return View["map.cshtml", Scrubber.GetMapDirections(start, end)];
      };
      Post["/iframe"] = _ => {
        string command = Request.Form["command"];
        Console.WriteLine("iframe route, command string: " + command);
        Uri model = new Uri(command);
        return View["map.cshtml", model.AbsoluteUri];
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
