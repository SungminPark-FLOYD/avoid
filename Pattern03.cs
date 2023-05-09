using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pattern03 : MonoBehaviour
{
    [SerializeField]
    private GameObject warningImage;
    [SerializeField]
    private Transform boom;
    [SerializeField]
    private GameObject boomEffect;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }
    private void OnDisable()
    {
        boom.GetComponent<MovingEntity>().Reset();

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        //패턴 시작 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        //경고 이미지 활성/비활성
        warningImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImage.SetActive(false);

        //폭탄 오브젝트 활성화/이동
        yield return StartCoroutine(nameof(MoveUp));

        //폭탄 이펙트 활성/비활성
        boomEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        boomEffect.SetActive(false);
        

        //패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }

    private IEnumerator MoveUp()
    {
        //목표 위치
        float boomDestinationY = 0;

        //폭탄 오브젝트를 활성화
        boom.gameObject.SetActive(true);

        //폭탄 목표위치까지 이동했는지 검사
        while(true)
        {
            if(boom.transform.position.y >= boomDestinationY)
            {
                //폭탄 오브젝트 비활성화
                boom.gameObject.SetActive(false);

                yield break;
            }

            yield return null;
        }
    }
}
