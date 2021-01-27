using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class MousePoint : MonoBehaviour
{
    [SerializeField] GameObject mouseCursor = null;
    [SerializeField] GameObject mouseCursorBlue = null;
    [SerializeField] GameObject mouseCursorRed = null;
    [SerializeField] GameObject human = null;
    [SerializeField] GameObject charaButton = null;
    [SerializeField] GameObject returnButton = null;
    [SerializeField] GameObject buttonMove = null;
    [SerializeField] GameObject ButtonAttack = null;

    [SerializeField] GameObject gameManager = null;
    GameManager gMScript;
    [SerializeField] GameObject charaManager = null;
    PickUp pickUpScript;

    GameObject hitObj;
    Chara chara;
    bool charaPoint = false;
    bool moveButton = false;

    void Start()
    {
        gMScript = gameManager.GetComponent<GameManager>();
        pickUpScript = charaManager.GetComponent<PickUp>();
    }


    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))//Rayが何かに当たった場合 （レイヤーがIgoneRaycastの物は当たらない）
        {
            hitObj = hit.collider.gameObject;
            chara = hitObj.GetComponent<Chara>();
            int x = (int)hit.point.x;//マスの中央に来るようにする
            int z = (int)hit.point.z;

            if (hitObj.layer == 9)//マウスをcharaにあてた時
            {
                if (mouseCursor)//charaのしたに表示する
                {
                    charaPoint = true;
                    //Vector3 mouseCursorP = new Vector3(hitObj.transform.position.x, hitObj.transform.position.y - 0.5f, hitObj.transform.position.z);
                    Vector3 mouseCursorP = hitObj.transform.position;
                    mouseCursorP.y += 0.01f;
                    mouseCursor.transform.position = mouseCursorP;
                }

                if (Input.GetButtonDown("Fire1"))//test
                {
                    charaButton.SetActive(true);
                    Action();

                    pickUpScript.GetChara(hitObj);
                    chara.CharaChoice(hitObj);
                    MoveButtonFalse();
                }
            }
            else
            {
                if (mouseCursor)//現在のマウスの位置
                {
                    charaPoint = false;
                    Vector3 mouseCursorP = new Vector3(x, hit.point.y + 0.01f, z);
                    mouseCursor.transform.position = mouseCursorP;
                }

                if (Input.GetButtonDown("Fire1") && moveButton)//clickしたとき移動する
                {
                    Vector3 movePoint = new Vector3(x, hit.point.y, z);
                    pickUpScript.CharaMove(movePoint);
                    buttonMove.GetComponent<Button>().interactable = false;
                    //human.transform.position = movePoint;
                    //Debug.Log(hit.transform.position);
                }
            }
        }

        //Charaにマウスを当てた時に色を変える
        if (mouseCursorBlue && mouseCursorRed)
        {
            if (charaPoint)
            {
                mouseCursorBlue.SetActive(false);
                mouseCursorRed.SetActive(true);
            }
            else
            {
                mouseCursorBlue.SetActive(true);
                mouseCursorRed.SetActive(false);
            }
        }
    }

    /// <summary>
    /// moveボタンを押す
    /// </summary>
    public void MoveButton()
    {
        moveButton = true;
    }
    /// <summary>
    /// moveボタンを押されてないことにする
    /// </summary>
    public void MoveButtonFalse()
    {
        moveButton = false;
    }

    /// <summary>
    /// キャラ選択前に戻る
    /// </summary>
    public void Return()
    {
        charaButton.SetActive(false);
    }

    /// <summary>
    /// キャラのできる行動をボタンに表す
    /// </summary>
    void Action()
    {
        if (!chara.charaMove)
        {
            buttonMove.GetComponent<Button>().interactable = false;
        }
        else
        {
            buttonMove.GetComponent<Button>().interactable = true;
        }
        if (!chara.attack)
        {
            ButtonAttack.GetComponent<Button>().interactable = false;
        }
        else
        {
            ButtonAttack.GetComponent<Button>().interactable = true;
        }
    }

    /// <summary>
    /// ターンチェンジ
    /// </summary>
    public void TurnChange()
    {
        moveButton = false;
    }
}
