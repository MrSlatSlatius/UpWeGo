using UnityEngine;

public class EyeBatAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float stunTime;
    private Transform target;
    private GameObject master;
    private bool initiated;

    public void Initiate(Transform _target, GameObject _master)
    {
        target = _target;
        master = _master;
        initiated = true;
    }

    private void Update()
    {
        if (initiated)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("P2") && master.CompareTag("P1") || other.CompareTag("P1") && master.CompareTag("P2"))
        {
            other.gameObject.GetComponent<PlayerMovement>().Stuned(stunTime);
            Destroy(gameObject);
        }
    }
}
