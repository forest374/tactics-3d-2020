using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stra : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject mapObj = null;
    Map map;

    [SerializeField] int playerMove = 3;
    List<GameObject> route = new List<GameObject>();
    List<GameObject> confirmRoute = new List<GameObject>();
    List<GameObject> confirmGround = new List<GameObject>();
    GameObject obj = null;

    Vector3[] vectors = new Vector3[]
    {
            Vector3.right, Vector3.left, Vector3.forward, Vector3.back
    };

    void Start()
    {
        map = mapObj.GetComponent<Map>();
    }

    void Update()
    {
        
    }

    public void Dijkstra()
    {
        //1 始点に0を書き込む
        //player の位置の下のground を求める
        GameObject startPoint = map.GroundCell(player.transform.position);

        Cell cell = startPoint.GetComponent<Cell>();
        cell.DistUpdate(0, confirmRoute);
        route.Add(startPoint);
        cell.Call();
        //cell.CanMove();
    }
}
