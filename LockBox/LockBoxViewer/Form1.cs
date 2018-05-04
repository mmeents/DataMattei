using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using C0DEC0RE;
using Ionic.Zip;
using System.IO;



namespace LockBoxViewer
{
  public partial class Form1:Form{

    public LockBox ox = null;
    public MMCredentialStore MCS = null;
    public Form1(){
      InitializeComponent();
      MCS = new MMCredentialStore("");
      string[] args = Environment.GetCommandLineArgs();
      if ((args.Length>1)&&(args[1].Trim() != "")&&(File.Exists(args[1]))){ 
        string sFile = args[1];        
        string sPwd = MCS["LockBox"].ParseString(" ",1);
        ox = new LockBox(sFile,sPwd);
        buildTree();
      }      
    }    
    
    private void toolStripMenuItem1_Click(object sender,EventArgs e) {
      // open LockBox.
      odMain.FileName = "*.lxb";
      //string sPassword = "";
      if(odMain.ShowDialog()==DialogResult.OK) {
        string sFile = odMain.FileName;        
        string sPwd = MCS["LockBox"].ParseString(" ",1);
        ox = new LockBox(sFile,sPwd);
        buildTree();
        // }
      }
    }

    private void addItemsToolStripMenuItem_Click(object sender,EventArgs e){
      
      if(odAdd.ShowDialog()==DialogResult.OK) {        
        ox.Add(odAdd.FileNames);
        buildTree();
      }

    }

    private void closeBoxToolStripMenuItem_Click(object sender,EventArgs e)
    {
      ox = null;
      buildTree();
    }

    private void buildTree() {
      treeView1.Nodes.Clear();
      if(ox != null) {
        foreach(string sFile in ox.Files) {
          treeView1.Nodes.Add(sFile);
        }
      }
    }

    private void toolStripButton1_Click(object sender, EventArgs e) {
      if (dlgBrowseFolder.ShowDialog() == DialogResult.OK){ 
        string sPath = dlgBrowseFolder.SelectedPath;
      }
    }

    private void button1_Click(object sender, EventArgs e) {
      if (treeView1.SelectedNode != null ){ 
        string sFileName = treeView1.SelectedNode.Text;
        Int32 iChunkNo = sFileName.ParseString(":", 0).toInt32();
        string sFilePathname = "C:\\"+sFileName.ParseString(":",1).Replace('/','\\');
        sdMain.FileName = sFilePathname;
        if( sdMain.ShowDialog() == DialogResult.OK){ 
          ox.Extract(iChunkNo, sFileName, sdMain.FileName);

        }        
      }
    }
  }

  public class LockBox {

    public string FileNamePath = "";
    public string TempWorkingPath = "";
    KeyPair kpFileKey;
    FileVar ivFile;
    ZipFile ZipBox;
    public List<string> Chunks;
    public List<string> Files;
    public LockBox(string sFileNamePath, string sPassword) {
      Files = new List<string>();
      Chunks = new List<string>();
      FileNamePath = sFileNamePath;
      kpFileKey = new KeyPair(KeyType.AES,sPassword);
      if(File.Exists(FileNamePath)) {
        Load();
      }
    }
    public void Load() {
      if(File.Exists(FileNamePath)) {
        ivFile = new FileVar(FileNamePath);
        string sChunkCount = ivFile["ChunkCount"];
        if(sChunkCount=="") {
          sChunkCount = "0";
          ivFile["ChunkCount"]=sChunkCount;
        }
        Int32 iChunkCount = 0;
        if(Int32.TryParse(sChunkCount,out iChunkCount)) {
          for(Int32 iChunk = 1;iChunk <= iChunkCount; iChunk++) {
            string sCypherChunk = ivFile["x"+iChunk.ToString()];
            Chunks.Add(sCypherChunk);
            string sChunk = kpFileKey.NextKeyPair(iChunk).toDecryptAES(sCypherChunk);
            MemoryStream zipMS = new MemoryStream(sChunk.toByteArray());
            using(ZipFile z = ZipFile.Read(zipMS)) {
              foreach(ZipEntry ze in z) {
                string sFileName = ze.FileName;
                Files.Add(iChunk.ToString()+":"+ sFileName);
              }
            }
          }
        }
      }
    }
    public void Write() {
      FileVar ivFile = new FileVar(FileNamePath);
      ivFile["ChunkCount"]=Chunks.Count.ToString();
      Int32 iChunck = 1;
      foreach(string sChunck in Chunks) {
        ivFile["x"+iChunck.ToString()]=sChunck;
        iChunck++;
      }
    }
    public void Extract(Int32 iChunk, string sZipFileName, string sNewFileName){
       string sCompare = sZipFileName.ParseString(":", 1).ToUpper();
       string sCypherChunk = ivFile["x"+iChunk.ToString()];       
       string sChunk = kpFileKey.NextKeyPair(iChunk).toDecryptAES(sCypherChunk);

       MemoryStream zipMS = new MemoryStream(sChunk.toByteArray());
       using(ZipFile z = ZipFile.Read(zipMS)) {
         foreach(ZipEntry ze in z) {
           string sFileName = ze.FileName.ToUpper();
           if (sFileName == sCompare ){                            
              ze.Extract(sNewFileName, ExtractExistingFileAction.DoNotOverwrite);

           }           
         }
       }
    }
    public void Add(string[] files) {

      Int32 iFileCount = files.Count();
      if(iFileCount > 0) {
        
        using(ZipFile zLoader = new ZipFile()) {
            
            zLoader.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
            Int32 iNextChunk = Chunks.Count+1;
            foreach(string sFileName in files)
            {
              Files.Add(iNextChunk.ToString()+":"+sFileName);
              zLoader.AddFile(sFileName,Path.GetDirectoryName(sFileName));
              // zLoader.AddFile(sFileName,Path.GetDirectoryName(sFileName));   // 
            }
            string sZipName = Path.GetTempFileName();
            zLoader.Save(sZipName);
            byte[] aBuf = File.ReadAllBytes(sZipName);
            string sChunk = aBuf.toHexStr();
            string sCyperChunk = kpFileKey.NextKeyPair(iNextChunk).toAESCipher(sChunk);
            Chunks.Add(sCyperChunk);
          }
        
        Write();
      }
    }
    
  }


}
