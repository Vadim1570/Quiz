using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionCheckOneAnswear : MonoBehaviour
{
    public string NextSceneName;
    public string SelectedAnswear;
    public string RightAnswear;

    public void SetSelectedAnswear(string answear)
    {
        this.SelectedAnswear = answear;
    }

    public void SetSelectedAnswearAndLoadNextScene(string answear)
    {
        this.SelectedAnswear = answear;
        SaveAnswearAndLoadScene(NextSceneName);
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        if(SelectedAnswear.ToLower() == RightAnswear.ToLower() )
            ScoreKeeper.GetScoreKeeper().Score += 1;

        SceneManager.LoadScene(sceneName);
    }
}