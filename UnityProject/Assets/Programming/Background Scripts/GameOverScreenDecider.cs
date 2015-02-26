using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameOverScreenDecider : MonoBehaviour
{
    public GameObject winWholeGameScreen;
    public GameObject winLevelScreen;

    protected void Start()
    {
        bool isLastLevel = LevelLoader.IsLastLevel();
        winWholeGameScreen.SetActive(isLastLevel);
        winLevelScreen.SetActive(!isLastLevel);
    }
}

