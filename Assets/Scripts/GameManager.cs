using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void IncreaseScore(int hitpount)
    {
        ScoreKeeper.GetScoreKeeper().Score =+ hitpount;
    }
}

public class ScoreKeeper
{
    public int Score { get; set; }
    private static ScoreKeeper _scoreKeeper;
    private ScoreKeeper() {}

    public static ScoreKeeper GetScoreKeeper()
    {
        if(_scoreKeeper == null)
            _scoreKeeper = new ScoreKeeper() { Score = 0 };
        return _scoreKeeper;
    }
}