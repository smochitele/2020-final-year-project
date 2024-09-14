package com.example.myhome.ui.home;

import android.Manifest;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.telephony.SmsManager;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.core.app.NotificationCompat;
import androidx.core.content.ContextCompat;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;

import com.example.myhome.Address;
import com.example.myhome.AlertScreen;
import com.example.myhome.ClientHome;
import com.example.myhome.House;
import com.example.myhome.R;
import com.example.myhome.SignIn;
import com.example.myhome.User;
import com.google.gson.Gson;

import java.io.IOException;
import java.util.Timer;
import java.util.TimerTask;
import java.util.zip.Inflater;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

import static android.content.Context.NOTIFICATION_SERVICE;

public class HomeFragment extends Fragment {

    private HomeViewModel homeViewModel;
    private final String ALARM_ON = "2";
    private final String ALARM_OFF = "1";
    private final String ALARM_TRIG = "-2";

    private final int OFF = 1;
    private final int ON = 2;

    private Button btnAlarmOff = null;
    private Button btnAlarmOn = null;
    private Button btnPanicAlert = null;
    private ProgressBar loader = null;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        homeViewModel =
                ViewModelProviders.of(this).get(HomeViewModel.class);
        View root = inflater.inflate(R.layout.fragment_home, container, false);
        User user = SignIn.occupant;
        btnAlarmOff = null;// root.findViewById(R.id.btnAlarmOff);
        btnAlarmOn = null;// root.findViewById(R.id.btnAlarmOn);
        btnPanicAlert = root.findViewById(R.id.btnPanic);
        //loader = root.findViewById(R.id.controlLoader);
        //loader.setVisibility(View.INVISIBLE);
        //TextView province = root.findViewById(R.id.suburb);
        //ProgressBar loader = root.findViewById(R.id.addressLoader);
        //TextView surburb = root.findViewById(R.id.province);
        //TextView city = root.findViewById(R.id.city);
        //province.setVisibility(View.INVISIBLE);
        //surburb.setVisibility(View.INVISIBLE);
        //city.setVisibility(View.INVISIBLE);
        Address address = getAddress(SignIn.occupant.getID());
        /*new java.util.Timer().schedule(
                new java.util.TimerTask() {
                    @Override
                    public void run() {
                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    loader.setVisibility(View.INVISIBLE);
                                    province.setVisibility(View.VISIBLE);
                                    surburb.setVisibility(View.VISIBLE);
                                    city.setVisibility(View.VISIBLE);
                                    province.setText(SignIn.occupant.getName());
                                    surburb.setText(SignIn.occupant.getSurname());
                                    city.setText(SignIn.occupant.getEmail());
                                }
                            });
                    }
                },
                5000
        );*/


        /*btnAlarmOff.setOnClickListener(e -> {
            switchOffAlarm();
        });


        btnAlarmOn.setOnClickListener(e -> {
            switchOnAlarm();
        });

        btnPanicAlert.setOnClickListener(e -> {
            panicAlert();
        });*/

