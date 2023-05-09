using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [Header("Main UI")]
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private TextMeshProUGUI textMainGrade;

    [Header("Game UI")]
    [SerializeField]
    private GameObject gamePanel;
    [SerializeField]
    private TextMeshProUGUI textScore;

    [Header("Result UI")]
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private TextMeshProUGUI textResultScore;
    [SerializeField]
    private TextMeshProUGUI textResultGrade;
    [SerializeField]
    private TextMeshProUGUI textResultTalk;
    [SerializeField]
    private TextMeshProUGUI textResultHighScore;

    [Header("Result UI Animation")]
    [SerializeField]
    private ScaleEffect effectGameOver;
    [SerializeField]
    private CountingEffect effectResultScore;
    [SerializeField]
    private FadeEffect effectResultGrade;


    public void Awake()
    {
        //처음 씬이 시작되어 Main UI가 활성화 상태일 때 최고 등급 불러오기
        textMainGrade.text = PlayerPrefs.GetString("HIGHGRADE");
    }

    public void GameStart()
    {
        mainPanel.SetActive(false);
        gamePanel.SetActive(true); 
    }

    public void GameOver()
    {
        int currentScore = (int)gameController.CurrentScore;

        //현재 점수 출력
        textResultScore.text = currentScore.ToString();
        //현재 등급 출력, 현재 등급에 해당하는 대사 출력
        CalculateGradeAndTalk(currentScore);
        //최고 점수 출력
        CalculateHighScore(currentScore);

        gamePanel.SetActive(false);
        resultPanel.SetActive(true);

        //게임오버 텍스트 크기 축소 애니메이션
        effectGameOver.Play(500, 200);
        //현재 점수를 0부터 카운팅하는 애니메이션
        //카운팅 애니메이션 종료후 재생
        effectResultScore.Play(0, currentScore, effectResultGrade.FadeIn);
    }

    public void GoToMainMenu()
    {
        //초기화 할게 많기 때문에 씬을 다시 불러온다
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        textScore.text = gameController.CurrentScore.ToString("F0");
    }

    private void CalculateGradeAndTalk(int score)
    {
        if(score < 2000)
        {
            textResultGrade.text = "F";
            textResultTalk.text = "TRY HAEDER!";
        }
        else if (score < 3000)
        {
            textResultGrade.text = "D";
            textResultTalk.text = "TRY HAEDER!";
        }
        else if (score < 4000)
        {
            textResultGrade.text = "C";
            textResultTalk.text = "TRY HAEDER!";
        }
        else if (score < 5000)
        {
            textResultGrade.text = "B";
            textResultTalk.text = "TRY HAEDER!";
        }
        else
        {
            textResultGrade.text = "A";
            textResultTalk.text = "TRY HAEDER!";
        }
        
    }

    private void CalculateHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");

        //최고 점수보다 높은 점수를 획득했을 때
        if(score > highScore)
        {
            //최고등급 갱신
            PlayerPrefs.SetString("HIGHGRADE", textResultGrade.text);
            //최고점수 갱신
            PlayerPrefs.SetInt("HIGHSCORE", score);

            textResultHighScore.text = score.ToString();
        }
        else
        {
            textResultHighScore.text = highScore.ToString();
        }
    }
}
