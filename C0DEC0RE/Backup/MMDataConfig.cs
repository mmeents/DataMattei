using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

//using Data;
//using Data.Configuration;


namespace SiteCore {

  [Serializable]
	public class DbConnectionInfo	{
		private string m_connectionName=null;	
		private string m_userName=String.Empty;
		private string m_password=String.Empty;
		private string m_serverName=String.Empty;
		private string m_initialCatalog=String.Empty;
		private bool m_useIntegratedSecurity=true;
		public DbConnectionInfo()	{			
		}
		public DbConnectionInfo(string connectionName, string connectionString) {
			if (connectionName==null ||	connectionString==null) { 
			  throw new ArgumentNullException(); 
			}	else {
				m_connectionName=connectionName;
				SetConnectionString(connectionString);
			}
		}		
		public string ConnectionName {
			get	{	return m_connectionName; } 
			set { if (value==null){throw new ArgumentNullException(); }
				m_connectionName=value;
			}
		}

		public string ConnectionString{
			get	{	return GetConnectionString();	}
			set	{ if (value==null){throw new ArgumentNullException();}
				SetConnectionString(value);
			}
		}
	
		public string UserName {	get { return m_userName; }set { m_userName = value; }}

		public string Password {	get { return m_password; }set { m_password = value; }}

		public string ServerName{	get { return m_serverName; }set { m_serverName = value; }}

		public string InitialCatalog{ get { return m_initialCatalog; }set { m_initialCatalog = value; }}

		public bool UseIntegratedSecurity {	get { return m_useIntegratedSecurity; }	set { m_useIntegratedSecurity = value; }}

		private string GetConnectionString() {		// Data Source=(local);Integrated Security=true;Database=ebcs;
			StringBuilder sb=new StringBuilder();
			sb.Append("Data Source=");
			sb.Append(m_serverName);
			sb.Append(";");
			sb.Append("Initial Catalog=");
			sb.Append(m_initialCatalog);
			sb.Append(";");
			if (m_useIntegratedSecurity==false)	{
				sb.Append("User ID=");
				sb.Append(m_userName);
				sb.Append(";");
				sb.Append("Password=");
				sb.Append(m_password);
				sb.Append(";");
			} else {
				sb.Append("Integrated Security=SSPI;");
			}
			return sb.ToString();
		}

		private void SetConnectionString(string connstr){
			Hashtable connStringKeys=new Hashtable();
			string[] keysBySemicolon=connstr.Split(';');
			string[] keysByEquals;
			foreach(string keySemicolon in keysBySemicolon)	{
				keysByEquals=keySemicolon.Split('=');

				if (keysByEquals.Length==0)	{
					// do nothing
				}	else if (keysByEquals.Length==1) {
					// assume key name but no value
					connStringKeys.Add(keysByEquals[0].ToUpper(), "");
				}	else 	{
					connStringKeys.Add(keysByEquals[0].ToUpper(), keysByEquals[1]);
				}
			}

			if (connStringKeys.ContainsKey("SERVER")==true){
				m_serverName=(string)connStringKeys["SERVER"];
			}	else {
			  if (connStringKeys.ContainsKey("DATA SOURCE")==true){
			    m_serverName=(string)connStringKeys["DATA SOURCE"];
			  } else {
				  m_serverName="";
				}
			}

			if (connStringKeys.ContainsKey("DATABASE")==true)	{
				m_initialCatalog=(string)connStringKeys["DATABASE"];
			}	else {
			  if (connStringKeys.ContainsKey("INITIAL CATALOG")==true) {
			    m_initialCatalog=(string)connStringKeys["INITIAL CATALOG"];
			  } else {
				  m_initialCatalog="";
				}
			}		
			
			if (connStringKeys.ContainsKey("USER")==true) {
				m_userName=(string)connStringKeys["USER"];
			}	else {
				if (connStringKeys.ContainsKey("USER ID")==true) {
			    m_userName=(string)connStringKeys["USER ID"];
		    }	else {
			    m_userName="";
		    }
			}

			if (connStringKeys.ContainsKey("PASSWORD")==true)	{
				m_password=(string)connStringKeys["PASSWORD"];
			}	else{
				m_password="";
			}
			
			if (connStringKeys.ContainsKey("INTEGRATED SECURITY")==true) {
				m_useIntegratedSecurity=true;
			} else {
				m_useIntegratedSecurity=false;
			}
		}

		public override string ToString()	{
		  return m_connectionName + " (SqlServer)";				
		}
	
	} // end of class

}
