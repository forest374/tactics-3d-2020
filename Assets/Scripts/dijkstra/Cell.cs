using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] GameObject canMove = null;
    [SerializeField] public int type = 1;
    int movingDistance = int.MaxValue;
    public bool confirm = false;
    List<GameObject> route = new List<GameObject>();


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    /// <summary>
    /// 移動距離とルートを受け取り距離が小さかったら更新する
    /// </summary>
    /// <param name="n">移動距離</param>
    /// <param name="num">ルート</param>
    public void DistUpdate(int n, List<GameObject> num)
    {
        if (movingDistance > n)
        {
            movingDistance = n;
            route = num;
        }
    }

    /// <summary>
    /// 確定する
    /// </summary>
    public void Confirm()
    {
        confirm = true;
    }

    /// <summary>
    /// ここまでの移動距離を返す
    /// </summary>
    /// <returns>移動距離</returns>
    public int MovingDistance()
    {
        return movingDistance;
    }

    public void CanMove()
    {
        Vector3 a = transform.position + Vector3.up * 0.51f;
        GameObject aaaa = Instantiate(canMove, a, Quaternion.identity);
    }
    public void Call()
    {
        Debug.Log("movingDistance" + movingDistance);
        Debug.Log("confirm" + confirm);

        Debug.Log("route");
        for (int i = 0; i < route.Count; i++)
        {
            Debug.Log(route[i].transform.position);
        }
    }
}
