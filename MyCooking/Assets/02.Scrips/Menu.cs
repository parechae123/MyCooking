using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
public class Menu : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Off(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Sequence sequence = DOTween.Sequence();
            if (menu.activeSelf)
            {
                
                sequence.Append(menu.transform.DOScale(new Vector2(0.01f, 0.01f), 0.5f));
                StartCoroutine(End());
            }
            else
            {
                menu.SetActive(true);
                sequence.Append(menu.transform.DOScale(new Vector2(1, 1), 0.5f));
                sequence.Append(menu.transform.DOPunchScale(new Vector2(0.2f, 0.2f), 0.5f, 1, 0.1f));

            }
        }
    }
    
    IEnumerator End()
    {
        yield return new WaitForSeconds(0.5f);
        menu.SetActive(false);
    }

}
