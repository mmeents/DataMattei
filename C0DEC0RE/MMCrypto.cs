﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace C0DEC0RE {

  public enum KeyType { AES, DES, RSA, HASHMAC };  

  public class KeyPair {
    private KeyType TypeOfKey;
    private string KeyPwd;
    private string KeyA;
    private string KeyB;
    private Int32 NextKeyBaseSalt = 41;
    public KeyPair(KeyType aKT, string sPassword)
    {
      TypeOfKey = aKT;
      KeyPwd = sPassword;
      PasswordDeriveBytes aPDB = new PasswordDeriveBytes(sPassword, null);
      switch (TypeOfKey)
      {
        case KeyType.AES:
          KeyA = aPDB.GetBytes(32).toHexStr();
          KeyB = aPDB.GetBytes(16).toHexStr();
          break;
        case KeyType.DES:
          KeyA = aPDB.CryptDeriveKey("DES", "SHA1", 64, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }).toHexStr();
          KeyB = MMExt.defIV.toHexStr();
          break;
        case KeyType.RSA:
          break;
        case KeyType.HASHMAC:
          break;
      }

    }
    public KeyPair NextKeyPair(Int32 Offset)
    {
      return new KeyPair(TypeOfKey, KeyPwd + (NextKeyBaseSalt + Offset).ToString());
    }
    //  used for AES & DES.
    public byte[] getKey { get { return KeyA.toByteArray(); } }
    public byte[] getIV { get { return KeyB.toByteArray(); } }
  }

  public class RSATool {
    private string KeyPass = "LockupKeyForPassword";
    public RSAParameters privateKey;
    public RSAParameters publicKey;
    private RSACryptoServiceProvider csp;
    public RSATool(Boolean bCreateNew) {
      if (bCreateNew) {
        csp = new RSACryptoServiceProvider(2048);
        privateKey = csp.ExportParameters(true);
        publicKey = csp.ExportParameters(false);
      }
      else
      {
        csp = new RSACryptoServiceProvider();
      }
    }
    private String doSerializeKey(RSAParameters rpValue)
    {
      var sw = new System.IO.StringWriter();
      var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
      xs.Serialize(sw, rpValue);
      //MMStrUtl su = new MMStrUtl();
      KeyPair akp = new KeyPair(KeyType.AES, KeyPass);
      return akp.toAESCipher(sw.ToString().toBase64EncodedStr());
    }
    private RSAParameters doDeserializeKey(String sKeyValue)
    {
      KeyPair akp = new KeyPair(KeyType.AES, KeyPass);
      String sXML = akp.toDecryptAES(sKeyValue).toBase64DecodedStr();
      var sr = new System.IO.StringReader(sXML);
      var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
      return (RSAParameters)xs.Deserialize(sr);
    }

    public String GetPublicCert()
    {
      return doSerializeKey(publicKey);
    }
    public void SetPublicCert(String sPublicCert)
    {
      publicKey = doDeserializeKey(sPublicCert);
    }

    public String GetPrivateCert()
    {
      return doSerializeKey(privateKey);
    }
    public void SetPrivateCert(String sPrivateCert)
    {
      privateKey = doDeserializeKey(sPrivateCert);
    }

    public String EncryptRSA(String sPublicCert, String sTextToEncode)
    {
      csp = new RSACryptoServiceProvider();
      csp.ImportParameters(doDeserializeKey(sPublicCert));
      byte[] bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(sTextToEncode);
      byte[] bytesCypherText = csp.Encrypt(bytesPlainTextData, false);
      return Convert.ToBase64String(bytesCypherText);
    }

    public String DecryptRSA(String sPrivateCert, String sTextToDecode)
    {
      csp = new RSACryptoServiceProvider();
      csp.ImportParameters(doDeserializeKey(sPrivateCert));
      byte[] bytesCypherText = Convert.FromBase64String(sTextToDecode);
      byte[] bytesPlainTextData = csp.Decrypt(bytesCypherText, false);
      return System.Text.Encoding.Unicode.GetString(bytesPlainTextData);
    }

    public String BroadCastPublicCert =
      "420450E2F9842C0D27FB7E347FAB59596E6B79B306CF5AAFC1EDC792F15F994024CD59C6E313B6C3DE68FE087F80793814A2FFB434C87119869910E861E68A1035F907F03E5E3593726889210BDD29C307D86032F246815AA945618FC1B186A5755D76CC06ED47EEB894C3F6C984A34DB906492BAA25EDF345A4D06846AC7C5C60BE082BE320E23BE04CA623D07AC1EAEE8C7E7B3B1700C2EEE3FF53024159F36D146993DB584EA00E2AD81496DD33970F9C1CECFB7F7DA2CDB" +
      "7A57DF6C16D85E84E391036973CF83286446BBD2B9C648F552AE42C4F8E5F5AEEA96A486F89C111531E1185147F730036473B92711017C9AF958B4710B855003E7B0A06B458050C3C445E2B10982F22357A10B89DF82EBB4AE5AD9FC99BA773E9AB6ACC824F37637416FF4BA0E42BCC2713FEFB50D42B22FAFB69C0C6C78A4EEB70227B6A67EAAE2F428ECADBD05A873475446E8CECC28B9B90F6B6D7B2363C45FBB4249AF0EF0BD8C79FBDA3DECE3EBF79C9F877DB80A540AEB1F1DCAADD9A15AED5518D12425" +
      "9C7168ED6F9CF2C9BDCA8504D793BA9E090652A72EBBA1B352A8B36A26B9913237986D402F1973C15B444471F6C6EDEF0DF3F103FC05A226606A12EF2F85BA45452C4BF01D2EE4A3EB40908AADD1DB4BFAD22E7D0D98D950258C3C88D1AF95E4CF9B9CC3A674087915F2950083C143AD011D92DD9232680DF82B420F47758FD8F8750AFC7AA52EBE0571CC5C3E301AC57C324BE8822A171D7C2582BE12989FA32FBD492CAEF2A9C681019B5D7C1F4A164E40EF9FB1CAF68783FC" +
      "E0605313D7D8EE4B83475E3C8BC05CAD590CCF5F07A8F1E43B9584F518D461EFE903260577E87CEA4CD9F8CA0AB76A06DEDCE241A325D7352520D46257DBBA2F31C3AD4D6FA85D443932AF3BDF5B3547FB148C420BFD0D89110E1F25735961A2221ECB0CA92C6611E399A190A781A4564895708D547DDB4F7C453A0BB42A6708CF5BE2840A03A1F9ADB55F2FCFBE6CEAE0D95F46980A82BBC9ADB00C89E9E31CA60F190A80D241AE79923749B495E2D2681C6681549A1C430322DEBB5524724C247AC9E3016";

    public String BroadCastPrivateCert =
      "420450E2F9842C0D27FB7E347FAB59596E6B79B306CF5AAFC1EDC792F15F994024CD59C6E313B6C3DE68FE087F80793814A2FFB434C87119869910E861E68A1035F907F03E5E3593726889210BDD29C307D86032F246815AA945618FC1B186A5755D76CC06ED47EEB894C3F6C984A34DB906492BAA25EDF345A4D06846AC7C5C60BE082BE320E23BE04CA623D07AC1EAEE8C7E7B3B1700C2EEE3FF53024159F36D146993DB584EA00E2AD81496DD33970F9C1CECFB7F7DA2CDB7A57DF6C16D85E84" +
      "E391036973CF83286446BBD2B9C648F552AE42C4F8E5F5AEEA96A486F89C111531E1185147F730036473B92711017C9AF958B4710B855003E7B0A06B458050C3C445E2B10982F22357A10B89DF82EBB4AE5AD9FC99BA773E9AB6ACC824F37637416FF4BA0E42BCC2713FEFB50D42B22FAFB69C0C6C78A4EEB70227B6A67EAAE2F428ECADBD05A873475446E8CECC28B9B90F6B6D7B2363C45FBB4249AF0EF0BD8C79FBDA3DECE3EBF79C9F877DB80A540AEB1F1DCAADD9A15AED5518D124259C7168" +
      "ED6F9CF2C9BDCA8504D793BA9E090652A72EBBA1B352A8B36A26B9913237986D402F1973C15B444471F6C6EDEF0DF3F103FC05A226606A12EF2F85BA45452C4BF01D2EE4A3EB40908AADD1DB4BFAD22E7D0D98D950258C3C88D1AF95E4CF9B9CC3A674087915F2950083C143AD011D92DD9232680DF" +
      "82B420F47758FD8F8750AFC7AA52EBE0571CC5C3E301AC57C324BE8822A171D7C2582BE12989FA32FBD492CAEF2A9C681019B5D7C1F4A164E40EF9FB1CAF68783FCE0605313D7D8EE4B83475E3C8BC05CAD590CCF5F07A8F1E43B9584F518D461EFE903260577E87CEA4CD9F8CA0AB76A06DEDCE241A325D7352520D46257DBBA2F31C3AD4D6FA85D443932AF3BDF5B3547FB148C420BFD0D89110E1F25735961A2221ECB0CA92C6611E399A190A781A4564895708D547DDB4F7C453A0BB42A6708C" +
      "F5BE2840A03A1F9ADB55F2FCFBE6CEAE0D95F46980A82BBC9ADB00C89E9E31CA60F190A80D759DFB193C39E9B9A1D2FC80D862FEBBEF21899A7E74472D8F446ECC42C152599B5AE92831CD1D2E86DE1D76F4E68811B1FEB17A119A9DA280B2068B718296556342127F25FE7CB6676CFCBC3E0CD530F6FFB71FAA7568C6A2F2F0A06C8357BFD9EA9972870F5581E68D6122F53ECE0FC702B2DE7446B7D8EF0DEF3EC3BA917A503523F30BCDA76DBE27E136B63C8ED66FFC4752A744A4DF337BC534F57" +
      "D6B66EB202970B0BE0EDA11A4F20A38077D34506C96BD4D3381BB0F254447EFCD567C03C2BECE15F93EFA89B58FB1FE163D901390E8DAF77DD70C6ECE9F08F72852FE3A96D2ADF3F52274DFB8D24E4989F32770F24B688888591B94D12EECB50BD4B737349A4322976303B5391EE15754BCCDD93E76938EE935C0A90D01E9753C96D1189D732E30DADBB329B3486DA2AF7BB007ED9A6D80F4763035079714125514C9A084C94139A3A8079093234563FE6A9710EDFC9064ADE7BF49D5ACD44D3163F0C" +
      "D596DD5FAC0417D65E9408FD330C5DBD301A190B3E00D3BFD037CBE071FDD336FBC2A9782056F0F155E1525FAFA4B46DA480A0D5ADBFDE4EC88C527347A5F13552774749C890C97C4AE3FC0ADFFBCAECA524ED02DB00C61E178669F6910A80A9D9D2A34236C49C24D73209A22069013C86A4AF476D33C5C95B92A8D9B9D560AE35F09445478B334171488019386CE5F41CA03F9D547" +
      "ED6011BE433C9C6D6942C5A7A7C21458E332D16EE7019E3FE55E2EFA856A63A63769DA0BADA7B6577C1AF7437A002603C0B0105CF96A33B634CA0D2F9D1D29E95AF98869C8999196274C66972AE91603F78675EE4EE745724625DE530D682A053A41033B27101D311D7744F8A28F99D325490A62713296EBBEA24035710AF8B9E6EC0B97BD0615B52C2659ECD9BD017DDD42EE15E586E7A3DD48434FF9C4ABF1F362CA6207EA23057F97BE1BDF3E70245724E337888D531711A3F5B3FDF624F2D7A1D" +
      "D63EEDB182CDB50883771F7E3FEED1BD44690BD5B8A88552E32426ED39CCB9BFFAE6C5A10F011CF108CBCBD2F0DEA1D7E9ACF31021E71F0E5633B29C47D7832DACA43DED7A089EA8F82B1831F40A78D38E95D96B5A4FC9C665656C6320F1675BCA1AA73AC10207F41353B4D388DF113DE1E615BD8A5097B1C3A50ECBE2449F92069D9BCB085B45B7559A241A6DA8B6B19158785BF580E3E983A3362220FEF63A145DE89F1A60CF39336E99AF9C8A2065B55C4CA10CFC4C946FCB681AA0D4EE01C23A7B" +
      "48EDE516198D59FF9EB27026EF726202A139CE51168BE6303DA2A5E1ADF1D0A59612AC9E51D17A661FAFD3796DC38E8B1453017934C160E08171BAFDC0D180BFE8B5A24A6307DFAE914E39CD12CDA207B41BAAEE0FEF29C58DC2C46A45544A976F6A5205DE17827737D9A058E7D06FAC191456EB" +
      "B97110F3D3C571911362046F7E6D300DCCE7FF9A9419A4C6EA4E833054F1026A2F5E396781B4A15064A28B63EAD372461E7D1F5DCAD55680BD30A31664E756D1D26BB7A8C70964CCF139F376573A550100588F860D9AA3834191164B7E17F1A23647CC7228632233B796D71373537D082F1C1E12650A3495604FB3E30734B6223D79F2AACA3258940F9CD57FB1FC0A7BDC1B0F7947A9B6B56E6A40A01AB3D76F3D833A9CAE85DF7FB03789C8B659DC14F7FFBD4ED15FA3CE20FC1D403684F266812378" +
      "4627389A269BD97EEB323DDDE351E9D1C7F3D9A9B4A177A5FC1E163806019D1B834E53BDC5A18F21DBA4E5F3CE4317CB4E9DD5CB21BCFC082E9F11437931847255F3FDE78ACA3DE3EE54ECA60E6D5CF3CE604690D3AEE4734DD4BD5AA2E8DECC740C03E4DC093C8D733D53E017054FA3A3F7B7B54D08719F8B5AA98135BF16BBE8D5F31B6B7CB5C56AF1A704E44B1733BE738A60479994767F6CC3DBB1BCB5BEB89C9CBD9AFC0B7C174B6A1DBC93A50D54B73F875400447034EE56F9A05A733A24C2833" +
      "9B473B820F3D009EB7FAA98A91E07712DFE8468476CB9DD3B38AC30FF903CE96815F51F9D06DC37906C02EF5EC1902417548E9FCB0C228F8666A7B1161FA0EEF26D192AD9FFA036D79CBC109BAFAE8D5579A489AFF43D522C29317FEBA985D3CA3D2B642B2281CCDD30074AF52051B5209F478B450FB2F5D2F8935AECA6C464BB3AC3C2FAC455026A0BE7CB0ED2083AA6EDC03ECBD17727C165E61C3C16C7AB744F3DF1AFD22F0B2BCF06678A2FB358111895B04A5BB69A5F8BE6A8A7EBD00EEC71247C5" +
      "25F63601F27B8D5167074E59E5E0B93D1B9D2B61697335AF69BB54210426EC01454A5D3E81A4B3593181729C9BED1330CB069BE889733B49508F2CC1E7AC981F103D086AF8DFC27533B6C3CDE3393A23707AA15FB25AFA7E47C2089F25CABF08FD9E222027EFFCF1613FDD9823D6E5710088C7CE433F8E8C7F8FC8A6F80AE4FD6429F5E395D38320853DF2C886005B298AF73D6657B4";


  }
    
  public class MMCredentialStore {
    public FileVar fvMain;
    public KeyPair kpMain;
    public RSATool rTool;
    public string StorageFileName;
    private string sMasterPwd = "";    
    public MMCredentialStore( string aMasterPwd, string aFileName) {
      string sFileName =(aFileName==""? "\\MachineCredentialStoreRoot.Cert" : aFileName);      
      if (aMasterPwd==""){
        PasswordDialog pd = new PasswordDialog();
        if (pd.ShowDialog()!=DialogResult.OK){ 
          throw new Exception("Password not present. Aborting start.");
        } else { 
          sMasterPwd = pd.sPassword;
          if (sMasterPwd==""){
            throw new Exception("Password not present. Aborting start.");
          }
        }
      } else { 
        sMasterPwd = aMasterPwd;
      }
      
      //sFileName = MMExt.MMConLocation() + sFileName;
      string sPriCert = "";
      string sPubCert = "";
      Boolean bRootCertFound = true;
      if(File.Exists(sFileName)) {
        fvMain = new FileVar(sFileName);        
        if(sMasterPwd != "") {
          try {
            kpMain = new KeyPair(KeyType.AES, sMasterPwd);
          } catch {
            bRootCertFound = false;
          }
        }        
        if(bRootCertFound) {
          string sUVPriCert = fvMain["RootPrivateCert"];
          if(sUVPriCert != "") {
            try {
              sPriCert = kpMain.toDecryptAES(sUVPriCert);
            } catch {
              bRootCertFound = false;
            }
          } else {
            bRootCertFound = false;
          }
        }
        if(bRootCertFound) {
          string sUVPriCert = fvMain["RootPublicCert"];
          if(sUVPriCert != "") {
            try {
              sPubCert = kpMain.toDecryptAES(sUVPriCert);
            } catch {
              bRootCertFound = false;
            }
          } else {
            bRootCertFound = false;
          }
        }
      } else {
        bRootCertFound = false;
      }

      if(bRootCertFound) {
        try {
          rTool = new RSATool(false);
          rTool.SetPrivateCert(sPriCert);
          rTool.SetPublicCert(sPubCert);
        } catch {
          bRootCertFound = false;
        }
      }

      if(!bRootCertFound) {      
        fvMain = new FileVar(sFileName);
        rTool = new RSATool(true);
        sPubCert = rTool.GetPublicCert();
        sPriCert = rTool.GetPrivateCert();        
        kpMain = new KeyPair(KeyType.AES, sMasterPwd);
        fvMain["RootPrivateCert"] = kpMain.toAESCipher(sPriCert);
        fvMain["RootPublicCert"] = kpMain.toAESCipher(sPubCert);                
      }
      
    }    
    public MMCredentialStore(string aMasterPwd) : this(aMasterPwd, "") { 

    }
    public string this [string sCredentialName]{ 
      get { return (fvMain["c"+sCredentialName]==null?"": kpMain.toDecryptAES( fvMain["c"+sCredentialName] )); } 
      set { fvMain["c"+sCredentialName] = kpMain.toAESCipher(value); }
    }
    public void RemoveCredential(string sCredentialName){ 
      fvMain.RemoveVar("c"+sCredentialName);
    }     
  }    

}
