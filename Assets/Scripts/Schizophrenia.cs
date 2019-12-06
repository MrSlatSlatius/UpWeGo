using UnityEngine;

public class Schizophrenia : MonoBehaviour, ICharacterBehaviour
{
    [SerializeField] private GameObject eyeBat;
    private Transform adversaryTransform;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Special();
        }
    }

    public void Special()
    {
        if (adversaryTransform == null)
        {
            if (gameObject.CompareTag("P1"))
                adversaryTransform = GameObject.FindGameObjectWithTag("P2").transform;
            else
                adversaryTransform = GameObject.FindGameObjectWithTag("P1").transform;
        }

        GameObject myInstance;
        myInstance = Instantiate(eyeBat, transform);
        myInstance.GetComponent<EyeBatAI>().Initiate(adversaryTransform, gameObject);
    }
}
