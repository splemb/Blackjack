using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderCard : MonoBehaviour
{
    public Card card;
    public TMPro.TextMeshProUGUI rank;
    public TMPro.TextMeshProUGUI rank2;
    public Image suit;
    public Image suit2;

    public Sprite[] suitImages;

    private void Start()
    {
        switch (card.rank){
            case '0':
                rank.text = "10";
                rank2.text = "10";
                break;
            default:
                rank.text = card.rank.ToString();
                rank2.text = card.rank.ToString();
                break;
        }

        switch (card.suit) {
            case '0':
                suit.sprite=suitImages[0];
                rank.color=Color.red;
                suit2.sprite=suitImages[0];
                rank2.color=Color.red;
                break;
            case '1':
                suit.sprite=suitImages[1];
                rank.color=Color.red;
                suit2.sprite=suitImages[1];
                rank2.color=Color.red;
                break;
            case '2':
                suit.sprite=suitImages[2];
                suit2.sprite=suitImages[2];
                break;
            case '3':
                suit.sprite=suitImages[3];
                suit2.sprite=suitImages[3];
                break;
        }
    }

}
