using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Induction : MonoBehaviour
{
    float timer = 0;
    public float tempSpeed;
    public float timeTem;
    private float test = 0;
    public int speed;                           //온도가 올라가는 속도를 증가 시키기 위해 선언
    public int fireLevel = 0;                       //숫자가 올라가면 인덕션 온도 단계도 같이 올라감
    public TextMeshProUGUI textInduction;           //인덕션 단계를 텍스트
    public Material inductionMAT;                   //인덕션 단계를 색깔
    public float inductionLayer;                    //인덕션 온도 단계
    public TextMeshProUGUI inductionDegrees;        //온도 표시
    public bool isInductionON;
    // Start is called before the first frame update        //인덕션에 있는 숫자를 fireLevel의 값으로 표시하기 위한 테스트용
    void Start()
    {
        textInduction = GetComponent<TextMeshProUGUI>();
        inductionMAT.color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame

    public void Up()
    {
        if (fireLevel <= 4)
        {
            fireLevel += 1;
            speed = 1;
        }
        SoundManager.SMInstance().ChangeSFX("BTNClickSound");
        inductionLayer = fireLevel * 0.2f;
        textInduction.text = fireLevel.ToString();
        Debug.Log("단계: " + fireLevel);
        tempSpeed = 1;
        /*StopCoroutine(TemDown());
        StartCoroutine(TemUp());*/


    }
    public void Down()
    {
        if (fireLevel >= 1)
        {
            fireLevel -= 1;
            speed = -1;
        }
        SoundManager.SMInstance().ChangeSFX("BTNClickSound");
        inductionLayer = fireLevel * 0.2f;
        textInduction.text = fireLevel.ToString();
        Debug.Log("단계: " + fireLevel);
        tempSpeed = 1;
        /*StopCoroutine(TemUp());
        
        StartCoroutine(TemDown());*/


    }
    /*IEnumerator TemUp()
    {
        if (!isInductionON)
        {
            isInductionON = true;
            Debug.Log("코루틴 실행UP");
            while (timeTem <= 150)
            {
                tempSpeed += 0.005f;
                Debug.Log(timeTem = Mathf.SmoothStep(timeTem, 30f * fireLevel, tempSpeed));
                Mathf.Clamp(timeTem, 0, 30 * fireLevel);
                yield return new WaitForSeconds(0.05f);

                if (timeTem >= fireLevel * 30)
                {
                    isInductionON = false;
                    tempSpeed = 0;
                    break;
                }

            }
            isInductionON = false;
        }
        
    }
    IEnumerator TemDown()
    {
        if (!isInductionON)
        {
            isInductionON = true;
            Debug.Log("코루틴 실행Down");
            while (timeTem >= 0)
            {
                tempSpeed += 0.005f;
                Debug.Log(timeTem = Mathf.SmoothStep(timeTem, 30f * fireLevel, tempSpeed));
                yield return new WaitForSeconds(0.05f);
                inductionMAT.color = new Color(timeTem / 150, 0, 0, 1);
                if (timeTem == fireLevel * 30*//* && timeTem / fireLevel > 30*//*)
                {
                    isInductionON = false;
                    tempSpeed = 0;
                    break;
                }

            }
            isInductionON = false;
        }
    }*/
    private void FixedUpdate()
    {

        Mathf.Clamp(tempSpeed, 0, 1);
        timer += Time.fixedDeltaTime;
        if(tempSpeed != 0 && timer >= 0.1f)
        {
            tempSpeed -= 0.01f;
            Debug.Log(timeTem = Mathf.SmoothStep( 30f * fireLevel,timeTem, tempSpeed));
            timer = 0;
            inductionMAT.color = new Color(timeTem / 150, 0, 0, 1);
            if (fireLevel * 30 == timeTem)
            {
                tempSpeed = 0;
            }
        }
    }




    //온도를 올렸다가 내리면 같은 온도 단계라도 중간에서부터 더 해져 기존과 다른 속도로 올라간다 EX) 0~30도까지는 10초라 했을 때 중간에 온도를 내려 15도로 떨어진 후 다시 온도를 올리면 15~45도까지 10초가 걸린다

}
