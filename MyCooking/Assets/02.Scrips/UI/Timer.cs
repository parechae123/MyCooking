using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GMinstatnce().timerUI = GetComponent<Image>();
    }
}
