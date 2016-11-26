package com.hiphiparray.quizify;

import android.app.Application;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.facebook.AccessToken;
import com.facebook.CallbackManager;
import com.facebook.FacebookCallback;
import com.facebook.FacebookException;
import com.facebook.FacebookSdk;
import com.facebook.login.LoginManager;
import com.facebook.login.LoginResult;
import com.facebook.login.widget.LoginButton;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.AuthCredential;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FacebookAuthProvider;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;

import org.json.JSONObject;

import java.util.Arrays;
import java.util.List;

/**
 * Created by TOSHIBA on 26.11.2016..
 */

public class LogInActivity extends AppCompatActivity {

    LoginButton loginButton;
    CallbackManager callbackManager;
    private FirebaseAuth.AuthStateListener mAuthListener;
    private FirebaseAuth mAuth;
    String name;
    String email;
    String username;
    String lastName;


    //private TwitterLoginButton loginButton;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.login);
        mAuth = FirebaseAuth.getInstance();

        // ...
        mAuthListener = new FirebaseAuth.AuthStateListener() {
            @Override
            public void onAuthStateChanged(@NonNull FirebaseAuth firebaseAuth) {
                FirebaseUser user = firebaseAuth.getCurrentUser();
                if (user != null) {
                    // User is signed in

                    name = user.getDisplayName();
                    email = user.getEmail();
                    Intent output = new Intent(getApplicationContext(), MainActivity.class);
                    output.putExtra("Username",name);
                    setResult(RESULT_OK,output);
                    finish();
                    Login();
                } else {
                    // User is signed out
                    //Log.d(TAG, "onAuthStateChanged:signed_out");
                }
                // ...
            }
        };


        //FacebookSdk.sdkInitialize(getActivity());
        callbackManager = CallbackManager.Factory.create();
        loginButton = (LoginButton) findViewById(R.id.login_button);
        //loginButton.setReadPermissions("email");
        loginButton.setReadPermissions("email", "public_profile");


        // Callback registration
        loginButton.registerCallback(callbackManager, new FacebookCallback<LoginResult>() {
            @Override
            public void onSuccess(LoginResult loginResult) {
                // App code
                //Log.d(TAG, "facebook:onSuccess:" + loginResult);
                handleFacebookAccessToken(loginResult.getAccessToken());
            }

            @Override
            public void onCancel() {
                // App code
                FirebaseAuth.getInstance().signOut();
                LoginManager.getInstance().logOut();
            }

            @Override
            public void onError(FacebookException exception) {
                // App code
            }
        });

    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        callbackManager.onActivityResult(requestCode, resultCode, data);


    }

    @Override
    public void onStart() {
        super.onStart();
        mAuth.addAuthStateListener(mAuthListener);
    }

    @Override
    public void onStop() {
        super.onStop();
        if (mAuthListener != null) {
            mAuth.removeAuthStateListener(mAuthListener);
            FirebaseAuth.getInstance().signOut();
        }
    }

    private void handleFacebookAccessToken(AccessToken token) {
        //Log.d(TAG, "handleFacebookAccessToken:" + token);

        AuthCredential credential = FacebookAuthProvider.getCredential(token.getToken());
        mAuth.signInWithCredential(credential)
                .addOnCompleteListener(this, new OnCompleteListener<AuthResult>() {
                    @Override
                    public void onComplete(@NonNull Task<AuthResult> task) {
                        //Log.d(TAG, "signInWithCredential:onComplete:" + task.isSuccessful());

                        // If sign in fails, display a message to the user. If sign in succeeds
                        // the auth state listener will be notified and logic to handle the
                        // signed in user can be handled in the listener.
                        if (!task.isSuccessful()) {
                            Log.d("Token", "signInWithCredential", task.getException());

                            /*Toast.makeText(getApplicationContext(), "Authentication failed.",
                                    Toast.LENGTH_SHORT).show();*/
                        }

                        // ...
                    }
                });
    }



    public void Login(){
        RequestQueue queue = Volley.newRequestQueue(this);


        String url = "http://quizify.online/api/Users/Login";
        Log.d("DSFD", email);
        Log.d("DSFD", name);
        String prepData = "{\"email\":\""+email+"\", \"ime\":\""+name+"\"}";
        Log.d("DSFD", prepData);
        JsonObjectRequest putRequest = null;

        try{
            putRequest = new JsonObjectRequest(Request.Method.PUT, url, new JSONObject(prepData), new Response.Listener<JSONObject>() {
                @Override
                public void onResponse(JSONObject response) {
                    String id_korisnika = response.optString("ret");


                    if (id_korisnika.compareTo("Error")==1) {
                        Toast.makeText(getApplicationContext(), "Invalid userdata, try again", Toast.LENGTH_LONG).show();
                        Toast.makeText(getApplicationContext(),"error",Toast.LENGTH_LONG).show();
                    }else{
                        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
                        SharedPreferences.Editor editor = preferences.edit();
                        editor.putString("UserId", id_korisnika);
                        Toast.makeText(getApplicationContext(),"Uspješno logiran!",Toast.LENGTH_LONG).show();
                        editor.apply();
                    }
                }
            }, new Response.ErrorListener() {
                @Override
                public void onErrorResponse(VolleyError error) {
                    Log.d("DSFD", error.toString());
                }
            });
        }catch (Exception e){}

        queue.add(putRequest);

    }



}
