using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern07 : MonoBehaviour
{
    [SerializeField]
    private GameObject ground;
    [SerializeField]
    private GameObject doctorKO;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private Collider2D[] laserCollider2D;
    [SerializeField]
    private float rotateTime;
    [SerializeField]
    private int anglePerSeconds;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        //패턴 시작 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        //발판, 고박사, 레이저 활성화
        ground.SetActive(true);
        doctorKO.SetActive(true);
        laser.SetActive(true);

        //레이저가 등장하자마자 플레이어와 충돌하지 않도록 collider2D를 잠시 비활성화
        for(int i = 0; i < laserCollider2D.Length; ++ i )
        {
            laserCollider2D[i].enabled = false;
        }

        //레이저가 등장하고 잠시 대기하는 시간 
        yield return new WaitForSeconds(0.5f);

        //레이저의 Collider2D 활성화
        for(int i = 0; i < laserCollider2D.Length; ++ i)
        {
            laserCollider2D[i].enabled = true;
        }

        //레이저 회전 시간
        float time = 0;
        while ( time < rotateTime)
        {
            laser.transform.Rotate(Vector3.forward * anglePerSeconds * Time.deltaTime);

            time += Time.deltaTime;

            yield return null;
        }

        //종료 시 레이저, 고박사, 발판 비활성화
        ground.SetActive(false);
        doctorKO.SetActive(false);
        laser.SetActive(false);

        //패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }
}
