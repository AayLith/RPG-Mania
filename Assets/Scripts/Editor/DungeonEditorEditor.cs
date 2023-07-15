using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor ( typeof ( BoardEditor ) )]
public class DungeonEditorEditor : Editor
{
    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector ();
        BoardEditor myScript = ( BoardEditor ) target;

        if ( GUILayout.Button ( "Add Tile Mode" ) )
            myScript.currentState = BoardEditor.states.addTile;
        if ( GUILayout.Button ( "Remove Tile Mode" ) )
            myScript.currentState = BoardEditor.states.remTile;
        if ( GUILayout.Button ( "Select Tile Mode" ) )
            myScript.currentState = BoardEditor.states.selectTile;
        if ( GUILayout.Button ( "Switch Tile Mode" ) )
            myScript.currentState = BoardEditor.states.switchTile;
        if ( GUILayout.Button ( "Remove Object Mode" ) )
            myScript.currentState = BoardEditor.states.remObject;
        if ( GUILayout.Button ( "Terrain Mode" ) )
            myScript.currentState = BoardEditor.states.terrain;

        GUILayout.Space ( 20 );
        if ( GUILayout.Button ( "Reset/New Room" ) )
            myScript.reset ();
    }
}
