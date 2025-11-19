using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


[Serializable]
public class EdificioData
{
    public string nombre;
    public string texto;
    public string codigo;
    public Sprite silueta;
    public Sprite color;
    public Sprite blanco_negro;
    public Sprite coordenadas;
}

[Serializable]
public class ModoData
{
    public string modo;
    public List<EdificioData> datos;
}

[Serializable]
public class ModosWrapper
{
    public List<ModoData> modos;
}

public class EdificiosDataModel
{
    public static EdificiosDataModel Instance { get; private set; }

    private List<ModoData> data;
    private string filePath;

    public EdificiosDataModel(string filePath)
    {
        this.filePath = filePath;
        data = new List<ModoData>();
        Instance = this;
    }

    // Initialization method that loads and stores the JSON data
    public void Initialize()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("edificios");
        if (textAsset != null)
        {
            try
            {
                string json = textAsset.text;
                ModosWrapper wrapper = JsonUtility.FromJson<ModosWrapper>("{\"modos\":" + json + "}");
                data = wrapper.modos ?? new List<ModoData>();

                // Assign Sprite values to each edificio
                foreach (ModoData modoData in data)
                {
                    string folderModo = modoData.modo;

                    foreach (EdificioData edificioData in modoData.datos)
                    {
                        string folderEdificio = edificioData.codigo;

                        try
                        {
                            // Load silueta Sprite
                            edificioData.silueta = Resources.Load<Sprite>(folderModo + "/" + folderEdificio + "/SILUETA " + folderEdificio);
                            if (edificioData.silueta == null)
                            {
                                Debug.LogWarning("Silueta Sprite not found for " + folderModo + "/" + folderEdificio + "/SILUETA " + folderEdificio);
                            }

                            // Load color Sprite
                            edificioData.color = Resources.Load<Sprite>(folderModo + "/" + folderEdificio + "/" + folderEdificio);
                            if (edificioData.color == null)
                            {
                                Debug.LogWarning("Color Sprite not found for " + folderModo + "/" + folderEdificio + "/" + folderEdificio);
                            }

                            // Load blanco_negro Sprite
                            if (edificioData.nombre != null)
                            {
                                int index = edificioData.nombre.IndexOf(' ');
                                string blanco_negro = edificioData.nombre.Length > 0 ? edificioData.nombre.Substring(index + 1) : "";
                                edificioData.blanco_negro = Resources.Load<Sprite>(folderModo + "/" + folderEdificio + "/" + blanco_negro.Trim());
                                if (edificioData.blanco_negro == null)
                                {
                                    Debug.LogWarning("Blanco_negro Sprite not found for " + folderModo + "/" + folderEdificio + "/" + blanco_negro.Trim());
                                }
                            }
                            else
                            {
                                Debug.LogWarning("Nombre is null for edificio " + folderEdificio + ", skipping blanco_negro Sprite load.");
                            }

                            // Load coordenadas Sprite
                            string coordenadas = folderEdificio.Length > 0 ? folderEdificio.Substring(0, folderEdificio.Length - 1) : "";
                            edificioData.coordenadas = Resources.Load<Sprite>(folderModo + "/" + folderEdificio + "/" + coordenadas);
                            if (edificioData.coordenadas == null)
                            {
                                Debug.LogWarning("Coordenadas Sprite not found for " + folderModo + "/" + folderEdificio + "/" + coordenadas);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("Error loading Sprites for edificio " + folderEdificio + ": " + e.Message);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Debug.LogError("Error loading JSON from Resources: " + e.Message);
                data = new List<ModoData>();
            }
        }
        else
        {
            Debug.LogWarning("JSON file not found in Resources. Initializing with empty data.");
            data = new List<ModoData>();
        }
    }

    // Get data for access
    public List<ModoData> GetData()
    {
        return data;
    }
}
