using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using Random = UnityEngine.Random;

[System.Serializable]
public class MarkerInfo
{
    public string name;
    public GameObject obj;
}
// ARTrackedImageManager에게 추적한 이미지 정보를 얻어오고싶다.
// 그리고 추적된 이미지가 있다면 그 이미지에 해당하는 물체를 그 이미지 위치에 배치하고싶다.
public class ARMarkerMulti : MonoBehaviour
{
    ARTrackedImageManager imageManager = null;
    // 마커와 물체에 대한 목록을 만들고싶다.
    public MarkerInfo[] infos;

    // Start is called before the first frame update
    void Awake()
    {
        imageManager = gameObject.GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        imageManager.trackedImagesChanged += OnChanged;
    }
    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnChanged;
    }

    private void OnChanged(ARTrackedImagesChangedEventArgs obj)
    {
        var list = obj.updated;
        for (int i = 0; i < list.Count; i++)
        {
            // 인식된 이미지가 있는지 알고싶다.
            var marker = list[i];
            for (int j = 0; j < infos.Length; j++)
            {
                // 만약 내가 가진 목록과 동인한 마커가 있다면
                if (marker.referenceImage.name
                     == infos[j].name)
                {
                    // 그 마커가 추적중이라면
                    if (marker.trackingState == TrackingState.Tracking)
                    {
                        // 그 마커 위치에 물체를 활성 및 배치하고싶다.
                        infos[j].obj.gameObject.SetActive(true);
                        infos[j].obj.transform.position = marker.transform.position;
                        infos[j].obj.transform.rotation = marker.transform.rotation;
                    }
                    else
                    {
                        // 그 마커의 물체를 비활성 하고싶다.
                        infos[j].obj.gameObject.SetActive(false);
                    }
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                ChangeColor(ray);
            }
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    ChangeColor(ray);
        //}
    }

    void ChangeColor(Ray ray)
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            MeshRenderer ren = hitInfo.transform.GetComponent<MeshRenderer>();

            ren.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
