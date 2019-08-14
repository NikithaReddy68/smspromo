using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntigralHelpDeskCore;
using HelpDeskCore;
using LogDll;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace fcc_promo_sms
{
    public partial class Index : System.Web.UI.Page
    {
        private string helpDeskDBConnStr = DBLib.DBLibProcessor.GetCPGDBConString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if ((Request["signout"] != null) && (Request["signout"] == "yes"))
                {
                    BAL objBAL = new BAL(helpDeskDBConnStr);
                    int loginID = 0;
                    int.TryParse(Session["Login_LoginID"].ToString(), out loginID);
                    int result = objBAL.SaveUserActionLogs(loginID, "HelpDesk.Mater", "Redirect", "User " + Session["Login_LoginName"] + " has logged out");
                    Log.Write("User " + Session["Login_LoginName"] + " has Logged out");
                }
                Session["Login_LoginID"] = null;
                txtUserName.Focus();


            }
        }
        protected void Login()
        {
            divError.Visible = false;
            lblErrMsg.Visible = false;
            lblErrMsg.Text = GetValidationError();

            if (lblErrMsg.Text.Trim().Length > 0)
                return;

            Session["Login_LoginID"] = null;
            string loginName = txtUserName.Text;
            string password = txtPassword.Text;
            BAL obj = new BAL(helpDeskDBConnStr);

            UserBO user = obj.GetUserDetails(loginName);
            if (user.LoginID > 0)
            {
                if (Encrypt(password) == user.Password)
                {
                    if (user.UserStatus.Trim().ToLower() == "active")
                    {
                        Session["Login_LoginID"] = user.LoginID.ToString();
                        Session["Login_LoginName"] = user.LoginName.ToString();
                        Session["Login_RoleID"] = user.RoleID.ToString();
                        Session["Login_FullName"] = user.Name.ToString();
                        Session["PageNames_ReadWrite"] = user.PageNames_ReadWrite;
                        Session["PageNames_ReadOnly"] = user.PageNames_ReadOnly;
                        Session["PageNames_NoAccess"] = user.PageNames_NoAccess;
                        Log.Write(Convert.ToString(Session["Login_LoginName"]) + "- successfully logged in");
                        if (user.LoginID == 1 || user.LoginID == 2)
                            Response.Redirect("cmpsms.aspx");
                        else
                            Response.Redirect("spreps.aspx");
                    }
                    else
                    {
                        txtPassword.Focus();
                        lblErrMsg.Text = "User account is inactive";
                        divError.Visible = true;
                        lblErrMsg.Visible = true;
                    }
                }
                else
                {
                    txtPassword.Focus();
                    lblErrMsg.Text = "Invalid password";
                    divError.Visible = true;
                    lblErrMsg.Visible = true;
                }
            }
            else
            {
                txtUserName.Focus();
                lblErrMsg.Text = "Invalid login name";
                divError.Visible = true;
                lblErrMsg.Visible = true;
            }
        }
        public static string Encrypt(string encString)
        {
            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

            try
            {

                byte[] byteHash, byteBuff;
                string strTempKey = ConfigurationManager.AppSettings["DESEncryptionKey"].ToString();

                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB

                byteBuff = ASCIIEncoding.ASCII.GetBytes(encString);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch
            {
                return "Wrong password. "; // +ex.Message;
            }
            finally
            {
                objDESCrypto = null;
                objHashMD5 = null;
            }
        }
        private string GetValidationError()
        {
            string strErr = string.Empty;
            if ((txtUserName.Text.Trim().Length == 0) && (txtPassword.Text.Trim().Length == 0))
            {
                txtUserName.Focus();
                strErr = "Please enter login name and password";
                divError.Visible = true;
                lblErrMsg.Visible = true;
            }
            else if (txtUserName.Text.Trim().Length == 0)
            {
                txtUserName.Focus();
                strErr = "Please enter login name";
                divError.Visible = true;
                lblErrMsg.Visible = true;
            }
            else if (txtUserName.Text.Trim().Length == 0)
            {
                txtPassword.Focus();
                strErr = "Please enter password";
                divError.Visible = true;
                lblErrMsg.Visible = true;
            }

            return strErr;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Login();
        }
    }
}
