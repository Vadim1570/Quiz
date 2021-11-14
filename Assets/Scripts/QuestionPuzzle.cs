using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionPuzzle : MonoBehaviour
{
    GameObject SelectedPiece;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit != null && hit.transform != null &&
                hit.transform.CompareTag("Puzzle"))
            {
                SelectedPiece = hit.transform.gameObject;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            SelectedPiece = null;
        }

        if(SelectedPiece != null)
        {
            var mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0f);
        }
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        //if(AnwearInput.text.ToLower() == RightAnswear.ToLower() )
        //    ScoreKeeper.GetScoreKeeper().Score += 1;

        SceneManager.LoadScene(sceneName);
    }
}
