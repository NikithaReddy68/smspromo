using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using HelpDeskCore;
using System.Data.OleDb;
using System.Drawing;
using LogDll;

namespace fcc_promo_sms
{
    public partial class psms : System.Web.UI.Page
    {
        private string helpDeskDBConnStr = DBLib.DBLibProcessor.GetCPGDBConString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                BAL objBAL = new BAL(helpDeskDBConnStr);
                int loginID = 0;
                try
                {

                    int.TryParse(Session["Login_LoginID"].ToString(), out loginID);
                }
                catch
                {
                    Response.Redirect("Index.aspx");
                }
                int result = objBAL.SaveUserActionLogs(loginID, "psms.aspx", "PageLoad", "HomePage");
                Log.Write("User " + Session["Login_LoginName"] + " entered into Home page");

            }
        }

        protected void lnkSignOut_Click(object sender, EventArgs e)
        {
            Session["Login_LoginID"] = null;
            Session["Login_LoginName"] = null;
            Response.Redirect("Index.aspx");
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Log.Write("Entered into button click event");
            errorMessage.Visible = false;
            divError.Visible = false;
            if (fuExcel.HasFile)
            {
                string FileName = Path.GetFileName(fuExcel.PostedFile.FileName);
                string Extension = Path.GetExtension(fuExcel.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                Log.Write("FilePath:::" + FolderPath + FileName);
                string FilePath = FolderPath + FileName;
                fuExcel.SaveAs(FilePath);
                Log.Write("File Uploaded");
                errorMessage.ForeColor = Color.Green;
                errorMessage.Text = "Sheet Uploaded successfully";
                divError.Visible = true;
                errorMessage.Visible = true;

            }
        }

    }
}