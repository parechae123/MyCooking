using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuttingBoard : CookerBase
{
    //도마
    private void Start()
    {
        TargetLayer = LayerMask.NameToLayer("1번")|LayerMask.NameToLayer("2번");//추후 레이어 지정되면 이걸 넣어주면됨
    }
    public override void FuncForEachTypes()
    {

    }
}
