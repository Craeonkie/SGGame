using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Camera[] cam;

    public void EnableCamera(Camera camera)
    {
        foreach (Camera c in cam)
        {
            c.gameObject.SetActive(false);
        }
        camera.gameObject.SetActive(true);
    }

    //private void DisableCamera(Camera camera)
    //{
    //    for (int i = 0; i < cam.Length; i++)
    //    {
    //        if (camera == cam[i])
    //        {

    //            cam[i].gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            cam[i].gameObject.SetActive(false);
    //        }
    //    }
    //    EnableCinemachine();
    //}

    //private void EnableCinemachine()
    //{
    //    cinemachineObj.SetActive(true);
    //}
    ////re-enable it everytime

    //private void DisableCinemachine()
    //{
    //    cinemachineObj.SetActive(false);
    //}
}