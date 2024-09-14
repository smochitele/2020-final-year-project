package com.example.myhome.ui.slideshow;

import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout;

import com.example.myhome.Adapter;
import com.example.myhome.Case;
import com.example.myhome.CaseAdapter;
import com.example.myhome.Model;
import com.example.myhome.R;
import com.example.myhome.SignIn;
import com.example.myhome.User;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.io.IOException;
import java.lang.reflect.Type;
import java.util.ArrayList;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

public class SlideshowFragment extends Fragment {

    private SlideshowViewModel slideshowViewModel;
    private ArrayList<Model> list = new ArrayList<>();
    private ArrayList<Case> cases = new ArrayList<>();
    private CaseAdapter adapter;
    private RecyclerView recyclerView;
    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {

        slideshowViewModel =
                ViewModelProviders.of(this).get(SlideshowViewModel.class);
        View root = inflater.inflate(R.layout.fragment_slideshow, container, false);
        recyclerView = root.findViewById(R.id.casesRecyclerView);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
        SwipeRefreshLayout swipeRefreshLayout = root.findViewById(R.id.refreshCases);
        swipeRefreshLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                swipeRefreshLayout.setRefreshing(true);
                refreshCases(swipeRefreshLayout, adapter, recyclerView);
            }
        });
        adapter = new CaseAdapter(getContext(), getArrayList());
        recyclerView.setAdapter(adapter);
        refreshCases(swipeRefreshLayout, adapter, recyclerView);
        return root;
    }

    private ArrayList<Model> getArrayList() {
        for(Case c : cases) {
            Model model = new Model();
            model.setOccupantName("Case ID: " + c.getCaseID());
            model.setOccupantEmail("Date: " + c.getStringDate());
            list.add(model);
        }
        return list;
    }

    private void refreshCases(SwipeRefreshLayout refreshLayout, CaseAdapter adapter, RecyclerView recyclerView) {
        String url = "https://livessh.conveyor.cloud/Service1.svc/web/useralerts/?userID=" + SignIn.occupant.getID();
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
                    Type classType = new TypeToken<ArrayList<Case>>() {}.getType();
                    cases  = gson.fromJson(response.body().string(), classType);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            refreshAdapter();
                            recyclerView.setAdapter(adapter);
                            refreshLayout.setRefreshing(false);
                        }
                    });
                }
            }
        });
    }

    private void refreshAdapter() {
        adapter = new CaseAdapter(getContext(), getArrayList());
    }
}