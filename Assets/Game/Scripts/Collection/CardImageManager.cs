using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Recriar arquivo dentro da Unity
public class CardImageManager : MonoBehaviour {

    public static List<Texture2D> LoadImages() {

        Texture2D[] images = Resources.LoadAll<Texture2D>("CardImages");

        var p_images = new List<Texture2D>();
        p_images.AddRange(images);
        
        return p_images;
    }

    public static Texture2D GetCardSpriteFromList(string name, List<Texture2D> list) {
        return list.FirstOrDefault(s => s.name == name + "Card");
    }
}