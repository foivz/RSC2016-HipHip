package com.hiphiparray.quizify;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.ArrayList;

/**
 * Created by TOSHIBA on 27.11.2016..
 */

public class TeamNamesAdapter extends BaseAdapter {

    LayoutInflater inflater;
    ArrayList<String> names;
    TextView members;

    public TeamNamesAdapter(ArrayList<String> names, Context context){
        this.names = names;
        inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
    }

    @Override
    public int getCount() {
        return names.size();
    }

    @Override
    public Object getItem(int i) {
        return names.get(i);
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {


        view = inflater.inflate(R.layout.item_membernames, viewGroup, false);
        members = (TextView) view.findViewById(R.id.name);



        members.setText(names.get(i));


        return view;
    }
}
