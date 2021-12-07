using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class SpriteOnVisible : MonoBehaviour
{
    public GameObject [] targetObjects;
    
    public int k=0;
   
    void Start()
    {
        k=0;
        foreach(var targetObject in targetObjects)
        {
            targetObject.SetActive(false);
        }
    }

    public void OnButtonClick(GameObject targetObject)
    {
        k++;
        targetObject.SetActive(true);
        Debug.Log(targetObject.name);
        
        //StartCoroutine(GameObject.Find("audioClick").GetComponent<SoundBt>().ClickSound());
        GameObject.Find("audioClick").GetComponent<SoundBt>().ClickSound();
        var lr = GameObject.Find("star"+k.ToString());
        lr.GetComponent<SpriteRenderer>().color = new Color(178f/255f,209f/255f,121f/255f);
        ScoreKeeper.GetScoreKeeper().Score += 1;

        var bt = EventSystem.current.currentSelectedGameObject;
        bt.GetComponent<Button>().enabled = false;
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        
        var OK = EventSystem.current.currentSelectedGameObject;
        if(OK != null) { OK.GetComponent<Button>().enabled = false;}
         StartCoroutine(LoadSceneAsync(sceneName));
         if (k==5) {StartCoroutine(GameObject.Find("audioClick").GetComponent<SoundBt>().winSound());}
         else {StartCoroutine(GameObject.Find("audioClick").GetComponent<SoundBt>().errorSound());}
        // ScoreKeeper.GetScoreKeeper().Score -= 1;
         
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }

}

