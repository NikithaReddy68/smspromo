using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntigralHelpDeskCore.BO;

namespace HelpDeskCore
{
    public class BAL
    {
        private string connectionString = string.Empty;

        public BAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int DeletePostpaidNumbers(string PostpaidNumbers, int DeletedBy)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.DeletePostpaidNumbers(PostpaidNumbers, DeletedBy);
        }
        public DataSet GetServiceDetails(string shortcode, string keyword)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetServiceDetails(shortcode, keyword);
        }
        public int UpdateServiceName(int telcoId, string shortcode, int serviceid, string servicename)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.UpdateServiceName(telcoId, shortcode, serviceid, servicename);
        }
        public int UpdateServicePrice(int telcoId, string shortcode, int serviceid, string serviceSourceType, string Price)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.UpdateServicePrice(telcoId, shortcode, serviceid, serviceSourceType, Price);
        }

        public int SaveCommonLookupData(int telcoId, string shortcode, int serviceid, string serviceSourceType, BillingInfo[] BillServiceId, int LookupID)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.SaveCommonLookupData(telcoId, shortcode, serviceid, serviceSourceType, BillServiceId, LookupID);
        }
        public int UpdateKeyword(string Serviceid, string shortcode, string keyword)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.UpdateKeyword(Serviceid, shortcode, keyword);
        }
        public int InsertPostpaidNumbers(string PostpaidNumbers, bool DeleteAllExisting, int AddedBy)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.InsertPostpaidNumbers(PostpaidNumbers, DeleteAllExisting, AddedBy);
        }


        public List<UserDetailsBO> GetLoginNames()
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetLoginNames();

        }

        public int AddToFreeNumber(int loginId, string mobileNumber, int telcoId, string shortcode, int billserviceId)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.AddToFreeNumber(loginId, mobileNumber, telcoId, shortcode, billserviceId);

        }

        public int DeleteFromFreeNumber(int loginId, string mobileNumber, int telcoId, string shortcode, int billserviceId)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.DeleteFromFreeNumber(loginId, mobileNumber, telcoId, shortcode, billserviceId);

        }

        public List<UserDetailsBO> GetCPGUserDetails(string loginName)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetCPGUserDetails(loginName);
        }

        public UserDetailsBO UpdateUserDetails(string loginName, string mobilenumber, string email, string password, string newloginName, string APIPassword, string firstName, string lastName, string companyName, bool pinBySMS, bool pinByEmail)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.UpdateUserDetails(loginName, mobilenumber, email, password, newloginName, APIPassword, firstName, lastName, companyName, pinBySMS, pinByEmail);

        }

        public List<ChangeSubscriberServicesBO> GetAllTelcos()
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetAllTelcos();
        }
        public List<ChangeSubscriberServicesBO> GetLookupTelcos(string ServiceSourceType)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetLookupTelcos(ServiceSourceType);
        }
        public List<ChangeSubscriberServicesBO> GetShortcodesByTelcoID(int telcoid)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetShortcodesByTelcoID(telcoid);
        }
        public List<ChangeSubscriberServicesBO> GetLookupShortcodesByTelcoID(int TelcoID, string ServiceSourceType)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetLookupShortcodesByTelcoID(TelcoID, ServiceSourceType);
        }



        public List<LookupKeywordBO> GetLookupKeywords(int TelcoID, string Shortcode, string ServiceSourceType)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetLookupKeywords(TelcoID, Shortcode, ServiceSourceType);
        }
        public List<LookupKeywordBO> GetLookupKeywordsPRICESourceType(int TelcoID, string Shortcode)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetLookupKeywordsPRICESourceType(TelcoID, Shortcode);
        }
        public List<PriceListBO> GetPricesByTelco(int TelcoID, string Shortcode)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetPricesByTelco(TelcoID, Shortcode);
        }
        public List<PriceListBO> GetPriceCodesById(int PriceId)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetPriceCodesById(PriceId);
        }


        public List<ChangeSubscriberServicesBO> GetServicesByShortcodes(int telcoid, int shortcode)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetServicesByShortcodes(telcoid, shortcode);
        }
        public List<ChangeSubscriberServicesBO> GetServicesByCPID(int telcoid, int CPID)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetServicesByCPID(telcoid,CPID);
        }
        public DataSet GetCallBacks(int telcoid, int CPID,string serviceID,string fromDate,string ToDate)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetCallBacks( telcoid,  CPID, serviceID, fromDate, ToDate);
        }
        public DataSet GetSummary(string fromDate, string ToDate, string rptType)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetSummary(fromDate, ToDate, rptType);
        }
        public DataSet GetChargeLogs(int telcoid, int CPID, string serviceID, string fromDate, string ToDate)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetChargeLogs(telcoid, CPID, serviceID, fromDate, ToDate);
        }
        public List<ChangeSubscriberServicesBO> GetkeywordsByServiceID(int serviceid)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetkeywordsByServiceID(serviceid);
        }

        public string GetLookupkeywordsByServiceID(int telcoId, string shortcode, int serviceId)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetLookupkeywordsByServiceID(telcoId, shortcode, serviceId);
        }

        public string GetInUseCommonLookupKeywords(string ShortCode, string Keywords, int TelcoID, int LookupID)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetInUseCommonLookupKeywords(ShortCode, Keywords, TelcoID, LookupID);
        }

        public int InsertNewSubscriptionKeywords(string TotKeywords, string newKeyword, int lookupId, int subscriptionid, int languageid)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.InsertNewSubscriptionKeywords(TotKeywords, newKeyword, lookupId, subscriptionid, languageid);
        }
        public int ChangeSubscriberServices(int OLDTelco_ID, int OldServiceID, int OldKeywordID, int NewTelco_ID, int NewServiceID, int NewKeywordID)
        {
            DAL objDB = new DAL(connectionString);
            int EffectedRows = objDB.ChangeSubscriberServices(OLDTelco_ID, OldServiceID, OldKeywordID, NewTelco_ID, NewServiceID, NewKeywordID);
            return EffectedRows;
        }
        public List<ChangeSubscriberServicesBO> GetKeywordsByShortcode(string shortcode)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetKeywordsByShortcode(shortcode);
        }
        public int Unsubscribeusers(int TelcoID, int Shortcode, string MSISDN, int ServiceID, out int PremiumUnsubCount, out int NonPremiumUnsubCount)
        {
            PremiumUnsubCount = 0;
            NonPremiumUnsubCount = 0;
            DAL objDB = new DAL(connectionString);
            int EffectedRows = objDB.Unsubscribeusers(TelcoID, Shortcode, MSISDN, ServiceID, out PremiumUnsubCount, out NonPremiumUnsubCount);
            return EffectedRows;
        }
        public UserBO GetUserDetails(int LoginID)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetUserDetails(LoginID);
        }
        public UserBO GetUserDetails(string LoginName)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetUserDetails(LoginName);
        }
        public UserBO GetMICCUserDetails(string LoginName)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetMICCUserDetails(LoginName);
        }

        public string GetServiceNameByKeyword(int TelcoId, string Shortcode, int ServiceId, string ServiceType)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetServiceNameByKeyword(TelcoId, Shortcode, ServiceId, ServiceType);

        }
        public List<ContentBO> GetContentTypes()
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetContentTypes();

        }
        public List<ContentBO> GetContentDetails(string subscriptionId, string contentTypeId)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetContentDetails(subscriptionId, contentTypeId);

        }
        public DataTable GetFinanceCategories()
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetFinanceCategories();
        }
        public List<ChangeSubscriberServicesBO> GetKeywordsByShortcode_ContentDownload(string shortcode)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetKeywordsByShortcode_ContentDownload(shortcode);
        }
        public DataTable GetServiceTypes()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetServiceTypes();
        }
        public DataTable GetServiceDetailsByServiceType(string TelcoId, string shortcode, string ServiceType)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetServiceDetailsByServiceType(TelcoId, shortcode, ServiceType);
        }
        public int UpdateFinanceCategory(int FinanceCatId, string Lookupids)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.UpdateFinanceCategory(FinanceCatId, Lookupids);
        }
        #region Logic for to send message to Configured queue for unsubscribed users who has notification urls
        public List<MessageQueueDetails> GetAllDataToPostMessage(int TelcoID, int Shortcode, string MSISDN, int ServiceID)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetAllDataToPostMessage(TelcoID, Shortcode, MSISDN, ServiceID);

        }
        public List<MessageQueueDetails> GetAllDataToPostMessage(string MSISDN)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetAllDataToPostMessage(MSISDN);

        }
        public List<MessageQueueDetails> GetAllRenewalFailedServiceMSISDNs(int Days)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetAllRenewalFailedServiceMSISDNs(Days);

        }

        public int UnsubscribeUsersBySubscriptions(string subscriptions)
        {
            DAL objDB = new DAL(connectionString);
            int EffectedRows = objDB.UnsubscribeUsersBySubscriptions(subscriptions);
            return EffectedRows;
        }
        #endregion

        public List<StopBillingBO> GetBillableServicesForShortcode(int TelcoID, string Shortcode, bool showZeroPriceRec)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetBillableServicesForShortcode(TelcoID, Shortcode, showZeroPriceRec);
        }
        public bool StopBillingServicesForShortcode(int TelcoID, string Shortcode)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.StopBillingServicesForShortcode(TelcoID, Shortcode);
        }

        public List<StopServicesBO> GetActiveServicesForShortcode(int TelcoID, string Shortcode)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetActiveServicesForShortcode(TelcoID, Shortcode);
        }

        public bool InactiveShortcodeServices(int TelcoID, string Shortcode)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.InactiveShortcodeServices(TelcoID, Shortcode);
        }
        public DataSet GetSubscriptionParamaters(int Telcoid, string shortcode, string Keywords)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetSubscriptionParamaters(Telcoid, shortcode, Keywords);
        }
        public DataSet GetSubscriptionDetails(int Telcoid, string shortcode, string Keyword)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetSubscriptionDetails(Telcoid, shortcode, Keyword);
        }
        public DataSet GetDetailsOfAllLogins(int cpid, int amid, int ceid)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetDetailsOfAllLogins(cpid, amid, ceid);
        }
        public DataSet GetServiceDtlsOfCP(int cpid)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetServiceDtlsOfCP(cpid);
        }
        public string UpdateManagerForCP(int CPID, int newCEID, int newAMID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.UpdateManagerForCP(CPID, newCEID, newAMID);
        }
        public DataSet GetSubscriptionDetailsForTrails()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetSubscriptionDetailsForTrails();
        }

        public bool UpdateTrailPeriod(int shortcodeid, int trailperiod)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.UpdateTrailPeriod(shortcodeid, trailperiod);
        }

        public DataSet GetShortcodes()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetShortcodes();
        }
        public DataSet GetServicesOnShortcodes(int cpid, string Shortcode)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetServicesOnShortcodes(cpid, Shortcode);
        }
        public string SwapShortcode(int TelcoID, string Shortcode, int OldCpid, int Newcpid)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.SwapShortcode(TelcoID, Shortcode, OldCpid, Newcpid);
        }

        public List<ReportsBO> GetAllReports()
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetAllReports();
        }

        public List<ConstraintTypesBO> GetAllConstraintTypes()
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetAllConstraintTypes();
        }

        public List<FinanceCategoryBO> GetAllFinanceCategories()
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetAllFinanceCategories();
        }

        public int SaveReportsRecipients(int configID, int reportID, string email_To, string email_Cc, string email_Bcc, string email_Consolidate_To, string email_Consolidate_Cc, string email_Consolidate_Bcc, string email_VivaKuwait_To, string email_VivaKuwait_Cc, string email_VivaKuwait_Bcc, int loginID, int constraintID, string constraintName)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.SaveReportsRecipients(configID, reportID, email_To, email_Cc, email_Bcc, email_Consolidate_To, email_Consolidate_Cc, email_Consolidate_Bcc, email_VivaKuwait_To, email_VivaKuwait_Cc, email_VivaKuwait_Bcc, loginID, constraintID, constraintName);
        }

        public List<ReportsRecipientsBO> GetReportRecipients(int RecipientID, bool isReportIndexChanged)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetReportRecipients(RecipientID, isReportIndexChanged);
        }

        public int DeleteReportRecipient(int ConfigId)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.DeleteReportRecipient(ConfigId);
        }

        public List<PriceListBO> GetAllPriceList()
        {
            DAL objDB = new DAL(connectionString);
            return objDB.getAllPriceList();
        }

        public List<ActivateInactivateServicesBO> GetServiceSourceTypesByTelcoAndShortcode(int TelcoID, string Shortcode)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetServiceSourceTypesByTelcoAndShortcode(TelcoID, Shortcode);
        }

        public List<ActivateInactivateServicesBO> GetServicesToActivateInactivate(int telcoID, string shortcode, string serviceSourceType, string keyword, int serviceStatus)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetServicesToActivateInactivate(telcoID, shortcode, serviceSourceType, keyword, serviceStatus);
        }

        public int ActivateInactivateServices(ActivateInactivateServicesBO objService)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.ActivateInactivateServices(objService);
        }

        public List<IPRangeBO> GetIPRangesList(int telcoID, int createdBy, int sNo, bool isIncludeInactive)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetIPRangesList(telcoID, createdBy, sNo, isIncludeInactive);
        }

        public DataSet GetIPRangesListDS(int telcoID, int createdBy, int sNo, bool isIncludeInactive)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetIPRangesListDS(telcoID, createdBy, sNo, isIncludeInactive);
        }

        public int DeleteIPRanges(string SNoList, string Separater, int LoginID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.DeleteIPRanges(SNoList, Separater, LoginID);
        }

        public int UpdateIPRangeStatus(int SNo, bool IsActive, int LoginID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.UpdateIPRangeStatus(SNo, IsActive, LoginID);
        }

        public bool UpdateIPRange(int SNo, string IPLowerbound, string IPUpperbound, int LoginID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.UpdateIPRange(SNo, IPLowerbound, IPUpperbound, LoginID);
        }

        public int SaveIPRange(int sNo, int telcoId, string IPLowerbound, string IPUpperbound, int LoginID, bool IsActive)
        {
            DAL objDAL = new DAL(connectionString);
            return SaveIPRange(sNo, telcoId, IPLowerbound, IPUpperbound, LoginID, IsActive);
        }
        public DataSet GetListofSubscribedUsersCount(string shortcode, string keyword, string Subscription_Id, string SDPDLID, string StartDate, string EndDate)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetListofSubscribedUsersCount(shortcode, keyword, Subscription_Id, SDPDLID, StartDate, EndDate);

        }

        public DataSet GetContentsperShortcode(string SDPDLID, string shortcode, string StartDate, string EndDate)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetContentsperShortcode(SDPDLID, shortcode, StartDate, EndDate);

        }

        public DataSet GetHitsReport(string shortcode, string Keyword, string Subcriptionid)
        {
            DAL objDB = new DAL(connectionString);
            return objDB.GetHitsReport(shortcode, Keyword, Subcriptionid);

        }

        #region HelpDesk User Management

        public DataSet GetHeadersList()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetHeadersList();
        }

        public DataSet GetLinksbyHeader(int HeaderID, int RoleID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetLinksbyHeader(HeaderID, RoleID);
        }

        public void ChangeUserPassword(int LoginID, string Password)
        {
            DAL objDAL = new DAL(connectionString);
            objDAL.ChangeUserPassword(LoginID, Password);
        }

        public void ChangeUserStatus(int LoginID, string Status)
        {
            DAL objDAL = new DAL(connectionString);
            objDAL.ChangeUserStatus(LoginID, Status);
        }

        public DataSet GetHelpDeskUsersData()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetHelpDeskUsersData();
        }

        public DataSet GetHelpDeskUsersDatabyLoginId(int LoginID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetHelpDeskUsersDatabyLoginId(LoginID);
        }

        public DataSet GetHelpDeskRoles()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetHelpDeskRoles();
        }

        public bool CheckHelpDeskUserExist(string LoginName)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.CheckHelpDeskUserExist(LoginName);
        }

        public void AddEditUser(string LoginName, string Password, int RoleID, string Name, string EmailID, string MobileNumber, string UserStatus,
                                        int Createdby, int Updatedby, int LoginID, bool IsAdd)
        {
            DAL objDAL = new DAL(connectionString);
            objDAL.AddEditUser(LoginName, Password, RoleID, Name, EmailID, MobileNumber, UserStatus, Createdby, Updatedby, LoginID, IsAdd);
        }

        public int AddEditMasterUser(string LoginName, string Password, string Name, string EmailID, string MobileNumber, string UserStatus, int LoginID, bool IsAdd, string MappedTelcoIDs, out bool UsernameExists)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.AddEditMasterUser(LoginName, Password, Name, EmailID, MobileNumber, UserStatus, LoginID, IsAdd, MappedTelcoIDs, out UsernameExists);
        }

        public DataSet GetMasterUsersData()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetMasterUsersData();
        }

        public void ChangeMasterUserStatus(int LoginID, string Status)
        {
            DAL objDAL = new DAL(connectionString);
            objDAL.ChangeMasterUserStatus(LoginID, Status);
        }

        public void ChangeMasterUserPassword(int LoginID, string Password)
        {
            DAL objDAL = new DAL(connectionString);
            objDAL.ChangeMasterUserPassword(LoginID, Password);
        }

        public DataSet GetMasterUsersDatabyLoginId(int LoginID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetMasterUsersDatabyLoginId(LoginID);
        }

        public int DeleteMasterUser(int LoginID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.DeleteMasterUser(LoginID);
        }

        #endregion
        public string InsertBulkServiceTableData(DataTable dt)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.InsertBulkServiceTableData(dt);
        }
        public void InsertBulkRecords(DataTable dt)
        {
            DAL objDAL = new DAL(connectionString);
             objDAL.InsertBulkRecords(dt);
        }
        public void InsertBulkRecordsWithDataTable(DataTable dtMsg)
        {
            DAL objDAL = new DAL(connectionString);
            objDAL.InsertBulkRecordsWithDataTable(dtMsg);
        }
        public void InsertBulkRecordsWithDataTable_MSISDN(DataTable dtMsisdn)
        {
            DAL objDAL = new DAL(connectionString);
            objDAL.InsertBulkRecordsWithDataTable_MSISDN(dtMsisdn);
        }
        public int UpdateBulkServiceCreationStatus(int ID,string response,bool isProcessed)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.UpdateBulkServiceCreationStatus( ID, response,isProcessed);
        }
        public DataSet GetDataToCreateServices()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetDataToCreateServices();
        }
        #region Price Limit Configuration
        public List<PriceLimitConfiguration> SearchMSISDNChargeLimitDetails(string msisdn_part, int telco_id)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.SearchMSISDNChargeLimitDetails(msisdn_part, telco_id);
        }
        public bool DeleteMSISDNChargeLimitDetails(long limitID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.DeleteMSISDNChargeLimitDetails(limitID);
        }
        public void SaveMSISDNChargeLimitDetails(PriceLimitConfiguration objLimit, out bool isExists)
        {
            DAL objDAL = new DAL(connectionString);
            objDAL.SaveMSISDNChargeLimitDetails(objLimit, out isExists);
        }
        public void SaveDefaultChargeLimitDetails(ChargeLimit objLimit, out string errMsg)
        {
            errMsg = string.Empty;
            DAL objDAL = new DAL(connectionString);
            objDAL.SaveDefaultChargeLimitDetails(objLimit, out errMsg);
        }
        public PriceLimitConfiguration GetMSISDNChargeLimitDetailsByMSISDN(string MSISDN)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetMSISDNChargeLimitDetailsByMSISDN(MSISDN);
        }
        public List<PriceLimitConfiguration> GetServicePriceLimit(string DirectBillingTelcos)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetServicePriceLimit(DirectBillingTelcos);
        }
        public List<PriceLimitConfiguration> GetTelcoPriceLimit(string DirectBillingTelcos)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetTelcoPriceLimit(DirectBillingTelcos);
        }
        public List<ChargeLimitType> GetChargeLimitTypes()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetChargeLimitTypes();
        }
        public List<ChargeLimit> GetDefaultChargeLimitDetails(string LimitCategory, string DirectBillingTelcoIDs)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetDefaultChargeLimitDetails(LimitCategory, DirectBillingTelcoIDs);
        }
        public bool DeleteDefaultChargeLimitDetails(long limitID, int chargeLimitTypeID, string limitCategory)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.DeleteDefaultChargeLimitDetails(limitID, chargeLimitTypeID, limitCategory);
        }
        public List<PriceLimitConfiguration> GetMSISDNChargeLimitDetails()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetMSISDNChargeLimitDetails();
        }
        public void SaveTelcoPriceLimitDetails(PriceLimitConfiguration objLimit)
        {
            DAL objDAL = new DAL(connectionString);
            objDAL.SaveTelcoPriceLimitDetails(objLimit);
        }
        #endregion



        #region BlockedServiceMsisdns

        public List<BlockedServiceMsisdnsBO> GetBlockedServiceMsisdns()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetBlockedServiceMsisdns();
        }
        public void UnblockServiceMsisdns(int LoginID, string BlockedIds)
        {
            DAL objDAL = new DAL(connectionString);
            objDAL.UnblockServiceMsisdns(LoginID, BlockedIds);
        }

        #endregion

        public DataTable GetOperatorServiceSource()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetOperatorServiceSource();
        }

        public DataTable GetServiceMsisdnsAllowedSuccessTransSourceMst()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetServiceMsisdnsAllowedSuccessTransSourceMst();
        }
        public string SaveRestrictedWords(int Telco_Id, string RestrictedWords, int CreatedBy)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.SaveRestrictedWords(Telco_Id, RestrictedWords, CreatedBy);
        }
        public DataTable GetRestrictedWordsByTelcoId(int Telco_Id)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetRestrictedWordsByTelco(Telco_Id);
        }
        public int DeleteRestrictedWordsByTelcoId(int Telco_Id, string RestrictedWords)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.DeleteRestrictedWordsByTelcoId(Telco_Id, RestrictedWords);
        }
        public DataTable GetServiceTypeByShortCode(int TelcoId, int ShortCode)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetServiceTypeByShortCode(TelcoId, ShortCode);
        }
        public DataTable GetServicesForPriceLimitConfiguration(int TelcoId, int ShortCode, string ServiceType, string limitType)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetServicesForPriceLimitConfiguration(TelcoId, ShortCode, ServiceType, limitType);
        }
        public int SaveServicesForPriceLimitConfiguration(int LookupId, decimal Daily_Price_Limit, decimal Monthly_Price_Limit, bool IsChargeLimitApplied, int LoginID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.SaveServicesForPriceLimitConfiguration(LookupId, Daily_Price_Limit, Monthly_Price_Limit, IsChargeLimitApplied, LoginID);
        }
        public DataTable GetSubscriptionServicesForDND(int TelcoId, int ShortCode)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetSubscriptionServicesForDND(TelcoId, ShortCode);
        }
        public int SaveSubscriptionServicesForDND(int SubscriptionId, bool DND_ContentDelivery_Flag, TimeSpan DND_StartTime, int DND_TimeSpan_InMin, int DND_DelayAfterDND_TimeSpan_InMin)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.SaveSubscriptionServicesForDND(SubscriptionId, DND_ContentDelivery_Flag, DND_StartTime, DND_TimeSpan_InMin, DND_DelayAfterDND_TimeSpan_InMin);
        }

        public DataTable GetMenuHeaders()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetMenuHeaders();
        }

        public DataTable GetMenuHeaderLinks(int LoginID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetMenuHeaderLinks(LoginID);
        }

        public DataSet GetHeaderLinkDetailsByID(int HeaderLinkID, bool isHeader, int loginRoleID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetHeaderLinkDetailsByID(HeaderLinkID, isHeader, loginRoleID);
        }

        public int SaveHeaderLinkDetails(int LinkID, int HeaderID, bool IsHeader, string HeaderLinkName, string LinkURL, bool HeaderLinkStatus, string ReadWriteLoginIDs, string ReadOnlyLoginIDs, string NoAccessLoginIDs, int LoginRoleID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.SaveHeaderLinkDetails(LinkID, HeaderID, IsHeader, HeaderLinkName, LinkURL, HeaderLinkStatus, ReadWriteLoginIDs, ReadOnlyLoginIDs, NoAccessLoginIDs, LoginRoleID);
        }

        public int SaveMenuHeaders(string HeaderName, bool Status, int Rank)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.SaveMenuHeaders(HeaderName, Status, Rank);
        }

        public int SaveMenuHeaderLinks(int HeaderID, string LinkName, string LinkURL, int Rank, bool Status)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.SaveMenuHeaderLinks(HeaderID, LinkName, LinkURL, Rank, Status);
        }
        public int SaveUserActionLogs(int Login_Id, string PageName, string Action, string Remarks)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.SaveUserActionLogs(Login_Id, PageName, Action, Remarks);
        }
        public int BlacklistMsisdn(string Login_Id, string msisdn, string operatorId)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.BlacklistMsisdn(Login_Id, msisdn, operatorId);
        }


        public DataSet GetSTKWAPMenuWiseCount(string FromDate, string ToDate, string Menu_Type)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetSTKWAPMenuWiseCount(FromDate, ToDate, Menu_Type);
        }

        public DataSet GetSTKWAPMenus(string Menu_Type)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetSTKWAPMenus(Menu_Type);
        }


        public DataSet GetSTKWAPMenuWiseMSISDN(string Clicked_Date, int Clicked_MenuID)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetSTKWAPMenuWiseMSISDN(Clicked_Date, Clicked_MenuID);
        }

        public int DeleteMenuOptions(int HeaderLinkID, bool IsHeader)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.DeleteMenuOptions(HeaderLinkID, IsHeader);
        }
        public void SaveHeaderLinkSortDetails(int LinkID, int HeaderID, int Rank)
        {
            DAL objDAL = new DAL(connectionString);
            objDAL.SaveHeaderLinkSortDetails(LinkID, HeaderID, Rank);
        }

        public int SaveBillingBundleCaps(string bundleName, int telcoId, string shortcode, string serviceSourceType, string billServiceIds, int createdBy, out string errMsg)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.SaveBillingBundleCaps(bundleName, telcoId, shortcode, serviceSourceType, billServiceIds, createdBy, out errMsg);
        }
        public DataTable GetBundleServicesByBundleId(long bundleId)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetBundleServicesByBundleId(bundleId);
        }
        public int UpdateBundleName(string bundleName, int status, long bundleId)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.UpdateBundleName(bundleName, status, bundleId);
        }
        public DataTable GetBundleDetails()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetBundleDetails();
        }
        public int SaveEquipCommitmentDetails(string offerID, DateTime newUseractivationDate, DateTime newUserexpiryDate, DateTime existingUserActivationDate, DateTime existingUserExpiryDate, int discServicesCount, string targetedSubscriberFlag, int telcoId, string dynExpTextEng, string dynExpTextArb, string status, string subscriptionDetails, string vasPromMessages, out string responseCode, out long equipCommitId)
        {
            responseCode = string.Empty;
            DAL objDAL = new DAL(connectionString);
            return objDAL.SaveEquipCommitmentDetails(offerID, newUseractivationDate, newUserexpiryDate, existingUserActivationDate, existingUserExpiryDate, discServicesCount, targetedSubscriberFlag, telcoId, dynExpTextEng, dynExpTextArb, status, subscriptionDetails, vasPromMessages, out responseCode, out equipCommitId);
        }
        public DataSet GetEquipCommitmentDetails(long equipCommitId)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetEquipCommitmentDetails(equipCommitId);
        }
        public int UpdateEquipCommitmentDetails(long equipCommitmentId, string offerID, DateTime newUseractivationDate, DateTime newUserexpiryDate, DateTime existingUserActivationDate, DateTime existingUserExpiryDate, int telcoId, int discountedServicesCount, string targetedSubscriberFlag, string DynamicExplanatoryText_Eng, string dynamicExplanatoryText_Ar, string status, string subscriptionDetails, string vasPromotionMessages, out string responseCode)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.UpdateEquipCommitmentDetails(equipCommitmentId, offerID, newUseractivationDate, newUserexpiryDate, existingUserActivationDate, existingUserExpiryDate, telcoId, discountedServicesCount, targetedSubscriberFlag, DynamicExplanatoryText_Eng, dynamicExplanatoryText_Ar, status, subscriptionDetails, vasPromotionMessages, out  responseCode);
        }
        public DataSet GetPromotionsDetails()
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetPromotionsDetails();
        }
        public DataSet GetPromotionSubscriptionDetails(long equipCommitId, out bool userExists)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetPromotionSubscriptionDetails(equipCommitId, out userExists);
        }
        public DataSet GetVasPromotionTelco(string vasPromTelcoIds)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetVasPromotionTelco(vasPromTelcoIds);
        }
        public DataSet GetServicePriceByLookupId(int telcoID, string shortcode, int lookupId, out string servicePrice)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetServicePriceByLookupId(telcoID, shortcode, lookupId, out servicePrice);
        }
        public DataSet GetVasPromMessagesByLookupId(int planId, int lookupId, out long planServiceId)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetVasPromMessagesByLookupId(planId, lookupId, out planServiceId);
        }
        public string GetBillingXMLByEquipCommitID(int planID, int lookupId)
        {
            DAL objDAL = new DAL(connectionString);
            return objDAL.GetBillingXMLByEquipCommitID(planID, lookupId);
        }
    }
}
