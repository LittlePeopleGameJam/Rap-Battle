using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button button;

    private Phrase m_Phrase;
    private Text m_ButtonText;
	
    // Use this for initialization
	void Start ()
    {
	    if (button != null)
        {
            m_ButtonText = button.GetComponentInChildren<Text>();
        }
        else
        {
            Debug.Log("no button assigned! abort!");
        }
	}
	
    public void SetFont (Font aFont)
    {
        m_ButtonText.font = aFont;
    }

    public void SetFontSize(int aSize)
    {
        m_ButtonText.fontSize = aSize;
    }

    public void SetFont(Font aFont, int aSize)
    {
        m_ButtonText.font = aFont;
        m_ButtonText.fontSize = aSize;
    }

    public void SetPhrase(Phrase aPhrase)
    {
        m_Phrase = aPhrase;
        m_ButtonText.text = m_Phrase.text;
    }

    public Phrase GetPhrase()
    {
        return m_Phrase;
    }
}
