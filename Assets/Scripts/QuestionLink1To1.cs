using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionLink1To1 : MonoBehaviour
{
    public string questionUniqueId;

    public Camera mainCamera;

    //Заранее заполненные правильные ответы
    public PointPair [] rightLinks;

    //Сюда запоминаем точки, которые пытаються связать между собой
    private GameObject point1 = null;
    private GameObject point2 = null;
    
    //Сюда запоминаем уже связанные между собой точки
    private List<PointPair> alreadylinked = new List<PointPair>();
    private bool isAlreadyLinked(GameObject point)
    {
        foreach(var pair in alreadylinked)
        {
            if(point == pair.Point1 || point == pair.Point2)
            return true;
        }
        return false;
    }

    private void removefromAlreadyLinked(GameObject point)
    {
        var pairToRemove = alreadylinked.FirstOrDefault(pair => point == pair.Point1 || point == pair.Point2);
        if (pairToRemove != null)
            alreadylinked.Remove(pairToRemove);
    }
    private void Awake()
    {
    }

    void Start()
    {
    }

    public void linkpoint_OnPointerClick(GameObject point)
    {
        //При нажатии на первую точку, включим рисовалку линии
        if(point1 == null)
        {
            point1 = point;
            point1.GetComponent<LineRenderer>().enabled = true;
            removefromAlreadyLinked(point1);
        }
        else //У второй точки, отключим рисовалку линии
        if(!isAlreadyLinked(point))
        {
            point2 = point;
            point2.GetComponent<LineRenderer>().enabled = false;
        }

        //Если привязка двух точек произошла, то начертим линию между ними
        if(point1 != null && point2 != null)
        {
            var point1_lr = point1.GetComponent<LineRenderer>();
            point1_lr.SetPosition(0, point1.GetComponent<Transform>().position);
            point1_lr.SetPosition(1, point2.GetComponent<Transform>().position);

            alreadylinked.Add(new PointPair() { Point1 = point1, Point2 = point2});
            point1 = null;
            point2 = null;
        }
    }

    void Update()
    {
        //Если во время перетаскивания нажали прав.кнопку.мыши, вернем линию в точку
        if (Input.GetButtonDown("Fire2"))
        {
            if(point1 != null && point2 == null)
            {
                point1.GetComponent<LineRenderer>().SetPosition(1, point1.GetComponent<Transform>().position);
                point1 = null;
            }
                
        }
        
        //Анимация перетаскивания линии за курсором мыши
        if(point1 != null && point2 == null)
        {
            var point1_tr = point1.GetComponent<Transform>();
            var point1_lr = point1.GetComponent<LineRenderer>();
            
            point1_lr.positionCount = 2;
            var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            point1_lr.SetPosition(0, point1_tr.position);
            point1_lr.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
        }
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        bool isAnswearCorrect = true;

        foreach(var pair in alreadylinked)
        {
            foreach(var rpair in rightLinks)
            {
                bool zacepil = pair.Point1 == rpair.Point2 || pair.Point1 == rpair.Point2;
                bool vitanul = pair.Point2 == rpair.Point1 || pair.Point2 == rpair.Point1;
                if(zacepil && !vitanul)
                    isAnswearCorrect = false;
            }
        }

        if(isAnswearCorrect)
        {
            //var gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
            //gameManager.IncreaseScore(1);
            ScoreKeeper.GetScoreKeeper().Score += 1;
        }

        SceneManager.LoadScene(sceneName);
    }
}

[System.Serializable]
public class PointPair {
    public GameObject Point1;
    public GameObject Point2;
}
