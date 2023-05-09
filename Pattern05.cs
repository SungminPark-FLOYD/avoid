using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern05 : MonoBehaviour
{
    [SerializeField]
    private GameObject warningImage;
    [SerializeField]
    private GameObject doctorKO;
    [SerializeField]
    private Vector3[] spawnPositions;   //오브젝트 생성 위치
    [SerializeField]
    private float spawnCycle;           //생성 주기
    [SerializeField]
    private int maxCount;               //등장 횟수

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

        //경고 이미지를 0.5초동안 활성화했다가 비활성화
        warningImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImage.SetActive(false);

        //고박사 등장 및 이동
        int count = 0;
        while (count < maxCount)
        {
            //0 : 완쪽에서 오른쪽으로 1 : 오른쪽에서 왼쪽으로
            int spawnType = Random.Range(0, 2);

            //오브젝트 생성 및 위치 설정
            GameObject clone = Instantiate(doctorKO, spawnPositions[spawnType], Quaternion.identity);
            //이미지 좌우반전
            clone.GetComponent<SpriteRenderer>().flipX = spawnType == 0;
            //오브젝트 이동방향
            clone.GetComponent<MovementTransform2D>().MoveTo(spawnType == 0 ? Vector3.right : Vector3.left);

            count++;

            yield return new WaitForSeconds(spawnCycle);
        }

        //패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }
}
