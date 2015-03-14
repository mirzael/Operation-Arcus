using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SubmitScore : MenuAction
{
    public HiscoreTextbox hiScore;

    public void TakeAction()
    {
        hiScore.SubmitHiScore();
    }
}

