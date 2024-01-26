using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper_Narrative : MonoBehaviour
{
    public string Mensagem;

    private AudioController _AudioController;

    public Image Text_background;
    public Text _Text;

    private void Start()
    {
        _AudioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        Text_background.gameObject.SetActive(false);
        _Text.gameObject.SetActive(false);
    }

    public void Texto()
    {
        _Text.gameObject.SetActive(true);
        _AudioController.AudioPlay(_AudioController.Paper_turn,1f);
        Text_background.gameObject.SetActive(true); 
        _Text.text = Mensagem;
    }

    public void Self_destruct()
    {
        Destroy(this.gameObject);
    }
}
