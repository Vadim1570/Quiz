using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    public UnityEngine.UI.Text YourScoreText;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
        YourScoreText.text = $"Ваш счет: {ScoreKeeper.GetScoreKeeper().Score}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}