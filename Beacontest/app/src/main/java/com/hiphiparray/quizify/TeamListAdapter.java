package com.hiphiparray.quizify;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.ArrayList;

/**
 * Created by TOSHIBA on 26.11.2016..
 */

public class TeamListAdapter extends BaseAdapter {

    private ArrayList<String> teams;
    private LayoutInflater inflater=null;
    TextView teamName;

    public TeamListAdapter(ArrayList<String> teams) {
        this.teams = teams;
    }

    public TeamListAdapter(){
    }

    @Override
    public int getCount() {
        return teams.size();
    }

    @Override
    public Object getItem(int i) {
        return i;
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {

        if(view == null){
            view =  inflater.inflate(R.layout.item_team, null);
            teamName = (TextView) view.findViewById(R.id.teamName);

        }

        teamName.setText(teams.get(i));

        return view;
    }
}
