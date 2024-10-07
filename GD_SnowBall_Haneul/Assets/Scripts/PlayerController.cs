using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
//using UnityEditorInternal;
//using System.Threading;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private int count;

    private int HP = 100;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    public float speed = 0;

    public GameObject winTextObject;

    public GameObject OuchTextObject;

    public GameObject PlayerObject;

    public GameObject DeadText;


    public TextMeshProUGUI countText;

    public TextMeshProUGUI HPText;


    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        OuchTextObject.SetActive(false);
    }

    void Start()
    {
        winTextObject.SetActive(false);
        DeadText.SetActive(false);

        count = 0;
        rb = GetComponent<Rigidbody>();
        SetCountText();
        SetHPText();
        OuchTextObject.SetActive(false);
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetHPText()
    {
        HPText.text = "HP: " + HP.ToString();

        if (HP <= 0)
        {
            DeadText.SetActive(true);

            Destroy(GameObject.FindGameObjectWithTag("Player"));
            //PlayerObject.SetActive(false);
        }
    }
    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();

        if (count >= 19)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }

    }

    private void FixedUpdate()
    {

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ouch"))
        {
            OuchTextObject.SetActive(true);

            HP -= 25;

            SetHPText();

            StartCoroutine(Delay());
        }



        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OuchTextObject.SetActive(true);

            HP -= 25;

            SetHPText();

            StartCoroutine(Delay());

            
            if (HP < 0)
            {
                Destroy(gameObject);

                winTextObject.gameObject.SetActive(true);
                winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

            }

        }
    }


}
