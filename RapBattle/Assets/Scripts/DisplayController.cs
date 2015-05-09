using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour
{
    [SerializeField]
    private GameController m_GameController;

    public Button topButton;
    public Button middleButton;
    public Button bottomButton;

    public Text displayText;

	// Use this for initialization
	void Start ()
    {
        displayText.text = StringConsts.PHRASE;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
