package com.example.smartsecuredhome.ui;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class Alert extends CustomAlert {
   private String date;
   private String time;
   private String responceTime;
  public  Alert(CustomAlert alert){
       this.HouseID=alert.HouseID;
       this.id=alert.id;
       this.setOccupantID(alert.OccupantID);
       date = new SimpleDateFormat("dd/MM/yyyy", Locale.getDefault()).format(new Date());
       time = new SimpleDateFormat("HH:mm:ss", Locale.getDefault()).format(new Date());

    }

    public Alert() {
    }

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public String getTime() {
        return time;
    }

    public void setTime(String time) {
        this.time = time;
    }

    public String getResponceTime() {
        return responceTime;
    }

    public void setResponceTime(String responceTime) {
        this.responceTime = responceTime;
    }
}
