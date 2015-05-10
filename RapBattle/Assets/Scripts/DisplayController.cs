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

    public Font textFont;

    private IEnumerator m_TimerCoroutine;

    public Text displayText;
    public Text timeDisplay;

    public bool timerActive = false;

    public float maxTimeDefault = 5.0f;

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

        //UpdateDisplayText(StringConsts.PHRASE_1_KEY);
        displayText.font = textFont;
        timeDisplay.font = textFont;

        for (int i = 0 ; i < m_ButtonControllers.Length ; i++)
        {
            m_ButtonControllers[i].SetFont(textFont);
        }
    }

    public void UpdateDisplayText(string key)
    {
        List<Phrase> phraseList = new List<Phrase>();
        
        if (m_GameController.phraseDictionary.TryGetValue(key, out phraseList))
        {
            Debug.Log("Key found: " + key);
            Debug.Log(phraseList.Count + " phraseList count");
        }
        else
        {
            Debug.Log("Key not found: " + key);
            Debug.Log(phraseList.Count + " phraseList count");
            return;
        }

        displayText.text = phraseList[0].text;

        for (int j = 0; j < activePhraseChoices.Length; j++)
        {
            activePhraseChoices[j] = phraseList[j + 1];
        }
        Shuffle(activePhraseChoices);

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

    public void ResetTimer()
    {
        ResetTimer(maxTimeDefault);
    }

    public void ResetTimer(float aTimeLimit)
    {
        //StopTimer();
        timerActive = true;
        m_TimerCoroutine = RunChoiceTimer(aTimeLimit);
        StartCoroutine(m_TimerCoroutine);
        //StartCoroutine(RunChoiceTimer(aTimeLimit));
    }

    public void StopTimer()
    {
        timerActive = false;
        //StopCoroutine("RunChoiceTimer");
        if (m_TimerCoroutine != null) { StopCoroutine(m_TimerCoroutine); }
    }

    IEnumerator<YieldInstruction> RunChoiceTimer(float aDuration)
    {
        timerActive = true;
        float time = aDuration;
        while (time > 0.0f && timerActive)
        {
            time -= Time.deltaTime;
            UpdateTimerDisplay(time);
            yield return null;
        }
        // this is the ensure we can see the time remaining when the coroutine is stopped
        // early like when a choice was made, otherwise we show 0
        if (timerActive)
        {
            time = 0.0f;
            UpdateTimerDisplay(time);
            TimeExpired();
        }
        else
        {
            UpdateTimerDisplay(time);
            timerActive = false;
        }
    }

    private void TimeExpired()
    {
        StopTimer();
        m_GameController.ChoiceSelected(PhraseValue.CHOKE);
    }

    public void UpdateTimerDisplay(float aTime)
    {
        decimal displayTime = decimal.Round((decimal)aTime, 3);
        timeDisplay.text = displayTime.ToString();
    }
}
