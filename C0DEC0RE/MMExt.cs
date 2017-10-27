﻿using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;


namespace C0DEC0RE {
    
  public static class MMExt {

    #region Salts...
    public static byte[] defIV = new byte[] { 11, 13, 27, 31, 37, 41, 71, 87 };
    #endregion 

    #region Integers

    public static decimal toDecimal(this Int32 x)
    {
      decimal y = Convert.ToDecimal(x);
      return y;
    }

    #endregion

    #region Double

    public static Int32 toInt32(this double x)
    {
      Int32 y = Convert.ToInt32(x);  // rounds
      return y;
    }

    public static Int32 toInt32T(this double x)
    {
      Int32 y = Convert.ToInt32(x.toStr2().ParseString(".", 0));
      return y;
    }

    public static string toStr2(this double x)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x);
      return y;
    }
    public static string toStr2P(this double x, Int32 iDigitToPad)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }
    public static string toStr4(this double x)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x);
      return y;
    }
    public static string toStr4P(this double x, Int32 iDigitToPad)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }
    public static string toStr8(this double x)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x);
      return y;
    }

    public static string toStr8P(this double x, Int32 iDigitToPad)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }

    public static decimal toDecimal(this double x)
    {
      decimal y = Convert.ToDecimal(x);
      return y;
    }

    #endregion

    #region Decimal 

    public static Int32 toInt32(this decimal x)
    {
      Int32 y = Convert.ToInt32(x);
      return y;
    }

    public static Int32 toInt32T(this decimal x)
    {
      Int32 y = Convert.ToInt32(x.toStr2().ParseString(".", 0));
      return y;
    }

    public static string toStr2(this decimal x)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x);
      return y;
    }
    public static string toStr2P(this decimal x, Int32 iDigitToPad)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }
    public static string toStr4(this decimal x)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x);
      return y;
    }
    public static string toStr4P(this decimal x, Int32 iDigitToPad)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }
    public static string toStr8(this decimal x)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x);
      return y;
    }

    public static string toStr8P(this decimal x, Int32 iDigitToPad)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }

    public static double toDouble(this decimal x)
    {
      double y = Convert.ToDouble(x);
      return y;
    }


    #endregion

    #region Strings

    #region Parse strings
    public static int ParseCount(this string content, string delims)
    {
      return content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length;
    }
    public static string ParseString(this string content, string delims, int take)
    {
      string[] split = content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      return (take >= split.Length ? "" : split[take]);
    }
    #endregion 

    #region crypto strings

    #region byte[]

    public static string toHexStr(this byte[] byteArray)
    {
      string outString = "";
      foreach (Byte b in byteArray)
        outString += b.ToString("X2");
      return outString;
    }

    public static byte[] toByteArray(this string hexString)
    {
      byte[] returnBytes = new byte[hexString.Length / 2];
      for (int i = 0; i < returnBytes.Length; i++)
        returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
      return returnBytes;
    }

    #endregion

    public static string toBase64EncodedStr(this string Text)
    {
      byte[] encBuff = System.Text.Encoding.UTF8.GetBytes(Text);
      return Convert.ToBase64String(encBuff);
    }
    public static string toBase64DecodedStr(this string Text)
    {
      byte[] decbuff = Convert.FromBase64String(Text);
      string s = System.Text.Encoding.UTF8.GetString(decbuff);
      return s;
    }

    public static string toAESCipher(this KeyPair akp, string sText)
    {
      string sResult = "";
      AesCryptoServiceProvider aASP = new AesCryptoServiceProvider();
      AesManaged aes = new AesManaged();
      aes.Key = akp.getKey;
      aes.IV = akp.getIV;
      MemoryStream ms = new MemoryStream();
      CryptoStream encStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
      StreamWriter sw = new StreamWriter(encStream);
      sw.WriteLine(sText.toBase64EncodedStr());
      sw.Close();
      encStream.Close();
      byte[] buffer = ms.ToArray();
      ms.Close();
      sResult = buffer.toHexStr();
      return sResult;
    }
    public static string toDecryptAES(this KeyPair akp, string sAESCipherText)
    {
      string val = "";
      AesCryptoServiceProvider aASP = new AesCryptoServiceProvider();
      AesManaged aes = new AesManaged();
      aes.Key = akp.getKey;
      aes.IV = akp.getIV;
      MemoryStream ms = new MemoryStream(sAESCipherText.toByteArray());
      CryptoStream encStream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
      StreamReader sr = new StreamReader(encStream);
      val = sr.ReadLine().toBase64DecodedStr();
      sr.Close();
      encStream.Close();
      ms.Close();
      return val;
    }

    public static string toDESCipher(this KeyPair akp, string sText)
    {
      string sResult = "";
      DESCryptoServiceProvider aCSP = new DESCryptoServiceProvider();
      aCSP.Key = akp.getKey;
      aCSP.IV = akp.getIV;
      MemoryStream ms = new MemoryStream();
      CryptoStream encStream = new CryptoStream(ms, aCSP.CreateEncryptor(), CryptoStreamMode.Write);
      StreamWriter sw = new StreamWriter(encStream);
      sw.WriteLine(sText.toBase64EncodedStr());
      sw.Close();
      encStream.Close();
      byte[] buffer = ms.ToArray();
      ms.Close();
      sResult = buffer.toHexStr();
      return sResult;
    }
    public static string toDecryptDES(this KeyPair akp, string sDESCipherText)
    {
      DESCryptoServiceProvider aCSP = new DESCryptoServiceProvider();
      aCSP.Key = akp.getKey;
      aCSP.IV = akp.getIV;
      MemoryStream ms = new MemoryStream(sDESCipherText.toByteArray());
      CryptoStream encStream = new CryptoStream(ms, aCSP.CreateDecryptor(), CryptoStreamMode.Read);
      StreamReader sr = new StreamReader(encStream);
      string val = sr.ReadLine().toBase64DecodedStr();
      sr.Close();
      encStream.Close();
      ms.Close();
      return val;
    }

    #endregion

    #endregion

    #region Dates and Times

    public static string toStrDateTime(this DateTime x)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd hh:mm:ss.FFF}", x);
      return y;
    }
    public static string toStrDate(this DateTime x)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}", x);
      return y;
    }
    public static string toStrTime(this DateTime x)
    {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:mm:ss.FFF}", x);
      return y;
    }
    public static string ToStrDateMM(this DateTime x) {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy hh:mm}", x);
      return y;
    }

    #endregion

    #region Files and Locations

    public static string SettingFileName(string sSettingName)
    {
      return UserLogLocation() + sSettingName + ".ini";
    }

    public static string LogFileName(string sLogName)
    {
      return UserLogLocation() + sLogName + DateTime.Now.toStrDate().Trim() + ".txt";
    }

    public static string UserLogLocation()
    {
      String sUserDataDir = Application.CommonAppDataPath + "\\";
      if (!Directory.Exists(sUserDataDir))
      {
        Directory.CreateDirectory(sUserDataDir);
      }
      return sUserDataDir;
    }

    public static string toLog(this string sMsg, string sLogName){
      using (StreamWriter w = File.AppendText(LogFileName(sLogName))) { w.WriteLine(DateTime.Now.toStrDateTime() + ":" + sMsg); }
      return sMsg;
    }

    public static string MMConLocation() {
      string sCommon = Application.CommonAppDataPath;
      sCommon = sCommon.Substring(0, sCommon.LastIndexOf('\\'));
      sCommon = sCommon.Substring(0, sCommon.LastIndexOf('\\'));
      sCommon = sCommon.Substring(0, sCommon.LastIndexOf('\\') + 1);
      return sCommon + "MMCommons";

    }

    #endregion
    
  }



}