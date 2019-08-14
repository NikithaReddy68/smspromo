using HelpDeskCore;
using LogDll;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fcc_promo_sms
{
    public partial class spreps : System.Web.UI.Page
    {
        private string helpDeskDBConnStr = DBLib.DBLibProcessor.GetCPGDBConString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = "";
                txtFromDate.Text = "";
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
                string dtF = Request.Form[txtFromDate.UniqueID];
                string dtT = Request.Form[txtToDate.UniqueID];
                if (!string.IsNullOrEmpty(dtF) && !string.IsNullOrEmpty(dtT))
                {
                    Request.Form[txtFromDate.UniqueID] = DateTime.Now.ToShortDateString();
                    Request.Form[txtToDate.UniqueID] = DateTime.Now.ToShortDateString();
                }
                LoadValues();

            }
           

        }
        protected void lnkBtnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        private void ClearControls()
        {
            divError.Visible = false;
            ddlService.SelectedIndex = 0;
            txtFromDate.Attributes.Clear();
            txtToDate.Attributes.Clear();

        }
        private void LoadValues()
        {
                   
            LoadServices();
            LoadData();
        }
        private void LoadServices()
        {
            int telcoid = Convert.ToInt32(ConfigurationManager.AppSettings["MI_OperatorID"]);
            int cpid = Convert.ToInt32(Session["Login_LoginID"]);
            BAL objBL = new BAL(helpDeskDBConnStr);
            ddlService.Items.Clear();
            ddlService.DataTextField = "Subscription_Name";
            ddlService.DataValueField = "Subscription_Id";

            ddlService.DataSource = objBL.GetServicesByCPID(telcoid, cpid);
            ddlService.DataBind();

            ddlService.ClearSelection();
            if (ddlService.Items.Count > 0)
            {
                ddlService.Enabled = true;

            }
            else
                ddlService.Text = "No Service Exist";
        }
        private void LoadData()
        {
            divError.Visible = false;
            errorMessage.Visible = false;
            txtFromDate.Text = "";
            txtFromDate.Text = "";
            
            int telcoid = Convert.ToInt32(ConfigurationManager.AppSettings["MI_OperatorID"]);
            int cpid = Convert.ToInt32(Session["Login_LoginID"]);
            BAL objBL = new BAL(helpDeskDBConnStr);
            string dtF = Request.Form[txtFromDate.UniqueID];
            string dtT = Request.Form[txtToDate.UniqueID];
            if (!string.IsNullOrEmpty(dtF) && !string.IsNullOrEmpty(dtT))
            {
                txtFromDate.Text = dtF;
                txtToDate.Text = dtT;
                Log.Write("TelcoID " + telcoid + " cpid: " + cpid + "ddlService.SelectedValue" + ddlService.SelectedValue.ToString()+"fromDate: "+dtF+"ToDate: "+dtT);
                gvData.DataSource = objBL.GetCallBacks(telcoid, cpid, ddlService.SelectedValue.ToString(), dtF, dtT);
                gvData.DataBind();
            }
            else
            {
                divError.Visible = true;
                errorMessage.Text = "Please select FROM and TO date";
                errorMessage.ForeColor = Color.Red;
                errorMessage.Visible = true;
            }
            
            if (gvData.Rows.Count > 0)
                lnkExport.Visible = true;
            
        }

        protected void lnkBtnSubmit_Click(object sender, EventArgs e)
        {

            LoadData();
        }
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
           
            string FileName = Session["Login_LoginName"].ToString() + "_CallBacks" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvData.GridLines = GridLines.Both;
            gvData.HeaderStyle.Font.Bold = true;
            gvData.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void lnkExport_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

    }
}