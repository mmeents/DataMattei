using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace C0DEC0RE {

  public class MMWebSiteConstants {
   public static String DatabaseName() { return "PD"; }
  }
    
  public class MMWebSiteContext {
    HttpContext flhc;
    User flu;
    public MMWebSiteContext(HttpContext hc) {
      flhc = hc;      
      UserManager um = new UserManager();
      MMStrUtl x = new MMStrUtl();      
      string userName = hc.User.Identity.Name;
      if (MMStrUtl.ParseCount(userName, "\\") == 2) {
        flu = um.GetUser(MMStrUtl.ParseString(userName, "\\", 1)); // since first part is domain in windows
      } else {
        flu = um.GetUser(userName);  // using forms authentication
      }   
    }
    public string varAt(string title, string sdefault) {
      string s = "";
      try { s = flhc.Request.QueryString.GetValues(title)[0].ToString(); } catch {
        try { s = flhc.Request.Form.GetValues(title)[0].ToString(); } catch {
          try { s = flhc.Request.Params.GetValues(title)[0].ToString(); } catch {
            s = sdefault;
          }
        }
      }
      return s;
    }
    public User ActiveUser { get { return flu; } set { flu = value; } }
  }

  public class User {
    private string pDomain = string.Empty;
    private string pLoginName = string.Empty;
    private string pPassword = string.Empty;
    private string pName = string.Empty;
    private string pStaffName = string.Empty;
    private string pEmail = string.Empty;
    private string pSalter = string.Empty;
    private string pNotes = string.Empty;
    private string pUserID = string.Empty;
    private bool pIsActive = true;
    DateTime pPasswordExpires = DateTime.MaxValue;
    private bool pIsUserAdmin = true;    
    private bool pRecDirty = false;
    private string getSALT() {
      if (pSalter == string.Empty) {
        Guid salter = Guid.NewGuid();
        pSalter = salter.ToString();
      }
      return pSalter;
    }
    private string getUserID() {
      if (pUserID == string.Empty) {
        Guid U_ID = Guid.NewGuid();
        pUserID = U_ID.ToString();
      }
      return pUserID;
    }
    public void EncodePassword(string pwd) {
      if (pSalter == string.Empty) { getSALT(); };
      MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
      Byte[] hashedBytes;
      UTF8Encoding encoder = new UTF8Encoding();
      hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(pwd + pSalter));
      pPassword = BitConverter.ToString(hashedBytes);
      pRecDirty = true;
    }
    public string Domain { get { return this.pDomain; } set { this.pDomain = value; RecDirty = true; } }
    public string LoginName { get { return this.pLoginName; } set { this.pLoginName = value; RecDirty = true; } }
    public string Name { get { return this.pName; } set { this.pName = value; RecDirty = true; } }
    public string Password { get { return this.pPassword; } set { pPassword = value; RecDirty = true; } }
    public bool IsActive { get { return pIsActive; } set { pIsActive = value; } }
    public string Email { get { return pEmail; } set { pEmail = value; RecDirty = true; } }
    public string Salter { get { return getSALT(); } set { pSalter = value; } }
    public string UserID { get { return getUserID(); } set { pUserID = value; } }
    public DateTime PasswordExpires { get { return pPasswordExpires; } set { pPasswordExpires = value; } }    
    public string StaffName { get { return pStaffName; } set { pStaffName = value; } }    
    public bool IsUserAdmin { get { return pIsUserAdmin; } set { pIsUserAdmin = value; RecDirty = true; } }    
    public bool RecDirty { get { return pRecDirty; } set { pRecDirty = value; } }
  }

  public sealed class UnknownUser : User { }

  public sealed class UserManager {
    private static readonly string policy = "LocalPolicy";
    public static readonly string SelectUser =
      "SELECT U_ID, U_Login, U_PASSWORD, U_Name, U_Email, U_SALT, U_IsUserAdmin, U_Domain " +
      "FROM Users U ";
    public void UserUpdate(User toUpdate) {
      MMData d = new MMData();
      Int32 iU_ID = -1;
      try { iU_ID = Convert.ToInt32(toUpdate.UserID); } catch (Exception e) {  
      }
      DataSet ud = d.GetStProcDataSet(MMWebSiteConstants.DatabaseName(), "exec dbo.sp_AddUpdateUsers @aU_ID, @aU_Login, @aU_Password, @aU_Name, @aU_Email, @aU_SALT, @aU_IsUserAdmin, @aU_Domain", 
        new StProcParam[] {
        new StProcParam("@aU_ID", DbType.String, iU_ID),
        new StProcParam("@aU_Login", DbType.String, toUpdate.LoginName),
        new StProcParam("@aU_Password", DbType.String, toUpdate.Password),
        new StProcParam("@aU_Name", DbType.String, toUpdate.Name),
        new StProcParam("@aU_Email", DbType.String, toUpdate.Email),
        new StProcParam("@aU_SALT", DbType.String, toUpdate.Salter),
        new StProcParam("@aU_IsUserAdmin", DbType.Boolean, toUpdate.IsUserAdmin),        
        new StProcParam("@aU_Domain", DbType.String, toUpdate.Domain)
      });    
    }
    public User GetUser(string userName) {
      User user = new UnknownUser();
      MMData d = new MMData();        
      try {
          DataSet x = d.GetStProcDataSet(MMWebSiteConstants.DatabaseName(), SelectUser + " WHERE (U_Login = @aLogin)",
          new StProcParam[] { new StProcParam("@aLogin", DbType.AnsiString, userName) });
        if ((x.Tables.Count == 1) & (x.Tables[0].Rows.Count == 1)) {
          user = BuildUserFromRow(x.Tables[0].Rows[0]);
        }        
      } catch { }
      return user;
    }
    public User GetUserID(string userID) {
      User user = new UnknownUser();
      MMData d = new MMData();
      try {
          DataSet x = d.GetStProcDataSet(MMWebSiteConstants.DatabaseName(), SelectUser + "WHERE (U_ID = cast( @aID as uniqueidentifier ))",
          new StProcParam[] { new StProcParam("@aID", DbType.String, userID) });
        if ((x.Tables.Count == 1) & (x.Tables[0].Rows.Count == 1)) {
          user = BuildUserFromRow(x.Tables[0].Rows[0]);
        }
      }  catch { }
      return user;
    }
    private User BuildUserFromRow(DataRow userRecord) {      
      User user = new User();
      user.UserID = userRecord["U_ID"].ToString();
      user.Domain = userRecord["U_Domain"].ToString();
      user.LoginName = userRecord["U_Login"].ToString();
      user.Password = userRecord["U_PASSWORD"].ToString();
      user.Name = userRecord["U_Name"].ToString();
      user.Email = userRecord["U_Email"].ToString();
      user.Salter = userRecord["U_Salt"].ToString();
      user.IsUserAdmin = Convert.ToBoolean(userRecord["U_IsUserAdmin"]);      
      user.PasswordExpires = DateTime.Now.AddYears(20);
      user.RecDirty = false;
      return user;
    }
    public bool Authenticate(string userName, string password) {
      bool authented = false;
      User user = GetUser(userName);
      if (user is UnknownUser) {
        return authented;
      } else {
        string encrPassword = user.Password;
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        Byte[] hashedBytes;
        UTF8Encoding encoder = new UTF8Encoding();
        hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(password + user.Salter));
        string reHashedPassword = BitConverter.ToString(hashedBytes);
        authented = reHashedPassword == encrPassword ? true : false;
      }
      return authented;
    }   
    private int getRoleID(string role) {
      int roleID = -1;   
      return roleID;
    }
  }
 
  public class LocalRoleProvider : RoleProvider {
    private UserManager userManager = new UserManager();
    private string applicationName = "App";
    public override string ApplicationName {get {return applicationName;} set { applicationName = value;}}
    public override void CreateRole(string roleName) { throw new NotSupportedException("The method or operation is not implemented."); }
    public override void AddUsersToRoles(string[] usernames, string[] roleNames){throw new NotSupportedException("The method or operation is not implemented."); }   
    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {throw new NotSupportedException("The method or operation is not implemented."); }
    public override string[] FindUsersInRole(string roleName, string usernameToMatch) {throw new NotSupportedException("The method or operation is not implemented."); }
    public override string[] GetAllRoles(){throw new NotSupportedException("The method or operation is not implemented."); }
    public override string[] GetRolesForUser(string username) {throw new NotSupportedException("The method or operation is not implemented."); }
    public override string[] GetUsersInRole(string roleName) {throw new NotSupportedException("The method or operation is not implemented."); }
    public override bool IsUserInRole(string username, string roleName) {throw new NotSupportedException("The method or operation is not implemented."); }
    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {throw new NotSupportedException("The method or operation is not implemented."); }
    public override bool RoleExists(string roleName) {throw new NotSupportedException("The method or operation is not implemented."); }    
  }

  public class LocalMembershipProvider : MembershipProvider {
    private string applicationName = "";
    private string name = "LocalMembershipProvider";
    private UserManager userManager = new UserManager();
    private static readonly string policy = "LocalPolicy";
    private bool pEnablePasswordReset;
    private bool pEnablePasswordRetrieval;
    private bool pRequiresQuestionAndAnswer;
    private bool pRequiresUniqueEmail;
    private int pMaxInvalidPasswordAttempts;
    private int pPasswordAttemptWindow;
    private int pMinRequiredNumericPasswordLength;
    private int pMinRequiredNonAlphanumericCharacters;
    private int pMinRequiredPasswordLength;
    private string pPasswordStrengthRegularExpression;
    private int pPasswordExpiresInDays;
    private MembershipPasswordFormat pPasswordFormat = 0;
    public LocalMembershipProvider() { }
    public override string ApplicationName { get { return applicationName; } set { applicationName = value; } }
    public override string Name { get { return name; } }
    public override bool EnablePasswordReset { get { return pEnablePasswordReset; } }
    public override bool EnablePasswordRetrieval { get { return pEnablePasswordRetrieval; } }
    public override bool RequiresQuestionAndAnswer { get { return pRequiresQuestionAndAnswer; } }
    public override bool RequiresUniqueEmail { get { return pRequiresUniqueEmail; } }
    public override int MaxInvalidPasswordAttempts { get { return pMaxInvalidPasswordAttempts; } }
    public override int PasswordAttemptWindow { get { return pPasswordAttemptWindow; } }
    public override MembershipPasswordFormat PasswordFormat { get { return pPasswordFormat; } }
    public override int MinRequiredNonAlphanumericCharacters { get { return pMinRequiredNonAlphanumericCharacters; } }
    public override int MinRequiredPasswordLength { get { return pMinRequiredPasswordLength; } }
    public int PasswordExpiresInDays { get { return pPasswordExpiresInDays; } }
    public int MinRequiredNumericPasswordLength { get { return pMinRequiredNumericPasswordLength; } }
    public override string PasswordStrengthRegularExpression { get { return pPasswordStrengthRegularExpression; } }
    public bool IsValidPassword(string password) {      
      string regex = "";
      regex = @"[a-zA-Z0-9]{" + pMinRequiredPasswordLength + @",}";
      Regex regValidate = new Regex(regex);
      Match m = regValidate.Match(password);
      if (!m.Success) { return false; }
      if (pMinRequiredNumericPasswordLength > 0) {
        regex = @"[0-9]{" + pMinRequiredNumericPasswordLength + @",}";
        regValidate = new Regex(regex);
        m = regValidate.Match(password);
        if (!m.Success) {
          return false;
        }
      }
      return true;      
    }
    public override bool ChangePassword(string username, string oldPassword, string newPassword) { throw new NotSupportedException("The method or operation is not implemented."); }
    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer) { throw new NotSupportedException("The method or operation is not implemented."); }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
      try {   // does user exist ? 
        User u = userManager.GetUser(username);
        if (!(u is UnknownUser)) {
          status = MembershipCreateStatus.DuplicateUserName;
          return BuildMembershipUser(u);
        } else {  //  if not add user.
            u = new User();            
            u.LoginName = username;
            u.EncodePassword(password);
            u.Email = email;
            userManager.UserUpdate(u);
            status = MembershipCreateStatus.Success;
            return BuildMembershipUser(userManager.GetUser(username));            
        }
      } catch {
        status = MembershipCreateStatus.ProviderError;
        return null;
      }      
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData) { throw new NotSupportedException("The method or operation is not implemented."); }
    private string GetConfigValue(string configValue, string defaultValue) {
      if (String.IsNullOrEmpty(configValue)) return defaultValue;
      return configValue;
    }
    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config) {      
      if (config == null) { throw new ArgumentException("config"); }
      if (String.IsNullOrEmpty(name)) { name = "LocalMembershipProvider"; }
      if (string.IsNullOrEmpty(config["description"])) {
        config.Remove("description");
        config.Add("description", "Website Membership provider");
      }

      this.name = name;
      base.Initialize(name, config);
      //pApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
      pMaxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
      pPasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
      pMinRequiredNonAlphanumericCharacters = 0; // Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
      pMinRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
      pMinRequiredNumericPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredNumericCharacters"], "0"));
      pPasswordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
      pEnablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
      pEnablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
      pRequiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
      pRequiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
      pPasswordExpiresInDays = Convert.ToInt32(GetConfigValue(config["passwordExpirationDays"], "-1"));
      //pWriteExceptionsToEventLog = Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "true"));     
      
    }
    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords) { throw new NotSupportedException("The method or operation is not implemented."); }
    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) { throw new NotSupportedException("The method or operation is not implemented."); }
    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) { throw new NotSupportedException("The method or operation is not implemented."); }
    public override int GetNumberOfUsersOnline() { throw new NotSupportedException("The method or operation is not implemented."); }
    public override string GetPassword(string username, string answer) { throw new NotSupportedException("The method or operation is not implemented."); }
    public override MembershipUser GetUser(string username, bool userIsOnline) {      
      User u = userManager.GetUser(username);
      if (!(u is UnknownUser)) {
        return BuildMembershipUser(u);
      } else {
        return null;
      }
    }
    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) { throw new NotSupportedException("The method or operation is not implemented."); }
    public override string GetUserNameByEmail(string email) { throw new NotSupportedException("The method or operation is not implemented."); }
    public override string ResetPassword(string username, string answer) { throw new NotSupportedException("The method or operation is not implemented."); }
    public override bool UnlockUser(string userName) { throw new NotSupportedException("The method or operation is not implemented."); }
    public override void UpdateUser(MembershipUser user) {throw new NotSupportedException("The method or operation is not implemented."); }
    public override bool ValidateUser(string username, string password) {
      return userManager.Authenticate(username, password);
    }
    private static MembershipUser BuildMembershipUser(User user) {
      MembershipUser membershipUser = null;
      membershipUser = new MembershipUser(
        "LocalMembershipProvider",
        user.LoginName,
        user.UserID.ToString(),
        user.Email,
        String.Empty,
        user.Name,
        true,
        false,
        DateTime.Now,
        DateTime.Now,
        DateTime.Now,
        DateTime.Now,
        new DateTime(1980, 1, 1));    
      return membershipUser;
    }
  }



}
