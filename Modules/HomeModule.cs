using Nancy;
using UltimateRoadTripMachineNS.Objects;
using System.Collections.Generic;
namespace UltimateRoadTripMachineNS
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
    }
  }
}
