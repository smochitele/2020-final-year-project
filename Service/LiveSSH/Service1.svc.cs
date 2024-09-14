using LiveSSH.Models;
using SSH_Service;
using SSH_Service.Models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LiveSSH
{
    public class Service1 : IService1
    {
        #region Constants
        private const int ACTIVE = 1;
        private const int SUCCESS = 1;
        private const int PANIC = 1;
        private const int BREAK_IN = 2;
        private const int FAIL = -1;
        private const int DEACTIVE = -1;
        private const int ALARM_TRIGGERED = -2;
        private const int ALARM_OFF = 1;
        private const int ALARM_ON = 2;
        private const string ON = "2";
        private const string OFF = "1";
        private const string TRIG = "-2";

        #endregion
        /*
         * Occupant = 1
         * Owner = 2,
         * Respondent = 3,
         * Admin/Manager = 4
         */
        #region Database
        private readonly DatabaseDataContext db = new DatabaseDataContext();
        #endregion

        #region User Retrieval
        [WebInvoke(Method = "GET", UriTemplate = "user/?username={username}&password={password}", ResponseFormat = WebMessageFormat.Json)]
        public User GetUser(string username, string password)
        {
            var user = db.DB_Users.Where(u => u.Email.Equals(username) && u.Password.Equals(Secrecy.HashPassword(password)) && u.IsActive >= 0).SingleOrDefault();
            if (user != null)
            {
                return new User
                {
                    ID = user.ID,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Password = null,
                    IsActive = user.IsActive,
                    UserType = user.UserType,
                    DateRegistered = user.DateRegistered
                };
            }
            else
            {
                return null;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "user", ResponseFormat = WebMessageFormat.Json)]
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            dynamic dbUsers = db.DB_Users.Where(u => u.Email != null);
            if (users != null)
            {
                foreach (DB_User user in dbUsers)
                {
                    users.Add(new User
                    {
                        ID = user.ID,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        Password = null,
                        IsActive = user.IsActive,
                        UserType = user.UserType,
                        DateRegistered = user.DateRegistered
                    });
                }
                return users;
            }
            else
            {
                return null;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "users/?ownerID={ownerID}", ResponseFormat = WebMessageFormat.Json)]
        public List<User> GetOwnerOccupants(string ownerID)
        {
            List<User> occupants = new List<User>();
            dynamic dbOccupants = (from o in db.DB_HouseOccupants
                                   where o.HouseOwnerID.Equals(ownerID)
                                   select o);
            if (dbOccupants != null)
            {
                foreach (DB_HouseOccupant item in dbOccupants)
                {
                    occupants.Add(new User
                    {
                        ID = db.DB_Users.Where(x => x.ID.Equals(item.ID)).SingleOrDefault().ID,
                        Name = db.DB_Users.Where(x => x.ID.Equals(item.ID)).SingleOrDefault().Name,
                        Surname = db.DB_Users.Where(x => x.ID.Equals(item.ID)).SingleOrDefault().Surname,
                        Email = db.DB_Users.Where(x => x.ID.Equals(item.ID)).SingleOrDefault().Email,
                        Password = null,
                        IsActive = db.DB_Users.Where(x => x.ID.Equals(item.ID)).SingleOrDefault().IsActive,
                        UserType = db.DB_Users.Where(x => x.ID.Equals(item.ID)).SingleOrDefault().UserType,
                        DateRegistered = db.DB_Users.Where(x => x.ID.Equals(item.ID)).SingleOrDefault().DateRegistered,
                    });
                }
                return occupants;
            }
            else
            {
                return null;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "owner/?username={username}&password={password}&userType={userType}", ResponseFormat = WebMessageFormat.Json)]
        public User GetHouseOwner(string username, string password, int userType)
        {
            User user = GetUser(username, password);
            if (user.UserType.Equals(userType))
                return user;
            else
                return null;
        }

        [WebInvoke(Method = "GET", UriTemplate = "occupant/?username={username}&password={password}&userType={userType}", ResponseFormat = WebMessageFormat.Json)]
        public User GetOccupant(string username, string password, int userType)
        {
            User user = GetUser(username, password);
            if (user.UserType.Equals(userType))
                return user;
            else
                return null;
        }

        [WebInvoke(Method = "GET", UriTemplate = "respondent/?username={username}&password={password}&userType={userType}", ResponseFormat = WebMessageFormat.Json)]
        public User GetRespondent(string username, string password, int userType)
        {
            User user = GetUser(username, password);
            if (user.UserType.Equals(userType))
                return user;
            else
                return null;
        }
        #endregion

        #region User Removal
        [WebInvoke(Method = "GET", UriTemplate = "deleteuser/?userID={userID}", ResponseFormat = WebMessageFormat.Json)]
        private int DeleteUser(string userID)
        {
            var dbUser = db.DB_Users.Where(u => u.ID.Equals(userID)).SingleOrDefault();
            if (dbUser != null)
            {
                dbUser.IsActive = DEACTIVE;
                try
                {
                    db.SubmitChanges();
                    return SUCCESS;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return FAIL;
                }
            }
            else
            {
                return FAIL;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "deleteoccupant/?userID={userID}", ResponseFormat = WebMessageFormat.Json)]
        public int DeleteOccupant(string userID)
        {
            return DeleteUser(userID);
        }

        [WebInvoke(Method = "GET", UriTemplate = "deleterespondent/?userID={userID}", ResponseFormat = WebMessageFormat.Json)]
        public int DeleteRespondent(string userID)
        {
            return DeleteUser(userID);
        }

        [WebInvoke(Method = "GET", UriTemplate = "deleteowner/?userID={userID}", ResponseFormat = WebMessageFormat.Json)]
        public int DeleteOwner(string userID)
        {
            return DeleteUser(userID);
        }
        #endregion

        #region User Modification
        [WebInvoke(Method = "GET", UriTemplate = "edituser/?userID={userID}&name={name}&surname={surname}&email={email}", ResponseFormat = WebMessageFormat.Json)]
        public int EditUser(string userID, string name, string surname, string email)
        {
            var dbUser = db.DB_Users.Where(u => u.ID.Equals(userID)).SingleOrDefault();
            try
            {
                dbUser.Name = name;
                dbUser.Surname = surname;
                dbUser.Email = email;
                db.SubmitChanges();
                return SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return FAIL;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "changepassword/?userID={userID}&newPassword={newPassword}", ResponseFormat = WebMessageFormat.Json)]
        public int ChangePassword(string userID, string newPassword)
        {
            var dbUser = db.DB_Users.Where(u => u.ID.Equals(userID)).SingleOrDefault();
            try
            {
                dbUser.Password = Secrecy.HashPassword(newPassword);
                db.SubmitChanges();
                return SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return FAIL;
            }
        }
        #endregion

        #region User Submission
        private int AddUser(User user)
        {
            DB_User newUser = new DB_User
            {
                ID = user.ID,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Password = Secrecy.HashPassword(user.Password),
                IsActive = user.IsActive,
                UserType = user.UserType,
                DateRegistered = DateTime.Now
            };

            try
            {
                db.DB_Users.InsertOnSubmit(newUser);
                db.SubmitChanges();
                return SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return FAIL;
            }
        }

        [WebInvoke(Method = "POST", UriTemplate = "registeroccupant", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public int RegisterOccupant(User occupant, string houseOwnerID)
        {
            AddUser(occupant);
            try
            {
                DB_HouseOccupant dbOcccupant = new DB_HouseOccupant
                {
                    ID = occupant.ID,
                    HouseOwnerID = houseOwnerID
                };
                db.DB_HouseOccupants.InsertOnSubmit(dbOcccupant);
                db.SubmitChanges();
                return SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return FAIL;
            }
        }


        [WebInvoke(Method = "POST", UriTemplate = "registerrespondent", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public int RegisterRespondent(User repondent)
        {
            AddUser(repondent);
            DB_Respondent dbRespondent = new DB_Respondent
            {
                ID = repondent.ID
            };
            try
            {
                db.DB_Respondents.InsertOnSubmit(dbRespondent);
                db.SubmitChanges();
                return SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return FAIL;
            }
        }

        [WebInvoke(Method = "POST", UriTemplate = "registerowner", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public int RegisterOwner(User user, House house, int packageID)
        {
            try
            {
                AddUser(user);
                DB_HouseOwner owner = new DB_HouseOwner
                {
                    ID = user.ID,
                    PackageID = packageID
                };
                db.DB_HouseOwners.InsertOnSubmit(owner);
                db.SubmitChanges();

                AddHouse(house, user.ID);
                SetAddress(house, user.ID);
                return SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return FAIL;
            }
        }
        #endregion

        #region House Manipulation
        private int AddHouse(House house, string userID)
        {
            try
            {
                DB_House dbHouse = new DB_House
                {
                    AlarmStatus = ACTIVE,
                    OwnerID = userID
                };
                db.DB_Houses.InsertOnSubmit(dbHouse);
                db.SubmitChanges();
                return SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return FAIL;
            }
        }

        private DB_House GetHouse(string ownerID)
        {
            var house = db.DB_Houses.Where(h => h.OwnerID.Equals(ownerID)).SingleOrDefault();
            return house;
        }

        private int SetAddress(House house, string ownerID)
        {
            var dbHouse = GetHouse(ownerID);
            DB_Address address = new DB_Address
            {
                Province = house.HouseAddress.Province,
                City = house.HouseAddress.City,
                Surburb = house.HouseAddress.Surburb,
                StreetName = house.HouseAddress.StreetName,
                HouseNo = house.HouseAddress.HouseNo,
                ZIPCode = house.HouseAddress.ZIPCode,
                Longitute = house.HouseAddress.Longitute,
                Lattitute = house.HouseAddress.Lattitute,
                HouseID = dbHouse.HouseID
            };
            try
            {
                db.DB_Addresses.InsertOnSubmit(address);
                db.SubmitChanges();
                return SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return FAIL;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "getuserhome/?userID={userID}", ResponseFormat = WebMessageFormat.Json)]
        public House GetUserHome(string userID)
        {
            var landlord = db.DB_HouseOwners.Where(l => l.ID.Equals(userID)).SingleOrDefault();
            if (landlord != null)
            {
                var house = db.DB_Houses.Where(h => h.OwnerID.Equals(userID)).SingleOrDefault();
                if (house != null)
                {
                    return new House
                    {
                        HouseID = house.HouseID,
                        AlarmStatus = house.AlarmStatus,
                        OwnerID = landlord.ID
                    };
                }
                else
                {
                    return null;
                }
            }
            else if (landlord == null)
            {
                var occupant = db.DB_HouseOccupants.Where(o => o.ID.Equals(userID)).SingleOrDefault();
                if (occupant != null)
                {
                    landlord = db.DB_HouseOwners.Where(l => l.ID.Equals(occupant.HouseOwnerID)).SingleOrDefault();
                    if (landlord != null)
                    {
                        var house = db.DB_Houses.Where(h => h.OwnerID.Equals(userID)).SingleOrDefault();
                        if (house != null)
                        {
                            return new House
                            {
                                HouseID = house.HouseID,
                                AlarmStatus = house.AlarmStatus,
                                OwnerID = landlord.ID
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "getuseraddress/?userID={userID}", ResponseFormat = WebMessageFormat.Json)]
        public Address GetUserAddress(string userID)
        {
            var house = GetUserHome(userID);
            if (house != null)
            {
                var address = db.DB_Addresses.Where(a => a.HouseID.Equals(house.HouseID)).SingleOrDefault();
                if (address != null)
                {
                    return new Address
                    {
                        ID = address.ID,
                        Province = address.Province,
                        City = address.City,
                        Surburb = address.Surburb,
                        StreetName = address.StreetName,
                        HouseNo = address.HouseNo,
                        ZIPCode = address.ZIPCode,
                        Longitute = address.Longitute,
                        Lattitute = address.Lattitute
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Packages
        [WebInvoke(Method = "GET", UriTemplate = "packages", ResponseFormat = WebMessageFormat.Json)]
        public List<Package> Packages()
        {
            List<Package> packages = new List<Package>();
            dynamic dbPackages = db.DB_Packages.Where(p => p.Description != null);
            if (dbPackages != null)
            {
                foreach (DB_Package item in dbPackages)
                {
                    packages.Add(new Package
                    {
                        PackageID = item.PackageID,
                        Type = item.Type,
                        Description = item.Description,
                        Price = item.Price
                    });
                }
                return packages;
            }
            else
            {
                return null;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "packages/?userID={userID}", ResponseFormat = WebMessageFormat.Json)]
        public Package UserPackage(string userID)
        {
            var owner = db.DB_HouseOwners.Where(o => o.ID.Equals(userID)).SingleOrDefault();
            var occupant = db.DB_HouseOccupants.Where(o => o.ID.Equals(userID)).SingleOrDefault();

            if (owner != null) //Check if @user is the owner
            {
                var dbPackage = db.DB_Packages.Where(p => p.PackageID.Equals(owner.PackageID)).SingleOrDefault();
                return new Package
                {
                    PackageID = dbPackage.PackageID,
                    Type = dbPackage.Type,
                    Description = dbPackage.Description,
                    Price = dbPackage.Price
                };
            }
            else if (occupant != null) //Check else if @user is an occupant
            {
                var landlord = db.DB_HouseOwners.Where(l => l.ID.Equals(occupant.HouseOwnerID)).SingleOrDefault();
                var dbPackage = db.DB_Packages.Where(p => p.PackageID.Equals(landlord.PackageID)).SingleOrDefault();
                return new Package
                {
                    PackageID = dbPackage.PackageID,
                    Type = dbPackage.Type,
                    Description = dbPackage.Description,
                    Price = dbPackage.Price
                };

            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Cases
        [WebInvoke(Method = "GET", UriTemplate = "cases", ResponseFormat = WebMessageFormat.Json)]
        public List<Case> GetAllCases()
        {
            List<Case> cases = new List<Case>();
            dynamic dbCases = db.DB_Cases.Where(c => c.Description != null);
            if (dbCases != null)
            {
                foreach (DB_Case item in dbCases)
                {
                    cases.Add(new Case
                    {
                        CaseID = item.CaseID,
                        Date = item.Date,
                        ReposponseTime = item.ReposponseTime,
                        ResolutionDate = (item.ResolutionDate != null)?(DateTime)item.ResolutionDate : new DateTime(),
                        Description = item.Description,
                        HouseID = item.HouseID,
                        RespondentID = item.RespondentID
                    });
                }
                return cases;
            }
            else
            {
                return null;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "getRespondentCases/?respID={respID}", ResponseFormat = WebMessageFormat.Json)]
        public List<Case> getRespondentCases(string respID)
        {
            List<Case> cases = new List<Case>();
            dynamic dbCases = (from cs in db.DB_Cases
                               where cs.RespondentID.Equals(respID)
                               select cs) ?? null;
            if (dbCases != null)
            {
                foreach (DB_Case item in dbCases)
                {
                    cases.Add(new Case
                    {
                        CaseID = item.CaseID,
                        Date = item.Date,
                        ReposponseTime = item.ReposponseTime,
                        ResolutionDate = (DateTime)item.ResolutionDate,
                        Description = item.Description,
                        HouseID = item.HouseID,
                        RespondentID = item.RespondentID
                    });
                }
                return cases;
            }
            else
            {
                return null;
            }
        }


        [WebInvoke(Method = "GET", UriTemplate = "case/?ownerID={ownerID}", ResponseFormat = WebMessageFormat.Json)]
        public List<Case> HouseCases(string ownerID)
        {
            List<Case> cases = new List<Case>();
            dynamic dbCases = (from cs in db.DB_Cases
                               join
                                house in db.DB_Houses
                                on cs.HouseID equals house.HouseID
                               where house.OwnerID == ownerID
                               select cs) ?? null;
            if (dbCases != null)
            {
                foreach (DB_Case item in dbCases)
                {
                    cases.Add(new Case
                    {
                        CaseID = item.CaseID,
                        Date = item.Date,
                        ReposponseTime = item.ReposponseTime,
                        ResolutionDate = (DateTime)item.ResolutionDate,
                        Description = item.Description,
                        HouseID = item.HouseID,
                        RespondentID = item.RespondentID
                    });
                }
                return cases;
            }
            else
            {
                return null;
            }
        }

        [WebInvoke(Method = "POST", UriTemplate = "addcase/?userID={userID}&respondentID={respondentID}&description={description}" +
            "&houseID={houseID}&Strdatetime={Strdatetime}&StrresponseTime={StrresponseTime}"
            , ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public int AddCase(string userID, string respondentID,
            string description, string houseID, string Strdatetime, string StrresponseTime, string type)
        {

            //convert time from string to timespan
            DateTime datetime = DateTime.Parse(Strdatetime);
            TimeSpan responseTime = TimeSpan.Parse(StrresponseTime);

            DB_Case dbDase = new DB_Case
            {
                UserID = userID,
                HouseID = GetUserHome(userID).HouseID,
                RespondentID = respondentID,
                Description = description,
                Date = datetime,
                ResolutionDate = null,
                ReposponseTime = responseTime.ToString(),
                Type = Convert.ToInt32(type)
                
            };
            try
            {
                db.DB_Cases.InsertOnSubmit(dbDase);
                db.SubmitChanges();
                return SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return FAIL;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "AddCaseObject/?data={data}&description={description}&Strdatetime={Strdatetime}"
           , ResponseFormat = WebMessageFormat.Json)]
        public string AddCaseObject(string data, string description, string Strdatetime)
        {

            //data=userID  Houseid respondenid type responcetime 
            //desciptiom
            //datetime
            //convert time from string to timespan
            string[] words = data.Split(' ');
            string userID = words[0];
            string houseID = words[1];
            string respondentID = words[2];
            string type = words[3];
            string StrresponseTime = words[4];

            DateTime datetime = DateTime.Parse(Strdatetime);
            TimeSpan responseTime = TimeSpan.Parse(StrresponseTime);
            string userfullname;

            DB_Case dbDase = new DB_Case
            {
                UserID = userID,
                HouseID = Convert.ToInt32(houseID),
                RespondentID = respondentID,
                Description = description,
                Date = datetime,
                ResolutionDate = null,
                ReposponseTime = "",
                Type = Convert.ToInt32(type)

            };
            try
            {
                db.DB_Cases.InsertOnSubmit(dbDase);
                db.SubmitChanges();
                userfullname = getUserFullname(userID);
                return userfullname;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region Respondent
        [WebInvoke(Method = "GET", UriTemplate = "location/?respondentID={respondentID}&longitute={longitute}&lattitue={lattitue}", ResponseFormat = WebMessageFormat.Json)]
        public int UpdateRespondentLocation(string respondentID, string longitute, string lattitue)
        {
            var respondent = db.DB_Respondents.Where(r => r.ID.Equals(respondentID)).SingleOrDefault();
            if (respondent != null)
            {
                try
                {
                    respondent.LastLongitute = longitute;
                    respondent.LastLattitute = lattitue;
                    db.SubmitChanges();
                    return SUCCESS;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return FAIL;
                }
            }
            else
            {
                return FAIL;
            }
        }

        private Respondent GetNearestRespondent(string houseLongitute, string houseLattitute)
        {
            GeoCoordinate coordinate = new GeoCoordinate(Double.Parse(ChangeToRegionalFormat(houseLattitute)), Double.Parse(ChangeToRegionalFormat(houseLongitute)));
            Dictionary<Respondent, double> distances = Distances(coordinate);
            var nearestRespondent = distances.OrderBy(r => r.Value).First();
            return nearestRespondent.Key;
        }

        private List<Respondent> GetRespondents()
        {
            List<Respondent> respondents = new List<Respondent>();
            dynamic dbRespondents = (from r in db.DB_Respondents
                                     select r);
            if (dbRespondents != null)
            {
                foreach (DB_Respondent item in dbRespondents)
                {
                    respondents.Add(new Respondent
                    {
                        ID = item.ID,
                        LastLongitute = item.LastLongitute,
                        LastLattitute = item.LastLattitute
                    });
                }
                return respondents;
            }
            else
            {
                return null;
            }
        }

        private Dictionary<Respondent, double> Distances(GeoCoordinate coordinate)
        {
            Dictionary<Respondent, double> distances = new Dictionary<Respondent, double>();
            foreach (var item in GetRespondents())
            {
                if (item.LastLattitute != null && item.LastLongitute != null)
                {
                    GeoCoordinate respondentLoc = new GeoCoordinate(Convert.ToDouble(ChangeToRegionalFormat(item.LastLattitute)), Convert.ToDouble(ChangeToRegionalFormat(item.LastLongitute)));
                    distances.Add(item, coordinate.GetDistanceTo(respondentLoc));
                }
            }
            return distances;
        }


        [WebInvoke(Method = "GET", UriTemplate = "Respondent_DutyStatus/?respondentID={respondentID}&status={status}", ResponseFormat = WebMessageFormat.Json)]
        public int Respondent_DutyStatus(string respondentID, string status)
        {
            var respondent = db.DB_Respondents.Where(r => r.ID.Equals(respondentID)).SingleOrDefault();
            if (respondent != null)
            {
                try
                {
                    respondent.Status = int.Parse(status);
                    db.SubmitChanges();
                    return SUCCESS;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return FAIL;
                }
            }
            else
            {
                return FAIL;
            }
        }

        #endregion

        #region Alerts
        [WebInvoke(Method = "GET", UriTemplate = "panic/?userID={userID}&AlertType={AlertType}", ResponseFormat = WebMessageFormat.Json)]
        public Respondent PanicAlert(string userID, int AlertType)
        {
            var owner = db.DB_HouseOwners.Where(o => o.ID.Equals(userID)).SingleOrDefault();
            if (owner != null)
            {
                DB_House house = db.DB_Houses.Where(h => h.OwnerID.Equals(userID)).SingleOrDefault();
                DB_Address address = db.DB_Addresses.Where(a => a.HouseID.Equals(house.HouseID)).SingleOrDefault();
                var nearestRespondent = GetNearestRespondent(address.Longitute, address.Lattitute);
                SubmitAlert(userID, house.HouseID, nearestRespondent.ID, PANIC);
                return nearestRespondent;
            }
            else if (owner == null)
            {
                var occupant = db.DB_HouseOccupants.Where(o => o.ID.Equals(userID)).SingleOrDefault();
                if (occupant != null)
                {
                    DB_House house = db.DB_Houses.Where(h => h.OwnerID.Equals(occupant.HouseOwnerID)).SingleOrDefault();
                    DB_Address address = db.DB_Addresses.Where(a => a.HouseID.Equals(house.HouseID)).SingleOrDefault();
                    var nearestRespondent = GetNearestRespondent(address.Longitute, address.Lattitute);
                    SubmitAlert(userID, house.HouseID, nearestRespondent.ID, PANIC);
                    return nearestRespondent;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private int SubmitAlert(string userID, int houseID, string respondentID, int alertType)
        {
            const int UNATTENDED = -1;
            DB_Alert dbAlert = new DB_Alert
            {
                Date = DateTime.Now,
                AlartMethod = alertType,
                HouseID = houseID,
                Status = UNATTENDED,
                OccupantID = userID,
                RespondentID = respondentID
            };
            try
            {
                db.DB_Alerts.InsertOnSubmit(dbAlert);
                db.SubmitChanges();
                return SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return FAIL;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "alert/?respondentID={respondentID}", ResponseFormat = WebMessageFormat.Json)]
        public GeoLocation CheckAlerts(string respondentID)
        {
            const int ATTENDED = 1;
            const int UNATTENDED = -1;
            var alert = db.DB_Alerts.Where(a => a.RespondentID.Equals(respondentID) && a.Status.Equals(UNATTENDED)).SingleOrDefault();
            if(alert != null)
            {
                alert.Status = ATTENDED;
                try
                {
                    db.SubmitChanges();
                    var address = db.DB_Addresses.Where(a => a.HouseID.Equals(alert.HouseID)).SingleOrDefault();
                    GeoLocation location = new GeoLocation();
                    location.Longitude = address.Longitute;
                    location.Lattitude = address.Lattitute;
                    return location;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

            [WebInvoke(Method = "GET", UriTemplate = "checkalert/?respondentID={respondentID}", ResponseFormat = WebMessageFormat.Json)]
       public string CheckCustomAlerts(string respondentID)
        {
            const int ATTENDED = 1;
            const int UNATTENDED = -1;
            CustomAlert Alert;
            string answer;
            var alert = db.DB_Alerts.Where(a => a.RespondentID.Equals(respondentID) && a.Status.Equals(UNATTENDED)).SingleOrDefault();
            if (alert != null)
            {
                alert.Status = ATTENDED;
                try
                {
                
                    db.SubmitChanges();
                    var address = db.DB_Addresses.Where(a => a.HouseID.Equals(alert.HouseID)).SingleOrDefault();
                    answer = alert.HouseID + " " + alert.OccupantID + " " + address.Longitute + " " + address.Lattitute;
               
                
                    return answer;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
   
        [WebInvoke(Method = "GET", UriTemplate = "cases/?respondentID={respondentID}", ResponseFormat = WebMessageFormat.Json)]
        public List<Case> GetRespondentCases(string respondentID)
        {
            List<Case> cases = new List<Case>();
            dynamic dbCases = db.DB_Cases.Where(c => c.RespondentID.Equals(respondentID));
            if(dbCases != null)
            {
                foreach (DB_Case item in dbCases)
                {
                    cases.Add(new Case
                    {
                        CaseID = item.CaseID,
                        Date = item.Date,
                        ReposponseTime = item.ReposponseTime,
                        ResolutionDate = (DateTime)item.ResolutionDate,
                        Description = item.Description,
                        UserID = item.UserID,
                        HouseID = item.HouseID,
                        RespondentID = item.RespondentID
                    });
                }
                return cases;
            }
            else
            {
                return null;
            }
        }

        private string ChangeToRegionalFormat(string coordinate)
        {
            string newString = "";
            foreach (var item in coordinate)
            {
                if(!item.Equals('.'))
                {
                    newString += item;
                }
                else
                {
                    newString += ',';
                }
            }
            return newString;
        }

        public User GetUserByID(string ID)
        {
            return GetAllUsers().SingleOrDefault(a => a.ID == ID);
        }


        [WebInvoke(Method = "GET", UriTemplate = "activecases/?ownerID={ownerID}", ResponseFormat = WebMessageFormat.Json)]
        public int ActiveCases(string ownerID)
        {
            List<Case> cases = HouseCases(ownerID);
            return cases.Where(a => a.ResolutionDate.Year <= 2000).Count();
        }

        [WebInvoke(Method = "GET", UriTemplate = "activecases", ResponseFormat = WebMessageFormat.Json)]
        public int AllActiveCases()
        {
            return GetAllCases().Where(a => a.ResolutionDate.Year <= 2000).Count();
        }

        [WebInvoke(Method = "GET", UriTemplate = "totaloccupants", ResponseFormat = WebMessageFormat.Json)]
        public int TotalOccupants()
        {
            return db.DB_HouseOccupants.Count();
        }

        [WebInvoke(Method = "GET", UriTemplate = "totalrespondents", ResponseFormat = WebMessageFormat.Json)]
        public int TotalRespondents()
        {
            return db.DB_Respondents.Count();
        }

        [WebInvoke(Method = "GET", UriTemplate = "totalhomeowners", ResponseFormat = WebMessageFormat.Json)]
        public int TotalHomeOwners()
        {
            return db.DB_HouseOwners.Count();
        }

        [WebInvoke(Method = "GET", UriTemplate = "allrespondents", ResponseFormat = WebMessageFormat.Json)]
        public List<User> GetAllRespondents()
        {
            List<User> users = new List<User>();
            dynamic dbUsers = (from r in db.DB_Respondents join u in db.DB_Users on r.ID equals u.ID select u) ?? null;
            if (dbUsers != null)
            {
                foreach (var item in dbUsers)
                {
                    users.Add(new User()
                    {
                        Name = item.Name,
                        Surname = item.Surname,
                        ID = item.ID,
                        Email = item.Email,
                        Password = null,
                    });
                }
                return users;
            }
            return null;
        }

        [WebInvoke(Method = "GET", UriTemplate = "respcases/?respondentID={respondentID}", ResponseFormat = WebMessageFormat.Json)]
        public List<Case> GetCasesByRespondent(string respondentID)
        {
            return GetAllCases().FindAll(a => a.RespondentID == respondentID);
        }

        private List<int> GetMixedCases()
        {
            DateTime date = DateTime.Now;
            DateTime LastDay = new DateTime(date.Year, date.Month - 1, DateTime.DaysInMonth(date.Year, date.Month - 1));
            int theLastDay = LastDay.Day;
            int Counter = 0;
            DateTime today = DateTime.Now, LastDays = new DateTime(today.Year, today.Month - 1, theLastDay - (6 - today.Day));
            dynamic numberCases = (from cs in db.DB_Cases where cs.Date >= LastDays && cs.Date <= today select cs);
            List<int> Cases = new List<int>();

            for (int i = theLastDay - (6 - today.Day); i <= theLastDay; i++)
            {
                DateTime newDate = new DateTime(today.Year, today.Month - 1, i);
                foreach (var item in numberCases)
                {
                    if (newDate.ToShortDateString() == Convert.ToDateTime(item.Date).ToShortDateString())
                        Counter++;
                }
                Cases.Add(Counter);
                Counter = 0;
            }
            for (int i = 1; i <= today.Day; i++)
            {
                DateTime newDate = new DateTime(today.Year, today.Month, i);
                foreach (var item in numberCases)
                {
                    if (newDate.ToShortDateString() == Convert.ToDateTime(item.Date).ToShortDateString())
                        Counter++;
                }
                Cases.Add(Counter);
                Counter = 0;
            }
            return Cases;
        }
        [WebInvoke(Method = "GET", UriTemplate = "sevendays", ResponseFormat = WebMessageFormat.Json)]
        public List<int> GetCasesForLastSevenDays()
        {
            if (DateTime.Now.Day < 7)
                return GetMixedCases();
            DateTime today = DateTime.Now, lastSeven = new DateTime(today.Year, today.Month, today.Day - 6); ;
            dynamic numberCases = (from cs in db.DB_Cases where cs.Date >= lastSeven && cs.Date <= today select cs);
            List<int> cases = new List<int>();
            int counter = 0;


            for (int i = 7; i > 0; i--)
            {
                DateTime newDate = new DateTime(today.Year, today.Month, today.Day - (i - 1));
                foreach (var item in numberCases)
                {
                    if (newDate.ToShortDateString() == Convert.ToDateTime(item.Date).ToShortDateString())
                        counter++;
                }
                cases.Add(counter);
                counter = 0;
            }
            return cases;
        }
        [WebInvoke(Method = "GET", UriTemplate = "allalerts", ResponseFormat = WebMessageFormat.Json)]
        public List<Alert> GetAlerts()
        {
            List<Alert> alerts = new List<Alert>();
            dynamic dbAlerts = db.DB_Alerts.Where(a => a.AlertID != 0);
            if(dbAlerts != null)
            {
                foreach (DB_Alert item in dbAlerts)
                {
                    alerts.Add(new Alert
                    {
                        Date = item.Date,
                        OccupantID = item.OccupantID,
                        RespondentID = item.RespondentID,
                        Status = item.Status
                    });
                }
                return alerts;
            }
            else
            {
                return null;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "respalerts/?respondentID={respondentID}", ResponseFormat = WebMessageFormat.Json)]
        public List<Alert> GetRespondentAlerts(string respondentID)
        {
            List<Alert> alerts = new List<Alert>();
            dynamic dbAlerts = db.DB_Alerts.Where(a => a.RespondentID.Equals(respondentID));
            if (dbAlerts != null)
            {
                foreach (DB_Alert item in dbAlerts)
                {
                    alerts.Add(new Alert
                    {
                        Date = item.Date,
                        OccupantID = item.OccupantID,
                        RespondentID = item.RespondentID,
                        Status = item.Status
                    });
                }
                return alerts;
            }
            else
            {
                return null;
            }
        }

        [WebInvoke(Method = "GET", UriTemplate = "useralerts/?userID={userID}", ResponseFormat = WebMessageFormat.Json)]
        public List<Alert> GetUserAlerts(string userID)
        {
            List<Alert> alerts = new List<Alert>();
            dynamic dbAlerts = db.DB_Alerts.Where(a => a.OccupantID.Equals(userID));
            if (dbAlerts != null)
            {
                foreach (DB_Alert item in dbAlerts)
                {
                    alerts.Add(new Alert
                    {
                        AlertID = item.AlertID,
                        Date = item.Date,
                        OccupantID = item.OccupantID,
                        RespondentID = item.RespondentID,
                        Status = item.Status,
                        StringDate = item.Date.ToShortDateString().ToString()
                    });
                }
                return alerts;
            }
            else
            {
                return null;
            }
        }
        [WebInvoke(Method = "GET", UriTemplate = "getBestRespondent", ResponseFormat = WebMessageFormat.Json)]
        public List<string> getBestRespondent()
        {
            List<string> resData = new List<string>();
            int count = -1;
            String respId = "";

            dynamic t = (from p in db.DB_Cases
                         group p by p.RespondentID into g
                         select new { RespondentID = g.Key, count = g.Count() });

            foreach (var v in t)
            {
                if (count < v.count)
                {
                    count = v.count;
                    respId = v.RespondentID;
                        
                }
            };
            resData.Add(respId);
            resData.Add("" + count);
            return resData;
        }
        [WebInvoke(Method = "GET", UriTemplate = "getWorstRespondent", ResponseFormat = WebMessageFormat.Json)]
        public List<string> getWorstRespondent()
        {
            List<string> resData = new List<string>();
            double count = Double.PositiveInfinity;
            String respId = "";

            dynamic t = (from p in db.DB_Cases
                         group p by p.RespondentID into g
                         select new { RespondentID = g.Key, count = g.Count() });

            foreach (var v in t)
            {
                if (count > v.count)
                {
                    count = v.count;
                    respId = v.RespondentID;
                }
            };
            resData.Add(respId);
            resData.Add(""+count);
            return resData;
        }

        [WebInvoke(Method = "GET", UriTemplate = "Top5Cities", ResponseFormat = WebMessageFormat.Json)]
        public List<String> Top5Cities()
        {
            List<String> cityNames = new List<String>();
            List<int> cityCount = new List<int>();
            List<String> top5Names = new List<String>();

            dynamic t = (from p in db.DB_Cases
                         group p by p.HouseID into g
                         orderby g.Count() descending
                         select new { HoueseId = g.Key, count = g.Count() });
            //Get names of all the cities from all the houses
            foreach (var v in t)
            {
                int id = v.HoueseId;
                var address = db.DB_Addresses.Where(u => u.HouseID.Equals(id)).SingleOrDefault();

                if (!cityNames.Contains(address.City))
                    cityNames.Add(address.City);
            }

            //Count total for each city
            foreach (string p in cityNames)
            {
                int total = 0;
                foreach (var v in t)
                {
                    int id = v.HoueseId;
                    var address = db.DB_Addresses.Where(u => u.HouseID.Equals(id)).SingleOrDefault();
                    if (address.City.Equals(p) ) 
                        total += v.count;   
                }
                cityCount.Add(total);
            }

            //Find top 5
            int i = 5;
            while(i>0 && cityCount.Count>0)
            {
                int index = cityCount.IndexOf(cityCount.Max());
                top5Names.Add(cityNames[index]);
                cityCount.RemoveAt(index);
                i--;
            }

            return cityNames;
        }

        [WebInvoke(Method = "GET", UriTemplate = "Top5Surbub/?CityName={CityName}", ResponseFormat = WebMessageFormat.Json)]
        public List<String> Top5Surbub(String CityName)
        {
            List<String> SuburbNames = new List<String>();
            List<int> SuburbCount = new List<int>();
            List<String> top5Names = new List<String>();

            dynamic t = (from p in db.DB_Cases
                         group p by p.HouseID into g
                         orderby g.Count() descending
                         select new { HoueseId = g.Key, count = g.Count() });
            //Get names of all the cities from all the houses
            foreach (var v in t)
            {
                int id = v.HoueseId;
                var address = db.DB_Addresses.Where(u => u.HouseID.Equals(id) && u.City.Equals(CityName)).SingleOrDefault();
                if (address != null) { 
                    if (!SuburbNames.Contains(address.Surburb))
                        SuburbNames.Add(address.Surburb);
                 }
            }

            //Count total for each city
            foreach (string p in SuburbNames)
            {
                int total = 0;
                foreach (var v in t)
                {
                    int id = v.HoueseId;
                    var address = db.DB_Addresses.Where(u => u.HouseID.Equals(id)).SingleOrDefault();
                    if (address.Surburb.Equals(p))
                        total += v.count;
                }
                SuburbCount.Add(total);
            }

            //Find top 5
            int i = 5;
            while (i > 0 && SuburbCount.Count > 0)
            {
                int index = SuburbCount.IndexOf(SuburbCount.Max());
                top5Names.Add(SuburbNames[index]);
                SuburbCount.RemoveAt(index);
                i--;
            }

            return SuburbNames;
        }
        [WebInvoke(Method = "GET", UriTemplate = "GetAllAdresses", ResponseFormat = WebMessageFormat.Json)]
        public List<Address> GetAllAdresses()
        {
            List<Address> addresses = new List<Address>();
            dynamic t = db.DB_Addresses.Where(u => u.ID !=null );
            if (t != null)
            {
                foreach (DB_Address items in t)
                {
                    addresses.Add(new Address
                    {
                        ID = items.ID,
                        Province = items.Province,
                        City = items.City,
                        Surburb = items.Surburb,
                        StreetName = items.StreetName,
                        HouseID = (int)items.HouseID,
                        ZIPCode = items.ZIPCode,
                        Longitute = items.Longitute,
                        Lattitute = items.Lattitute,
                        HouseNo = items.HouseNo
                    });
                }
                return addresses;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Area Cases
        public List<Address> getAffectedAddresses(String city)
        {
            List<Address> AffectedAddresses = new List<Address>();
            dynamic db_Addresses = (from o in db.DB_Addresses
                                    where o.City.Equals(city)
                                    select o);

            if (db_Addresses != null)
            {
                foreach (DB_Address item in db_Addresses)
                {
                    AffectedAddresses.Add( new Address
                    {
                        ID = item.ID,
                        Province = item.Province,
                        City = item.City,
                        Surburb = item.Surburb,
                        StreetName = item.StreetName,
                        HouseID = (int)item.HouseID,
                        ZIPCode = item.ZIPCode,
                        Longitute = item.Longitute,
                        Lattitute = item.Lattitute,
                        HouseNo = item.HouseNo
                    });

                }
            }
            return AffectedAddresses;
        }


        public List<House> getParticularHouses(String city)
        {
            List<House> particularHouses = new List<House>();
            List<Address> affectedAddresses = getAffectedAddresses(city);

            foreach (Address address in affectedAddresses)
            {
                dynamic db_house = (from o in db.DB_Houses
                                    where o.HouseID.Equals(address.HouseID)
                                    select o);

                if (db_house != null)
                {
                    foreach (DB_House item in db_house)
                    {
                        particularHouses.Add(new House
                        {
                            HouseID = item.HouseID,
                            OwnerID = item.OwnerID,
                            AlarmStatus = item.AlarmStatus,
                            HouseAddress = address
                        });
                    }
                }

            }
            return particularHouses;
        }


        [WebInvoke(Method = "GET", UriTemplate = "Areacases/?city={city}", ResponseFormat = WebMessageFormat.Json)]
        public List<Case> GetAreaCases(String city)
        {
            List<Case> cases = new List<Case>();
            List<House> houses = getParticularHouses(city);

            //get cases from the Database match it with the particular houses to find one that hase cases in that 
            //particular place


            foreach (House h in houses)
            {
                dynamic cases_We_Want = (from o in db.DB_Cases
                                         where o.HouseID.Equals(h.HouseID)
                                         select o);

                if (cases_We_Want != null)
                {
                    foreach (DB_Case c in cases_We_Want)
                    {
                        cases.Add(new Case
                        {
                            CaseID = c.CaseID,
                            Date = c.Date,
                            ReposponseTime = c.ReposponseTime,
                            ResolutionDate = (DateTime)c.ResolutionDate,
                            Description = c.Description,
                            UserID = c.UserID,
                            HouseID = c.HouseID,
                            RespondentID = c.RespondentID
                        });
                    }
                }



            }

            return cases;
        }


        #endregion

        #region Cases Interval

        [WebInvoke(Method = "GET", UriTemplate = "GetTimeInterval/?", ResponseFormat = WebMessageFormat.Json)]
        public String GetTimeInterval()
        {
            List<DateTime> CasesTime = new List<DateTime>();

            dynamic dates = (from o in db.DB_Cases
                             select o);

            if (dates != null)
            {
                foreach (DB_Case item in dates)
                {
                    DateTime dateTime = item.Date;
                    CasesTime.Add(dateTime);
                }
            }

            int interval1 = 0;
            int interval2 = 0;
            int interval3 = 0;
            int interval4 = 0;
            int interval5 = 0;
            int interval6 = 0;
            int interval7 = 0;
            int interval8 = 0;



            foreach (DateTime o in CasesTime)
            {
                if (o.Hour >= 0 && o.Hour <= 3) //00:00 - 03:00
                {
                    interval1++;
                }
                else if (o.Hour >= 3 && o.Hour <= 6) //03:00 - 06:00
                {
                    interval2++;
                }
                else if (o.Hour >= 6 && o.Hour <= 9) //06:00 - 09:00
                {
                    interval3++;
                }
                else if (o.Hour >= 9 && o.Hour <= 12) //09:00 - 12:00
                {
                    interval4++;
                }
                else if (o.Hour >= 12 && o.Hour <= 15) //12:00 - 15:00
                {
                    interval5++;
                }
                else if (o.Hour >= 15 && o.Hour <= 18) //15:00 - 18:00
                {
                    interval6++;
                }
                else if (o.Hour >= 18 && o.Hour <= 21) //18:00 - 21:00
                {
                    interval7++;
                }
                else if (o.Hour >= 21 && o.Hour <= 23) //21:00 - 23:00
                {
                    interval8++;
                }

            }


            String TimeCasesInterval = "00:00 - 03:00: " + interval1 + " cases \n";
            TimeCasesInterval += "03H00 - 06H00: " + interval2 + " cases \n";
            TimeCasesInterval += "06H00 - 09H00: " + interval3 + " cases \n";
            TimeCasesInterval += "09H00 - 12H00: " + interval4 + " cases \n";
            TimeCasesInterval += "12H00 - 15H00: " + interval5 + " cases \n";
            TimeCasesInterval += "15H00 - 18H00: " + interval6 + " cases \n";
            TimeCasesInterval += "18H00 - 21H00: " + interval7 + " cases \n";
            TimeCasesInterval += "21H00 - 23H00: " + interval8 + " cases \n";

            return TimeCasesInterval;
        }

        [WebInvoke(Method = "GET", UriTemplate = "trigger/?userID={userID}&triggerType={triggerType}", ResponseFormat = WebMessageFormat.Json)]
        public int TriggerAlarm(string userID, int triggerType)
        {
            var house = GetUserHome(userID);
            var dbHouse = db.DB_Houses.Where(h => h.HouseID.Equals(house.HouseID)).SingleOrDefault();
            dbHouse.AlarmStatus = triggerType;
            try
            {
                db.SubmitChanges();
                return SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return FAIL;
            }

        }


        [WebInvoke(Method = "GET", UriTemplate = "housealerts/?userID={userID}", ResponseFormat = WebMessageFormat.Json)]
        public string CheckHomeAlerts(string userID)
        {
            var house = GetUserHome(userID);
            if(house.AlarmStatus == ALARM_TRIGGERED)
            {
                var dbHouse = db.DB_Houses.Where(h => h.HouseID.Equals(house.HouseID)).SingleOrDefault();
                dbHouse.AlarmStatus = ALARM_OFF;
                db.SubmitChanges();
                return TRIG;
            }
            else
            {
                return "";
            }
        }
        public string getUserFullname(string id)
        {
            User user = GetUserByID(id);
            return user.Name + " "+ user.Surname;
        }
        [WebInvoke(Method = "GET", UriTemplate = "getResCases/?respondentID={respondentID}", ResponseFormat = WebMessageFormat.Json)]
        public List<StringDateCase> GetResCases(string respondentID)
        {
            List<StringDateCase> cases = new List<StringDateCase>();
            dynamic dbCases = db.DB_Cases.Where(c => c.RespondentID.Equals(respondentID));
            String alertType = "";
            if (dbCases != null)
            {
                foreach (DB_Case item in dbCases)
                {
                    if (item.Type == 3)
                        alertType = "Window Break-in";
                    else if (item.Type == 2)
                        alertType = "Door Break-in";
                    else if (item.Type == 1)
                        alertType = "Panic Alert";
                    cases.Add(new StringDateCase
                    {

                        Date = String.Format("{0:dd/MM/yyyy}", item.Date),
                        Time = item.Date.ToString("hh:mm tt"),
                        fullnames = getUserFullname(item.UserID),
                        Description = "Break-in Type: " + alertType + "\n" + item.Description,


                    });
                }
                return cases;
            }
            else
            {
                return null;
            }
        }
        #endregion
        public List<GeoLocation> GetGeoLocations()
        {
            dynamic addresses = (from addr in db.DB_Addresses
                                 join
                                 house in db.DB_Houses on addr.HouseID equals house.HouseID
                                 join a in db.DB_Alerts on house.HouseID equals a.HouseID
                                 select addr) ?? null;
            List<GeoLocation> Locations = new List<GeoLocation>();
            if (addresses != null)
            {
                foreach (DB_Address adrs in addresses)
                {
                    if (adrs.Lattitute != null || adrs.Longitute != null)
                    {
                        Locations.Add(new GeoLocation()
                        {
                            Lattitude = adrs.Lattitute,
                            Longitude = adrs.Longitute
                        });
                    }

                }
            }
            return Locations;
        }

        public GeoLocation GetRespondentLocation(string userID)
        {
            var location = db.DB_Respondents.Where(a => a.ID == userID).FirstOrDefault() ?? null;
            if (location != null)
                return new GeoLocation()
                {
                    Lattitude = location.LastLattitute,
                    Longitude = location.LastLongitute
                };
            return null;
        }

        [WebInvoke(Method = "GET", UriTemplate = "addoccupant/?name={name}&surname={surname}&id={id}&email={email}&ownwerID={ownwerID}", ResponseFormat = WebMessageFormat.Json)]
        public int AddOccupant(string name, string surname, string id, string email, string ownwerID)
        {
            User user = new User
            {
                ID = id,
                Name = name,
                Surname = surname,
                Email = email,
                Password = Secrecy.HashPassword(email),
                DateRegistered = DateTime.Now,
                UserType = 1,
                IsActive = 1
            };
            return RegisterOccupant(user, ownwerID);
        }

        public int SolveCase(int caseID)
        {
            var resolveCase = db.DB_Cases.Where(a => a.CaseID == caseID).FirstOrDefault();
            if (resolveCase == null)
                return -1;

            try
            {
                resolveCase.ResolutionDate = DateTime.Now;
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                return -1;
            }
            return 1;
        }

        public int GetStatus(string userID)
        {
            var res = db.DB_Respondents.Where(a => a.ID == userID).FirstOrDefault();
            if(res != null)
                return Convert.ToInt32(res.Status);

            return 0;
        }
    }
}
