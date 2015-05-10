using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisplayController : MonoBehaviour
{
    [SerializeField]
    private GameController m_GameController;

    public Button topButton;
    public Button middleButton;
    public Button bottomButton;

    public Text displayText;

    public bool timerActive = false;

    public float maxTimeDefault = 5.0f;
    private float m_Timer = 0.0f;

	// Use this for initialization
	void Start ()
    {
        //displayText.text = StringConsts.PHRASE_1;

        StartCoroutine(LateStart(1.0f));
	}
	
    IEnumerator LateStart(float aTime)
    {
        yield return new WaitForSeconds(aTime);

        List<Phrase> temp = m_GameController.phraseDictionary[StringConsts.PHRASE_1_KEY];
        displayText.text = temp[0].text;
        
    }

	// Update is called once per frame
	void Update ()
    {
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
	}
    public bool tempTest = false;
    public bool tempStopTest = false;

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
        displayText.text = displayTime.ToString();
    }
}
