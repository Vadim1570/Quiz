using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionCheckOneAnswear : MonoBehaviour
{
    public GameObject RightAnswear;
    public GameObject SelectedAnswear;
    public GameObject[] Answears;

    public void SetAnswear(GameObject answear)
    {
        this.SelectedAnswear = answear;
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        StartCoroutine(SaveAnswearAndLoadSceneAsync(sceneName));
    }

    public IEnumerator SaveAnswearAndLoadSceneAsync(string sceneName)
    {
        foreach(var a in Answears)
            a.GetComponent<Button>().interactable = false;

        //RightAnswear.GetComponent<Button>().interactable = false;
        //SelectedAnswear.GetComponent<Button>().interactable = false;

        if(SelectedAnswear == RightAnswear )
            ScoreKeeper.GetScoreKeeper().Score += 1;

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}