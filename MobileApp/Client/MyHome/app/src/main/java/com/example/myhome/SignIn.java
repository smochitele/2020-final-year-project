package com.example.myhome;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.google.gson.Gson;

import java.io.IOException;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

public class SignIn extends AppCompatActivity {
    public static User occupant;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_in);
        ProgressBar loader = findViewById(R.id.loader);
        EditText txtUsername = findViewById(R.id.txt_username);
        EditText txtPassword = findViewById(R.id.txt_password);
        Button btnLogin = findViewById(R.id.btn_sign_in);
        Button btnRegister = findViewById(R.id.btn_register);
        loader.setVisibility(View.INVISIBLE);
        OkHttpClient client = new OkHttpClient();
        btnLogin.setOnClickListener(e -> {
            if(txtPassword.getText().toString().isEmpty() || txtUsername.getText().toString().isEmpty())
            {
                Toast.makeText(getApplicationContext(), "Username and password required", Toast.LENGTH_LONG ).show();
                return;
            }
            else {
                loader.setVisibility(View.VISIBLE);
                btnLogin.setVisibility(View.INVISIBLE);
                btnRegister.setVisibility(View.INVISIBLE);
                String url = "https://livessh.conveyor.cloud/Service1.svc/web/user/?username=" + txtUsername.getText().toString() + "&password=" + txtPassword.getText().toString();
                Request request = new Request.Builder()
                        .url(url)
                        .build();
                client.newCall(request).enqueue(new Callback() {
                    @Override
                    public void onFailure(Call call, IOException e) {
                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                Toast.makeText(getApplicationContext(), "Invalid username or password", Toast.LENGTH_LONG ).show();
                            }
                        });
                    }

                    @Override
                    public void onResponse(Call call, Response response) throws IOException {
                        if(response.isSuccessful()) {
                            Gson gson = new Gson();
                            occupant  = gson.fromJson(response.body().string(), User.class);
                            SignIn.this.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    try {
                                        if(occupant != null) {
                                            startActivity(new Intent(getApplicationContext(), ClientHome.class));
                                            btnLogin.setVisibility(View.VISIBLE);
                                            btnRegister.setVisibility(View.VISIBLE);
                                            loader.setVisibility(View.INVISIBLE);
                                        }
                                        else {
                                            btnLogin.setVisibility(View.VISIBLE);
                                            btnRegister.setVisibility(View.VISIBLE);
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
}