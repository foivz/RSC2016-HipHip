package com.hiphiparray.quizify;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.text.Html;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.ExecutorDelivery;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

import static android.R.attr.data;

/**
 * Created by TOSHIBA on 26.11.2016..
 */

public class MyTeams extends AppCompatActivity {

    ArrayList<String> teams;
    ListView listOfTeams;
    String id;
    String name;
    Context context;
    SharedPreferences preferences;
    SharedPreferences.Editor editor;
    String userId;
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.myteams_layout);


        context = getApplicationContext();
        preferences = PreferenceManager.getDefaultSharedPreferences(this);
        userId = preferences.getString("UserId","Default_Value");

        teams = new ArrayList<String>();
        listOfTeams = (ListView) findViewById(R.id.teamList);
        sendUserId(userId);

/*
        listOfTeams.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {


                TeamDetail teamDetail = TeamDetail.newInstance(userId);
                teamDetail.show(getFragmentManager(),"TeamDetail");
            }
        });*/

    }

    public void leo(String i){
        Log.d("DSFD", i);
    }
    public void sendUserId(String userId){

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = "http://quizify.online/api/Teams/My";
        String prepData = "{\"id\":\""+userId+"\"}";


        JSONObject data = null;
        try{
            data = new JSONObject(prepData);
        }catch (Exception e){}

        Log.d("DSFD", prepData);

        JsonArrayRequest postRequest = new JsonArrayRequest(Request.Method.POST, url, data,
                new Response.Listener<JSONArray>() {
                    @Override
                    public void onResponse(JSONArray response) {
                        Log.d("DSFD", response.toString());
                        LinearLayout prostorZaPopis = (LinearLayout)findViewById(R.id.popisNaloga);
                        for(int i = 0; i < response.length(); i++){


                            try{

                                JSONObject object = response.getJSONObject(i);
                                final String a  = object.getString("id");
                                String e = object.getString("imeTima");
                                //teams.add(name);

                                TextView pogled = new TextView(MyTeams.this);
                                int idd = pogled.generateViewId();

                                pogled.setText(Html.fromHtml("<font color=\"#222222\"><big>"+e+"</big></font>"));
                                pogled.setHeight(150);
                                pogled.setOnClickListener(new View.OnClickListener() {
                                    @Override
                                    public void onClick(View v) {
                                        TextView t = (TextView)v;
                                        odaberiNalog(a);

                                    }
                                });
                                Log.d("DSFD", "IDDDD: "+a+"");
                                prostorZaPopis.addView(pogled);

                                Log.d("DSFD", name);
                            }
                            catch (Exception e){
                                Log.e("Likvidatura", "Greška kod kreiranja popisa!");
                            }
                        }
                       // setAdapter();
                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // error
                        Log.d("DSFD", error.toString());
                        CharSequence text = "Došlo je pogreške, molimo Vas kontaktirajte administtratora!";
                        int duration = Toast.LENGTH_LONG;
                        Toast toast = Toast.makeText(getApplicationContext(), error.toString(), duration);
                        toast.show();
                    }});
        queue.add(postRequest);




    }
    public void odaberiNalog(String i){

        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        SharedPreferences.Editor editor = preferences.edit();
        editor.putString("TimId", i);
        editor.commit();
        //TeamDetail teamDetail = TeamDetail.newInstance(userId);


        //TeamDetail teamDetail = new TeamDetail();
        //teamDetail.show(getFragmentManager(),"TeamDetail");

        Intent intent = new Intent(this,TeamMembers.class);
        startActivity(intent);

    }
    public void setAdapter(){
        if(id == "0"){
            Toast.makeText(getApplicationContext(),Toast.LENGTH_SHORT,Toast.LENGTH_SHORT).show();
        }else{
            TeamListAdapter teamListAdapter = new TeamListAdapter(teams, context);
            listOfTeams.setAdapter(teamListAdapter);
        }
    }
}
