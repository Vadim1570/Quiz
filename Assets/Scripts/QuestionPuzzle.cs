using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionPuzzle : MonoBehaviour
{
    public List<PieceWithProperty> piecesWithProperty = new List<PieceWithProperty>();
    public GameObject selectedPiece;
    public PieceWithProperty selectedPieceProp;

    void Start()
    {
        foreach(var piece in  GameObject.FindGameObjectsWithTag("Puzzle"))
        {
            //Запоминаем как расставлены кусочки пазла
            piecesWithProperty.Add(new PieceWithProperty() { pieceGameObject = piece, rightPosition = piece.transform.position });
            //Разбросать элементы пазла
            piece.transform.position = new Vector3(Random.Range(-2.18f, 1.32f), Random.Range(-2.72f, -3.72f));
        }
    }

    void Update()
    {
        //Запоминаем выбранный элемент пазла
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.transform != null && hit.transform.CompareTag("Puzzle"))
            {
                selectedPiece = hit.transform.gameObject;
            }
        }

        //Забываем о выбранном элементе пазла
        if(Input.GetMouseButtonUp(0))
        {
            selectedPiece = null;
        }

        if(selectedPiece != null)
        {
            selectedPieceProp = piecesWithProperty.FirstOrDefault(pp => pp.pieceGameObject == selectedPiece);

            //Двиагем выбранный элемент пазла
            var mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0f);

            //Магнитим выбранный элемент пазла
            if(Vector3.Distance(selectedPiece.transform.position, selectedPieceProp.rightPosition) < 0.5f)
            {
                selectedPiece.transform.position = selectedPieceProp.rightPosition;
            }
        }
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        //Если все кусочки лежат в правильных местах, то прибавляем очко
        if(piecesWithProperty.Count() == piecesWithProperty.Count(p => p.pieceGameObject.transform.position == p.rightPosition))
            ScoreKeeper.GetScoreKeeper().Score += 1;

        SceneManager.LoadScene(sceneName);
    }
}

[System.Serializable]
public class PieceWithProperty {
    public GameObject pieceGameObject;
    public Vector3 rightPosition;
}