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
        //
        foreach(var answear in Answears)
        {
            //var button = answear.GetComponent<Button>();
            //button.interactable = false;
            //var colors = button.colors;
            //if(answear == RightAnswear)
            //    colors.normalColor = Color.green;
            //else
            //    colors.normalColor = Color.red;  
            //button.colors = colors;
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