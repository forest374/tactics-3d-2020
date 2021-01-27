using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    GameObject charaObj = null;
    Charactor charactorScript;
    void Start()
    {
    }


    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            

            if (Input.GetButtonDown("Fire1"))
            {
                //キャラクターをクリックしたとき
                if (hitObject.layer == 9)
                {
                    charaObj = hitObject;
                    charactorScript = charaObj.GetComponent<Charactor>();
                    Debug.Log("a");
                }

                //地面をクリックしたとき
                if (hitObject.layer == 8)
                {
                    Debug.Log("jimen");
                    if (charaObj)
                    {
                        charactorScript.MovePoint(hit.point);
                    }
                    Cell cell = hitObject.GetComponent<Cell>();
                    //if (cell.onOff)
                    //{
                    //    Debug.Log(hitObject.transform.position);
                    //    for (int i = 0; i < cell.travlRouteA.Count; i++)
                    //    {
                    //        Debug.Log(cell.travlRouteA[i]);
                    //    }
                    //}
                }
            }
        }
    }
}
