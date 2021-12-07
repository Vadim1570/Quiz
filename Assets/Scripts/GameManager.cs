
using UnityEngine;

using UnityEngine.SceneManagement;
using System.Collections;



public class GameManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        StartCoroutine(waitOkSound(sceneName));      
        
        //SceneManager.LoadScene(sceneName);
       // Debug.Log("test");
    }

    public IEnumerator waitOkSound(string sceneName)
    {
     
     yield return new WaitForSeconds(0.3f); 
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
        Application.OpenURL("https://youtube.com/playlist?list=PLsDUIWlwo18rrEEsrEpbyxGfi1-F8QCrZ");
    }

    public void OpenVideo()
    {
        Application.OpenURL("https://youtube.com/playlist?list=PLsDUIWlwo18qs1zLiQAGVAEr0HFOOrMzq");
    }

      public void OpenQuiz()
    {
     // SceneManager.LoadScene("Level1");
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