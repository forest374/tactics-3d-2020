using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] GameObject mouseManager = null;
    MousePoint mousePoint;

    GameObject getChara = null;
    Chara chara;
    void Start()
    {
        mousePoint = mouseManager.GetComponent<MousePoint>();
    }


    void Update()
    {
    }

    /// <summary>
    /// 選択したキャラをgetCharaに保存する
    /// </summary>
    /// <param name="obj">選択したキャラ</param>
    public void GetChara(GameObject obj)
    {
        getChara = obj;
        chara = getChara.GetComponent<Chara>();
        Debug.Log("Chara");
    }

    public void CharaMove(Vector3 movePoint)
    {
        if (chara)
        {
            if (getChara && chara.charaMove)
            {
                getChara.transform.position = movePoint;
                chara.charaMove = false;
                mousePoint.MoveButtonFalse();
            }
            else
            {
                Debug.Log("a");
            }
        }
    }

}
