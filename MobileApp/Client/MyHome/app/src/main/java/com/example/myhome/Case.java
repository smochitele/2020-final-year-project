package com.example.myhome;

public class Case {
    private int AlertID;
    private int CaseID;
    private String Date;
    private String ReposponseTime;
    private String ResolutionDate;
    private String Description;
    private String UserID;
    private int HouseID;
    private String StringDate;

    private String RespondentID;

    public int getCaseID() {
        return AlertID;
    }

    public void setCaseID(int caseID) {
        CaseID = caseID;
    }

    public String getDate() {
        return Date;
    }

    public void setDate(String date) {
        Date = date;
    }

    public String getReposponseTime() {
        return ReposponseTime;
    }

    public void setReposponseTime(String reposponseTime) {
        ReposponseTime = reposponseTime;
    }

    public String getResolutionDate() {
        return ResolutionDate;
    }

    public void setResolutionDate(String resolutionDate) {
        ResolutionDate = resolutionDate;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public String getUserID() {
        return UserID;
    }

    public void setUserID(String userID) {
        UserID = userID;
    }

    public int getHouseID() {
        return HouseID;
    }

    public void setHouseID(int houseID) {
        HouseID = houseID;
    }

    public String getRespondentID() {
        return RespondentID;
    }

    public void setRespondentID(String respondentID) {
        RespondentID = respondentID;
    }

    public int getAlertID() {
        return AlertID;
    }

    public void setAlertID(int alertID) {
        AlertID = alertID;
    }

    public String getStringDate() {
        return StringDate;
    }

    public void setStringDate(String stringDate) {
        StringDate = stringDate;
    }
}
