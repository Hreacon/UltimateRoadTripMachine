using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Net;
using System.IO;

namespace UltimateRoadTripMachineNS.Objects
{
  public class Scrubber
  {
    public static List<string> Search(string term, int limit = 6)
    {
      Console.WriteLine("Starting Search");
      List<string> links = new List<string>(){};
      List<string> urls = new List<string>(){};
      List<string> additionalUrls = new List<string>(){};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT TOP " +limit+" images.link, search_terms.id FROM images JOIN search_terms ON (images.search_terms_id = search_terms.id) WHERE search_terms.term = @Term", conn);
      SqlParameter TermParameter = new SqlParameter();
      TermParameter.ParameterName = "@Term";
      TermParameter.Value = term;
      cmd.Parameters.Add(TermParameter);


      rdr = cmd.ExecuteReader();
      Console.WriteLine("Reader Reading");


        if(!(rdr.HasRows))
        {
          Console.WriteLine("reverting to scrub");
          urls = Scrub(term, limit);
          int termId = AddSearch(term);
          Console.WriteLine(termId);
          foreach(string link in urls)
          {
            Console.WriteLine(link);
            AddImageLink(link, termId);
          }
        }
        else
        {
          int termId = 0;
          while(rdr.Read())
          {
            termId = rdr.GetInt32(1);
            Console.WriteLine("We made it here! false");
            string link = rdr.GetString(0);
            Console.WriteLine("adding image link: "+link);
            urls.Add(link);
          }
          if(urls.Count < limit)
          {
            List<string> addImages = Scrub(term, limit - urls.Count);
            foreach( string img in addImages)
            {
              AddImageLink(img, termId);
            }
            urls.AddRange(addImages);
          }
        }
      Console.WriteLine("We made it here! true");
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return urls;
    }
    

    public static int AddSearch(string term)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO search_terms (term) OUTPUT INSERTED.id VALUES (@Term);", conn);
      SqlParameter TermParameter = new SqlParameter();
      TermParameter.ParameterName = "@Term";
      TermParameter.Value = term;
      int termId = 0;

      cmd.Parameters.Add(TermParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        termId = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return termId;
    }

    public static void AddImageLink(string link, int termId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO images (link, search_terms_id) VALUES (@Link, @TermId);", conn);
      SqlParameter LinkParameter = new SqlParameter();
      LinkParameter.ParameterName = "@Link";
      LinkParameter.Value = link;

      SqlParameter TermIdParameter = new SqlParameter();
      TermIdParameter.ParameterName = "@TermId";
      TermIdParameter.Value = termId;

      cmd.Parameters.Add(LinkParameter);
      cmd.Parameters.Add(TermIdParameter);

      rdr = cmd.ExecuteReader();

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static string GetPageContent(string url)
    {
      string output = String.Empty;
      WebRequest req = WebRequest.Create(new Uri(url).AbsoluteUri);
      req.Timeout = 2000;
      try{
        WebResponse response = req.GetResponse();
        Stream data = response.GetResponseStream();
        using(StreamReader sr = new StreamReader(data))
        {
          output = sr.ReadToEnd();
        }
      } catch(WebException e) {
        Console.WriteLine("Timeout. URL: " + url);
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
        if(link.ToLower().Contains(c.ToLower()) && c.Length > 3)
          output += 1;
      }
      if(link.Contains("thumb"))
        output -= 10;
      return output > 0;
    }
    private static bool CheckForImage(string url)
    {
      int minsize = 20000; // Minimum image size in bits
      int maxsize = 1200000;// max image size in bits
      bool output = false;
      WebRequest req = WebRequest.Create(new Uri(url).AbsoluteUri);
      req.Timeout = 1000;
      req.Method="HEAD";
      try{
        WebResponse response = req.GetResponse();
        output = response.ContentLength > minsize && response.ContentLength < maxsize && response.ContentType.StartsWith("image/");
      } catch(WebException e) {
        Console.WriteLine("Timeout Checking Image. URL: " + url);
      }
      return output;
    }

    public static List<string> Scrub(string command, int limit = 30)
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

              if(Scrubber.CheckLink(sourceImg, command)) // check to see if the image tag has a word from the original command in it, attempting to circumvent getting useless photos
              {
                Console.WriteLine("Source of scrub " + sourceImg);
                int position = sourceImg.IndexOf("http"); // find the actual link to the image
                char endQuote = '"';
                if(position < 0)
                  position = sourceImg.IndexOf("src=")+5; // not found with http? try src
                endQuote = sourceImg[position-1];
                string src = sourceImg.Substring(position); // get the link to the actual image
                Console.WriteLine("src: " + src);
                try { // try to catch 404 exceptions... etc
                  src = src.Substring(0, src.IndexOf(endQuote)); // cut off the rest of the string after the link ends
                } catch(Exception e) {} // empty catch - ignore errors!
                if((src.Substring(0,2) == "ht" || src.Substring(0,2) == "//") && CheckForImage(src)){ // make sure the link starts with http or // so its a full path link.
                  images.Add(src);
                  Console.WriteLine("Adding Image: " + src);
                }
                Console.WriteLine("Image COunt: " + images.Count);
                if(images.Count >= limit)
                  return images;
              }
            }
          } catch (WebException ex){}
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
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM search_terms; DELETE FROM images;", conn);
      cmd.ExecuteNonQuery();
    }
  } // end class
} // end namespace
