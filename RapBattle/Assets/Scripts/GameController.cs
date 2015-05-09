using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public ButtonEvents buttonEvents;

    public Button topButton;
    public Button middleButton;
    public Button bottomButton;

    public Text displayText;

    public int maxHypeValue;
    public Color maxHypeColor;
    public Color minHypeColor;

    public Slider hypeMeter;
    public Image hypeMeterBackground;

    public int currentHype;

    public int hypeIncrement = 5;
	
    
    // Use this for initialization
	void Start ()
    {
        hypeMeter.fillRect.GetComponent<Image>().color = maxHypeColor;
        hypeMeterBackground.color = minHypeColor;
        hypeMeter.minValue = 0;
        hypeMeter.maxValue = maxHypeValue;
        currentHype = (int)(maxHypeValue * 0.5f);
        updateHypeMeter();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("getting hype!");
            currentHype += hypeIncrement;
            updateHypeMeter();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("getting unhype!");
            currentHype -= hypeIncrement;
            updateHypeMeter();
        }
	}

    private void updateHypeMeter()
    {
        hypeMeter.value = currentHype;
    }
}
