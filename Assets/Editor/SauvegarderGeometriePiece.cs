using UnityEngine;
using UnityEditor;
using Meta.XR.MRUtilityKit;

public static class SauvegarderGeometriePiece
{
    [MenuItem("Outils MR/Sauvegarder la geometrie de la piece")]
    private static void Sauvegarder()
    {
        if (!Application.isPlaying)
        {
            Debug.LogError("Lance Play et attends le chargement de la piece.");
            return;
        }

        var effets = Object.FindObjectsByType<EffectMesh>(
            FindObjectsSortMode.None);

        int nombreSurfaces = 0;

        foreach (var effet in effets)
        {
            if (effet.isActiveAndEnabled)
                nombreSurfaces += effet.EffectMeshObjects.Count;
        }

        if (nombreSurfaces == 0)
        {
            Debug.LogError("Aucune surface generee par EffectMesh.");
            return;
        }

        string dossier = AssetDatabase.GenerateUniqueAssetPath(
            "Assets/PieceVisuelle");

        AssetDatabase.CreateFolder(
            "Assets", System.IO.Path.GetFileName(dossier));

        var racine = new GameObject("Piece_Apercu");
        racine.tag = "EditorOnly";

        int compteur = 0;

        try
        {
            foreach (var effet in effets)
            {
                if (!effet.isActiveAndEnabled)
                    continue;

                foreach (var element in effet.EffectMeshObjects)
                {
                    GameObject source = element.Value.effectMeshGO;

                    if (source == null)
                        continue;

                    var filtre = source.GetComponent<MeshFilter>();
                    var rendu = source.GetComponent<MeshRenderer>();

                    if (filtre == null || filtre.sharedMesh == null ||
                        rendu == null)
                        continue;

                    Mesh mesh = Object.Instantiate(filtre.sharedMesh);
                    mesh.name = "Surface_" + compteur;

                    AssetDatabase.CreateAsset(
                        mesh, $"{dossier}/Surface_{compteur}.asset");

                    var copie = new GameObject(
                        $"{effet.name}_{compteur}");

                    copie.tag = "EditorOnly";
                    copie.transform.SetParent(racine.transform, false);
                    copie.transform.SetPositionAndRotation(
                        source.transform.position,
                        source.transform.rotation);
                    copie.transform.localScale =
                        source.transform.lossyScale;

                    copie.AddComponent<MeshFilter>().sharedMesh = mesh;
                    copie.AddComponent<MeshRenderer>().sharedMaterials =
                        rendu.sharedMaterials;

                    compteur++;
                }
            }

            string chemin = $"{dossier}/Piece_Apercu.prefab";
            PrefabUtility.SaveAsPrefabAsset(racine, chemin);
            AssetDatabase.SaveAssets();

            Debug.Log(
                $"Piece sauvegardee : {chemin} ({compteur} surfaces)");
        }
        finally
        {
            Object.DestroyImmediate(racine);
        }
    }
}