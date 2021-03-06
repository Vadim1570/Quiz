using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=KHZFpRL3Xzc
public class QuestionDragAnswear : MonoBehaviour
{    //Заранее заполненные правильные ответы
    public StampPair [] rightAnswears;
    public GameObject selectedStamp;
    
    private SoundBt audioClick_SoundBt;
    private SoundBt audioPoem_SoundBt;

    //Сюда запоминаем уже положенные ответы между собой точки
    public List<StampPair> alreadyPutStamps = new List<StampPair>();    
    #region Private methods
    private bool IsAlreadyLinked(GameObject stamp)
    {
        foreach(var pair in alreadyPutStamps)
        {
            if(stamp == pair.Stamp1 || stamp == pair.Stamp2)
            return true;
        }
        return false;
    }

    private void RemoveFromAlreadyLinked(GameObject stamp)
    {
        var pairToRemove = alreadyPutStamps.FirstOrDefault(pair => stamp == pair.Stamp1 || stamp == pair.Stamp2);
        if (pairToRemove != null)
            alreadyPutStamps.Remove(pairToRemove);
    }
    #endregion

    void Start()
    {
        this.audioClick_SoundBt = GameObject.Find("audioClick").GetComponent<SoundBt>();
        this.audioPoem_SoundBt = GameObject.Find("audioPoem").GetComponent<SoundBt>();
    }

    public void OnAnswearDrag(BaseEventData eventData)
    {
        selectedStamp = eventData.selectedObject;
    }
    public void OnPointerDown(BaseEventData eventData)
    {
        audioClick_SoundBt.ClickSound();
    }

    void Update()
    {
        //Забываем о выбранном элементе пазла
        if(Input.GetMouseButtonUp(0))
        {
            
            //Магнитим выбранный элемент пазла
            if(selectedStamp != null)
            {
                RemoveFromAlreadyLinked(selectedStamp);
                foreach(var rightStamp in rightAnswears)
                {
                    var holeStamp = rightStamp.Stamp2;
                    
                    if(Vector2.Distance(selectedStamp.transform.position, holeStamp.transform.position) < 0.2f)
                    {
                        selectedStamp.transform.position = holeStamp.transform.position;
                        
                        alreadyPutStamps.Add(new StampPair() { Stamp1 = selectedStamp, Stamp2 = holeStamp});
                        audioClick_SoundBt.ChoicePlace();
                    }
                }
            }

            selectedStamp = null;
        }

        if(selectedStamp != null)
        {
            //Двиагем выбранный элемент пазла
            var mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedStamp.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0f);
        }
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        #region Блокируем все элементы на форме и останавливаем плеер
        audioPoem_SoundBt.PoemSoundStop();
        var OK = EventSystem.current.currentSelectedGameObject;
        if(OK != null)
            OK.GetComponent<Button>().enabled = false;
        foreach(var pair in rightAnswears)
        {
            if(pair.Stamp1 != null)
                pair.Stamp1.GetComponent<Button>().enabled = false;
        }
        #endregion 

        //Подсветим поставленные значения
        foreach(var put in alreadyPutStamps)
        {
            var image = put.Stamp1.GetComponent<Image>();
            //Каждую связанную пару точек, проверим в массиве правильных ответов
            if(rightAnswears.Count(right => right.Stamp1 == put.Stamp1 && right.Stamp2 == put.Stamp2) != 0)
            {
                image.color = new Color(178f/255f,209f/255f,121f/255f);//green 
            }
            else
            {
                image.color = new Color(183f/255f,80f/255f,84f/255f);//red
            }
        }

        if(rightAnswears.Count() == alreadyPutStamps.Sum(put => rightAnswears.Count(right => right.Stamp1 == put.Stamp1 && right.Stamp2 == put.Stamp2)))
        {
            ScoreKeeper.GetScoreKeeper().Score += 1;
            StartCoroutine(audioClick_SoundBt.winSound());
        }
       else {StartCoroutine(audioClick_SoundBt.errorSound());}

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }
}

[System.Serializable]
public class StampPair {
    public GameObject Stamp1;
    public GameObject Stamp2;
}