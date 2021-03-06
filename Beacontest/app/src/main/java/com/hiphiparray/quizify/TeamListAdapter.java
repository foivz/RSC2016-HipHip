package com.hiphiparray.quizify;

import android.content.Context;
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
    TextView teamName;
    TextView teamNo;
    LayoutInflater inflater;

    public TeamListAdapter(ArrayList<String> teams, Context context) {
        this.teams = teams;
        inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
    }


    @Override
    public int getCount() {
        return teams.size();
    }

    @Override
    public Object getItem(int i) {
        return teams.get(i);
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {

        if (view == null){
            view =  inflater.inflate(R.layout.item_team, null);
            teamName = (TextView) view.findViewById(R.id.teamName);
            teamNo = (TextView) view.findViewById(R.id.teamNumber);
        }


        teamName.setText(teams.get(i));
        teamNo.setText(i+"");

        return view;
    }
}
