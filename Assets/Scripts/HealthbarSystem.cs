using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarSystem : MonoBehaviour, NotificationReceiver
{
    public Slider healthBarPrefab;
    public Canvas canvas;
    Dictionary<Monster , Slider> healthBars = new Dictionary<Monster , Slider> ();

    private void Awake ()
    {
        NotificationCenter.instance.AddObserver ( this , Notification.notifications.monsterSpawn );
        NotificationCenter.instance.AddObserver ( this , Notification.notifications.monsterDespawn );
        NotificationCenter.instance.AddObserver ( this , Notification.notifications.monsterTakeDamage );
    }

    private void Update ()
    {
        foreach ( var v in healthBars )
            v.Value.transform.position = Camera.main.WorldToScreenPoint ( v.Key.transform.position ) + new Vector3 ( 0 , v.Key.healthbarOffset , 0 );
    }

    public void receiveNotification ( Notification notification )
    {
        Monster m = ( Monster ) notification.data[ Notification.datas.character ];
        switch ( notification.name )
        {
            case Notification.notifications.monsterSpawn:
                healthBars.Add ( m , Instantiate ( healthBarPrefab.gameObject , canvas.transform ).GetComponent<Slider> () );
                healthBars[ m ].maxValue = m.health;
                healthBars[ m ].value = m.curHealth;
                break;
            case Notification.notifications.monsterDespawn:
                Destroy ( healthBars[ m ].gameObject );
                healthBars.Remove ( m );
                break;
            case Notification.notifications.monsterTakeDamage:
                healthBars[ m ].value = m.curHealth;
                break;
        }
    }
}
