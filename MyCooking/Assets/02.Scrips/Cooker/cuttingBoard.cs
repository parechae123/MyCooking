using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuttingBoard : CookerBase
{
    //����
    private void Start()
    {
        TargetLayer = LayerMask.NameToLayer("1��")|LayerMask.NameToLayer("2��");//���� ���̾� �����Ǹ� �̰� �־��ָ��
    }
    public override void FuncForEachTypes()
    {

    }
}
