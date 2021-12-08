using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    public UnityEngine.UI.Text YourScoreText;

    void Start()
    {
        YourScoreText.text = $"{ScoreKeeper.GetScoreKeeper().Score}";
    }

    public void SetScoreToZero()
    {
        ScoreKeeper.GetScoreKeeper().Score = 0;
    }
}
