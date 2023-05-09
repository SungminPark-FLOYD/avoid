using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pattern02 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] warningImages;     //경고 이미지
    [SerializeField]
    private GameObject[] playerObjects;     //플레이어 오브젝트
    [SerializeField]
    private float spawnCycle = 1;           //생성 주기

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        //다음 사용 위해 플레이어 오브젝트의 상태를 초기화
        for(int i = 0; i < playerObjects.Length; ++i)
        {
            playerObjects[i].SetActive(false);
            playerObjects[i].GetComponent<MovingEntity>().Reset();
        }

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        //패턴 시작 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        //플레이어 등장 위치를 겹치지 않는 임의의 위치로설정
        int[] numbers = Utils.RandomNumbers(3, 3);

        int index = 0;
        while(index < numbers.Length)
        {
            StartCoroutine(nameof(SpawnPlayer), numbers[index]);

            index++;

            yield return new WaitForSeconds(spawnCycle);
        }

        //패턴 종료 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        //패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnPlayer(int index)
    {
        //경고 이미지 활성/비활성
        warningImages[index].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImages[index].SetActive(false);

        //플레이어 오브젝트 활성화
        playerObjects[index].SetActive(true);
    }
}
