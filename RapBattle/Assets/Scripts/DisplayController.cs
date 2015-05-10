using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisplayController : MonoBehaviour
{
    [SerializeField]
    private GameController m_GameController;

    [SerializeField]
    private ButtonController[] m_ButtonControllers;

    public Text displayText;

    public bool timerActive = false;

    public float maxTimeDefault = 5.0f;
    private float m_Timer = 0.0f;

    private Phrase[] activePhraseChoices;

	// Use this for initialization
	void Start ()
    {
        //displayText.text = StringConsts.PHRASE_1;
        activePhraseChoices = new Phrase[3];
        StartCoroutine(LateStart(1.0f));
	}
	
    IEnumerator LateStart(float aTime)
    {
        yield return new WaitForSeconds(aTime);

        UpdateDisplayText(StringConsts.PHRASE_1_KEY);
    }

    public void UpdateDisplayText(string key)
    {
        List<Phrase> phraseList = m_GameController.phraseDictionary[key];
        displayText.text = phraseList[0].text;

        Phrase[] phraseChoices = { phraseList[1], phraseList[2], phraseList[3] };
        Shuffle(phraseChoices);

        activePhraseChoices = phraseChoices;
        
        for (int i = 0 ; i < activePhraseChoices.Length ; i++)
        {
            m_ButtonControllers[i].SetPhrase(activePhraseChoices[i]);
        }
    }

    public static void Shuffle<T>(T[] array)
    {
        int length = array.Length;
        for (int i = length - 1 ; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i);
            T temp = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = temp;
        }
    }


	// Update is called once per frame
	void Update ()
    {
        #region TEMPORARY TESTING
        if (tempTest)
        {
            tempTest = false;
            ResetTimer();
        }

        if (tempStopTest)
        {
            tempStopTest = false;
            StopTimer();
        }
        #endregion
    }
    #region TEMPORARY TESTING
    public bool tempTest = false;
    public bool tempStopTest = false;
    #endregion

    public void ResetTimer()
    {
        ResetTimer(maxTimeDefault);
    }

    public void ResetTimer(float aTimeLimit)
    {
        timerActive = true;
        StartCoroutine(RunChoiceTimer(aTimeLimit));
    }

    public void StopTimer()
    {
        timerActive = false;
        StopCoroutine("RunChoiceTimer");
    }

    IEnumerator<YieldInstruction> RunChoiceTimer(float aDuration)
    {
        float time = aDuration;
        while (time > 0.0f && timerActive)
        {
            time -= Time.deltaTime;
            UpdateTimerDisplay(time);
            yield return null;
        }
        // this is the ensure we can see the time remaining when the coroutine is stopped
        // early like when a choice was made, otherwise we show 0
        time = timerActive ? 0.0f : time;

        UpdateTimerDisplay(time);
        timerActive = false;
    }

    public void UpdateTimerDisplay(float aTime)
    {
        decimal displayTime = decimal.Round((decimal)aTime, 3);
        displayText.text = displayTime.ToString();  // move to something else, displaytext shows words
    }
}
