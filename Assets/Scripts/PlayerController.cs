using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void Move(float x, float y)
    {
        if (x > 0 && y > 0) {
            //[w] and [d]
            x = Mathf.Sqrt((x * x) / 2);
            y = Mathf.Sqrt((y * y) / 2);
        } else if (x < 0 && y < 0) {
            //[a] and [s]
            x = -Mathf.Sqrt((x * x) / 2);
            y = -Mathf.Sqrt((y * y) / 2);
        } else if (x > 0 && y < 0) {
            //[d] and [s]
            x = Mathf.Sqrt((x * x) / 2);
            y = -Mathf.Sqrt((y * y) / 2);
        } else if (x < 0 && y > 0) {
            //[a] and [w]
            x = -Mathf.Sqrt((x * x) / 2);
            y = Mathf.Sqrt((y * y) / 2);
        }

        transform.Translate(new Vector2(x, y) * speed * Time.deltaTime);

    }
}
