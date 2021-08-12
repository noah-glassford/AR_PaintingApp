using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceUserPlane : MonoBehaviour
{


    private bool startedPlacing = false;
    private bool progressFlagPlace = false;
    private bool progressFlagRotate = true;
    private bool progressFlagScale = true;


    // Update is called once per frame
    void Update()
    {


        DetectTouchMovement.Calculate();
        Touch touch = Input.GetTouch(0);

        if (startedPlacing)
        {
            float pinchAmount = 0f;
            if (!progressFlagPlace)
            {
                if (Mathf.Abs(DetectTouchMovement.pinchDistanceDelta) > 0)
                { // zoom

                    pinchAmount = DetectTouchMovement.pinchDistanceDelta;

                    Matrix4x4 viewMatrix;

                    viewMatrix = Matrix4x4.Inverse(Camera.main.transform.localToWorldMatrix);

                    Vector3 forward;
                    forward.x = viewMatrix.m20;
                    forward.y = viewMatrix.m21;
                    forward.z = viewMatrix.m22;

                    transform.position += forward * pinchAmount * Time.deltaTime;
                }
            }
            else if (!progressFlagRotate)
            {
                //test code for rotation
                Vector2 touchMovement = touch.deltaPosition;

                Vector3 newRotation = transform.rotation.eulerAngles;

                newRotation.z += touchMovement.x * Time.deltaTime;
                newRotation.x += touchMovement.y * Time.deltaTime;

                transform.rotation = Quaternion.Euler(newRotation);
            }
            else if (!progressFlagScale)
            {
                if (Mathf.Abs(DetectTouchMovement.pinchDistanceDelta) > 0)
                { // zoom

                    pinchAmount = DetectTouchMovement.pinchDistanceDelta;

                    transform.localScale += (new Vector3(pinchAmount, pinchAmount, pinchAmount) / 3) * Time.deltaTime;
                }
            }
        }
    }

    public void PlacePlane()
    {
        gameObject.SetActive(true);
    }

    public void DestroyPlane()
    {
        gameObject.SetActive(false);
    }

    public void StartSequence()
    {
        startedPlacing = true;
    }

    //these exist to confirm progress using the ui buttons
    public void SetPlace()
    {
        gameObject.transform.parent = null;
        progressFlagPlace = true;
        progressFlagRotate = false;
    }

    public void SetRotation()
    {
        progressFlagRotate = true;
        progressFlagScale = false;
    }

    public void SetScale()
    {
        progressFlagScale = true;
    }

}