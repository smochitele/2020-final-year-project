package com.example.myhome;

public class House {
    private int HouseID;
    private int AlarmStatus;
    private String OwnerID;
    private Address HouseAddress;

    public int getHouseID() {
        return HouseID;
    }

    public void setHouseID(int houseID) {
        HouseID = houseID;
    }

    public int getAlarmStatus() {
        return AlarmStatus;
    }

    public void setAlarmStatus(int alarmStatus) {
        AlarmStatus = alarmStatus;
    }

    public String getOwnerID() {
        return OwnerID;
    }

    public void setOwnerID(String ownerID) {
        OwnerID = ownerID;
    }

    public Address getHouseAddress() {
        return HouseAddress;
    }

    public void setHouseAddress(Address houseAddress) {
        HouseAddress = houseAddress;
    }
}
