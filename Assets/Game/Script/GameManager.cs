using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    public static FishInterface fishInterface;
    public static Preview preview;
    public static GameObject scoreObject;
    public static int score = 0;

    public static void ToggleCollection() {
        fishInterface.Toggle();
    }

    public static void OpenPreview(Card card)
    {
        ToggleCollection();
        preview.Toggle();

        preview.AssignInfo(card.data);
        preview.previewCard.DrawCard(card.textureFront, card.textureBack);
    }

    public static void ClosePreview()
    {
        ToggleCollection();
        preview.Toggle();
    }
}