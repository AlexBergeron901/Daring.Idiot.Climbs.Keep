using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MurCollision : MonoBehaviour
{
    //Prendre le joueur et le punir quand il s'éloigne de la map
    [SerializeField] GameObject joueur;
    [SerializeField] TextMeshProUGUI texteAlerte;
    [SerializeField] GameObject alerteCanvasVisible;

    public void MurSafeZone(bool estActif)
    {
        if (estActif == true)
        {
            alerteCanvasVisible.SetActive(estActif);
            texteAlerte.text = "Bon choix !";
        }
        else
        {
            alerteCanvasVisible.SetActive(estActif);
        }

    }
    public void MurDangerZone(bool estActif)
    {
        if (estActif == true)
        {
            alerteCanvasVisible.SetActive(estActif);
            texteAlerte.text = "Il vous reste 15s avant de mourrir !";
        }
        else
        {
            alerteCanvasVisible.SetActive(estActif);
        }

    }
    public void MurTuMeurs(bool estActif)
    {
        if (estActif == true)
        {
            alerteCanvasVisible.SetActive(estActif);
            texteAlerte.text = "Ahah tu es mort !";
        }
        else
        {
            alerteCanvasVisible.SetActive(estActif);
        }
    }
}
