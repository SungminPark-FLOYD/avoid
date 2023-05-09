using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern10 : MonoBehaviour
{
    [SerializeField]
    private GameObject warningObject;
    [SerializeField]
    private MovementTransform2D unityLogo;
    [SerializeField]
    private float moveTime;
    [SerializeField]
    private float minX = -2.7f;
    [SerializeField]
    private float maxX = 2.7f;
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

        //경고 오브젝트 활성/비활성
        warningObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningObject.SetActive(false);

        //로고 오브젝트 활성화
        unityLogo.gameObject.SetActive(true);

        //처음 이동방향을 오른쪽으로 설정
        unityLogo.MoveTo(Vector3.right);

        //로고 오브젝트 좌우이동
        float time = 0;
        while (time < moveTime)
        {
            time += Time.deltaTime;

            //로고의 위치가 왼쪽 최소범위를 넘어가면 이동방향을 오른쪽으로 설정
            if(unityLogo.transform.position.x <= minX)
            {
                unityLogo.MoveTo(Vector3.right);
            }
            //로고의 위치가 오른쪽 최소범위를 넘어가면 이동방향을 왼쪽으로 설정
            else if (unityLogo.transform.position.x >= maxX)
            {
                unityLogo.MoveTo(Vector3.left);
            }

            yield return null;
        }

        unityLogo.gameObject.SetActive(false);

        //패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }
}
