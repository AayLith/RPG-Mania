using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : InputReceiver
{
    public float speed = 1.0F;

    private void Start ()
    {
        InputDispatcher.instance.openReceiver ( this );
    }

    private void OnDestroy ()
    {
        InputDispatcher.instance.closeReceiver ( this );
    }

    public override void updateInput ( Dictionary<InputDispatcher.inputs , InputDispatcher.input> inputList )
    {
        transform.position += new Vector3 ( Input.GetAxis ( "Horizontal" ) , Input.GetAxis ( "Vertical" ) , 0 ) * Time.fixedDeltaTime;
    }

    public override void fixedUpdateInput ()
    {
        transform.position += new Vector3 ( Input.GetAxis ( "Horizontal" ) , Input.GetAxis ( "Vertical" ) , 0 ) * Time.fixedDeltaTime;
    }
}
