package com.example.smartsecuredhome.ui.slideshow;

public class Case {
    String Date; String ResponceTime; String Discription;     String RespondentName;

    public Case(String date, String responceTime, String discription, String respondentName) {
        Date = date;
        ResponceTime = responceTime;
        Discription = discription;
        RespondentName = respondentName;
    }


    public Case() {
        Date = "";
        ResponceTime = "";
        Discription = "";
    }

    public Case(String date, String responceTime, String discription) {

        Date = date;
        ResponceTime = responceTime;
        Discription = discription;
        //this.thisRespondent = thisRespondent;
    }

    public String getRespondentName() {
        return RespondentName;
    }

    public void setRespondentName(String respondentName) {
        RespondentName = respondentName;
    }

    public void setDate(String date) {
        Date = date;
    }

    public void setResponceTime(String responceTime) {
        ResponceTime = responceTime;
    }

    public void setDiscription(String discription) {
        Discription = discription;
    }



    public String getDate() {
        return Date;
    }

    public String getResponceTime() {
        return ResponceTime;
    }

    public String getDiscription() {
        return Discription;
    }
}
