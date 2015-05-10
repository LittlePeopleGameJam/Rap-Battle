using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    public void ResolveClick(Object sender)
    {   
        GameObject gO = sender as GameObject;

        ButtonController buttonController = gO.GetComponent<ButtonController>();

        if (buttonController == null)
        {
            Debug.Log("button null");
            return;
        }

        Debug.Log("clicked: " + buttonController.GetPhrase().text);
    }
}
