using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingModifier : MonoBehaviour
{
    public static Volume volume;
    public static Bloom bl;
    public static MotionBlur mb;
    public static ChromaticAberration ca;
    public static Vignette vg;
    public static FilmGrain fg;
    public static LensDistortion ld;

    public static bool debuffActivated;
    public float debuffDuration;
    private float debuffDurationCount;

    private void Start()
    {
        volume = GetComponent<Volume>();
    }

    public static void changeBloomProperties(float Threshold, float Intensity, float Scatter)
    {
        if (volume.profile.TryGet<Bloom>(out bl))
        {
            bl.threshold.value = Threshold;
            bl.intensity.value = Intensity;
            bl.scatter.value = Scatter;
        }
    }

    public static void EnableDisableChromaticAberration(bool boolean)
    {
        if (volume.profile.TryGet<ChromaticAberration>(out ca))
            ca.active = boolean;
    }

    public static void EnableDisableMotionBlur(bool boolean)
    {
        if (volume.profile.TryGet<MotionBlur>(out mb))
            mb.active = boolean;
    }

    public static void EnableDisableVignette(bool boolean)
    {
        if (volume.profile.TryGet<Vignette>(out vg))
            vg.active = boolean;
    }

    public static void EnableDisableFilmGrain(bool boolean)
    {
        if (volume.profile.TryGet<FilmGrain>(out fg))
            fg.active = boolean;
    }

    public static void EnableDisableLensDistortion(bool boolean)
    {
        if (volume.profile.TryGet<LensDistortion>(out ld))
            ld.active = boolean;
    }

    public static void DisableLSDEffects() {
        EnableDisableMotionBlur(false);
        EnableDisableChromaticAberration(false);
        EnableDisableVignette(false);
        EnableDisableLensDistortion(false);
        EnableDisableFilmGrain(false);
    }

    public static void EnableLSDEffects()
    {
        EnableDisableMotionBlur(true);
        EnableDisableChromaticAberration(true);
        EnableDisableVignette(true);
        EnableDisableLensDistortion(true);
        EnableDisableFilmGrain(true);
    }

    void Update()
    {
        if (debuffActivated) {
            debuffDurationCount += Time.deltaTime;
            if (debuffDurationCount >= debuffDuration) {
                debuffDurationCount = 0;
                DisableLSDEffects();
                debuffActivated = false;
            }
        }
    }
}

