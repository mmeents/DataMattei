using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

      var sRepName = "MMDataStore";
      var rMMDataStore = await github.Repository.Get("mmeents", "MMDataStore");  
      var sName = rMMDataStore.Name;
      var sLogin = rMMDataStore.Owner.Login;
     

      /*  worked added file to location.
       *  https://laedit.net/2016/11/12/GitHub-commit-with-Octokit-net.html
       *  
       var owner = "mmeents";
       var repo = "MMDataStore";
       var branch = "master";
      var sFileName = "EFJGJNATBKTTHHQYGL";
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
      }     */


    }

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
}
