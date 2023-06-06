using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    #region 변수
    private Transform leftHandTR;
    private Transform rightHandTR;
    public Transform objOnLeftHand;
    public Transform objOnRighttHand;
    public LayerMask OBJLayers;
    #endregion
    private void Awake()
    {
        leftHandTR = GameObject.Find("LeftHand Controller").GetComponent<Transform>();
        rightHandTR = GameObject.Find("RightHand Controller").GetComponent<Transform>();
    }
    #region VR 검지손가락 인풋관리
    public void OnRightTriggerPress(InputAction.CallbackContext ctx)
    {

    }
    public void OnLeftTriggerPress(InputAction.CallbackContext ctx)
    {

    }
    private void SenseObjects(Transform handTR)
    {
        RaycastHit Hit;
        if (Physics.Raycast(handTR.position, handTR.forward, out Hit, 0.3f, OBJLayers, QueryTriggerInteraction.UseGlobal))
        {

            Debug.Log("레이케스터");
            if (Hit.collider.gameObject.layer == 7)
            {
                GameObject instTemp = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/" + Hit.transform.name.Replace(" Installer", "")), handTR);
                float instExtend = instTemp.GetComponent<BoxCollider>().bounds.extents.z / 2;
                instTemp.transform.localPosition = Vector3.forward * instExtend;
                instTemp.transform.localRotation = Quaternion.identity;
                if (instTemp.gameObject.TryGetComponent<Rigidbody>(out Rigidbody targetRB))
                {
                    targetRB.useGravity = false;
                    targetRB.constraints = RigidbodyConstraints.FreezeAll;
                }
                if (handTR == leftHandTR)
                {
                    objOnLeftHand = instTemp.transform;
                }
                else if (handTR == rightHandTR)
                {
                    objOnRighttHand = instTemp.transform;
                }
            }
            else if (Hit.collider.gameObject.layer == 6)
            {
                Debug.Log("레이어 64");
                GameObject OBJTemp = Hit.collider.gameObject;
                OBJTemp.transform.SetParent(handTR);
                float objExtend = OBJTemp.GetComponent<BoxCollider>().bounds.extents.z / 2;
                OBJTemp.transform.localPosition = Vector3.forward * objExtend;
                OBJTemp.transform.localRotation = Quaternion.identity;
                if (OBJTemp.gameObject.TryGetComponent<Rigidbody>(out Rigidbody targetRB))
                {
                    targetRB.useGravity = false;
                    targetRB.constraints = RigidbodyConstraints.FreezeAll;
                }
                if (handTR == leftHandTR)
                {
                    objOnLeftHand = OBJTemp.transform;
                }
                else if (handTR == rightHandTR)
                {
                    objOnRighttHand = OBJTemp.transform;
                }
            }
            else if (Hit.collider.gameObject.layer == 9)
            {
                GameObject objTemp = Hit.collider.gameObject;
                if (handTR == leftHandTR)
                {
                    StartCoroutine(FreazerRotations(objTemp,leftHandTR.transform));
                    
                }
                else if (handTR == rightHandTR)
                {
                    StartCoroutine(FreazerRotations(objTemp, rightHandTR.transform));
                }
            }
            if (objOnLeftHand != null)
            {
                if (objOnLeftHand.name.Contains("(Clone)"))
                {
                    objOnLeftHand.name = objOnLeftHand.name.Replace("(Clone)", "");
                }
                GameManager.GMinstatnce().InteractIngredientItems(objOnLeftHand.gameObject);
            }
            else if (objOnRighttHand != null)
            {
                if (objOnRighttHand.name.Contains("(Clone)"))
                {
                    objOnRighttHand.name = objOnRighttHand.name.Replace("(Clone)", "");
                    Debug.Log("앙");
                }
                GameManager.GMinstatnce().InteractIngredientItems(objOnRighttHand.gameObject);
            }
        }
    }
    IEnumerator FreazerRotations(GameObject target,Transform handDir)
    {
        Vector2 lastHandPosition = new Vector2(handDir.transform.position.x, handDir.transform.position.z);
        float disFromOrigin = 0;
        while (RightTriggerOn||LeftTriggerOn)
        {
            yield return null;
            disFromOrigin = Vector2.Distance(lastHandPosition, new Vector2(handDir.position.x, handDir.position.z));
            Mathf.Clamp(disFromOrigin, 0, -110);
            target.transform.rotation = Quaternion.Euler(new Vector3(0, -disFromOrigin * 220, 0));
        }
    }
    private void CancelGrapping(Transform handTR)
    {
        if (objOnLeftHand != null && handTR == leftHandTR)
        {
            if (objOnLeftHand.gameObject.TryGetComponent<Rigidbody>(out Rigidbody targetRB))
            {
                targetRB.useGravity = true;
                targetRB.constraints = RigidbodyConstraints.None;
            }
            objOnLeftHand.parent = null;
            objOnLeftHand = null;

        }
        else if (objOnRighttHand != null && handTR == rightHandTR)
        {
            if (objOnRighttHand.gameObject.TryGetComponent<Rigidbody>(out Rigidbody targetRB))
            {
                targetRB.useGravity = true;
                targetRB.constraints = RigidbodyConstraints.None;
            }
            objOnRighttHand.parent = null;
            objOnRighttHand = null;
        }
    }
    #endregion
    #region
    private bool RightTriggerOn = false;

    public void RightMiddleFinger(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            RightTriggerOn = true;
            SenseObjects(rightHandTR);
        }
        if (ctx.canceled)
        {
            RightTriggerOn = false;
            CancelGrapping(rightHandTR);

        }
    }
    private bool LeftTriggerOn = false;
    public void LeftMiddleFinger(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            LeftTriggerOn = true;
            SenseObjects(leftHandTR);
        }
        if (ctx.canceled)
        {
            LeftTriggerOn = false;
            CancelGrapping(leftHandTR);

        }
    }
    #endregion
}
