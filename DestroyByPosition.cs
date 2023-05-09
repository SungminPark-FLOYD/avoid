
using System.Transactions;
using UnityEngine;

public class DestroyByPosition : MonoBehaviour
{
    //ȭ�� �� ���� ������ ����� �� �����ϱ� ���� ����ġ ��
    private float destroyWeight = 2;

    private void LateUpdate()
    {
        if(transform.position.x < Contants.min.x - destroyWeight || 
           transform.position.x > Contants.max.x + destroyWeight ||
           transform.position.y < Contants.min.y - destroyWeight ||
           transform.position.y > Contants.max.y + destroyWeight)
        {
            Destroy(gameObject);
        }
    }
}
