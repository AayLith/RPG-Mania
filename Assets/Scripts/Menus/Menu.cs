using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : InputReceiver
{
    public int curOption = 0;
    public List<MenuOption> options = new List<MenuOption> ();


    public virtual void nextOption ()
    {/*
        options[ curOption ].onEndHover ();
        curOption++;
        if ( curOption == options.Count )
            curOption = 0;
        options[ curOption ].onStartHover ();
        if ( !options[ curOption ].optionEnabled )
            nextOption ();
        */
    }

    public virtual void prevOption ()
    {/*
        options[ curOption ].onEndHover ();
        curOption--;
        if ( curOption < 0 )
            curOption = options.Count - 1;
        options[ curOption ].onStartHover ();
        if ( !options[ curOption ].optionEnabled )
            prevOption ();
        */
    }

    public abstract void open();
    public abstract void close();
}
