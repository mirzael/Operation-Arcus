using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Manage functionality common to both multiplayer and singleplayer UI events
/// </summary>
public class UIEvents : Singleton<UIEvents>
{
    private Dictionary<Color, AudioClip> soundForColorReady;
    private Dictionary<Color, List<Action>> colorReadyAction = new Dictionary<Color,List<Action>>();

    public AudioClip orangeReadySound;
    public AudioClip greenReadySound;
    public AudioClip purpleReadySound;

    public Color Orange;
    public Color Purple;
    public Color Green;

    protected void Awake()
    {
        soundForColorReady = new Dictionary<Color, AudioClip>();
        soundForColorReady[Orange] = orangeReadySound;
        soundForColorReady[Green] = greenReadySound;
        soundForColorReady[Purple] = purpleReadySound;

        colorReadyAction[Orange] = new List<Action>();
        colorReadyAction[Purple] = new List<Action>();
        colorReadyAction[Green] = new List<Action>();
    }

    public void AddActionOnColorActivation(Color color, Action action)
    {
        colorReadyAction[color].Add(action);
    }

    public void MakeSecondaryReady(Color color)
    {
        audio.PlayOneShot(soundForColorReady[color]);
        foreach(Action action in colorReadyAction[color])
        {
            action();
        }
    }
}

