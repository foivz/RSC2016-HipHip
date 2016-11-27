package com.hiphiparray.quizify;

import android.app.ActivityManager;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.Handler;
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
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.Timer;
import java.util.TimerTask;

/**
 * Created by TOSHIBA on 27.11.2016..
 */

public class Quiz extends AppCompatActivity {

    int quizId;
    String userId;

    Timer timer;
    Context context;
    Handler mHandler;
    String answer;
    int questionId;
    TextView tvQuestion;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.quiz_layout);

        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
        quizId = preferences.getInt("QuizId",0);
        userId = preferences.getString("UserId", "DEFAULT");
        context = getApplicationContext();

        this.mHandler = new Handler();

        this.mHandler.postDelayed(m_Runnable,5000);



    }


    public void refresh(){



        Intent refresh = new Intent(this, Quiz.class);
        startActivity(refresh);
        this.finish();
    }


    public void sendPost(){

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = "http://quizify.online/api/Start";
        String prepData = "{\"idKor\":\""+userId+"\",\"idKviz\":\"7\"}";


        JSONObject data = null;
        try{
            data = new JSONObject(prepData);
        }catch (Exception e){}

        Log.d("POST", prepData);

        JsonObjectRequest postRequest = new JsonObjectRequest(Request.Method.POST, url, data,
                new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {

                        String pitanj = response.optString("pitanj");
                        int tip = response.optInt("tip");


                        JSONArray odgovori = response.optJSONArray("odgovori");

                        for(int i = 0; i < odgovori.length(); i++){
                            try{

                                JSONObject object = odgovori.getJSONObject(i);

                                LinearLayout prostorZaPopis = (LinearLayout)findViewById(R.id.popisOdgovora);

                                answer = object.getString("odgovor");
                                questionId = object.getInt("id");

                                TextView pogled = new TextView(Quiz.this);
                                int idd = pogled.generateViewId();

                                tvQuestion.setText(pitanj);
                                pogled.setText(Html.fromHtml("<font color=\"#222222\"><big>"+answer+"</big></font>"));
                                pogled.setHeight(150);
                                pogled.setOnClickListener(new View.OnClickListener() {
                                    @Override
                                    public void onClick(View v) {
                                        TextView t = (TextView)v;

                                    }
                                });


                                prostorZaPopis.addView(pogled);

                                //Log.d("DSFD", names+"_------");
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


                    }});
        queue.add(postRequest);
    }

    private final Runnable m_Runnable = new Runnable()
    {
        public void run()

        {
            Toast.makeText(Quiz.this,"in runnable",Toast.LENGTH_SHORT).show();
            sendPost();
            Quiz.this.mHandler.postDelayed(m_Runnable, 2000);
        }

    };
}
