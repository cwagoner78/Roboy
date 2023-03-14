using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DepthLight : MonoBehaviour
{
    public float intensityMult = .2f;


    PlayerController player;
    Rigidbody2D rb;
    Light2D light;

    private string currentState;

    void Start()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        light = GetComponentInChildren<Light2D>();
    }

    private void Update()
    {

        light.intensity = (Mathf.Abs(transform.position.y) * intensityMult) - 3;
        if (player.rb.position.y > -3) light.intensity = 0;
        if (light.intensity < 0) light.intensity = 0;
        if (light.intensity > 5) light.intensity = 5;

    }


}
