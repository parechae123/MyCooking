using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spatula : CookerBase
{
    //������
    private void Start()
    {
        TargetLayer = LayerMask.NameToLayer("1��") | LayerMask.NameToLayer("2��");//���� ���̾� �����Ǹ� �̰� �־��ָ��
    }
    public override void FuncForEachTypes()
    {
        if (Physics.Raycast(transform.position,Vector3.down,out hit,1,TargetLayer))
        {
            hit.transform.rotation = new Quaternion(0, 0, 1, 0);//�����ִ°� �����ؼ� �÷��̾� ��Ʈ�ѿ� �߰��ϱ�
        }
    }
}
