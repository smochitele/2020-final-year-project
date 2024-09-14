package com.example.myhome;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.Toast;

import java.io.IOException;
import java.util.concurrent.atomic.AtomicBoolean;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

public class AlertScreen extends Activity {
    private static boolean alertTriggered = false;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        AtomicBoolean notConfirmed = new AtomicBoolean(true);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_alert_screen);
        Button btnConfirm = findViewById(R.id.btnConfirm);
        Button btnFalse = findViewById(R.id.btnFalseAlarm);

        btnConfirm.setOnClickListener(e -> {
            notConfirmed.set(false);
            panicAlert();
            runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    Toast.makeText(getApplicationContext(), "A respondent is coming to the rescue", Toast.LENGTH_LONG ).show();
                }
            });
            startActivity(new Intent(getApplicationContext(), ClientHome.class));
        });

        btnFalse.setOnClickListener(e -> {
            notConfirmed.set(false);
            runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    Toast.makeText(getApplicationContext(), "Alarm disabled", Toast.LENGTH_LONG ).show();
                }
            });
            dismissAlarm();

        });
        new java.util.Timer().schedule(
                new java.util.TimerTask() {
                    @Override
                    public void run() {
                        if(notConfirmed.get()) {
                            panicAlert();
                            runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Toast.makeText(getApplicationContext(), "A respondent is coming to the rescue", Toast.LENGTH_LONG ).show();
                                }
                            });
                        }
                    }
                },
                20000
        );
    }

    public static boolean alarmIsTriggered() {
        return alertTriggered;
    }

    public static void triggerAlarm(boolean status) {
        alertTriggered = status;
    }

    private void panicAlert() {
        new Thread(() -> {
            String url = "https://livessh.conveyor.cloud/Service1.svc/web/panic/?userID=" + SignIn.occupant.getID() + "&AlertType=2";
            OkHttpClient client = new OkHttpClient();
            Request request = new Request.Builder()
                    .url(url)
                    .build();
            client.newCall(request).enqueue(new Callback() {
                @Override
                public void onFailure(Call call, IOException e) {

                }

                @Override
                public void onResponse(Call call, Response response) throws IOException {
                    if(response.isSuccessful()) {

                    }
                }
            });

        }).start();
    }

    private void dismissAlarm() {
        startActivity(new Intent(getApplicationContext(), ClientHome.class));
    }
}