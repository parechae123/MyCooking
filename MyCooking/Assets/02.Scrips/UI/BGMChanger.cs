using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BGMChanger : MonoBehaviour
{
    private TMP_Dropdown thisDropDown;
    
    public void Awake()
    {
        thisDropDown = GetComponent<TMP_Dropdown>();
    }
    public void OnBGMChange()
    {
        SoundManager.SMInstance().ChangeBGM("BGM"+thisDropDown.value.ToString());
    }
}
