using System.Collections;
using System.Collections.Generic;
using MoreMountains.FeedbacksForThirdParty;
using Lofelt.NiceVibrations;
using UnityEngine;

public class Haptic : Singleton<Haptic>
{
    public void SetHaptic(HapticPatterns.PresetType type = HapticPatterns.PresetType.MediumImpact)
    {
        if (DeviceCapabilities.isVersionSupported)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
        }
    }
}
