using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSlot : MonoBehaviour
{
    public Level level;

    [Header("Ui")]
    public TextMeshProUGUI levelName;

    public LevelSlot(Level _level)
    {
        level = _level;
    }

    public void SetLevel(Level _level)
    {
        level = _level;
    }

    public void SetLevelUI()
    {
        levelName.text = level.name;
    }

    public void Selected()
    {
        
    }
}
