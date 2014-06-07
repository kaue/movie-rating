using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.IO;
using System.Text;
public class Core
{
    public Core()
    {

    }
    #region Computer
    /// <summary>
    /// Return total CPU usage in %
    /// </summary>
    /// <param name="lDelay">Delay in seconds for more precision</param>
    /// <returns></returns>
    public static float GetCPUTotalUse(int lDelay = 1)
    {
        PerformanceCounter processorTotal =
            new PerformanceCounter("Processor", "% Processor Time", "_Total");
        processorTotal.NextValue();
        Thread.Sleep(lDelay * 1000);
        return processorTotal.NextValue();
    }
    /// <summary>
    /// Return total avaliable memory in MBs
    /// </summary>
    /// <returns></returns>
    public static long GetAvaliableMemory()
    {
        PerformanceCounter memoryAvaliable =
            new PerformanceCounter("Memory", "Available Bytes");
        long availableMemory = Convert.ToInt64(memoryAvaliable.NextValue()) / (1024 * 1024);
        return availableMemory;
    }
    /// <summary>
    /// Return Download Speed in MBs
    /// </summary>
    /// <param name="lInstance">Network Interface ID</param>
    /// <param name="lDelay">Delay in seconds for more precision</param>
    /// <returns></returns>
    public static float GetDownloadSpeed(int lInstance = 1, int lDelay = 1)
    {
        PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
        string networkInstance = performanceCounterCategory.GetInstanceNames()[lInstance];

        PerformanceCounter performanceCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", networkInstance);

        performanceCounterReceived.NextValue();
        Thread.Sleep(lDelay * 1000);
        float downloadSpeed = performanceCounterReceived.NextValue() / (1024f * 1024f);
        return downloadSpeed;
    }
    /// <summary>
    /// Return Upload Speed in MBs
    /// </summary>
    /// <param name="lInstance">Network Interface ID</param>
    /// <param name="lDelay">Delay in seconds for more precision</param>
    /// <returns></returns>
    public static float GetUploadSpeed(int lInstance = 1, int lDelay = 1)
    {
        PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
        string networkInstance = performanceCounterCategory.GetInstanceNames()[lInstance];

        PerformanceCounter performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", networkInstance);

        performanceCounterSent.NextValue();
        Thread.Sleep(lDelay * 1000);
        float uploadSpeed = performanceCounterSent.NextValue() / (1024f * 1024f);
        return uploadSpeed;
    }
    /// <summary>
    /// Return Public IP
    /// </summary>
    /// <returns></returns>
    public static string GetPublicIP()
    {
        string url = "http://checkip.dyndns.org";
        System.Net.WebRequest req = System.Net.WebRequest.Create(url);
        System.Net.WebResponse resp = req.GetResponse();
        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
        string response = sr.ReadToEnd().Trim();
        string[] a = response.Split(':');
        string a2 = a[1].Substring(1);
        string[] a3 = a2.Split('<');
        string a4 = a3[0];
        return a4;
    }
    #endregion

    #region Program
    /// <summary>
    /// Returns Application directory
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentDirectory()
    {
        return Directory.GetCurrentDirectory();
    }
    /// <summary>
    /// Returns the total runtime
    /// </summary>
    /// <returns></returns>
    public static TimeSpan GetTotalRuntime()
    {
        return DateTime.Now - Process.GetCurrentProcess().StartTime;
    }
    /// <summary>
    /// Returns the file version
    /// </summary>
    /// <returns></returns>
    public static string GetVersion()
    {
        return Process.GetCurrentProcess().MainModule.FileVersionInfo.FileVersion;
    }
    #endregion

    #region File
    /// <summary>
    /// Returns a list with each line of a file
    /// </summary>
    /// <param name="lFilePath"></param>
    /// <param name="lEncoding"></param>
    /// <returns></returns>
    public static List<string> FileToList(string lFilePath, string lEncoding = "iso-8859-1")
    {
        IEnumerable<string> lines = File.ReadLines(lFilePath, Encoding.GetEncoding(lEncoding));
        return new List<string>(lines);
    }
    #endregion

    #region String
    /// <summary>
    /// Return a substring delimited by the lStart/lEnd parameters.
    /// </summary>
    /// <param name="lString"></param>
    /// <param name="lStart"></param>
    /// <param name="lEnd"></param>
    /// <param name="lStartIndex"></param>
    /// <param name="lEndIndex"></param>
    /// <returns></returns>
    public static string Split(string lString, string lStart, string lEnd = "", int lStartIndex = 1, int lEndIndex = 0)
    {
        try
        {
            string returnStr = lString.Split(new string[] { lStart }, StringSplitOptions.None)[lStartIndex];
            if (!string.IsNullOrEmpty(lEnd))
                returnStr = returnStr.Split(new string[] { lEnd }, StringSplitOptions.None)[lEndIndex];
            return returnStr;
        }
        catch
        {
            return null;
        }
    }
    #endregion

    #region Network
    /// <summary>
    /// Send a HTTP Post Request
    /// </summary>
    /// <param name="lUrl"></param>
    /// <param name="lPostData"></param>
    /// <returns></returns>
    public static string HttpPost(string lUrl, string lPostData)
    {
        System.Net.WebRequest webRequest = System.Net.WebRequest.Create(lUrl);
        webRequest.ContentType = "application/x-www-form-urlencoded";
        webRequest.Method = "POST";
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(lPostData);
        webRequest.ContentLength = bytes.Length;
        System.IO.Stream os = webRequest.GetRequestStream();
        os.Write(bytes, 0, bytes.Length);
        os.Close();
        System.Net.WebResponse webResponse = webRequest.GetResponse();
        if (webResponse == null) return null;
        System.IO.StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
        string strResponse = streamReader.ReadToEnd().Trim();
        return strResponse;
    }
    /// <summary>
    /// Send a HTTP Get Request
    /// </summary>
    /// <param name="lUrl"></param>
    /// <returns></returns>
    public static string HttpGet(string lUrl)
    {
        System.Net.WebRequest req = System.Net.WebRequest.Create(lUrl);
        System.Net.WebResponse resp = req.GetResponse();
        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
        return sr.ReadToEnd().Trim();
    }
    /// <summary>
    /// Send a HTTP Get Request
    /// </summary>
    /// <param name="lUrl"></param>
    /// <param name="lReferer"></param>
    /// <returns></returns>
    public static string HttpGet(string lUrl, string lReferer)
    {
        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(lUrl);
        myHttpWebRequest.Referer = lReferer;
        HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
        Stream streamResponse = myHttpWebResponse.GetResponseStream();
        StreamReader streamRead = new StreamReader(streamResponse);
        string strResponse = streamRead.ReadToEnd().Trim();
        streamRead.Close();
        streamResponse.Close();
        myHttpWebResponse.Close();
        return strResponse;
    }
    /// <summary>
    /// Returns the page HTML using UTF8 Encoding
    /// </summary>
    /// <param name="lUrl"></param>
    /// <returns></returns>
    public static string WebClientRequest(string lUrl)
    {
        WebClient wc = new WebClient();
        byte[] mybytes = wc.DownloadData(lUrl);
        string html = Encoding.UTF8.GetString(mybytes);
        return html;
    }
    #endregion
}
