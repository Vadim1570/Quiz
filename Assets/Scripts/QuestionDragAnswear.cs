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
    }

    public void OnAnswearDrag(BaseEventData eventData)
    {
        selectedStamp = eventData.selectedObject;
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
        bool isAnswearCorrect = true;

        foreach(var put in alreadyPutStamps)
        {
            var image = put.Stamp1.GetComponent<Image>();
            //Каждую связанную пару точек, проверим в массиве правильных ответов
            if(rightAnswears.Count(right => right.Stamp1 == put.Stamp1 && right.Stamp2 == put.Stamp2) == 0)
            {
                image.color = new Color(183f/255f,80f/255f,84f/255f);  
            }
            else
            {
                isAnswearCorrect = false;
                image.color = new Color(178f/255f,209f/255f,121f/255f);
            }
        }

        if(isAnswearCorrect)
        {
            ScoreKeeper.GetScoreKeeper().Score += 1;
        }

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(sceneName);
    }
}

[System.Serializable]
public class StampPair {
    public GameObject Stamp1;
    public GameObject Stamp2;
}