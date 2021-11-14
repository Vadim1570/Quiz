using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionWriteString : MonoBehaviour
{
    public UnityEngine.UI.InputField AnwearInput;
    public string RightAnswear;

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        if(AnwearInput.text.ToLower() == RightAnswear.ToLower() )
            ScoreKeeper.GetScoreKeeper().Score += 1;

        SceneManager.LoadScene(sceneName);
    }
}
