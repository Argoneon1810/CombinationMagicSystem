using TMPro;
using UnityEngine;

public class DebugShowNotif : MonoBehaviour
{
    public TextMeshProUGUI notif;
    public void EnableNotif()
    {
        notif.gameObject.SetActive(true);
    }
}
