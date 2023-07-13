using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string _name;
    public Ability[] abilities;

    public int health;
    public int curHealth;

    public int healthbarOffset = 90;
    public int abilitiesOffset = 110;

    private void OnEnable ()
    {
        NotificationCenter.instance.PostNotification ( this , Notification.notifications.monsterSpawn , new Hashtable () { { Notification.datas.character , this } } );
    }

    private void OnDisable ()
    {
        NotificationCenter.instance.PostNotification ( this , Notification.notifications.monsterDespawn , new Hashtable () { { Notification.datas.character , this } } );
    }
}
