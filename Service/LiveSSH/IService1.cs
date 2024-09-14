using LiveSSH.Models;
using SSH_Service;
using SSH_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LiveSSH
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        User GetUser(string username, string password);

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        User GetHouseOwner(string username, string password, int userType);

        [OperationContract]
        User GetOccupant(string username, string password, int userType);

        [OperationContract]
        User GetRespondent(string username, string password, int userType);

        [OperationContract]
        List<User> GetOwnerOccupants(string ownerID);

        [OperationContract]
        int RegisterOwner(User user, House house, int packageID);

        [OperationContract]
        int RegisterOccupant(User occupant, string houseOwnerID);

        [OperationContract]
        int RegisterRespondent(User repondent);

        [OperationContract]
        int DeleteOccupant(string userID);

        [OperationContract]
        int DeleteRespondent(string userID);

        [OperationContract]
        int DeleteOwner(string userID);

        [OperationContract]
        int EditUser(string userID, string name, string surname, string email);

        [OperationContract]
        int ChangePassword(string userID, string newPassword);

        [OperationContract]
        List<Package> Packages();

        [OperationContract]
        Package UserPackage(string userID);

        [OperationContract]
        List<Case> GetAllCases();

        [OperationContract]
        int AddCase(string userID, string respondentID, string description, string houseID, string Strdatetime, string StrresponseTime, string type);

        [OperationContract]
        List<Case> HouseCases(string ownerID);

        [OperationContract]
        int UpdateRespondentLocation(string respondentID, string longitute, string lattitue);

        [OperationContract]
        Respondent PanicAlert(string userID, int AlertType);

        [OperationContract]
        House GetUserHome(string userID);

        [OperationContract]
        Address GetUserAddress(string userID);

        [OperationContract]
        GeoLocation CheckAlerts(string respondentID);

        [OperationContract]
        string CheckCustomAlerts(string respondentID);
        [OperationContract]
        List<Case> GetRespondentCases(string respondentID);

        [OperationContract]
        User GetUserByID(string ID);

        [OperationContract]
        int ActiveCases(string ownerID);

        [OperationContract]
        int AllActiveCases();

        [OperationContract]
        int TotalOccupants();

        [OperationContract]
        int TotalRespondents();

        [OperationContract]
        int TotalHomeOwners();

        [OperationContract]
        List<User> GetAllRespondents();

        [OperationContract]
        List<Case> GetCasesByRespondent(string respondentID);

        [OperationContract]
        List<int> GetCasesForLastSevenDays();

        [OperationContract]
        List<Alert> GetAlerts();

        [OperationContract]
        List<Alert> GetRespondentAlerts(string respondentID);

        [OperationContract]
        List<Alert> GetUserAlerts(string userID);

        [OperationContract]
        List<string> getBestRespondent();

        [OperationContract]
        List<string> getWorstRespondent();

        [OperationContract]
        List<String> Top5Cities();

        [OperationContract]
        List<String> Top5Surbub(String CityName);

        [OperationContract]
        List<Case> GetAreaCases(String city);

        [OperationContract]
        String GetTimeInterval();

        [OperationContract]
        int TriggerAlarm(string userID, int triggerType);

        [OperationContract]
        string CheckHomeAlerts(string userID);

        [OperationContract]
        List<Address> GetAllAdresses();

        [OperationContract]
        List<StringDateCase> GetResCases(string respondentID);

        [OperationContract]
        int Respondent_DutyStatus(string respondentID, string status);
        [OperationContract]
        string AddCaseObject(string data, string description, string Strdatetime);

        [OperationContract]
        List<GeoLocation> GetGeoLocations();

        [OperationContract]
        GeoLocation GetRespondentLocation(string userID);

        [OperationContract]
        int AddOccupant(string name, string surname, string id, string email, string ownwerID);

        [OperationContract]
        int SolveCase(int caseID);

        [OperationContract]
        int GetStatus(string userID);
    }
} 

