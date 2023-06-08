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
        if (Physics.Raycast(transform.position, transform.forward, 0.7f, layerMask))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.7f, layerMask) && hitInfo.collider.name == "FryPan")
            {
                Debug.Log("if 반응" + hitInfo.collider.transform.childCount);
                for (int i = 0; i < hitInfo.collider.transform.childCount; i++)
                {
                    if (hitInfo.collider.transform.GetChild(i).gameObject.layer == 6)       //hitInfo.collider.transform.GetChild(i).gameObject.layer 이건 10진법이고 layerMask 이건 2진법
                    {
                        cook.Add(hitInfo.collider.transform.GetChild(i).name);           //자식오브젝트를 리스트에 넣음
                        Debug.Log("자식 오브젝트 접근 반응");
                        Destroy(hitInfo.collider.transform.GetChild(i).gameObject);
                    }
                }                                                                   //여기 이름은 나중에 대체할 수도 있음
                Debug.Log("리스트 크기: "+ cook.Count);
                if (cook.Contains("Kimchi") && cook.Contains("Rice"))               //김치볶음밥 생성
                {
                    cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/KimchiFriedRice"));
                    cookingCompletion.transform.position = hitInfo.transform.position;
                    cook.Clear();
                }
                if (cook.Contains("Scrambledeggs") && cook.Contains("Rice"))               //간장계란비빔밥
                {
                    cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/SoySauceAndEggBibimbap"));
                    cookingCompletion.transform.position = hitInfo.transform.position;
                    cook.Clear();
                }
                if (cook.Contains("Friedegg"))                                      //스크럼블에그
                {
                    cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Scrambledeggs"));
                    cookingCompletion.transform.parent = foodPoint;
                    cook.Clear();
                }
                if (cook.Contains("Spam"))                                      //스팸
                {
                    cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/PieceOfSpam"));
                    cookingCompletion.transform.position = hitInfo.transform.position;
                    cook.Clear();
                }

            }

        }
    }
}


