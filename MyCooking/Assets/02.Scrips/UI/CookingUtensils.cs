using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingUtensils : MonoBehaviour
{
    public int layerMask;
    public List<string> cook = new List<string>();
    public FryFan fryfan;
    public int hitnum = 0;
    private GameObject cookingCompletion;
    public Transform foodPoint;
    // Start is called before the first frame update
    void Start()        //���� ��ġ���� �� ������ �ϼ��ǰ� �ϱ� �� �ش� ������ ��ġ�뿡 �ɾ����� �ϱ�
    {
        layerMask = 1 << LayerMask.NameToLayer("Objects");
        fryfan = GetComponent<FryFan>();
        //����Ʈ���� �丮 ��� Data�� �����ͼ� �� ����Ʈ�� �ִ� �͵�� ���� �̸��� ���� ������Ʈ�� �� ��ũ��Ʈ�� ���� ������Ʈ�� ����� �� gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, transform.forward * 0.7f, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, 0.7f, layerMask))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.7f, layerMask) && hitInfo.collider.name == "FryPan")
            {
                Debug.Log("if ����" + hitInfo.collider.transform.childCount);
                for (int i = 0; i < hitInfo.collider.transform.childCount; i++)
                {
                    if (hitInfo.collider.transform.GetChild(i).gameObject.layer == 6)       //hitInfo.collider.transform.GetChild(i).gameObject.layer �̰� 10�����̰� layerMask �̰� 2����
                    {
                        cook.Add(hitInfo.collider.transform.GetChild(i).name);           //�ڽĿ�����Ʈ�� ����Ʈ�� ����
                        Debug.Log("�ڽ� ������Ʈ ���� ����");
                        Destroy(hitInfo.collider.transform.GetChild(i).gameObject);
                    }
                }                                                                   //���� �̸��� ���߿� ��ü�� ���� ����
                Debug.Log("����Ʈ ũ��: "+ cook.Count);
                if (cook.Contains("Kimchi") && cook.Contains("Rice"))               //��ġ������ ����
                {
                    cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/KimchiFriedRice"));
                    cookingCompletion.transform.position = hitInfo.transform.position;
                    cook.Clear();
                }
                if (cook.Contains("Scrambledeggs") && cook.Contains("Rice"))               //�����������
                {
                    cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/SoySauceAndEggBibimbap"));
                    cookingCompletion.transform.position = hitInfo.transform.position;
                    cook.Clear();
                }
                if (cook.Contains("Friedegg"))                                      //��ũ������
                {
                    cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Scrambledeggs"));
                    cookingCompletion.transform.parent = foodPoint;
                    cook.Clear();
                }
                if (cook.Contains("Spam"))                                      //����
                {
                    cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/PieceOfSpam"));
                    cookingCompletion.transform.position = hitInfo.transform.position;
                    cook.Clear();
                }

            }

        }
    }
}


