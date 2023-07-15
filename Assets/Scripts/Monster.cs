using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string _name;

    public SpriteRenderer spriteRenderer;

    [Header ( "Stats" )]
    public Ability[] abilities;
    public int health;
    public int curHealth;
    public int movement;

    [Header ("Movement")]
    public int moveLeft;
    public float moveSpeed = 3;
    public Tile wantedTile;

    [Header ( "UI" )]
    public int healthbarOffset = 90;
    public int abilitiesOffset = 110;

    [Header ( "AI" )]
    public AIBrain brain;

    [Header ( "Battle" )]
    public int preferedStartingPos;
    public Tile tile;
    public GameObject LOSComponent;
    public GlobalGameValues.Team team;
    public List<Board.moveTypes> revokedMoveTypes = new List<Board.moveTypes> ();

    private void OnEnable ()
    {
        NotificationCenter.instance.PostNotification ( this , Notification.notifications.monsterSpawn , new Hashtable () { { Notification.datas.character , this } } );
    }

    private void OnDisable ()
    {
        NotificationCenter.instance.PostNotification ( this , Notification.notifications.monsterDespawn , new Hashtable () { { Notification.datas.character , this } } );
    }
}
