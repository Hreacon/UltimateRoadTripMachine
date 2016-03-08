using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;

namespace UltimateRoadTripMachineNS.Objects
{
  public class Scrubber
  {
    public Scrubber()
    {
    }
    
    public static string GetPageContent(string url)
    {
      string output = String.Empty;
      WebRequest req = WebRequest.Create(new Uri(url).AbsoluteUri);
      WebResponse response = req.GetResponse();
      Stream data = response.GetResponseStream();
      using(StreamReader sr = new StreamReader(data))
      {
        output = sr.ReadToEnd();
      }
      return output;
    }
    
    public static List<string> GetList(string html, string start, string end)
    {
      List<string> output = new List<string>(){};
      bool done = false;
      while(!done && html.Length>100) 
      {
        int position = html.IndexOf(start, 0);
        int endposition = 0;
        if(position > 0)
          endposition = html.IndexOf(end, position);
        // Console.WriteLine("Position: " + position);
        // Console.WriteLine("EndPosition: " + endposition);
        
        if( position > 0 && endposition > 0)
        {
          output.Add(html.Substring(position, endposition - position));
          html = html.Substring(endposition+10);
        } else {
          done = true;
        }
      }
      return output;
    }
    
    public static bool CheckLink(string link, string command)
    {
      string[] commandsArray = command.Split(' ');
      List<string> commands = new List<string>(commandsArray.Length);
      commands.AddRange(commandsArray);
      int output = 0;
      foreach(string c in commands)
      {
        if(link.Contains(c))
          output += 1;
      }
      if(link.Contains("thumb"))
        output -= 10;
      return output > 0;
    }
    
    public static List<string> Scrub(string command)
    {
        string binguri = "http://www.bing.com/images/search?q=";
        Console.WriteLine("Searching for URLs...");
        string html = Scrubber.GetPageContent(binguri + command);
        string start =  ";\"><img";
        string end = "class=\"tit\"";
        List<string> source = Scrubber.GetList(html, start, end);
        
        for(int i = 0; i < source.Count; i++)
        {
           source[i] = source[i].Substring(source[i].IndexOf("href=")+6);
           source[i] = source[i].Substring(0, source[i].Length -2);
           Console.WriteLine("URL Found: " + source[i]);
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
              Console.WriteLine("Source of scrub " + sourceImg);
              int position = sourceImg.IndexOf("http");
              if(position == -1)
                position = 0;
              if(position < 0)
                position = sourceImg.IndexOf("src=")+5;
              string src = sourceImg.Substring(position);
              Console.WriteLine("src: " + src);
              try {
                src = src.Substring(0, src.IndexOf('"'));
              } catch(Exception e) {}
              if(src.Substring(0,2) == "ht" || src.Substring(0,2) == "//")
                images.Add(src);
              Console.WriteLine("Adding Image: " + src);
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
        return images;
    } // end func scrub
    
    // Dealing with the map
    public static string GetMapOnLocation(string location)
    {
      string placeUri = "place?q=" + location;
      return GetMap(placeUri);
    }
    private static string GetMap(string command)
    {
      string baseUri = "https://www.google.com/maps/embed/v1/";
      string zoomLevel = "";//"&zoom=17";
      string apiKey = "&key=AIzaSyCw5z-eino8TADQRsp4NX0pxg4C6ZnMKSA";
      Console.WriteLine("map route, command string: " + command);
      Uri model = new Uri(baseUri + command + zoomLevel + apiKey);
      return model.AbsoluteUri;
    }
    public static string GetMapDirections(string start, string end)
    {
      start = "origin="+start;
      end = "&destination="+end;
      return GetMap("directions?"+start+end);
    }
  } // end class
} // end namespace
