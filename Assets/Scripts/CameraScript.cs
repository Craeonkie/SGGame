using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Camera[] cam;
    public Camera testCam;
    public GameObject cinemachineObj;

    public void enableCamera(Camera camera)
    {
        for (int i = 0; i < cam.Length; i++)
        {
            if (camera == cam[i])
            {

                cam[i].gameObject.SetActive(true);
            }
            else
            {
                cam[i].gameObject.SetActive(false);
            }
        }
        disableCinemachine();
    }

    public void disableCamera(Camera camera)
    {
        for (int i = 0; i < cam.Length; i++)
        {
            if (camera == cam[i])
            {

                cam[i].gameObject.SetActive(true);
            }
            else
            {
                cam[i].gameObject.SetActive(false);
            }
        }
        enableCinemachine();
    }

    public void enableCinemachine()
    {
        cinemachineObj.gameObject.SetActive(true);
    }
    //re-enable it everytime

    public void disableCinemachine()
    {
        cinemachineObj.gameObject.SetActive(false);
    }
    //always disable
}