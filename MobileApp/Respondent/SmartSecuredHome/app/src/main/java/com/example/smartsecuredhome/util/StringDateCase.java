package com.example.smartsecuredhome.util;

import java.sql.Date;

public class StringDateCase {

    public String Date ;
    public String Time;
    public String Description;
    public String fullnames;

    public StringDateCase() {
    }

    public StringDateCase(String date, String time, String description, String fullnames) {
        Date = date;
        Time = time;
        Description = description;
        this.fullnames = fullnames;
    }

    public String getDate() {
        return Date;
    }

    public void setDate(String date) {
        Date = date;
    }

    public String getTime() {
        return Time;
    }

    public void setTime(String time) {
        Time = time;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public String getFullnames() {
        return fullnames;
    }

    public void setFullnames(String fullnames) {
        this.fullnames = fullnames;
    }
}
