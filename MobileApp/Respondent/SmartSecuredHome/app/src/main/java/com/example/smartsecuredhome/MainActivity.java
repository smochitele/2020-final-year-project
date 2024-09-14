package com.example.smartsecuredhome;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.NotificationCompat;
import androidx.core.view.GravityCompat;
import androidx.drawerlayout.widget.DrawerLayout;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.Dialog;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.content.Intent;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.smartsecuredhome.ui.Alert;
import com.example.smartsecuredhome.ui.CustomAlert;
import com.example.smartsecuredhome.ui.home.HomeFragment;
import com.example.smartsecuredhome.ui.home.HomeViewModel;
import com.example.smartsecuredhome.ui.slideshow.Case;
import com.example.smartsecuredhome.util.Address;
import com.example.smartsecuredhome.util.GeoLocation;
import com.example.smartsecuredhome.util.Respondent;
import com.example.smartsecuredhome.util.StringDateCase;
import com.google.android.material.navigation.NavigationView;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.mapbox.android.core.permissions.PermissionsListener;
import com.mapbox.android.core.permissions.PermissionsManager;
import com.mapbox.api.directions.v5.models.DirectionsResponse;
import com.mapbox.api.directions.v5.models.DirectionsRoute;
import com.mapbox.geojson.Feature;
import com.mapbox.geojson.Point;
import com.mapbox.mapboxsdk.Mapbox;
import com.mapbox.mapboxsdk.annotations.Icon;
import com.mapbox.mapboxsdk.annotations.IconFactory;
import com.mapbox.mapboxsdk.annotations.MarkerOptions;
import com.mapbox.mapboxsdk.geometry.LatLng;
import com.mapbox.mapboxsdk.location.LocationComponent;
import com.mapbox.mapboxsdk.location.modes.CameraMode;
import com.mapbox.mapboxsdk.maps.MapView;
import com.mapbox.mapboxsdk.maps.MapboxMap;
import com.mapbox.mapboxsdk.maps.OnMapReadyCallback;
import com.mapbox.mapboxsdk.maps.Style;
import com.mapbox.mapboxsdk.maps.UiSettings;
import com.mapbox.mapboxsdk.style.layers.SymbolLayer;
import com.mapbox.mapboxsdk.style.sources.GeoJsonSource;
import com.mapbox.services.android.navigation.ui.v5.NavigationLauncher;
import com.mapbox.services.android.navigation.ui.v5.NavigationLauncherOptions;
import com.mapbox.services.android.navigation.ui.v5.listeners.NavigationListener;
import com.mapbox.services.android.navigation.ui.v5.route.NavigationMapRoute;
import com.mapbox.services.android.navigation.v5.navigation.NavigationRoute;

import org.jetbrains.annotations.NotNull;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.lang.reflect.Type;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;
import java.util.concurrent.TimeUnit;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import timber.log.Timber;

import static com.mapbox.mapboxsdk.Mapbox.getApplicationContext;
import static com.mapbox.mapboxsdk.style.layers.PropertyFactory.iconAllowOverlap;
import static com.mapbox.mapboxsdk.style.layers.PropertyFactory.iconIgnorePlacement;
import static com.mapbox.mapboxsdk.style.layers.PropertyFactory.iconImage;

