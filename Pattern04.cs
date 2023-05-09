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
        //패턴 시작 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        //보스가 아래로 이동
        yield return StartCoroutine(nameof(MoveDown));
        //보스 좌우이동
        StartCoroutine(nameof(MoveLeftAndRight));

        //보스 공격
        int count = 0;
        if(count < maxFireCount)
        {
            CircleFire();

            count++;

            yield return new WaitForSeconds(attackRate);
        }

        //보스 오브젝트비활성화
        boss.gameObject.SetActive(false);

        //패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }

    private IEnumerator MoveDown()
    {
        //목표 위치
        float bossDestinationY = 2;

        //보스 오브젝트 활성화
        boss.gameObject.SetActive(true);

        //보스가 목표위치까지 이동했는지 검사
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
        //보스 오브젝트가 맵 바깥까지 이동하지 않도록 하는 가중치 값
        float xWeight = 3;

        //처음 이동 방향을 오른쪽으로 설정
        boss.MoveTo(Vector3.right);

        while(true)
        {
            //보스의 위치가 왼쪽 최소 범위를 넘어가면 이동방향을 오른쪽으로 전환
            if(boss.transform.position.x <= Contants.min.x + xWeight)
            {
                boss.MoveTo(Vector3.right);
            }
            //보스의 위치가 오른쪽 최소 범위를 넘어가면 이동방향을 왼쪽으로 전환
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

        //원 형태로 방사하는 발사체 생성
        for(int i = 0; i < count; ++i)
        {
            //발사체 생성
            GameObject clone = Instantiate(bossProjectile, boss.transform.position, Quaternion.identity);

            //발사체 이동방향(각도)
            float angle = intervalAngle * i;
            //발사체 이동방향 (벡터)
            float x = Mathf.Cos(angle * Mathf.PI / 180.0f); //Cos 라디안 단위의 각도 표현을 위해 PI / 180을 곱함
            float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
            //발사체 이동방향 설정
            clone.GetComponent<MovementTransform2D>().MoveTo(new Vector2(x, y));
        }
    }
}
