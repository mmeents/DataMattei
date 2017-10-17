using System;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.Web;
using System.Web.Caching;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace C0DEC0RE {

  public class StProcParam {
    DbType fParamType;
    string fVarName;
    object fVarValue;
    public StProcParam(string aVarName, DbType aParamType, object aVarValue) {
      fParamType = aParamType;
      fVarName = aVarName;
      fVarValue = aVarValue;
    }
    public string VarName { get { return fVarName; } set { fVarName = value; } }
    public DbType ParamType { get { return fParamType; } set { fParamType = value; } }
    public object VarValue { get { return fVarValue; } set { fVarValue = value; } }
  }
  
  public class MMData{
    public MMData(){}
    public DataSet GetDataSet(string queryString) {
      Database db = DatabaseFactory.CreateDatabase();            
      DbCommand dbCommand = db.GetSqlStringCommand(queryString);      
      return db.ExecuteDataSet(dbCommand);
    }
    public DataSet GetDataSet(string connectionName, string queryString) {      
      Database db = DatabaseFactory.CreateDatabase(connectionName);
      DbCommand dbCommand = db.GetSqlStringCommand(queryString);
      return db.ExecuteDataSet(dbCommand);
    }
    public void ExecuteSQLStr(string queryString) {
      Database db = DatabaseFactory.CreateDatabase();
      DbCommand dbCommand = db.GetSqlStringCommand(queryString);
      DataSet t = db.ExecuteDataSet(dbCommand);
    }
    public void ExecuteSQLStr(string connectionName, string queryString) {
      Database db = DatabaseFactory.CreateDatabase(connectionName);
      DbCommand dbCommand = db.GetSqlStringCommand(queryString);
      DataSet t = db.ExecuteDataSet(dbCommand);
    }
    public void ExecuteStoredProc(string connectionName, string procedureCode, StProcParam[] paramsList) {
      Database db = DatabaseFactory.CreateDatabase(connectionName);
      DbCommand dbSelectCmd = db.GetSqlStringCommand(procedureCode);
      if (paramsList != null){
        foreach (StProcParam p in paramsList){
          db.AddInParameter(dbSelectCmd, p.VarName, p.ParamType, p.VarValue);
        }
      }
      db.ExecuteNonQuery(dbSelectCmd);
    }
    public DataSet GetStProcDataSet(string connectionName, string procedureCode, StProcParam[] paramsList) {      
      Database db = DatabaseFactory.CreateDatabase(connectionName);
      DbCommand dbSelectCmd = db.GetSqlStringCommand(procedureCode);
      if (paramsList != null) {  
        foreach (StProcParam p in paramsList) {
          db.AddInParameter(dbSelectCmd, p.VarName, p.ParamType, p.VarValue);
        }
      }
      return db.ExecuteDataSet(dbSelectCmd);
    }  
  }
  
  public class RCData{  
    private DbConnectionInfo CI;
    public RCData( DbConnectionInfo ConInfo ){
      CI = ConInfo;	
    }
    public DataSet GetDataSet(string QueryString) {
      Database db = new SqlDatabase(CI.ConnectionString);
      DbCommand dbCommand = db.GetSqlStringCommand(QueryString);      
      return db.ExecuteDataSet(dbCommand);
    }    
    public void ExecuteSQLStr(string QueryString) {
      Database db = new SqlDatabase(CI.ConnectionString);
      DbCommand dbCommand = db.GetSqlStringCommand(QueryString);
      db.ExecuteDataSet(dbCommand);
    }    
    public void ExecuteStoredProc(string ProcedureCode, StProcParam[] ParamsList) {
      Database db = new SqlDatabase(CI.ConnectionString);
      string sqlCommand = ProcedureCode;
      DbCommand dbSelectCmd = db.GetSqlStringCommand(ProcedureCode);
      foreach (StProcParam p in ParamsList) {
        db.AddInParameter(dbSelectCmd, p.VarName, p.ParamType, p.VarValue);
      }
      db.ExecuteNonQuery(dbSelectCmd);
    }
    public DataSet GetStProcDataSet(string ProcedureCode, StProcParam[] ParamsList) {
      Database db = new SqlDatabase(CI.ConnectionString);
      string sqlCommand = ProcedureCode;
      DbCommand dbSelectCmd = db.GetSqlStringCommand(ProcedureCode);
      foreach (StProcParam p in ParamsList) {
        db.AddInParameter(dbSelectCmd, p.VarName, p.ParamType, p.VarValue);
      }
      return db.ExecuteDataSet(dbSelectCmd);
    }  
  }

  public class MMStrUtl {
    public static int ParseCount(string content, string delimeters) {
      if (delimeters == null) throw new ArgumentNullException("delimeters");
      if (content == null) throw new ArgumentNullException("content");
      return content.Split(delimeters.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length;
    }
    public static string ParseString(string content, string delimeters, int take) {
      if (delimeters == null) throw new ArgumentNullException("delimeters");
      if (content == null) throw new ArgumentNullException("content");
      string[] split = content.Split(delimeters.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      return (take >= split.Length ? "" : split[take]);
    }
    public static string TranslateToHTMLEncoded(string str) {
      string s = str.Replace("\n", "<br>");
      return s;
    }
    public static string Base64EncodeText(string Text) {
      byte[] encBuff = System.Text.Encoding.UTF8.GetBytes(Text);
      return Convert.ToBase64String(encBuff);
    }
    public static string Base64DecodeText(string Text) {
      byte[] decbuff = Convert.FromBase64String(Text);
      string s = System.Text.Encoding.UTF8.GetString(decbuff);
      return s;
    }
    public static string ByteToHex(byte[] byteArray) {
      string outString = "";
      foreach (Byte b in byteArray)
        outString += b.ToString("X2");
      return outString;
    }
    public static byte[] HexToByte(string hexString) {
      byte[] returnBytes = new byte[hexString.Length / 2];
      for (int i = 0; i < returnBytes.Length; i++)
        returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
      return returnBytes;
    }
    public static string Encrypt(string value) {
      string key = Base64DecodeText("QURGNzJBMDUxMjkwMDlGRg==");
      string iv = Base64DecodeText("MTQxRUM4QTkxNDU2OTMzNw==");
      byte[] buffer = null;
      DESCryptoServiceProvider des = new DESCryptoServiceProvider();
      try {
        des.Key = HexToByte(key);
        des.IV = HexToByte(iv);
        MemoryStream ms = new MemoryStream();
        CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        StreamWriter sw = new StreamWriter(encStream);
        sw.WriteLine(value);
        sw.Close();
        encStream.Close();
        buffer = ms.ToArray();
        ms.Close();
      } finally {
        des.Clear();
      }
      return ByteToHex(buffer);
    }
    public static string Decrypt(string value) {
      string key = Base64DecodeText("QURGNzJBMDUxMjkwMDlGRg==");
      string iv = Base64DecodeText("MTQxRUM4QTkxNDU2OTMzNw==");
      string returnValue = "";
      DESCryptoServiceProvider des = new DESCryptoServiceProvider();
      try {
        des.Key = HexToByte(key);
        des.IV = HexToByte(iv);
        MemoryStream ms = new MemoryStream(HexToByte(value));
        CryptoStream encStream = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(encStream);
        returnValue = sr.ReadLine();
        sr.Close();
        encStream.Close();
        ms.Close();
      } finally {
        des.Clear();
      }
      return returnValue;
    }
  }

  public class SysVar {
    Cache fCache;
    Boolean fOverrideLocalCache;
    public SysVar(Cache LocalCacheAccess) {
      fCache = LocalCacheAccess;
      fOverrideLocalCache = false;
    }
    private void SetVarValue(string DatabaseName, string VarName, string VarValue) {
      try {
        MMData d = new MMData();
        d.ExecuteStoredProc(DatabaseName, "exec dbo.sp_SetDBVars @VarName, @VarValue",
          new StProcParam[] {
            new StProcParam("@VarName", DbType.String, VarName), 
            new StProcParam("@VarValue", DbType.String, VarValue)
          }
        );
        fCache[VarName] = VarValue;
      } catch { }
    }

    private string GetVarValue(string DatabaseName, string VarName) {
      string result = "";
      try {
        if ((fCache[VarName] == null) || (OverrideLocalCache)) {
          MMData d = new MMData();
          DataSet ds = d.GetStProcDataSet(DatabaseName, "SELECT dv_VarValue, dv_VarName FROM DBVars WHERE dv_VarName = @VarName ",
            new StProcParam[] {
              new StProcParam("@VarName", DbType.String, VarName)
            });

          if ((ds.Tables.Count > 0) & (ds.Tables[0].Rows.Count > 0)) {
              try { result = Convert.ToString(ds.Tables[0].Rows[0]["dv_VarValue"]); }  catch { result = ""; }
          }

          fCache[VarName] = result;
        } else {
          result = fCache[VarName].ToString();
        }
      } catch { }
      return result;
    }
    public string this[string DatabaseName, string VarName] { get { return GetVarValue(DatabaseName, VarName); } set { SetVarValue(DatabaseName, VarName, value); } } 
    public Boolean OverrideLocalCache { get { return fOverrideLocalCache; } set { fOverrideLocalCache = value; } }
  }
  
  public class SysVarDBRC {    
    DbConnectionInfo CI;
    public SysVarDBRC(String ConnectionString) {
      CI = new DbConnectionInfo("adatabasename", ConnectionString);      
    }
    private void SetVarValue(string VarName, string VarValue) {
      try {
        RCData d = new RCData(CI);
        d.ExecuteStoredProc("exec dbo.sp_SetDBVars @VarName, @VarValue",
          new StProcParam[] {
            new StProcParam("@VarName", DbType.String, VarName), 
            new StProcParam("@VarValue", DbType.String, VarValue)
          }
        );        
      } catch { }
    }

    private string GetVarValue( string VarName) {
      string result = "";
      try {
        
        RCData d = new RCData(CI);
        DataSet ds = d.GetStProcDataSet( "SELECT dv_VarValue, dv_VarName FROM DBVars WHERE VarName = @VarName ",
          new StProcParam[] {
            new StProcParam("@VarName", DbType.String, VarName)
          });

        if ((ds.Tables.Count > 0) & (ds.Tables[0].Rows.Count > 0)) {
          try { result = Convert.ToString(ds.Tables[0].Rows[0]["dv_VarValue"]); } catch { result = ""; }
        }       
        
      } catch { }
      return result;
    }
    public string this[string VarName] { get { return GetVarValue(VarName); } set { SetVarValue( VarName, value); } } 
    
  }




}
