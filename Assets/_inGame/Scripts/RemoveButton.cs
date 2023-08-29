using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveButton : MonoBehaviour
{
    public static int wheelPieceIndex;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        wheelPieceIndex = transform.childCount; // Get the index of the button in the parent's children
        Debug.Log(wheelPieceIndex);
    }
}
