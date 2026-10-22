using System.IO;
using UnityEngine;
using Meta.XR.MRUtilityKit;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExporteurPieceMR : MonoBehaviour
{
    [ContextMenu("Exporter la piece en JSON")]
    private void Exporter()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            Debug.LogError("Lance Play avant d'exporter la piece.");
            return;
        }

        MRUK mruk = MRUK.Instance;

        if (mruk == null  || !mruk.IsInitialized ||

            mruk.Rooms.Count == 0)
        {
            Debug.LogError(
                "Aucune piece chargee dans MRUK. Export annule.");
            return;
        }

        string json = mruk.SaveSceneToJsonString(
            SerializationHelpers.CoordinateSystem.Unity,
            true
        );

        string dossier = Path.Combine(
            Application.dataPath, "RoomData");

        Directory.CreateDirectory(dossier);

        string chemin = Path.Combine(
            dossier, "PieceReference.json");

        File.WriteAllText(chemin, json);
        AssetDatabase.Refresh();

        Debug.Log("Piece exportee : " + chemin);
#else
        Debug.LogWarning(
            "Cet export est prevu pour l'editeur Unity.");
#endif
    }
}