using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingUtensils : MonoBehaviour
{
    public int layerMask;
    public List<Transform> cook = new List<Transform>();
    public FryFan fryfan;
    public int hitnum = 0;
    public int childCount;
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
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.7f, layerMask))
        {
            childCount = hitInfo.collider.transform.hierarchyCount - 1; //������Ʈ�� �� �ڽ��� ����
            for (int i = 0; i < childCount; i++)
            {
                Debug.Log("�ݺ��� ����" + hitInfo.collider.name);
                if (hitInfo.collider.transform.GetChild(i).gameObject.layer == 6)       //hitInfo.collider.transform.GetChild(i).gameObject.layer �̰� 10�����̰� layerMask �̰� 2����
                {
                    cook.Add(hitInfo.collider.transform.GetChild(i));           //�ڽĿ�����Ʈ�� ����Ʈ�� ����
                    Debug.Log("�ڽ� ������Ʈ ���� ����");
                    hitInfo.collider.transform.GetChild(i).gameObject.SetActive(false);
                }
            }

            /*for (int i = 0; i < cook.Count - 1; i++)
            {
                if (list[i].name == �ڽ� ������Ʈ �̸� && list[i].name == �ڽ� ������Ʈ �̸�)
                    {
                        �ش� ��ġ�� ���¿��� ������Ʈ�� ã�� Instantiate �ϱ�
                    }
                if (list[i].name == �ڽ� ������Ʈ �̸� && list[i].name == �ڽ� ������Ʈ �̸�)
                    {
                        �ش� ��ġ�� ���¿��� ������Ʈ�� ã�� Instantiate �ϱ�
                    }
                if (list[i].name == �ڽ� ������Ʈ �̸� && list[i].name == �ڽ� ������Ʈ �̸�)
                    {
                        �ش� ��ġ�� ���¿��� ������Ʈ�� ã�� Instantiate �ϱ�
                    }
                if (list[i].name == �ڽ� ������Ʈ �̸� && list[i].name == �ڽ� ������Ʈ �̸�)
                    {
                        �ش� ��ġ�� ���¿��� ������Ʈ�� ã�� Instantiate �ϱ�
                    }
                if (list[i].name == �ڽ� ������Ʈ �̸� && list[i].name == �ڽ� ������Ʈ �̸�)
                    {
                        �ش� ��ġ�� ���¿��� ������Ʈ�� ã�� Instantiate �ϱ�
                    }
            {*/

        }
    }
}

