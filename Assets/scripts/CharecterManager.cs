using TMPro;
using UnityEngine;

public class CharecterManager : MonoBehaviour
{
    public Pins pinsDB;

    public static int selection = 0;
    public SpriteRenderer sprite;
    public TMP_Text nameLabel;

    void updateCharecter()
    {
        Pin current = pinsDB.getPin(selection);
        sprite.sprite = current.prefab.GetComponent<SpriteRenderer>().sprite;
        nameLabel.SetText(current.name);
    }

    private void Start()
    {
        updateCharecter();
    }

    public void next()
    {
        int numberPins = pinsDB.getCount();

        if(selection < numberPins - 1)
        {
            selection = selection + 1;
        }

        else
        {
            selection = 0;
        }

        updateCharecter();
    }

    public void previous()
    {
        if (selection > 0)
        {
            selection = selection - 1;
        }
        else
        {
            selection = pinsDB.getCount() - 1;
        }

        updateCharecter();
    }
}
