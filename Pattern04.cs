using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Pattern04 : MonoBehaviour
{
    [SerializeField]
    private MovementTransform2D boss;
    [SerializeField]
    private GameObject bossProjectile;
    [SerializeField]
    private float attackRate = 1;
    [SerializeField]
    private int maxFireCount = 5;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        boss.GetComponent<MovingEntity>().Reset();

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        //���� ���� �� ��� ����ϴ� �ð�
        yield return new WaitForSeconds(1);

        //������ �Ʒ��� �̵�
        yield return StartCoroutine(nameof(MoveDown));
        //���� �¿��̵�
        StartCoroutine(nameof(MoveLeftAndRight));

        //���� ����
        int count = 0;
        if(count < maxFireCount)
        {
            CircleFire();

            count++;

            yield return new WaitForSeconds(attackRate);
        }

        //���� ������Ʈ��Ȱ��ȭ
        boss.gameObject.SetActive(false);

        //���� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private IEnumerator MoveDown()
    {
        //��ǥ ��ġ
        float bossDestinationY = 2;

        //���� ������Ʈ Ȱ��ȭ
        boss.gameObject.SetActive(true);

        //������ ��ǥ��ġ���� �̵��ߴ��� �˻�
        while(true)
        {
            if(boss.transform.position.y <= bossDestinationY)
            {
                boss.MoveTo(Vector3.zero);

                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator MoveLeftAndRight() 
    {
        //���� ������Ʈ�� �� �ٱ����� �̵����� �ʵ��� �ϴ� ����ġ ��
        float xWeight = 3;

        //ó�� �̵� ������ ���������� ����
        boss.MoveTo(Vector3.right);

        while(true)
        {
            //������ ��ġ�� ���� �ּ� ������ �Ѿ�� �̵������� ���������� ��ȯ
            if(boss.transform.position.x <= Contants.min.x + xWeight)
            {
                boss.MoveTo(Vector3.right);
            }
            //������ ��ġ�� ������ �ּ� ������ �Ѿ�� �̵������� �������� ��ȯ
            else if (boss.transform.position.x >= Contants.min.x - xWeight)
            {
                boss.MoveTo(Vector3.left) ;
            }

            yield return null;
        }
    }

    private void CircleFire()
    {
        int count = 30;
        float intervalAngle = 360 / count;

        //�� ���·� ����ϴ� �߻�ü ����
        for(int i = 0; i < count; ++i)
        {
            //�߻�ü ����
            GameObject clone = Instantiate(bossProjectile, boss.transform.position, Quaternion.identity);

            //�߻�ü �̵�����(����)
            float angle = intervalAngle * i;
            //�߻�ü �̵����� (����)
            float x = Mathf.Cos(angle * Mathf.PI / 180.0f); //Cos ���� ������ ���� ǥ���� ���� PI / 180�� ����
            float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
            //�߻�ü �̵����� ����
            clone.GetComponent<MovementTransform2D>().MoveTo(new Vector2(x, y));
        }
    }
}
