using UnityEngine;

public class GenerateurObjetsMR : MonoBehaviour
{
    [SerializeField] private GameObject prefabObjet;

    private void Start()
    {
        if (prefabObjet == null)
        {
            Debug.LogError("Aucun prefab assigné au générateur.", this);
            return;
        }

        Instantiate(prefabObjet, transform.position, transform.rotation);
    }
}