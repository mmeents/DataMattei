# DataMattei  
## The Repo that houses C0DEC0RE and other core development algos.  

## C0DEC0RE
  - Core Project continous intergration style.  This project is to pile all the reuable code I normaly use into 1 giant all purpose library.  the libary consists of the following components:
    - MS Enterprise Lib objects consists of an entire earlier version of the library merging many small individual dlls into 1.  
    - MMConMgr object for saving encrypted connection strings outside .Config files.  Uses reflection to override read only nature of the ConnectionManager structure and jacks into the ConnectionManager an array of connectionSettingsStrings taken from an encrypted file.  Object also poses windows form dialogs to request connection string details.  
    - MMData and RCData objects for data access.  RC is for Remote Control.  their purpose is to get DataSet's of sql data.  dbWorkshop project below then generates code to work with MMData objects. 
    - INI file support objects for file IO ops. thanks https://www.codeproject.com/Articles/20120/INI-Files  by Jacek Gajek 
    - MMExt object is a Static object library with static methods that appear within code completion.
    - MMStrUtils object is a pre cursor to the MMExt object before they went static.  Left in for backward compatability purposes.
    - MMCrypto  Cryptography support for encodeing and encrypting content via AES, DES and RSA.  Main objects are Keypair, RSATool and MMCredential Store.  MMCredential store is component that encrypts account credentials in a file in the ProgramData folder under MMCommon directory.  This allows for a way to store additional named credentials to a password protected file.  
    - MMWebSiteUsers  UserManager to implement multi user website authentication with either Forms or Windows authentication.
    - MMWebsiteContext  has varAt methods that searches the multitude of locations for web request response parameter passing.
    - dbVar and FileVar object that provides persistance via Database and File to name value pairs. 

## TestStrings 
  - Prototype app to test, list, maintain the encrypted connection strings for a windows machine.  
  - Demo of, method of saving the connection strings in the local machine's common app data folder specifically the MMCommon folder. 
  - Data saves to file system and do not get included within repositories.  Data is encrypted on filesystem with AES PDK.   
  - TestStrings project is demo of how to configure MMConMgr, so it's a most simple demo that also shows you how to configure MMConMgr in your apps.  Still need to test MMConMgr in web apps.
  - App can be used to configure connection strings for dbWorkshop, SqlChangeTracker below. 
  
## TestCredentialStore 
  - App that generalizes the TestStrings app to stores user defigned data at mmcommon folder.   
  - Allows you to inspect and modifiy the credentials on within the MMCredentialStore on a particular machine. 
  - Details stored on disk in AES PDK. 
  - App can be used to configure credentials for LockBox, TestOckto apps below. 

## dbWorkshop -- Let you database code for you!  
  - Demo app is a working example of querying the database for it's objects then layout over a treeview control. 
  - showcases many of C0DEC0RE capabilities.  
  - generate C# and SQL code fragments from the database.  Some of the features are the following:  
  - Quickly see the contents of Stored Procedures, Functions and views.
  - Generate a stored procedure that Adds and/or Updates a table or view.
  - Generate a stored procedure that iterates across a table or view via sql cursor.

## SqlChangeTracker
  - Port from dbWorkshop of query database for objects then iterate them and dump to filesystem.  
  - Great when used with Repo to track sql changes across time. 

## TestTrayApp 
  - Prototype tray app 
  - Basic functionality of a minimal tray app. 

## TaskRunnerTrayApp
  - Prototype longrunning tray app that maintains a list of schedulable runs daily task launcher  

## LockBox 
  - prototype zip file that uses Credential Store to password encrypt files within the zip. 

## TestOckto  
  - Prototype application that is used to test the Github api and automation therein.

## That's pretty much it folks.  A library of all the $$$ routines, may they live long and we all enjoy and happy coding.

# Mattei
