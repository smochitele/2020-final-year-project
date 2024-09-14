package com.example.smartsecuredhome;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.example.smartsecuredhome.util.Address;
import com.example.smartsecuredhome.util.Respondent;
import com.example.smartsecuredhome.util.StringDateCase;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.io.IOException;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;


public class Sign extends AppCompatActivity {
    public static Respondent loggedUser;
    public static List<Address> Homes = new ArrayList<>();
    public static List<StringDateCase> cases = new ArrayList();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign);
        EditText txtUsername = findViewById(R.id.txt_username);
        EditText txtPassword = findViewById(R.id.txt_password);
        Button btnSignIn = findViewById(R.id.btn_sign_in);
        ProgressBar loader = findViewById(R.id.loader);
        loader.setVisibility(View.INVISIBLE);
        OkHttpClient client = new OkHttpClient();
        btnSignIn.setOnClickListener(e -> {
            if(txtPassword.getText().toString().isEmpty() || txtUsername.getText().toString().isEmpty())
            {
                Toast.makeText(getApplicationContext(), "Username and password required", Toast.LENGTH_LONG ).show();
                return;
            }
            else {
                btnSignIn.setVisibility(View.INVISIBLE);
                loader.setVisibility(View.VISIBLE);
                String url = "https://livessh.conveyor.cloud/Service1.svc/web/user/?username=" + txtUsername.getText().toString() + "&password=" + txtPassword.getText().toString();
                Request request = new Request.Builder()
                        .url(url)
                        .build();
                client.newCall(request).enqueue(new Callback() {
                    @Override
                    public void onFailure(Call call, IOException e) {

                        //Toast.makeText(getApplicationContext(), "Invalid username or password", Toast.LENGTH_LONG ).show();
                    }

                    @Override
                    public void onResponse(Call call, Response response) throws IOException {
                        if(response.isSuccessful()) {
                            Gson gson = new Gson();
                            loggedUser  = gson.fromJson(response.body().string(), Respondent.class);
                            Sign.this.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    try {
                                        if(loggedUser != null) {
                                            OkHttpClient client = new OkHttpClient();
                                            String url = "https://livessh.conveyor.cloud/Service1.svc/web/GetAllAdresses";
                                            Request request = new Request.Builder()
                                                    .url(url)
                                                    .build();
                                            client.newCall(request).enqueue(new okhttp3.Callback() {
                                                @Override
                                                public void onFailure(okhttp3.Call call, IOException e) {
                                                    Toast.makeText(getApplicationContext(), "Invalid username or password", Toast.LENGTH_LONG ).show();

                                                }

                                                @Override
                                                public void onResponse(okhttp3.Call call, okhttp3.Response response) throws IOException {
                                                    if(response.isSuccessful()) {
                                                        Gson gson = new Gson();
                                                        Type sizesType = new TypeToken<List<Address>>() {
                                                        }.getType();
                                                        Homes = gson.fromJson(response.body().string(), sizesType);

                                                    }
                                                }
                                            });
                                            startActivity(new Intent(getApplicationContext(), MainActivity.class));
                                            btnSignIn.setVisibility(View.VISIBLE);
                                            loader.setVisibility(View.INVISIBLE);
                                            getRespondentCases(loggedUser.getId());
                                            dutyStatus(loggedUser.getId(),1);
                                        }
                                        else {
                                            btnSignIn.setVisibility(View.VISIBLE);
                                            loader.setVisibility(View.INVISIBLE);
                                            txtPassword.setText("");
                                            Toast.makeText(getApplicationContext(), "Invalid username or password", Toast.LENGTH_LONG ).show();
                                        }
                                    } catch (Exception ex) {
                                        ex.printStackTrace();
                                    }
                                }
                            });
                        }
                    }
                });
            }
        });

    }
    public static void dutyStatus(String ResId, int status){
        OkHttpClient client = new OkHttpClient();
        String url = "https://livessh.conveyor.cloud/Service1.svc/web/Respondent_DutyStatus/?respondentID="+ResId+"&status="+status;

        Request request = new Request.Builder()
                .url(url)
                .build();
        client.newCall(request).enqueue(new okhttp3.Callback() {
            @Override
            public void onFailure(okhttp3.Call call, IOException e) {
               // Toast.makeText(getApplicationContext(), "Invalid username or password", Toast.LENGTH_LONG ).show();
            }

            @Override
            public void onResponse(okhttp3.Call call, okhttp3.Response response) throws IOException {
                if(response.isSuccessful()) {

                }
            }
        });
    }
    public static void getRespondentCases(String ResId){
        OkHttpClient client = new OkHttpClient();
        String url = "https://livessh.conveyor.cloud/Service1.svc/web/getResCases/?respondentID="+ResId;

        Request request = new Request.Builder()
                .url(url)
                .build();
        client.newCall(request).enqueue(new okhttp3.Callback() {
            @Override
            public void onFailure(okhttp3.Call call, IOException e) {
               // Toast.makeText(getApplicationContext(), "Invalid username or password", Toast.LENGTH_LONG ).show();

            }

            @Override
            public void onResponse(okhttp3.Call call, okhttp3.Response response) throws IOException {
                if(response.isSuccessful()) {
                    Gson gson = new Gson();
                    Type sizesType = new TypeToken<List<StringDateCase>>(){}.getType();
                    cases = gson.fromJson(response.body().string(), sizesType);

                }
            }
        });
    }
}