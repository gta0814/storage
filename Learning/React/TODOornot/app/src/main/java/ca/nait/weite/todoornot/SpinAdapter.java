package ca.nait.weite.todoornot;

import android.content.Context;
import android.widget.ArrayAdapter;

import java.util.ArrayList;

/**
 * Created by wgeng1 on 4/9/2018.
 */

public class SpinAdapter extends ArrayAdapter
{
    private Context context;
    private ArrayList chatter;

    public SpinAdapter(Context context, int resourceId, ArrayList objects)
    {
        super(context, resourceId, objects);
        this.context = context;
        this.chatter = objects;
    }

}
