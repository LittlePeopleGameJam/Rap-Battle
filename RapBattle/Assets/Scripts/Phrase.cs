using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PhraseValue
{
    OPENER,
    BAD,
    TOUGH,
    DIRTY,
    SMART
}


public class Phrase
{
    public string text;
    public PhraseValue value;

    public Phrase(string aText, PhraseValue aValue)
    {
        text = aText;
        value = aValue;
    }
}

