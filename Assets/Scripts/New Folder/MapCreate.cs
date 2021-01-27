using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Scripting;

public class MapCreate : MonoBehaviour
{
    [SerializeField] GameObject tail = null;
    [SerializeField] GameObject tail0 = null;
    [SerializeField] GameObject tail1 = null;
    [SerializeField] GameObject tail2 = null;

    List<GameObject> gameObjects = new List<GameObject>();
    List<int> travlRoute = new List<int>();

    Vector3 a = new Vector3(5, 0, 5);

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int k = 0; k < 10; k++)
            {
                GameObject obj;

                //int random = Random.Range(0, 14);
                int random = 10;
                Vector3 position = new Vector3(i, 0f, k);
                if (random >= 0 && random <= 3)
                {
                    obj = Instantiate(tail0, position, Quaternion.identity);
                }
                else if(random <= 5 && random >= 4)
                {
                    obj = Instantiate(tail1, position, Quaternion.identity);
                }
                else if (random == 6)
                {
                    obj = Instantiate(tail2, position, Quaternion.identity);
                }
                else
                {
                    obj = Instantiate(tail, position, Quaternion.identity);
                }

                gameObjects.Add(obj);
            }
        }
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Movable(a, 4);
            
        }
    }

    /// <summary>
    /// x軸とｚ軸が同じ地面のオブジェクトを返す
    /// </summary>
    /// <param name="Position">座標</param>
    /// <returns>地面のオブジェクト</returns>
    GameObject GroundPosition(Vector3 position)
    {
        int xz = GroundPositionNum(position);

        if (xz > -1)
        {
            return gameObjects[xz];
        }
        return null;

        //int x = (int)Position.x;
        //int z = (int)Position.z;
        //string xzStr = x.ToString() + z.ToString();

        //int xz;
        //if (int.TryParse(xzStr, out xz))
        //{
        //    if (xz >= 0 && xz <= 100)
        //    {
        //        return gameObjects[xz];
        //    }
        //}
        //return null;
    }

    /// <summary>
    /// x軸とｚ軸が同じ地面のオブジェクトのlistナンバーを返す
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    int GroundPositionNum(Vector3 position)
    {
        int x = (int)position.x;
        int z = (int)position.z;
        string xzStr = x.ToString() + z.ToString();

        int xz;
        if (int.TryParse(xzStr, out xz))
        {
            if (xz >= 0 && xz <= 100)
            {
                return xz;
            }
        }
        return -100;
    }

    /// <summary>
    /// キャラの動ける範囲を表示する
    /// </summary>
    /// <param name="chara">キャラの座標</param>
    /// <param name="moveDist">移動可能距離</param>
    void Movable(Vector3 chara, int moveDist)
    {
        int aaa;
        Vector3[] vectors = new Vector3[]
        {
            new Vector3(chara.x + 1, chara.y, chara.z),
            new Vector3(chara.x - 1, chara.y, chara.z),
            new Vector3(chara.x, chara.y, chara.z + 1),
            new Vector3(chara.x, chara.y, chara.z - 1)
        };


        for (int i = 0; i < vectors.Length; i++)
        {
            GameObject obj = GroundPosition(vectors[i]);
            if (obj)
            {
                Cell cell = obj.GetComponent<Cell>();
                int dist = moveDist - cell.type;

                if (dist >= 0)
                {
                    aaa = GroundPositionNum(vectors[i]);
                    travlRoute.Add(aaa);
                    //cell.MoveTrue(travlRoute);
                    if (dist > 0)
                    {
                        Movable(obj.transform.position, dist, i);
                    }
                }
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chara"></param>
    /// <param name="moveDist"></param>
    /// <param name="direction"></param>
    void Movable(Vector3 chara, int moveDist, int direction)
    {

        List<Vector3> vector3s = new List<Vector3>()
        {
            new Vector3(chara.x + 1, chara.y, chara.z),
            new Vector3(chara.x - 1, chara.y, chara.z),
            new Vector3(chara.x, chara.y, chara.z + 1),
            new Vector3(chara.x, chara.y, chara.z - 1)
        };
        List<int> k = new List<int>()
        {
            0, 1, 2, 3
        };
        //前回から来た方向を除く
        if (direction == 0)
        {
            vector3s.RemoveAt(1);
            k.RemoveAt(1);
        }
        else if(direction == 1)
        {
            vector3s.RemoveAt(0);
            k.RemoveAt(0);
        }
        else if (direction == 2)
        {
            vector3s.RemoveAt(3);
            k.RemoveAt(3);
        }
        else
        {
            vector3s.RemoveAt(2);
            k.RemoveAt(2);
        }

        for (int i = 0; i < vector3s.Count; i++)
        {
            GameObject obj = GroundPosition(vector3s[i]);
            if (obj)
            {
                Cell cell = obj.GetComponent<Cell>();

                int dist = moveDist - cell.type;

                if (dist >= 0)
                {
                    int a = GroundPositionNum(vector3s[i]);
                    travlRoute.Add(a);
                    //cell.MoveTrue(travlRoute);
                    if (dist > 0)
                    {
                        Movable(obj.transform.position, dist, k[i]);
                    }
                }
            }
            travlRoute.RemoveAt(travlRoute.Count - 1);
        }

    }
    /// <summary>
    /// 移動経路を求める
    /// </summary>
    /// <param name="position">キャラの現在地</param>
    /// <param name="destination">目的地</param>
    /// <param name="moveDist">キャラの移動可能距離</param>
    public void TravlRoute(Vector3 position, Vector3 destination, int moveDist)
    {
        //最短
        Vector3 difference = position - destination;
        int dist = 0;

        if (difference.x > 0)
        {
            for (int i = 0; i < difference.x; i++)
            {
                position += Vector3.right;
                GameObject obj = GroundPosition(position);
                Cell cell = GetComponent<Cell>();
                dist += cell.type;
            }
        }
        else if (difference.x < 0)
        {
            for (int i = 0; i > difference.x; i--)
            {
                position += Vector3.left;
                GameObject obj = GroundPosition(position);
                Cell cell = GetComponent<Cell>();
                dist += cell.type;
            }
        }

        if (difference.z > 0)
        {
            for (int i = 0; i < difference.x; i++)
            {
                position += Vector3.forward;
                GameObject obj = GroundPosition(position);
                Cell cell = GetComponent<Cell>();
                dist += cell.type;
            }
        }
        else if (difference.z< 0)
        {
            for (int i = 0; i > difference.x; i--)
            {
                position += Vector3.back;
                GameObject obj = GroundPosition(position);
                Cell cell = GetComponent<Cell>();
                dist += cell.type;
            }
        }






        //最短でいけない場合
        Vector3[] vectors = new Vector3[]
        {
            new Vector3(position.x + 1, position.y, position.z),
            new Vector3(position.x - 1, position.y, position.z),
            new Vector3(position.x, position.y, position.z + 1),
            new Vector3(position.x, position.y, position.z - 1)
        };

        for (int i = 0; i < moveDist; i++)
        {
            
        }

        for (int i = 0; i < vectors.Length; i++)
        {
            GameObject obj = GroundPosition(vectors[i]);
            Cell cell = obj.GetComponent<Cell>();

            //if (cell.onOff)
            //{

            //}
        }
    }
}
