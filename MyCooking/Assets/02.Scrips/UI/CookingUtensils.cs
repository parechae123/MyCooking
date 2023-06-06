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
    void Start()        //음식 터치했을 때 음식이 완성되게 하기 및 해당 도구가 거치대에 걸어지게 하기
    {
        fryfan = GetComponent<FryFan>();
        layerMask = 1 << LayerMask.NameToLayer("Objects");
        //리스트에서 요리 재료 Data를 가져와서 그 리스트에 있는 것들과 같은 이름을 가진 오브젝트가 이 스크립트를 가진 오브젝트랑 닿았을 때 gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*cook.Add(GameManager.GMinstatnce().ingredientCookIndex.Dequeue());              //큐에서 빼낸 오브젝트를 리스트에 넣어준다.*/
        Debug.DrawRay(transform.position, transform.forward * 0.5f, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(this.transform.position, transform.forward, out hitInfo, 0.7f, layerMask))
        {
            if (hitInfo.collider.transform.childCount > 0)
            {
                if (hitInfo.collider.transform.GetChild(0).gameObject.layer == layerMask)
                {
                    hitInfo.collider.transform.GetChild(0).gameObject.SetActive(false);
                    Debug.Log("반응2");
                }
            }

            if (hitInfo.collider.transform.childCount > 1)
            {
                if (hitInfo.collider.transform.GetChild(1).gameObject.layer == layerMask)
                {
                    hitInfo.collider.transform.GetChild(1).gameObject.SetActive(false);
                    Debug.Log("반응2");
                }
            }

            if (hitInfo.collider.transform.childCount > 2)
            {
                if (hitInfo.collider.transform.GetChild(2).gameObject.layer == layerMask)
                {
                    hitInfo.collider.transform.GetChild(2).gameObject.SetActive(false);
                    Debug.Log("반응2");
                }
            }

            Debug.Log("반응");
        }
    }
}