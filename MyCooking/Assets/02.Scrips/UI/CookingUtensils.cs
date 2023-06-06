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
    void Start()        //음식 터치했을 때 음식이 완성되게 하기 및 해당 도구가 거치대에 걸어지게 하기
    {
        fryfan = GetComponent<FryFan>();
        layerMask = 1 << LayerMask.NameToLayer("Objects");
        //리스트에서 요리 재료 Data를 가져와서 그 리스트에 있는 것들과 같은 이름을 가진 오브젝트가 이 스크립트를 가진 오브젝트랑 닿았을 때 gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        me = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);

        Debug.DrawRay(transform.position, transform.forward * 0.7f, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.7f, layerMask))
        {
            Debug.Log("반응");
            if (hitInfo.collider.transform.childCount > 0)
            {
                GameObject childObject = hitInfo.collider.transform.GetChild(0).gameObject;
                if (childObject.layer == layerMask)
                {
                    Debug.Log("반응2");
                    childObject.SetActive(false);
                    Debug.Log("반응3");
                }
            }

            if (hitInfo.collider.transform.childCount > 1)
            {
                Debug.Log("반응2");
                GameObject childObject = hitInfo.collider.transform.GetChild(1).gameObject;
                if (childObject.layer == layerMask)
                {
                    childObject.SetActive(false);
                    Debug.Log("반응3");
                }
            }

            if (hitInfo.collider.transform.childCount > 2)
            {
                GameObject childObject = hitInfo.collider.transform.GetChild(2).gameObject;
                if (childObject.layer == layerMask)
                {
                    childObject.SetActive(false);
                    Debug.Log("반응3");
                }
            }

            Debug.Log("반응" + hitInfo.transform.name);
        }
    }
}