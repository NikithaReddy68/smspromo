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
    public partial class cmpsms : System.Web.UI.Page
    {
        private string helpDeskDBConnStr = DBLib.DBLibProcessor.GetCPGDBConString();
        List<ChangeSubscriberServicesBO> lstShortCodes;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

               // lblRecieps.Text = "0 Total Recipients";
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
                int result = objBAL.SaveUserActionLogs(loginID, "cmpsms.aspx", "PageLoad", "HomePage");
                Log.Write("User " + Session["Login_LoginName"] + " entered into Home page");
                LoadShortcodes();
            }
            
        }
        protected void lnkBtnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        private void LoadShortcodes()
        {
            ddlShortCode.Items.Clear();
            ddlShortCode.Enabled = true;

            BAL objBAL = new BAL(helpDeskDBConnStr);
            lstShortCodes = new List<ChangeSubscriberServicesBO>();

            lstShortCodes = objBAL.GetLookupShortcodesByTelcoID(7, "VAULT_SUB");
            if (lstShortCodes.Count > 0)
            {
                ddlShortCode.Items.Clear();
                ddlShortCode.DataSource = lstShortCodes;
                ddlShortCode.DataTextField = "Shortcode";
                ddlShortCode.DataValueField = "Shortcode";
                ddlShortCode.DataBind();
                //ddlShortCode.Items.Insert(0, "--Select--");
                //ddlShortCode.SelectedIndex = 0;
            }
            else
            {
                ddlShortCode.Text = "Shortcode doesnot exists";
            }
        }
        protected void lnkBtnSubmit_Click(object sender, EventArgs e)
        {
            string msisdns = string.Empty;
            divError.Visible = false;
            //if (Session["UploadedFile"] != null)
            //    fileUpload = (FileUpload)Session["UploadedFile"];           

            try
            {
                if (!validate())
                    return;

                //if (fileUpload.HasFile)
                //{
                //    string FileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                //    string Extension = Path.GetExtension(fileUpload.PostedFile.FileName);
                //    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                //    if (Extension != ".txt")
                //    {
                //        errorMessage.Text = "The uploaded file format is invalid, Please upload .txt file only.";
                //        return;
                //    }
                //    string promoID = "Pro_" + System.DateTime.Now.Ticks.ToString();
                //    string FilePath = FolderPath + "\\" + promoID+".txt";
                //    fileUpload.SaveAs(FilePath);

                string colorbatch =rblcolorbatch.SelectedItem.Text;

                    string language = rbllanguage.SelectedItem.Text;

                
                string shortcode = ddlShortCode.SelectedItem.Value;
                string message = txtMessage.Text;
                string publishtime =DateTime.Parse(Request.Form[txtPublishTime.UniqueID]).ToString();

                    DataTable dtMsg = new DataTable();
                    dtMsg.Columns.AddRange(new DataColumn[5] { 
                            new DataColumn("Shortcode", typeof(string)),
                            //new DataColumn("PromoID", typeof(string)),
                            new DataColumn("PublishTime", typeof(DateTime)),
                            new DataColumn("ColorBatch",typeof(string)),
                            new DataColumn("Language",typeof(string)),
                            new DataColumn("message",typeof(string)) });
                    DataRow drM = dtMsg.NewRow();

                    drM["Shortcode"] = ddlShortCode.SelectedItem.Value;
                    //drM["PromoID"] = promoID;
                    drM["PublishTime"] = DateTime.Parse(Request.Form[txtPublishTime.UniqueID]);
                    drM["ColorBatch"] = colorbatch;
                    drM["Language"] = language;
                    drM["message"] = txtMessage.Text;
                    dtMsg.Rows.Add(drM);

                    BAL obj = new BAL(helpDeskDBConnStr);
                int res=obj.GetSMSPromoconfiguration(colorbatch, language, shortcode, message, publishtime);
                   

                    
                    ClearControls();
                if(res!=-1)
                {
                    errorMessage.ForeColor = Color.Green;
                    errorMessage.Text = "File Uploaded successfully";
                    divError.Visible = true;
                    errorMessage.Visible = true;
                }
                  else
                {
                    errorMessage.ForeColor = Color.Green;
                    errorMessage.Text = "File not Uploaded";
                    divError.Visible = true;
                    errorMessage.Visible = true;
                
            }

                    

                
                
             
            }
            catch (Exception ex)
            {
                errorMessage.Text = "Error occured: " + ex.Message; divError.Visible = true; errorMessage.Visible = true;

            }
        }

        private bool validate()
        {
            try
            {

                //if (ddlShortCode.SelectedItem.Text=="--Select--")
                //{
                //    errorMessage.Text = "Please select Shortcode"; divError.Visible = true; errorMessage.Visible = true;
                //    return false;
                //}

                if (string.IsNullOrEmpty(txtMessage.Text.Trim()))
                {
                    errorMessage.Text = "Please enter Message"; divError.Visible = true; errorMessage.Visible = true;
                    return false;
                }
                //if (!fileUpload.HasFile)
                //{
                //    errorMessage.Text = "Please upload MSISDN file"; divError.Visible = true; errorMessage.Visible = true;
                //    return false;
                //}
                if (string.IsNullOrEmpty(txtPublishTime.ClientID))
                {
                    errorMessage.Text = "Please enter Publish Time."; divError.Visible = true; errorMessage.Visible = true;
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                errorMessage.Text = "Validation failed: " + ex.Message.ToString(); divError.Visible = true; errorMessage.Visible = true;
                return false;

            }
        }
        private void ClearControls()
        {
            divError.Visible = false;
            ddlShortCode.SelectedIndex = 0;
            txtPublishTime.Text = "";
            txtMessage.Text = "";
            //fileUpload.Attributes.Clear();
        }
        public string Decrypt(string strDecrypt)
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
                byteBuff = Convert.FromBase64String(strDecrypt);
                string strDecrypted = ASCIIEncoding.ASCII.GetString(objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch
            {
                return "Wrong Password.";// +ex.Message;
            }
            finally
            {
                objDESCrypto = null;
                objHashMD5 = null;
            }
        }



        protected void lnkSignOut_Click(object sender, EventArgs e)
        {
            Session["Login_LoginID"] = null;
            Session["Login_LoginName"] = null;
            Response.Redirect("Index.aspx");
        }

       


        //protected void fileUpload_Load(object sender, EventArgs e)
        //{
        //    Session["UploadedFile"] = null;

        //    if (fileUpload.HasFile)
        //    {
        //        Session["UploadedFile"] = fileUpload;
        //        string FileName = Path.GetFileName(fileUpload.PostedFile.FileName);
        //        string Extension = Path.GetExtension(fileUpload.PostedFile.FileName);
        //        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
        //        if (Extension != ".txt")
        //        {
        //            errorMessage.Text = "The uploaded file format is invalid, Please upload .txt file only.";
        //            return;
        //        }
        //        string FilePath = FolderPath + "\\" + FileName;
        //        fileUpload.SaveAs(FilePath);

        //        int counter = 0;
        //        string line;


        //        // Read the file and display it line by line.
        //        System.IO.StreamReader file =
        //           new System.IO.StreamReader(FilePath);
        //        int lines = File.ReadAllLines(FilePath).Length;
        //        while ((line = file.ReadLine()) != null)
        //        {
        //           /// txtRecipients.Text = txtRecipients.Text + "\n" + line;
        //            counter++;
        //        }

        //        file.Close();
        //       // lblRecieps.Text = lines.ToString() + " Total Recipients";

        //    }
        //}

        //private void Import_To_Grid(string FilePath, string Extension, string FileName)
        //{
        //    string conStr = "";
        //    switch (Extension)
        //    {
        //        case ".xls": //Excel 97-03
        //            conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
        //                     .ConnectionString;
        //            break;
        //        case ".xlsx": //Excel 07
        //            conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
        //                      .ConnectionString;
        //            break;
        //    }
        //    conStr = String.Format(conStr, FilePath, "Yes");
        //    OleDbConnection connExcel = new OleDbConnection(conStr);
        //    OleDbCommand cmdExcel = new OleDbCommand();
        //    OleDbDataAdapter oda = new OleDbDataAdapter();
        //    DataTable dt = new DataTable();
        //    cmdExcel.Connection = connExcel;

        //    //Get the name of First Sheet
        //    connExcel.Open();
        //    DataTable dtExcelSchema;
        //    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        //    connExcel.Close();

        //    //Read Data from First Sheet
        //    connExcel.Open();
        //    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        //    oda.SelectCommand = cmdExcel;
        //    oda.Fill(dt);
        //    connExcel.Close();
        //    bool Check = true;

        //    if (Check == true)
        //    {
        //        FileName = FileName.Substring(0, FileName.IndexOf("."));
        //        BAL obj = new BAL(helpDeskDBConnStr);
        //        try
        //        {
        //            obj.InsertBulkRecords(dt);
        //            errorMessage.ForeColor = Color.Green;
        //            errorMessage.Text = "Sheet Uploaded to DB successfully";
        //            divError.Visible = true;
        //            errorMessage.Visible = true;

        //        }
        //        catch (Exception ex)
        //        {
        //            errorMessage.ForeColor = Color.Red;
        //            errorMessage.Text = "There is a problem in uploading sheet";
        //            divError.Visible = true;
        //            errorMessage.Visible = true;
        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }


        //}

        ////   string shortcode = ddlShortcode.Text.Trim();
        //        string keyword = "";
        //        int telcoid = 7;


        //        string messageText = txtMessage.Text.Trim();
        //        // string tslmethod = cmbTSLMethod.Text.Trim();
        //        string tslmethod = "delivermtmessage";
        //        string moMessageId = "BULK_" + ""; //Message ID which was present in the MO message.
        //        int applicationid = 0;
        //        string price = "";
        //        string billServiceId = "";
        //        string chargeId = "";
        //        string chargeMsisdn = "";
        //        string actUrl = "";
        //        string ua = "";

        //        int messageType = 0; // "0 for english, 5 for arabic and hex content";
        //        string userName = "";
        //        string password = "";
        //        int serviceType = 0;
        //        string additionalInformation = "<AI><SA>" + "" + "</SA></AI>";  //short code alias name

        //        string mTMessageId = new Random().Next(1000000).ToString(); //Unique random number
        //        string[] receiverMsisdn = msisdns.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries); //Regex.Split(txtMSISDN.Text.Trim(),","; errorMessage.Visible=true;
        //        string senderMsisdn = receiverMsisdn[0];
        //        int mtType = 2; //Keep 2 while testing because no MO message ID will be there during testing.
        //        short messageScheduleFlag = 0;
        //        string messageScheduleTime = DateTime.Now.ToString("yyyyMMddHHmmss");  //YYYYMMDDHHMMSS
        //        int priority = 0;

        //        using (SqlConnection con = new SqlConnection(helpDeskDBConnStr))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("atsl_gettslparams", con))
        //            {
        //                cmd.Parameters.AddWithValue("@Shortcode", "");
        //                cmd.Parameters.AddWithValue("@Keyword", keyword);
        //                cmd.Parameters.AddWithValue("@TelcoID", telcoid);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                {
        //                    DataTable dt = new DataTable();
        //                    da.Fill(dt);
        //                    if (dt.Rows.Count > 0)
        //                    {
        //                        applicationid = Convert.ToInt32(dt.Rows[2][0].ToString().Split(':')[1].Trim());
        //                        userName = dt.Rows[3][0].ToString().Split(':')[1].Trim();
        //                        password = Decrypt(dt.Rows[4][0].ToString().Trim());
        //                        price = dt.Rows[5][0].ToString().Split(':')[1].Trim();
        //                        //moMessageId = dt.Rows[6][0].ToString().Split(':')[1].Trim(); 
        //                        serviceType = Convert.ToInt32(dt.Rows[14][0].ToString().Split(':')[1].Trim());
        //                        messageScheduleFlag = Convert.ToInt16(dt.Rows[15][0].ToString().Split(':')[1].Trim());
        //                        billServiceId = dt.Rows[19][0].ToString().Split(':')[1].Trim();
        //                        priority = Convert.ToInt32(dt.Rows[20][0].ToString().Split(':')[1].Trim());

        //                        //using (VasSender.VasSenderClient objService = new VasSender.VasSenderClient("Basic"))
        //                        //{
        //                        //    string result = "";
        //                        //    if (tslmethod.ToLower() == "delivermtmessage")
        //                        //    {
        //                        //        //result = objService.deliverMTMessage(applicationid, userName, password, price, moMessageId, mTMessageId, telcoid, receiverMsisdn
        //                        //        //      , senderMsisdn, messageType, mtType, messageText, serviceType, messageScheduleFlag, messageScheduleTime, additionalInformation, shortcode, billServiceId, priority);
        //                        //        result = retriveTSLResponseDesc(result, "delivermtmessage");
        //                        //    }
        //                        //    else if (tslmethod.ToLower() == "charge")
        //                        //    {
        //                        //        result = objService.charge(applicationid, price, billServiceId, chargeId, chargeMsisdn, messageType, telcoid, actUrl, ua, userName, password,
        //                        //            shortcode, serviceType, additionalInformation, moMessageId);
        //                        //        result = retriveTSLResponseDesc(result, "charge");
        //                        //    }
        //                        //    else if (tslmethod.ToLower() == "chargerenewal")
        //                        //    {
        //                        //        result = objService.chargeRenewal(applicationid, price, billServiceId, chargeId, chargeMsisdn, messageType, telcoid, actUrl, ua, userName, password,
        //                        //            shortcode, serviceType, additionalInformation, moMessageId);
        //                        //        result = retriveTSLResponseDesc(result, "charge");
        //                        //    }
        //                        //    else
        //                        //    {
        //                        //        errorMessage.Text = "Please select valid method."; divError.Visible = true; errorMessage.Visible = true;

        //                        //    }
        //                        //    errorMessage.Text = result; divError.Visible = true; divError.Attributes.Add("class", "b-login-form-success"); errorMessage.Visible = true;

        //                        //}
        //                    }
        //                    else
        //                    {
        //                        errorMessage.Text = "No Records found with this shortcode and keyword combination."; divError.Visible = true; errorMessage.Visible = true;
        //                        return;
        //                    }
        //                }
        //            }
        //        }

    }
}