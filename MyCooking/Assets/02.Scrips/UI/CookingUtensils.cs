using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingUtensils : MonoBehaviour
{
    public int layerMask;
    public List<GameObject> cook = new List<GameObject>();
    public FryFan fryfan;
    public int hitnum = 0;
    // Start is called before the first frame update
    void Start()        //���� ��ġ���� �� ������ �ϼ��ǰ� �ϱ� �� �ش� ������ ��ġ�뿡 �ɾ����� �ϱ�
    {
        fryfan = GetComponent<FryFan>();
        layerMask = 1 << LayerMask.NameToLayer("Objects");
        //����Ʈ���� �丮 ��� Data�� �����ͼ� �� ����Ʈ�� �ִ� �͵�� ���� �̸��� ���� ������Ʈ�� �� ��ũ��Ʈ�� ���� ������Ʈ�� ����� �� gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*cook.Add(GameManager.GMinstatnce().ingredientCookIndex.Dequeue());              //ť���� ���� ������Ʈ�� ����Ʈ�� �־��ش�.*/
        Debug.DrawRay(transform.position, transform.forward * 0.5f, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(this.transform.position, transform.forward, out hitInfo, 0.7f, layerMask))
        {
            if (hitInfo.collider.transform.childCount > 0)
            {
                if (hitInfo.collider.transform.GetChild(0).gameObject.layer == layerMask)
                {
                    hitInfo.collider.transform.GetChild(0).gameObject.SetActive(false);
                    Debug.Log("����2");
                }
            }

            if (hitInfo.collider.transform.childCount > 1)
            {
                if (hitInfo.collider.transform.GetChild(1).gameObject.layer == layerMask)
                {
                    hitInfo.collider.transform.GetChild(1).gameObject.SetActive(false);
                    Debug.Log("����2");
                }
            }

            if (hitInfo.collider.transform.childCount > 2)
            {
                if (hitInfo.collider.transform.GetChild(2).gameObject.layer == layerMask)
                {
                    hitInfo.collider.transform.GetChild(2).gameObject.SetActive(false);
                    Debug.Log("����2");
                }
            }

            Debug.Log("����");
        }
    }
}