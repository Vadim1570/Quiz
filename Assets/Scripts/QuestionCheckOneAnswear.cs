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

        //Подсвечиваю выбранный ответ желтым, а другие ответы сбрасыаю до перв. состояния
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
        //Подветить правильные ответы зеленым, а неверные красным
        foreach(var answear in Answears)
        {
            var image = answear.GetComponent<Image>();
            image.material = AnswearMaterial;
            if(answear == RightAnswear)
               {
                   
                   image.color = new Color(178f/255f,209f/255f,121f/255f);
                }
            else
               {
                   //GameObject.Find("audioClick").GetComponent<SoundBt>().errorSound();
                   image.color = new Color(183f/255f,80f/255f,84f/255f); 
                   } 
        }

        if(SelectedAnswear == RightAnswear )
            {
            ScoreKeeper.GetScoreKeeper().Score += 1;
            StartCoroutine(GameObject.Find("audioClick").GetComponent<SoundBt>().winSound());
            }
            else{StartCoroutine(GameObject.Find("audioClick").GetComponent<SoundBt>().errorSound());}

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }
}