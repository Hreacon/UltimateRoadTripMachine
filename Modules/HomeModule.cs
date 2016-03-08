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
        string command = Request.Form["command"];
        string baseUri = "https://www.google.com/maps/embed/v1/";
        string placeUri = "place?q=" + command;
        string zoomLevel = "&zoom=17";
        string apiKey = "&key=AIzaSyCw5z-eino8TADQRsp4NX0pxg4C6ZnMKSA";
        Console.WriteLine("map route, command string: " + command);
        Uri model = new Uri(baseUri + placeUri + zoomLevel + apiKey);
        return View["map.cshtml", model.AbsoluteUri];
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
        return View["list.cshtml", model];
      };
    }
  }
}
