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
  } // end class
} // end namespace
