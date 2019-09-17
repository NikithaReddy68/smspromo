using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;
using IntigralHelpDeskCore.BO;


namespace HelpDeskCore
{
    public class DAL
    {
        private string connectionString = string.Empty;

        public DAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int DeletePostpaidNumbers(string PostpaidNumbers, int DeletedBy)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@PostpaidNumbers", PostpaidNumbers);
                sqlParam[0].DbType = DbType.String;
                sqlParam[0].Size = -1;
                sqlParam[0].SqlDbType = SqlDbType.NVarChar;
                sqlParam[1] = new SqlParameter("@DeletedBy", SqlDbType.Int);
                sqlParam[1].Value = DeletedBy;

                return SqlHelper.ExecuteNonQuery("HelpDesk_Delete_PostpaidNumbers", ref sqlParam, connectionString);
            }
        }
        public DataSet GetServiceDetails(string shortcode, string keyword)
        {
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@shortcode", SqlDbType.VarChar);
            sqlParam[0].Value = shortcode;
            sqlParam[1] = new SqlParameter("@keyword", SqlDbType.VarChar);
            sqlParam[1].Value = keyword;
            return SqlHelper.ExecuteDataSet("HelpDesk_GetServiceDetails", ref sqlParam, connectionString);
        }

        public int UpdateServiceName(int telcoId, string shortcode, int serviceid, string servicename)
        {
            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@TelcoID", SqlDbType.Int);
            sqlParam[0].Value = telcoId;
            sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.NVarChar);
            sqlParam[1].Value = shortcode;
            sqlParam[2] = new SqlParameter("@serviceid", SqlDbType.Int);
            sqlParam[2].Value = serviceid;
            sqlParam[3] = new SqlParameter("@servicename", SqlDbType.NVarChar);
            sqlParam[3].Value = servicename;
            sqlParam[4] = new SqlParameter("@Status", SqlDbType.Int);
            //sqlParam[4].Value =0;
            sqlParam[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery("HelpDesk_Update_ServiceName", ref sqlParam, connectionString);
            return Convert.ToInt32(sqlParam[4].Value);
        }

        public int UpdateBulkServiceCreationStatus(int id, string response, bool isProcessed)
        {
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@DataID", SqlDbType.Int);
            sqlParam[0].Value = id;
            sqlParam[1] = new SqlParameter("@Response", response);
            sqlParam[1].DbType = DbType.String;
            sqlParam[1].Size = -1;
            sqlParam[2] = new SqlParameter("@IsProcessed", SqlDbType.Bit);
            sqlParam[2].Value = isProcessed;

            return SqlHelper.ExecuteNonQuery("SPRT_HelpDesk_UpdateBulkServiceTableDataWithResponse", ref sqlParam, connectionString);

        }

        public int UpdateServicePrice(int telcoId, string shortcode, int serviceid, string serviceSourceType, string Price)
        {
            SqlParameter[] sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@TelcoID", SqlDbType.Int);
            sqlParam[0].Value = telcoId;
            sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.NVarChar);
            sqlParam[1].Value = shortcode;
            sqlParam[2] = new SqlParameter("@serviceid", SqlDbType.Int);
            sqlParam[2].Value = serviceid;
            sqlParam[3] = new SqlParameter("@serviceSourceType", SqlDbType.NVarChar);
            sqlParam[3].Value = serviceSourceType;
            sqlParam[4] = new SqlParameter("@Price", SqlDbType.NVarChar);
            sqlParam[4].Value = Price;
            sqlParam[5] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParam[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery("HelpDesk_Update_ServicePrice", ref sqlParam, connectionString);
            return Convert.ToInt32(sqlParam[5].Value);
        }

        public int SaveCommonLookupData(int telcoId, string shortcode, int serviceid, string serviceSourceType, BillingInfo[] BillServiceId, int LookupID)
        {
            SqlParameter[] sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@TelcoID", SqlDbType.Int);
            sqlParam[0].Value = telcoId;
            sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.NVarChar);
            sqlParam[1].Value = shortcode;
            sqlParam[2] = new SqlParameter("@serviceid", SqlDbType.Int);
            sqlParam[2].Value = serviceid;
            sqlParam[3] = new SqlParameter("@serviceSourceType", SqlDbType.NVarChar);
            sqlParam[3].Value = serviceSourceType;
            sqlParam[4] = new SqlParameter("@BillServiceId", SqlDbType.NVarChar);
            sqlParam[4].Value = GetBillingInfoXMLString(BillServiceId, LookupID);
            sqlParam[5] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParam[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery("HelpDesk_Update_BillServiceId", ref sqlParam, connectionString);
            return Convert.ToInt32(sqlParam[5].Value);
        }
        public string GetBillingInfoXMLString(BillingInfo[] BillingInfoList, int LookupID)
        {
            bool zeroPriceIncluded = false;

            string strAutoCode = "[PRICECODE]";
            if (LookupID > 0)
                strAutoCode = LookupID.ToString();

            XmlDocument xmlContent = new XmlDocument();
            XmlElement node = xmlContent.CreateElement("BILLING");
            xmlContent.AppendChild(node);
            if (BillingInfoList != null)
            {
                foreach (BillingInfo obj in BillingInfoList)
                {
                    XmlElement nodeChild = xmlContent.CreateElement("PRICE");

                    if (obj.Price == 0)
                    {
                        zeroPriceIncluded = true;

                        XmlAttribute attr1 = xmlContent.CreateAttribute("Value");
                        attr1.Value = "0";
                        nodeChild.Attributes.Append(attr1);

                        XmlAttribute attr2 = xmlContent.CreateAttribute("Code");
                        attr2.Value = "0-" + strAutoCode;
                        nodeChild.Attributes.Append(attr2);

                        XmlElement nodeSubChild1 = xmlContent.CreateElement("PREPAIDCODE");
                        nodeSubChild1.InnerText = "0";
                        nodeChild.AppendChild(nodeSubChild1);

                        XmlElement nodeSubChild2 = xmlContent.CreateElement("PREPAIDDESCRIPTION");
                        nodeSubChild2.InnerText = string.Empty;
                        nodeChild.AppendChild(nodeSubChild2);

                        XmlElement nodeSubChild3 = xmlContent.CreateElement("POSTPAIDCODE");
                        nodeSubChild3.InnerText = "0";
                        nodeChild.AppendChild(nodeSubChild3);

                        XmlElement nodeSubChild4 = xmlContent.CreateElement("POSTPAIDDESCRIPTION");
                        nodeSubChild4.InnerText = string.Empty;
                        nodeChild.AppendChild(nodeSubChild4);
                    }
                    else
                    {

                        XmlAttribute attr1 = xmlContent.CreateAttribute("Value");
                        attr1.Value = obj.Price.ToString();
                        nodeChild.Attributes.Append(attr1);

                        XmlAttribute attr2 = xmlContent.CreateAttribute("Code");
                        attr2.Value = GetPriceCode(obj.Price, strAutoCode);
                        nodeChild.Attributes.Append(attr2);

                        if (obj.PrepaidCode == null)
                            obj.PrepaidCode = string.Empty;
                        if (obj.PrepaidDescription == null)
                            obj.PrepaidDescription = string.Empty;
                        if (obj.PostpaidCode == null)
                            obj.PostpaidCode = string.Empty;
                        if (obj.PostpaidDescription == null)
                            obj.PostpaidDescription = string.Empty;

                        XmlElement nodeSubChild1 = xmlContent.CreateElement("PREPAIDCODE");
                        nodeSubChild1.InnerText = obj.PrepaidCode;
                        nodeChild.AppendChild(nodeSubChild1);

                        XmlElement nodeSubChild2 = xmlContent.CreateElement("PREPAIDDESCRIPTION");
                        nodeSubChild2.InnerText = obj.PrepaidDescription;
                        nodeChild.AppendChild(nodeSubChild2);

                        XmlElement nodeSubChild3 = xmlContent.CreateElement("POSTPAIDCODE");
                        nodeSubChild3.InnerText = obj.PostpaidCode;
                        nodeChild.AppendChild(nodeSubChild3);

                        XmlElement nodeSubChild4 = xmlContent.CreateElement("POSTPAIDDESCRIPTION");
                        nodeSubChild4.InnerText = obj.PostpaidDescription;
                        nodeChild.AppendChild(nodeSubChild4);
                    }
                    node.AppendChild(nodeChild);
                }
            }

            if (!zeroPriceIncluded)
            {
                XmlElement nodeChild = xmlContent.CreateElement("PRICE");

                zeroPriceIncluded = true;

                XmlAttribute attr1 = xmlContent.CreateAttribute("Value");
                attr1.Value = "0";
                nodeChild.Attributes.Append(attr1);

                XmlAttribute attr2 = xmlContent.CreateAttribute("Code");
                attr2.Value = "0-" + strAutoCode;
                nodeChild.Attributes.Append(attr2);

                XmlElement nodeSubChild1 = xmlContent.CreateElement("PREPAIDCODE");
                nodeSubChild1.InnerText = "0";
                nodeChild.AppendChild(nodeSubChild1);

                XmlElement nodeSubChild2 = xmlContent.CreateElement("PREPAIDDESCRIPTION");
                nodeSubChild2.InnerText = string.Empty;
                nodeChild.AppendChild(nodeSubChild2);

                XmlElement nodeSubChild3 = xmlContent.CreateElement("POSTPAIDCODE");
                nodeSubChild3.InnerText = "0";
                nodeChild.AppendChild(nodeSubChild3);

                XmlElement nodeSubChild4 = xmlContent.CreateElement("POSTPAIDDESCRIPTION");
                nodeSubChild4.InnerText = string.Empty;
                nodeChild.AppendChild(nodeSubChild4);

                node.AppendChild(nodeChild);
            }
            return xmlContent.InnerXml;
        }
        public string GetPriceCode(decimal Price, string LookupID)
        {
            return Convert.ToInt32((Convert.ToDecimal(Price.ToString("0.00")) * 100)).ToString() + "-" + LookupID;
        }
        public decimal GetXMLAttributeValue_Decimal(XmlAttribute attr)
        {
            try
            {
                if (attr == null)
                    return 0;

                if (string.IsNullOrEmpty(attr.Value))
                    return 0;

                return Convert.ToDecimal(attr.Value.Trim());
            }
            catch
            {
                return 0;
            }
        }
        public string GetXMLAttributeValue_String(XmlAttribute attr)
        {
            try
            {
                if (attr == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(attr.Value))
                    return string.Empty;

                return attr.Value;
            }
            catch
            {
                return string.Empty;
            }
        }
        public BillingInfo[] GetBillingInfoList(string xmlContent)
        {
            bool dataCollected = false;

            List<BillingInfo> listBillingInfo = new List<BillingInfo>();

            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.LoadXml(xmlContent.Trim());
            }
            catch (Exception ex)
            {
                return null;
            }

            XmlNode mainNode = xmldoc.SelectSingleNode(@"BILLING");

            if (mainNode != null)
            {
                if (mainNode.HasChildNodes)
                {
                    foreach (XmlNode ctlNode in mainNode.ChildNodes)
                    {
                        BillingInfo obj = new BillingInfo();
                        try
                        {
                            obj.Price = GetXMLAttributeValue_Decimal(ctlNode.Attributes["Value"]);
                            obj.PriceCode = GetXMLAttributeValue_String(ctlNode.Attributes["Code"]);

                            XmlNode ctlNode1 = ctlNode.SelectSingleNode(@"PREPAIDCODE");
                            XmlNode ctlNode2 = ctlNode.SelectSingleNode(@"PREPAIDDESCRIPTION");
                            XmlNode ctlNode3 = ctlNode.SelectSingleNode(@"POSTPAIDCODE");
                            XmlNode ctlNode4 = ctlNode.SelectSingleNode(@"POSTPAIDDESCRIPTION");
                            if ((ctlNode1 != null) && (ctlNode2 != null) && (ctlNode3 != null) && (ctlNode4 != null))
                            {
                                obj.PrepaidCode = ctlNode1.InnerText;
                                obj.PrepaidDescription = ctlNode2.InnerText;
                                obj.PostpaidCode = ctlNode3.InnerText;
                                obj.PostpaidDescription = ctlNode4.InnerText;
                                dataCollected = true;
                            }
                        }
                        catch (Exception ex1)
                        {
                            dataCollected = false;
                        }
                        if (dataCollected)
                        {
                            listBillingInfo.Add(obj);
                            dataCollected = false;
                        }
                    }
                }
            }

            BillingInfo[] listBillingInfoArr = new BillingInfo[0];
            if (listBillingInfo.Count > 0)
            {
                listBillingInfoArr = new BillingInfo[listBillingInfo.Count];
                for (int i = 0; i < listBillingInfo.Count; i++)
                {
                    listBillingInfoArr[i] = listBillingInfo[i];
                }
            }

            return listBillingInfoArr;
        }

        public int UpdateKeyword(string serviceid, string shortcode, string servicename)
        {

            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@serviceid", SqlDbType.VarChar);
            sqlParam[0].Value = serviceid;
            sqlParam[1] = new SqlParameter("@shortcode", SqlDbType.VarChar);
            sqlParam[1].Value = shortcode;
            sqlParam[2] = new SqlParameter("@keyword", SqlDbType.NVarChar);
            sqlParam[2].Value = shortcode;
            return SqlHelper.ExecuteNonQuery("HelpDesk_Update_Keyword", ref sqlParam, connectionString);
        }

        public int Unsubscribeusers(int TelcoID, int Shortcode, string MSISDN, int ServiceID, out int PremiumUnsubCount, out int NonPremiumUnsubCount)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@TelcoID", SqlDbType.Int);
                sqlParam[0].Value = TelcoID;
                sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.BigInt);
                sqlParam[1].Value = Shortcode;
                sqlParam[2] = new SqlParameter("@MSISDN", SqlDbType.NVarChar, -1);
                sqlParam[2].Value = MSISDN;
                sqlParam[3] = new SqlParameter("@ServiceID", SqlDbType.BigInt);
                sqlParam[3].Value = ServiceID;
                sqlParam[4] = new SqlParameter("@EffectedRows", SqlDbType.Int);
                sqlParam[4].Direction = ParameterDirection.Output;
                sqlParam[5] = new SqlParameter("@PremiumUnsubCount", SqlDbType.Int);
                sqlParam[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery("HelpDesk_UnsubscribeServicesByMSISDN", ref sqlParam, connectionString);
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                int TotalEffectedCount = Convert.ToInt32(sqlParam[4].Value);
                PremiumUnsubCount = Convert.ToInt32(sqlParam[5].Value);
                NonPremiumUnsubCount = TotalEffectedCount - PremiumUnsubCount;
                return TotalEffectedCount;
            }
        }

        public int InsertPostpaidNumbers(string PostpaidNumbers, bool DeleteAllExisting, int AddedBy)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@PostpaidNumbers", PostpaidNumbers);
                sqlParam[0].DbType = DbType.String;
                sqlParam[0].Size = -1;
                sqlParam[0].SqlDbType = SqlDbType.NVarChar;
                sqlParam[1] = new SqlParameter("@AddedBy", SqlDbType.Int);
                sqlParam[1].Value = AddedBy;

                return SqlHelper.ExecuteNonQuery("HelpDesk_Insert_PostpaidNumbers", ref sqlParam, connectionString);
            }
        }

        public int AddToFreeNumber(int loginId, string mobileNumber, int telcoId, string shortcode, int billserviceId)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@LoginId", SqlDbType.Int);
                sqlParam[0].Value = loginId;
                sqlParam[1] = new SqlParameter("@MSISDN", SqlDbType.VarChar);
                sqlParam[1].Value = mobileNumber;
                sqlParam[2] = new SqlParameter("@TelcoId", SqlDbType.Int);
                sqlParam[2].Value = telcoId;
                sqlParam[3] = new SqlParameter("@shortcode", SqlDbType.VarChar);
                sqlParam[3].Value = shortcode;
                sqlParam[4] = new SqlParameter("@billserviceId", SqlDbType.Int);
                sqlParam[4].Value = billserviceId;
                result = SqlHelper.ExecuteNonQuery("HelpDesk_Add_FreeNumbers", ref sqlParam, connectionString);
            }
            return result;
        }

        public int DeleteFromFreeNumber(int loginId, string mobileNumber, int telcoId, string shortcode, int billserviceId)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@LoginId", SqlDbType.Int);
                sqlParam[0].Value = loginId;
                sqlParam[1] = new SqlParameter("@MSISDN", SqlDbType.VarChar);
                sqlParam[1].Value = mobileNumber;
                sqlParam[2] = new SqlParameter("@TelcoId", SqlDbType.Int);
                sqlParam[2].Value = telcoId;
                sqlParam[3] = new SqlParameter("@shortcode", SqlDbType.VarChar);
                sqlParam[3].Value = shortcode;
                sqlParam[4] = new SqlParameter("@billserviceId", SqlDbType.Int);
                sqlParam[4].Value = billserviceId;
                result = SqlHelper.ExecuteNonQuery("HelpDesk_Delete_FreeNumbers", ref sqlParam, connectionString);
            }
            return result;
        }

        public List<UserDetailsBO> GetLoginNames()
        {
            DataTable dtLoginNames = new DataTable();
            List<UserDetailsBO> lstUserDetails = new List<UserDetailsBO>();
            dtLoginNames = SqlHelper.ExecuteDataTable("HelpDesk_GetLoginNames", connectionString);
            if (dtLoginNames != null && dtLoginNames.Rows.Count > 0)
            {
                foreach (DataRow row in dtLoginNames.Rows)
                {
                    UserDetailsBO objUserBO = new UserDetailsBO();
                    objUserBO.LoginName = row["LoginName"].ToString();
                    objUserBO.CPGLoginID = Convert.ToInt32(row["LoginID"].ToString());
                    lstUserDetails.Add(objUserBO);
                }
            }
            return lstUserDetails;
        }

        public List<UserDetailsBO> GetCPGUserDetails(string loginName)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@LoginName", SqlDbType.VarChar);
            sqlParam[0].Value = loginName;
            DataTable dtLoginNames = new DataTable();
            List<UserDetailsBO> lstUserDetails = new List<UserDetailsBO>();
            dtLoginNames = SqlHelper.ExecuteDataTable("HelpDesk_GetUserDetails", ref sqlParam, connectionString);
            if (dtLoginNames != null && dtLoginNames.Rows.Count > 0)
            {
                foreach (DataRow row in dtLoginNames.Rows)
                {
                    UserDetailsBO objUserBO = new UserDetailsBO();
                    objUserBO.AcctManager = row["Acct_Manager"].ToString();
                    objUserBO.AccManager_ID = (row["AE_ID"] != null) ? Convert.ToInt32(row["AE_ID"].ToString()) : 0;
                    objUserBO.CEAdmin = row["CEAdmin"].ToString();
                    objUserBO.CEAdmin_ID = (row["CE_ID"] != null) ? Convert.ToInt32(row["CE_ID"].ToString()) : 0;
                    objUserBO.VaultLoginID = (row["VaultLoginID"] != null) ? Convert.ToInt32(row["VaultLoginID"].ToString()) : 0;
                    objUserBO.CompanyName = row["CompanyName"].ToString();
                    objUserBO.Contact_Mobile = row["Contact_Mobile"].ToString();
                    objUserBO.CPGLoginID = (row["CPGLoginID"] != null) ? Convert.ToInt32(row["CPGLoginID"].ToString()) : 0;
                    objUserBO.EmailID = row["EmailID"].ToString();
                    objUserBO.FirstName = row["FirstName"].ToString();
                    objUserBO.LastName = row["LastName"].ToString();
                    objUserBO.LockId = (row["LockStatusID"] != null) ? Convert.ToInt32(row["LockStatusID"].ToString()) : 0;
                    objUserBO.LockStatus = row["LockStatus"].ToString();
                    objUserBO.LoginName = row["LoginName"].ToString();
                    objUserBO.Password = row["Password"].ToString();
                    objUserBO.UPM_AppID = (row["UPM_Appid"] != null) ? Convert.ToInt32(row["UPM_Appid"].ToString()) : 0;
                    objUserBO.UserRole = row["UserRole"].ToString();
                    objUserBO.RoleID = (row["RoleID"] != null) ? Convert.ToInt32(row["RoleID"].ToString()) : 0;
                    objUserBO.WFStatus = row["WFStatus"].ToString();
                    objUserBO.APIPassword = row["APIpassword"].ToString();
                    if (string.IsNullOrEmpty(row["SendPinBySMS"].ToString()))
                        objUserBO.SendPinBySMS = false;
                    else
                        objUserBO.SendPinBySMS = Convert.ToBoolean(row["SendPinBySMS"]);

                    if (string.IsNullOrEmpty(row["SendPinByEmail"].ToString()))
                        objUserBO.SendPinByEmail = false;
                    else
                        objUserBO.SendPinByEmail = Convert.ToBoolean(row["SendPinByEmail"]);


                    lstUserDetails.Add(objUserBO);

                }
            }

            return lstUserDetails;
        }

        public UserDetailsBO UpdateUserDetails(string loginName, string mobilenumber, string email, string password, string newloginName, string APIPassword, string firstName, string lastName, string companyName, bool SendPinBySMS, bool SendPinByEmail)
        {
            UserDetailsBO UserDetails = new UserDetailsBO();
            SqlDataReader dr;

            //int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@USERNAME", SqlDbType.VarChar);
                sqlParam[0].Value = loginName;
                sqlParam[1] = new SqlParameter("@MobileNumber", SqlDbType.VarChar);
                sqlParam[1].Value = mobilenumber;
                sqlParam[2] = new SqlParameter("@PrimaryEmail", SqlDbType.VarChar);
                sqlParam[2].Value = email;
                sqlParam[3] = new SqlParameter("@NEWEncryptedPAssword", SqlDbType.VarChar);
                sqlParam[3].Value = password;
                sqlParam[4] = new SqlParameter("@NewUserName", SqlDbType.VarChar);
                sqlParam[4].Value = newloginName;
                sqlParam[5] = new SqlParameter("@NewAPIPassword", SqlDbType.VarChar);
                sqlParam[5].Value = APIPassword;
                sqlParam[6] = new SqlParameter("@FirstName", SqlDbType.NVarChar);
                sqlParam[6].Value = firstName;
                sqlParam[7] = new SqlParameter("@LastName", SqlDbType.NVarChar);
                sqlParam[7].Value = lastName;
                sqlParam[8] = new SqlParameter("@CompanyName", SqlDbType.NVarChar);
                sqlParam[8].Value = companyName;
                sqlParam[9] = new SqlParameter("@SendPinBySMS", SqlDbType.Bit);
                sqlParam[9].Value = SendPinBySMS;
                sqlParam[10] = new SqlParameter("@SendPinByEmail", SqlDbType.Bit);
                sqlParam[10].Value = SendPinByEmail;

                //result = SqlHelper.ExecuteNonQuery("HelpDesk_UPDATE_USER_INFO", ref sqlParam, connectionString);
                dr = SqlHelper.ExecuteReader("HelpDesk_UPDATE_USER_INFO", ref sqlParam, connectionString);
            }

            if (dr.HasRows)
                while (dr.Read())
                {
                    UserDetails.AcctManager = dr["Acct_Manager"].ToString();
                    UserDetails.AccManager_ID = Convert.ToInt32(dr["AE_ID"].ToString());
                    UserDetails.CEAdmin = dr["CEAdmin"].ToString();
                    UserDetails.CEAdmin_ID = Convert.ToInt32(dr["CE_ID"].ToString());
                    UserDetails.VaultLoginID = Convert.ToInt32(dr["VaultLoginID"].ToString());
                    UserDetails.CompanyName = dr["CompanyName"].ToString();
                    UserDetails.Contact_Mobile = dr["Contact_Mobile"].ToString();
                    UserDetails.CPGLoginID = Convert.ToInt32(dr["CPGLoginID"].ToString());
                    UserDetails.EmailID = dr["EmailID"].ToString();
                    UserDetails.FirstName = dr["FirstName"].ToString();
                    UserDetails.LastName = dr["LastName"].ToString();
                    UserDetails.LockId = Convert.ToInt32(dr["LockStatusID"].ToString());
                    UserDetails.LockStatus = dr["LockStatus"].ToString();
                    UserDetails.LoginName = dr["LoginName"].ToString();
                    UserDetails.Password = dr["Password"].ToString();
                    UserDetails.UPM_AppID = Convert.ToInt32(dr["UPM_Appid"].ToString());
                    UserDetails.UserRole = dr["UserRole"].ToString();
                    UserDetails.RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                    UserDetails.WFStatus = dr["WFStatus"].ToString();
                    UserDetails.APIPassword = dr["APIpassword"].ToString();

                    if (string.IsNullOrEmpty(dr["SendPinBySMS"].ToString()))
                        UserDetails.SendPinBySMS = false;
                    else
                        UserDetails.SendPinBySMS = Convert.ToBoolean(dr["SendPinBySMS"]);

                    if (string.IsNullOrEmpty(dr["SendPinByEmail"].ToString()))
                        UserDetails.SendPinByEmail = false;
                    else
                        UserDetails.SendPinByEmail = Convert.ToBoolean(dr["SendPinByEmail"]);


                }
            return UserDetails;

        }
        public List<ChangeSubscriberServicesBO> GetkeywordsByServiceID(int serviceid)
        {
            List<ChangeSubscriberServicesBO> lstkeywords = new List<ChangeSubscriberServicesBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetKeywordsBySubscriptionID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ServiceID", serviceid);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ChangeSubscriberServicesBO objkeywords = new ChangeSubscriberServicesBO();
                    objkeywords.Keyword_Id = dr["Keyword_Id"].ToString();
                    objkeywords.Keyword = dr["Keyword"].ToString();
                    lstkeywords.Add(objkeywords);
                }
            }
            connection.Close();
            return lstkeywords;
        }
        public string GetLookupkeywordsByServiceID(int telcoId, string shortcode, int serviceId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetLookupKeywordsBySubscriptionID", connection);

            string keywordsLookupId = string.Empty;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TelcoId", telcoId);
            cmd.Parameters.AddWithValue("@Shortcode", shortcode);
            cmd.Parameters.AddWithValue("@ServiceID", serviceId);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    keywordsLookupId = dr["Keyword"].ToString() + "|" + dr["LookupId"].ToString();
                }
            }
            connection.Close();
            return keywordsLookupId;
        }
        public string GetInUseCommonLookupKeywords(string ShortCode, string Keywords, int TelcoID, int LookupID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("CPG_GetInUseCommonLookupKeywords", connection);

            string InUsekeywords = string.Empty;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShortCode", ShortCode);
            cmd.Parameters.AddWithValue("@Keywords", Keywords);
            cmd.Parameters.AddWithValue("@TelcoId", TelcoID);
            cmd.Parameters.AddWithValue("@LookupID", LookupID);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    InUsekeywords = dr[0].ToString();
                }
            }
            connection.Close();
            return InUsekeywords;
        }

        public int InsertNewSubscriptionKeywords(string TotKeywords, string newKeyword, int lookupId, int subscriptionid, int languageid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@Keywords", TotKeywords);
                sqlParam[0].Value = TotKeywords;
                sqlParam[1] = new SqlParameter("@newKeyword", SqlDbType.NVarChar);
                sqlParam[1].Value = newKeyword;
                sqlParam[2] = new SqlParameter("@lookupid", SqlDbType.BigInt);
                sqlParam[2].Value = lookupId;
                sqlParam[3] = new SqlParameter("@SubsriptionId", SqlDbType.BigInt);
                sqlParam[3].Value = subscriptionid;
                sqlParam[4] = new SqlParameter("@languageId", SqlDbType.Int);
                sqlParam[4].Value = languageid;

                return SqlHelper.ExecuteNonQuery("HelpDesk_InsertNewSubscriptionkeywords", ref sqlParam, connectionString);
            }
        }

        public List<ChangeSubscriberServicesBO> GetServicesByShortcodes(int telcoid, int shortcode)
        {
            List<ChangeSubscriberServicesBO> lstservices = new List<ChangeSubscriberServicesBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetSubscriptionServicesByShortcode", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TelcoID", telcoid);
            cmd.Parameters.AddWithValue("@Shortcode", shortcode);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ChangeSubscriberServicesBO objservices = new ChangeSubscriberServicesBO();
                    objservices.Subscription_Id = dr["Subscription_Id"].ToString();
                    objservices.Subscription_Name = dr["Subscription_Name"].ToString();
                    lstservices.Add(objservices);
                }
            }
            connection.Close();
            return lstservices;
        }
        public List<ChangeSubscriberServicesBO> GetServicesByCPID(int telcoid, int CPID)
        {
            List<ChangeSubscriberServicesBO> lstservices = new List<ChangeSubscriberServicesBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetSubscriptionServicesByCPID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TelcoID", telcoid);
            cmd.Parameters.AddWithValue("@CPID", CPID);

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ChangeSubscriberServicesBO objservices = new ChangeSubscriberServicesBO();
                    objservices.Subscription_Id = dr["Subscription_Id"].ToString();
                    objservices.Subscription_Name = dr["Subscription_Name"].ToString();
                    lstservices.Add(objservices);
                }
            }
            connection.Close();
            return lstservices;
        }
        public DataSet GetCallBacks(int telcoid, int CPID, string serviceID, string fromDate, string ToDate)
        {
            
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@TelcoID", SqlDbType.Int);
                sqlParam[0].Value = telcoid;
                sqlParam[1] = new SqlParameter("@CPID", SqlDbType.Int);
                sqlParam[1].Value = CPID;
                sqlParam[2] = new SqlParameter("@serviceID", SqlDbType.VarChar,10);
                sqlParam[2].Value = serviceID;
                sqlParam[3] = new SqlParameter("@fromDate", SqlDbType.VarChar, 100);
                sqlParam[3].Value = fromDate;
                sqlParam[4] = new SqlParameter("@ToDate", SqlDbType.VarChar, 100);
                sqlParam[4].Value = ToDate;
                ds = SqlHelper.ExecuteDataSet("HelpDesk_GetCallBacks", ref sqlParam, connectionString);
            }
            return ds;
        }
        private string GetStoreProcName(string rptType)
        {
            string sp=string.Empty;
            switch (rptType)
            {
                case "1": sp = "MI_GetInternalRevenue_Summary"; break;
                case "2": sp = "MI_GetSPRevenueSummary"; break;
                case "3": sp = "MI_GetRevenue_Summary"; break;
                case "4": sp = "MI_GetActivationDetails"; break;
                case "5": sp = "MI_GetActivationSummary"; break;                    
                case "6": sp = "MI_GetSftpstatus"; break;                    
                case "7": sp = "MI_GetPrepaidPostpaidRevenue"; break;
                default: sp = ""; break;
            }
            return sp;
        }
        public DataSet GetSummary(string fromDate, string ToDate,string rptType)
        {
            string StoreProcName = GetStoreProcName(rptType);

            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[2];


                sqlParam[0] = new SqlParameter("@fromDate", SqlDbType.VarChar, 100);
                sqlParam[0].Value = fromDate;
                sqlParam[1] = new SqlParameter("@ToDate", SqlDbType.VarChar, 100);
                sqlParam[1].Value = ToDate;                
                ds = SqlHelper.ExecuteDataSet(StoreProcName, ref sqlParam, connectionString);
            }
            return ds;
        }
        public int updatesubscriber()
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                result = SqlHelper.ExecuteNonQuery("MI_GetUPDATED_SUBSCRIBE_USERS",  connectionString);
            }
            return result;
        }
        public DataSet Getdeletedsubscriber(string msisdn)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@msisdn", SqlDbType.VarChar,15);
                sqlParam[0].Value = msisdn;
                ds = SqlHelper.ExecuteDataSet("MI_GetFCC_DELETESUBSCRIBERSDETAILS", ref sqlParam, connectionString);
            }
            return ds;
        }

        public int GetSMSPromoconfiguration(string colorbatch, string language, string shortcode, string messageText, string publishtime)
        {
            int result = -1;
            try {
                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@ColorBatch", SqlDbType.VarChar, 10);
                sqlParam[0].Value = colorbatch;
                sqlParam[1] = new SqlParameter("@Lang", SqlDbType.VarChar, 10);
                sqlParam[1].Value = language;
                sqlParam[2] = new SqlParameter("@Shortcode", SqlDbType.VarChar, 20);
                sqlParam[2].Value = shortcode;
                sqlParam[3] = new SqlParameter("@MessageText", SqlDbType.NVarChar);
                sqlParam[3].Value = messageText;
                sqlParam[4] = new SqlParameter("@publishtime", SqlDbType.DateTime);
                sqlParam[4].Value = Convert.ToDateTime(publishtime);

                 result=SqlHelper.ExecuteNonQuery("MI_FCC_updated_SMSPromo_Congiguration", ref sqlParam, connectionString);

            }
            catch(Exception ex)
            {
                result = -1;

            }

            return result;
        }
        public DataSet GetChargeLogs(int telcoid, int CPID, string serviceID, string fromDate, string ToDate)
        {

            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@TelcoID", SqlDbType.Int);
                sqlParam[0].Value = telcoid;
                sqlParam[1] = new SqlParameter("@CPID", SqlDbType.Int);
                sqlParam[1].Value = CPID;
                sqlParam[2] = new SqlParameter("@serviceID", SqlDbType.VarChar, 10);
                sqlParam[2].Value = serviceID;
                sqlParam[3] = new SqlParameter("@fromDate", SqlDbType.VarChar, 100);
                sqlParam[3].Value = fromDate;
                sqlParam[4] = new SqlParameter("@ToDate", SqlDbType.VarChar, 100);
                sqlParam[4].Value = ToDate;
                ds = SqlHelper.ExecuteDataSet("HelpDesk_GetChargeLogs", ref sqlParam, connectionString);
            }
            return ds;
        }

        public List<ChangeSubscriberServicesBO> GetShortcodesByTelcoID(int telcoid)
        {
            List<ChangeSubscriberServicesBO> lstshortcode = new List<ChangeSubscriberServicesBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetShortcodesByTelcoID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TelcoID", telcoid);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ChangeSubscriberServicesBO objshortcode = new ChangeSubscriberServicesBO();
                    objshortcode.Shortcode = dr["Shortcode"].ToString();
                    lstshortcode.Add(objshortcode);
                }
            }
            connection.Close();
            return lstshortcode;
        }

        public List<ChangeSubscriberServicesBO> GetLookupTelcos(string ServiceSourceType)
        {
            List<ChangeSubscriberServicesBO> lsttelcos = new List<ChangeSubscriberServicesBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetLookupTelcos", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ServiceSourceTypes", ServiceSourceType);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ChangeSubscriberServicesBO objtelcos = new ChangeSubscriberServicesBO();
                    objtelcos.TelcoID = dr["Telco_ID"].ToString();
                    objtelcos.TelcoName = dr["Telco_Name"].ToString();
                    lsttelcos.Add(objtelcos);
                }
            }
            connection.Close();
            return lsttelcos;
        }

        public List<ChangeSubscriberServicesBO> GetLookupShortcodesByTelcoID(int TelcoID, string ServiceSourceType)
        {
            List<ChangeSubscriberServicesBO> lstshortcode = new List<ChangeSubscriberServicesBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetLookupShortcodesByTelcoID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TelcoID", TelcoID);
            cmd.Parameters.AddWithValue("@ServiceSourceTypes", ServiceSourceType);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ChangeSubscriberServicesBO objshortcode = new ChangeSubscriberServicesBO();
                    objshortcode.Shortcode = dr["Shortcode"].ToString();
                    lstshortcode.Add(objshortcode);
                }
            }
            connection.Close();
            return lstshortcode;
        }

        public List<LookupKeywordBO> GetLookupKeywords(int TelcoID, string Shortcode, string ServiceSourceType)
        {
            LookupKeywordBO objlookupKWBO = new LookupKeywordBO();
            List<LookupKeywordBO> lstlookupkws = new List<LookupKeywordBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetCommonLookupKeywords", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TelcoID", TelcoID);
            cmd.Parameters.AddWithValue("@Shortcode", Shortcode);
            cmd.Parameters.AddWithValue("@ServiceSourceTypes", ServiceSourceType);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int lookup_id = Convert.ToInt32(dr["LookupID"].ToString());
                    int lookup_telcoid = Convert.ToInt32(dr["TelcoID"].ToString());
                    string lookup_shortcode = dr["ShortCode"].ToString();
                    string lookup_kws = dr["Keyword"].ToString();
                    string lookup_sourcename = dr["SourceName"].ToString();
                    string lookup_idkws = lookup_id.ToString() + "|" + lookup_kws;
                    string servicename = dr["servicename"].ToString();
                    int serviceId = Convert.ToInt32(dr["serviceid"].ToString());


                    if (lookup_kws == string.Empty)
                    {
                        objlookupKWBO = new LookupKeywordBO();
                        objlookupKWBO.LookupID = lookup_id;
                        objlookupKWBO.TelcoID = lookup_telcoid;
                        objlookupKWBO.Shortcode = lookup_shortcode;
                        objlookupKWBO.Keyword = "[BLANK_KEYWORD]";
                        objlookupKWBO.SourceName = lookup_sourcename;
                        objlookupKWBO.LookupIDKeyword = lookup_idkws;
                        objlookupKWBO.ServiceName = servicename;
                        objlookupKWBO.ServiceId = serviceId;
                        lstlookupkws.Add(objlookupKWBO);
                    }
                    else
                    {
                        string[] kwlist = lookup_kws.Split(',');
                        for (int i = 0; i < kwlist.Length; i++)
                        {
                            if (kwlist[i] == string.Empty)
                                continue;
                            objlookupKWBO = new LookupKeywordBO();
                            objlookupKWBO.LookupID = lookup_id;
                            objlookupKWBO.TelcoID = lookup_telcoid;
                            objlookupKWBO.Shortcode = lookup_shortcode;
                            objlookupKWBO.Keyword = kwlist[i];
                            objlookupKWBO.SourceName = lookup_sourcename;
                            objlookupKWBO.LookupIDKeyword = lookup_idkws;
                            objlookupKWBO.ServiceName = servicename;
                            objlookupKWBO.ServiceId = serviceId;
                            lstlookupkws.Add(objlookupKWBO);
                        }
                    }

                }
            }
            connection.Close();
            return lstlookupkws;
        }
        public List<LookupKeywordBO> GetLookupKeywordsPRICESourceType(int TelcoID, string Shortcode)
        {
            LookupKeywordBO objlookupKWBO = new LookupKeywordBO();
            List<LookupKeywordBO> lstlookupkws = new List<LookupKeywordBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetCommonLookupKeywordsPRICESourceType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TelcoID", TelcoID);
            cmd.Parameters.AddWithValue("@Shortcode", Shortcode);

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int lookup_id = Convert.ToInt32(dr["LookupID"].ToString());
                    int lookup_telcoid = Convert.ToInt32(dr["TelcoID"].ToString());
                    string lookup_shortcode = dr["ShortCode"].ToString();
                    string lookup_kws = dr["Keyword"].ToString();
                    string lookup_sourceType = dr["SourceType"].ToString();
                    string lookup_idkws = lookup_id.ToString() + "|" + lookup_kws;
                    string ServicePrice = dr["Price"].ToString();
                    int serviceId = Convert.ToInt32(dr["serviceid"].ToString());
                    string BillServiceId = dr["BillServiceId"].ToString();


                    if (lookup_kws == string.Empty)
                    {
                        objlookupKWBO = new LookupKeywordBO();
                        objlookupKWBO.LookupID = lookup_id;
                        objlookupKWBO.TelcoID = lookup_telcoid;
                        objlookupKWBO.Shortcode = lookup_shortcode;
                        objlookupKWBO.Keyword = "[BLANK_KEYWORD]";
                        objlookupKWBO.SourceType = lookup_sourceType;
                        objlookupKWBO.LookupIDKeyword = lookup_idkws;
                        objlookupKWBO.ServiceName = ServicePrice;
                        objlookupKWBO.ServiceId = serviceId;
                        objlookupKWBO.BillingInfoList = GetBillingInfoList(BillServiceId); ;
                        lstlookupkws.Add(objlookupKWBO);
                    }
                    else
                    {
                        string[] kwlist = lookup_kws.Split(',');
                        for (int i = 0; i < kwlist.Length; i++)
                        {
                            if (kwlist[i] == string.Empty)
                                continue;
                            objlookupKWBO = new LookupKeywordBO();
                            objlookupKWBO.LookupID = lookup_id;
                            objlookupKWBO.TelcoID = lookup_telcoid;
                            objlookupKWBO.Shortcode = lookup_shortcode;
                            objlookupKWBO.Keyword = kwlist[i];
                            objlookupKWBO.SourceType = lookup_sourceType;
                            objlookupKWBO.LookupIDKeyword = lookup_idkws;
                            objlookupKWBO.ServiceName = ServicePrice;
                            objlookupKWBO.ServiceId = serviceId;
                            objlookupKWBO.BillingInfoList = GetBillingInfoList(BillServiceId); ;
                            lstlookupkws.Add(objlookupKWBO);
                        }
                    }

                }
            }
            connection.Close();
            return lstlookupkws;
        }

        public List<PriceListBO> GetPricesByTelco(int TelcoID, string Shortcode)
        {
            PriceListBO objPrice = new PriceListBO();
            List<PriceListBO> lstPrice = new List<PriceListBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetPricesByTelco", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TelcoID", TelcoID);
            cmd.Parameters.AddWithValue("@Shortcode", Shortcode);

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (!string.IsNullOrEmpty(dr["Price"].ToString()))
                    {
                        objPrice = new PriceListBO();
                        objPrice.Price_Id = Convert.ToInt32(dr["id"]);
                        objPrice.Price = Convert.ToDouble(dr["Price"]);
                        var match = lstPrice.FirstOrDefault(p => p.Price == objPrice.Price);
                        if (match == null)
                        {
                            lstPrice.Add(objPrice);
                        }
                    }
                }
            }
            connection.Close();
            return lstPrice;
        }

        public List<PriceListBO> GetPriceCodesById(int PriceId)
        {

            List<PriceListBO> lstPriceCodes = new List<PriceListBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("CPG_GetPriceCodesByID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", PriceId);

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    PriceListBO objPrice = new PriceListBO();
                    objPrice.Price = Convert.ToDouble(dr["Price"]);
                    objPrice.PrepaidCode = Convert.ToString(dr["PrepaidCode"]);
                    objPrice.PrepaidDescription = Convert.ToString(dr["PrepaidDescription"]);
                    objPrice.PostpaidCode = Convert.ToString(dr["PostpaidCode"]);
                    objPrice.PostpaidDescription = Convert.ToString(dr["PostpaidDescription"]);

                    lstPriceCodes.Add(objPrice);
                }
            }
            connection.Close();
            return lstPriceCodes;
        }


        public List<ChangeSubscriberServicesBO> GetAllTelcos()
        {
            List<ChangeSubscriberServicesBO> lsttelcos = new List<ChangeSubscriberServicesBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetActiveTelcos", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ChangeSubscriberServicesBO objtelcos = new ChangeSubscriberServicesBO();
                    objtelcos.TelcoID = dr["Telco_ID"].ToString();
                    objtelcos.TelcoName = dr["Telco_Name"].ToString();
                    lsttelcos.Add(objtelcos);
                }
            }
            connection.Close();
            return lsttelcos;
        }

        public int ChangeSubscriberServices(int OLDTelco_ID, int OldServiceID, int OldKeywordID, int NewTelco_ID, int NewServiceID, int NewKeywordID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@OLDTelco_ID", SqlDbType.BigInt);
                sqlParam[0].Value = OLDTelco_ID;
                sqlParam[1] = new SqlParameter("@OldServiceID", SqlDbType.BigInt);
                sqlParam[1].Value = OldServiceID;
                sqlParam[2] = new SqlParameter("@OldKeywordID", SqlDbType.BigInt);
                sqlParam[2].Value = OldKeywordID;
                sqlParam[3] = new SqlParameter("@NewTelco_ID", SqlDbType.BigInt);
                sqlParam[3].Value = NewTelco_ID;
                sqlParam[4] = new SqlParameter("@NewServiceID", SqlDbType.BigInt);
                sqlParam[4].Value = NewServiceID;
                sqlParam[5] = new SqlParameter("@NewKeywordID", SqlDbType.BigInt);
                sqlParam[5].Value = NewKeywordID;
                sqlParam[6] = new SqlParameter("@EffectedRecords", SqlDbType.BigInt);
                sqlParam[6].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery("HelpDesk_Change_SubscriberServices", ref sqlParam, connectionString);

                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return Convert.ToInt32(sqlParam[6].Value);
            }
        }

        public UserBO GetUserDetails(int loginID)
        {
            UserBO user = new UserBO();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetUserDetails_ByLoginID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LoginID", loginID);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    user.LoginID = Convert.ToInt32(dr["LoginID"].ToString());
                    user.LoginName = dr["LoginName"].ToString();
                    user.Password = dr["Password"].ToString();
                    user.RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                    user.RoleDescription = dr["RoleDescription"].ToString();
                    user.Name = dr["Name"].ToString();
                    user.EmailID = dr["EmailID"].ToString();
                    user.MobileNumber = dr["MobileNumber"].ToString();
                    user.CreatedBy = Convert.ToInt32(dr["CreatedBy"].ToString());
                    user.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                    user.UpdatedBy = Convert.ToInt32(dr["UpdatedBy"].ToString());
                    user.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());
                    user.UserStatus = dr["UserStatus"].ToString();
                }
            }
            connection.Close();
            return user;
        }


        public UserBO GetUserDetails(string loginName)
        {
            UserBO user = new UserBO();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetUserDetails_ByLoginName", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LoginName", loginName);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    user.LoginID = Convert.ToInt32(dr["LoginID"].ToString());
                    user.LoginName = dr["LoginName"].ToString();
                    user.Password = dr["Password"].ToString();
                    user.RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                    user.RoleDescription = dr["RoleDescription"].ToString();
                    user.Name = dr["Name"].ToString();
                    user.EmailID = dr["EmailID"].ToString();
                    user.MobileNumber = dr["MobileNumber"].ToString();
                    user.CreatedBy = Convert.ToInt32(dr["CreatedBy"].ToString());
                    user.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                    user.UpdatedBy = Convert.ToInt32(dr["UpdatedBy"].ToString());
                    user.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());
                    user.UserStatus = dr["UserStatus"].ToString();
                    user.PageNames_ReadWrite = dr["PageNames_ReadWrite"].ToString();
                    user.PageNames_ReadOnly = dr["PageNames_ReadOnly"].ToString();
                    user.PageNames_NoAccess = dr["PageNames_NoAccess"].ToString();
                }
            }
            connection.Close();
            return user;
        }

        public UserBO GetMICCUserDetails(string loginName)
        {
            UserBO user = new UserBO();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("MI_GetUserDetails_ByLoginName", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LoginName", loginName);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    user.LoginID = Convert.ToInt32(dr["LoginID"].ToString());
                    user.LoginName = dr["LoginName"].ToString();
                    user.Password = dr["Password"].ToString();
                    user.UserStatus = dr["UserStatus"].ToString();

                }
            }
            connection.Close();
            return user;
        }
        public List<ChangeSubscriberServicesBO> GetKeywordsByShortcode(string shortcode)
        {
            List<ChangeSubscriberServicesBO> lstKeywords = new List<ChangeSubscriberServicesBO>();
            DataTable dtKeywords = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@shortcode", SqlDbType.NVarChar);
                parameter[0].Value = shortcode;
                dtKeywords = SqlHelper.ExecuteDataTable("HelpDesk_GetKeywordsByShortcode", ref parameter, connectionString);
                if (dtKeywords != null && dtKeywords.Rows.Count > 0)
                {
                    foreach (DataRow row in dtKeywords.Rows)
                    {
                        ChangeSubscriberServicesBO objBO = new ChangeSubscriberServicesBO();
                        objBO.Keyword = row["KeywordName"].ToString();
                        objBO.BillServiceId = Convert.ToInt32(row["LookupID"].ToString());
                        lstKeywords.Add(objBO);
                    }
                }
                return lstKeywords;
            }
        }

        public string GetServiceNameByKeyword(int TelcoId, string Shortcode, int ServiceId, string ServiceType)
        {
            string servicename = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] parameter = new SqlParameter[2];
                parameter[0] = new SqlParameter("@BillserviceId", SqlDbType.NVarChar);
                parameter[0].Value = ServiceId;
                parameter[1] = new SqlParameter("@ServiceSourceType", SqlDbType.NVarChar);
                parameter[1].Value = ServiceType;
                servicename = Convert.ToString(SqlHelper.ExecuteScalar("HelpDesk_GetServiceNameForKeyword", ref parameter, connectionString));
                return servicename;
            }

        }
        public List<ContentBO> GetContentTypes()
        {
            List<ContentBO> lstContentTypes = new List<ContentBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_Get_ContentTypes", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ContentBO objContentTypes = new ContentBO();
                    objContentTypes.ContentTypeID = Convert.ToString(dr["ContentTypeId"]);
                    objContentTypes.ContentTypeName = dr["ContentType"].ToString();
                    lstContentTypes.Add(objContentTypes);
                }
            }
            connection.Close();
            return lstContentTypes;
        }
        public List<ContentBO> GetContentDetails(string SubscriptionId, string contentTypeId)
        {
            List<ContentBO> lstcontents = new List<ContentBO>();
            DataTable dtContentDetails = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] parameter = new SqlParameter[2];
                parameter[0] = new SqlParameter("@SubscriptionId", SqlDbType.NVarChar);
                parameter[0].Value = SubscriptionId;
                parameter[1] = new SqlParameter("@ContentTypeid", SqlDbType.NVarChar);
                parameter[1].Value = contentTypeId;
                dtContentDetails = SqlHelper.ExecuteDataTable("HelpDesk_Get_ContentDetails", ref parameter, connectionString);
                if (dtContentDetails != null && dtContentDetails.Rows.Count > 0)
                {
                    foreach (DataRow row in dtContentDetails.Rows)
                    {
                        ContentBO objBO = new ContentBO();
                        objBO.ContentId = Convert.ToInt64(row["ContentId"]);
                        objBO.ContentTypeID = row["ContentTypeId"].ToString();
                        objBO.ContentTypeName = Convert.ToString(row["Name"]);
                        objBO.LoginName = Convert.ToString(row["LoginName"]);
                        lstcontents.Add(objBO);
                    }
                }
                return lstcontents;
            }
        }
        public List<ChangeSubscriberServicesBO> GetKeywordsByShortcode_ContentDownload(string shortcode)
        {
            List<ChangeSubscriberServicesBO> lstKeywords = new List<ChangeSubscriberServicesBO>();
            DataTable dtKeywords = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@shortcode", SqlDbType.NVarChar);
                parameter[0].Value = shortcode;
                dtKeywords = SqlHelper.ExecuteDataTable("HelpDesk_GetKeywordsByShortcode_Content", ref parameter, connectionString);
                if (dtKeywords != null && dtKeywords.Rows.Count > 0)
                {
                    foreach (DataRow row in dtKeywords.Rows)
                    {
                        ChangeSubscriberServicesBO objBO = new ChangeSubscriberServicesBO();
                        objBO.Keyword = row["KeywordName"].ToString();
                        objBO.Subscription_Id = row["ServiceID"].ToString();
                        lstKeywords.Add(objBO);
                    }
                }
                return lstKeywords;
            }
        }
        public DataTable GetFinanceCategories()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataSet finData = SqlHelper.ExecuteDataSet("HelpDesk_GetActiveFinanceCategories", connectionString);
                return finData.Tables[0];
            }
        }
        public DataTable GetServiceTypes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataSet serviceTypes = SqlHelper.ExecuteDataSet("HelpDesk_GetServiceTypes", connectionString);
                return serviceTypes.Tables[0];
            }
        }

        public DataTable GetServiceDetailsByServiceType(string TelcoId, string shortcode, string ServiceType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable serviceTab = new DataTable();
                connection.Open();
                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@TelcoID", SqlDbType.Int);
                parameter[0].Value = Convert.ToInt32(TelcoId);
                parameter[1] = new SqlParameter("@ShortCode", SqlDbType.VarChar);
                parameter[1].Value = shortcode;
                parameter[2] = new SqlParameter("@ServiceSourceTypes", SqlDbType.VarChar);
                parameter[2].Value = ServiceType;
                DataSet serviceTypes = SqlHelper.ExecuteDataSet("HelpDesk_GetServiceDetailsByServiceType", ref parameter, connectionString);
                if (serviceTypes != null)
                {
                    if (serviceTypes.Tables.Count > 0)
                        serviceTab = serviceTypes.Tables[0];
                }
                return serviceTab;
            }
        }
        public int UpdateFinanceCategory(int FinanceCatId, string Lookupids)
        {
            string servicename = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@FinanceCategoryId", SqlDbType.NVarChar);
                parameter[0].Value = FinanceCatId;
                parameter[1] = new SqlParameter("@LookUpIds", SqlDbType.NVarChar);
                parameter[1].Value = Lookupids;
                parameter[2] = new SqlParameter("@EffectedRecords", SqlDbType.BigInt);
                parameter[2].Direction = ParameterDirection.Output;
                Convert.ToString(SqlHelper.ExecuteNonQuery("HelpDesk_UpdateFinanceCategory", ref parameter, connectionString));
                connection.Close();
                return Convert.ToInt32(parameter[2].Value);
            }

        }
        #region Logic for to send message to Configured queue for unsubscribed users who has notification urls
        public List<MessageQueueDetails> GetAllDataToPostMessage(int TelcoID, int Shortcode, string MSISDN, int ServiceID)
        {
            List<MessageQueueDetails> lstQDtls = new List<MessageQueueDetails>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetCpNotificationURLsByMSISDN", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@TelcoID", TelcoID);
            cmd.Parameters.AddWithValue("@Shortcode", Shortcode);
            cmd.Parameters.AddWithValue("@MSISDN", MSISDN);
            cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    MessageQueueDetails MqDtls = new MessageQueueDetails();
                    MqDtls.MSISDN = dr["MSISDN"].ToString();
                    MqDtls.ServiceID = Convert.ToInt32(dr["ServiceId"].ToString());
                    MqDtls.ServiceType = dr["ServiceType"].ToString();
                    MqDtls.CPNotificationURL = dr["CpNotificationURL"].ToString();
                    MqDtls.Keyword = dr["Keyword"].ToString();
                    lstQDtls.Add(MqDtls);
                }
            }
            connection.Close();
            return lstQDtls;
        }

        public List<MessageQueueDetails> GetAllDataToPostMessage(string MSISDN)
        {

            List<MessageQueueDetails> lstQDtls = new List<MessageQueueDetails>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetCpNotificationURLsByMSISDN_Rfailed", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MSISDN", MSISDN);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    MessageQueueDetails MqDtls = new MessageQueueDetails();
                    MqDtls.MSISDN = dr["MSISDN"].ToString();
                    MqDtls.ServiceID = Convert.ToInt32(dr["ServiceId"].ToString());
                    MqDtls.ServiceType = dr["ServiceType"].ToString();
                    MqDtls.CPNotificationURL = dr["CpNotificationURL"].ToString();
                    lstQDtls.Add(MqDtls);

                }
            }
            connection.Close();
            return lstQDtls;
        }

        public List<MessageQueueDetails> GetAllRenewalFailedServiceMSISDNs(int Days)
        {

            List<MessageQueueDetails> MsisdnList = new List<MessageQueueDetails>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetAllRenewalFailedServiceMSISDNs", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Days", Days);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    MessageQueueDetails MqDtls = new MessageQueueDetails();
                    MqDtls.MSISDN = dr["MSISDN"].ToString();
                    MqDtls.ServiceID = Convert.ToInt32(dr["UserSubscriptionId"].ToString());
                    MsisdnList.Add(MqDtls);
                }
            }
            connection.Close();
            return MsisdnList;
        }

        public int UnsubscribeUsersBySubscriptions(string Subscriptions)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@Subscriptions", SqlDbType.VarChar, -1);
                sqlParam[0].Value = Subscriptions;

                sqlParam[1] = new SqlParameter("@EffectedRows", SqlDbType.Int);
                sqlParam[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery("HelpDesk_UnsubscribeUsersBySubscriptions", ref sqlParam, connectionString);
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return Convert.ToInt32(sqlParam[1].Value);
            }
        }
        #endregion

        public List<StopBillingBO> GetBillableServicesForShortcode(int TelcoID, string Shortcode, bool showZeroPriceRec)
        {
            List<StopBillingBO> lstBillSrvs = new List<StopBillingBO>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@Telcoid", SqlDbType.Int);
                sqlParam[0].Value = TelcoID;
                sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.VarChar, 20);
                sqlParam[1].Value = Shortcode;
                sqlParam[2] = new SqlParameter("@showZeroPriceRc", SqlDbType.Bit);
                sqlParam[2].Value = showZeroPriceRec;
                DataSet dsBillingSrvs = SqlHelper.ExecuteDataSet("HelpDesk_SP_GetBillableServicesForShortcode", ref sqlParam, connectionString);
                if (dsBillingSrvs != null)
                {
                    DataTable dtSrvDtls = dsBillingSrvs.Tables[0];
                    foreach (DataRow dr in dtSrvDtls.Rows)
                    {
                        StopBillingBO objBillBO = new StopBillingBO();
                        //objBillBO.BillServiceId = (dr["lookupid"] == DBNull.Value || dr["lookupid"] == string.Empty) ? 0 : Convert.ToInt32(dr["lookupid"].ToString());
                        //objBillBO.cpid = (dr["cpid"] == DBNull.Value || dr["cpid"] == string.Empty) ? 0 : Convert.ToInt32(dr["cpid"].ToString());
                        //objBillBO.cpName = (dr["cpname"] == DBNull.Value || dr["cpname"] == string.Empty) ? string.Empty : dr["cpname"].ToString();
                        //objBillBO.TelcoID = (dr["telcoid"] == DBNull.Value || dr["telcoid"] == string.Empty) ? 0 : Convert.ToInt16(dr["telcoid"].ToString());
                        //objBillBO.Shortcode = (dr["shortcode"] == DBNull.Value || dr["shortcode"] == string.Empty) ? string.Empty : dr["shortcode"].ToString();
                        //objBillBO.Keyword = (dr["keyword"] == DBNull.Value || dr["keyword"] == string.Empty) ? string.Empty : dr["keyword"].ToString();
                        //objBillBO.Subscription_Id = (dr["serviceID"] == DBNull.Value || dr["serviceID"] == string.Empty) ? 0 : Convert.ToInt32(dr["serviceID"].ToString());
                        //objBillBO.Subscription_Name = (dr["serviceName"] == DBNull.Value || dr["serviceName"] == string.Empty) ? string.Empty : dr["serviceName"].ToString();
                        //objBillBO.SrvSourceType = (dr["sstype"] == DBNull.Value || dr["sstype"] == string.Empty) ? string.Empty : dr["sstype"].ToString();
                        //objBillBO.SrvSourceDesc = (dr["sstypedesc"] == DBNull.Value || dr["sstypedesc"] == string.Empty) ? string.Empty : dr["sstypedesc"].ToString();
                        //objBillBO.price = (dr["price"] == DBNull.Value || dr["price"] == string.Empty) ? 0 : Convert.ToDecimal(dr["price"].ToString());
                        //objBillBO.TelcoName = (dr["telconame"] == DBNull.Value || dr["telconame"] == string.Empty) ? string.Empty : dr["telconame"].ToString();
                        //objBillBO.formatKwd = (dr["formatedKeyword"] == DBNull.Value || dr["formatedKeyword"] == string.Empty) ? string.Empty : dr["formatedKeyword"].ToString();
                        objBillBO.price = (dr["price"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["price"].ToString());
                        objBillBO.SrvSourceType = (dr["sstype"] == DBNull.Value) ? string.Empty : dr["sstype"].ToString();
                        objBillBO.servicesCount = (dr["ServicesCount"] == DBNull.Value) ? string.Empty : dr["ServicesCount"].ToString();
                        lstBillSrvs.Add(objBillBO);
                    }
                }
            }
            return lstBillSrvs;
        }

        public bool StopBillingServicesForShortcode(int TelcoID, string Shortcode)
        {
            bool results = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@Telcoid", SqlDbType.Int);
                sqlParam[0].Value = TelcoID;
                sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.VarChar, 20);
                sqlParam[1].Value = Shortcode;
                int result = SqlHelper.ExecuteNonQuery("HelpDesk_sp_UpDateZeroPriceforShortcodeServices", ref sqlParam, connectionString);
                if (result > 0)
                    results = true;
            }
            return results;
        }

        public List<StopServicesBO> GetActiveServicesForShortcode(int TelcoID, string Shortcode)
        {
            List<StopServicesBO> lstBillSrvs = new List<StopServicesBO>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@Telcoid", SqlDbType.Int);
                sqlParam[0].Value = TelcoID;
                sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.VarChar, 20);
                sqlParam[1].Value = Shortcode;
                DataSet dsBillingSrvs = SqlHelper.ExecuteDataSet("HelpDesk_SP_GetActiveServicesForShortcode", ref sqlParam, connectionString);
                if (dsBillingSrvs != null)
                {
                    DataTable dtSrvDtls = dsBillingSrvs.Tables[0];
                    foreach (DataRow dr in dtSrvDtls.Rows)
                    {
                        StopServicesBO objBillBO = new StopServicesBO();

                        objBillBO.SrvSourceType = (dr["sstype"] == DBNull.Value || dr["sstype"] == string.Empty) ? string.Empty : dr["sstype"].ToString();
                        objBillBO.price = (dr["price"] == DBNull.Value || dr["price"] == string.Empty) ? 0 : Convert.ToDecimal(dr["price"].ToString());
                        objBillBO.ServicesCount = (dr["serviceCount"] == DBNull.Value || dr["serviceCount"] == string.Empty) ? 0 : Convert.ToInt32(dr["serviceCount"].ToString());


                        lstBillSrvs.Add(objBillBO);
                    }
                }
            }
            return lstBillSrvs;
        }

        public bool InactiveShortcodeServices(int TelcoID, string Shortcode)
        {
            bool results = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@Telcoid", SqlDbType.Int);
                sqlParam[0].Value = TelcoID;
                sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.VarChar, 20);
                sqlParam[1].Value = Shortcode;
                int result = SqlHelper.ExecuteNonQuery("HelpDesk_sp_InactiveShortcodeServices", ref sqlParam, connectionString);
                if (result > 0)
                    results = true;
            }
            return results;
        }

        public DataSet GetSubscriptionParamaters(int Telcoid, string shortcode, string keywords)
        {
            DataSet resultDs = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@telcid", SqlDbType.Int);
                sqlParam[0].Value = Telcoid;
                sqlParam[1] = new SqlParameter("@shortcode", SqlDbType.VarChar, 20);
                sqlParam[1].Value = shortcode;
                sqlParam[1] = new SqlParameter("@keywords", SqlDbType.NVarChar);
                sqlParam[1].Value = keywords;
                resultDs = SqlHelper.ExecuteDataSet("HelpDesk_sp_GetSubscriptionParamaters", ref sqlParam, connectionString);
            }
            return resultDs;
        }

        public DataSet GetSubscriptionDetails(int Telcoid, string shortcode, string Keyword)
        {
            DataSet resultDs = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@telcoid", SqlDbType.Int);
                sqlParam[0].Value = Telcoid;
                sqlParam[1] = new SqlParameter("@shortcode", SqlDbType.VarChar, 20);
                sqlParam[1].Value = shortcode;
                sqlParam[2] = new SqlParameter("@keyword", SqlDbType.NVarChar, 50);
                sqlParam[2].Value = Keyword == string.Empty ? DBNull.Value.ToString() : Keyword;
                resultDs = SqlHelper.ExecuteDataSet("HelpDesk_SP_GetBasicSubDetails", ref sqlParam, connectionString);
            }
            return resultDs;
        }

        public DataSet GetDetailsOfAllLogins(int cpid, int amid, int ceid)
        {
            DataSet resLoginDs = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@CPLoginID", SqlDbType.Int);
                sqlParam[0].Value = cpid;
                sqlParam[1] = new SqlParameter("@CPAMLoginID", SqlDbType.Int);
                sqlParam[1].Value = amid;
                sqlParam[2] = new SqlParameter("@CPCELoginID", SqlDbType.Int);
                sqlParam[2].Value = ceid;
                resLoginDs = SqlHelper.ExecuteDataSet("HelpDesk_SP_GetCPDetails", ref sqlParam, connectionString);
            }
            return resLoginDs;
        }

        public DataSet GetServiceDtlsOfCP(int cpid)
        {
            DataSet resSrvDtls = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@cploginid", SqlDbType.Int);
                sqlParam[0].Value = cpid;
                resSrvDtls = SqlHelper.ExecuteDataSet("HelpDesk_SP_GetServiceDetailsByCPID", ref sqlParam, connectionString);
            }
            return resSrvDtls;
        }

        public string UpdateManagerForCP(int CPID, int newCEID, int newAMID)
        {
            string res = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@CPG_CELoginID", SqlDbType.Int);
                sqlParam[0].Value = newCEID;
                sqlParam[1] = new SqlParameter("@CPG_AMLoginID", SqlDbType.Int);
                sqlParam[1].Value = newAMID;
                sqlParam[2] = new SqlParameter("@CPG_CAID", SqlDbType.Int);
                sqlParam[2].Value = CPID;
                sqlParam[3] = new SqlParameter("@Rtn_Status", SqlDbType.NVarChar, -1);
                sqlParam[3].Direction = ParameterDirection.Output;
                int reccount = SqlHelper.ExecuteNonQuery("HelpDesk_SP_Change_CE_AM_ForCACP", ref sqlParam, connectionString);
                res = sqlParam[3].Value.ToString();
            }
            return res;
        }

        public DataSet GetSubscriptionDetailsForTrails()
        {
            DataSet resSrvDtls = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                resSrvDtls = SqlHelper.ExecuteDataSet("HelpDesk_SP_GetSubscriptionDetails", connectionString);
            }
            return resSrvDtls;
        }

        public bool UpdateTrailPeriod(int shortcodeid, int trailperiod)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@trailPeriod", SqlDbType.Int);
                sqlParam[0].Value = trailperiod;
                sqlParam[1] = new SqlParameter("@shortcodeid", SqlDbType.Int);
                sqlParam[1].Value = shortcodeid;

                int reccount = SqlHelper.ExecuteNonQuery("HelpDesk_SP_UpdateTrailPeriod", ref sqlParam, connectionString);
                if (reccount > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        #region "Shortcode Swapping"

        public DataSet GetShortcodes()
        {
            DataSet dsShortcodes = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dsShortcodes = SqlHelper.ExecuteDataSet("HelpDesk_SP_SwapShrtCode_GetShortcodes", connectionString);
            }
            return dsShortcodes;
        }

        public DataSet GetServicesOnShortcodes(int cpid, string Shortcode)
        {
            DataSet dsShortcodes = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@cpid", SqlDbType.Int);
                sqlParam[0].Value = cpid;
                sqlParam[1] = new SqlParameter("@shortcode", SqlDbType.VarChar, 20);
                sqlParam[1].Value = Shortcode;
                dsShortcodes = SqlHelper.ExecuteDataSet("HelpDesk_SP_SwapShortcode_GetShortcodeServiceDtls_CLP", ref sqlParam, connectionString);
            }
            return dsShortcodes;
        }

        public string SwapShortcode(int TelcoID, string Shortcode, int OldCpid, int Newcpid)
        {

            string response = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@retrn_status", SqlDbType.VarChar, -1);
                sqlParam[0].Direction = ParameterDirection.Output;
                sqlParam[1] = new SqlParameter("@CPG_oldcpid", SqlDbType.Int);
                sqlParam[1].Value = OldCpid;
                sqlParam[2] = new SqlParameter("@CPG_newcpid", SqlDbType.Int);
                sqlParam[2].Value = Newcpid;
                sqlParam[3] = new SqlParameter("@telcoid", SqlDbType.Int);
                sqlParam[3].Value = TelcoID;
                sqlParam[4] = new SqlParameter("@moveshortcode", SqlDbType.VarChar, 20);
                sqlParam[4].Value = Shortcode;

                int reccount = SqlHelper.ExecuteNonQuery("HelpDesk_SP_ChangeShortcodeBetweenCps", ref sqlParam, connectionString);
                if (sqlParam[0].Value != null)
                {
                    response = sqlParam[0].Value.ToString();
                }
            }
            return response;

        }
        #endregion

        public List<ReportsBO> GetAllReports()
        {
            List<ReportsBO> lstReports = new List<ReportsBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetReportNames", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ReportsBO objReports = new ReportsBO();
                    objReports.ReportID = Convert.ToInt32(dr["ReportID"].ToString());
                    objReports.ReportName = dr["ReportName"].ToString();
                    lstReports.Add(objReports);
                }
            }
            connection.Close();
            return lstReports;
        }

        public List<ConstraintTypesBO> GetAllConstraintTypes()
        {
            List<ConstraintTypesBO> lstConstraints = new List<ConstraintTypesBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetConstraintTypes", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ConstraintTypesBO objConstraints = new ConstraintTypesBO();
                    objConstraints.ConstraintTypeID = Convert.ToInt32(dr["ContraintTypeID"].ToString());
                    objConstraints.ConstraintTypeName = dr["ContraintTypeName"].ToString();
                    lstConstraints.Add(objConstraints);
                }
            }
            connection.Close();
            return lstConstraints;
        }

        public List<FinanceCategoryBO> GetAllFinanceCategories()
        {
            List<FinanceCategoryBO> lstFinanceCat = new List<FinanceCategoryBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetFinanceCategories", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    FinanceCategoryBO objFinanceCat = new FinanceCategoryBO();
                    objFinanceCat.FinanceCategoryID = Convert.ToInt32(dr["Id"].ToString());
                    objFinanceCat.FinanceCategoryName = dr["Name"].ToString();
                    lstFinanceCat.Add(objFinanceCat);
                }
            }
            connection.Close();
            return lstFinanceCat;
        }

        public int SaveReportsRecipients(int configID, int reportID, string email_To, string email_Cc, string email_Bcc, string email_Consolidate_To, string email_Consolidate_Cc, string email_Consolidate_Bcc, string email_VivaKuwait_To, string email_VivaKuwait_Cc, string email_VivaKuwait_Bcc, int loginID, int constraintID, string constraintName)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[14];
                sqlParam[0] = new SqlParameter("@ConfigID", SqlDbType.Int);
                sqlParam[0].Value = configID;
                sqlParam[1] = new SqlParameter("@ReportID", SqlDbType.Int);
                sqlParam[1].Value = reportID;
                sqlParam[2] = new SqlParameter("@Email_To", SqlDbType.VarChar, -1);
                sqlParam[2].Value = email_To;
                sqlParam[3] = new SqlParameter("@Email_Cc", SqlDbType.VarChar, -1);
                sqlParam[3].Value = email_Cc;
                sqlParam[4] = new SqlParameter("@Email_Bcc", SqlDbType.VarChar, -1);
                sqlParam[4].Value = email_Bcc;
                sqlParam[5] = new SqlParameter("@Email_Consolidated_To", SqlDbType.VarChar, -1);
                sqlParam[5].Value = email_Consolidate_To;
                sqlParam[6] = new SqlParameter("@Email_Consolidated_Cc", SqlDbType.VarChar, -1);
                sqlParam[6].Value = email_Consolidate_Cc;
                sqlParam[7] = new SqlParameter("@Email_Consolidated_Bcc", SqlDbType.VarChar, -1);
                sqlParam[7].Value = email_Consolidate_Bcc;
                sqlParam[8] = new SqlParameter("@Email_VivaKuwait_To", SqlDbType.VarChar, -1);
                sqlParam[8].Value = email_VivaKuwait_To;
                sqlParam[9] = new SqlParameter("@Email_VivaKuwait_Cc", SqlDbType.VarChar, -1);
                sqlParam[9].Value = email_VivaKuwait_Cc;
                sqlParam[10] = new SqlParameter("@Email_VivaKuwait_Bcc", SqlDbType.VarChar, -1);
                sqlParam[10].Value = email_VivaKuwait_Bcc;
                sqlParam[11] = new SqlParameter("@LoginID", SqlDbType.Int);
                sqlParam[11].Value = loginID;
                sqlParam[12] = new SqlParameter("@ConstraintTypeID", SqlDbType.Int);
                sqlParam[12].Value = constraintID;
                sqlParam[13] = new SqlParameter("@ConstraintName", SqlDbType.VarChar, -1);
                sqlParam[13].Value = constraintName;
                result = SqlHelper.ExecuteNonQuery("HelpDesk_SaveReportRecipients", ref sqlParam, connectionString);
            }
            return result;
        }

        public List<ReportsRecipientsBO> GetReportRecipients(int configID, bool isReportIndexChanged)
        {
            List<ReportsRecipientsBO> list = new List<ReportsRecipientsBO>();
            string SPName = string.Empty;
            if (isReportIndexChanged)
                SPName = "HelpDesk_GetReportRecipientsByReportID";
            else
                SPName = "HelpDesk_GetReportRecipients";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@ConfigID", SqlDbType.Int);
            sqlParam[0].Value = configID;
            DataSet ds = SqlHelper.ExecuteDataSet(SPName, ref sqlParam, connectionString);

            if ((ds != null) && (ds.Tables.Count > 0))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ReportsRecipientsBO obj = new ReportsRecipientsBO();
                    obj.ReportID = Convert.ToInt32(dr["ReportID"]);
                    obj.ConfigID = Convert.ToInt32(dr["ConfigID"]);
                    obj.ReportName = dr["ReportName"].ToString();
                    if (isReportIndexChanged)
                    {
                        obj.Email_To = dr["Email_To"].ToString();
                        obj.Email_Cc = dr["Email_Cc"].ToString();
                        obj.Email_Bcc = dr["Email_Bcc"].ToString();
                        obj.Email_Consolidated_To = dr["Email_Consolidated_To"].ToString();
                        obj.Email_Consolidated_Cc = dr["Email_Consolidated_Cc"].ToString();
                        obj.Email_Consolidated_Bcc = dr["Email_Consolidated_Bcc"].ToString();
                        obj.Email_VivaKuwait_To = dr["Email_VivaKuwait_To"].ToString();
                        obj.Email_VivaKuwait_Cc = dr["Email_VivaKuwait_Cc"].ToString();
                        obj.Email_VivaKuwait_Bcc = dr["Email_VivaKuwait_Bcc"].ToString();
                    }
                    else
                    {
                        obj.Email_To = dr["Email_To"].ToString().Replace(",", ", ");
                        obj.Email_Cc = dr["Email_Cc"].ToString().Replace(",", ", ");
                        obj.Email_Bcc = dr["Email_Bcc"].ToString().Replace(",", ", ");
                        obj.Email_Consolidated_To = dr["Email_Consolidated_To"].ToString().Replace(",", ", ");
                        obj.Email_Consolidated_Cc = dr["Email_Consolidated_Cc"].ToString().Replace(",", ", ");
                        obj.Email_Consolidated_Bcc = dr["Email_Consolidated_Bcc"].ToString().Replace(",", ", ");
                        obj.Email_VivaKuwait_To = dr["Email_VivaKuwait_To"].ToString().Replace(",", ", ");
                        obj.Email_VivaKuwait_Cc = dr["Email_VivaKuwait_Cc"].ToString().Replace(",", ", ");
                        obj.Email_VivaKuwait_Bcc = dr["Email_VivaKuwait_Bcc"].ToString().Replace(",", ", ");
                    }

                    if (!string.IsNullOrEmpty(dr["ConstraintTypeID"].ToString()))
                        obj.ConstraintTypeID = Convert.ToInt32(dr["ConstraintTypeID"]);
                    else
                        obj.ConstraintTypeID = 0;
                    if (!string.IsNullOrEmpty(dr["ConstraintName"].ToString()))
                        obj.ConstraintName = dr["ConstraintName"].ToString();
                    else
                        obj.ConstraintName = string.Empty;
                    if (!string.IsNullOrEmpty(dr["ConstraintNameText"].ToString()))
                        obj.ConstraintNameText = dr["ConstraintNameText"].ToString();
                    else
                        obj.ConstraintNameText = string.Empty;
                    obj.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                    list.Add(obj);
                }
            }

            return list;
        }

        public int DeleteReportRecipient(int ConfigId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@configID", SqlDbType.Int);
                sqlParam[0].Value = ConfigId;

                return SqlHelper.ExecuteNonQuery("HelpDesk_DeleteReportRecipient", ref sqlParam, connectionString);
            }
        }
        public List<PriceListBO> getAllPriceList()
        {

            List<PriceListBO> lstPrice = new List<PriceListBO>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("HelpDesk_GetAllPriceList", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PriceListBO priceList = new PriceListBO();
                        priceList.Price_Id = Convert.ToInt32(dr["Price_Id"]);
                        priceList.Price = Convert.ToDouble(dr["Price"]);
                        lstPrice.Add(priceList);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            { connection.Close(); }


            return lstPrice;
        }

        public List<ActivateInactivateServicesBO> GetServiceSourceTypesByTelcoAndShortcode(int telcoID, string shortcode)
        {
            List<ActivateInactivateServicesBO> lstServiceSource = new List<ActivateInactivateServicesBO>();
            SqlDataReader dr;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable serviceTab = new DataTable();
                connection.Open();
                SqlParameter[] parameter = new SqlParameter[2];
                parameter[0] = new SqlParameter("@TelcoId", SqlDbType.Int);
                parameter[0].Value = Convert.ToInt32(telcoID);
                parameter[1] = new SqlParameter("@ShortCode", SqlDbType.VarChar);
                parameter[1].Value = shortcode;
                dr = SqlHelper.ExecuteReader("HelpDesk_GetServiceSourceTypesBasedonTelcoAndShortcode", ref parameter, connectionString);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ActivateInactivateServicesBO objSrvSourceType = new ActivateInactivateServicesBO();
                        objSrvSourceType.ServiceSourceTypeID = Convert.ToInt32(dr["ServiceSourceTypeID"].ToString());
                        objSrvSourceType.ServiceSourceType = dr["ServiceSourceType"].ToString();
                        lstServiceSource.Add(objSrvSourceType);
                    }
                }
                return lstServiceSource;
            }
        }

        public List<ActivateInactivateServicesBO> GetServicesToActivateInactivate(int telcoID, string shortcode, string serviceSourceType, string keyword, int serviceStatus)
        {
            List<ActivateInactivateServicesBO> lstServices = new List<ActivateInactivateServicesBO>();
            SqlDataReader dr;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable serviceTab = new DataTable();
                connection.Open();
                SqlParameter[] parameter = new SqlParameter[5];
                parameter[0] = new SqlParameter("@TelcoId", SqlDbType.Int);
                parameter[0].Value = Convert.ToInt32(telcoID);
                parameter[1] = new SqlParameter("@ShortCode", SqlDbType.VarChar);
                parameter[1].Value = shortcode;
                parameter[2] = new SqlParameter("@ServiceSouceType", SqlDbType.VarChar);
                parameter[2].Value = serviceSourceType;
                parameter[3] = new SqlParameter("@Keyword", SqlDbType.NVarChar);
                parameter[3].Value = keyword;
                parameter[4] = new SqlParameter("@ServiceStatus", SqlDbType.Int);
                parameter[4].Value = serviceStatus;
                dr = SqlHelper.ExecuteReader("HelpDesk_GetServiceToActivateInactivate", ref parameter, connectionString);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ActivateInactivateServicesBO objServices = new ActivateInactivateServicesBO();
                        objServices.BillServiceId = Convert.ToInt32(dr["LookupID"].ToString());
                        objServices.ServiceID = Convert.ToInt32(dr["ServiceID"].ToString());
                        objServices.ServiceName = dr["ServiceName"].ToString();
                        objServices.ServiceSourceType = dr["ServiceSourceType"].ToString();
                        objServices.Keyword = dr["Keyword"].ToString();
                        objServices.Price = Convert.ToDecimal(dr["Price"].ToString());
                        objServices.ServiceStatus = dr["ServiceStatus"].ToString();
                        lstServices.Add(objServices);
                    }
                }
                return lstServices;
            }
        }

        public int ActivateInactivateServices(ActivateInactivateServicesBO objServices)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@LookUpIDs", SqlDbType.VarChar);
                sqlParam[0].Value = objServices.LookUpIDs;
                sqlParam[1] = new SqlParameter("@IsActivate", SqlDbType.Bit);
                sqlParam[1].Value = objServices.ToActivateServices;
                result = SqlHelper.ExecuteNonQuery("HelpDesk_ActivateInactivateServices", ref sqlParam, connectionString);
            }
            return result;
        }

        public List<IPRangeBO> GetIPRangesList(int telcoID, int createdBy, int sNo, bool isIncludeInactive)
        {
            List<IPRangeBO> lstIPRanges = new List<IPRangeBO>();
            SqlDataReader dr;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable serviceTab = new DataTable();
                connection.Open();
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@TelcoId", SqlDbType.Int);
                parameter[0].Value = telcoID;
                parameter[1] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameter[1].Value = createdBy;
                parameter[2] = new SqlParameter("@SNo", SqlDbType.Int);
                parameter[2].Value = sNo;
                parameter[3] = new SqlParameter("@IsIncludeInactive", SqlDbType.Bit);
                parameter[3].Value = isIncludeInactive;

                dr = SqlHelper.ExecuteReader("HelpDesk_GetIPRangesList", ref parameter, connectionString);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        IPRangeBO objIPRange = new IPRangeBO();
                        objIPRange.SNo = Convert.ToInt32(dr["SNo"]);
                        objIPRange.Telco_Id = Convert.ToInt32(dr["Telco_Id"]);
                        objIPRange.Telco_Name = dr["Telco_Name"].ToString();
                        objIPRange.IPLowerbound = dr["IPLowerbound"].ToString();
                        objIPRange.IPUpperbound = dr["IPUpperbound"].ToString();
                        objIPRange.CreatedByLoginID = Convert.ToInt32(dr["CreatedByLoginID"]);
                        objIPRange.CreatedByLoginName = dr["CreatedByLoginName"].ToString();
                        objIPRange.CreatedDateTime = Convert.ToDateTime(dr["CreatedDateTime"]);
                        objIPRange.LastUpdatedByLoginID = Convert.ToInt32(dr["LastUpdatedByLoginID"]);
                        objIPRange.LastUpdatedByLoginName = dr["LastUpdatedByLoginName"].ToString();
                        objIPRange.LastUpdatedDateTime = Convert.ToDateTime(dr["LastUpdatedDateTime"]);
                        objIPRange.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objIPRange.IsActiveText = dr["IsActiveText"].ToString();
                        lstIPRanges.Add(objIPRange);
                    }
                }
                return lstIPRanges;
            }
        }

        public DataSet GetIPRangesListDS(int telcoID, int createdBy, int sNo, bool isIncludeInactive)
        {
            DataSet result = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@TelcoId", SqlDbType.Int);
                parameter[0].Value = telcoID;
                parameter[1] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameter[1].Value = createdBy;
                parameter[2] = new SqlParameter("@SNo", SqlDbType.Int);
                parameter[2].Value = sNo;
                parameter[3] = new SqlParameter("@IsIncludeInactive", SqlDbType.Bit);
                parameter[3].Value = isIncludeInactive;

                result = SqlHelper.ExecuteDataSet("HelpDesk_GetIPRangesList", ref parameter, connectionString);
            }
            return result;
        }

        public int DeleteIPRanges(string SNoList, string Separater, int LoginID)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@SNoList", SqlDbType.NVarChar, -1);
                sqlParam[0].Value = SNoList;
                sqlParam[1] = new SqlParameter("@Separater", SqlDbType.VarChar, 1);
                sqlParam[1].Value = Separater;
                sqlParam[2] = new SqlParameter("@LoginID", SqlDbType.Int);
                sqlParam[2].Value = LoginID;

                result = SqlHelper.ExecuteNonQuery("HelpDesk_DeleteIPRanges", ref sqlParam, connectionString);
            }

            return result;
        }

        public int UpdateIPRangeStatus(int SNo, bool IsActive, int LoginID)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@SNo", SqlDbType.Int);
                sqlParam[0].Value = SNo;
                sqlParam[1] = new SqlParameter("@IsActive", SqlDbType.Bit);
                sqlParam[1].Value = IsActive;
                sqlParam[2] = new SqlParameter("@LoginID", SqlDbType.Int);
                sqlParam[2].Value = LoginID;

                result = SqlHelper.ExecuteNonQuery("HelpDesk_UpdateIPRangeStatus", ref sqlParam, connectionString);
            }

            return result;
        }

        public bool UpdateIPRange(int SNo, string IPLowerbound, string IPUpperbound, int LoginID)
        {
            int result = 0;
            bool isSaved = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@SNo", SqlDbType.Int);
                sqlParam[0].Value = SNo;
                sqlParam[1] = new SqlParameter("@IPLowerbound", SqlDbType.VarChar, 50);
                sqlParam[1].Value = IPLowerbound;
                sqlParam[2] = new SqlParameter("@IPUpperbound", SqlDbType.VarChar, 50);
                sqlParam[2].Value = IPUpperbound;
                sqlParam[3] = new SqlParameter("@LoginID", SqlDbType.Int);
                sqlParam[3].Value = LoginID;
                sqlParam[4] = new SqlParameter("@IsSaved", SqlDbType.Bit);
                sqlParam[4].Direction = ParameterDirection.Output;

                result = SqlHelper.ExecuteNonQuery("HelpDesk_UpdateIPRange", ref sqlParam, connectionString);

                if (sqlParam[4] != null && Convert.ToBoolean(sqlParam[4].Value))
                    isSaved = true;
            }

            return isSaved;
        }

        public int SaveIPRange(int sNo, int telcoId, string IPLowerbound, string IPUpperbound, int LoginID, bool IsActive)
        {
            int result = 0;
            int savedSNo = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@SNo", SqlDbType.Int);
                sqlParam[0].Value = sNo;
                sqlParam[1] = new SqlParameter("@Telco_Id", SqlDbType.Int);
                sqlParam[1].Value = telcoId;
                sqlParam[2] = new SqlParameter("@IPLowerbound", SqlDbType.VarChar, 50);
                sqlParam[2].Value = IPLowerbound;
                sqlParam[3] = new SqlParameter("@IPUpperbound", SqlDbType.VarChar, 50);
                sqlParam[3].Value = IPUpperbound;
                sqlParam[4] = new SqlParameter("@LoginID", SqlDbType.Int);
                sqlParam[4].Value = LoginID;
                sqlParam[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                sqlParam[5].Value = IsActive;
                sqlParam[6] = new SqlParameter("@SavedSNo", SqlDbType.Int);
                sqlParam[6].Direction = ParameterDirection.Output;

                result = SqlHelper.ExecuteNonQuery("HelpDesk_SaveIPRange", ref sqlParam, connectionString);

                if (sqlParam[6] != null)
                    int.TryParse(sqlParam[6].Value.ToString(), out savedSNo);

            }
            return savedSNo;
        }


        public DataSet GetListofSubscribedUsersCount(string shortcode, string keyword, string Subscription_Id, string SDPDLID, string StartDate, string EndDate)
        {
            DataSet result = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@Shortcode", SqlDbType.VarChar);
                sqlParam[0].Value = shortcode;
                sqlParam[1] = new SqlParameter("@keyword", SqlDbType.VarChar);
                sqlParam[1].Value = keyword;
                sqlParam[2] = new SqlParameter("@Subscription_Id", SqlDbType.VarChar);
                sqlParam[2].Value = Subscription_Id;
                sqlParam[3] = new SqlParameter("@SDPDLID", SqlDbType.VarChar);
                sqlParam[3].Value = SDPDLID;
                sqlParam[4] = new SqlParameter("@StartDate", SqlDbType.VarChar);
                sqlParam[4].Value = StartDate;  //Convert .ToDateTime (StartDate);
                sqlParam[5] = new SqlParameter("@EndDate", SqlDbType.VarChar);
                sqlParam[5].Value = EndDate; //Convert.ToDateTime(EndDate);

                result = SqlHelper.ExecuteDataSet("HelpDesk_ListofSubscribedUsersCount", ref sqlParam, connectionString);

            }
            return result;
        }
        public DataSet GetContentsperShortcode(string SDPDLID, string shortcode, string StartDate, string EndDate)
        {
            DataSet result = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@SDPDLID", SqlDbType.VarChar);
                sqlParam[0].Value = SDPDLID;
                sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.VarChar);
                sqlParam[1].Value = shortcode;
                sqlParam[2] = new SqlParameter("@StartDate", SqlDbType.VarChar);
                sqlParam[2].Value = StartDate;  //Convert .ToDateTime (StartDate);
                sqlParam[3] = new SqlParameter("@EndDate", SqlDbType.VarChar);
                sqlParam[3].Value = EndDate; //Convert.ToDateTime(EndDate);

                result = SqlHelper.ExecuteDataSet("sprt_GetContentsCountBySDPDLID", ref sqlParam, connectionString);

            }
            return result;
        }

        public DataSet GetHitsReport(string shortcode, string Keyword, string Subscriptionid)
        {
            DataSet result = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@Shortcode", SqlDbType.VarChar);
                sqlParam[0].Value = shortcode;
                sqlParam[1] = new SqlParameter("@keyword", SqlDbType.VarChar);
                sqlParam[1].Value = Keyword;  //Convert .ToDateTime (StartDate);
                sqlParam[2] = new SqlParameter("@Subscription_Id", SqlDbType.VarChar);
                sqlParam[2].Value = Subscriptionid; //Convert.ToDateTime(EndDate);

                result = SqlHelper.ExecuteDataSet("HelpDesk_GetHitsReport", ref sqlParam, connectionString);

            }
            return result;
        }


        #region HelpDesk User Management

        public DataSet GetHeadersList()
        {
            DataSet result = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                result = SqlHelper.ExecuteDataSet("HelpDesk_GetHeadersList", connectionString);
            }
            return result;
        }

        public DataSet GetLinksbyHeader(int HeaderID, int RoleID)
        {
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@HeaderID", SqlDbType.Int);
            sqlParam[0].Value = HeaderID;
            sqlParam[1] = new SqlParameter("@RoleID", SqlDbType.Int);
            sqlParam[1].Value = RoleID;
            return SqlHelper.ExecuteDataSet("HelpDesk_GetLinksbyHeaderID", ref sqlParam, connectionString);
        }

        public void ChangeUserPassword(int LoginID, string Password)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@LoginID", SqlDbType.Int);
            sqlParam[0].Value = LoginID;
            sqlParam[1] = new SqlParameter("@Password", SqlDbType.VarChar);
            sqlParam[1].Value = Password;
            sqlParam[2] = new SqlParameter("@IsChangePassword", SqlDbType.Bit);
            sqlParam[2].Value = true;

            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "HelpDesk_InsertUpdateUsers";
            sqlCmd.Parameters.AddRange(sqlParam);
            int res = sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
        }

        public void ChangeUserStatus(int LoginID, string Status)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@LoginID", SqlDbType.Int);
            sqlParam[0].Value = LoginID;
            sqlParam[1] = new SqlParameter("@UserStatus", SqlDbType.VarChar);
            sqlParam[1].Value = Status;
            sqlParam[2] = new SqlParameter("@IsChangeStatus", SqlDbType.Bit);
            sqlParam[2].Value = true;

            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "HelpDesk_InsertUpdateUsers";
            sqlCmd.Parameters.AddRange(sqlParam);
            int res = sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
        }

        public DataSet GetHelpDeskUsersData()
        {
            DataSet result = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                result = SqlHelper.ExecuteDataSet("HelpDesk_GetUsersData", connectionString);
            }
            return result;
        }

        public DataSet GetHelpDeskUsersDatabyLoginId(int LoginID)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@LoginID", SqlDbType.Int);
            sqlParam[0].Value = LoginID;
            return SqlHelper.ExecuteDataSet("HelpDesk_GetUsersDatabyLoginId", ref sqlParam, connectionString);
        }

        public DataSet GetHelpDeskRoles()
        {
            DataSet result = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                result = SqlHelper.ExecuteDataSet("HelpDesk_GetRoles", connectionString);
            }
            return result;
        }

        public bool CheckHelpDeskUserExist(string LoginName)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@LoginName", SqlDbType.VarChar);
            sqlParam[0].Value = LoginName;
            DataSet dsUser = SqlHelper.ExecuteDataSet("HelpDesk_CheckUserExist", ref sqlParam, connectionString);
            bool result = false;
            if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                result = Convert.ToBoolean(dsUser.Tables[0].Rows[0]["IsUserExist"]);
            return result;
        }


        public void AddEditUser(string LoginName, string Password, int RoleID, string Name, string EmailID, string MobileNumber, string UserStatus,
                                        int Createdby, int Updatedby, int LoginID, bool IsAdd)
        {

            SqlConnection sqlConn = new SqlConnection(connectionString);

            SqlParameter[] sqlParam = new SqlParameter[11];
            sqlParam[0] = new SqlParameter("@LoginName", SqlDbType.VarChar);
            sqlParam[0].Value = LoginName;
            if (!string.IsNullOrEmpty(Password))
            {
                sqlParam[1] = new SqlParameter("@Password", SqlDbType.VarChar);
                sqlParam[1].Value = Password;
            }
            else
            {
                sqlParam[1] = new SqlParameter("@Password", SqlDbType.VarChar);
                sqlParam[1].Value = DBNull.Value;
            }
            sqlParam[2] = new SqlParameter("@RoleID", SqlDbType.Int);
            sqlParam[2].Value = RoleID;
            sqlParam[3] = new SqlParameter("@Name", SqlDbType.VarChar);
            sqlParam[3].Value = Name;
            sqlParam[4] = new SqlParameter("@EmailID", SqlDbType.VarChar);
            sqlParam[4].Value = EmailID;
            sqlParam[5] = new SqlParameter("@MobileNumber", SqlDbType.VarChar);
            sqlParam[5].Value = MobileNumber;

            sqlParam[6] = new SqlParameter("@CreatedBy", SqlDbType.Int);
            sqlParam[6].Value = Createdby;

            sqlParam[7] = new SqlParameter("@UpdatedBy", SqlDbType.Int);
            sqlParam[7].Value = (IsAdd) ? Createdby : Updatedby;

            sqlParam[8] = new SqlParameter("@UserStatus", SqlDbType.VarChar);
            sqlParam[8].Value = UserStatus;

            sqlParam[9] = new SqlParameter("@IsAdd", SqlDbType.Bit);
            sqlParam[9].Value = IsAdd;
            sqlParam[10] = new SqlParameter("@LoginID", SqlDbType.Int);
            sqlParam[10].Value = LoginID;

            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "HelpDesk_InsertUpdateUsers";
            sqlCmd.Parameters.AddRange(sqlParam);
            int res = sqlCmd.ExecuteNonQuery();

            sqlConn.Close();

        }

        public int AddEditMasterUser(string LoginName, string Password, string Name, string EmailID, string MobileNumber, string UserStatus, int LoginID, bool IsAdd, string MappedTelcoIDs, out bool UsernameExists)
        {
            UsernameExists = false;
            SqlConnection sqlConn = new SqlConnection(connectionString);

            SqlParameter[] sqlParam = new SqlParameter[10];
            sqlParam[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            sqlParam[0].Value = LoginName;
            if (!string.IsNullOrEmpty(Password))
            {
                sqlParam[1] = new SqlParameter("@APIPassword", SqlDbType.NVarChar, 400);
                sqlParam[1].Value = Password;
            }
            else
            {
                sqlParam[1] = new SqlParameter("@APIPassword", SqlDbType.NVarChar, 400);
                sqlParam[1].Value = DBNull.Value;
            }
            sqlParam[2] = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
            sqlParam[2].Value = Name;
            sqlParam[3] = new SqlParameter("@EmailID", SqlDbType.NVarChar, -1);
            sqlParam[3].Value = EmailID;
            sqlParam[4] = new SqlParameter("@MSISDN", SqlDbType.NVarChar, 50);
            sqlParam[4].Value = MobileNumber;
            sqlParam[5] = new SqlParameter("@UserStatus", SqlDbType.VarChar);
            sqlParam[5].Value = UserStatus;
            sqlParam[6] = new SqlParameter("@IsAdd", SqlDbType.Bit);
            sqlParam[6].Value = IsAdd;
            sqlParam[7] = new SqlParameter("@LoginID", SqlDbType.Int);
            sqlParam[7].Value = LoginID;
            sqlParam[8] = new SqlParameter("@MappedTelcoIDs", SqlDbType.NVarChar, -1);
            sqlParam[8].Value = MappedTelcoIDs;
            sqlParam[9] = new SqlParameter("@UsernameExists", SqlDbType.Bit);
            sqlParam[9].Direction = ParameterDirection.Output;

            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "HelpDesk_SaveMasterUser";
            sqlCmd.Parameters.AddRange(sqlParam);
            int res = sqlCmd.ExecuteNonQuery();

            if (sqlParam[9] != null)
                bool.TryParse(sqlParam[9].Value.ToString(), out UsernameExists);

            sqlConn.Close();
            return res;
        }

        public DataSet GetMasterUsersData()
        {
            DataSet result = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                result = SqlHelper.ExecuteDataSet("HelpDesk_GetMasterUsersData", connectionString);
            }
            return result;
        }

        public void ChangeMasterUserStatus(int LoginID, string Status)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@LoginID", SqlDbType.Int);
            sqlParam[0].Value = LoginID;
            sqlParam[1] = new SqlParameter("@UserStatus", SqlDbType.VarChar);
            sqlParam[1].Value = Status;
            sqlParam[2] = new SqlParameter("@IsChangeStatus", SqlDbType.Bit);
            sqlParam[2].Value = true;

            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "HelpDesk_SaveMasterUser";
            sqlCmd.Parameters.AddRange(sqlParam);
            int res = sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
        }

        public void ChangeMasterUserPassword(int LoginID, string Password)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@LoginID", SqlDbType.Int);
            sqlParam[0].Value = LoginID;
            sqlParam[1] = new SqlParameter("@APIPassword", SqlDbType.VarChar);
            sqlParam[1].Value = Password;
            sqlParam[2] = new SqlParameter("@IsChangePassword", SqlDbType.Bit);
            sqlParam[2].Value = true;

            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "HelpDesk_SaveMasterUser";
            sqlCmd.Parameters.AddRange(sqlParam);
            int res = sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
        }

        public DataSet GetMasterUsersDatabyLoginId(int LoginID)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@LoginID", SqlDbType.Int);
            sqlParam[0].Value = LoginID;
            return SqlHelper.ExecuteDataSet("HelpDesk_GetMasterUsersDatabyLoginId", ref sqlParam, connectionString);
        }

        public int DeleteMasterUser(int LoginID)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@LoginID", SqlDbType.Int);
                sqlParam[0].Value = LoginID;
                result = SqlHelper.ExecuteNonQuery("HelpDesk_DeleteMasterUser", ref sqlParam, connectionString);
            }

            return result;
        }

        #endregion

        public string InsertBulkServiceTableData(DataTable dt)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlParameter[] sqlParam = new SqlParameter[15];
            string result = "failure";
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sqlParam[0] = new SqlParameter("@Shortcode", SqlDbType.NVarChar);
                    sqlParam[0].Value = dt.Rows[i]["Shortcode"].ToString();
                    sqlParam[1] = new SqlParameter("@KeyWordsEnglish", SqlDbType.NVarChar);
                    sqlParam[1].Value = dt.Rows[i]["English_Keywords"].ToString();
                    sqlParam[2] = new SqlParameter("@KeyWordsArabic", SqlDbType.NVarChar);
                    sqlParam[2].Value = dt.Rows[i]["Arabic_Keywords"].ToString();
                    sqlParam[3] = new SqlParameter("@Price", SqlDbType.NVarChar);
                    sqlParam[3].Value = dt.Rows[i]["Sub_Price"].ToString();
                    //sqlParam[4] = new SqlParameter("@CDRCode", SqlDbType.NVarChar);
                    //sqlParam[4].Value = dt.Rows[i]["CDRCode"].ToString();
                    sqlParam[4] = new SqlParameter("@CPLoginName", SqlDbType.NVarChar);
                    sqlParam[4].Value = dt.Rows[i]["CreatedByUserName"].ToString();
                    sqlParam[5] = new SqlParameter("@BillingTypeID", SqlDbType.NVarChar);
                    sqlParam[5].Value = dt.Rows[i]["Subscription_Type"].ToString();
                    sqlParam[6] = new SqlParameter("@ServiceName", SqlDbType.NVarChar);
                    sqlParam[6].Value = dt.Rows[i]["Subscription_Name"].ToString();
                    sqlParam[7] = new SqlParameter("@ServiceDescription", SqlDbType.NVarChar);
                    sqlParam[7].Value = dt.Rows[i]["Subscription_Description"].ToString();
                    //sqlParam[9] = new SqlParameter("@RevenueID", SqlDbType.Int);
                    //sqlParam[9].Value = Convert.ToInt32(dt.Rows[i]["RevenueID"]);
                    sqlParam[8] = new SqlParameter("@successfully_subscribed", SqlDbType.NVarChar);
                    sqlParam[8].Value = dt.Rows[i]["Response_Subscription_SS_EN"].ToString();
                    sqlParam[9] = new SqlParameter("@successfully_unsubscribed", SqlDbType.NVarChar);
                    sqlParam[9].Value = dt.Rows[i]["Response_Unsubscription_SU_EN"].ToString();

                    sqlParam[10] = new SqlParameter("@charging_failed", SqlDbType.NVarChar);
                    sqlParam[10].Value = dt.Rows[i]["Response_ChargeFailed_CF_EN"].ToString();

                    //sqlParam[15] = new SqlParameter("@Response_Subscription_SS_AR", SqlDbType.NVarChar);
                    //sqlParam[15].Value = dt.Rows[i]["Response_Subscription_SS_AR"].ToString();
                    //sqlParam[16] = new SqlParameter("@Response_Unsubscription_SU_AR", SqlDbType.NVarChar);
                    //sqlParam[16].Value = dt.Rows[i]["Response_Unsubscription_SU_AR"].ToString();
                    //sqlParam[17] = new SqlParameter("@Response_Resubscription_AS_AR", SqlDbType.NVarChar);
                    //sqlParam[17].Value = dt.Rows[i]["Response_Resubscription_AS_AR"].ToString();
                    //sqlParam[18] = new SqlParameter("@Response_ChargeFailed_CF_AR", SqlDbType.NVarChar);
                    //sqlParam[18].Value = dt.Rows[i]["Response_ChargeFailed_CF_AR"].ToString();
                    //sqlParam[19] = new SqlParameter("@Response_NotSubscribed_NS_AR", SqlDbType.NVarChar);
                    //sqlParam[19].Value = dt.Rows[i]["Response_NotSubscribed_NS_AR"].ToString();
                    sqlParam[11] = new SqlParameter("@VShopID", SqlDbType.Int);
                    sqlParam[11].Value = Convert.ToInt32(dt.Rows[i]["StorefrontID"]);
                    sqlParam[12] = new SqlParameter("@already_subscribed", SqlDbType.NVarChar);
                    sqlParam[12].Value = dt.Rows[i]["Response_Resubscription_AS_EN"].ToString();
                    //sqlParam[21] = new SqlParameter("@ParentSFCategoryId", SqlDbType.NVarChar);
                    //sqlParam[21].Value = dt.Rows[i]["ParentSFCategoryId"].ToString();
                    //sqlParam[22] = new SqlParameter("@ParentUserCategoryId", SqlDbType.NVarChar);
                    //sqlParam[22].Value = dt.Rows[i]["ParentUserCategoryId"].ToString();
                    //sqlParam[23] = new SqlParameter("@OriginalShortcode", SqlDbType.NVarChar);
                    //sqlParam[23].Value = dt.Rows[i]["OriginalShortcode"].ToString();
                    //sqlParam[24] = new SqlParameter("@UserCategoryname", SqlDbType.NVarChar);
                    //sqlParam[24].Value = dt.Rows[i]["UserCategoryname"].ToString();
                    //sqlParam[25] = new SqlParameter("@ParentCategoryname", SqlDbType.NVarChar);
                    //sqlParam[25].Value = dt.Rows[i]["ParentCategoryname"].ToString();
                    //sqlParam[26] = new SqlParameter("@SDPServiceId", SqlDbType.NVarChar);
                    //sqlParam[26].Value = dt.Rows[i]["SDPServiceId"].ToString();
                    sqlParam[13] = new SqlParameter("@OperatorID", SqlDbType.Int);
                    sqlParam[13].Value = Convert.ToInt32(dt.Rows[i]["TelcoID"]);
                    sqlParam[14] = new SqlParameter("@not_subscribed", SqlDbType.NVarChar);
                    sqlParam[14].Value = dt.Rows[i]["Response_NotSubscribed_NS_EN"].ToString();
                    //sqlParam[28] = new SqlParameter("@FinanceCategoryId", SqlDbType.Int);
                    //sqlParam[28].Value = Convert.ToInt32(dt.Rows[i]["FinanceCategoryId"]);

                    sqlConn.Open();
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlConn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "SPRT_HelpDesk_InsertBulkServiceTableData";
                    sqlCmd.Parameters.AddRange(sqlParam);
                    int res = sqlCmd.ExecuteNonQuery();

                    sqlConn.Close();
                }

                //SqlBulkCopy sbc = new SqlBulkCopy(connectionString); 

                //    foreach (DataColumn col in dt.Columns)
                //    {
                //        sbc.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                //    }
                //    sbc.BulkCopyTimeout = 600;
                //    sbc.DestinationTableName = FileName;
                //    sbc.WriteToServer(dt);
                //    sbc.Close();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();

            }
            return result;
        }
        public void InsertBulkRecords(DataTable dt)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            //creating object of SqlBulkCopy  
            SqlBulkCopy objbulk = new SqlBulkCopy(sqlConn);
            //assigning Destination table name  
            objbulk.DestinationTableName = "MI_PromotionsSMSConfiguration";
            //Mapping Table column  
            objbulk.ColumnMappings.Add("cpId", "cpId");
            objbulk.ColumnMappings.Add("CpName", "CpName");
            objbulk.ColumnMappings.Add("CpPwd", "CpPwd");
            objbulk.ColumnMappings.Add("correlatorid", "correlatorid");
            objbulk.ColumnMappings.Add("Msisdn", "Msisdn");
            objbulk.ColumnMappings.Add("message", "message");
            objbulk.ColumnMappings.Add("SCAlias", "SCAlias");
            objbulk.ColumnMappings.Add("sc", "sc");
            objbulk.ColumnMappings.Add("BillId", "BillId");
            objbulk.ColumnMappings.Add("publishTime", "publishTime");
            objbulk.ColumnMappings.Add("createddate", "createddate");
            //inserting bulk Records into DataBase   
            objbulk.WriteToServer(dt);
            //objbulk.BatchSize = 15000;
            //objbulk.BulkCopyTimeout = 600000;
            sqlConn.Close();

        }
        public void InsertBulkRecordsWithDataTable(DataTable dtMsg)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "MI_SMSPromo_Insert";
            sqlCmd.Parameters.AddWithValue("@SMSPromo_MSG_Dtls",dtMsg);
            int res = sqlCmd.ExecuteNonQuery();

            sqlConn.Close();

        }
        public void InsertBulkRecordsWithDataTable_MSISDN(DataTable dtMsisdn)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "MI_SMSPromo_Bulk_Insert";
            sqlCmd.Parameters.AddWithValue("@SMSPromo_MSISDN_Dtls", dtMsisdn);
            int res = sqlCmd.ExecuteNonQuery();

            sqlConn.Close();

        }
        public DataSet GetDataToCreateServices()
        {
            DataSet result = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                result = SqlHelper.ExecuteDataSet("SPRT_GetBulkServiceTableData", connectionString);
            }
            return result;
        }

        #region Price LImit Configuration

        public List<PriceLimitConfiguration> SearchMSISDNChargeLimitDetails(string msisdn_part, int telco_id)
        {
            List<PriceLimitConfiguration> objList = new List<PriceLimitConfiguration>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@MSISDN_Part", SqlDbType.VarChar, 200);
                sqlParam[0].Value = msisdn_part;
                sqlParam[1] = new SqlParameter("@TelcoID", SqlDbType.Int);
                sqlParam[1].Value = telco_id;

                DataSet dsList = SqlHelper.ExecuteDataSet("CPG_GetPriceLimitDetails_Search", ref sqlParam, connectionString);
                FillDetails(dsList, objList);
                return (objList);
            }
        }

        public PriceLimitConfiguration GetMSISDNChargeLimitDetailsByMSISDN(string MSISDN)
        {
            List<PriceLimitConfiguration> objList = new List<PriceLimitConfiguration>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@MSISDN", SqlDbType.VarChar, 200);
                sqlParam[0].Value = MSISDN;

                DataSet dsList = SqlHelper.ExecuteDataSet("CPG_GetPriceLimitDetailsByMSISDN", ref sqlParam, connectionString);
                FillDetails(dsList, objList);
                if (objList.Count > 0)
                    return objList[0];
                else
                    return null;
            }
        }

        public bool DeleteMSISDNChargeLimitDetails(long limitID)
        {
            bool deleted = false;

            try
            {
                List<PriceLimitConfiguration> objList = new List<PriceLimitConfiguration>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlParameter[] sqlParam = new SqlParameter[1];
                    sqlParam[0] = new SqlParameter("@LimitID", SqlDbType.BigInt);
                    sqlParam[0].Value = limitID;

                    SqlHelper.ExecuteNonQuery("HelpDesk_DeleteMsisdnPriceLimitDetails", ref sqlParam, connectionString);
                    deleted = true;
                }
            }
            catch
            {
                deleted = false;
            }
            return deleted;
        }

        public void SaveMSISDNChargeLimitDetails(PriceLimitConfiguration objLimit, out bool isExists)
        {
            isExists = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlParameter[] sqlParam = new SqlParameter[7];
                    sqlParam[0] = new SqlParameter("@MSISDN", SqlDbType.VarChar, 200);
                    sqlParam[0].Value = objLimit.MSISDN;
                    sqlParam[1] = new SqlParameter("@Telco_ID", SqlDbType.Int);
                    sqlParam[1].Value = objLimit.Telco_ID;
                    sqlParam[2] = new SqlParameter("@Daily_Price_Limit", SqlDbType.Money);
                    sqlParam[2].Value = objLimit.Daily_Price_Limit;
                    sqlParam[3] = new SqlParameter("@Monthly_Price_Limit", SqlDbType.Money);
                    sqlParam[3].Value = objLimit.Monthly_Price_Limit;
                    sqlParam[4] = new SqlParameter("@LoginID", SqlDbType.Int);

                    if (objLimit.ID == 0)
                        sqlParam[4].Value = objLimit.CreatedByLoginID;
                    else
                        sqlParam[4].Value = objLimit.LastUpdatedByLoginID;

                    sqlParam[5] = new SqlParameter("@IsInsert", SqlDbType.Bit);
                    sqlParam[5].Value = objLimit.IsInsert;

                    sqlParam[6] = new SqlParameter("@IsExists", SqlDbType.Bit);
                    sqlParam[6].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery("HelpDesk_SaveMsisdnPriceLimitDetails", ref sqlParam, connectionString);

                    if ((sqlParam[6] != null) && (sqlParam[6].Value != DBNull.Value))
                        bool.TryParse(sqlParam[6].Value.ToString(), out isExists);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public bool DeleteDefaultChargeLimitDetails(long limitID, int chargeLimitTypeID, string limitCategory)
        {
            bool deleted = false;

            try
            {
                List<PriceLimitConfiguration> objList = new List<PriceLimitConfiguration>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlParameter[] sqlParam = new SqlParameter[3];
                    sqlParam[0] = new SqlParameter("@LimitID", SqlDbType.BigInt);
                    sqlParam[0].Value = limitID;
                    sqlParam[1] = new SqlParameter("@ChargeLimitTypeID", SqlDbType.Int);
                    sqlParam[1].Value = chargeLimitTypeID;
                    sqlParam[2] = new SqlParameter("@LimitCategory", SqlDbType.VarChar, 50);
                    sqlParam[2].Value = limitCategory;

                    SqlHelper.ExecuteNonQuery("HelpDesk_DeleteDefaultChargeLimitDetails", ref sqlParam, connectionString);
                    deleted = true;
                }
            }
            catch
            {
                deleted = false;
            }
            return deleted;
        }

        public void SaveDefaultChargeLimitDetails(ChargeLimit objLimit, out string errMsg)
        {
            errMsg = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlParameter[] sqlParam = new SqlParameter[18];
                    sqlParam[0] = new SqlParameter("@LimitID", SqlDbType.BigInt);
                    sqlParam[0].Value = objLimit.LimitID;

                    sqlParam[1] = new SqlParameter("@MSISDN", SqlDbType.VarChar, 200);
                    sqlParam[1].Value = objLimit.MSISDN;
                    sqlParam[2] = new SqlParameter("@TelcoID", SqlDbType.Int);
                    sqlParam[2].Value = objLimit.TelcoID;
                    sqlParam[3] = new SqlParameter("@LookupID", SqlDbType.Int);
                    sqlParam[3].Value = objLimit.LookupID;
                    sqlParam[4] = new SqlParameter("@LimitCategory", SqlDbType.VarChar, 50);
                    sqlParam[4].Value = objLimit.LimitCategory;


                    sqlParam[5] = new SqlParameter("@ChargeLimitTypeID", SqlDbType.Int);
                    sqlParam[5].Value = objLimit.ChargeLimitTypeID;
                    sqlParam[6] = new SqlParameter("@ChargeLimitAmount", SqlDbType.Money);
                    sqlParam[6].Value = objLimit.ChargeLimitAmount;
                    sqlParam[7] = new SqlParameter("@ChargeLimitAmount_Prepaid", SqlDbType.Money);
                    sqlParam[7].Value = objLimit.ChargeLimitAmount_Prepaid;
                    sqlParam[8] = new SqlParameter("@ChargeLimitAmount_Postpaid", SqlDbType.Money);
                    sqlParam[8].Value = objLimit.ChargeLimitAmount_Postpaid;

                    if (objLimit.ChargeLimitTypeID == 4)
                    {
                        sqlParam[9] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                        sqlParam[9].Value = objLimit.FromDate;
                        sqlParam[10] = new SqlParameter("@ToDate", SqlDbType.DateTime);
                        sqlParam[10].Value = objLimit.ToDate;
                    }
                    else
                    {
                        sqlParam[9] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                        sqlParam[9].Value = DBNull.Value;
                        sqlParam[10] = new SqlParameter("@ToDate", SqlDbType.DateTime);
                        sqlParam[10].Value = DBNull.Value;
                    }

                    if (objLimit.LimitID == 0)
                    {
                        sqlParam[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                        sqlParam[11].Value = objLimit.CreatedByLoginID;
                        sqlParam[12] = new SqlParameter("@LastUpdatedBy", SqlDbType.Int);
                        sqlParam[12].Value = DBNull.Value;
                    }
                    else
                    {
                        sqlParam[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                        sqlParam[11].Value = objLimit.CreatedByLoginID;
                        sqlParam[12] = new SqlParameter("@LastUpdatedBy", SqlDbType.Int);
                        sqlParam[12].Value = objLimit.LastUpdatedByLoginID;
                    }

                    sqlParam[13] = new SqlParameter("@Status", SqlDbType.Bit);
                    sqlParam[13].Value = objLimit.Status;

                    sqlParam[14] = new SqlParameter("@NewLimitRefID", SqlDbType.VarChar, 50);
                    sqlParam[14].Direction = ParameterDirection.Output;
                    sqlParam[15] = new SqlParameter("@ErrorCode", SqlDbType.VarChar, 50);
                    sqlParam[15].Direction = ParameterDirection.Output;

                    sqlParam[16] = new SqlParameter("@IsRecursive", SqlDbType.Bit);
                    sqlParam[16].Value = objLimit.IsRecursive;

                    sqlParam[17] = new SqlParameter("@BundleId", SqlDbType.BigInt);
                    sqlParam[17].Value = objLimit.BundleServiceId;

                    SqlHelper.ExecuteNonQuery("HelpDesk_SaveDefaultChargeLimitDetails", ref sqlParam, connectionString);

                    if (sqlParam[15] != null && sqlParam[15].Value.ToString() != string.Empty)
                        errMsg = sqlParam[15].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                errMsg = "EXCEPTION:ErrorMessage:" + ex.Message + "; StackTrace:" + ex.StackTrace;
            }
        }

        public List<ChargeLimit> GetDefaultChargeLimitDetails(string LimitCategory, string DirectBillingTelcoIDs)
        {
            List<ChargeLimit> lstLimits = new List<ChargeLimit>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@LimitCategory", SqlDbType.VarChar, 50);
            sqlParam[0].Value = LimitCategory;
            sqlParam[1] = new SqlParameter("@DirectBillingTelcoIDs", SqlDbType.NVarChar, -1);
            sqlParam[1].Value = DirectBillingTelcoIDs;
            SqlDataReader dr = SqlHelper.ExecuteReader("HelpDesk_GetDefaultChargeLimitDetails", ref sqlParam, connectionString);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ChargeLimit objLimit = new ChargeLimit();
                    objLimit.LimitID = Convert.ToInt32(dr["LimitID"].ToString());
                    objLimit.MSISDN = dr["MSISDN"].ToString();
                    objLimit.TelcoID = Convert.ToInt32(dr["TelcoID"].ToString());
                    objLimit.TelcoName = dr["TelcoName"].ToString();
                    objLimit.LookupID = Convert.ToInt32(dr["LookupID"].ToString());
                    objLimit.ServiceSourceType = dr["ServiceSourceType"].ToString();
                    objLimit.ServiceName = dr["ServiceName"].ToString();
                    objLimit.Shortcode = dr["Shortcode"].ToString();
                    objLimit.Keywords = dr["Keywords"].ToString();
                    objLimit.ChargeLimitTypeID = Convert.ToInt32(dr["ChargeLimitTypeID"].ToString());
                    objLimit.ChargeLimitTypeCode = dr["ChargeLimitTypeCode"].ToString();
                    objLimit.ChargeLimitAmount = Convert.ToDecimal(dr["ChargeLimitAmount"].ToString());
                    objLimit.ChargeLimitAmount_Prepaid = Convert.ToDecimal(dr["ChargeLimitAmount_Prepaid"].ToString());
                    objLimit.ChargeLimitAmount_Postpaid = Convert.ToDecimal(dr["ChargeLimitAmount_Postpaid"].ToString());
                    objLimit.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"].ToString());
                    objLimit.CreatedByLoginID = Convert.ToInt32(dr["CreatedByLoginID"].ToString());
                    objLimit.CreatedByLoginName = dr["CreatedByLoginName"].ToString();
                    objLimit.LastUpdatedDateTime = Convert.ToDateTime(dr["LastUpdatedDateTime"].ToString());
                    objLimit.LastUpdatedByLoginID = Convert.ToInt32(dr["LastUpdatedByLoginID"].ToString());
                    objLimit.LastUpdatedByLoginName = dr["LastUpdatedByLoginName"].ToString();
                    objLimit.Status = Convert.ToBoolean(dr["Status"].ToString());
                    objLimit.StatusText = dr["StatusText"].ToString();
                    objLimit.FromDate = Convert.ToDateTime(dr["FromDate"].ToString());
                    objLimit.ToDate = Convert.ToDateTime(dr["ToDate"].ToString());
                    objLimit.LimitCategory = dr["LimitCategory"].ToString();
                    objLimit.LimitRefID = objLimit.LimitID.ToString() + "_" + objLimit.ChargeLimitTypeID.ToString();
                    objLimit.DateRangeText = "-NA-";
                    objLimit.IsRecursiveText = "-NA-";
                    if (objLimit.ChargeLimitTypeID == 4)
                    {
                        if (objLimit.FromDate.Year <= 1900 || objLimit.ToDate.Year <= 1900)
                        {
                            objLimit.DateRangeText = "ERROR";
                        }
                        else
                        {
                            objLimit.DateRangeText = objLimit.FromDate.ToString("dd/MM/yyyy HH:mm") + " to " + objLimit.ToDate.ToString("dd/MM/yyyy HH:mm");
                            objLimit.DateRangeText = objLimit.DateRangeText.Replace(" ", "&nbsp;");
                        }

                        objLimit.IsRecursive = Convert.ToBoolean(dr["IsRecursive"].ToString());
                        objLimit.IsRecursiveText = dr["IsRecursiveText"].ToString();

                    }
                    objLimit.BundleServiceId = Convert.ToInt64(dr["BundleServiceId"].ToString());
                    lstLimits.Add(objLimit);
                }
            }
            connection.Close();
            return lstLimits;
        }

        public List<ChargeLimitType> GetChargeLimitTypes()
        {
            DataTable dtLimitTypes = new DataTable();
            List<ChargeLimitType> lstLimitTypes = new List<ChargeLimitType>();
            dtLimitTypes = SqlHelper.ExecuteDataTable("HelpDesk_GetChargeLimitTypes", connectionString);
            if (dtLimitTypes != null && dtLimitTypes.Rows.Count > 0)
            {
                foreach (DataRow row in dtLimitTypes.Rows)
                {
                    ChargeLimitType obLimitType = new ChargeLimitType();
                    obLimitType.LimitTypeID = Convert.ToInt32(row["LimitTypeID"]);
                    obLimitType.LimitTypeCode = row["LimitTypeCode"].ToString();
                    obLimitType.LimitTypeDescription = row["LimitTypeDescription"].ToString();
                    lstLimitTypes.Add(obLimitType);
                }
            }
            return lstLimitTypes;
        }

        private void FillDetails(DataSet dsList, List<PriceLimitConfiguration> objList)
        {
            objList.Clear();
            for (int i = 0; i < dsList.Tables[0].Rows.Count; i++)
            {
                DataTable dt = dsList.Tables[0];
                PriceLimitConfiguration objLimit = new PriceLimitConfiguration();

                objLimit.ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                objLimit.MSISDN = dt.Rows[i]["MSISDN"].ToString();
                objLimit.Telco_ID = Convert.ToInt32(dt.Rows[i]["Telco_ID"]);
                objLimit.Telco_Name = dt.Rows[i]["Telco_Name"].ToString();
                objLimit.Daily_Price_Limit = Convert.ToDecimal(dt.Rows[i]["Daily_Price_Limit"]);
                objLimit.Daily_Available_Price = Convert.ToDecimal(dt.Rows[i]["Daily_Available_Price"]);
                objLimit.Daily_Available_Price_LastUpdatedDateTime = Convert.ToDateTime(dt.Rows[i]["Daily_Available_Price_LastUpdatedDateTime"]);
                objLimit.Monthly_Price_Limit = Convert.ToDecimal(dt.Rows[i]["Monthly_Price_Limit"]);
                objLimit.Monthly_Available_Price = Convert.ToDecimal(dt.Rows[i]["Monthly_Available_Price"]);
                objLimit.Monthly_Available_Price_LastUpdatedDateTime = Convert.ToDateTime(dt.Rows[i]["Monthly_Available_Price_LastUpdatedDateTime"]);
                objLimit.CreatedByLoginID = Convert.ToInt32(dt.Rows[i]["CreatedBy"]);
                objLimit.CreatedByLoginName = dt.Rows[i]["CreatedByLoginName"].ToString();
                objLimit.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]);
                objLimit.LastUpdatedByLoginID = Convert.ToInt32(dt.Rows[i]["LastUpdatedBy"]);
                objLimit.LastUpdatedByLoginName = dt.Rows[i]["LastUpdatedByLoginName"].ToString();
                objLimit.LastUpdatedDate = Convert.ToDateTime(dt.Rows[i]["LastUpdatedDate"]);
                //objLimit.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                //objLimit.StatusText = "undefined";

                objList.Add(objLimit);
            }
        }

        public List<PriceLimitConfiguration> GetServicePriceLimit(string DirectBillingTelcoIDs)
        {
            List<PriceLimitConfiguration> lsttelcos = new List<PriceLimitConfiguration>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@DirectBillingTelcoIDs", SqlDbType.NVarChar, -1);
            sqlParam[0].Value = DirectBillingTelcoIDs;
            SqlDataReader dr = SqlHelper.ExecuteReader("HelpDesk_GetTelcoPriceLimit", ref sqlParam, connectionString);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    PriceLimitConfiguration objtelcos = new PriceLimitConfiguration();
                    objtelcos.Telco_ID = Convert.ToInt32(dr["Telco_ID"].ToString());
                    objtelcos.Telco_Name = dr["Telco_Name"].ToString();
                    objtelcos.Daily_Price_Limit = Convert.ToDecimal(dr["Daily_Price_Limit"].ToString());
                    objtelcos.Monthly_Price_Limit = Convert.ToDecimal(dr["Monthly_Price_Limit"].ToString());
                    objtelcos.IsChargeLimitAllowed = Convert.ToBoolean(dr["IsChargeLimitAllowed"].ToString());
                    if (objtelcos.IsChargeLimitAllowed)
                        objtelcos.IsChargeLimitAppliedText = "Yes";
                    else
                        objtelcos.IsChargeLimitAppliedText = "No";
                    lsttelcos.Add(objtelcos);
                }
            }
            connection.Close();
            return lsttelcos;
        }

        public List<PriceLimitConfiguration> GetTelcoPriceLimit(string DirectBillingTelcoIDs)
        {
            List<PriceLimitConfiguration> lsttelcos = new List<PriceLimitConfiguration>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@DirectBillingTelcoIDs", SqlDbType.NVarChar, -1);
            sqlParam[0].Value = DirectBillingTelcoIDs;
            SqlDataReader dr = SqlHelper.ExecuteReader("HelpDesk_GetTelcoPriceLimit", ref sqlParam, connectionString);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    PriceLimitConfiguration objtelcos = new PriceLimitConfiguration();
                    objtelcos.Telco_ID = Convert.ToInt32(dr["Telco_ID"].ToString());
                    objtelcos.Telco_Name = dr["Telco_Name"].ToString();
                    objtelcos.Daily_Price_Limit = Convert.ToDecimal(dr["Daily_Price_Limit"].ToString());
                    objtelcos.Monthly_Price_Limit = Convert.ToDecimal(dr["Monthly_Price_Limit"].ToString());
                    objtelcos.IsChargeLimitAllowed = Convert.ToBoolean(dr["IsChargeLimitAllowed"].ToString());
                    if (objtelcos.IsChargeLimitAllowed)
                        objtelcos.IsChargeLimitAppliedText = "Yes";
                    else
                        objtelcos.IsChargeLimitAppliedText = "No";
                    lsttelcos.Add(objtelcos);
                }
            }
            connection.Close();
            return lsttelcos;
        }

        public List<PriceLimitConfiguration> GetMSISDNChargeLimitDetails()
        {
            List<PriceLimitConfiguration> objList = new List<PriceLimitConfiguration>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataSet dsList = SqlHelper.ExecuteDataSet("HelpDesk_GetMsisdnPriceLimitDetails", connectionString);
                FillDetails(dsList, objList);
                return (objList);
            }
        }

        public void SaveTelcoPriceLimitDetails(PriceLimitConfiguration objLimit)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@Telco_ID", SqlDbType.Int);
                sqlParam[0].Value = objLimit.Telco_ID;
                sqlParam[1] = new SqlParameter("@IsChargeLimitApplied", SqlDbType.Bit);
                sqlParam[1].Value = objLimit.IsChargeLimitAllowed;
                sqlParam[2] = new SqlParameter("@Daily_Price_Limit", SqlDbType.Money);
                sqlParam[2].Value = objLimit.Daily_Price_Limit;
                sqlParam[3] = new SqlParameter("@Monthly_Price_Limit", SqlDbType.Money);
                sqlParam[3].Value = objLimit.Monthly_Price_Limit;
                sqlParam[4] = new SqlParameter("@LoginID", SqlDbType.Int);
                sqlParam[4].Value = objLimit.CreatedByLoginID;

                SqlHelper.ExecuteNonQuery("HelpDesk_SaveTelcoPriceLimitDetails", ref sqlParam, connectionString);
            }
        }


        #endregion

        #region BlockedServiceMsisdns

        public List<BlockedServiceMsisdnsBO> GetBlockedServiceMsisdns()
        {
            List<BlockedServiceMsisdnsBO> objList = new List<BlockedServiceMsisdnsBO>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataSet dsList = SqlHelper.ExecuteDataSet("HelpDesk_GetBlockedServiceMsisdns", connectionString);
                FillBlockedMsisdns(dsList, objList);
                return (objList);
            }
        }

        private void FillBlockedMsisdns(DataSet dsList, List<BlockedServiceMsisdnsBO> objList)
        {
            objList.Clear();
            for (int i = 0; i < dsList.Tables[0].Rows.Count; i++)
            {
                DataTable dt = dsList.Tables[0];
                BlockedServiceMsisdnsBO obj = new BlockedServiceMsisdnsBO();

                obj.BlockedID = Convert.ToInt64(dt.Rows[i]["BlockedID"]);
                obj.Msisdn = dt.Rows[i]["MSISDN"].ToString();
                obj.BlockedSource = dt.Rows[i]["BlockedSource"].ToString();
                obj.BlockedDateTime = Convert.ToDateTime(dt.Rows[i]["BlockedDateTime"]);
                obj.LookupID = Convert.ToInt64(dt.Rows[i]["LookupID"]);
                obj.IsBlocked = Convert.ToBoolean(dt.Rows[i]["IsBlocked"]);
                obj.Remarks = dt.Rows[i]["Remarks"].ToString();
                obj.SubscriptionID = Convert.ToInt32(dt.Rows[i]["Subscription_Id"]);
                obj.LoginName = dt.Rows[i]["LoginName"].ToString();
                obj.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                obj.BillingType = dt.Rows[i]["BillingType"].ToString();
                obj.Price = Convert.ToDecimal(dt.Rows[i]["Price"].ToString());
                obj.TelcoName = dt.Rows[i]["Telco_Name"].ToString();
                obj.Shortcode = dt.Rows[i]["ShortCode"].ToString();
                obj.Keywords = dt.Rows[i]["Keyword"].ToString();
                objList.Add(obj);
            }
        }

        public void UnblockServiceMsisdns(int LoginID, string BlockedIds)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@LoginID", SqlDbType.Int);
                sqlParam[0].Value = LoginID;
                sqlParam[1] = new SqlParameter("@BlockedIds", SqlDbType.NVarChar, -1);
                sqlParam[1].Value = BlockedIds;

                SqlHelper.ExecuteNonQuery("HelpDesk_UnblockServiceMsisdns", ref sqlParam, connectionString);
            }
        }

        #endregion

        public DataTable GetOperatorServiceSource()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataSet serviceTypes = SqlHelper.ExecuteDataSet("HelpDesk_GetOperatorServiceSourceMstData", connectionString);
                return serviceTypes.Tables[0];
            }
        }

        public DataTable GetServiceMsisdnsAllowedSuccessTransSourceMst()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataSet serviceTypes = SqlHelper.ExecuteDataSet("HelpDesk_GetServiceMSISDNs_AllowedSuccessTrans_Source_Mst", connectionString);
                return serviceTypes.Tables[0];
            }
        }
        public DataTable GetRestrictedWordsByTelco(int Telco_Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Telco_Id", SqlDbType.Int);
                sqlParam[0].Value = Telco_Id;
                DataSet restrictedWords = SqlHelper.ExecuteDataSet("HelpDesk_GetRestrictedWordsByTelco", ref sqlParam, connectionString);
                return restrictedWords.Tables[0];

            }

        }
        public string SaveRestrictedWords(int Telco_Id, string RestrictedWords, int @CreatedBy)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@Telco_Id", SqlDbType.Int);
                sqlParam[0].Value = Telco_Id;
                sqlParam[1] = new SqlParameter("@RestrictedWords", SqlDbType.NVarChar, -1);
                sqlParam[1].Value = RestrictedWords;
                sqlParam[2] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                sqlParam[2].Value = @CreatedBy;
                sqlParam[3] = new SqlParameter("@ExistingRestrictedWrds", SqlDbType.NVarChar, -1);
                sqlParam[3].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery("HelpDesk_InsertRestrictedWords", ref sqlParam, connectionString);
                return Convert.ToString(sqlParam[3].Value);
            }

        }
        public int DeleteRestrictedWordsByTelcoId(int Telco_Id, string RestrictedWords)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@Telco_Id", SqlDbType.Int);
                sqlParam[0].Value = Telco_Id;
                sqlParam[1] = new SqlParameter("@RestrictedWords", SqlDbType.NVarChar, -1);
                sqlParam[1].Value = RestrictedWords;
                return SqlHelper.ExecuteNonQuery("HelpDesk_DeleteRestrictedWords", ref sqlParam, connectionString);

            }
        }
        public DataTable GetServiceTypeByShortCode(int TelcoId, int ShortCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@TelcoID", SqlDbType.Int);
                sqlParam[0].Value = TelcoId;
                sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.Int);
                sqlParam[1].Value = ShortCode;
                DataSet serviceTypes = SqlHelper.ExecuteDataSet("HelpDesk_GetServiceTypeByShortcode", ref sqlParam, connectionString);
                return serviceTypes.Tables[0];

            }

        }
        public DataTable GetServicesForPriceLimitConfiguration(int TelcoId, int ShortCode, string ServiceType, string limitType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@TelcoID", SqlDbType.Int);
                sqlParam[0].Value = TelcoId;
                sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.Int);
                sqlParam[1].Value = ShortCode;
                sqlParam[2] = new SqlParameter("@ServiceType", SqlDbType.VarChar, -1);
                sqlParam[2].Value = ServiceType;
                sqlParam[3] = new SqlParameter("@LimitCategory", SqlDbType.VarChar, 50);
                sqlParam[3].Value = limitType;
                DataSet services = SqlHelper.ExecuteDataSet("HelpDesk_GetServicesForPriceLimitConfiguration", ref sqlParam, connectionString);
                return services.Tables[0];

            }

        }
        public int SaveServicesForPriceLimitConfiguration(int LookupId, decimal Daily_Price_Limit, decimal Monthly_Price_Limit, bool IsChargeLimitApplied, int LoginID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@LookupID", SqlDbType.Int);
                sqlParam[0].Value = LookupId;
                sqlParam[1] = new SqlParameter("@Daily_Price_Limit", SqlDbType.Money);
                sqlParam[1].Value = Daily_Price_Limit;
                sqlParam[2] = new SqlParameter("@Monthly_Price_Limit", SqlDbType.Money);
                sqlParam[2].Value = Monthly_Price_Limit;
                sqlParam[3] = new SqlParameter("@IsChargeLimitApplied", SqlDbType.Bit);
                sqlParam[3].Value = IsChargeLimitApplied;
                sqlParam[4] = new SqlParameter("@LoginID", SqlDbType.Int);
                sqlParam[4].Value = LoginID;
                return SqlHelper.ExecuteNonQuery("HelpDesk_SaveServicesForPriceLimitConfiguration", ref sqlParam, connectionString);

            }
        }
        public DataTable GetSubscriptionServicesForDND(int TelcoId, int ShortCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@TelcoID", SqlDbType.Int);
                sqlParam[0].Value = TelcoId;
                sqlParam[1] = new SqlParameter("@Shortcode", SqlDbType.Int);
                sqlParam[1].Value = ShortCode;
                DataSet subServices = SqlHelper.ExecuteDataSet("HelpDesk_GetSubscriptionDetailsByShortCodeTelcoIDForDND", ref sqlParam, connectionString);
                return subServices.Tables[0];

            }

        }
        public int SaveSubscriptionServicesForDND(int SubscriptionId, bool DND_ContentDelivery_Flag, TimeSpan DND_StartTime, int DND_TimeSpan_InMin, int DND_DelayAfterDND_TimeSpan_InMin)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@SubscriptionId", SqlDbType.Int);
                sqlParam[0].Value = SubscriptionId;
                sqlParam[1] = new SqlParameter("@DND_ContentDelivery_Flag", SqlDbType.Bit);
                sqlParam[1].Value = DND_ContentDelivery_Flag;
                sqlParam[2] = new SqlParameter("@DND_StartTime", SqlDbType.Time);
                sqlParam[2].Value = DND_StartTime;
                sqlParam[3] = new SqlParameter("@DND_TimeSpan_InMin", SqlDbType.Int);
                sqlParam[3].Value = DND_TimeSpan_InMin;
                sqlParam[4] = new SqlParameter("@DND_DelayAfterDND_TimeSpan_InMin", SqlDbType.Int);
                sqlParam[4].Value = DND_DelayAfterDND_TimeSpan_InMin;
                return SqlHelper.ExecuteNonQuery("HelpDesk_SaveSubscriptionDetailsByShortCodeTelcoIDForDND", ref sqlParam, connectionString);

            }
        }

        public DataTable GetMenuHeaders()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataSet dsHeaders = SqlHelper.ExecuteDataSet("HelpDesk_GetMenuHeadersList", connectionString);
                if (dsHeaders != null && dsHeaders.Tables.Count > 0)
                    return dsHeaders.Tables[0];
                else
                    return null;

            }
        }

        public DataTable GetMenuHeaderLinks(int LoginID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@LoginID", SqlDbType.Int);
                paramList[0].Value = LoginID;

                DataSet dsHeaderLinks = SqlHelper.ExecuteDataSet("HelpDesk_GetMenuHeaderLinks", ref paramList, connectionString);
                if (dsHeaderLinks != null && dsHeaderLinks.Tables.Count > 0)
                    return dsHeaderLinks.Tables[0];
                else
                    return null;

            }
        }

        public DataSet GetHeaderLinkDetailsByID(int HeaderLinkID, bool isHeader, int loginRoleID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@HeaderLinkID", SqlDbType.Int);
                sqlParam[0].Value = HeaderLinkID;
                sqlParam[1] = new SqlParameter("@IsHeader", SqlDbType.Bit);
                sqlParam[1].Value = isHeader;
                sqlParam[2] = new SqlParameter("@LoginRoleID", SqlDbType.Int);
                sqlParam[2].Value = loginRoleID;
                DataSet subServices = SqlHelper.ExecuteDataSet("HelpDesk_GetHeaderLinkDetails", ref sqlParam, connectionString);
                return subServices;

            }
        }

        public int SaveHeaderLinkDetails(int LinkID, int HeaderID, bool IsHeader, string HeaderLinkName, string LinkURL, bool HeaderLinkStatus, string ReadWriteLoginIDs, string ReadOnlyLoginIDs, string NoAccessLoginIDs, int LoginRoleID)
        {
            int SavedHeaderLinkID = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@LinkID", SqlDbType.Int);
                sqlParam[0].Value = LinkID;
                sqlParam[1] = new SqlParameter("@HeaderID", SqlDbType.Int);
                sqlParam[1].Value = HeaderID;
                sqlParam[2] = new SqlParameter("@IsHeader", SqlDbType.Bit);
                sqlParam[2].Value = IsHeader;
                sqlParam[3] = new SqlParameter("@HeaderLinkName", SqlDbType.NVarChar, 200);
                sqlParam[3].Value = HeaderLinkName;
                sqlParam[4] = new SqlParameter("@LinkURL", SqlDbType.NVarChar, 200);
                sqlParam[4].Value = LinkURL;
                sqlParam[5] = new SqlParameter("@HeaderLinkStatus", SqlDbType.Bit);
                sqlParam[5].Value = HeaderLinkStatus;
                sqlParam[6] = new SqlParameter("@ReadWriteLoginIDs", SqlDbType.NVarChar, -1);
                sqlParam[6].Value = ReadWriteLoginIDs;
                sqlParam[7] = new SqlParameter("@ReadOnlyLoginIDs", SqlDbType.NVarChar, -1);
                sqlParam[7].Value = ReadOnlyLoginIDs;
                sqlParam[8] = new SqlParameter("@NoAccessLoginIDs", SqlDbType.NVarChar, -1);
                sqlParam[8].Value = NoAccessLoginIDs;
                sqlParam[9] = new SqlParameter("@SavedHeaderLinkID", SqlDbType.Int);
                sqlParam[9].Direction = ParameterDirection.Output;
                sqlParam[10] = new SqlParameter("@LoginRoleID", SqlDbType.Int);
                sqlParam[10].Value = LoginRoleID;
                int retVal = SqlHelper.ExecuteNonQuery("HelpDesk_SaveHeaderLinkDetails", ref sqlParam, connectionString);
                if (sqlParam[9] != null && sqlParam[9].Value != null && sqlParam[9].Value != DBNull.Value)
                    SavedHeaderLinkID = Convert.ToInt32(sqlParam[9].Value);
            }
            return SavedHeaderLinkID;
        }


        public int BlacklistMsisdn(string LoginId, string msisdn, string operatorId)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@LoginId", SqlDbType.VarChar, 100);
                sqlParam[0].Value = LoginId;
                sqlParam[1] = new SqlParameter("@msisdn", SqlDbType.VarChar, 100);
                sqlParam[1].Value = msisdn;
                sqlParam[2] = new SqlParameter("@operatorId", SqlDbType.VarChar, 100);
                sqlParam[2].Value = operatorId;
                sqlParam[3] = new SqlParameter("@result", SqlDbType.Int);
                sqlParam[3].Value = result;
                sqlParam[3].Direction = ParameterDirection.Output;

                int retVal = SqlHelper.ExecuteNonQuery("MI_BlacklistMsisdn", ref sqlParam, connectionString);
                if (sqlParam[3] != null && sqlParam[3].Value != null && sqlParam[3].Value != DBNull.Value)
                    result = Convert.ToInt32(sqlParam[3].Value);
            }
            return result;
        }


        public int SaveMenuHeaders(string HeaderName, bool Status, int Rank)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@HeaderName", SqlDbType.NVarChar, -1);
                sqlParam[0].Value = HeaderName;
                sqlParam[1] = new SqlParameter("@Status", SqlDbType.Bit);
                sqlParam[1].Value = Status;
                sqlParam[2] = new SqlParameter("@Rank", SqlDbType.Int);
                sqlParam[2].Value = Rank;

                return SqlHelper.ExecuteNonQuery("HelpDesk_SaveMenuHeaders", ref sqlParam, connectionString);
            }
        }

        public int SaveMenuHeaderLinks(int HeaderID, string LinkName, string LinkURL, int Rank, bool Status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@HeaderID", SqlDbType.Int);
                sqlParam[0].Value = HeaderID;
                sqlParam[1] = new SqlParameter("@LinkName", SqlDbType.NVarChar, -1);
                sqlParam[1].Value = LinkName;
                sqlParam[2] = new SqlParameter("@LinkURL", SqlDbType.NVarChar, -1);
                sqlParam[2].Value = LinkURL;
                sqlParam[3] = new SqlParameter("@Status", SqlDbType.Bit);
                sqlParam[3].Value = Status;
                sqlParam[4] = new SqlParameter("@Rank", SqlDbType.Int);
                sqlParam[4].Value = Rank;
                return SqlHelper.ExecuteNonQuery("HelpDesk_SaveMenuHeaderLinks", ref sqlParam, connectionString);
            }
        }

        public int SaveUserActionLogs(int Login_Id, string PageName, string Action, string Remarks)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@Login_Id", SqlDbType.Int);
                sqlParam[0].Value = Login_Id;
                sqlParam[1] = new SqlParameter("@PageName", SqlDbType.NVarChar, -1);
                sqlParam[1].Value = PageName;
                sqlParam[2] = new SqlParameter("@Action", SqlDbType.NVarChar, -1);
                sqlParam[2].Value = Action;
                sqlParam[3] = new SqlParameter("@Remarks", SqlDbType.NVarChar, -1);
                sqlParam[3].Value = Remarks;
                return SqlHelper.ExecuteNonQuery("Helpdesk_InsertUserActionLogs", ref sqlParam, connectionString);
            }
        }

        public DataSet GetSTKWAPMenuWiseCount(string FromDate, string ToDate, string Menu_Type)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                sqlParam[0].Value = FromDate;
                sqlParam[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
                sqlParam[1].Value = ToDate;
                sqlParam[2] = new SqlParameter("@Menu_Type", SqlDbType.VarChar, 50);
                sqlParam[2].Value = Menu_Type;
                return SqlHelper.ExecuteDataSet("Helpdesk_GetSTKWAP_MenuWise_Count", ref sqlParam, connectionString);
            }
        }

        public DataSet GetSTKWAPMenus(string Menu_Type)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Menu_Type", SqlDbType.VarChar, 50);
                sqlParam[0].Value = Menu_Type;
                return SqlHelper.ExecuteDataSet("Helpdesk_GetSTKWAP_Menus", ref sqlParam, connectionString);
            }
        }

        public DataSet GetSTKWAPMenuWiseMSISDN(string Clicked_Date, int Clicked_MenuID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@Clicked_Date", SqlDbType.NVarChar, -1);
                sqlParam[0].Value = Clicked_Date;
                sqlParam[1] = new SqlParameter("@ClickedMenu_ID", SqlDbType.Int);
                sqlParam[1].Value = Clicked_MenuID;
                return SqlHelper.ExecuteDataSet("Helpdesk_GetSTKWAP_MenuWise_MSISDN", ref sqlParam, connectionString);
            }
        }

        public int DeleteMenuOptions(int HeaderLinkID, bool IsHeader)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@HeaderLinkID", SqlDbType.Int);
                sqlParam[0].Value = HeaderLinkID;
                sqlParam[1] = new SqlParameter("@IsHeader", SqlDbType.Int);
                sqlParam[1].Value = IsHeader;
                result = SqlHelper.ExecuteNonQuery("HelpDesk_DeleteMenuOptions", ref sqlParam, connectionString);
            }

            return result;
        }

        public void SaveHeaderLinkSortDetails(int LinkID, int HeaderID, int Rank)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@LinkID", SqlDbType.Int);
                sqlParam[0].Value = LinkID;
                sqlParam[1] = new SqlParameter("@HeaderID", SqlDbType.Int);
                sqlParam[1].Value = HeaderID;
                sqlParam[2] = new SqlParameter("@Rank", SqlDbType.Int);
                sqlParam[2].Value = Rank;
                result = SqlHelper.ExecuteNonQuery("HelpDesk_SaveHeaderLinkSortDetails", ref sqlParam, connectionString);
            }
        }
        public int SaveDCBTos(string tos, string tosVersion, string tosType, bool isValid, int CarrierTosId)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@Tos", SqlDbType.NVarChar, -1);
                sqlParam[0].Value = tos;
                sqlParam[1] = new SqlParameter("@TosVersion", SqlDbType.VarChar, 200);
                sqlParam[1].Value = tosVersion;
                sqlParam[2] = new SqlParameter("@TosType", SqlDbType.VarChar, 50);
                sqlParam[2].Value = tosType;
                sqlParam[3] = new SqlParameter("@IsValid", SqlDbType.Bit);
                sqlParam[3].Value = isValid;
                sqlParam[4] = new SqlParameter("@CarrierTosID", SqlDbType.Int);
                sqlParam[4].Value = CarrierTosId;

                result = SqlHelper.ExecuteNonQuery("DCB_BatchProc_SaveDCBTos", ref sqlParam, connectionString);
                return result;
            }
        }

        public int SaveBillingBundleCaps(string bundleName, int telcoId, string shortcode, string serviceSourceType, string billServiceIds, int createdBy, out string errorMsg)
        {
            int result = 0;
            errorMsg = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlParameter[] sqlParam = new SqlParameter[7];
                    sqlParam[0] = new SqlParameter("@BundleCapName", SqlDbType.NVarChar, -1);
                    sqlParam[0].Value = bundleName;
                    sqlParam[1] = new SqlParameter("@TelcoID", SqlDbType.Int);
                    sqlParam[1].Value = telcoId;
                    sqlParam[2] = new SqlParameter("@Shortcode", SqlDbType.VarChar, 50);
                    sqlParam[2].Value = shortcode;
                    sqlParam[3] = new SqlParameter("@ServiceSourceType", SqlDbType.VarChar, 100);
                    sqlParam[3].Value = serviceSourceType;
                    sqlParam[4] = new SqlParameter("@LookupIDs", SqlDbType.VarChar, -1);
                    sqlParam[4].Value = billServiceIds;
                    sqlParam[5] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                    sqlParam[5].Value = createdBy;
                    sqlParam[6] = new SqlParameter("@ErrorCode", SqlDbType.VarChar, 50);
                    sqlParam[6].Direction = ParameterDirection.Output;

                    result = SqlHelper.ExecuteNonQuery("HelpDesk_SaveBilingCapBundles", ref sqlParam, connectionString);
                    errorMsg = Convert.ToString(sqlParam[6].Value);

                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            return result;
        }
        public DataTable GetBundleServicesByBundleId(long bundleId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@BundleId", SqlDbType.BigInt);
                sqlParam[0].Value = bundleId;
                DataSet dsServices = SqlHelper.ExecuteDataSet("HelpDesk_GetBundleServicesByBundleId", ref sqlParam, connectionString);
                return dsServices.Tables[0];

            }

        }
        public int UpdateBundleName(string bundleName, int status, long bundleId)
        {
            int result = 0;
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@BundleId", SqlDbType.BigInt);
            sqlParam[0].Value = bundleId;
            sqlParam[1] = new SqlParameter("@BundleName", SqlDbType.NVarChar);
            sqlParam[1].Value = bundleName;
            sqlParam[2] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParam[2].Value = status;
            result = SqlHelper.ExecuteNonQuery("HelpDesk_UpdateBundleNameByBundleId", ref sqlParam, connectionString);
            return result;
        }
        public DataTable GetBundleDetails()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataSet dsServices = SqlHelper.ExecuteDataSet("HelpDesk_GetBundleDetails", connectionString);
                return dsServices.Tables[0];

            }

        }
        public int SaveEquipCommitmentDetails(string offerID, DateTime newUseractivationDate, DateTime newUserexpiryDate, DateTime existingUserActivationDate, DateTime existingUserExpiryDate, int discServicesCount, string targetedSubscriberFlag, int telcoId, string dynExpTextEng, string dynExpTextArb, string status, string subscriptionDetails, string vasPromMessages, out string responseCode, out long equipCommitId)
        {
            int result = 0;
            responseCode = string.Empty;
            equipCommitId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlParameter[] sqlParam = new SqlParameter[15];
                    sqlParam[0] = new SqlParameter("@OfferID", SqlDbType.NVarChar, -1);
                    sqlParam[0].Value = offerID;
                    sqlParam[1] = new SqlParameter("@NewUserActivationDate", SqlDbType.DateTime);
                    sqlParam[1].Value = newUseractivationDate;
                    sqlParam[2] = new SqlParameter("@NewUserExpiryDate", SqlDbType.DateTime);
                    sqlParam[2].Value = newUserexpiryDate;
                    sqlParam[3] = new SqlParameter("@ExistingUserActivationDate", SqlDbType.DateTime);
                    sqlParam[3].Value = existingUserActivationDate;
                    sqlParam[4] = new SqlParameter("@ExistingUserExpiryDate", SqlDbType.DateTime);
                    sqlParam[4].Value = existingUserExpiryDate;
                    //sqlParam[1] = new SqlParameter("@Commitmentid", SqlDbType.NVarChar, -1);
                    //sqlParam[1].Value = commitId;
                    sqlParam[5] = new SqlParameter("@DiscServicesCount", SqlDbType.Int);
                    sqlParam[5].Value = discServicesCount;
                    sqlParam[6] = new SqlParameter("@TargetedSubscriberFlag", SqlDbType.VarChar, 50);
                    sqlParam[6].Value = targetedSubscriberFlag;
                    sqlParam[7] = new SqlParameter("@Status", SqlDbType.VarChar, 100);
                    sqlParam[7].Value = status;
                    sqlParam[8] = new SqlParameter("@Subscriptions", SqlDbType.NVarChar, -1);
                    sqlParam[8].Value = @subscriptionDetails;
                    sqlParam[9] = new SqlParameter("@Telcoid", SqlDbType.Int);
                    sqlParam[9].Value = telcoId;
                    sqlParam[10] = new SqlParameter("@DynExpTextEng", SqlDbType.NVarChar, -1);
                    sqlParam[10].Value = dynExpTextEng;
                    sqlParam[11] = new SqlParameter("@DynExpTextArb", SqlDbType.NVarChar, -1);
                    sqlParam[11].Value = dynExpTextArb;
                    //sqlParam[12] = new SqlParameter("@TargetedSubFlag", SqlDbType.VarChar, 50);
                    //sqlParam[12].Value = targSubFlag;
                    sqlParam[12] = new SqlParameter("@VasPromMessages", SqlDbType.NVarChar, -1);
                    sqlParam[12].Value = vasPromMessages;
                    sqlParam[13] = new SqlParameter("@ResponseCode", SqlDbType.Int);
                    sqlParam[13].Direction = ParameterDirection.Output;
                    sqlParam[14] = new SqlParameter("@EquipCommitmentId", SqlDbType.BigInt);
                    sqlParam[14].Direction = ParameterDirection.Output;
                    result = SqlHelper.ExecuteNonQuery("HelpDesk_InsertEquipCommitmentDetails", ref sqlParam, connectionString);
                    responseCode = sqlParam[13].Value.ToString();
                    equipCommitId = Convert.ToInt64(sqlParam[14].Value);
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public DataSet GetEquipCommitmentDetails(long equipCommitId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@EquipCommitmentID", SqlDbType.BigInt);
                sqlParam[0].Value = equipCommitId;
                DataSet dsServices = SqlHelper.ExecuteDataSet("HelpDesk_GetEquipCommitmentDetails", ref sqlParam, connectionString);
                return dsServices;

            }

        }

        public int UpdateEquipCommitmentDetails(long equipCommitmentId, string offerID, DateTime newUseractivationDate, DateTime newUserexpiryDate, DateTime existingUserActivationDate, DateTime existingUserExpiryDate, int telcoId, int discountedServicesCount, string targetedSubscriberFlag, string DynamicExplanatoryText_Eng, string dynamicExplanatoryText_Ar, string status, string subscriptionDetails, string vasPromotionMessages, out string responseCode)
        {
            int result = 0;
            responseCode = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlParameter[] sqlParam = new SqlParameter[15];
                    sqlParam[0] = new SqlParameter("@EquipCommitId", SqlDbType.BigInt);
                    sqlParam[0].Value = equipCommitmentId;
                    sqlParam[1] = new SqlParameter("@OfferID", SqlDbType.NVarChar, -1);
                    sqlParam[1].Value = offerID;
                    sqlParam[2] = new SqlParameter("@NewUserActivationDate", SqlDbType.DateTime);
                    sqlParam[2].Value = newUseractivationDate;
                    sqlParam[3] = new SqlParameter("@NewUserExpiryDate", SqlDbType.DateTime);
                    sqlParam[3].Value = newUserexpiryDate;
                    sqlParam[4] = new SqlParameter("@ExistingUserActivationDate", SqlDbType.DateTime);
                    sqlParam[4].Value = existingUserActivationDate;
                    sqlParam[5] = new SqlParameter("@ExistingUserExpiryDate", SqlDbType.DateTime);
                    sqlParam[5].Value = existingUserExpiryDate;
                    sqlParam[6] = new SqlParameter("@TelcoId", SqlDbType.Int);
                    sqlParam[6].Value = telcoId;
                    sqlParam[7] = new SqlParameter("@DiscountedServicesCount", SqlDbType.Int);
                    sqlParam[7].Value = discountedServicesCount;
                    sqlParam[8] = new SqlParameter("@TargetedSubscriberFlag", SqlDbType.VarChar, 50);
                    sqlParam[8].Value = targetedSubscriberFlag;
                    sqlParam[9] = new SqlParameter("@DynamicExplanatoryText_Eng", SqlDbType.NVarChar, -1);
                    sqlParam[9].Value = DynamicExplanatoryText_Eng;
                    sqlParam[10] = new SqlParameter("@DynamicExplanatoryText_Ar", SqlDbType.NVarChar, -1);
                    sqlParam[10].Value = dynamicExplanatoryText_Ar;
                    sqlParam[11] = new SqlParameter("@Status", SqlDbType.VarChar, 100);
                    sqlParam[11].Value = status;
                    sqlParam[12] = new SqlParameter("@Subscriptions", SqlDbType.NVarChar, -1);
                    sqlParam[12].Value = subscriptionDetails;
                    sqlParam[13] = new SqlParameter("@VasPromMessages", SqlDbType.NVarChar, -1);
                    sqlParam[13].Value = vasPromotionMessages;
                    sqlParam[14] = new SqlParameter("@ResponseCode", SqlDbType.Int);
                    sqlParam[14].Direction = ParameterDirection.Output;
                    result = SqlHelper.ExecuteNonQuery("HelpDesk_UpdateEquipCommitmentDetails", ref sqlParam, connectionString);
                    responseCode = sqlParam[14].Value.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public DataSet GetPromotionsDetails()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataSet dsServices = SqlHelper.ExecuteDataSet("HelpDesk_GetPromotionsDetails", connectionString);
                return dsServices;

            }

        }

        public DataSet GetPromotionSubscriptionDetails(long equipCommitId, out bool userExists)
        {
            userExists = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@EquipCommitmentId", SqlDbType.BigInt);
                sqlParam[0].Value = equipCommitId;
                sqlParam[1] = new SqlParameter("@UserExists", SqlDbType.Bit);
                sqlParam[1].Direction = ParameterDirection.Output;
                DataSet dsServices = SqlHelper.ExecuteDataSet("HelpDesk_GetPromoitonalSubscriptions", ref sqlParam, connectionString);
                userExists = Convert.ToBoolean(sqlParam[1].Value);
                return dsServices;

            }
        }
        public DataSet GetVasPromotionTelco(string vasPromotionTelcoids)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@VasPromotionTelcoIDs", SqlDbType.NVarChar, -1);
                sqlParam[0].Value = vasPromotionTelcoids;
                DataSet dsServices = SqlHelper.ExecuteDataSet("HelpDesk_GetVasPromotionTelcos", ref sqlParam, connectionString);
                return dsServices;

            }
        }

        public DataSet GetServicePriceByLookupId(int telcoID, string shortcode, int lookupId, out string servicePrice)
        {
            DataSet ds = new DataSet();
            servicePrice = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@TELCOID", SqlDbType.Int);
                sqlParam[0].Value = telcoID;
                sqlParam[1] = new SqlParameter("@SHORTCODE", SqlDbType.VarChar, 50);
                sqlParam[1].Value = shortcode;
                sqlParam[2] = new SqlParameter("@LookupId", SqlDbType.Int);
                sqlParam[2].Value = lookupId;
                sqlParam[3] = new SqlParameter("@ServicePrice", SqlDbType.Money);
                sqlParam[3].Direction = ParameterDirection.Output;
                DataSet dsServices = SqlHelper.ExecuteDataSet("HelpDesk_GetServicePricesByLookupId", ref sqlParam, connectionString);
                servicePrice = sqlParam[3].Value.ToString();

                return dsServices;

            }
        }
        public DataSet GetVasPromMessagesByLookupId(int planId, int lookupId, out long planServiceId)
        {
            DataSet ds = new DataSet();
            planServiceId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@LookupId", SqlDbType.Int);
                sqlParam[0].Value = lookupId;
                sqlParam[1] = new SqlParameter("@PlanId", SqlDbType.Int);
                sqlParam[1].Value = planId;
                sqlParam[2] = new SqlParameter("@PlanServiceId", SqlDbType.BigInt);
                sqlParam[2].Direction = ParameterDirection.Output;
                ds = SqlHelper.ExecuteDataSet("HelpDesk_GetVasPromotionMessagesByLookupId", ref sqlParam, connectionString);
                planServiceId = Convert.ToInt32(sqlParam[2].Value);
                return ds;
            }
        }
        public string GetBillingXMLByEquipCommitID(int planId, int lookupID)
        {
            int result = 0;
            string billingXML = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@PlanID", SqlDbType.Int);
                sqlParam[0].Value = planId;
                sqlParam[1] = new SqlParameter("@LookUpId", SqlDbType.Int);
                sqlParam[1].Value = lookupID;
                sqlParam[2] = new SqlParameter("@BillingXML", SqlDbType.VarChar, 8000);
                sqlParam[2].Direction = ParameterDirection.Output;
                result = SqlHelper.ExecuteNonQuery("CPG_GetBillingXMLByPlanID", ref sqlParam, connectionString);
                billingXML = sqlParam[2].Value.ToString();
                return billingXML;
            }
        }
    }

}