        new Timer().schedule(new TimerTask() {
            @Override
            public void run() {
                String url = "https://livessh.conveyor.cloud/Service1.svc/web/housealerts/?userID=" + SignIn.occupant.getID();
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
                        String resp = response.body().string();
                        if (response.isSuccessful()) {
                            try {
                                if(resp.contains("-2")) {
                                    getActivity().runOnUiThread(new Runnable() {
                                        @Override
                                        public void run() {
                                            displayNotification();
                                            startActivity(new Intent(getContext(), AlertScreen.class));
                                        }
                                    });
                                }
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    }
                });
            }

        }, 0, 5000);
        checkPermissions();
        return root;
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
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                Toast.makeText(getContext(),"Finding the nearest respondent for you", Toast.LENGTH_LONG).show();
                            }
                        });
                    }
                }
            });
        }).start();
        showDisplayLoader(btnAlarmOn, btnAlarmOff, btnPanicAlert, loader);
    }

    private void switchOnAlarm () {
        new Thread(() -> {
            String url = "https://livessh.conveyor.cloud/Service1.svc/web/trigger/?userID=" + SignIn.occupant.getID() + "&triggerType=" + ON;
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
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                Toast.makeText(getContext(),"Switching alarm on", Toast.LENGTH_LONG).show();
                            }
                        });
                    }
                    else {
                        Log.d("ERR:", "REQ FAIL");
                    }
                }
            });
        }).start();
        showDisplayLoader(btnAlarmOn, btnAlarmOff, btnPanicAlert, loader);
    }

    private void switchOffAlarm() {
        new Thread(() -> {
            String url = "https://livessh.conveyor.cloud/Service1.svc/web/trigger/?userID=" + SignIn.occupant.getID() + "&triggerType=" + OFF;
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
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                Toast.makeText(getContext(),"Switching alarm off", Toast.LENGTH_LONG).show();
                            }
                        });
                    }
                }
            });
        }).start();
        showDisplayLoader(btnAlarmOn, btnAlarmOff, btnPanicAlert, loader);
    }

    private void checkPermissions() {
        if(Build.VERSION.SDK_INT >= Build.VERSION_CODES.M && ContextCompat.checkSelfPermission(getContext(), Manifest.permission.RECEIVE_SMS) != PackageManager.PERMISSION_GRANTED) {
            requestPermissions(new String[] { Manifest.permission.RECEIVE_SMS }, 1000);
        }
        if(ContextCompat.checkSelfPermission(getContext(), Manifest.permission.SEND_SMS) != PackageManager.PERMISSION_GRANTED) {
            requestPermissions(new String[] { Manifest.permission.SEND_SMS }, 1001);
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        //checkPermissions();
        if(requestCode == 1000) {
            if(!(grantResults[0] == PackageManager.PERMISSION_GRANTED)) {
                Log.d("ERROR", "SMS permission not granted");
            }
        }
    }

    private void showDisplayLoader(Button btnOn, Button btnOff, Button btnPanic, ProgressBar loader) {
        btnOff.setVisibility(View.INVISIBLE);
        btnOn.setVisibility(View.INVISIBLE);
        btnPanic.setVisibility(View.INVISIBLE);
        loader.setVisibility(View.VISIBLE);
        new java.util.Timer().schedule(
                new java.util.TimerTask() {
                    @Override
                    public void run() {
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                loader.setVisibility(View.INVISIBLE);
                                btnOff.setVisibility(View.VISIBLE);
                                btnOn.setVisibility(View.VISIBLE);
                                btnPanic.setVisibility(View.VISIBLE);
                            }
                        });
                    }
                },
                2500
        );
    }

    private Address getAddress(String userID) {
        final Address[] addresses = {null};
        String url = "https://livessh.conveyor.cloud/Service1.svc/web/getuseraddress/?userID=" + userID;
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
                    Gson gson = new Gson();
                    addresses[0] = gson.fromJson(response.body().string(), Address.class);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            if(addresses[0] != null) {
                                try {


                                }
                                catch (Exception e) {
                                    e.printStackTrace();
                                }
                            }
                        }
                    });
                }
            }
        });
        return addresses[0];
    }
    private void displayNotification() {
        NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(getContext(), "CHANNEL_ID")
                .setSmallIcon(R.drawable.ic_baseline_android_24)
                .setContentTitle("House breaking alert")
                .setContentText("Motion sensors have been triggered in your home. Possible house breaking")
                .setColor(Color.parseColor("#009add"))
                .setAutoCancel(true);

        NotificationManager notificationManager = (NotificationManager)getActivity().getSystemService(
                NOTIFICATION_SERVICE);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            NotificationChannel mChannel = new NotificationChannel("CHANNEL_ID", "CHANNEL_NAME", NotificationManager.IMPORTANCE_HIGH);

            notificationManager.createNotificationChannel(mChannel);
        }

        notificationManager.notify(0, mBuilder.build());
    }
}