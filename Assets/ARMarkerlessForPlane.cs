using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 터치를 했을때 부딪힌 곳이 있다면 그곳에 물체를 배치하고싶다.
public class ARMarkerlessForPlane : MonoBehaviour
{
    public GameObject unitFactory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 터치를 했을때 부딪힌 곳이 있다면 그곳에 물체를 배치하고싶다.

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                GameObject unit = Instantiate(unitFactory);
                    unit.transform.position = hitInfo.point;
                    unit.transform.up = hitInfo.normal;
                }


            }
        }

    }
}
