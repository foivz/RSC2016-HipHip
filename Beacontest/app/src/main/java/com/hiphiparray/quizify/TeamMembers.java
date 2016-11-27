package com.hiphiparray.quizify;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.text.Html;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

/**
 * Created by TOSHIBA on 27.11.2016..
 */

public class TeamMembers extends AppCompatActivity {


    String userId;
    String timId;
    SharedPreferences preferences;
    ArrayList<String> arrayList;
    Context context;
    ListView list;
    String names;


    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.members);

        preferences = PreferenceManager.getDefaultSharedPreferences(this);
        userId = preferences.getString("UserId","Default_Value");
        timId = preferences.getString("TimId","Default_Value");

        list = (ListView) findViewById(R.id.list);
        context = this;

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = "http://quizify.online/api/Teams/Members";
        String prepData = "{\"idKor\": \""+userId+"\",\"idTima\":"+timId+"}";


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

                        LinearLayout prostorZaPopis = (LinearLayout)findViewById(R.id.popisMembera);

                        for(int i = 0; i < response.length(); i++){


                            try{

                                JSONObject object = response.getJSONObject(i);
                                //final String a  = object.getString("ime");
                                names = object.getString("ime");


                                String e = object.getString("ime");
                                //teams.add(name);

                                TextView pogled = new TextView(TeamMembers.this);
                                int idd = pogled.generateViewId();

                                pogled.setText(Html.fromHtml("<font color=\"#222222\"><big>"+e+"</big></font>"));
                                pogled.setHeight(150);


                                prostorZaPopis.addView(pogled);

                                Log.d("DSFD", names+"_------");
                            }
                            catch (Exception e){
                                Log.e("Likvidatura", "Greška kod kreiranja popisa!");
                            }
                        }

                        //setAdapter(arrayList);

                        //
                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // error
                        Log.d("DSFD", error.toString());
                        CharSequence text = "Došlo je pogreške, molimo Vas kontaktirajte administtratora!";
                        int duration = Toast.LENGTH_LONG;

                    }});
        queue.add(postRequest);


    }

    public void setAdapter(ArrayList<String> array){
        ArrayAdapter<String> itemsAdapter =
                new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, array);
        list.setAdapter(itemsAdapter);
        //TeamNamesAdapter teamNamesAdapter = new TeamNamesAdapter(array, context);
        //list.setAdapter(teamNamesAdapter);
    }
}
