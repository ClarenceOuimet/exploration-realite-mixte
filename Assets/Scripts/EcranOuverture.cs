using UnityEngine;
using UnityEngine.SceneManagement;

public class EcranOuverture : MonoBehaviour
{
    public void Entrer()
    {
        SceneManager.LoadScene("02_PIECE_CYBERPUNK");
    }
}
