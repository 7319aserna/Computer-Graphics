using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Engine_Materials_and_Lighting : MonoBehaviour {
    #region Private
    // *****----- Material Properties, Pt. 1 / Material Properties, Pt. 2 -----*****
    private int Texture_Counter = 0;

    private Renderer Main_Renderer;
    // *****-------------------------------------------------------------------*****

    // *****----- Smooth Dimmer -----*****
    // Is_Dimmer_Time_Increasing_Or_Decreasing: If false - Dimmer Timer is decreasing / Else if true - Dimmer Timer is increasing
    private bool Is_Dimmer_Time_Increasing_Or_Decreasing;
    // Has difference in seconds been found = HDISBF
    private bool HDISBF = false;

    // New Intensity / Span of Time = Difference (per second)
    private float Difference;
    private float Dimmer_Timer = 0f;

    private Light Main_Light;
    // *****-------------------------*****    

    // *****----- Fade on Death -----*****
    private bool Fade_Per_Second_Bool = false;
    private bool Has_Been_Clicked = false;

    private float Alpha_Channel = 1f;
    // Transparency / Fade Timer = Fade_Per_Second
    private float Fade_Per_Second = 0f;
    // *****-------------------------*****
    #endregion

    #region Public
    // *****----- Material Properties, Pt. 1 -----*****
    public Color Main_Color;
    // *****--------------------------------------*****

    // *****----- Material Properties, Pt. 2 -----*****
    public List<Texture> Texture_List = new List<Texture>();
    // *****--------------------------------------*****

    // *****----- Smooth Dimmer -----*****
    // Light Toggle: If false - light is off / Else if true - light is on
    public bool Light_Toggle;

    public float Current_Intensity;
    public float New_Intensity;
    public float Span_Of_Time;
    // *****-------------------------*****    

    // *****----- Fade on Death -----*****
    public float Fade_Timer = 0f;
    // *****-------------------------*****
    #endregion

    void Start () {
        // *****----- Material Properties, Pt. 1 / Material Properties, Pt. 2 -----*****
        if (gameObject.GetComponent<Renderer>() != null)
        {
            Main_Renderer = gameObject.GetComponent<Renderer>();
            Main_Renderer.material.mainTexture = Texture_List[Texture_Counter];
        }
        // *****-------------------------------------------------------------------*****

        // *****----- Smooth Dimmer -----*****
        if (gameObject.GetComponent<Light>() != null)
        {
            Is_Dimmer_Time_Increasing_Or_Decreasing = true;

            Main_Light = gameObject.GetComponent<Light>();
            Main_Light.intensity = Current_Intensity;
        }
        // *****-------------------------*****
    }

    void Update () {
        // *****----- Material Properties, Pt. 2 -----*****
        if (Input.GetKeyDown(KeyCode.Space) && gameObject.GetComponent<Renderer>() != null)
        {
            if (Texture_Counter == Texture_List.Count - 1)
            {
                Texture_Counter = 0;
            }
            else
            {
                Texture_Counter++;
            }
            Main_Renderer.material.mainTexture = Texture_List[Texture_Counter];
        }
        // *****--------------------------------------*****
        // *****----- Material Properties, Pt. 1 -----*****
        if(gameObject.GetComponent<Renderer>() != null)
        {
            Main_Renderer.material.color = Main_Color;
        }
        // *****--------------------------------------*****
        
        // *****----- Smooth Dimmer -----*****
        if (gameObject.GetComponent<Light>() != null)
        {
            if (Light_Toggle == false) { Main_Light.enabled = false; }
            else { Main_Light.enabled = true; }

            if (HDISBF == false)
            {
                Difference = New_Intensity / Span_Of_Time;
                HDISBF = true;
            }

            if (Is_Dimmer_Time_Increasing_Or_Decreasing == true)
            {
                if(Dimmer_Timer > Span_Of_Time) { Is_Dimmer_Time_Increasing_Or_Decreasing = false; }
                else
                {
                    Dimmer_Timer += Time.deltaTime;
                    Main_Light.intensity += Difference * Time.deltaTime;
                } 
            }
            else
            {
                if(Dimmer_Timer < 0f) { Is_Dimmer_Time_Increasing_Or_Decreasing = true; }
                else
                {
                    Dimmer_Timer -= Time.deltaTime;
                    Main_Light.intensity -= Difference * Time.deltaTime;
                }
            }
        }
        // *****-------------------------*****

        // *****----- Fade on Death -----*****
        if (gameObject.GetComponent<Renderer>() != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Has_Been_Clicked = true;
            }
            if (Has_Been_Clicked == true)
            {
                if (Fade_Per_Second_Bool == false)
                {
                    Fade_Per_Second = Alpha_Channel / Fade_Timer;
                    Fade_Per_Second_Bool = true;
                }
                if (Alpha_Channel <= 0f || Fade_Timer <= 0f)
                {
                    Alpha_Channel = 0f;
                    Fade_Timer = 0f;
                }
                else
                {
                    Alpha_Channel -= Fade_Per_Second * Time.deltaTime;
                    Fade_Timer -= Time.deltaTime;
                }
                Main_Renderer.material.color = new Color(Main_Color.r, Main_Color.g, Main_Color.b, Alpha_Channel);
            }
            else { Main_Renderer.material.color = new Color(Main_Color.r, Main_Color.g, Main_Color.b, Alpha_Channel); }
        }
        // *****-------------------------*****
    }
}