using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private Transform playerTrans;

    [Header("Properties")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI textShadow;
    [SerializeField] private Image[] childImages;

    private float alphaColor;
    [SerializeField] private float maxRadius;
    [SerializeField] private float minRadius;
    private float distanceToPlayer;

    private bool foundPlayer;

    private void Start()
    {
        StartCoroutine(DelayToFindPlayer());
    }

    private void OnEnable()
    {
        StartCoroutine(DelayToFindPlayer());
    }

    private IEnumerator DelayToFindPlayer() 
    {
        foundPlayer = false;

        yield return new WaitForEndOfFrame();

        playerTrans = FindObjectOfType<PlayerMovement>().transform;
        foundPlayer = true;
    }

    private void Update()
    {
        CheckPosition();
    }

    private void CheckPosition() 
    {
        if (playerTrans == null && foundPlayer)
        {
            StartCoroutine(DelayToFindPlayer());
            return;
        }

        if (playerTrans == null)
            return;

        distanceToPlayer = Vector3.Distance(playerTrans.position, transform.position);

        if (Mathf.Abs(distanceToPlayer) <= minRadius)
        {
            SetChildObjectsAlpha(1f);
        }
        else 
        {
            SetChildObjectsAlpha(1f - ((distanceToPlayer/(maxRadius + minRadius))));
        }
    }

    private void SetChildObjectsAlpha(float newAlpha) 
    {
        text.color = new Vector4(text.color.r, text.color.g, text.color.b, newAlpha);
        textShadow.color = new Vector4(textShadow.color.r, textShadow.color.g, textShadow.color.b, newAlpha);

        foreach (Image img in childImages) 
        {
            img.color = new Vector4(img.color.r, img.color.g, img.color.b, newAlpha);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minRadius);
    }
}
