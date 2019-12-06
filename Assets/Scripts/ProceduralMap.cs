using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMap : MonoBehaviour
{
    public event System.Action<ProceduralMap> OnActiveChanged;

    [SerializeField] private GameObject player1 = null;
    [SerializeField] private GameObject player2 = null;
    [SerializeField] private GameObject platform;
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private float moveSpeed = -0.1f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float maxXJumpDistance = 10f;
    [SerializeField] private Vector2 maxRange = new Vector2(-10, 10);
    private float lastPlatformX = 0f;
    private float randomDistance = 0;
    private bool active = true;

    public bool Active
    {
        get => active;

        set
        {
            if (active == value)
                return;

            active = value;

            OnActiveChanged?.Invoke(this);
        }
    }

    private void Start()
    {
        StartRandomPlatforms();
    }

    private void FixedUpdate()
    {
        if (!Active)
            return;

        transform.position += Vector3.up * moveSpeed * Time.fixedDeltaTime;
        if (transform.position.y <= randomDistance)
            InstantiatePlatform();
    }

    private void InstantiatePlatform()
    {
        List<GameObject> platforms = new List<GameObject>();
        Vector3 position;

        uint rnd = (uint)Random.Range(3, 6);
        for (uint j = 0; j < rnd; j++)
        {
            float x;
            do
            {
                x = Random.Range(maxRange.x, maxRange.y);
            }
            while (platforms.Find(y => 
            Vector3.Distance(y.transform.position, 
            new Vector3(x, transform.position.y, 0f)) < 2) != null);

            position = new Vector3(
                x,
                -transform.position.y + 12,
                0f);

            platforms.Add(InstantiatePlatform(ref position));
        }
        randomDistance = transform.position.y - maxDistance;
        Debug.Log(randomDistance);
    }

    private GameObject InstantiatePlatform(ref Vector3 position)
    {
        GameObject go = Instantiate(platform, transform);
        go.transform.localPosition = position;

        return go;
    }

    private void StartRandomPlatforms()
    {
        Vector3 pos1 = new Vector3(-3f, -8, 0);
        Vector3 pos2 = new Vector3(3f, -8, 0);

        PlayerMovement p1 = Instantiate(player1, pos1 + Vector3.up * 2, Quaternion.identity).GetComponent<PlayerMovement>();
        PlayerMovement p2 = Instantiate(player2, pos2 + Vector3.up * 2, Quaternion.identity).GetComponent<PlayerMovement>();

        p1.SetInputActions(PlayerInputs.PlayerOneJump, PlayerInputs.PlayerOneHorizontal);
        p2.SetInputActions(PlayerInputs.PlayerTwoJump, PlayerInputs.PlayerTwoHorizontal);

        InstantiatePlatform(ref pos1);
        InstantiatePlatform(ref pos2);

        List<GameObject> platforms = new List<GameObject>();
        float yPos = -8;
        Vector3 position;
        while (yPos < 10)
        {
            uint rnd = (uint)Random.Range(2, 4);
            for (uint j = 0; j < rnd; j++)
            {
                float x;
                do
                {
                    x = Random.Range(maxRange.x, maxRange.y);
                }
                while (platforms.Find(y => 
                Vector3.Distance(y.transform.position, 
                new Vector3(x, transform.position.y, 0f)) < 2) != null);
                    
                
                position = new Vector3(
                    x,
                    yPos,
                    0f);

                platforms.Add(InstantiatePlatform(ref position));
            }
            yPos += jumpHeight;
        }
    }
}
