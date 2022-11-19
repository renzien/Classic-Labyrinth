using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchController : MonoBehaviour
{
    [SerializeField, Range(0, 20)] float filterFactor = 1;
    float distance;
    void Start()
    {
        distance = this.transform.position.y;
    }

    Vector3 touchBeganWorldPos;
    Vector3 cameraBeganWorldPos;

    void Update()
    {
        if(Input.touchCount == 0)
            return;
        
        var touch0 = Input.GetTouch(0);

        // Simpan Posisi

        if(touch0.phase == TouchPhase.Began)
        {
            touchBeganWorldPos = Camera.main.ScreenToWorldPoint(
                    new Vector3(touch0.position.x, touch0.position.y, distance)
            );
            cameraBeganWorldPos = this.transform.position;
        }

        if(touch0.phase == TouchPhase.Moved)
        {
            var touchMovedWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(touch0.position.x, touch0.position.y, distance)
            );
            var delta = touchMovedWorldPos - touchBeganWorldPos;

            var targetPos = cameraBeganWorldPos - delta;
            this.transform.position = Vector3.Lerp(
                this.transform.position,
                targetPos,
                Time.deltaTime * filterFactor
            );
        }
    }
}