public class MainActivity extends AppCompatActivity implements OnMapReadyCallback, MapboxMap.OnMapClickListener, PermissionsListener, NavigationListener,NavigationView.OnNavigationItemSelectedListener {
    private MapView mapView;
    private MapboxMap mapboxMap;
    private LocationComponent locationComponent;
    private PermissionsManager permissionsManager;
    private DirectionsRoute currentRoute;
    private NavigationMapRoute navigationRoute;
    Button btnNavigation;
    Dialog myDialog;
   // private HomeViewModel homeViewModel;
    private List<Address> Homes = new ArrayList<>();
    DrawerLayout drawerLayout;
    NavigationView navigationView;
    androidx.appcompat.widget.Toolbar toolbar;
    ActionBarDrawerToggle toogle;
    Respondent respondent;
    Alert alert;
    int alertType;
    CustomAlert cusalert;
    String alertdate;
    String alerttime;
    String responceTime;
    DateFormat dfDate = new SimpleDateFormat("yyyy/MM/dd");
    DateFormat dfTime = new SimpleDateFormat("HH:mm:ss");
    boolean hasShown=false;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
         respondent = Sign.loggedUser;
         Homes = Sign.Homes;
        super.onCreate(savedInstanceState);
        Mapbox.getInstance(this, getString(R.string.access_token));
        myDialog = new Dialog(this);
        setContentView(R.layout.activity_main2);
        mapView =findViewById(R.id.mapView);
        mapView.onCreate(savedInstanceState);
        mapView.getMapAsync(this);
         btnNavigation = findViewById(R.id.btnNavigation);
        Button addCase = findViewById(R.id.addCase);
       // getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
        navigationView=findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);
        navigationView.bringToFront();
        toolbar=findViewById(R.id.toolbarb);
        drawerLayout=findViewById(R.id.drawerlayout);
        setSupportActionBar(toolbar);
        toogle= new ActionBarDrawerToggle(this,drawerLayout,toolbar,R.string.nav_app_bar_open_drawer_description,R.string.nav_app_bar_navigate_up_description);
        drawerLayout.addDrawerListener(toogle);
        toogle.setDrawerIndicatorEnabled(true);
        toogle.syncState();
        toolbar.setNavigationIcon(R.drawable.ic_baseline_menu_24);
        View header =navigationView.getHeaderView(0);
        TextView txtUser = (TextView) header.findViewById(R.id.header_name);
        btnNavigation.bringToFront();
        btnNavigation.setVisibility(View.VISIBLE);
        addCase.setVisibility(View.INVISIBLE);
        addCase.bringToFront();

        if(respondent != null && txtUser!=null ) {
            txtUser.setText(respondent.getName() +" "+ respondent.getSurname());
        }
        String url = "https://livessh.conveyor.cloud/Service1.svc/web/checkalert/?respondentID=" + respondent.getId();
        OkHttpClient client = new OkHttpClient();
        final long period = 10000;
        new Timer().schedule(new TimerTask() {
            @Override
            public void run() {
                if( MainActivity.this!=null) {
                    MainActivity.this.runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Request request = new Request.Builder()
                                    .url(url)
                                    .build();
                            client.newCall(request).enqueue(new okhttp3.Callback() {
                                @Override
                                public void onFailure(okhttp3.Call call, IOException e) {
                                    Timber.d("Cannot access method");
                                }

                                @Override
                                public void onResponse(okhttp3.Call call, okhttp3.Response response) throws IOException {
                                    if (response.isSuccessful()) {
                                        Gson gson = new Gson();
                                        String answer= gson.fromJson(response.body().string(),String.class);

                                                alertdate=dfDate.format(Calendar.getInstance().getTime());
                                                alerttime = dfTime.format(Calendar.getInstance().getTime());

                                                if (answer != null) {
                                                   // Toast.makeText(getApplicationContext()," "+answer, Toast.LENGTH_LONG).show();
                                                    String[] splited = answer.split("\\s");
                                                    cusalert=new CustomAlert();
                                                    cusalert.setHouseID(Integer.parseInt(splited[0]));
                                                    cusalert.setOccupantID(splited[1]);
                                                    cusalert.setLongitude(splited[2]);
                                                    cusalert.setLattitude(splited[3]);
                                                    MainActivity.this.runOnUiThread(new Runnable() {
                                                        @Override
                                                        public void run() {
                                                           // Toast.makeText(getApplicationContext()," "+answer, Toast.LENGTH_LONG).show();

                                                            double lattitude = Double.parseDouble(cusalert.getLattitude());
                                                            double longitude = Double.parseDouble(cusalert.getLongitude());
                                                            LatLng destination = new LatLng(lattitude, longitude);
                                                            onMapClick(destination);
                                                          while(  btnNavigation.getVisibility() == View.INVISIBLE ) {
                                                                  btnNavigation.setVisibility(View.VISIBLE);
                                                                  btnNavigation.bringToFront();

                                                          }
                                                            Sign.dutyStatus(respondent.getId(),2);

                                                                displayNotification();
                                                                hasShown=true;

                                                        }
                                                    });
                                                }



                                    }
                                }
                            });
                        }
                    });
                }
            }
        }, 0, period);

        btnNavigation.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startNavigation();
                btnNavigation.setVisibility(View.INVISIBLE);
                addCase.setVisibility(View.VISIBLE);
            }
        });

        addCase.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                addCase.setVisibility(View.INVISIBLE);
                myDialog.setContentView(R.layout.activity_add_case);
                myDialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));

                Button button=myDialog.findViewById(R.id.AddCaseBtn) ;
                button.bringToFront();
                EditText des=myDialog.findViewById(R.id.description) ;
                Spinner dropdown=myDialog.findViewById(R.id.spinner) ;
                ArrayAdapter adapter = ArrayAdapter.createFromResource(
                        myDialog.getContext(),
                        R.array.break_in_method,
                        R.layout.spinner_layout
                );
                adapter.setDropDownViewResource(R.layout.spinner_drop_down);
                dropdown.setAdapter(adapter);
                dropdown.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                    @Override
                    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                        String selected=adapterView.getSelectedItem().toString();
                        if(i==0) {
                         //   Toast.makeText(myDialog.getContext(), selected, Toast.LENGTH_LONG).show();
                            alertType = 3;
                        }else if(i==1) {
                        //    Toast.makeText(myDialog.getContext(), selected, Toast.LENGTH_LONG).show();
                            alertType = 2;
                        }else if(i==2) {
                            //  Toast.makeText(myDialog.getContext(), selected, Toast.LENGTH_LONG).show();
                            alertType = 1;
                        }
                    }

                    @Override
                    public void onNothingSelected(AdapterView<?> adapterView) {

                    }

                });
                dropdown.setPrompt("Select Break-in Method");
                WindowManager.LayoutParams lp = myDialog.getWindow().getAttributes();
                lp.dimAmount = 0.0f;
                myDialog.show();
                button.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        responceTime = dfTime.format(Calendar.getInstance().getTime());
                        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("HH:mm");
                        Date date1 = null;
                        Date date2=null;
                        try {
                            date1 = simpleDateFormat.parse(alerttime);
                            date2 = simpleDateFormat.parse(responceTime);
                           long difference = date2.getTime() - date1.getTime();
                           int hours = Long.valueOf(TimeUnit.MILLISECONDS.toHours(difference)).intValue();
                           int min = Long.valueOf(TimeUnit.MILLISECONDS.toMinutes(difference)).intValue();
                           int sec = Long.valueOf(TimeUnit.MILLISECONDS.toSeconds(difference)).intValue();
                            String respTime = hours+":"+min+":"+sec;
                            EditText des=myDialog.findViewById(R.id.description);
                            String descrip=des.getText().toString();
                           // Toast.makeText(getApplicationContext(), respTime, Toast.LENGTH_LONG ).show();
                            //data=userID  Houseid respondenid type responcetime
                            String casedata= cusalert.getOccupantID()+" "+cusalert.getHouseID()+" "+respondent.getId()+" "+alertType+" "+respTime;
                            String url = "https://livessh.conveyor.cloud/Service1.svc/web/AddCaseObject/?data="+casedata+"&description="+descrip+"&Strdatetime="+alertdate+" "+alerttime;
                            OkHttpClient client = new OkHttpClient();
                            Request request = new Request.Builder()
                                    .url(url)
                                    .build();
                            client.newCall(request).enqueue(new okhttp3.Callback() {
                                @Override
                                public void onFailure(okhttp3.Call call, IOException e) {
                                    Timber.d("Cannot access method");
                                   // Toast.makeText(getApplicationContext(), "Here", Toast.LENGTH_LONG ).show();
                                }

                                @Override
                                public void onResponse(okhttp3.Call call, okhttp3.Response response) throws IOException {
                                    if (response.isSuccessful()) {
                                       Sign.getRespondentCases(respondent.getId());
                                       hasShown=false;
                                        myDialog.dismiss();
                                        Sign.dutyStatus(respondent.getId(),2);
                                    }
                                }
                            });

                        } catch (ParseException e) {
                            e.printStackTrace();
                        }

                    }
                });
            }
        });
    }
    @Override
    public void onExplanationNeeded(List<String> permissionsToExplain) {

    }

    @Override
    public void onPermissionResult(boolean granted) {
        if(granted) {
            enableLocationComponent(mapboxMap.getStyle());
        }
        else {
            //Toast.makeText(getApplicationContext(), "Location permission not grantred", Toast.LENGTH_LONG).show();
            //finish();
        }
    }

    @Override
    public boolean onMapClick(@NonNull LatLng point) {
        Point destination = Point.fromLngLat(point.getLongitude(), point.getLatitude());
        Point origin = Point.fromLngLat(locationComponent.getLastKnownLocation().getLongitude(), locationComponent.getLastKnownLocation().getLatitude());
        GeoJsonSource source = mapboxMap.getStyle().getSourceAs("destination-source-id");
        if(source != null) {
            source.setGeoJson(Feature.fromGeometry(destination));
        }
        getRoute(origin, destination);
        return true;
    }

    private void getRoute(Point origin, Point destination) {
        NavigationRoute.builder(this)
                .accessToken(Mapbox.getAccessToken())
                .origin(origin)
                .destination(destination)
                .build()
                .getRoute(new Callback<DirectionsResponse>() {
                    @Override
                    public void onResponse(Call<DirectionsResponse> call, Response<DirectionsResponse> response) {
                        if(response.body() != null && response.body().routes().size() > 0) {
                            currentRoute = response.body().routes().get(0);
                            if(navigationRoute != null) {
                                navigationRoute.removeRoute();
                            }
                            else {
                                navigationRoute = new NavigationMapRoute(null, mapView, mapboxMap, R.style.NavigationMapRoute);

                            }
                            navigationRoute.addRoute(currentRoute);
                        }
                    }
                    @Override
                    public void onFailure(Call<DirectionsResponse> call, Throwable t) {

                    }
                });
    }

    @Override
    public void onMapReady(@NonNull MapboxMap mapboxMap) {
        this.mapboxMap = mapboxMap;
        this.mapboxMap.setMinZoomPreference(14);
        AddHouseMakers();
        mapboxMap.setStyle(getString(R.string.nav_style), new Style.OnStyleLoaded() {
            @Override
            public void onStyleLoaded(@NonNull Style style) {
                enableLocationComponent(style);
                addDestinationIconLayer(style);
                mapboxMap.addOnMapClickListener(MainActivity.this::onMapClick);
            }
        });
        new java.util.Timer().schedule(

                new java.util.TimerTask() {
                    @Override
                    public void run() {
                        updateUserLocation(Sign.loggedUser.getId(), String.valueOf(locationComponent.getLastKnownLocation().getLongitude()), String.valueOf(locationComponent.getLastKnownLocation().getLatitude()));
                    }
                },
                2000
        );
        UiSettings uiSettings = mapboxMap.getUiSettings();
        uiSettings.setZoomGesturesEnabled(true);
        uiSettings.setZoomRate(50);
        uiSettings.setQuickZoomGesturesEnabled(true);
        mapboxMap.setMaxZoomPreference(25);


    }
    String display="";
    private void AddHouseMakers(){

        IconFactory icf= IconFactory.getInstance(this);
        Icon icon =icf.fromResource(R.drawable.homemaker);
        for (Address p :Homes
        ) {
            mapboxMap.addMarker(new MarkerOptions()
                    .position(new LatLng(Double.parseDouble(p.getLattitute()), Double.parseDouble(p.getLongitute())))
                    .icon(icon)
                    .title("Smart home"));


        }
    }
    private void addDestinationIconLayer(Style style) {
        style.addImage("destination-icon-id", BitmapFactory.decodeResource(this.getResources(), R.drawable.mapbox_marker_icon_default));
        GeoJsonSource geoJsonSource = new GeoJsonSource("destination-source-id");
        style.addSource(geoJsonSource);
        SymbolLayer destinationLayer = new SymbolLayer("destination-symbol-layer-id", "destination-source-id");
        destinationLayer.withProperties(iconImage("destination-icon-id"), iconAllowOverlap(true), iconIgnorePlacement(true));
        style.addLayer(destinationLayer);
    }

    public void startNavigation() {
        NavigationLauncherOptions navigationLauncher = NavigationLauncherOptions.builder()
                .directionsRoute(currentRoute)
                .shouldSimulateRoute(true)
                .build();
        NavigationLauncher.startNavigation(this, navigationLauncher);



    }
    /*@Override
    public void onPointerCaptureChanged(boolean hasCapture) {

    }*/

    @SuppressLint("MissingPermission")
    private void enableLocationComponent(@NotNull Style style) {
        if(PermissionsManager.areLocationPermissionsGranted(this)) {
            locationComponent = mapboxMap.getLocationComponent();
            locationComponent.activateLocationComponent(this, style);
            locationComponent.setLocationComponentEnabled(true);
            locationComponent.setCameraMode(CameraMode.TRACKING);
        }
        else {
            permissionsManager = new PermissionsManager(this);
            permissionsManager.requestLocationPermissions(this);
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        permissionsManager.onRequestPermissionsResult(requestCode, permissions, grantResults);
    }

    @Override
    public void onStart() {
        super.onStart();
        mapView.onStart();


    }

    @Override
    public void onResume(){
        super.onResume();
        mapView.onResume();
    }

    @Override
    public void onPause() {
        super.onPause();
        mapView.onPause();
    }

    @Override
    public void onStop() {
        super.onStop();
        mapView.onStop();
    }

    @Override
    public void onSaveInstanceState(@NonNull Bundle outState) {
        super.onSaveInstanceState(outState);
        mapView.onSaveInstanceState(outState);
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        mapView.onDestroy();
    }

    @Override
    public void onLowMemory() {
        super.onLowMemory();
        mapView.onLowMemory();
    }

    private void updateUserLocation(String id, String longitude, String latitude) {
        String url = "https://livessh.conveyor.cloud/Service1.svc/web/location/?respondentID=" + Sign.loggedUser.getId() + "&longitute=" + longitude + "&lattitue=" + latitude;
        OkHttpClient client = new OkHttpClient();
        Request request = new Request.Builder()
                .url(url)
                .build();
        client.newCall(request).enqueue(new okhttp3.Callback() {
            @Override
            public void onFailure(okhttp3.Call call, IOException e) {
                MainActivity.this.runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        Log.d("ERR", "Cannot update location");
                    }
                });
            }

            @Override
            public void onResponse(okhttp3.Call call, okhttp3.Response response) throws IOException {
                if(response.isSuccessful()) {

                }
            }
        });
    }

    private void displayNotification() {
        NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(this, "CHANNEL_ID")
                .setSmallIcon(R.drawable.ic_baseline_android_24)
                .setContentTitle("House breaking alert")
                .setContentText("There's a house breaking to report to")
                .setColor(Color.parseColor("#009add"))
                .setAutoCancel(true);

        NotificationManager notificationManager = (NotificationManager) this.getSystemService(
                NOTIFICATION_SERVICE);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            NotificationChannel mChannel = new NotificationChannel("CHANNEL_ID", "CHANNEL_NAME", NotificationManager.IMPORTANCE_HIGH);

            notificationManager.createNotificationChannel(mChannel);
        }

        notificationManager.notify(0, mBuilder.build());
        btnNavigation.setVisibility(View.VISIBLE);
    }
    
    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem item) {
        switch (item.getItemId()) {
            case R.id.Home: {
                Intent intent = new Intent(MainActivity.this, MainActivity.class);
                MainActivity.this.startActivity(intent);
                break;
            }

            case R.id.profile: {
                Intent intent = new Intent(MainActivity.this, Profile.class);
                MainActivity.this.startActivity(intent);
                break;
            }

            case R.id.cases: {
                Intent intent = new Intent(MainActivity.this, caseHistory.class);
                MainActivity.this.startActivity(intent);
                break;
            }
            case R.id.sign_out: {
                Sign.dutyStatus(respondent.getId(),0);
                Intent intent = new Intent(MainActivity.this, Sign.class);
                MainActivity.this.startActivity(intent);
                break;
            }
        }
        item.setChecked(true);
        drawerLayout.closeDrawer(GravityCompat.START);
        return true;
    }

    @Override
    public void onCancelNavigation() {

    }

    @Override
    public void onNavigationFinished() {
        myDialog.setContentView(R.layout.activity_add_case);
        myDialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        Button button=myDialog.findViewById(R.id.AddCaseBtn) ;
        EditText des=myDialog.findViewById(R.id.description) ;
        WindowManager.LayoutParams lp = myDialog.getWindow().getAttributes();
        lp.dimAmount = 0.0f;
        myDialog.show();
    }

    @Override
    public void onNavigationRunning() {

    }
}