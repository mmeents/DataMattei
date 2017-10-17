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

namespace SiteCore {

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

    public DataSet GetDataSet(string QueryString) {
      Database db = DatabaseFactory.CreateDatabase();            
      DbCommand dbCommand = db.GetSqlStringCommand(QueryString);      
      return db.ExecuteDataSet(dbCommand);
    }
    public DataSet GetDataSet(string ConnectionName, string QueryString) {      
      Database db = DatabaseFactory.CreateDatabase(ConnectionName);
      DbCommand dbCommand = db.GetSqlStringCommand(QueryString);
      return db.ExecuteDataSet(dbCommand);
    }
    public void ExecuteSQLStr(string QueryString) {
      Database db = DatabaseFactory.CreateDatabase();
      DbCommand dbCommand = db.GetSqlStringCommand(QueryString);
      DataSet t = db.ExecuteDataSet(dbCommand);
    }
    public void ExecuteSQLStr(string ConnectionName, string QueryString) {
      Database db = DatabaseFactory.CreateDatabase(ConnectionName);
      DbCommand dbCommand = db.GetSqlStringCommand(QueryString);
      DataSet t = db.ExecuteDataSet(dbCommand);
    }
    public void ExecuteStoredProc(string ConnectionName, string ProcedureCode, StProcParam[] ParamsList) {
      Database db = DatabaseFactory.CreateDatabase(ConnectionName);
      string sqlCommand = ProcedureCode;
      DbCommand dbSelectCmd = db.GetSqlStringCommand(ProcedureCode);
      foreach (StProcParam p in ParamsList) {
        db.AddInParameter(dbSelectCmd, p.VarName, p.ParamType, p.VarValue);
      }
      db.ExecuteNonQuery(dbSelectCmd);
    }
    public DataSet GetStProcDataSet(string ConnectionName, string ProcedureCode, StProcParam[] ParamsList) {
      Database db = DatabaseFactory.CreateDatabase(ConnectionName);
      string sqlCommand = ProcedureCode;
      DbCommand dbSelectCmd = db.GetSqlStringCommand(ProcedureCode);
      foreach (StProcParam p in ParamsList) {
        db.AddInParameter(dbSelectCmd, p.VarName, p.ParamType, p.VarValue);
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
      DataSet t = db.ExecuteDataSet(dbCommand);
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
    public int ParseCount(string content, string delims) {
      return content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length;
    }
    public string ParseString(string content, string delims, int take) {
      string[] split = content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      return (take >= split.Length ? "" : split[take]);
    }
    public string TranslateToHTMLEncoded(string str) {
      string s = str.Replace("\n", "<br>");
      return s;
    }
    public string Base64EncodeText(string Text) {
      byte[] encBuff = System.Text.Encoding.UTF8.GetBytes(Text);
      return Convert.ToBase64String(encBuff);
    }
    public string Base64DecodeText(string Text) {
      byte[] decbuff = Convert.FromBase64String(Text);
      string s = System.Text.Encoding.UTF8.GetString(decbuff);
      return s;
    }
    public string ByteToHex(byte[] byteArray) {
      string outString = "";
      foreach (Byte b in byteArray)
        outString += b.ToString("X2");
      return outString;
    }
    public byte[] HexToByte(string hexString) {
      byte[] returnBytes = new byte[hexString.Length / 2];
      for (int i = 0; i < returnBytes.Length; i++)
        returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
      return returnBytes;
    }
    public string Encrypt(string StringToEncrypt) {
      string key = Base64DecodeText("QURGNzJBMDUxMjkwMDlGRg==");
      string iv = Base64DecodeText("MTQxRUM4QTkxNDU2OTMzNw==");
      DESCryptoServiceProvider des = new DESCryptoServiceProvider();
      des.Key = HexToByte(key);
      des.IV = HexToByte(iv);
      MemoryStream ms = new MemoryStream();
      CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
      StreamWriter sw = new StreamWriter(encStream);
      sw.WriteLine(StringToEncrypt);
      sw.Close();
      encStream.Close();
      byte[] buffer = ms.ToArray();
      ms.Close();
      return ByteToHex(buffer);
    }
    public string Decrypt(string CipherString) {
      string key = Base64DecodeText("QURGNzJBMDUxMjkwMDlGRg==");
      string iv = Base64DecodeText("MTQxRUM4QTkxNDU2OTMzNw==");
      DESCryptoServiceProvider des = new DESCryptoServiceProvider();
      des.Key = HexToByte(key);
      des.IV = HexToByte(iv);
      MemoryStream ms = new MemoryStream(HexToByte(CipherString));
      CryptoStream encStream = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
      StreamReader sr = new StreamReader(encStream);
      string val = sr.ReadLine();
      sr.Close();
      encStream.Close();
      ms.Close();
      return val;
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
        d.ExecuteStoredProc(DatabaseName, "exec dbo.rl_SetVariable @VarName, @VarValue",
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
          DataSet ds = d.GetStProcDataSet(DatabaseName, "SELECT VarValue, VarName FROM Variables WHERE VarName = @VarName ",
            new StProcParam[] {
              new StProcParam("@VarName", DbType.String, VarName)
            });

          if ((ds.Tables.Count > 0) & (ds.Tables[0].Rows.Count > 0)) {
            try { result = Convert.ToString(ds.Tables[0].Rows[0]["VarValue"]); } catch { result = ""; }
          }

          fCache[VarName] = result;
        } else {
          result = fCache[VarName].ToString();
        }
      } catch { }
      return result;
    }
    public string this[string DatabaseName, string VarName] { get { return GetVarValue(DatabaseName, VarName); } set { SetVarValue(DatabaseName, VarName, value); } } // set { SetVarValue(VarName, value); } }
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
        d.ExecuteStoredProc("exec dbo.rl_SetVariable @VarName, @VarValue",
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
        DataSet ds = d.GetStProcDataSet( "SELECT VarValue, VarName FROM Variables WHERE VarName = @VarName ",
          new StProcParam[] {
            new StProcParam("@VarName", DbType.String, VarName)
          });

        if ((ds.Tables.Count > 0) & (ds.Tables[0].Rows.Count > 0)) {
          try { result = Convert.ToString(ds.Tables[0].Rows[0]["VarValue"]); } catch { result = ""; }
        }       
        
      } catch { }
      return result;
    }
    public string this[string VarName] { get { return GetVarValue(VarName); } set { SetVarValue( VarName, value); } } // set { SetVarValue(VarName, value); } }
    
  }




}
