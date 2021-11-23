
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

  public void BtQuit()
    {
       Application.Quit();
    }
 

 public void OpenAudio()
    {
        Application.OpenURL("https://youtube.com/playlist?list=PLsDUIWlwo18rUD1-3R3QoSDn3dzQtfGrT");
    }

    public void OpenVideo()
    {
        Application.OpenURL("https://youtube.com/playlist?list=PLsDUIWlwo18rcBp4t99VibngiWnQHtyEo");
    }

      public void OpenQuiz()
    {
      SceneManager.LoadScene("Level1");
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