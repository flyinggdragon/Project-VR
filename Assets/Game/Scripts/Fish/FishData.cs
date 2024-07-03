using System;

// Coloque as diretivas using no início do arquivo
using UnityEngine;

[Serializable]
public class FishData
{
    public string spriteName;
    public string fishName;
    public string popularName;
    public string scientificName;
    public float rarity;
    public string description;

    [NonSerialized]
    public Card card;
}

// Aqui você continua com outras classes, enumerações, ou métodos dentro do mesmo namespace
