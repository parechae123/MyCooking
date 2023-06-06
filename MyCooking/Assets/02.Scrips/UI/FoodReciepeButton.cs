using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodReciepeButton : MonoBehaviour
{
    public int foodIndex;
    public void OnButtonFoodPick()
    {
        SoundManager.SMInstance().ChangeSFX("BTNClickSound");
        GameManager.GMinstatnce().GetIngredientPosition(foodIndex);
    }
}
