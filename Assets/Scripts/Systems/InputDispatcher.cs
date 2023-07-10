using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDispatcher : MonoBehaviour
{
    public static InputDispatcher instance;

    public float lockLength = 0.1f;
    public bool masterLock = false;
    public List<InputReceiver> receivers = new List<InputReceiver> ();
    InputReceiver _activeReceiver { get { return receivers.Count == 0 ? null : receivers[ receivers.Count - 1 ]; } }
    Dictionary<inputs , input> inputList = new Dictionary<inputs , input> ();

    public enum inputs { Horizontal, Vertical }

    public class input
    {
        public float value;
        public bool locked = false;
    }

    private void Awake ()
    {
        instance = this;
    }

    private void Update ()
    {
        if ( masterLock ) return;
        if ( _activeReceiver == null ) return;

        foreach ( InputReceiver.input i in _activeReceiver.inputs )
        {
            // Si l'input n'est pas dans le dictionnaire, l'ajouter
            if ( !inputList.ContainsKey ( i.axis ) )
                inputList.Add ( i.axis , new input () );

            // Intégrer la valeur si l'input est actif, sinon la reset à 0
            input inp = inputList[ i.axis ];
            inp.value = inp.locked ? 0 : Input.GetAxis ( i.axis + "" );
            if ( i.lockAfterInput )
                StartCoroutine ( lockInput ( inp ) );
        }

        _activeReceiver.updateInput ( inputList );
    }

    public void openReceiver ( InputReceiver receiver )
    {
        if ( receivers.Contains ( receiver ) )
            closeReceiver ( receiver );
        receivers.Add ( receiver );
        StartCoroutine ( lockDispatcher () );
    }

    public void closeReceiver ( InputReceiver receiver )
    {
        receivers.Remove ( receiver );
        StartCoroutine ( lockDispatcher () );
    }

    IEnumerator lockInput ( input i )
    {
        i.locked = true;
        yield return new WaitForSeconds ( lockLength );
        i.locked = false;
    }

    IEnumerator lockDispatcher ()
    {
        masterLock = true;
        yield return new WaitForSeconds ( lockLength );
        masterLock = false;
    }
}
