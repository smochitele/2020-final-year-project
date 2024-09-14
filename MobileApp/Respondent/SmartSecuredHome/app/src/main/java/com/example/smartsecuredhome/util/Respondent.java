package com.example.smartsecuredhome.util;

import java.util.Date;

public class Respondent {

    private String ID;
    private String Name;
    private String Surname;
    private String Email;
    private String Password;
    private int IsActive;
    private int UserType;
    private String DateRegistered;

    public Respondent(String id, String name, String surname, String email) {
        this.ID = id;
        this.Name = name;
        this.Surname = surname;
        this.Email = email;
    }

    public Respondent() {

    }

    public String getId() {
        return ID;
    }

    public String getName() {
        return Name;
    }

    public String getSurname() {
        return Surname;
    }

    public String getEmail() {
        return Email;
    }

    public String getPassword() {
        return Password;
    }

    public int getIsActive() {
        return IsActive;
    }

    public int getUserType() {
        return UserType;
    }

    public String getDateRegistered() {
        return DateRegistered;
    }
}
