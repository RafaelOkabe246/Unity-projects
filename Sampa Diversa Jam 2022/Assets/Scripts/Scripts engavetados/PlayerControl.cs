using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speedHorizontal = 5.0f;
    public float speedVertical = 5.0f;

    [Header("Shadow and light")]
    public GameObject Estrela;
    public Camera CameraMain;
    internal Vector3 Target;
    public int EstrelasQuantity;

    // Update is called once per frame
    void Update()
    {
        Target = CameraMain.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speedVertical);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -speedVertical);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.right * Time.deltaTime * -speedHorizontal);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * +speedHorizontal);
        }

        if (Input.GetMouseButtonDown(0))
        {
          //  AtirarEstrela();
        }

    }

    void AtirarEstrela()
    {
        if (EstrelasQuantity > 0)
        {
            GameObject a;
            a = Instantiate(Estrela, Target, Quaternion.identity);
            Destroy(a, 10);
        }
    }


}
