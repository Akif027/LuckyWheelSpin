using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RemoveButton : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<Image>().sprite == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

 
}
