using System.Collections.Generic;
using UnityEngine;

public class FishJSONReader : MonoBehaviour
{
    [System.Serializable]
    public class FishDataList
    {
        public List<FishData> fish;
    }

    public static List<FishData> ReadFishDataFromJSON()
    {
        // Carrega o JSON como texto da pasta Resources
        TextAsset jsonTextAsset = Resources.Load<TextAsset>("fishData");

        if (jsonTextAsset == null)
        {
            Debug.LogError("Não foi possível encontrar o arquivo fishData.json na pasta Resources.");
            return null;
        }

        // Converte o texto JSON para uma lista de FishData
        FishDataList fishDataList = JsonUtility.FromJson<FishDataList>(jsonTextAsset.text);

        return fishDataList.fish;
    }
}


