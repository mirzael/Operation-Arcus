using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    protected void Start()
    {
        Slider slider = gameObject.GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnVolumeChange);
        slider.value = AudioListener.volume;
    }

    public void OnVolumeChange(float volume)
    {
        AudioListener.volume = volume;
    }
}

