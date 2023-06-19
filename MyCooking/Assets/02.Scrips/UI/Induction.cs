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
    public int speed;                           //�µ��� �ö󰡴� �ӵ��� ���� ��Ű�� ���� ����
    public int fireLevel = 0;                       //���ڰ� �ö󰡸� �δ��� �µ� �ܰ赵 ���� �ö�
    public TextMeshProUGUI textInduction;           //�δ��� �ܰ踦 �ؽ�Ʈ
    public Material inductionMAT;                   //�δ��� �ܰ踦 ����
    public float inductionLayer;                    //�δ��� �µ� �ܰ�
    public TextMeshProUGUI inductionDegrees;        //�µ� ǥ��
    public bool isInductionON;
    // Start is called before the first frame update        //�δ��ǿ� �ִ� ���ڸ� fireLevel�� ������ ǥ���ϱ� ���� �׽�Ʈ��
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
        Debug.Log("�ܰ�: " + fireLevel);
        tempSpeed = 1;
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
        Debug.Log("�ܰ�: " + fireLevel);
        tempSpeed = 1;
    }
    
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




    //�µ��� �÷ȴٰ� ������ ���� �µ� �ܰ�� �߰��������� �� ���� ������ �ٸ� �ӵ��� �ö󰣴� EX) 0~30�������� 10�ʶ� ���� �� �߰��� �µ��� ���� 15���� ������ �� �ٽ� �µ��� �ø��� 15~45������ 10�ʰ� �ɸ���

}
