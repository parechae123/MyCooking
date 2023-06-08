using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FryFan : BowlBase
{
    protected override void BowlProperty()
    {
        if (IDTTem.timeTem > 80)
        {
            SoundManager.SMInstance().ChangeSFX("FrySound");
            //���⿡ �丮���� ��ũ��Ʈ ������ �ɵ�
            Debug.Log("�丮 ���� �Լ� ������ �ǰԶ�");
        }
        hit.collider.GetComponent<BoxCollider>().enabled = false;
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
            Debug.Log("�����ٵ� ����");
        }

        hit.collider.transform.parent = null;
        hit.collider.transform.parent = transform;
        hit.collider.transform.rotation = Quaternion.identity;
        hit.collider.transform.localPosition = FoodPoint.localPosition;
        if (hit.collider.transform.Find("Bowl"))
        {
            hit.collider.transform.Find("Bowl").gameObject.SetActive(false);
            //����Ķ��� ����ó�� �ʿ�
        }
    }
}
