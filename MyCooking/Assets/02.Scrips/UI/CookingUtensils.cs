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
    void Start()        //음식 터치했을 때 음식이 완성되게 하기 및 해당 도구가 거치대에 걸어지게 하기
    {
        layerMask = 1 << LayerMask.NameToLayer("Objects");
        fryfan = GetComponent<FryFan>();
        //리스트에서 요리 재료 Data를 가져와서 그 리스트에 있는 것들과 같은 이름을 가진 오브젝트가 이 스크립트를 가진 오브젝트랑 닿았을 때 gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 0.7f, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.7f, layerMask))
        {
            childCount = hitInfo.collider.transform.hierarchyCount - 1; //오브젝트에 든 자식의 갯수
            for (int i = 0; i < childCount; i++)
            {
                Debug.Log("반복문 반응" + hitInfo.collider.name);
                if (hitInfo.collider.transform.GetChild(i).gameObject.layer == 6)       //hitInfo.collider.transform.GetChild(i).gameObject.layer 이건 10진법이고 layerMask 이건 2진법
                {
                    cook.Add(hitInfo.collider.transform.GetChild(i));           //자식오브젝트를 리스트에 넣음
                    Debug.Log("자식 오브젝트 접근 반응");
                    hitInfo.collider.transform.GetChild(i).gameObject.SetActive(false);
                }
            }

            /*for (int i = 0; i < cook.Count - 1; i++)
            {
                if (list[i].name == 자식 오브젝트 이름 && list[i].name == 자식 오브젝트 이름)
                    {
                        해당 위치에 에셋에서 오브젝트를 찾아 Instantiate 하기
                    }
                if (list[i].name == 자식 오브젝트 이름 && list[i].name == 자식 오브젝트 이름)
                    {
                        해당 위치에 에셋에서 오브젝트를 찾아 Instantiate 하기
                    }
                if (list[i].name == 자식 오브젝트 이름 && list[i].name == 자식 오브젝트 이름)
                    {
                        해당 위치에 에셋에서 오브젝트를 찾아 Instantiate 하기
                    }
                if (list[i].name == 자식 오브젝트 이름 && list[i].name == 자식 오브젝트 이름)
                    {
                        해당 위치에 에셋에서 오브젝트를 찾아 Instantiate 하기
                    }
                if (list[i].name == 자식 오브젝트 이름 && list[i].name == 자식 오브젝트 이름)
                    {
                        해당 위치에 에셋에서 오브젝트를 찾아 Instantiate 하기
                    }
            {*/

        }
    }
}

