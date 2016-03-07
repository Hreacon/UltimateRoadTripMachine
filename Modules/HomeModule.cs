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
        string googleuri = "https://www.google.com/search?safe=medium&site=&tbm=isch&source=hp&biw=1533&bih=1148&q=";
        string binguri = "http://www.bing.com/images/search?q=";
        
        string html = Scrubber.GetPageContent(binguri + command);
        string start =  ";\"><img";
        string end = "class=\"tit\"";
        List<string> source = Scrubber.GetList(html, start, end);
        
        for(int i = 0; i < source.Count; i++)
        {
           source[i] = source[i].Substring(source[i].IndexOf("href=")+6);
           source[i] = source[i].Substring(0, source[i].Length -2);
        }
        
        List<string> images = new List<string>(){};
        foreach(string page in source)
        {
          try {
          Console.WriteLine("Scrubbing Page: " + page);
          string code = Scrubber.GetPageContent(page);
          Console.WriteLine("Getting Source Images");
          List<string> sourceImgs = Scrubber.GetList(code, "<img", ">");
          foreach(string sourceImg in sourceImgs)
          {
            // filter out the links that reference something on the page and instead focus on the links that link directly to the image, but this has to be checked at the img src level
            //  sourceImg.Substring(0,2) == "ht" || sourceImg.Substring(0,2) == "//"
            
            if(Scrubber.CheckLink(sourceImg, command))
            {
              Console.WriteLine("Adding " + sourceImg + " To Images");
              string src = sourceImg.Substring(sourceImg.IndexOf("src=")+5);
              try {
                src = src.Substring(0, src.IndexOf('"'));
              } catch(Exception e) {}
              if(src.Substring(0,2) == "ht" || sourceImg.Substring(0,2) == "//")
                images.Add(src);
            }
          }
            
          } catch (WebException ex)
          {
            if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
            {
              var resp = (HttpWebResponse) ex.Response;
              if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
              {
                  // Do something
              }
              else
              {
                  // Do something else
              }
            }
            else
            {
                // Do something else
            }
          }
        }
        
        Dictionary<string, object> model = new Dictionary<string, object>(){};
        model.Add("list", images);
        model.Add("html", html);
        return View["list.cshtml", model];
      };
    }
  }
}
