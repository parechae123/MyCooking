using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingUtensils : MonoBehaviour
{
    public int layerMask;
    public List<GameObject> cook = new List<GameObject>();
    public FryFan fryfan;
    public int hitnum = 0;
    public Vector3 me;
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
        me = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);

        Debug.DrawRay(transform.position, transform.forward * 0.7f, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.7f, layerMask))
        {
            Debug.Log("����");
            if (hitInfo.collider.transform.childCount > 0)
            {
                GameObject childObject = hitInfo.collider.transform.GetChild(0).gameObject;
                if (childObject.layer == layerMask)
                {
                    Debug.Log("����2");
                    childObject.SetActive(false);
                    Debug.Log("����3");
                }
            }

            if (hitInfo.collider.transform.childCount > 1)
            {
                Debug.Log("����2");
                GameObject childObject = hitInfo.collider.transform.GetChild(1).gameObject;
                if (childObject.layer == layerMask)
                {
                    childObject.SetActive(false);
                    Debug.Log("����3");
                }
            }

            if (hitInfo.collider.transform.childCount > 2)
            {
                GameObject childObject = hitInfo.collider.transform.GetChild(2).gameObject;
                if (childObject.layer == layerMask)
                {
                    childObject.SetActive(false);
                    Debug.Log("����3");
                }
            }

            Debug.Log("����" + hitInfo.transform.name);
        }
    }
}