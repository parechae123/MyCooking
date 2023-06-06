using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideLineChecker : MonoBehaviour
{
    private Toggle thisTG;
    private void Start()
    {
        thisTG = GetComponent<Toggle>();
    }
    public void OnGuideChecker()
    {
        if(thisTG.isOn == true)
        {
            GameManager.GMinstatnce().isGuideLineEnabled = true;
        }
        else
        {
            GameManager.GMinstatnce().isGuideLineEnabled = false;
        }
    }
}
