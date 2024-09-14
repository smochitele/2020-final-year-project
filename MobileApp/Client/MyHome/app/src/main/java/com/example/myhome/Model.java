package com.example.myhome;

import android.widget.ImageView;

public class Model {
    private String occupantName;
    private String occupantEmail;
    private int imgAvatar;

    public String getOccupantName() {
        return occupantName;
    }

    public void setOccupantName(String occupantName) {
        this.occupantName = occupantName;
    }

    public String getOccupantEmail() {
        return occupantEmail;
    }

    public void setOccupantEmail(String occupantEmail) {
        this.occupantEmail = occupantEmail;
    }

    public int getImgAvatar() {
        return imgAvatar;
    }

    public void setImgAvatar(int imgAvatar) {
        this.imgAvatar = imgAvatar;
    }
}
