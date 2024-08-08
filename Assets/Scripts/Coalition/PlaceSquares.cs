using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ImageGridGenerator : MonoBehaviour
{
    public int rows = 3;
    public int columns = 3;
    public float spacing = 1.0f;
    public GameObject imagePrefab;

    private GameObject[,] imageGrid;

    public void GenerateGrid()
    {
        if (imagePrefab == null)
        {
            Debug.LogError("Image Prefab is not assigned.");
            return;
        }

        ClearGrid();

        imageGrid = new GameObject[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(j * spacing, -i * spacing, 0);
                GameObject imageInstance = Instantiate(imagePrefab,this.transform);
                imageInstance.transform.localPosition = position;
                imageInstance.name = $"Image_{i}_{j}";

                imageGrid[i, j] = imageInstance;
            }
        }
    }

    public void ClearGrid()
    {
        if (imageGrid == null) return;

        for (int i = 0; i < imageGrid.GetLength(0); i++)
        {
            for (int j = 0; j < imageGrid.GetLength(1); j++)
            {
                if (imageGrid[i, j] != null)
                {
                    DestroyImmediate(imageGrid[i, j]);
                }
            }
        }
    }
}

[CustomEditor(typeof(ImageGridGenerator))]
public class ImageGridGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ImageGridGenerator script = (ImageGridGenerator)target;
        if (GUILayout.Button("Generate Grid"))
        {
            script.GenerateGrid();
        }

        if (GUILayout.Button("Clear Grid"))
        {
            script.ClearGrid();
        }
    }
}
