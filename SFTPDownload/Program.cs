using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Tamir.SharpSsh;
using Tamir.SharpSsh.jsch;

namespace ZipDB {
  class Program {

    //  string sPickupPath = ConfigurationManager.AppSettings["PickupPath"];
    //  string sPickupMask = ConfigurationManager.AppSettings["PickupMask"];
    //  string sSFTPServer = ConfigurationManager.AppSettings["SFTPServer"];
    //  string sSFTPUser = ConfigurationManager.AppSettings["SFTPUser"];
    //  string sSFTPPass = ConfigurationManager.AppSettings["SFTPPass"];
    //  string sSFTPDir = ConfigurationManager.AppSettings["SFTPDir"];
    //  string[] filelist = Directory.GetFiles(sPickupPath, sPickupMask);

    static void Main(string[] args) {
      try { 
        string sSFTPServer = "SFTP.Pickit.COM";
        string sSFTPUser = "username";
        string sSFTPPass = "password";        
        string sDest1 = "c:\\DestNew\\";
        string sDest2 = "S:\\Dest\\";
        string sTmpSrc = "";
        string sTmpDest = "";

        List<string> sFiles = new List<string>();
        sFiles.Clear();
        Sftp ss = new Sftp(sSFTPServer, sSFTPUser, sSFTPPass);

        try {
          ss.Connect(22);
          System.Collections.ArrayList ar = ss.GetFileList("/Outbound/");
          foreach (string sItem in ar ){
            try { 
              sTmpSrc = "/Outbound/" + sItem;
              if ((sItem != ".")&& (sItem != "..")) {
                
                if (sItem.Contains("New.txt")) {
                  sTmpDest = sDest1 + sItem;                  
                } else {
                  sTmpDest = sDest2 + sItem;                  
                }

                if (File.Exists(sTmpDest)) {
                  sFiles.Add(sTmpSrc);
                  Console.WriteLine("skipping: " + sTmpSrc);
                  ("Skipped: " + sTmpSrc).toLog("DownLoadLog");
                } else  {
                  ss.Get(sTmpSrc, sTmpDest);                 
                  Console.WriteLine("downloaded: "+sTmpDest );
                  ("DownLoaded: " + sTmpDest).toLog("DownLoadLog");
                  if (File.Exists(sTmpDest)){
                    sFiles.Add(sTmpSrc);
                  }
                }

              }             
            } catch (Exception e) { 
              appUtils.LogException("GetFileError", e);
            }
          }
        
          Int32 iErrorCount = 0;
          var prop = ss.GetType().GetProperty("SftpChannel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
          var methodInfo = prop.GetGetMethod(true);
          var sftpChannel = methodInfo.Invoke(ss, null);           
          foreach (string sItem in sFiles) {
            try {            
              ((ChannelSftp)sftpChannel).rm(sItem);
              Console.WriteLine("Removing: "+sItem);
              ("Removed: "+sItem).toLog("RemoveLog");
            } catch (Exception e){
              appUtils.LogException("DeleteFileError", e);
              iErrorCount = iErrorCount + 1;
              if (iErrorCount > 3) { 
                throw new Exception("Too many errors.");
              }
            }
          }

      } finally {
        ss.Close();
      }
      Console.WriteLine("Done. Closing Connections Bye. " );
      System.Threading.Thread.Sleep(3000);

    } catch (Exception ee) {
        appUtils.LogException("OutBlockError", ee);
      }

    }   // end of main 
  }

  public static class appUtils
  {
    public static void LogException(String sExceptionTag, Exception e)
    {
      try
      {
        String sMessage = e.Message;
        String sSource = e.Source;
        String sStack = e.StackTrace;
        ("*** Error(" + sExceptionTag + "): " + sMessage + "; Source: " + sSource + "; Stack: " + sStack).toLog("PoloLog");
        if (e.InnerException != null)
        {
          LogException(sExceptionTag + "A", e.InnerException);
        }
      }
      catch { }
    }

    static public string UserLogLocation()
    {

      String sUserDataDir = ConfigurationManager.AppSettings["ProcessPath"];
      if (!Directory.Exists(sUserDataDir))
      {
        Directory.CreateDirectory(sUserDataDir);
      }
      return sUserDataDir;
    }

    static public string LogFileName(string sLogName)
    {
      return UserLogLocation() + sLogName + ".txt";
    }

    static public string toLog(this string sMsg, string sLogName)
    {
      using (StreamWriter w = File.AppendText(LogFileName(sLogName))) { w.WriteLine(DateTime.Now.ToString() + ":" + sMsg); }
      return sMsg;
    }

  }

}
