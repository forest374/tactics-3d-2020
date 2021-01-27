using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Turn
{
    MyTurn, EnemyTurn, TurnChange
}
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject charaButton = null;
    [SerializeField] GameObject mouseManager = null;
    MousePoint mousePoint;

    //GameObject human = null;
    //GameObject getChara = null;

    Turn turn = Turn.MyTurn;
    Turn turnRecord;
    public static int turnR;
    void Start()
    {
        turnRecord = turn;
        turnR = (int)turn; 
        mousePoint = mouseManager.GetComponent<MousePoint>();
    }


    void Update()
    {
        switch (turn)
        {
            case Turn.MyTurn:
                break;

            case Turn.EnemyTurn:

                TurnEnd();
                break;

            case Turn.TurnChange:

                if (turnRecord == Turn.MyTurn)
                {
                    Debug.Log("敵のターン");
                    mousePoint.TurnChange();
                    turnRecord = Turn.EnemyTurn;
                    turnR = (int)Turn.EnemyTurn;
                    turn = Turn.EnemyTurn;
                }
                else if (turnRecord == Turn.EnemyTurn)
                {
                    Debug.Log("自分のターン");
                    turnRecord = Turn.MyTurn;
                    turnR = (int)Turn.MyTurn;
                    turn = Turn.MyTurn;
                }
                break;

            default:
                break;
        }

    }


    /// <summary>
    /// ターン終了   
    /// </summary>
    public void TurnEnd()
    {
        Debug.Log("終わり");
        turn = Turn.TurnChange;
    }


}
