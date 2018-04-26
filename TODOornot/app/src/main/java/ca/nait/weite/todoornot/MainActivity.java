package ca.nait.weite.todoornot;

import android.content.ContentValues;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleCursorAdapter;
import android.widget.Spinner;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity implements View.OnClickListener,AdapterView.OnItemSelectedListener
{

    DBManager dbManager;
    SQLiteDatabase db;
    ListView listview;
    Cursor cursor;
    Spinner titleSpinner;
    ArrayList<String> titleSpinAdapter = new ArrayList<String>();
    ArrayList<String> itemSpinAdapter = new ArrayList<String>();
    SimpleCursorAdapter itemListadapter;

    static final String[] FROM = {DBManager.COLUMN_TITLE};
    static final int[] TO = {R.id.item_list};

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);


        dbManager = new DBManager(this);
        refreshTitleSpinner();
        titleSpinner = (Spinner) findViewById(R.id.title_spinner);
        titleSpinner.setOnItemSelectedListener(this);
        loadSpinner();

        Button subButton = (Button) findViewById(R.id.send_title_button);
        subButton.setOnClickListener(this);
        Button addButton = (Button) findViewById(R.id.send_item_button);
        addButton.setOnClickListener(this);


    }

    @Override
    public void onClick(View view)
    {
        //db = dbManager.getWritableDatabase();
        switch(view.getId())
        {
            case R.id.send_title_button:
            {
                dbManager = new DBManager(getApplicationContext());
                EditText testView = (EditText) findViewById(R.id.job_title_text_sender);
                String lName =  testView.getText().toString();
                dbManager.insertTitle(lName);
                testView.setText("");
                loadSpinner();
                break;
            }
            case R.id.send_item_button:
            {
                EditText testView = (EditText) findViewById(R.id.item_text_sender);
                String mItem =  testView.getText().toString();
                int titleID = titleSpinner.getSelectedItemPosition() + 1;
                dbManager.insertItem(mItem, titleID);
                testView.setText("");
                refreshItemlist(titleID);
                break;
            }
        }
        loadSpinner();
    }

    @Override
    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l)
    {
        int titleid = titleSpinner.getSelectedItemPosition() +  1;
        refreshItemlist(titleid);
    }

    private void loadSpinner()
    {
        DBManager db = new DBManager(getApplicationContext());

        List<String> titles = db.GetTitles();

        ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, titles);

        dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

        titleSpinner.setAdapter(dataAdapter);
    }

    private void refreshTitleSpinner() {
        titleSpinAdapter = new ArrayList<String>();
        db = dbManager.getReadableDatabase();
        Cursor cursor = db.query(DBManager.TABLE, null, null, null, null, null, null);
        startManagingCursor(cursor);
        String title;
        while(cursor.moveToNext())
        {
            title = cursor.getString(cursor.getColumnIndex(DBManager.COLUMN_TITLE));
            titleSpinAdapter.add(title);
        }
        db.close();
    }
    private void refreshItemlist(int titleID)
    {
        itemSpinAdapter = new ArrayList<String>();
        db = dbManager.getReadableDatabase();
        Cursor cursor = db.query(dbManager.TABLE_SEC, null, "title_id=" + titleID, null, null, null, null);
        startManagingCursor(cursor);
        String item;
        while(cursor.moveToNext())
        {
            item = cursor.getString(cursor.getColumnIndex(DBManager.COLUMN_ITEM));
            itemSpinAdapter.add(item);
        }
        listview.setAdapter((ListAdapter) itemSpinAdapter);

        db.close();
    }



    @Override
    public void onNothingSelected(AdapterView<?> adapterView) {

    }
}