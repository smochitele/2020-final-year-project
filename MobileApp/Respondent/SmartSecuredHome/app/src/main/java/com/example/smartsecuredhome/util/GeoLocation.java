package com.example.smartsecuredhome.util;

public class GeoLocation {
    private String Lattitude;
    private String Longitude;


    public GeoLocation()
    {

    }

    public GeoLocation(String Longitude, String Lattitude) {
        this.Longitude = Longitude;
        this.Lattitude = Lattitude;
    }
    public String getLattitude() {
        return Lattitude;
    }

    public String getLongitude() {
        return Longitude;
    }
}
