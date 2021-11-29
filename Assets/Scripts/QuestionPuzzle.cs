using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=KHZFpRL3Xzc
public class QuestionPuzzle : MonoBehaviour
{
    public UnityEngine.UI.InputField AnwearInput;
    public string RightAnswear;
    public Material AnswearMaterial;
    public List<PieceWithProperty> piecesWithProperty = new List<PieceWithProperty>();
    public GameObject selectedPiece;
    int OIL =1;
    public bool Selected;

    void Start()
    {
        //Прячем тестовое поле
        AnwearInput.gameObject.SetActive(false);

        foreach(var piece in  GameObject.FindGameObjectsWithTag("Puzzle"))
        {
            //Запоминаем как расставлены кусочки пазла
            piecesWithProperty.Add(new PieceWithProperty() { pieceGameObject = piece, rightPosition = piece.transform.position });
            //Разбросать элементы пазла
            piece.transform.position = new Vector3(Random.Range(-2.18f, 3f), Random.Range(-2.42f, -1f));
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
                Selected = true;
                selectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                OIL++;
                 Debug.Log(selectedPiece.transform.position);

            }
        }

        //Забываем о выбранном элементе пазла
        if(Input.GetMouseButtonUp(0))
        {
            Selected = false;
            selectedPiece = null;
        }

        if(selectedPiece != null)
        {
            var selectedPieceProp = piecesWithProperty.FirstOrDefault(pp => pp.pieceGameObject == selectedPiece);

            //Двиагем выбранный элемент пазла
            var mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0f);

            //Магнитим выбранный элемент пазла
            if(Vector3.Distance(selectedPiece.transform.position, selectedPieceProp.rightPosition) < 0.2f)
            {
                selectedPiece.transform.position = selectedPieceProp.rightPosition;
                selectedPiece.GetComponent<SortingGroup>().sortingOrder = 0;

                //Отображаем тестовое поле, если все кусочки лежат в правильных местах, то прибавляем очко
                if(piecesWithProperty.Count() == piecesWithProperty.Count(p => p.pieceGameObject.transform.position == p.rightPosition))
                {
                    //AnwearInput.ActivateInputField();
                    AnwearInput.gameObject.SetActive(true);
                }
            }
        }
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        var image = AnwearInput.GetComponent<Image>();
        image.material = AnswearMaterial;
        if(AnwearInput.text.ToLower() == RightAnswear.ToLower())
        {
            image.color = new Color(178f/255f,209f/255f,121f/255f);
            ScoreKeeper.GetScoreKeeper().Score += 1;
        }
        else
        {
            image.color = new Color(183f/255f,80f/255f,84f/255f); 
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
public class PieceWithProperty {
    public GameObject pieceGameObject;
    public Vector3 rightPosition;
}