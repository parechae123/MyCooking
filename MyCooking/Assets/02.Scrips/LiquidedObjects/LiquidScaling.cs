using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LiquidScaling : MonoBehaviour
{
    public float length;
    // Start is called before the first frame update
    private void OnEnable()
    {
        transform.DOScaleY(length*3, length*2);
    }
    private void OnDisable()
    {
        DOTween.Clear();
        transform.localScale = new Vector3(1, 0, 1);
    }
}
