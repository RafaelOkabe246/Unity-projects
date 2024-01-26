using UnityEngine;
using TMPro;

public class ShowTimeUI : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI timeTextShadow;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if(anim == null)
            anim = GetComponent<Animator>();
    }

    public void UpdateTimeOnScreen(string text) 
    {
        anim.SetTrigger("Show");
        timeText.text = text;
        timeTextShadow.text = timeText.text;
    }
}
