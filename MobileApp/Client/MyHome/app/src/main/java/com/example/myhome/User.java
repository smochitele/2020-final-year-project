package com.example.myhome;

import java.util.Date;

public class User {
    private String ID;
    private String Name;
    private String Surname;
    private String Email;
    private String Password;
    private int IsActive;
    private int UserType;
    private String DateRegistered;

    public String getID() {
        return ID;
    }

    public void setID(String ID) {
        this.ID = ID;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getSurname() {
        return Surname;
    }

    public void setSurname(String surname) {
        Surname = surname;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public int getIsActive() {
        return IsActive;
    }

    public void setIsActive(int isActive) {
        IsActive = isActive;
    }

    public int getUserType() {
        return UserType;
    }

    public void setUserType(int userType) {
        UserType = userType;
    }

    public String getDateRegistered() {
        return DateRegistered;
    }

    public void setDateRegistered(String dateRegistered) {
        DateRegistered = dateRegistered;
    }
}
