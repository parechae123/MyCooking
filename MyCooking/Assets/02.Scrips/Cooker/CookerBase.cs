using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookerBase : MonoBehaviour
{
    protected LayerMask TargetLayer;
    public Cookers whatIsThis;
    public RaycastHit hit;
    private void Update()
    {
        FuncForEachTypes();
    }
    public virtual void FuncForEachTypes()
    {
        
    }
}
public enum Cookers
{
    Spatula,
    cuttingBoard,
}
