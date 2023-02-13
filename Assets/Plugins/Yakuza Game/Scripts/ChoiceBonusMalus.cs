using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceBonusMalus : MonoBehaviour
{
    public void ChangePriceCupcakeNextDay(float valuePercentage)
    {
        /*
        VisualNovelManager _visualNovelManager = VisualNovelManager.Instance;
        GameplayManager _gameplayManager = GameplayManager.Instance;
        
        string valueText = valuePercentage.ToString();
        _gameplayManager.variableConfig.priceMultiplierCC += valuePercentage / 100;

        if (valuePercentage >= 0)
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_PRICE_CC_BONUS") + " " + valueText + "% ,");
        }
        else
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_PRICE_CC_MALUS") + " " + valueText.Substring(1,valueText.Length-1) + "% ,");
        }
    }
    
    public void ChangePriceBubbleTeaNextDay(float valuePercentage)
    {
        VisualNovelManager _visualNovelManager = VisualNovelManager.Instance;
        GameplayManager _gameplayManager = GameplayManager.Instance;
        
        string valueText = valuePercentage.ToString();
        _gameplayManager.variableConfig.priceMultiplierBT += valuePercentage / 100;

        if (valuePercentage >= 0)
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_PRICE_BT_BONUS") + " " + valueText + "% ,");
        }
        else
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_PRICE_BT_MALUS") + " " + valueText.Substring(1,valueText.Length-1) + "% ,");
        }
    }
    
    public void ChangePriceCoffeeNextDay(float valuePercentage)
    {
        VisualNovelManager _visualNovelManager = VisualNovelManager.Instance;
        GameplayManager _gameplayManager = GameplayManager.Instance;
        
        string valueText = valuePercentage.ToString();
        _gameplayManager.variableConfig.priceMultiplierC += valuePercentage / 100;

        if (valuePercentage >= 0)
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_PRICE_C_BONUS") + " " + valueText + "% ,");
        }
        else
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_PRICE_C_MALUS") + " " + valueText.Substring(1,valueText.Length-1) + "% ,");
        }
    }
    
    public void ChangeSpeedCharactersNextDay(float valuePercentage)
    {
        VisualNovelManager _visualNovelManager = VisualNovelManager.Instance;
        GameplayManager _gameplayManager = GameplayManager.Instance;
        
        string valueText = valuePercentage.ToString();
        _gameplayManager.variableConfig.spdCharacterMultiplier += valuePercentage / 100;
        
        if (valuePercentage >= 0)
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_SPEED_CHARACTER_BONUS") + " " + valueText + "% ,");
        }
        else
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_SPEED_CHARACTER_MALUS") + " " + valueText.Substring(1,valueText.Length-1) + "% ,");
        }
    }
    
    public void ChangeSpeedMitsuriNextDay(float valuePercentage)
    {
        VisualNovelManager _visualNovelManager = VisualNovelManager.Instance;
        GameplayManager _gameplayManager = GameplayManager.Instance;
        
        string valueText = valuePercentage.ToString();
        _gameplayManager.variableConfig.spdCharacterMultiplier += valuePercentage / 100;
        
        if (valuePercentage >= 0)
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_SPEED_MITSURI_BONUS") + " " + valueText + "% ,");
        }
        else
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_SPEED_MITSURI_MALUS") + " " + valueText.Substring(1,valueText.Length-1) + "% ,");
        }
    }
    
    public void ChangeAllPriceNextDay(float valuePercentage)
    {
        VisualNovelManager _visualNovelManager = VisualNovelManager.Instance;
        GameplayManager _gameplayManager = GameplayManager.Instance;
        
        string valueText = valuePercentage.ToString();
        _gameplayManager.variableConfig.priceMultiplierC += valuePercentage / 100;
        _gameplayManager.variableConfig.priceMultiplierCC += valuePercentage / 100;
        _gameplayManager.variableConfig.priceMultiplierBT += valuePercentage / 100;
        
        if (valuePercentage >= 0)
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_PRICES_BONUS") + " " + valueText + "% ,");
        }
        else
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_PRICES_MALUS") + " " + valueText.Substring(1,valueText.Length-1) + "% ,");
        }
    }
    
    public void ChangeClientSpawnNextDay(float valuePercentage)
    {
        VisualNovelManager _visualNovelManager = VisualNovelManager.Instance;
        GameplayManager _gameplayManager = GameplayManager.Instance;
        
        string valueText = valuePercentage.ToString();
        _gameplayManager.variableConfig.clientSpawnMultiplier += valuePercentage / 100;
        
        if (valuePercentage >= 0)
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_CLIENTS_NUMBER_BONUS") + " " + valueText + "% ,");
        }
        else
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_CLIENTS_NUMBER_MALUS") + " " + valueText.Substring(1,valueText.Length-1) + "% ,");
        }
    }
    
    public void ChangeClientWaitingTimeNextDay(float valuePercentage)
    {
        VisualNovelManager _visualNovelManager = VisualNovelManager.Instance;
        GameplayManager _gameplayManager = GameplayManager.Instance;
        
        string valueText = valuePercentage.ToString();
        _gameplayManager.variableConfig.clientWaitMultiplier += valuePercentage / 100;
        
        if (valuePercentage >= 0)
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_CLIENTS_STAY_BONUS") + " " + valueText + "% ,");
        }
        else
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_CLIENTS_STAY_MALUS") + " " + valueText.Substring(1,valueText.Length-1) + "% ,");
        }
    }
    
    public void ChangeClientConsomationNextDay(float valuePercentage)
    {
        VisualNovelManager _visualNovelManager = VisualNovelManager.Instance;
        GameplayManager _gameplayManager = GameplayManager.Instance;
        
        string valueText = valuePercentage.ToString();
        _gameplayManager.variableConfig.clientConsoMultiplier += valuePercentage / 100;
        
        if (valuePercentage >= 0)
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_CLIENTS_CONSO_BONUS") + " " + valueText + "% ,");
        }
        else
        {
            _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_CLIENTS_CONSO_MALUS") + " " + valueText.Substring(1,valueText.Length-1) + "% ,");
        }
    }

    public void DayOff(float valuePercentage)
    {
        GameplayManager _gameplayManager = GameplayManager.Instance;
        VisualNovelManager _visualNovelManager = VisualNovelManager.Instance;
        
        _gameplayManager.dayRepertory.dayList.Add(new Day());
        _gameplayManager.indexDay++;
        

        string valueText = valuePercentage.ToString();
        _gameplayManager.variableConfig.clientConsoMultiplier += valuePercentage / 100;
        
        _visualNovelManager.AddStringToEndText(I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_DAY_OFF_1") + " " + valueText + "%" + I2.Loc.LocalizationManager.GetTranslation("ChoiceBonus_Malus/CHOICE_DAY_OFF_2"));
        

    }

    public void AddSequence(TriggeringSequenceVN sequenceVn)
    {
        sequenceVn.ResetTrigger();
        sequenceVn.TriggerSequence();
    }
    
    public void DisplayCat()
    {
        EventObjectManager.Instance.willDisplayCat = true;
    }
    
    
    public void HideCat()
    {
        EventObjectManager.Instance.willHideCat = true;
    }
    
    public void DisplayStatue()
    {
        EventObjectManager.Instance.willDisplayStatue = true;
    }

    public void HideStatue()
    {
        EventObjectManager.Instance.willHideStatue = true;
    }*/
    }
}
