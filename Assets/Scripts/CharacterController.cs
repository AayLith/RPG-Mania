using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : InputReceiver
{
    public float speed=1.0F;
    public override void updateInput ( Dictionary<InputDispatcher.inputs , InputDispatcher.input> inputList )
    {
        throw new System.NotImplementedException ();
    }

    public override void fixedUpdateInput ()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)*Time.fixedDeltaTime;
    }
}
