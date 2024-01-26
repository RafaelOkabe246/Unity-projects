using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDaFase : MonoBehaviour
{
    public int MinLuzesSombrasNecessarias;
    public int MaxLuzesSombrasNecessarias;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CheckConditions(collision.gameObject.GetComponent<Player2DControl>());
        }
    }


    void CheckConditions(Player2DControl player)
    {
        if(MaxLuzesSombrasNecessarias > player.LuzesSombras || player.LuzesSombras > MinLuzesSombrasNecessarias)
        {
            //Venceu, vai para a próxima fase
            Debug.Log("Venceu a fase");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
