using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalSceneManager : MonoBehaviour
{
    public UnityEngine.UI.Text YourScoreText;

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
