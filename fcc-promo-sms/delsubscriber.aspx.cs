using System;
using System.Collections.Generic;
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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fcc_promo_sms
{
    public partial class delsubscriber : System.Web.UI.Page
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
                int result = objBAL.SaveUserActionLogs(loginID, "delsubscriber.aspx", "PageLoad", "HomePage");
                Log.Write("User " + Session["Login_LoginName"] + " entered into Home page");

            }
        }

        protected void lnkBtnSubmit_Click(object sender, EventArgs e)
        {
            BAL objBAL = new BAL(helpDeskDBConnStr);
            string msisdn = string.Empty;
           
            Session["UploadedFile"] = null;
            if (fileUpload.HasFile)
            {
                Session["UploadedFile"] = fileUpload;
                string FileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileUpload.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                if (Extension != ".txt")
                {
                    errorMessage.Text = "The uploaded file format is invalid, Please upload .txt file only.";
                    return;
                }
                string FilePath = FolderPath + "\\" + FileName + ".txt";
                fileUpload.SaveAs(FilePath);
                int counter = 0;
                string line;


                // Read the file and display it line by line.
                System.IO.StreamReader file =
                   new System.IO.StreamReader(FilePath);
                int lines = File.ReadAllLines(FilePath).Length;
                while ((line = file.ReadLine()) != null)
                {
                    msisdn=line+","+msisdn;
                    counter++;
                    msisdn= msisdn.TrimEnd(',');
                }

                file.Close();
                // lblRecieps.Text = lines.ToString() + " Total Recipients";

                gvData.DataSource = objBAL.Getdeletedsubscriber(msisdn);
                gvData.DataBind();
            }
        }

    
        protected void lnkUnsub_Click(object sender, EventArgs e)
        {
            BAL objBAL = new BAL(helpDeskDBConnStr);
            int result = objBAL.updatesubscriber();
        }
    }
}