using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button_Interaction_Behavior : MonoBehaviour {

    #region Private
    private Animation_I A_I;
    private Animation_II A_II;

    private bool Has_Switch_Been_Activated = false;
    private bool Is_Player_On_Trigger = false;

    private float Timer = 0.0f;

    private GameObject Player_GameObject;

    private Light Light_Object;

    private Quaternion Player_Rotation;
    #endregion

    #region Public
    public float Distance_Between_Objects;
    // Distance between Player's left arm and the trigger
    public float Distance_Between_Trigger;

    public List<GameObject> Light_GameObjects = new List<GameObject>();

    public TextMeshProUGUI Button_Text;

    public Transform Left_Arm_Transform;
    #endregion

    void Start () {
        A_I = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_I>();

        Button_Text.enabled = false;
	}

    private void Update()
    {
        if(Vector3.Distance(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < Distance_Between_Objects)
        {
            if (A_I.Are_Player_Controls_Enabled == true) { Button_Text.enabled = true; }
            if (Input.GetKeyDown(KeyCode.E) && Is_Player_On_Trigger == false)
            {
                Disable_Or_Enable_Controls(false);
            }
            else if(Is_Player_On_Trigger == true)
            {
                A_I.Turn_Reset();
                if(Vector3.Distance(Left_Arm_Transform.position, this.gameObject.transform.position) < Distance_Between_Trigger)
                {
                    Debug.Log("Stefanie Joosten says hello ( =");
                    // This would turn on all of the lights
                    Triggle_Particle_Event("Execute Order 66");
                    Is_Player_On_Trigger = false;
                }
            }
        }
        else { Button_Text.enabled = false; Is_Player_On_Trigger = false; }
    }

    private void Enable_Button()
    {
        A_II = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_II>();

        A_II.Left_Hand_Object = this.gameObject.transform;
        A_II.Is_IK_Active = true;
    }

    private void Disable_Or_Enable_Controls(bool _False_Or_True)
    {
        if(_False_Or_True == false)
        {
            A_I.Are_Player_Controls_Enabled = false;
            A_I.Turn_Reset();

            Button_Text.enabled = false;

            Camera.main.transform.parent = null;

            Enable_Button();

            Is_Player_On_Trigger = true;

            Player_GameObject = GameObject.FindGameObjectWithTag("Player");

            Player_GameObject.transform.position = new Vector3(.625f, 0.1f, -0.875f);
            Player_GameObject.transform.eulerAngles = new Vector3(0f, 180.0f, 0f);

            Camera.main.transform.eulerAngles = new Vector3(0f, -90f, 0f);
            Camera.main.transform.position = new Vector3(1.25f, 0.625f, -0.75f);

            Camera.main.transform.parent = Player_GameObject.transform;
        }
        else
        {
            A_I.Are_Player_Controls_Enabled = true;

            Camera.main.transform.eulerAngles = new Vector3(16.875f, 0f, 0f);
            Camera.main.transform.position = new Vector3(0f, 1.45f, -2f);

            Player_GameObject = GameObject.FindGameObjectWithTag("Player");
            Player_GameObject.transform.eulerAngles = Vector3.zero;
        }
    }

    private void Triggle_Particle_Event(string _Event)
    {
        if(_Event == /*"LightOn"*/ "Execute Order 66")
        {
            if(Has_Switch_Been_Activated == false)
            {
                for (int SJ = 0; SJ < Light_GameObjects.Count; SJ++)
                {
                    ParticleSystem Particle_System;
                    if (Light_GameObjects[SJ].GetComponent<ParticleSystem>() != null)
                    {
                        Particle_System = Light_GameObjects[SJ].GetComponent<ParticleSystem>();
                        ParticleSystem.EmissionModule E_M = Particle_System.emission;

                        // ParticleSystem.Burst(Time, Count, Cycles, Interval)
                        // Default Value(Time: 0.0, Count: 2, Cycles: 3, Interval: 0.250)
                        E_M.SetBurst(0, new ParticleSystem.Burst(0.0f, 2, 22, 0.250f));
                    }
                }
                Has_Switch_Been_Activated = true;
            }
            if(Has_Switch_Been_Activated == true)
            {
                for (int SJ = 0; SJ < Light_GameObjects.Count; SJ++)
                {
                    ParticleSystem Particle_System = Light_GameObjects[SJ].GetComponent<ParticleSystem>();
                    ParticleSystem.EmissionModule E_M = Particle_System.emission;
                    E_M.enabled = false;
                }
                for (int SJ = 0; SJ < Light_GameObjects.Count; SJ++)
                {
                    if (Light_GameObjects[SJ].GetComponent<ParticleSystem>() != null)
                    {
                        GameObject GO;
                        GO = Light_GameObjects[SJ];
                        Light_Object = GO.transform.Find("Light Bulb").GetComponent<Light>();
                        // Renderer Renderer_Object = GO.GetComponent<Renderer>();
                        Renderer Renderer_Object = GO.transform.Find("Light Bulb").GetComponent<Renderer>();
                        Light_Object.enabled = true;

                        // Light Bulb's default emission values (R: 161, G: 150, B: 81, A: 0)
                        Color Emission_Color = Renderer_Object.material.GetColor("_EmissionColor");
                        Emission_Color = Vector4.Normalize(Emission_Color);

                        Renderer_Object.material.SetColor("_EmissionColor", Emission_Color);
                    }
                }
                Disable_Or_Enable_Controls(true);
            }
        }
    }
}