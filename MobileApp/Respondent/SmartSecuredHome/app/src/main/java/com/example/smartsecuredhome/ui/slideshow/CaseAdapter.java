package com.example.smartsecuredhome.ui.slideshow;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.example.smartsecuredhome.R;

import java.util.ArrayList;

public class CaseAdapter extends ArrayAdapter<Case> {

    private Context mContext;
    private int mResource;
    private int lastPosition = -1;
    /**
     * Holds variables in a View
     */
    private static class ViewHolder {
        TextView name;
        TextView birthday;
        TextView sex;
    }

    /**
     * Default constructor for the PersonListAdapter
     * @param context
     * @param resource
     * @param objects
     */
    public CaseAdapter(Context context, int resource, ArrayList<Case> objects) {
        super(context, resource, objects);
        mContext = context;
        mResource = resource;
    }

    @NonNull
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        //get the persons information
        String name = getItem(position).getRespondentName();
        String email = getItem(position).getDate();
        String idnum = getItem(position).getResponceTime();

        //Create the person object with the information
        MemberData person = new MemberData(name,email,idnum);

        //create the view result for showing the animation
        final View result;

        //ViewHolder object
        CaseAdapter.ViewHolder holder;


        if(convertView == null){
            LayoutInflater inflater = LayoutInflater.from(mContext);
            convertView = inflater.inflate(mResource, parent, false);
            holder= new CaseAdapter.ViewHolder();
            holder.name = (TextView) convertView.findViewById(R.id.textView1);
            holder.birthday = (TextView) convertView.findViewById(R.id.textView2);
            holder.sex = (TextView) convertView.findViewById(R.id.textView3);

            result = convertView;

            convertView.setTag(holder);
        }
        else{
            holder = (CaseAdapter.ViewHolder) convertView.getTag();
            result = convertView;
        }


        Animation animation = AnimationUtils.loadAnimation(mContext,
                (position > lastPosition) ? R.anim.load_down_anim : R.anim.load_up_anim);
        result.startAnimation(animation);
        lastPosition = position;

        holder.name.setText(person.getName_surname());
        holder.birthday.setText(person.getEmail());
        holder.sex.setText(person.getIdnum());


        return convertView;
    }
}
