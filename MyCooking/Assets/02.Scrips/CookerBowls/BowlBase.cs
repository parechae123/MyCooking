using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BowlBase : MonoBehaviour
{
    public LayerMask whatIsInduction;
    public BoxCollider bc;
    protected RaycastHit hit;
    public Induction IDTTem;
    public PlayerControl PC;
    bool isOnInduction;
    public Vector3 Center;
    public Transform FoodPoint;
    private Vector3 originSize;
    public List<GameObject> cook = new List<GameObject>();
    private void Start()
    {
        IDTTem = GameObject.Find("InductionIndex").GetComponent<Induction>();
        bc = GetComponent<BoxCollider>();
        originSize = transform.lossyScale;
    }
    private void Update()
    {
        if (transform.parent == null)
        {
            isOnInduction = false;
            transform.localScale = originSize;
        }
        else
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.3f, whatIsInduction))
            {
                if (!isOnInduction&& hit.collider.gameObject.name != "FryPan"&&hit.collider.gameObject.layer == 8)
                {
                    Debug.Log("1");
                    if (transform.parent.name.Contains("Controller"))
                    {
                        Debug.Log("2");
                        if (PC.objOnRighttHand == transform)
                        {
                            Debug.Log("3");
                            PC.objOnRighttHand = null;
                            this.transform.parent = null;
                        }
                        else if (PC.objOnLeftHand == transform)
                        {
                            Debug.Log("4");
                            PC.objOnRighttHand = null;
                            this.transform.parent = null;
                        }
                    }
                    Debug.Log("5");
                    BoxCollider inductionBC = hit.collider.GetComponent<BoxCollider>();
                    transform.parent = hit.collider.transform;

                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    transform.localScale = new Vector3(1.28844f, 67.06584f, 1.28844f);
                    transform.localPosition = new Vector3(-0.799000025f, 5f, 0);
                    Debug.Log("6");
                    isOnInduction = true;
                }
            }

        }
        if (Physics.SphereCast(FoodPoint.position, 0.05f, FoodPoint.up, out hit, 0.3f, whatIsInduction))
        {
            if (hit.collider.gameObject.name != "FryPan"&&hit.collider.gameObject.layer != 8)
            {
                Debug.Log("음식 인식");
                BowlProperty();
            }
        }

    }
    protected virtual void BowlProperty()
    {
        //추후 유지보수를 원하면 아래에서 구현하는 이 함수에 요리 방식을 이프문으로 넣어준다.
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position+Center,0.2f);
    }
}
