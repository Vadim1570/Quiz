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
    public Material AnswearMaterial;

    public void SetAnswear(GameObject selectedAnswear)
    {
        this.SelectedAnswear = selectedAnswear;
        foreach(var answear in Answears)
        {
            var image = answear.GetComponent<Image>();
            if(answear == selectedAnswear)
            {
                image.material = AnswearMaterial;
                image.color = Color.yellow;  
            }
            else
            {
                image.material = null;
                image.color = Color.white;  
            } 
        }
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        StartCoroutine(SaveAnswearAndLoadSceneAsync(sceneName));
    }

    public IEnumerator SaveAnswearAndLoadSceneAsync(string sceneName)
    {
        //
        foreach(var answear in Answears)
        {
            var image = answear.GetComponent<Image>();
            image.material = AnswearMaterial;
            if(answear == RightAnswear)
               image.color = Color.green;
            else
               image.color = Color.red;  
        }
            

        //RightAnswear.GetComponent<Button>().interactable = false;
        //SelectedAnswear.GetComponent<Button>().interactable = false;

        if(SelectedAnswear == RightAnswear )
            ScoreKeeper.GetScoreKeeper().Score += 1;

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }
}