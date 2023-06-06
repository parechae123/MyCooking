using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spatula : CookerBase
{
    //뒤집개
    private void Start()
    {
        TargetLayer = LayerMask.NameToLayer("1번") | LayerMask.NameToLayer("2번");//추후 레이어 지정되면 이걸 넣어주면됨
    }
    public override void FuncForEachTypes()
    {
        if (Physics.Raycast(transform.position,Vector3.down,out hit,1,TargetLayer))
        {
            hit.transform.rotation = new Quaternion(0, 0, 1, 0);//여기있는거 조합해서 플레이어 컨트롤에 추가하기
        }
    }
}
