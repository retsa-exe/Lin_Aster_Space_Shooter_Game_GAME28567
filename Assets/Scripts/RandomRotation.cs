using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public float minSpeed = 0f;
    public float maxSpeed = 3f;
    float speed;

    public float minRotationSpeed = -10f;
    public float maxRotationSpeed = 10f;
    float rotationSpeed;

    float initialRotate;

    public Vector3 direction;

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

        initialRotate = Random.Range(0f, 360f);
        transform.Rotate(0, 0, initialRotate);

        direction = (Vector3)Random.insideUnitCircle * speed;
    }
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        transform.position += direction * Time.deltaTime;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.x < 0 || screenPos.x > Screen.width)
        {
            direction.x = -direction.x;
        }

        if (screenPos.y < 0 || screenPos.y > Screen.height)
        {
            direction.y = -direction.y;
        }
    }
}
