using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : InputReceiver
{
    //Mise en commentaire des <<<, >>>, === car erreur: "Assets\Scripts\CharacterController.cs(32,1): error CS8300: Merge conflict marker encountered


    //<<<<<<< HEAD
//=======
    public float speed = 1.0F;

    private void Start ()
    {
        InputDispatcher.instance.openReceiver ( this );
    }

    private void OnDestroy ()
    {
        InputDispatcher.instance.closeReceiver ( this );
//>>>>>>> 1045d62aa8c7429dc0b7af6a4e14b81168387874
    }

    public override void updateInput ( Dictionary<InputDispatcher.inputs , InputDispatcher.input> inputList )
    {
//<<<<<<< HEAD
        transform.position += new Vector3(inputList[InputDispatcher.inputs.Horizontal].value, inputList[InputDispatcher.inputs.Vertical].value, 0)*Time.fixedDeltaTime;

//=======
        transform.position += new Vector3 ( Input.GetAxis ( "Horizontal" ) , Input.GetAxis ( "Vertical" ) , 0 ) * Time.fixedDeltaTime;
//>>>>>>> 1045d62aa8c7429dc0b7af6a4e14b81168387874
    }

    public override void fixedUpdateInput ()
    {
//<<<<<<< HEAD
        
//=======
        transform.position += new Vector3 ( Input.GetAxis ( "Horizontal" ) , Input.GetAxis ( "Vertical" ) , 0 ) * Time.fixedDeltaTime;
//>>>>>>> 1045d62aa8c7429dc0b7af6a4e14b81168387874
    }
}
