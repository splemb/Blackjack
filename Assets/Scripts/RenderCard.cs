using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderCard : MonoBehaviour
{
    public Card card;
    public Text rank1;
    public Text rank2;

    private void Start()
    {
        rank1.text = card.rank.ToString();
        rank2.text = card.rank.ToString();
    }

}
