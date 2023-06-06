using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSliders : MonoBehaviour
{
    public bool isThisBGMSlider;
    Slider thisSlider;
    private void Start()
    {
        thisSlider = GetComponent<Slider>();
    }
    public void OnSliderValue()
    {
        if (isThisBGMSlider)
        {
            SoundManager.SMInstance().AS["BGM"].volume = thisSlider.value;
        }
        else
        {
            SoundManager.SMInstance().AS["SFX"].volume = thisSlider.value;
        }
    }
}
