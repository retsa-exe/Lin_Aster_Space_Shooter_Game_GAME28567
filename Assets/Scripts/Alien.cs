using UnityEngine;

public class Alien : MonoBehaviour
{
    public Vector3 hook;
    public float hookSpeed = 2f;
    public float hookLength = 3f;
    public float maxAngle = 60f;

    public float shootDuration = 2f;

    public GameObject hookPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //reset the hook position
        hook = transform.position + Vector3.up * hookLength;
    }

    // Update is called once per frame
    void Update()
    {
        HookSwing();
    }

    public void HookSwing()
    {
        float angle = Mathf.Sin(Time.time * hookSpeed) * maxAngle + 90f;
        float radius = angle * Mathf.Deg2Rad;

        float hookX = Mathf.Cos(radius);
        float hookY = Mathf.Sin(radius);

        hook = transform.position + new Vector3(hookX, hookY, 0) * hookLength;

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

        Debug.DrawLine(transform.position, hook, Color.yellow);

        //update the hook position
        hookPrefab.transform.position = hook;

        //set hook rotation
        Vector3 direction = hook - transform.position;
        float angleDegree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        hookPrefab.transform.rotation = Quaternion.Euler(0, 0, angleDegree + 90f);
    }
}
