using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _HouseEnter;
    [SerializeField] private GameObject[] _HouseExit;
    [SerializeField] private GameObject[] _Cameras;
    [SerializeField] private GameObject _player;

    public void enterHouse(int houseNo)
    {
        switch (houseNo)
        {
            case 0:
                _player.transform.position = _HouseEnter[0].transform.position;
                _Cameras[0].SetActive(true);
                break;
            case 1:
                _player.transform.position = _HouseEnter[1].transform.position;
                _Cameras[1].SetActive(true);
                break;
            case 2:
                _player.transform.position = _HouseEnter[2].transform.position;
                _Cameras[2].SetActive(true);
                break;
            default:
                _Cameras[3].SetActive(false); //cinemachine
                break;
        }
    }

    public void exitHouse(int houseNo)
    {
        switch (houseNo)
        {
            case 0:
                _player.transform.position = _HouseExit[0].transform.position;
                _Cameras[0].SetActive(false);
                break;
            case 1:
                _player.transform.position = _HouseExit[1].transform.position;
                _Cameras[1].SetActive(false);
                break;
            case 2:
                _player.transform.position = _HouseExit[2].transform.position;
                _Cameras[2].SetActive(false);
                break;
            default:
                _Cameras[3].SetActive(true); //cinemachine
                break;
        }
    }
}
