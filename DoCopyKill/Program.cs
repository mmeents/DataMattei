using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace DoCopyKill {

  class Program {

    static void Main(string[] args) {

      string sPickupPath = ConfigurationManager.AppSettings["PickupPath"];
      string sPickupMask = ConfigurationManager.AppSettings["PickupMask"];
      string sDropOffPath = ConfigurationManager.AppSettings["DropOffPath"];
      string sCopyTo = "";

      List<string> lFiles = new List<string>();

      AddFiles( sPickupPath, sPickupMask, lFiles);

      foreach(string sFile in lFiles){ 
        if(File.Exists(sFile)) {
          try{
            sCopyTo = ""; 
            DateTime aFT = File.GetLastWriteTime(sFile);
          
            Int32 iMonth = aFT.Month;
            string sQuarter = ( ((iMonth == 1)||(iMonth == 2)||(iMonth == 3))? "Q1" : 
                                ( ((iMonth == 4)||(iMonth == 5)||(iMonth == 6))? "Q2" : 
                                ( ((iMonth == 7)||(iMonth == 8)||(iMonth == 9))? "Q3" : 
                                ( ((iMonth == 10)||(iMonth == 11)||(iMonth == 12))? "Q4": ""  ))));          
            string sYear = aFT.Year.ToString();          
            string sDestQu = sDropOffPath+"\\"+sYear+sQuarter;

            if (!Directory.Exists(sDestQu)){ 
              Directory.CreateDirectory(sDestQu);
            }

            sCopyTo = sDestQu +"\\"+ Path.GetFileName(sFile);          

            //if (File.Exists(sCopyTo)){ 
             // File.Delete(sCopyTo);
            //}

            if (!File.Exists(sCopyTo)){ 
              File.Copy(sFile , sCopyTo );
            }
            
          } catch (Exception ee) {
            appUtils.LogException("DoCopy: From:\""+sFile+"\" To:"+sCopyTo ,ee);        
          }
        }
      }


       foreach(string sFile in lFiles){ 
      //  File.Copy(sFile, sDropOffPath + "\\")
        if(File.Exists(sFile)) {
          try {
            DateTime aFT = File.GetLastWriteTime(sFile);
          
            Int32 iMonth = aFT.Month;
            string sQuarter = ( ((iMonth == 1)||(iMonth == 2)||(iMonth == 3))? "Q1" : 
                                ( ((iMonth == 4)||(iMonth == 5)||(iMonth == 6))? "Q2" : 
                                ( ((iMonth == 7)||(iMonth == 8)||(iMonth == 9))? "Q3" : 
                                ( ((iMonth == 10)||(iMonth == 11)||(iMonth == 12))? "Q4": ""  ))));          
            string sYear = aFT.Year.ToString();          
            string sDestQu = sDropOffPath+"\\"+sYear+sQuarter+"\\";

            if (!Directory.Exists(sDestQu)){ 
              Directory.CreateDirectory(sDestQu);
            }
            sCopyTo = sDestQu +"\\"+ Path.GetFileName(sFile);                 

            if(File.Exists(sCopyTo)) {
              File.Delete(sFile);
            }         
          
          } catch (Exception ee) {
            appUtils.LogException("DoDelete: CheckFileMadeIt:"+sCopyTo+"+ OriginalToDelete:"+sFile,ee);        
          }
        }
      }
           

      
    }

    static public void AddFiles( string sFilePath, string sMask, List<string> lFiles ){ 

      string[] dirs = Directory.GetDirectories(sFilePath);
      if (dirs.Length > 0) {
        foreach (string subDir in dirs) {
          AddFiles(subDir+"\\", sMask, lFiles);
        }
      }

      string[] filelist = Directory.GetFiles(sFilePath, sMask);
      try {
        if (filelist.Length > 0) {
          foreach (string sFN in filelist) {
            lFiles.Add(sFN);
          }
        }
      } catch (Exception ee) {
        appUtils.LogException("SpanDir:", ee);        
      }

    }  
  }  

  public static class appUtils {
    public static void LogException(String sExceptionTag, Exception e) {
      try {
        String sMessage = e.Message;
        String sSource = e.Source;
        String sStack = e.StackTrace;
        ("*** Error(" + sExceptionTag + "): " + sMessage + "; Source: " + sSource + "; Stack: " + sStack).toLog("PoloLog");
        if (e.InnerException != null) {
          LogException(sExceptionTag + "A", e.InnerException);
        }
      } catch { }
    }

    static public string UserLogLocation() {

      String sUserDataDir = ConfigurationManager.AppSettings["ProcessPath"];
      if (!Directory.Exists(sUserDataDir)) {
        Directory.CreateDirectory(sUserDataDir);
      }
      return sUserDataDir;
    }

    static public string LogFileName(string sLogName) {
      return UserLogLocation() + sLogName + ".txt";
    }

    static public string toLog(this string sMsg, string sLogName) {
      using (StreamWriter w = File.AppendText(LogFileName(sLogName))) { w.WriteLine(DateTime.Now.ToString() + ":" + sMsg); }
      return sMsg;
    }

  }

}
