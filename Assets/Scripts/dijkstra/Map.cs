using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] GameObject[] block = null;
    List<GameObject> objs = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                int type = Random.Range(0, 10);
                if (type > 2)
                {
                    type = 0;
                }
                else if (type == 0)
                {
                    type = 2;
                }
                else
                {
                    type = 1;
                }
                GameObject obj = Instantiate(block[type], new Vector3(i, 0, k), Quaternion.identity);
                objs.Add(obj);
            }
        }
    }


    void Update()
    {

    }
    /// <summary>
    /// positionを受け取り x,z軸が同じ地面のオブジェクトを返す
    /// </summary>
    /// <param name="position">座標</param>
    /// <returns>座標とx,z軸が同じ地面のオブジェクト</returns>
    public GameObject GroundCell(Vector3 position)
    {
        position.y = 0;
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].transform.position == position)
            {
                return objs[i];
            }
        }
        return null;
    }
}
