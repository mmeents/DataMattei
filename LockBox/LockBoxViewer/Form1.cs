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
    public Form1()
    {
      InitializeComponent();
    }    

    private void toolStripMenuItem1_Click(object sender,EventArgs e) {
      // open LockBox.
      odMain.FileName = "*.lxb";
      if(odMain.ShowDialog()==DialogResult.OK) {
        string sFile = odMain.FileName;
        ox = new LockBox(sFile,"r2d2");
        buildTree();
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
          ivFile["ChunkCount"]="0";
        }
        Int32 iChunkCount = 0;
        if(Int32.TryParse(sChunkCount,out iChunkCount)) {
          for(Int32 iChunk = 1;iChunk <= iChunkCount; iChunk++) {
            string sCypherChunk = ivFile["x"+iChunk.ToString()];
            Chunks.Add(sCypherChunk);
            string sChunk = kpFileKey.NextKeyPair(iChunk).toDecryptAES(sCypherChunk);
            using(ZipFile z = ZipFile.Read(sChunk.ToStream())) {
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

    public void Add(string[] files) {

      Int32 iFileCount = files.Count();
      if(iFileCount > 0) {
        using(ZipFile zLoader = new ZipFile()){
          zLoader.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
          MemoryStream aStr = new MemoryStream();          
          Int32 iNextChunk = Chunks.Count+1;
          foreach(string sFileName in files){
            Files.Add(iNextChunk.ToString()+":"+sFileName);
            zLoader.AddFile(sFileName,Path.GetDirectoryName(sFileName)); 
              // zLoader.AddFile(sFileName,Path.GetDirectoryName(sFileName));   // 
          }          
          zLoader.Save(aStr);
          string sChunk = aStr.toString();          
          aStr.Close();
          string sCyperChunk = kpFileKey.NextKeyPair(iNextChunk).toAESCipher(sChunk);
          Chunks.Add(sCyperChunk);
        }
        Write();
      }
    }
    
  }


}
