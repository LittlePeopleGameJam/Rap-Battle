using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public ButtonEvents buttonEvents;

    public Dictionary<string, List<Phrase>> phraseDictionary;

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
        createPhraseDictionary();
        
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

    private void createPhraseDictionary()
    {
        phraseDictionary = new Dictionary<string,List<Phrase>>();

        List<Phrase> PhraseList1 = new List<Phrase>();
        PhraseList1.Add(new Phrase(StringConsts.PHRASE_1, PhraseValue.OPENER));
        PhraseList1.Add(new Phrase(StringConsts.RESP_1_BAD, PhraseValue.BAD));
        PhraseList1.Add(new Phrase(StringConsts.RESP_1_GOOD_SMART, PhraseValue.SMART));
        PhraseList1.Add(new Phrase(StringConsts.RESP_1_GOOD_TOUGH, PhraseValue.TOUGH));

        phraseDictionary.Add(StringConsts.PHRASE_1_KEY, PhraseList1);


        Debug.Log("dictionary compiled");
    }
}
