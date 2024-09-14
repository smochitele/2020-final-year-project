package com.example.smartsecuredhome.ui;

public class CustomAlert {
    public int id=-1;
    public int HouseID=-1 ;
    public String OccupantID="" ;
    public String Lattitude="" ;
    public String Longitude="" ;

    public CustomAlert() {
    }

    public CustomAlert(int id, int houseID, String occupantID, String lattitude, String longitude) {
        this.id = id;
        HouseID = houseID;
        OccupantID = occupantID;
        Lattitude = lattitude;
        Longitude = longitude;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getHouseID() {
        return HouseID;
    }

    public void setHouseID(int houseID) {
        HouseID = houseID;
    }

    public String getOccupantID() {
        return OccupantID;
    }

    public void setOccupantID(String occupantID) {
        OccupantID = occupantID;
    }

    public String getLattitude() {
        return Lattitude;
    }

    public void setLattitude(String lattitude) {
        Lattitude = lattitude;
    }

    public String getLongitude() {
        return Longitude;
    }

    public void setLongitude(String longitude) {
        Longitude = longitude;
    }
}
