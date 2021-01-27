using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAAA : MonoBehaviour
{
    [SerializeField] public int type = 1;
    [SerializeField] GameObject obj = null;
    public bool onOff = false;
    List<GameObject> lood = new List<GameObject>();
    List<int> travlRoute = new List<int>();
    public List<int> travlRouteA = new List<int>();
    void Start()
    {
        travlRouteA = travlRoute;
    }


    public void MoveTrue(List<int> route)
    {
        if (travlRoute.Count > route.Count)
        {
            travlRoute = route;
        }
        if (!onOff)
        {
            onOff = true;
            Vector3 position = this.transform.position;
            position.y += 0.01f;
            Instantiate(obj, position, Quaternion.identity);
        }
    }

    public void Call()
    {
        Debug.Log(lood.Count);
        foreach (var item in lood)
        {
            Debug.Log(item.transform.position);
        }
    }
}
