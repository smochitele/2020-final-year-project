package com.example.myhome;

import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.view.View;
import android.view.Menu;
import android.widget.TextView;

import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.google.android.material.snackbar.Snackbar;
import com.google.android.material.navigation.NavigationView;

import androidx.core.app.NotificationCompat;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

public class ClientHome extends AppCompatActivity {

    private AppBarConfiguration mAppBarConfiguration;
    private static final String SMS_RECEIVED = "android.provider.Telephony.SMS_RECEIVED";
    private User user;
    private ReceiveAlert receiver = new ReceiveAlert(){
        @Override
        public void onReceive(Context context, Intent intent) {
            super.onReceive(context, intent);
            String messageBody = super.getAlertBody();
            if(messageBody.equals("ALERT")) {
                displayNotification();
                startActivity(new Intent(getApplicationContext(), AlertScreen.class));
            }
        }
    };
    @Override
    protected void onDestroy() {
        super.onDestroy();
        unregisterReceiver(receiver);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        user = SignIn.occupant;
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_client_home);
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        NavigationView navigationView = findViewById(R.id.nav_view);
        mAppBarConfiguration = new AppBarConfiguration.Builder(
                R.id.nav_home, R.id.nav_gallery, R.id.nav_slideshow)
                .setDrawerLayout(drawer)
                .build();
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupActionBarWithNavController(this, navController, mAppBarConfiguration);
        NavigationUI.setupWithNavController(navigationView, navController);
        NavigationView navView = (NavigationView)findViewById(R.id.nav_view);
        View headerView = navView.getHeaderView(0);
        TextView txtUser = (TextView)headerView.findViewById(R.id.txt_loggedUser);
        if(user != null) {
            txtUser.setText(user.getName() + " " + user.getSurname());
        }
        registerReceiver(receiver, new IntentFilter(SMS_RECEIVED));
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.client_home, menu);
        return true;
    }

    @Override
    public boolean onSupportNavigateUp() {
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        return NavigationUI.navigateUp(navController, mAppBarConfiguration)
                || super.onSupportNavigateUp();
    }
    private void displayNotification() {
        NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(ClientHome.this, "CHANNEL_ID")
                .setSmallIcon(R.drawable.ic_baseline_android_24)
                .setContentTitle("House breaking alert")
                .setContentText("Motion sensors have been triggered in your home. Possible house breaking")
                .setColor(Color.parseColor("#009add"))
                .setAutoCancel(true);

        NotificationManager notificationManager = (NotificationManager)getSystemService(
                NOTIFICATION_SERVICE);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            NotificationChannel mChannel = new NotificationChannel("CHANNEL_ID", "CHANNEL_NAME", NotificationManager.IMPORTANCE_HIGH);

            notificationManager.createNotificationChannel(mChannel);
        }

        notificationManager.notify(0, mBuilder.build());
    }
}