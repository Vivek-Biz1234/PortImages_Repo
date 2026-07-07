using Microsoft.Data.SqlClient;
using PORTIMAGES.IntegrationService.Helpers;
using System.Data;
using DT = System.Data.DataTable;

namespace PORTIMAGES.IntegrationService.Services
{
    public class ExternalDbService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public ExternalDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ExternalDB");
        } 

        #region Get The Data For CountryMaster From Bizupon
        public async Task<DT> GetCountryDataAsync()
        {
            var query = @"SELECT CID AS ID,countryName AS CountryName,ISDCode AS ISDCode FROM COUNTRY";
            return await DbHelper.ExecuteDataTableAsync(_connectionString, query, CommandType.Text);             
        }
        #endregion        
        #region Get The Data For PortMaster From Bizupon
        public async Task<DT> GetPortDataAsync()
        {
            var query = @"SELECT C.CID,C.countryName CountryName,P.Name AS PortName,P.SName,P.email Email,P.contact Contact
                        ,P.fax Fax,P.address Address FROM PortMaster P JOIN COUNTRY C ON C.CID=P.CID";
            return await DbHelper.ExecuteDataTableAsync(_connectionString, query, CommandType.Text);             
        }
        #endregion
        #region Get The Data For TerminalMaster From Bizupon
        public async Task<DT> GetTerminalDataAsync()
        {
            var query = @"SELECT PM.ID PortId,PM.Name PortName,J.ID,J.Tname FROM JapanTerminal J WITH(NOLOCK) JOIN PortMaster PM WITH(NOLOCK) ON PM.ID=J.PID";
            return await DbHelper.ExecuteDataTableAsync(_connectionString, query, CommandType.Text);
        }
        #endregion
        #region Get The Data For CategoryMaster From Bizupon
        public async Task<DT> GetCategoryDataAsync()
        {
            var query = @"SELECT Name CategoryName,title AS Title,keyword AS Keyword,description AS Description FROM SUBCATEGORY";
            return await DbHelper.ExecuteDataTableAsync(_connectionString, query, CommandType.Text);
        }
        #endregion
        #region Get The Data For ShippingMaster From Bizupon
        public async Task<DT> GetShippingDataAsync()
        {
            var query = @"SELECT C.countryName CountryName,S.Name ShippingName,S.Emailid Email,S.ContactNo Contact,S.FaxNo Fax
                        ,S.PWD PasswordHash,S.ccemail CCMail,S.haddress HOAddress,S.Baddress BOAddress,S.Cperson PersonInCharge
                        ,S.Srate Rate,S.Obalance OpeningBal FROM Shipping S JOIN COUNTRY C ON C.CID=S.CID  ";
            return await DbHelper.ExecuteDataTableAsync(_connectionString, query, CommandType.Text);
        }
        #endregion
        #region Get The Data For ShipMaster From Bizupon
        public async Task<DT> GetShipDataAsync()
        {
            var query = @"SELECT SM.Shipname ShipName,CASE WHEN ISNULL(SM.Stype,0)=1 THEN 'Real Ship' WHEN ISNULL(SM.Stype,0)=2 THEN'Tentative Ship' END ShipType
                        ,CASE WHEN ISNULL(SM.Shipuse,0)=1 THEN 'Normal' WHEN ISNULL(SM.Shipuse,0)=2 THEN'Next Time'
                        WHEN ISNULL(SM.Shipuse,0)=3 THEN'Final Time' END Shipuse,S.Name ShippingName,PM.Name PortName,TM.Tname TerminalName
                        ,C.countryName CountryName,SM.Ddate DepDate,SM.Adate ArrDate,SM.Sfreight Freight
                        ,CASE WHEN ISNULL(SM.LoadingCapacity,'')='' THEN 0 ELSE SM.LoadingCapacity END LCapacity
                        FROM Shipmaster SM LEFT JOIN Shipping S ON S.ID=SM.SID LEFT JOIN PortMaster PM ON PM.ID=SM.PID
                        LEFT JOIN Terminalmaster TM ON TM.ID=SM.Terminal LEFT JOIN COUNTRY C ON C.CID=SM.CID";
            return await DbHelper.ExecuteDataTableAsync(_connectionString, query, CommandType.Text);
        }
        #endregion

        #region Get The Data For MakerMaster From Bizupon
        public async Task<DT> GetMakerDataAsync()
        {
            var query = @"SELECT C.countryName AS CountryName,M.Name AS MakerName,M.title AS Title,M.keyword AS Keyword,M.description AS Description
            ,M.Canonical,M.Details,'https://www.bizupon.com/Makerimage/'+M.Flag AS Logo FROM makers M JOIN COUNTRY C ON C.CID=M.Country";
            return await DbHelper.ExecuteDataTableAsync(_connectionString, query, CommandType.Text);
        }
        #endregion
        
        #region Get The Data For ModelMaster From Bizupon
        public async Task<DT> GetModelDataAsync()
        {
            var query = @"SELECT C.Name CategoryName,M.Name MakerName,PM.Name ModelName,PM.title AS Title,PM.keyword AS Keyword
                        ,PM.description Description FROM ProductMaster PM JOIN subcategory C ON C.SID=PM.CID JOIN makers M ON M.CID=PM.MID WHERE ISNULL(PM.NAME,'') <> '' ";
            return await DbHelper.ExecuteDataTableAsync(_connectionString, query, CommandType.Text);
        }
        #endregion
    }
}
