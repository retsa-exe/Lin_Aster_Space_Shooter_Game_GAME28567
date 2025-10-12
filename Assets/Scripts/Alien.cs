using UnityEngine;
using System.Collections.Generic;

public class Alien : MonoBehaviour
{
    public Vector3 hook;
    public float hookSpeed = 2f;
    public float hookLength = 3f;
    public float maxAngle = 60f;
    Vector3 direction;

    public float shootDuration = 2f;

    public GameObject hookPrefab;

    public bool isShooting = false;
    float t;
    float shootTimer = 0f;
    public float shootSpeed = 2f;

    public List<GameObject> objects;
    public float caputureDistance = 1f;
    public int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //reset the hook position
        hook = transform.position + Vector3.up * hookLength;

        //reset the timer
        t = 0f;
        shootTimer = 0f;

        //reset score
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HookSwing();
        ObjectCaputure();
    }

    public void HookSwing()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
        {
            isShooting = true;

            float angle = Mathf.Sin(t * hookSpeed) * maxAngle + 90f;
            float radius = angle * Mathf.Deg2Rad;

            float hookX = Mathf.Cos(radius);
            float hookY = Mathf.Sin(radius);

            direction = new Vector3(hookX, hookY, 0);
        }

        if (!isShooting)
        {
            //update the timer
            t += Time.deltaTime;

            float angle = Mathf.Sin(t * hookSpeed) * maxAngle + 90f;
            float radius = angle * Mathf.Deg2Rad;

            float hookX = Mathf.Cos(radius);
            float hookY = Mathf.Sin(radius);

            direction = new Vector3(hookX, hookY, 0).normalized;

            hook = transform.position + direction * hookLength;
        }

        if (isShooting)
        {
            //update shoot timer
            shootTimer += Time.deltaTime;

            hook = transform.position + direction * hookLength * (1 + shootTimer * shootSpeed);

            if ( shootTimer > shootDuration)
            {
                isShooting = false;
                shootTimer = 0f;
            }
        }

        Debug.DrawLine(transform.position, hook, Color.yellow);

        //update the hook position
        hookPrefab.transform.position = hook;

        //set hook rotation
        float angleDegree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        hookPrefab.transform.rotation = Quaternion.Euler(0, 0, angleDegree + 90f);
    }

    public void ObjectCaputure()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            GameObject obj = objects[i];
            if (Vector3.Distance(hook, obj.transform.position) < caputureDistance)
            {
                obj.transform.position = hook;
                isShooting = false;
                shootTimer = 0f;

                score++;
                objects.Remove(obj);
                Destroy(obj);
            }
        }
    }
}
