package com.example.myhome;

import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

public class Template extends RecyclerView.ViewHolder {

    public TextView occupantName;
    public TextView occupantEmail;
    public ImageView imgAvatar;

    public Template(@NonNull View itemView) {
        super(itemView);
        this.setImgAvatar(itemView.findViewById(R.id.imgAvatar));
        this.setOccupantName(itemView.findViewById(R.id.occupantName));
        this.setOccupantEmail(itemView.findViewById(R.id.occupantEmail));
    }

    public TextView getOccupantName() {
        return occupantName;
    }

    public void setOccupantName(TextView occupantName) {
        this.occupantName = occupantName;
    }

    public TextView getOccupantEmail() {
        return occupantEmail;
    }

    public void setOccupantEmail(TextView occupantEmail) {
        this.occupantEmail = occupantEmail;
    }

    public ImageView getImgAvatar() {
        return imgAvatar;
    }

    public void setImgAvatar(ImageView imgAvatar) {
        this.imgAvatar = imgAvatar;
    }


}
