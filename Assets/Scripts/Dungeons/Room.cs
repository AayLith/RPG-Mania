using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform tiles;
    public Transform doodads;
    public Transform spawns;
    public Transform objects;
    public Dictionary<Point , Tile> roomTiles = new Dictionary<Point , Tile> ();

    public int leftLimit = 0;
    public int rightLimit = 0;
}
