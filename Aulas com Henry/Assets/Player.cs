using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Transform detectorChao;
    public bool estaNoChao;
    public float velocidade;
    public float velocidadeRotacao;

    // Start is called before the first frame update
    void Start()
    { 
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        checarChao();

        if (Input.GetKeyDown(KeyCode.Space) == true && estaNoChao == true)
        {
            Debug.Log("Pulou");
            pular();
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Movimentar(x, y);
    }

    void Movimentar(float horizontalInput, float verticalInput)
    {
        //playerRigidbody.velocity = new Vector3(direcaoX * 2f, playerRigidbody.velocity.y, direcaoY * 2f);

        transform.Rotate(Vector3.up * velocidadeRotacao * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.forward * velocidade * verticalInput * Time.deltaTime);
    }



    /*
        Vector3 movementDir = transform.forward;
        Debug.Log(movementDir);

        playerRigidbody.velocity = movementDir * 10f * direcaoY;

        Quaternion newRotation = Quaternion.Euler(0, 90f * direcaoX * Time.deltaTime, 0);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * newRotation);
        */

    void checarChao()
    {
        estaNoChao = Physics.CheckSphere(detectorChao.position, 0.1f);   
    }

    void pular()
    {
        playerRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(detectorChao.position, 0.1f);
    }

    /*
    Agora, vamos mudar a movimentação para que o jogador se movimente somente em linha reta e gira ao redor de si mesmo
    */

}
