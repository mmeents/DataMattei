# DataMattei


# C0DEC0RE -- my Core project likely never be finished.  This project is to pile all the reuable code I normaly use into 1 giant all purpose library.  the libary contains the following objects:
  ### MS Enterprise Lib objects consists of an entire earlier version of the library merging many small individual dlls into 1.  
  ### MMConMgr object for saving encrypted connection strings outside .Config files.  Uses reflection to override read only nature of the ConnectionManager structure and jacks into the ConnectionManager an array of connectionSettingsStrings taken from an encrypted file.  Object also poses windows form dialogs to request connection string details.  
  ### MMData and RCData objects for data access.  RC is for Remote Control.  their purpose is to get DataSet's of sql data.  dbWorkshop project below then generates code to work with MMData objects. 
  ### INI file support objects for file IO ops.
  ### MMExt object is a Static object with static methods that do a lot and appear within code completion.
  ### MMStrUtils object is a pre cursor to the MMExt object before they went static.  Left in for backward compatability purposes.
  ### Cryptography support for encodeing and encrypting content via AES, DES and RSA.  Main objects are Keypair, RSATool and MMExt 
  ### UserManager and providers to implement multi user website authentication with either Forms or Windows authentication.
  ### MMWebsiteContext has varAt methods that searches the multitude of locations for web request response parameter passing.
  ### dbVar and FileVar object that provides persistance via Database and File to name value pairs. 


# dbWorkshop -- Let you database code for you!  Demo app that uses C0DEC0RE to generate C# and SQL code fragments from the database.  Some of the features are the following:  
  ### Quickly see the contents of Stored Procedures, Functions and views.
  ### Generate a stored procedure that Adds and/or Updates a table or view.
  ### Generate a stored procedure that iterates across a table or view via sql cursor.


## TestStrings -- Project to list/maintain the connection strings for a machine.  I think this method of saving the connection strings in the local machine's common app data folder is great because they do not get included within repositories.  TestStrings project uses MMConMgr, so it's a most simple demo that also shows you how to configure MMConMgr in your apps.  Still need to test MMConMgr in web app.


# That's pretty much it folks.  A library of all the $$$ routines, enjoy and happy coding.

# Matt
