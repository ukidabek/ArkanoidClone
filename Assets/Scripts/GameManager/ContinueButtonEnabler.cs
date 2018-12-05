using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonEnabler : MonoBehaviour
{
    [SerializeField] private Button _continueButton = null;
    [SerializeField] private SaveLoadManager saveLoadManager = null;

    public void ValidateContinieOption()
    {
        if (saveLoadManager != null && _continueButton != null)
            _continueButton.interactable = saveLoadManager.SaveExists;
        else if (_continueButton != null)
            _continueButton.interactable = false;
    }
}
