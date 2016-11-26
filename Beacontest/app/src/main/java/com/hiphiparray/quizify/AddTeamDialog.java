package com.hiphiparray.quizify;

import android.app.AlertDialog;
import android.app.Dialog;
import android.app.DialogFragment;
import android.content.DialogInterface;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

/**
 * Created by TOSHIBA on 26.11.2016..
 */

public class AddTeamDialog extends DialogFragment {

    View view;
    LayoutInflater inflater;
    EditText teamName;


    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        inflater = getActivity().getLayoutInflater();
        view = inflater.inflate(R.layout.dialog_addteeam,null);

        teamName = (EditText) view.findViewById(R.id.addTeamName);

        builder.setPositiveButton("Submit", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int i) {
                Toast.makeText(getActivity(),"Submit",Toast.LENGTH_SHORT).show();
            }
        });

        builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int i) {
                Toast.makeText(getActivity(),"Cancel",Toast.LENGTH_SHORT).show();
            }
        });



        Dialog dialog = builder.create();

        return dialog;
    }
}
