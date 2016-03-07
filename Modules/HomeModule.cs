using Nancy;
using UltimateRoadTripMachineNS.Objects;
using System.Collections.Generic;
using System;

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
        Console.WriteLine("map route, command string: " + command);
        Uri model = new Uri(command);
        return View["map.cshtml", model.AbsoluteUri];
      };
    }
  }
}
