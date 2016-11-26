package com.hiphiparray.quizify;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.widget.ArrayAdapter;
import android.widget.ListView;
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

import java.util.List;

import static android.R.attr.data;

/**
 * Created by TOSHIBA on 26.11.2016..
 */

public class MyTeams extends AppCompatActivity {
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.myteams_layout);



        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
        String userId = preferences.getString("UserId","Default_Value");

        sendUserId(userId);



       // ArrayAdapter<String> adapter = new ArrayAdapter(this,R.layout.item_team,);
        //ListView listView = new ListView(this);
        //listView.setAdapter(adapter);
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
                        for(int i = 0; i < response.length(); i++){

                            try{
                                JSONObject object = response.getJSONObject(i);
                                String id = object.getString("id");
                                String name = object.getString("name");

                                if(id.compareTo("0")==1)
                                    Toast.makeText(getApplicationContext(),"You don't have teams ", Toast.LENGTH_SHORT).show();
                                Log.d("DSFD", name);
                            }
                            catch (Exception e){
                                Log.e("Likvidatura", "Greška kod kreiranja popisa!");
                            }
                        }
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
}
