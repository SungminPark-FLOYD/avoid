using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject[] patterns;      //�����ϰ� �ִ� ��� ����
    [SerializeField]
    private GameObject hpPotion;        //ü��ȸ������
    private GameObject currentPattern;  //���� ������� ����
    private int[] patternIndexs;        //��ġ�� �ʴ� patterns.Length ������ ����
    private int current = 0;            //patternIndexs �迭�� ����

    private void Awake()
    {
        //�����ϰ� �ִ� ���� ������ �����ϰ� �޸� �Ҵ�
        patternIndexs = new int[patterns.Length];

        //ó������ ������ �������� �����ϵ��� ����
        for(int i =0; i < patternIndexs.Length;  ++i)
        {
            patternIndexs[i] = i;
        }
    }

    private void Update()
    {
        if (gameController.IsGamePlay == false) return;

        //���� ������� ������ ����Ǿ� ������Ʈ�� ��Ȱ��ȭ�Ǹ�
        if(currentPattern.activeSelf == false)
        {
            //���� ���� ����
            changePattern();
        }
    }

    public void GameStart()
    {
        changePattern();
    }

    public void GameOver()
    {
        //���� ������� ���ϸ� ��Ȱ��ȭ
         currentPattern.SetActive(false);
    }

    public void changePattern()
    {
        //���� ����(currentPattern)����
        currentPattern = patterns[patternIndexs[current]];

        //���� ���� Ȱ��ȭ
        currentPattern.SetActive(true);

        current++;

        // 4, 3, 3���� ������ Ŭ�������� �� ü��ȸ���������
        if(current == 4 || current == 7 || current == 10) 
        {
            hpPotion.SetActive(true);  
        }

        //������ �ѹ��� ��� �����ߴٸ� ���� ������ ��ġ�� �ʴ� ������ ���ڷ� ����
        if( current >= patternIndexs.Length )
        {
            patternIndexs = Utils.RandomNumbers(patternIndexs.Length, patternIndexs.Length);
            current = 0;
        }
    }

}
