using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ダイクストラ法のわかりやすい解説 https://www.youtube.com/watch?v=X1AsMlJdiok
public class Dijkstra : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] int playerMove = 3;

    [SerializeField] GameObject map = null;
    Map mapScript;

    List<GameObject> route = new List<GameObject>();
    List<GameObject> confirmRoute = new List<GameObject>();
    int distance = 0;

    void Start()
    {
        mapScript = map.GetComponent<Map>();
    }


    void Update()
    {
    }

    public void DijkstraOn()
    {
        Debug.Log("start");
        //1 始点に0を書き込む
        GameObject startPoint = mapScript.GroundCell(player.transform.position);
        route.Add(startPoint);
        Cell cell = startPoint.GetComponent<Cell>();
        cell.DistUpdate(0, route);
        // 始点を確定させる
        cell.Confirm();

        Vector3 point = startPoint.transform.position;
        List<GameObject> ground = new List<GameObject>(); // 未確定のものしか入れない
        //List<GameObject> confirmPoint = new List<GameObject>();
        GameObject confirmPoint = startPoint;
        confirmRoute.Add(startPoint);
        DikstraWhile(point, 0, 999);
    }

    void DikstraWhile(Vector3 point, int distance, int iii)
    {
        if (iii != 999)
        {
            confirmRoute.RemoveAt(iii);
        }
        Cell cell;
        //Vector3 point = startPoint.transform.position;
        List<GameObject> ground = new List<GameObject>(); // 未確定のものしか入れない
        //3 2で確定した地点(始点)から直接繋がっていて、かつ未確定な地点に対し距離を求め小さければ更新する
        ground.Clear();
        Debug.Log("ground" + ground.Count);
        Vector3[] vectors = new Vector3[]
        {
            Vector3.right, Vector3.left, Vector3.forward, Vector3.back
        };
        int beforeDistance = distance;
        for (int i = 0; i < 4; i++)
        {
            GameObject aaa = mapScript.GroundCell(point + vectors[i]);
            Debug.Log(point + vectors[i]);
            if (aaa)
            {
                cell = aaa.GetComponent<Cell>();
                if (!cell.confirm)
                {
                    ground.Add(aaa);
                    distance = beforeDistance;
                    distance += cell.type;
                    // 移動力を超えた場合
                    Debug.Log("distance: " + distance);
                    if (distance > playerMove)
                    {
                        Debug.Log("Over");
                        ground.Remove(aaa);
                    }
                    else
                    {
                        cell.DistUpdate(distance, route);
                    }
                }
                else
                {
                    Debug.Log("confirm");
                }
            }
            else
            {
                Debug.Log("null");
            }
        }

        //Debug.Log(ground.Count);
        if (confirmRoute.Count == 0)
        {
            Debug.Log("break");
        }
        else
        {
            //2 未確定の中で一番小さい値を持つ１つを確定させる
            int min = int.MaxValue;
            for (int i = 0; i < ground.Count; i++)
            {
                cell = ground[i].GetComponent<Cell>();
                confirmRoute.Add(ground[i]);
                int a = cell.MovingDistance();
                if (min > a)
                {
                    min = a;
                }
            }
            for (int i = 0; i < confirmRoute.Count; i++)
            {
                cell = confirmRoute[i].GetComponent<Cell>();
                int a = cell.MovingDistance();
                if (min == a)
                {
                    GameObject confirmPoint = confirmRoute[i];// おかしい
                    point = confirmPoint.transform.position; // 確定した位置
                    Debug.Log("confirmPoin" + point);
                    cell = confirmPoint.GetComponent<Cell>();
                    cell.Confirm();
                    cell.CanMove();
                    distance = cell.MoveingDistance();
                    if (distance == int.MaxValue)
                    {
                        distance = 0;
                    }
                    DikstraWhile(point, distance, i);
                }
            }
        }
        
    }
}
