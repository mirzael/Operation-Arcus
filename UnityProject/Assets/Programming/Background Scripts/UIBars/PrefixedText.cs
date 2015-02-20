using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class PrefixedText : MonoBehaviour
{
    protected Text text;
    public string prefix;

    protected void Start()
    {
        text = gameObject.GetComponent<Text>();
        if(prefix==null || prefix=="")
        {
            prefix = text.text;
        }
    }

    public void UpdateText(string newText)
    {
        text.text = prefix + newText;
    }
}
