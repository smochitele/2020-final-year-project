package com.example.myhome;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;

public class Adapter extends RecyclerView.Adapter<Template> {

    private Context context;
    private ArrayList<Model> models;

    public Adapter(Context context, ArrayList<Model> models) {
        this.models = models;
        this.context = context;
    }

    @NonNull
    @Override
    public Template onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.row, null);
        return new Template(view);
    }

    @Override
    public void onBindViewHolder(@NonNull Template holder, int position) {
        holder.occupantName.setText(models.get(position).getOccupantName());
        holder.occupantEmail.setText(models.get(position).getOccupantEmail());
        //holder.imgAvatar.setImageResource(models.get(position).getImgAvatar());
    }

    @Override
    public int getItemCount() {
        return models.size();
    }
}
