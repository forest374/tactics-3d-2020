using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara : MonoBehaviour
{
    GameObject human = null;
    GameObject getGameObj = null;
    
    public bool charaMove = false;
    public bool attack = false;
    public bool myTurn = false;
    Turn turn;

    void Start()
    {
    }


    void Update()
    {
        turn = (Turn)GameManager.turnR;
        if (turn == Turn.MyTurn)
        {
            if (!myTurn)
            {
                charaMove = true;
                attack = true;
                myTurn = true;
            }
        }
        else
        {
            if (myTurn)
            {
                myTurn = false;
            }
        }

    }

    public void CharaChoice(GameObject hitObj)
    {
        human = hitObj;
    }
    /// <summary>
    /// 攻撃した
    /// </summary>
    public void Attack()
    {
        attack = false;
    }
    /// <summary>
    /// 移動した
    /// </summary>
    public void Move()
    {
        charaMove = false;
    }

}
