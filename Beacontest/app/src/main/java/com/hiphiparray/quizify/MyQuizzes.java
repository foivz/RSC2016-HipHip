package com.hiphiparray.quizify;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.text.Html;
import android.util.Log;
import android.view.View;
import android.widget.LinearLayout;
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

/**
 * Created by TOSHIBA on 26.11.2016..
 */

public class MyQuizzes extends AppCompatActivity {

    SharedPreferences preferences;
    String userId;
    String name;
    int id;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.quizzes_layout);


        preferences = PreferenceManager.getDefaultSharedPreferences(this);
        userId = preferences.getString("UserId","Default_Value");


        RequestQueue queue = Volley.newRequestQueue(this);
        String url = "http://quizify.online/api/Quizzez/My";
        String prepData = "{\"id\": \""+userId+"\"}";


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

                        LinearLayout prostorZaPopis = (LinearLayout)findViewById(R.id.popisQuizzes);

                        for(int i = 0; i < response.length(); i++){

                            try{

                                JSONObject object = response.getJSONObject(i);


                                name = object.getString("name");
                                id = object.getInt("id");
                                //teams.add(name);

                                Log.d("QUIZ",name);

                                TextView pogled = new TextView(MyQuizzes.this);
                                int idd = pogled.generateViewId();

                                pogled.setText(Html.fromHtml("<font color=\"#222222\"><big>"+name+"</big></font>"));
                                pogled.setHeight(150);
                                pogled.setOnClickListener(new View.OnClickListener() {
                                    @Override
                                    public void onClick(View v) {
                                        TextView t = (TextView)v;
                                        startQuiz(id);

                                    }
                                });


                                prostorZaPopis.addView(pogled);

                                //Log.d("DSFD", names+"_------");
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

    public void startQuiz(int id){


        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        SharedPreferences.Editor editor = preferences.edit();
        editor.putInt("QuizId", id+1);
        editor.commit();

        Intent intent = new Intent(this,Quiz.class);
        startActivity(intent);
    }

}
