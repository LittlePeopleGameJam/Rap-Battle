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
	
	}
}
