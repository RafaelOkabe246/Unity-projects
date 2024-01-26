using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carro : MonoBehaviour
{
    /* 
     * 
     * 
     * 
     * 
    /

    /* 1. Escrever as ações que o carro 
     * 2. Variáveis essenciais para o carro se mover
     * 3. Regras do jogo de corrida: tem linha de chegada? tem muros para impedir que o carro saia da pista? 
     * 4. Importar um modelo de carro para dentro da unity
     * 5. Aplicar as animações no modelo 3d
     */

    public float velocity = 10f;
    public float turnSpeed = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Vertical");
        float turnInput = Input.GetAxisRaw("Horizontal");

        Movement(moveInput, turnInput);
    }

    void Movement(float dir, float turnDir)
    {
        Vector3 direction = new Vector3(dir, 0, 0) * velocity;//Vector3.forward * velocity * dir;
        rb.AddForce(direction);

        Quaternion rotationValue = Quaternion.Euler(0,turnSpeed * turnDir * Time.fixedDeltaTime,0);
        rb.MoveRotation(rb.rotation * rotationValue);
        //transform.Rotate(Vector3.up * turnDir * turnSpeed * Time.fixedDeltaTime);
    }
}
