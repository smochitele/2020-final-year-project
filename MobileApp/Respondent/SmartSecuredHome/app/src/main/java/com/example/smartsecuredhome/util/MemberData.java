package com.example.smartsecuredhome.util;

public class MemberData {
    String name_surname;
    String email;
    String idnum;

    public MemberData(String name_surname, String email, String idnum) {
        this.name_surname = name_surname;
        this.email = email;
        this.idnum = idnum;
    }

    public void setName_surname(String name_surname) {
        this.name_surname = name_surname;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public void setIdnum(String idnum) {
        this.idnum = idnum;
    }

    public String getName_surname() {
        return name_surname;
    }

    public String getEmail() {
        return email;
    }

    public String getIdnum() {
        return idnum;
    }


}
