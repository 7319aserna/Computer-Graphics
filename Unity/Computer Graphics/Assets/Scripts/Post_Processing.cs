using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// ***--- Post-Processing Volumes ---***

// ***-------------------------------***

public class Post_Processing : MonoBehaviour {
    #region Private
    // ***--- Bleeding Out ---***
    // Enable effect at percentage of remaining health = EEAPORH
    private float EEAPORH;

    private int Current_Health;

    private Vignette M_Vignette;
    // ***--------------------***

    private int Vignette_Intensity;

    private PostProcessVolume M_Volume;

    // ***--- Post-Processing Volumes ---***
    private Bloom M_Bloom;
    private ChromaticAberration M_ChromaticAberration;
    // ***-------------------------------***
    #endregion

    #region Public
    public bool Is_Bloom_Enabled;
    public bool Is_Chromatic_Abberation_Enabled;
    public bool Is_Vignette_Enabled;

    // ***--- Bleeding Out ---***
    public int Max_Health;
    // Enable effect at a percentage of health remaining (would only go up to 100%)
    public float Enable_Effect_At_Percentage_Of_Remaining_Health;

    public TextMeshProUGUI Health_Value_TMP;
    // ***--------------------***
    #endregion

    void Start () {
        // ***--- Bleeding Out ---***
        if(Is_Vignette_Enabled == true)
        {
            M_Vignette = ScriptableObject.CreateInstance<Vignette>();
            M_Vignette.enabled.Override(true);
            // At start, this would display a Vignette filter over the screen
            // M_Vignette.intensity.Override(1f);

            M_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, M_Vignette);
            // M_Volume.weight = 0f;

            Current_Health = Max_Health;

            if (Enable_Effect_At_Percentage_Of_Remaining_Health > 1f) { Enable_Effect_At_Percentage_Of_Remaining_Health = 1f; }
            else if (Enable_Effect_At_Percentage_Of_Remaining_Health < 0f) { Enable_Effect_At_Percentage_Of_Remaining_Health = 0f; }

            EEAPORH = Max_Health * Enable_Effect_At_Percentage_Of_Remaining_Health;
            Vignette_Intensity = Mathf.FloorToInt(EEAPORH / 4);
        }
        // ***--------------------***

        // ***--- Post-Processing Volumes ---***
        if(Is_Bloom_Enabled == true)
        {
            M_Bloom = ScriptableObject.CreateInstance<Bloom>();
            M_Bloom.enabled.Override(true);

            M_Bloom.intensity.Override(1f);

            M_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, M_Bloom);
        }

        if (Is_Chromatic_Abberation_Enabled == true)
        {
            M_ChromaticAberration = ScriptableObject.CreateInstance<ChromaticAberration>();
            M_ChromaticAberration.enabled.Override(true);

            M_ChromaticAberration.intensity.Override(1f);

            M_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, M_ChromaticAberration);
            // M_Volume.weight = 0f;
        }
        // ***-------------------------------***
    }

    void Update () {
        // ***--- Bleeding Out ---***
        if(Is_Vignette_Enabled == true)
        {
            // Take the max amount of health, and once the max health gets to the percentage amount, enable the post-processing effect.
            if (Current_Health <= EEAPORH)
            {
                // Change intensity based on amount of health
                Debug.Log("Vignette Intensity at 100%: " + Vignette_Intensity * 4);
                Debug.Log("Vignette Intensity at 75%: " + Vignette_Intensity * 3);
                Debug.Log("Vignette Intensity at 50%: " + Vignette_Intensity * 2);
                Debug.Log("Vignette Intensity at 25%: " + Vignette_Intensity);
                
                if(Max_Health != 0)
                {
                    // If at 100%
                    if (Current_Health > Vignette_Intensity * 3 && Current_Health < Vignette_Intensity * 4) { M_Vignette.intensity.Override(1f); M_Vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup); }
                    // If at 75%
                    if (Current_Health > Vignette_Intensity * 2 && Current_Health < Vignette_Intensity * 3) { M_Vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup * 2); }
                    // If at 50%
                    if (Current_Health > Vignette_Intensity && Current_Health < Vignette_Intensity * 2) { M_Vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup * 3); }
                    // If at 25%
                    if (Current_Health > 0 && Current_Health < Vignette_Intensity) { M_Vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup * 4); }
                }
                else { M_Vignette.intensity.Override(1f); M_Vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);}
            }
            else { Is_Vignette_Enabled = false; M_Vignette.intensity.Override(0f); }

            // Receive Health
            if (Current_Health > Max_Health) { Current_Health = Max_Health; }
            else if (Input.GetKeyDown(KeyCode.H)) { Current_Health += 10; }

            // Take Damage
            if (Current_Health <= 0) { Current_Health = Max_Health; }
            if (Input.GetKeyDown(KeyCode.D)) { Current_Health -= 10; }

            Health_Value_TMP.text = Current_Health.ToString();
        }
        // ***--------------------***

        // ***--- Post-Processing Volumes ---***
        if(Is_Bloom_Enabled == true) { M_Bloom.intensity.value = Mathf.Sin(Time.realtimeSinceStartup); }
        if (Is_Chromatic_Abberation_Enabled == true) { M_ChromaticAberration.intensity.value = Mathf.Sin(Time.realtimeSinceStartup); }
        // ***-------------------------------***
    }

    private void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(M_Volume, true, true);
    }
}
