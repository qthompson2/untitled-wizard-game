using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    public GameObject spriteObject;
    public bool bobbing;
    public string animationState = "growing";
    public Vector3 scaleChange = new Vector3(0f, 0.001f, 0f);
    public HealthBar healthBar;
    public int maxHealth;
    public int health;
    public int maxTempHealth;
    public int tempHealth;
    public int immortalityFrameTimer;
    public int maxIFTimer;
    public GameObject manabolt;
    public int lastX;
    public int lastY;
    void Awake() {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
        healthBar.SetMaxTemp(maxTempHealth);
        healthBar.SetTemp(tempHealth);
        immortalityFrameTimer = 0;
    }

    void Update() 
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Animate();
        if (immortalityFrameTimer >= 0) {
            immortalityFrameTimer -= 1;
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            Injure(1);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            CastManaBolt();
        }
    }

    private void Move(float x, float y)
    {
        if (x > 0 && y > 0) {
            //[w] and [d]
            x = Mathf.Sqrt((x * x) / 2);
            y = Mathf.Sqrt((y * y) / 2);
            lastX = 1;
            lastY = 1;
        } else if (x < 0 && y < 0) {
            //[a] and [s]
            x = -Mathf.Sqrt((x * x) / 2);
            y = -Mathf.Sqrt((y * y) / 2);
            lastX = -1;
            lastY = -1;
        } else if (x > 0 && y < 0) {
            //[d] and [s]
            x = Mathf.Sqrt((x * x) / 2);
            y = -Mathf.Sqrt((y * y) / 2);
            lastX = 1;
            lastY = -1;
        } else if (x < 0 && y > 0) {
            //[a] and [w]
            x = -Mathf.Sqrt((x * x) / 2);
            y = Mathf.Sqrt((y * y) / 2);
            lastX = -1;
            lastY = 1;
        } else if (x > 0 && y < 0.9) {
            lastX = 1;
            lastY = 0;
        } else if (x < 0 && y < 0.9) {
            lastX = -1;
            lastY = 0;
        } else if (y > 0 && x < 0.9) {
            lastX = 0;
            lastY = 1;
        } else if (y < 0 && x < 0.9) {
            lastX = 0;
            lastY = -1;
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
    public void Injure(int value) {
        if (immortalityFrameTimer < 0) {
            immortalityFrameTimer = maxIFTimer;
            if (tempHealth == 0) {
                if (health - value >= 0) {
                    health -= value;
                    healthBar.SetHealth(health);
                } else {
                    health = 0;
                    healthBar.SetHealth(0);
                }
            } else {
                if (tempHealth - value >= 0) {
                    tempHealth -= value;
                    healthBar.SetTemp(tempHealth);
                } else if (health + tempHealth - value >= 0) {
                    tempHealth = 0;
                    health = health + tempHealth - value;
                    healthBar.SetTemp(0);
                    healthBar.SetHealth(health);
                } else {
                    tempHealth = 0;
                    health = 0;
                    healthBar.SetTemp(0);
                    healthBar.SetHealth(0);
                }
            }
        }
    }

    public void CastManaBolt() {
        Projectile p = new Projectile();
        Instantiate(manabolt, transform.position, Quaternion.LookRotation(new Vector3(0, 0, 0)));
        p.SetProjectile(new Vector2(lastX, lastY), 10, 1);
    }
}
