using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject[] patterns;      //보유하고 있는 모든 패턴
    [SerializeField]
    private GameObject hpPotion;        //체력회복물약
    private GameObject currentPattern;  //현재 재생중인 패턴
    private int[] patternIndexs;        //겹치지 않는 patterns.Length 개수의 숫자
    private int current = 0;            //patternIndexs 배열의 순번

    private void Awake()
    {
        //보유하고 있는 패턴 개수와 동일하게 메모리 할당
        patternIndexs = new int[patterns.Length];

        //처음에는 패턴을 순차적을 실행하도록 설정
        for(int i =0; i < patternIndexs.Length;  ++i)
        {
            patternIndexs[i] = i;
        }
    }

    private void Update()
    {
        if (gameController.IsGamePlay == false) return;

        //현재 재생중인 패턴이 종료되어 오브젝트가 비활성화되면
        if(currentPattern.activeSelf == false)
        {
            //다음 패턴 실행
            changePattern();
        }
    }

    public void GameStart()
    {
        changePattern();
    }

    public void GameOver()
    {
        //현재 재생중인 패턴만 비활성화
         currentPattern.SetActive(false);
    }

    public void changePattern()
    {
        //현재 패턴(currentPattern)변경
        currentPattern = patterns[patternIndexs[current]];

        //현재 패턴 활성화
        currentPattern.SetActive(true);

        current++;

        // 4, 3, 3개의 패턴을 클리어했을 때 체력회복물약등장
        if(current == 4 || current == 7 || current == 10) 
        {
            hpPotion.SetActive(true);  
        }

        //패턴을 한바퀴 모두 실행했다면 패턴 순서를 겹치지 않는 임의의 숫자로 설정
        if( current >= patternIndexs.Length )
        {
            patternIndexs = Utils.RandomNumbers(patternIndexs.Length, patternIndexs.Length);
            current = 0;
        }
    }

}
