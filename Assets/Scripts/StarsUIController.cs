using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsUIController : MonoBehaviour
{
    [SerializeField] Color disabledColor;
    [SerializeField] Color enabledColor;
    [SerializeField] List<Image> stars;

    private Image[] starImages;
    private Inventory inventory;

    private void Awake()
    {
        starImages = stars.ToArray();
        inventory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        foreach ( var image in starImages)
        {
            image.color = disabledColor;
        }
    }

    private void Update()
    {
        int currentStars = inventory.GetItemCount("StarPickup");
        for (int idx = 0; idx < currentStars; ++idx)
        {
            starImages[idx].color = enabledColor;
        }
        for (int idx = currentStars; idx < stars.Count; ++idx)
        {
            starImages[idx].color = disabledColor;
        }
    }
}
