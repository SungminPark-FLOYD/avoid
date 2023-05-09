using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern08 : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private GameObject warningImage;
    [SerializeField]
    private GameObject prefab;
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
        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        //패턴 시작 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        int count = 0;
        while (count < maxCount)
        {
            StartCoroutine(nameof(SpawnPrefab));

            count++;

            yield return new WaitForSeconds(spawnCycle);
        }

        //패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnPrefab()
    {
        //현재 플레이어 위치에 경고 이미지 출력
        GameObject warningClone = Instantiate(warningImage, playerTransform.position, Quaternion.identity);
        warningClone.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(0.5f);

        //경고 이미지 위치에 프리펩 생성
        GameObject prefabClone = Instantiate(prefab, warningClone.transform.position, Quaternion.identity);
        Destroy(warningClone);
        Destroy(prefabClone, 0.5f);
    }
}
