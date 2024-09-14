package com.example.smartsecuredhome.ui.home;

import androidx.annotation.MainThread;
import androidx.annotation.NonNull;
import androidx.core.app.NotificationCompat;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProviders;
import android.annotation.SuppressLint;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.content.Context;
import android.content.Intent;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.Toast;
import com.example.smartsecuredhome.R;
import com.example.smartsecuredhome.RespondentHome;
import com.example.smartsecuredhome.Sign;
import com.example.smartsecuredhome.util.Address;
import com.example.smartsecuredhome.util.GeoLocation;
import com.example.smartsecuredhome.util.Respondent;
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
import com.mapbox.services.android.navigation.ui.v5.route.NavigationMapRoute;
import com.mapbox.services.android.navigation.v5.navigation.NavigationRoute;
import org.jetbrains.annotations.NotNull;
import java.io.IOException;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import timber.log.Timber;
import okhttp3.OkHttpClient;
import okhttp3.Request;


import static android.content.Context.NOTIFICATION_SERVICE;
import static com.mapbox.mapboxsdk.Mapbox.getApplicationContext;
import static com.mapbox.mapboxsdk.style.layers.PropertyFactory.iconAllowOverlap;
import static com.mapbox.mapboxsdk.style.layers.PropertyFactory.iconIgnorePlacement;
import static com.mapbox.mapboxsdk.style.layers.PropertyFactory.iconImage;


public class HomeFragment extends Fragment implements OnMapReadyCallback, MapboxMap.OnMapClickListener, PermissionsListener {
    private MapView mapView;
    private MapboxMap mapboxMap;
    private LocationComponent locationComponent;
    private  PermissionsManager permissionsManager;
    private DirectionsRoute currentRoute;
    private NavigationMapRoute navigationRoute;
    private HomeViewModel homeViewModel;
    private List<Address> Homes = new ArrayList<>();

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {

        homeViewModel = ViewModelProviders.of(this).get(HomeViewModel.class);
        Mapbox.getInstance(this.getContext(), getString(R.string.access_token));
        View root = inflater.inflate(R.layout.fragment_home, container, false);
        super.onCreate(savedInstanceState);
        mapView = root.findViewById(R.id.mapView);
        mapView.onCreate(savedInstanceState);
        mapView.getMapAsync(this);
        Button btnNavigation = root.findViewById(R.id.btnNavigation);
        btnNavigation.setOnClickListener(e -> {
            startNavigation();
            btnNavigation.setVisibility(View.INVISIBLE);
        });
        btnNavigation.setVisibility(View.INVISIBLE);
        String url = "https://livessh.conveyor.cloud/Service1.svc/web/alert/?respondentID=" + Sign.loggedUser.getId();
        OkHttpClient client = new OkHttpClient();
        final long period = 10000;
        new Timer().schedule(new TimerTask() {
            @Override
            public void run() {
                if( getActivity()!=null) {
                    getActivity().runOnUiThread(new Runnable() {
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
                                        GeoLocation geoLocation = gson.fromJson(response.body().string(), GeoLocation.class);
                                        if (geoLocation != null) {
                                            getActivity().runOnUiThread(new Runnable() {
                                                @Override
                                                public void run() {
                                                    double lattitude = Double.parseDouble(geoLocation.getLattitude());
                                                    double longitude = Double.parseDouble(geoLocation.getLongitude());
                                                    LatLng destination = new LatLng(lattitude, longitude);
                                                    onMapClick(destination);
                                                    btnNavigation.setVisibility(View.VISIBLE);
                                                    displayNotification();
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

        return root;


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
        NavigationRoute.builder(this.getContext())
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
        mapboxMap.setStyle(getString(R.string.nav_style), new Style.OnStyleLoaded() {
            @Override
            public void onStyleLoaded(@NonNull Style style) {
                enableLocationComponent(style);
                addDestinationIconLayer(style);
                mapboxMap.addOnMapClickListener(HomeFragment.this);
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
        AddHouseMakers();

    }
    String display="";
    private void AddHouseMakers(){
        display+="  start";
        OkHttpClient client = new OkHttpClient();
        String url = "https://livessh-vt3.conveyor.cloud/Service1.svc/web/GetAllAdresses";

        Request request = new Request.Builder()
                .url(url)
                .build();
        client.newCall(request).enqueue(new okhttp3.Callback() {
            @Override
            public void onFailure(okhttp3.Call call, IOException e) {
                Toast.makeText(getApplicationContext(), "Invalid username or password", Toast.LENGTH_LONG ).show();
                display+="  fail";
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
        IconFactory icf= IconFactory.getInstance(this.getContext());
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
        NavigationLauncher.startNavigation(getActivity(), navigationLauncher);
    }
    /*@Override
    public void onPointerCaptureChanged(boolean hasCapture) {

    }*/

    @SuppressLint("MissingPermission")
    private void enableLocationComponent(@NotNull Style style) {
        if(PermissionsManager.areLocationPermissionsGranted(this.getContext())) {
            locationComponent = mapboxMap.getLocationComponent();
            locationComponent.activateLocationComponent(this.getContext(), style);
            locationComponent.setLocationComponentEnabled(true);
            locationComponent.setCameraMode(CameraMode.TRACKING);
        }
        else {
            permissionsManager = new PermissionsManager(this);
            permissionsManager.requestLocationPermissions(this.getActivity());
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
                getActivity().runOnUiThread(new Runnable() {
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
        NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(getContext(), "CHANNEL_ID")
                .setSmallIcon(R.drawable.ic_baseline_android_24)
                .setContentTitle("House breaking alert")
                .setContentText("There's a house breaking to report to")
                .setColor(Color.parseColor("#009add"))
                .setAutoCancel(true);

        NotificationManager notificationManager = (NotificationManager) getContext().getSystemService(
                NOTIFICATION_SERVICE);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            NotificationChannel mChannel = new NotificationChannel("CHANNEL_ID", "CHANNEL_NAME", NotificationManager.IMPORTANCE_HIGH);

            notificationManager.createNotificationChannel(mChannel);
        }

        notificationManager.notify(0, mBuilder.build());
    }
}