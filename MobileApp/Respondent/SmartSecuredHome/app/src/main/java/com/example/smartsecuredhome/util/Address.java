package com.example.smartsecuredhome.util;

public class Address {
    private int ID ;
    private String Province ;
    private String City ;
    private String Surburb ;
    private String StreetName ;
    private String HouseNo ;
    private String ZIPCode ;
    private String Longitute ;
    private String Lattitute ;
    private int HouseID ;

    public Address() {

    }

    public Address(int ID, String province, String city, String surburb, String streetName, String houseNo, String ZIPCode, String longitute, String lattitute, int houseID) {
        this.ID = ID;
        Province = province;
        City = city;
        Surburb = surburb;
        StreetName = streetName;
        HouseNo = houseNo;
        this.ZIPCode = ZIPCode;
        Longitute = longitute;
        Lattitute = lattitute;
        HouseID = houseID;
    }

    public int getID() {
        return ID;
    }

    public void setID(int ID) {
        this.ID = ID;
    }

    public String getProvince() {
        return Province;
    }

    public void setProvince(String province) {
        Province = province;
    }

    public String getCity() {
        return City;
    }

    public void setCity(String city) {
        City = city;
    }

    public String getSurburb() {
        return Surburb;
    }

    public void setSurburb(String surburb) {
        Surburb = surburb;
    }

    public String getStreetName() {
        return StreetName;
    }

    public void setStreetName(String streetName) {
        StreetName = streetName;
    }

    public String getHouseNo() {
        return HouseNo;
    }

    public void setHouseNo(String houseNo) {
        HouseNo = houseNo;
    }

    public String getZIPCode() {
        return ZIPCode;
    }

    public void setZIPCode(String ZIPCode) {
        this.ZIPCode = ZIPCode;
    }

    public String getLongitute() {
        return Longitute;
    }

    public void setLongitute(String longitute) {
        Longitute = longitute;
    }

    public String getLattitute() {
        return Lattitute;
    }

    public void setLattitute(String lattitute) {
        Lattitute = lattitute;
    }

    public int getHouseID() {
        return HouseID;
    }

    public void setHouseID(int houseID) {
        HouseID = houseID;
    }
}
