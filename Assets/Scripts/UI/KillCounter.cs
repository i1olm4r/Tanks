using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text killText;
    private int kills = 0;

    public void RegisterKill()
    {
        kills++;
        killText.text = "Kills: " + kills;
    }
}