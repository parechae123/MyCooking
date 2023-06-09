using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public List<Customer> customers = new List<Customer>();
    public float respawnTime = 10;
    public float timer = 0;
    public bool isAllSetting = false;
    private void Start()
    {
        for (int i = 0; i <= 2; i++)
        {
            customers.Add(transform.GetChild(i).GetComponent<Customer>());
            customers[i].CM = this;
        }
        foreach (var item in customers)
        {
            item.customerQueueText.text = (1+customers.IndexOf(item)).ToString();
        }
        StartCoroutine(OnStartSonom());
    }
    public void ChangeCustomerQueue()
    {
        foreach (var item in customers)
        {
            item.customerQueueText.text = (1+customers.IndexOf(item)).ToString();
        }
    }
    public void Update()
    {
        timer += Time.deltaTime;
        if(timer> respawnTime&&isAllSetting)
        {
            if (GameManager.GMinstatnce().CustomerPool.Count>0)
            {
                Customer tempSon = GameManager.GMinstatnce().CustomerPool.Dequeue();
                customers.Add(tempSon);
                tempSon.gameObject.SetActive(true);
            }
            timer = 0;
        }
    }
    IEnumerator OnStartSonom()
    {
        isAllSetting = false;
        for (int i = 0; i < 3; i++)
        {
            GameManager.GMinstatnce().CustomerPool.Enqueue(customers[i]);
        }
        for (int i = 0; i<3;i++)
        {
            GameManager.GMinstatnce().CustomerPool.Dequeue().gameObject.SetActive(true);
            yield return new WaitForSeconds(respawnTime);
        }
        isAllSetting = true;
    }
}
