using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Customer : MonoBehaviour
{
    public bool orderIsDone = false;
    public int randomValue;
    public float waitTime = 3;
    public Image timerUI;
    public TextMeshProUGUI customerText;
    public TextMeshProUGUI customerQueueText;
    public LayerMask cookedFoodLayer;
    public CustomerManager CM;
    public Image menuImage;
    public void OnEnable()
    {
        customerText.gameObject.SetActive(false);
        randomValue = Random.Range(0, 4);
        menuImage.sprite = Resources.Load<Sprite>("FoodSprites/" + randomValue.ToString());
        orderIsDone = false;
        GameManager.GMinstatnce().GetOrder(randomValue);
        CM.ChangeCustomerQueue();
        StartCoroutine(sonNomTimer());
    }
    public void OnDisable()
    {
        CM.customers.RemoveAt(0);
        GameManager.GMinstatnce().ingredientCookIndex.Clear();
        GameManager.GMinstatnce().NextOrder();
        GameManager.GMinstatnce().CustomerPool.Enqueue(GetComponent<Customer>());
        CM.ChangeCustomerQueue();
        customerText.text = "";
        customerQueueText.text = "5";
    }
    public void FixedUpdate()
    {
        if (!orderIsDone)
        {
            RaycastHit hit;
            if (Physics.BoxCast(transform.position, Vector3.one * 0.4f, transform.forward, out hit, Quaternion.identity, 1, cookedFoodLayer))
            {
                if (hit.collider.name.Contains(GameManager.GMinstatnce().SelectedFood.foodName))
                {
                    Debug.Log("�̸�����");
                    orderIsDone = true;
                    Destroy(hit.collider.gameObject);
                }
            } 
        }
    }
    IEnumerator sonNomTimer()
    {
        //���ĺ� �ð� �־������ GoalTime�� ���ĺ� ��ǥ�ð�
        timerUI.color = Color.green;
        float timerNum = 1;
        for (int i = 0; i < 2; i++)
        {
            while (timerNum >= 0)
            {
                if (orderIsDone)
                {
                    Debug.Log("���Ľ���");
                    cumstomerPay();
                    yield break;
                }
                yield return null;
                timerNum -= Time.deltaTime / waitTime  /*/GoalTime*/;
                timerUI.fillAmount = timerNum;
                //��ǥ�� 10���Ͻ� 10������ �ʷ� 10~20�ʱ����� ȸ�� 20~30�ʱ��� ������
            }
            timerNum = 1;
            timerUI.color = Color.yellow;
        }
        timerUI.color = Color.red;
        timerUI.fillAmount = 1;
        cumstomerPay();
    }
    public void cumstomerPay()
    {
        if(timerUI.color == Color.red)
        {
            customerText.text = "**** you";
            //���⿡ ���� ��޿� �´� ���� �������ְ�����
        }
        else if(timerUI.color == Color.yellow)
        {
            customerText.text = "not bad";
        }
        else if(timerUI.color == Color.green)
        {
            customerText.text = "Good!";
        }
        StartCoroutine(timerToCustomerOut());
    }
    IEnumerator timerToCustomerOut()
    {
        yield return new WaitForSeconds(1);
        GameManager.GMinstatnce().IngredPosition.Clear();
        customerText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
