using UnityEngine;

public class LedgeClimbScript : MonoBehaviour
{
    public bool ifOnLedge = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ledge"))
        {
            ifOnLedge = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ifOnLedge = false;
    }
}
