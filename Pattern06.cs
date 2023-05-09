using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern06 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] warningImages;
    [SerializeField]
    private GameObject doctorKO;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float spawnCycle;
    [SerializeField]
    private int maxCount;
    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {

        doctorKO.GetComponent<MovingEntity>().Reset();
        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        //패턴 시작 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        //경고 이미지를 0.5초동안 활성화했다가 비활성화
        warningImages[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImages[0].SetActive(false);

        //고박사 오브젝트가 위에서 아래로 이동
        yield return StartCoroutine(nameof(MoveDown));

        //C# 발사체 생성
        yield return StartCoroutine(nameof(SpawnProjectile));
        //경고 이미지를 0.5초 동안 활성화했다가 비활성화
        warningImages[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImages[1].SetActive(false);

        //고박사 오브젝트 좌우이동
        yield return StartCoroutine(nameof(MoveHorizontal));
        //패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }

    private IEnumerator MoveDown()
    {
        //목표 위치
        float destinationY = -2.7f;

        //고박사 오브젝트 활성화
        doctorKO.gameObject.SetActive(true);

        //고박사가 목표위치까지 이동했는지 검사
        while(true)
        {
            if(doctorKO.transform.position.y <= destinationY )
            {
                doctorKO.GetComponent<MovementTransform2D>().MoveTo(Vector3.zero);

                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator SpawnProjectile()
    {
        float minSpeed = 2;
        float maxSpeed = 10;

        int count = 0;
        while(count < maxSpeed)
        {
            GameObject clone = Instantiate(projectilePrefab, doctorKO.transform.position, Quaternion.identity);
            var movement2D = clone.GetComponent<MoveRigidbody>();

            movement2D.MoveSpeed = Random.Range(minSpeed, maxSpeed);
            movement2D.MoveTo(1 - 2 * Random.Range(0, 2));
            movement2D.IsLongJump = Random.Range(0, 2) == 0 ? false : true;
            movement2D.JumpTo();

            count++;   
            
            yield return new WaitForSeconds(spawnCycle);
        }
    }

    private IEnumerator MoveHorizontal()
    {
        Vector3 direction = Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right;
        doctorKO.GetComponent<MovementTransform2D>().MoveTo(direction);

        while(true)
        {
            if(doctorKO.transform.position.x < Contants.min.x ||
                doctorKO.transform.position.x > Contants.max.x)
            {
                doctorKO.gameObject.SetActive(false);

                yield break;
            }

            yield return null;
        }
    }
}

