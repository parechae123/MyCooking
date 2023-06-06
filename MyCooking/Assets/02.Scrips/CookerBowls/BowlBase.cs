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
            if (Physics.Raycast(FoodPoint.position, Vector3.down, out hit, 0.3f, whatIsInduction))
            {
                if (!isOnInduction)
                {
                    isOnInduction = true;
                    if (transform.parent.name.Contains("Controller"))
                    {
                        if (PC.objOnRighttHand == transform)
                        {
                            PC.objOnRighttHand = null;
                            this.transform.parent = null;
                        }
                        else if (PC.objOnLeftHand == transform)
                        {
                            PC.objOnRighttHand = null;
                            this.transform.parent = null;
                        }
                    }
                    BoxCollider inductionBC = hit.collider.GetComponent<BoxCollider>();
                    transform.parent = hit.collider.transform;

                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    transform.localScale = new Vector3(1.288439f, 67.06584f, 1.288439f);
                    transform.localPosition = new Vector3(-0.867f, 8.3f, 0f);
                }
            }

        }
        if (Physics.SphereCast(FoodPoint.position, 0.05f, FoodPoint.up, out hit, 0.3f, whatIsInduction))
        {
            BowlProperty();
        }

    }
    protected virtual void BowlProperty()
    {
        //���� ���������� ���ϸ� �Ʒ����� �����ϴ� �� �Լ��� �丮 ����� ���������� �־��ش�.
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position+Center,0.2f);
    }
}
