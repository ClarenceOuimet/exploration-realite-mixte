using UnityEngine;

public class BoutonDecorCyberpunk : MonoBehaviour
{
    [System.Serializable]
    private class LumiereDecor
    {
        public Light lumiere;
        public Color couleurCyberpunk = Color.magenta;
    }

    [SerializeField] private GameObject decorOriginal;
    [SerializeField] private GameObject decorCyberpunk;
    [SerializeField] private Color couleurNormale = new Color(1f, 0.95f, 0.85f);
    [SerializeField] private LumiereDecor[] lumieres = new LumiereDecor[0];

    private void Awake()
    {
        if (decorOriginal == null || decorCyberpunk == null)
        {
            Debug.LogError("Assigne les deux decors au bouton industriel.", this);
            return;
        }

        decorOriginal.SetActive(true);
        decorCyberpunk.SetActive(false);
        AppliquerLumieres(false);
    }

    [ContextMenu("Tester le decor cyberpunk (Play)")]
    public void AfficherCyberpunk()
    {
        if (decorOriginal == null || decorCyberpunk == null)
            return;

        decorCyberpunk.SetActive(true);
        AppliquerLumieres(true);
        decorOriginal.SetActive(false);
    }

    private void AppliquerLumieres(bool cyberpunk)
    {
        foreach (var reglage in lumieres)
        {
            if (reglage == null || reglage.lumiere == null)
                continue;

            reglage.lumiere.useColorTemperature = false;
            reglage.lumiere.color = cyberpunk
                ? reglage.couleurCyberpunk
                : couleurNormale;
        }
    }
}
