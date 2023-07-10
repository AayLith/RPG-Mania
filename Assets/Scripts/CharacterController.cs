using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : InputReceiver
{
    public float speed=1.0F;
    public void Start ()
    {
        InputDispatcher.instance.openReceiver(this);
    }   
    public void OnDestroy()
    {
        InputDispatcher.instance.closeReceiver(this);
    }

    public override void updateInput ( Dictionary<InputDispatcher.inputs , InputDispatcher.input> inputList )
    {
        transform.position += new Vector3(inputList[InputDispatcher.inputs.Horizontal].value, inputList[InputDispatcher.inputs.Vertical].value, 0)*Time.fixedDeltaTime;

    }

    public override void fixedUpdateInput ()
    {
        
    }
}
