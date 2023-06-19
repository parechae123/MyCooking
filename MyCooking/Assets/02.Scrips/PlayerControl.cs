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
    private Queue<Vector3> leftThrowingVector = new Queue<Vector3>();
    private Queue<Vector3> rightThrowingVector = new Queue<Vector3>();
    private float[] throwingTimer = new float[2];
    public Transform plr;
    //준희
    public int layerMask;
    public List<string> cook = new List<string>();
    public int hitnum = 0;
    private GameObject cookingCompletion;
    public Transform foodPoint;
    public Transform fryFan1;
    public GameObject spoon;
    #endregion
    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Objects");
    }
    private void Awake()
    {
        leftHandTR = GameObject.Find("LeftHand Controller").GetComponent<Transform>();
        rightHandTR = GameObject.Find("RightHand Controller").GetComponent<Transform>();
    }
    private void Update()
    {
        throwingTimer[0] += Time.deltaTime;
        throwingTimer[1] += Time.deltaTime;
        if (objOnRighttHand != null && throwingTimer[0] >= 0.03f)
        {
            rightThrowingVector.Enqueue(rightHandTR.position);
            if (rightThrowingVector.Count >= 6)
            {
                rightThrowingVector.Dequeue();
            }
            throwingTimer[0] = 0;
        }
        else if (objOnLeftHand != null && throwingTimer[1] >= 0.03f)
        {

            leftThrowingVector.Enqueue(leftHandTR.position);
            if (leftThrowingVector.Count >= 6)
            {
                leftThrowingVector.Dequeue();
            }
            throwingTimer[1] = 0;
        }
    }
    public void OnQuestJoy(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        Vector3 tempVec = Camera.main.transform.TransformDirection(dir.x, 0, dir.y);
        plr.transform.position += new Vector3(tempVec.x / 20, 0, tempVec.z / 20);
    }
    #region VR 검지손가락 인풋관리
    public void OnRightTriggerPress(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (spoon.transform.parent == rightHandTR)
            {
                Debug.Log("오른쪽 스푼 반응");
                if (Physics.Raycast(rightHandTR.transform.position, rightHandTR.transform.forward, 0.7f, layerMask))
                {
                    RaycastHit hitInfo;
                    if (Physics.Raycast(rightHandTR.transform.position, rightHandTR.transform.forward, out hitInfo, 0.7f, layerMask) && hitInfo.collider.name == "FryPan")
                    {
                            Debug.Log("프라이팬 반응");
                        for (int i = 0; i < hitInfo.collider.transform.childCount; i++)
                        {
                            Debug.Log("반복문 반응");
                            if (hitInfo.collider.transform.GetChild(i).gameObject.layer == 6)       //hitInfo.collider.transform.GetChild(i).gameObject.layer 이건 10진법이고 layerMask 이건 2진법
                            {
                                cook.Add(hitInfo.collider.transform.GetChild(i).name);           //자식오브젝트를 리스트에 넣음
                                Destroy(hitInfo.collider.transform.GetChild(i).gameObject);
                            }
                        }                                                                   //여기 이름은 나중에 대체할 수도 있음
                        if (cook.Contains("Kimchi") && cook.Contains("Rice"))               //김치볶음밥 생성
                        {
                            Debug.Log("김치볶음밥 반응");
                            cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/KimchiFriedRice"));
                            cookingCompletion.transform.position = foodPoint.transform.position;
                            cook.Clear();
                        }
                        if (cook.Contains("Scrambledeggs") && cook.Contains("Rice"))               //간장계란비빔밥
                        {
                            cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/SoySauceAndEggBibimbap"));
                            cookingCompletion.transform.position = foodPoint.transform.position;
                            cook.Clear();
                        }
                        if (cook.Contains("EggFry"))                                      //스크럼블에그
                        {
                            Debug.Log("계란후라이");
                            cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Scrambledeggs"));
                            cookingCompletion.name = cookingCompletion.name.Replace("(Clone)", "");
                            cookingCompletion.gameObject.transform.position = foodPoint.position;
                            cookingCompletion.gameObject.transform.parent = fryFan1;
                            cook.Clear();
                        }
                        if (cook.Contains("Spams"))                                      //스팸
                        {
                            cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/PieceOfSpam"));
                            cookingCompletion.transform.position = foodPoint.transform.position;
                            cook.Clear();
                        }
                        if (cook.Contains("RedPepperTuna") && cook.Contains("Rice"))      //고추참치비빔밥
                        {
                            cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/RedPepperTunaBibimbap"));
                            cookingCompletion.transform.position = foodPoint.transform.position;
                            cook.Clear();
                        }
                    }
                }
            }

        }
    }
    public void OnLeftTriggerPress(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (spoon.transform.parent == leftHandTR)
            {
                if (Physics.Raycast(leftHandTR.transform.position, leftHandTR.transform.forward, 0.7f, layerMask))
                {
                    RaycastHit hitInfo;
                    if (Physics.Raycast(leftHandTR.transform.position, leftHandTR.transform.forward, out hitInfo, 0.7f, layerMask) && hitInfo.collider.name == "FryPan")
                    {
                        for (int i = 0; i < hitInfo.collider.transform.childCount; i++)
                        {
                            if (hitInfo.collider.transform.GetChild(i).gameObject.layer == 6)       //hitInfo.collider.transform.GetChild(i).gameObject.layer 이건 10진법이고 layerMask 이건 2진법
                            {
                                cook.Add(hitInfo.collider.transform.GetChild(i).name);           //자식오브젝트를 리스트에 넣음
                                Destroy(hitInfo.collider.transform.GetChild(i).gameObject);
                            }
                        }                                                                   //여기 이름은 나중에 대체할 수도 있음
                        if (cook.Contains("Kimchi") && cook.Contains("Rice"))               //김치볶음밥 생성
                        {
                            cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/KimchiFriedRice"));
                            cookingCompletion.transform.position = foodPoint.transform.position;
                            cook.Clear();
                        }
                        if (cook.Contains("Scrambledeggs") && cook.Contains("Rice"))               //간장계란비빔밥
                        {
                            cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/SoySauceAndEggBibimbap"));
                            cookingCompletion.transform.position = foodPoint.transform.position;
                            cook.Clear();
                        }
                        if (cook.Contains("EggFry"))                                      //스크럼블에그
                        {
                            cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Scrambledeggs" + cookingCompletion.name.Replace("(Clone)", "")));
                            cookingCompletion.gameObject.transform.position = foodPoint.position;
                            cookingCompletion.gameObject.transform.parent = fryFan1;
                            cook.Clear();
                        }
                        if (cook.Contains("Spams"))                                      //스팸
                        {
                            cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/PieceOfSpam"));
                            cookingCompletion.transform.position = foodPoint.transform.position;
                            cook.Clear();
                        }
                        if (cook.Contains("RedPepperTuna") && cook.Contains("Rice"))      //고추참치비빔밥
                        {
                            cookingCompletion = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/RedPepperTunaBibimbap"));
                            cookingCompletion.transform.position = foodPoint.transform.position;
                            cook.Clear();
                        }
                    }

                }
            }
        }
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
            else if (Hit.collider.gameObject.layer == 6 || Hit.collider.gameObject.layer == 10)//레이어 10 추가
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
                    StartCoroutine(FreazerRotations(objTemp, leftHandTR.transform));

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
    IEnumerator FreazerRotations(GameObject target, Transform handDir)
    {
        Vector2 lastHandPosition = new Vector2(handDir.transform.position.x, handDir.transform.position.z);
        float disFromOrigin = 0;
        while (RightTriggerOn || LeftTriggerOn)
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
                Debug.Log("던짐");
                Vector2 LastThrowingValue = Vector2.zero;
                Vector3 ThrowForce = Vector3.zero;
                foreach (var item in leftThrowingVector)
                {
                    Debug.Log("던짐 각도분석");
                    if (item.x > LastThrowingValue.x)
                    {
                        ThrowForce += Vector3.right * 20;
                    }
                    else
                    {
                        ThrowForce -= Vector3.right * 20;
                    }
                    if (item.z > LastThrowingValue.y)
                    {
                        ThrowForce += Vector3.forward * 20;
                    }
                    else
                    {
                        ThrowForce -= Vector3.forward * 20;
                    }
                    LastThrowingValue = new Vector2(item.x, item.z);
                }
                leftThrowingVector.Clear();
                targetRB.AddForce(ThrowForce * 5);

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
                Debug.Log("던짐");
                Vector2 LastThrowingValue = Vector2.zero;
                Vector3 ThrowForce = Vector3.zero;
                foreach (var item in rightThrowingVector)
                {
                    Debug.Log("던짐 각도분석");
                    if (item.x > LastThrowingValue.x)
                    {
                        ThrowForce += Vector3.right * 20;
                    }
                    else
                    {
                        ThrowForce -= Vector3.right * 20;
                    }
                    if (item.z > LastThrowingValue.y)
                    {
                        ThrowForce += Vector3.forward * 20;
                    }
                    else
                    {
                        ThrowForce -= Vector3.forward * 20;
                    }
                    LastThrowingValue = new Vector2(item.x, item.z);
                }
                rightThrowingVector.Clear();
                targetRB.AddForce(ThrowForce * 5);
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
