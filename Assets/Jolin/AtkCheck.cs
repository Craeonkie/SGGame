using Unity.VisualScripting;
using UnityEngine;

public class AtkCheck : MonoBehaviour
{
    [SerializeField] private Collider atkCollider;
    [SerializeField] PlayerController playerController;

    //[SerializeField] private float swingCDTimer;
    [SerializeField] private float swingActiveTimer;

    [SerializeField] private float pushForce;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        atkCollider.enabled = false;
        timer = swingActiveTimer;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("can swing: " + playerController._swing);
        if (playerController._swing)
        {
            atkCollider.enabled = true;
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timer = swingActiveTimer;
            atkCollider.enabled = false;
            playerController._swing = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Vector3 objA = other.transform.position;
            Vector3 objB = playerController.transform.position;
            //calculate the direction:
            //other.gameObject.transform.position = Vector3.MoveTowards(objA , -objB, pushForce);

            var dist = objB - objA;
            var oppDir = -dist;

            Collider[] test = Physics.OverlapBox(objB, other.transform.localScale / 2, Quaternion.identity);

            foreach (Collider b in test)
            {
                other.GetComponent<Rigidbody>().AddForce(oppDir * pushForce, ForceMode.Impulse);
            }

        }
    }
}
