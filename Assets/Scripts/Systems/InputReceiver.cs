using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputReceiver : MonoBehaviour
{
    [System.Serializable]
    public class input
    {
        public InputDispatcher.inputs axis;
        public bool axislock = false;
        public bool lockAfterInput = true;
        public bool fixedupdate = false;
    }

    public List<input> inputs = new List<input> ();

    public abstract void updateInput ( Dictionary<InputDispatcher.inputs , InputDispatcher.input> inputList );

    public abstract void fixedUpdateInput ();
}
