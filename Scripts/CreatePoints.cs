using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePoints : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject point;
    public GameObject[] pointsArray;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int i = (int) Math.Round(pz.x / 2);
            if( i > 0 && i <= pointsArray.Length)  // pointsArray[2].transform.position is (3, x, x)
            {
                Debug.Log(pz);
                pz.x = (float)i * 2;
                pz.z = 0f;
                if (pointsArray[i-1] == null)
                {
                    pointsArray[i-1] = Instantiate(point);
                }
                pointsArray[i-1].transform.position = pz;
            }
            else
            {
                Debug.Log("in failed coord");
            }

        }
    }
}
