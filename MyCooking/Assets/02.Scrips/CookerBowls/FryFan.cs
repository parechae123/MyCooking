using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FryFan : BowlBase
{
    protected override void BowlProperty()
    {
        if (GameManager.GMinstatnce().ingredientCookIndex.Count != 0)
        {
            if (GameManager.GMinstatnce().ingredientCookIndex.Peek().name == hit.collider.gameObject.name)
            {
                if (IDTTem.timeTem > 80)
                {
                    SoundManager.SMInstance().ChangeSFX("FrySound");
                    //여기에 요리관련 스크립트 넣으면 될듯
                    Debug.Log("요리 관련 함수 넣으면 되게땅");
                }
                hit.collider.GetComponent<BoxCollider>().enabled = false;
                GameManager.GMinstatnce().ingredientCookIndex.Dequeue();
                if (hit.collider.transform == PC.objOnLeftHand)
                {
                    PC.objOnLeftHand = null;
                }
                else if (hit.collider.transform == PC.objOnRighttHand)
                {
                    PC.objOnRighttHand = null;
                }
                if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody targetRB))
                {
                    targetRB.constraints = RigidbodyConstraints.FreezeAll;
                    targetRB.useGravity = false;
                    Debug.Log("리짓바디 얼어라");
                }

                hit.collider.transform.parent = null;
                hit.collider.transform.parent = transform;
                hit.collider.transform.rotation = Quaternion.identity;
                hit.collider.transform.localPosition = FoodPoint.localPosition;
                if (hit.collider.transform.Find("Bowl"))
                {
                    hit.collider.transform.Find("Bowl").gameObject.SetActive(false);
                }
            }
        }
    }
}
