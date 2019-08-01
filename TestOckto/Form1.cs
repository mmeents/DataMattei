using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using Octokit;
using C0DEC0RE;

namespace tstockto
{
  public partial class Form1:Form
  {
    public Form1() {
      InitializeComponent();
    }

    delegate void SetMessageCallback(string message);
    private void setMessage(string message) {
      if (this.edOut.InvokeRequired) {
          SetMessageCallback d = new SetMessageCallback(setMessage);
          this.Invoke(d, new object[] { message });
      } else {
        this.edOut.Text = message + Environment.NewLine + this.edOut.Text;          
      }
    }

    private async void button1_ClickAsync(object sender, EventArgs e) {
      var github = new GitHubClient(new ProductHeaderValue("SyncDrop"));
      #region Credit
      MMCredentialStore mc = new MMCredentialStore("");
      if(mc["github"]!="") {
        string sCredits = mc["github"];
        string sUser = sCredits.ParseString(" ", 0);
        string sPwd = sCredits.ParseString(" ", 1);
        github.Credentials = new Credentials(sUser, sPwd);
      }     
      #endregion

      var sRepUser = "mmeents";
      var sRepName = "MMDataStore";
      var sFileName = "EFJGJNATBKTTHHQYGL.txt";
      var branch = "master";

    //  var rMMDataStore = await github.Repository.Get("mmeents", "MMDataStore");  
    //  var sName = rMMDataStore.Name;
    //  var sLogin = rMMDataStore.Owner.Login;
      
      var RepContent = await github.Repository.Content.GetAllContents(sRepUser, sRepName, "path");
      foreach(RepositoryContent rc in RepContent){         
        if (rc.Name == sFileName){ 
          setMessage(rc.Name+" found in Encoded");
          String sTempName = Path.GetTempFileName();
          rc.DownloadUrl.SaveAs( sTempName );
          FileVar fv = new FileVar(sTempName);
          string sUserHash = mc.rTool.GetPublicCert().toHashSHA512();
          string sUserCert = mc.rTool.GetPublicCert();
          if (fv["Usr"+sUserHash] != sUserCert ) {
            fv["Usr"+sUserHash] = mc.rTool.GetPublicCert();          
            string sSha = rc.Sha;
            string sContents = File.ReadAllText(sTempName);
            var updateChangeSet = await github.Repository.Content.UpdateFile(sRepUser, sRepName, "path/"+sFileName, 
              new UpdateFileRequest("RegisterUserX", sContents, sSha, branch));
          }
          break;                 
        }        
      } 
    }
     

      /*  worked added file to location.
       *  https://laedit.net/2016/11/12/GitHub-commit-with-Octokit-net.html
       *  
       var owner = "mmeents";
       var repo = "MMDataStore";
       var branch = "master";
      
      var cSet = await github.Repository.Content.CreateFile("mmeents", "MMDataStore", "path/"+sFileName+".txt", new CreateFileRequest("Message", "[Contents][NATBKTTHHQ]", "master"));
      string sSha = cSet.Content.Sha; 

      //update  not tested yet
      updateChangeSet = await github.Repository.Content.UpdateFile(owner,repo, "path/file.txt", 
        new UpdateFileRequest("File update", "Hello Universe!", cSet.Content.Sha, branch));

      // delete file  not tested yet
      await github.Repository.Content.DeleteFile(owner, repo, "path/file.txt",
        new DeleteFileRequest("File deletion", updateChangeSet.Content.Sha, branch));
              */
      //var aUser = await github.User.Get("mmeents");
      //var x = await github.Repository.Get("mmeents", "MMDataStore");      
      //var rl = await github.Repository.GetAllForCurrent( );
      /*
      foreach(Repository r in rl){ 
        if (r.Name == "gitMsgr"){           
          var grcMain = await github.Repository.Content.GetAllContents(r.Owner.Login, r.Name); 
          foreach(RepositoryContent rc in grcMain){ 
            setMessage(rc.Name ) ;
            if (rc.Type == "Dir"){ 
              WalkRepContAsync(github, r, rc.Name);
            }
            
          }

        }
      }    
      
       https://raw.githubusercontent.com/mmeents/MMDataStore/master/path/EFJGJNATBKTTHHQYGL.txt 
       */


    

    public async void WalkRepContAsync(GitHubClient ghc, Repository r, string sPath){ 
      var grcMain = await ghc.Repository.Content.GetAllContents(r.Owner.Login, r.Name, sPath); 
      foreach(RepositoryContent rc in grcMain){ 
        setMessage(  rc.Name ) ;
        if (rc.Type == "Dir"){ 
          WalkRepContAsync(ghc, r, sPath+"\\"+rc.Name);
        } 
      }
    }

    private void button2_Click(object sender, EventArgs e) {

     

    }

    

  }

  public static class HelperClassX { 
    public static HttpWebRequest GetRequest( this string sURL ){ 
      var req = WebRequest.CreateHttp(sURL);
      return req;
    }
    public static string GetResponseString(this HttpWebRequest request) {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      using (var response = request.GetResponse()) {
        using (var stream = response.GetResponseStream()) {
          if (stream == null) throw new NullReferenceException("The HttpWebRequest's response stream cannot be empty.");
          using (var reader = new StreamReader(stream)) {
              return reader.ReadToEnd();
          }
        }
      }
    }
    public static string GetContentsAt(this string sURL){ 
      return sURL.GetRequest().GetResponseString();
    }
    public static Boolean SaveAs(this string sURL, string sFileName){ 
      Boolean didItExcept = false;
      try{ 
        string sContents = sURL.GetContentsAt();
        using (StreamWriter w = File.AppendText(sFileName)){ w.Write( sContents ); }
      } catch {
        didItExcept = true;
      }
      return !didItExcept;
    }
  }


}
