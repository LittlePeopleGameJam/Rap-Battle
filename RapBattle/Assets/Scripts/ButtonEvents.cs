using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    public void ResolveClick(Object sender)
    {   
        GameObject gO = sender as GameObject;

        Button button = gO.GetComponent<Button>();
        
        if (button == null)
        {
            Debug.Log("button null");
            return;
        }

        button.GetComponentInChildren<Text>().text = "happy";
    }
}
