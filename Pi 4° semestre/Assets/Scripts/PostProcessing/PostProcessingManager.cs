using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager instance;

    private Volume globalVolume;
    private Bloom bloom;
    private ChromaticAberration chromaticAberration;
    private ColorAdjustments colorAdjustment;
    private Vignette vignette;
    private FilmGrain filmGrain;

    #region INITIAL VALUES
    //Bloom
    private float bThreshold;
    private float bIntensity;
    private float bScatter;
    //Chromatic Aberration
    private float caIntensity;
    //Color Adjustment
    private float colAdjSaturation;
    //Vignette
    private Color vigColor;
    private float vigIntensity;
    //Film Grain
    private float filmGrainIntensity;
    #endregion

    #region SETTING INITIAL VALUES
    private void Awake()
    {
        instance = this;

        globalVolume = GetComponent<Volume>();
        globalVolume.profile.TryGet(out bloom);
        globalVolume.profile.TryGet(out chromaticAberration);
        globalVolume.profile.TryGet(out vignette);
        globalVolume.profile.TryGet(out colorAdjustment);
        globalVolume.profile.TryGet(out filmGrain);
    }

    private void Start()
    {
        bThreshold = bloom.threshold.value;
        bIntensity = bloom.intensity.value;
        bScatter = bloom.scatter.value;

        caIntensity = chromaticAberration.intensity.value;

        colAdjSaturation = colorAdjustment.saturation.value;

        vigColor = vignette.color.value;
        vigIntensity = vignette.intensity.value;

        filmGrainIntensity = filmGrain.intensity.value;
    }
    #endregion

    public void SetTemporaryBloom(float threshold, float intensity, float scatter, float time) 
    {
        StartCoroutine(SetTempBloom(threshold, intensity, scatter, time));
    }

    private IEnumerator SetTempBloom(float threshold, float intensity, float scatter, float time)
    {
        bloom.threshold.value = threshold;
        bloom.intensity.value = intensity;
        bloom.scatter.value = scatter;

        yield return new WaitForSeconds(time);

        bloom.threshold.value = bThreshold;
        bloom.intensity.value = bIntensity;
        bloom.scatter.value = bScatter;
    }

    public void SetTemporaryChromaticAberration(float intensity, float time) 
    {
        StartCoroutine(SetTempChromaticAberration(intensity, time));
    }

    private IEnumerator SetTempChromaticAberration(float intensity, float time)
    {
        chromaticAberration.intensity.value = intensity;

        yield return new WaitForSeconds(time);

        chromaticAberration.intensity.value = caIntensity;
    }

    public void SetTemporaryVignette(Color color, float intensity, float time) 
    {
        StartCoroutine(SetTempVignette(color, intensity, time));
    }

    private IEnumerator SetTempVignette(Color color, float intensity, float time) 
    {
        vignette.color.value = color;
        vignette.intensity.value = intensity;

        yield return new WaitForSeconds(time);

        vignette.color.value = vigColor;
        vignette.intensity.value = vigIntensity;
    }

    public void SetColorAdjustment(float saturation) 
    {
        colorAdjustment.saturation.value = saturation;
    }

    public void SetFilmGrain(float intensity) 
    {
        filmGrain.intensity.value = intensity;
    }
}
