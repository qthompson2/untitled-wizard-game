using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    public GameObject spriteObject;
    public bool bobbing;
    public int timer = 10;
    public string animationState = "growing"; //possible values: "shrinking", "growing"
    public Vector3 scaleChange = new Vector3(0f, 0.001f, 0f);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Animate();
    }

    private void Move(float x, float y)
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
        
        if (x != 0 || y != 0) {
            bobbing = true;
        } else {
            bobbing = false;
        }

        transform.Translate(new Vector2(x, y) * speed * Time.deltaTime);
    }

    private void Animate() {
        float maxScale = 1.1f;
        float minScale = 0.9f;
        
        if (animationState == "growing" && bobbing){
            spriteObject.transform.localScale += scaleChange;
        } else if (animationState == "shrinking" && bobbing) {
            spriteObject.transform.localScale -= scaleChange;
        } else if (!(bobbing)) {
            spriteObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (spriteObject.transform.localScale.y < minScale) {
            animationState = "growing";
            scaleChange = new Vector3(0f, 0.001f, 0f);
        } else if (spriteObject.transform.localScale.y > maxScale) {
            animationState = "shrinking";
            scaleChange = new Vector3(0f, 0.002f, 0f);
        }

        if (Input.GetAxis("Horizontal") < 0) {
            spriteObject.GetComponent<SpriteRenderer>().flipX = true;
        } else if (Input.GetAxis("Horizontal") > 0) {
            spriteObject.GetComponent<SpriteRenderer>().flipX = false;
        }

    }
}
