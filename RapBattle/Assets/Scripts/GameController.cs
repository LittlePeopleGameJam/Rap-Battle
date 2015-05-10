using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public ButtonEvents buttonEvents;
    public DisplayController displayController;

    public Dictionary<string, List<Phrase>> phraseDictionary;

    public int maxHypeValue;
    public Color maxHypeColor;
    public Color minHypeColor;

    public Slider hypeMeter;
    public Image hypeMeterBackground;

    public int currentHype;

    public int hypeIncrement = 10;

    public int hypeGoodModifier = 1;
    public int hypeGreatModifier = 2;
    public int hypeBadModifier = -1;

    public int hypeChokeModifier = -2;

    public int activeKeyIndex;
    private List<string> m_PhraseKeys;

    public int opponentIndex;
    public PhraseValue[] opponentWeaknesses;
    
    // Use this for initialization
	void Start ()
    {
        createKeyList();
        createPhraseDictionary();
        
        //hypeMeter.fillRect.GetComponent<Image>().color = maxHypeColor;
        //hypeMeterBackground.color = minHypeColor;
        hypeMeter.minValue = 0;
        hypeMeter.maxValue = maxHypeValue;
        currentHype = (int)(maxHypeValue * 0.5f);
        updateHypeMeter();

        updateDisplayText();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("getting hype!");
        //    currentHype += hypeIncrement;
        //    updateHypeMeter();
        //}
        //
        //if (Input.GetMouseButtonDown(1))
        //{
        //    Debug.Log("getting unhype!");
        //    currentHype -= hypeIncrement;
        //    updateHypeMeter();
        //}
	}

    private void updateHypeMeter()
    {
        hypeMeter.value = currentHype;
    }

    private void createPhraseDictionary()
    {
        phraseDictionary = new Dictionary<string, List<Phrase>>();

        List<Phrase> PhraseList1 = new List<Phrase>();
        PhraseList1.Add(new Phrase(StringConsts.PHRASE_1, PhraseValue.OPENER));
        PhraseList1.Add(new Phrase(StringConsts.RESP_1_BAD, PhraseValue.BAD));
        PhraseList1.Add(new Phrase(StringConsts.RESP_1_GOOD_SMART, PhraseValue.SMART));
        PhraseList1.Add(new Phrase(StringConsts.RESP_1_GOOD_TOUGH, PhraseValue.TOUGH));

        phraseDictionary.Add(StringConsts.PHRASE_1_KEY, PhraseList1);

        List<Phrase> PhraseList2 = new List<Phrase>();
        PhraseList2.Add(new Phrase(StringConsts.PHRASE_2, PhraseValue.OPENER));
        PhraseList2.Add(new Phrase(StringConsts.RESP_2_BAD, PhraseValue.BAD));
        PhraseList2.Add(new Phrase(StringConsts.RESP_2_GOOD_DIRTY, PhraseValue.DIRTY));
        PhraseList2.Add(new Phrase(StringConsts.RESP_2_GOOD_SMART, PhraseValue.SMART));

        phraseDictionary.Add(StringConsts.PHRASE_2_KEY, PhraseList2);

        List<Phrase> PhraseList3 = new List<Phrase>();
        PhraseList3.Add(new Phrase(StringConsts.PHRASE_3, PhraseValue.OPENER));
        PhraseList3.Add(new Phrase(StringConsts.RESP_3_BAD, PhraseValue.BAD));
        PhraseList3.Add(new Phrase(StringConsts.RESP_3_GOOD_TOUGH, PhraseValue.TOUGH));
        PhraseList3.Add(new Phrase(StringConsts.RESP_3_GOOD_SMART, PhraseValue.SMART));

        phraseDictionary.Add(StringConsts.PHRASE_3_KEY, PhraseList3);

        List<Phrase> PhraseList4 = new List<Phrase>();
        PhraseList4.Add(new Phrase(StringConsts.PHRASE_4, PhraseValue.OPENER));
        PhraseList4.Add(new Phrase(StringConsts.RESP_4_BAD, PhraseValue.BAD));
        PhraseList4.Add(new Phrase(StringConsts.RESP_4_GOOD_DIRTY, PhraseValue.DIRTY));
        PhraseList4.Add(new Phrase(StringConsts.RESP_4_GOOD_SMART, PhraseValue.SMART));

        phraseDictionary.Add(StringConsts.PHRASE_4_KEY, PhraseList4);

        Debug.Log("dictionary compiled");

    }

    private void createKeyList()
    {
        m_PhraseKeys = new List<string>();
        m_PhraseKeys.Add(StringConsts.PHRASE_1_KEY);
        m_PhraseKeys.Add(StringConsts.PHRASE_2_KEY);
        m_PhraseKeys.Add(StringConsts.PHRASE_3_KEY);
        m_PhraseKeys.Add(StringConsts.PHRASE_4_KEY);
    }

    private void updateDisplayText()
    {
        displayController.UpdateDisplayText(m_PhraseKeys[activeKeyIndex]);
        displayController.ResetTimer();
    }

    private void updateScore(PhraseValue aPhraseValue)
    {
        int scoreChange = hypeIncrement;
        switch(aPhraseValue)
        {
            case PhraseValue.BAD:
                scoreChange *= hypeBadModifier;
                break;

            case PhraseValue.OPENER:
                // NOTHING HAPPENS
                break;

            case PhraseValue.CHOKE:
                scoreChange *= hypeChokeModifier;
                break;
            
            default:
                if (aPhraseValue == opponentWeaknesses[opponentIndex])
                {
                    scoreChange *= hypeGreatModifier;
                }
                else
                {
                    scoreChange *= hypeGoodModifier;
                }
                break;
        }

        currentHype += scoreChange;
        updateHypeMeter();
    }

    public void ChoiceSelected(PhraseValue aPhraseValue)
    {
        if (activeKeyIndex < m_PhraseKeys.Count)
        {
            updateScore(aPhraseValue);
            activeKeyIndex++;
            if (activeKeyIndex < m_PhraseKeys.Count)
            {
                updateDisplayText();
                //displayController.ResetTimer();
            }
            else
            {
                displayController.StopTimer();
                // battle over
            }
        }
    }
}
