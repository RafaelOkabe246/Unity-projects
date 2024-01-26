using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiSystem : MonoBehaviour
{
    public bool isInPlayScene;

    [Header("Ui relatives systems")]
    public PointSystem pointSystem;

    [Header("Points")]
    public TextMeshProUGUI textPoints;

    [Header("Emblema")]
    public TextMeshProUGUI codigoEmblemaText;

    public FileSystem fileSystem;

    private void Update()
    {
        if(textPoints != null && pointSystem!= null)
        SetPoints();
        if (codigoEmblemaText != null)
        ShowCodigo();
    }

    void ShowCodigo()
    {

        codigoEmblemaText.text = "Codigo do emblema: \n" + "\n" + fileSystem.codigoEmblema;
    }

    void SetPoints()
    {
        textPoints.text = "Pontos: " + PointSystem.points;
    }

}
