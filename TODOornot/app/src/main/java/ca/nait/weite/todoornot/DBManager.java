package ca.nait.weite.todoornot;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.provider.BaseColumns;
import android.util.Log;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by wgeng1 on 3/14/2018.
 */

public class DBManager extends SQLiteOpenHelper
{
    static final String DB_NAME = "todo.db";
    static final int DB_VERSION = 3;

    static final String TABLE = "titles";
    static final String TABLE_SEC = "items";

    static final String COLUMN_TITLE_ID = BaseColumns._ID;
    static final String COLUMN_TITLE = "title";

    static final String COLUMN_ITEM_ID = COLUMN_TITLE_ID;
    static final String COLUMN_ITEM = "itemname";




    public DBManager(Context context)
    {
        super(context,DB_NAME,null,DB_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase database)
    {
        String sql = "CREATE TABLE " + TABLE + "("
                + COLUMN_TITLE_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, "
                + COLUMN_TITLE + " TEXT "
                + ")";
        database.execSQL(sql);
        sql = "CREATE TABLE " + TABLE_SEC + "( "
                + COLUMN_ITEM_ID + " INTEGER primary key, "
                + COLUMN_ITEM + " text, "
                + "title_id INTEGER "
                + ")";
        database.execSQL(sql);
    }

    @Override
    public void onUpgrade(SQLiteDatabase database, int oldVersion, int newVersion)
    {
        database.execSQL("drop table if exists " + TABLE_SEC);
        database.execSQL("drop table if exists " + TABLE); // drops the old database
        onCreate(database);
    }

    public void insertTitle(String title){
        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(COLUMN_TITLE, title);
        db.insert(TABLE, null, values);
        db.close(); // Closing database connection
    }
    public void insertItem(String item, int title)
    {
        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(COLUMN_ITEM, item);
        values.put("title_id", title);
        db.insert(TABLE_SEC, null, values);
    }
    public List<String> GetTitles(){
        List<String> titles = new ArrayList<String>();

        String query = "SELECT * FROM " + TABLE;

        SQLiteDatabase db = this.getReadableDatabase();
        Cursor cursor = db.rawQuery(query, null);

        if (cursor.moveToFirst()) {
            while(cursor.moveToNext())
            {
                titles.add(cursor.getString(1));
            };
        }

        cursor.close();
        db.close();

        return titles;
    }
}
