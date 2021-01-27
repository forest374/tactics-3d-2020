using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPoint : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))//Rayを飛ばす
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))//Rayが何かに当たった場合
            {
                //this.transform.position = hit.point;
                //Debug.Log(hit.point);
                int x = (int)hit.point.x;
                int z = (int)hit.point.z;
                Vector3 fixPoint = new Vector3(x + 0.5f, hit.point.y, z + 0.5f);
                this.transform.position = fixPoint;
                Debug.Log(hit.transform.position);
            }
        }
        
    }
}
