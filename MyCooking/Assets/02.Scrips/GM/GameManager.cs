using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager gm;
    public static GameManager GMinstatnce() {  return gm;  }
    public foodList.Param SelectedFood;
    public foodList foodTable;
    public ingredientList IGList;
    public bool isGuideLineEnabled;
    public GameObject guideSphere;
    public Queue<Vector3> IngredPosition = new Queue<Vector3>();
    public Queue <GameObject> ingredientCookIndex = new Queue<GameObject>();
    public List<bool> ingredientIsOut = new List<bool>();
    public Image timerUI;
    public bool isCookDone = false;
    public Slider CookStar;
    private void Awake()
    {
        SelectedFood = null;
        if (gm != null)
        {
            Destroy(this);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this);
        }
    }
    public void GetIngredientPosition(int foodIndex)//������ �丮�� ��� �����ǰ��� ����
    {
        StartCoroutine(CookTimer(10));
        if (IngredPosition.Count ==0)
        {
            ingredientIsOut.Clear();
            SelectedFood = foodTable.sheets[0].list[foodIndex];
            IngredPosition.Enqueue(new Vector3(IGList.sheets[0].list[SelectedFood.ingredientIndex1].installerPosX, IGList.sheets[0].list[SelectedFood.ingredientIndex1].installerPosY, IGList.sheets[0].list[SelectedFood.ingredientIndex1].installerPosZ));
            ingredientIsOut.Add(false);
            if (SelectedFood.ingredientIndex2 !=0)
            {
                ingredientIsOut.Add(false);
                IngredPosition.Enqueue(new Vector3(IGList.sheets[0].list[SelectedFood.ingredientIndex2].installerPosX, IGList.sheets[0].list[SelectedFood.ingredientIndex2].installerPosY, IGList.sheets[0].list[SelectedFood.ingredientIndex2].installerPosZ));
                if (SelectedFood.ingredientIndex3 != 0)
                {
                    ingredientIsOut.Add(false);
                    IngredPosition.Enqueue(new Vector3(IGList.sheets[0].list[SelectedFood.ingredientIndex3].installerPosX, IGList.sheets[0].list[SelectedFood.ingredientIndex3].installerPosY, IGList.sheets[0].list[SelectedFood.ingredientIndex3].installerPosZ));
                }
            }
            IngredientPositionDequeue();
        }
    }
    public void IngredientPositionDequeue()
    {
        if(guideSphere == null&& isGuideLineEnabled)
        {
            guideSphere = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/TestShpere"));
        }
        if (IngredPosition.Count>0 && isGuideLineEnabled)
        {
            guideSphere.SetActive(true);
            guideSphere.transform.position = IngredPosition.Dequeue();
        }
    }
    public void InteractIngredientItems(GameObject OBJ)
    {
        if (ingredientIsOut.Count != 0)
        {
            if (ingredientIsOut[0] == false)
            {
                foreach (var item in IGList.sheets[0].list)
                {
                    if (item.englishName == OBJ.name && item.index == SelectedFood.ingredientIndex1)
                    {
                        ingredientIsOut[0] = true;
                        ingredientCookIndex.Enqueue(OBJ);
                        break;
                    }
                }
            }
            else if (ingredientIsOut.Count > 1 && ingredientIsOut[1] == false)
            {
                foreach (var item in IGList.sheets[0].list)
                {
                    if (item.englishName == OBJ.name && item.index == SelectedFood.ingredientIndex2)
                    {
                        ingredientIsOut[1] = true;
                        ingredientCookIndex.Enqueue(OBJ);
                        break;
                    }

                }
            }
            else if (ingredientIsOut.Count> 2 &&ingredientIsOut[2] == false)
            {
                foreach (var item in IGList.sheets[0].list)
                {
                    if (item.englishName == OBJ.name && item.index == SelectedFood.ingredientIndex3)
                    {
                        ingredientIsOut[2] = true;
                        ingredientCookIndex.Enqueue(OBJ);
                        guideSphere.SetActive(false);
                        break;
                    }

                }
            }
            IngredientPositionDequeue();
        }
    }
    IEnumerator CookTimer(float GoalTime)
    {
        //���ĺ� �ð� �־������ GoalTime�� ���ĺ� ��ǥ�ð�
        timerUI.color = Color.green;
        float timerNum = 1;
        isCookDone = false;
        for (int i = 0; i < 2; i++)
        {
            while (timerNum >= 0)
            {
                if (isCookDone)
                {
                    Debug.Log("���Ľ���");
                    GetCookStars(timerNum);
                    yield break;
                }
                yield return null;
                timerNum -= Time.deltaTime / GoalTime  /*/GoalTime*/;
                timerUI.fillAmount = timerNum;
                //��ǥ�� 10���Ͻ� 10������ �ʷ� 10~20�ʱ����� ȸ�� 20~30�ʱ��� ������
            }
            timerNum = 1;
            timerUI.color = Color.yellow;
        }
        timerUI.fillAmount = 1;
        timerUI.color = Color.red;
        GetCookStars(timerNum);
    }
    public void GetCookStars(float timerTime)
    {
        if(timerUI.color == Color.red)
        {
            CookStar.value = 1;
            Debug.Log(timerTime * 10);
        }
        else if (timerUI.color == Color.yellow)
        {
            CookStar.value = 2;
            Debug.Log((10-(timerTime * 10))+10);
        }
    }
}
