package com.example.smartsecuredhome;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

public class Profile extends AppCompatActivity {
    androidx.appcompat.widget.Toolbar toolbar;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);
        toolbar=findViewById(R.id.toolbarb);
        toolbar.setTitle(R.string.profile);
        setSupportActionBar(toolbar);
        toolbar.setNavigationIcon(R.drawable.ic_baseline_arrow_back_24);
        toolbar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onBackPressed();
            }
        });

        TextView resID = findViewById(R.id.resID);
        TextView resName = findViewById(R.id.resName);
        TextView resSurname = findViewById(R.id.resSurname);
        TextView resEmail = findViewById(R.id.resEmail);
        TextView totalcases =findViewById(R.id.casecount);
        resID.setText(   Sign.loggedUser.getId());
        resName.setText( Sign.loggedUser.getName() );
        resSurname.setText( Sign.loggedUser.getSurname());
        resEmail.setText( Sign.loggedUser.getEmail());
        totalcases.setText("Responded to "+Sign.cases.size()+" cases");
    }

}