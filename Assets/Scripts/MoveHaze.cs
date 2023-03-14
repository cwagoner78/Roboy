using UnityEngine;

public class MoveHaze : MonoBehaviour
{
    public float speed;
    public float gravity;
    public float resetXPosition = -27;


    void Update()
    {
        transform.position += new Vector3(-1, gravity, 0) * Time.deltaTime * speed;
        if (transform.position.x <= resetXPosition) transform.position += Vector3.right * 60f;
    }
}
