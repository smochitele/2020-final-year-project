package com.example.smartsecuredhome;

import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Dialog;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;


import com.example.smartsecuredhome.ui.CustomAlert;
import com.example.smartsecuredhome.ui.slideshow.Case;
import com.example.smartsecuredhome.ui.slideshow.CaseAdapter;
import com.example.smartsecuredhome.util.StringDateCase;

import java.util.ArrayList;
import java.util.List;

public class caseHistory extends AppCompatActivity {
    ArrayList<Case> Cases;
    //ExpandableListAdapter listAdapter
    ListView listView;
    public static List<StringDateCase> cases ;
    Dialog myDialog;
    androidx.appcompat.widget.Toolbar toolbar;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_case_history);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);

        myDialog = new Dialog(this);
        toolbar=findViewById(R.id.toolbarb);
        toolbar.setTitle(R.string.add_case_title);
        setSupportActionBar(toolbar);
        toolbar.setNavigationIcon(R.drawable.ic_baseline_arrow_back_24);

        listView =(ListView) findViewById(R.id.list_view);
        //  expandableListView.setAdapter(listAdapter);
        createCases();
        CaseAdapter listAdapter=new CaseAdapter(this,R.layout.caselist_adapter,Cases);
        listView.setAdapter(listAdapter);
        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

                myDialog.setContentView(R.layout.popup);
                myDialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
                Button button=myDialog.findViewById(R.id.back) ;
                TextView des=myDialog.findViewById(R.id.txtDes2) ;
                des.setText(cases.get(i).getDescription());
                WindowManager.LayoutParams lp = myDialog.getWindow().getAttributes();
                lp.dimAmount = 0.0f;
                myDialog.show();
                button.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        myDialog.dismiss();
                    }
                });
            }
        });
        toolbar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onBackPressed();
            }
        });
    }


    public void createCases(){
        cases = Sign.cases;
        Cases =new ArrayList();
        for (StringDateCase p: cases
             ) {
            Case case1=new Case(p.getDate(), p.getTime(), p.Description,p.getFullnames());
            Cases.add(case1);
           // Toast.makeText(getApplicationContext(), p.fullnames, Toast.LENGTH_LONG).show();
        }

    }
}