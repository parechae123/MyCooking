using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoiSourcePett : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Liquid;
    private LiquidScaling LiquidSize;
    public LayerMask interactiveLayers;
    public Transform sourcePosition;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (transform.rotation.z > 0.258819103)
        {
            if (Liquid ==null)
            {
                Liquid = Instantiate(Resources.Load<GameObject>("Prefabs/SoiSourceLiquid")).transform;
                LiquidSize = Liquid.gameObject.GetComponent<LiquidScaling>();


            }
            else
            {
                Liquid.gameObject.SetActive(true);
                Liquid.position = sourcePosition.position;

            }
            if (Physics.Raycast(Liquid.position, -Liquid.up, out hit, 10,interactiveLayers))
            {
                LiquidSize.length = Vector3.Distance(Liquid.transform.position, hit.point);
                Debug.Log(Vector3.Distance(Liquid.transform.position, hit.point));
            }
        }
        else if (transform.rotation.z < 0.258819103)
        {
            if (Liquid != null)
            {
                Liquid.gameObject.SetActive(false);
            }
        }

    }
}

