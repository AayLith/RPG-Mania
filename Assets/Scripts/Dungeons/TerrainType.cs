using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainType
{
    public enum terrainTypes { Ground, Void, Water, ShallowWater }
    public static Color getColor ( terrainTypes type )
    {
        switch ( type )
        {
            case terrainTypes.Void:
                return Color.black;
            case terrainTypes.Ground:
                return Color.yellow;
            case terrainTypes.Water:
                return Color.blue;
            case terrainTypes.ShallowWater:
                return Color.cyan;
        }
        return Color.white;
    }

    public static List<Board.moveTypes> getAllowedMoveTypes ( terrainTypes type )
    {
        List<Board.moveTypes> list = new List<Board.moveTypes> ();
        switch ( type )
        {
            case terrainTypes.Void:
                list.Add ( Board.moveTypes.Fly );
                break;
            case terrainTypes.Ground:
                list.Add ( Board.moveTypes.Fly );
                list.Add ( Board.moveTypes.Walk );
                list.Add ( Board.moveTypes.Ethereal );
                break;
            case terrainTypes.Water:
                list.Add ( Board.moveTypes.Fly );
                list.Add ( Board.moveTypes.Swim );
                list.Add ( Board.moveTypes.Ethereal );
                break;
            case terrainTypes.ShallowWater:
                list.Add ( Board.moveTypes.Fly );
                list.Add ( Board.moveTypes.Swim );
                list.Add ( Board.moveTypes.Walk );
                list.Add ( Board.moveTypes.Ethereal );
                break;
        }
        list.Add ( Board.moveTypes.Teleport );
        return list;
    }
}
