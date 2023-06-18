using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    // 壁との衝突検知
    bool isTouchTop;
    bool isTouchBottom;
    bool isTouchRight;
    bool isTouchLeft;

    Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;
        Vector3 dist = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position += dist;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int) h);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ぶつかったのはBorderタグか
        if (other.gameObject.tag == "Border")
        {
            switch (other.gameObject.name)
            {
                // 各壁の名前で該当メンバ変数設定
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Border")
        {
            switch (other.gameObject.name)
            {
                // 各壁の名前で該当メンバ変数設定
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
