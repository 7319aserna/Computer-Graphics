using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_Interaction_Behavior : MonoBehaviour {

    #region Private
    #region Animation
    private Animation_I A_I;
    private Animation_II A_II;

    private Animator IK_Demonstration_Animator;
    #endregion

    #region Bool
    private bool Has_Event_Been_Triggered = false;
    private bool Has_Max_Emission_Been_Met = false;
    private bool Has_Particle_System_Been_Setup = false;
    private bool Has_Switch_Been_Activated = false;
    private bool Is_Player_On_Trigger = false;
    #endregion

    #region Camera
    private Camera_Management C_M;
    #endregion

    #region Float
    private float Starting_Simulation_Speed = 0f;
    private float Timer = 0.0f;
    #endregion

    #region GameObject
    private GameObject IK_Demonstration_GO;
    private GameObject Player_GameObject;
    private GameObject Pressure_Plate_GO;
    #endregion

    #region Miscellaneous
    private Light Light_Object;

    private Quaternion Player_Rotation;

    private string Default_Event;
    #endregion
    #endregion

    #region Public
    #region Bool
    [HideInInspector]
    public bool Execute_Order_66;
    [HideInInspector]
    public bool IK_Demonstration;
    [HideInInspector]
    public bool IK_Minigame;
    [HideInInspector]
    public bool Instantiate_Main_Stage;
    #endregion

    #region Float
    [HideInInspector]
    public float Distance_Between_Objects;
    // Distance between Player's left arm and the trigger
    [HideInInspector]
    public float Distance_Between_Trigger;
    [HideInInspector]
    public float Max_Simulation_Speed;
    #endregion

    #region Miscellaneous
    public List<GameObject> Light_GameObjects = new List<GameObject>();

    [HideInInspector]
    public string Current_Event;

    [HideInInspector]
    public TextMeshProUGUI Button_Text;

    [HideInInspector]
    public Transform Left_Arm_Transform;
    #endregion
    #endregion

    void Start() {
        Debug.Log(Current_Event);

        A_I = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_I>();

        if (Current_Event == "Execute Order 66") { Button_Text.enabled = false; }

        C_M = GameObject.FindGameObjectWithTag("Scene Manager").GetComponent<Camera_Management>();

        if (Current_Event == "Instantiate Main Stage")
        {
            IK_Demonstration_GO = GameObject.Find("IK Demonstration");
            IK_Demonstration_Animator = IK_Demonstration_GO.GetComponent<Animator>();
            IK_Demonstration_GO.SetActive(false);
        }

        Player_GameObject = GameObject.FindGameObjectWithTag("Player");

        Pressure_Plate_GO = GameObject.Find("Pressure Plate");
    }

    private void Update()
    {
        Trigger_Event(Current_Event);
        Triggle_Particle_Event(Current_Event);
    }

    private bool Set_Event_Trigger(bool Disabled_Or_Enabled)
    {
        ////if (Disabled_Or_Enabled) { False_Or_True = false; }
        ////else { False_Or_True = true; }

        return False_Or_True;
    }

    public void Enable_IK(bool _False_Or_True)
    {
        A_II = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_II>();

        if (!_False_Or_True)
        {
            A_II.Left_Hand_Object = null;
            A_II.Is_IK_Active = false;
        }
        else
        {
            A_II.Left_Hand_Object = this.gameObject.transform;
            A_II.Is_IK_Active = true;
        }
    }

    private void Disable_Or_Enable_Controls(bool _False_Or_True)
    {
        if(_False_Or_True == false)
        {
            A_I.Are_Player_Controls_Enabled = false;
            A_I.Turn_Reset();

            Button_Text.enabled = false;

            Enable_IK(true);

            Is_Player_On_Trigger = true;

            Player_GameObject.transform.eulerAngles = new Vector3(0f, -180f, 0f);
            Player_GameObject.transform.position = new Vector3(.5f, Player_GameObject.transform.position.y, -.875f);
        }
        else { A_I.Are_Player_Controls_Enabled = true; Enable_IK(false); }
    }

    private void Trigger_Event(string _Event)
    {
        if(_Event == "Instantiate Main Stage")
        {
            Default_Event = "Instantiate Main Stage"
            Transform Pressure_Point_Pivot_Point;
            Pressure_Point_Pivot_Point = gameObject.transform.GetChild(1);
            if(Vector3.Distance(Player_GameObject.transform.position, Pressure_Point_Pivot_Point.transform.position) < Distance_Between_Trigger)
            {
                A_I.Are_Player_Controls_Enabled = false;
                A_I.Turn_Reset();

                Current_Event = "IK Demonstration";

                Timer = 0f;
            }
        }

        if(_Event == "IK Minigame")
        {

        }
        return;
    }

    private void Triggle_Particle_Event(string _Event)
    {
        if(_Event == /*"LightOn"*/ "Execute Order 66")
        {
            if (Pressure_Plate_GO != null) { Pressure_Plate_GO.SetActive(false); }

            if (Vector3.Distance(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < Distance_Between_Objects)
            {
                if (A_I.Are_Player_Controls_Enabled == true) { Button_Text.enabled = true; }
                if (Input.GetKeyDown(KeyCode.E) && Is_Player_On_Trigger == false)
                {
                    C_M.Camera_Enabler("Button Camera");
                    Disable_Or_Enable_Controls(false);
                }
                else if (Is_Player_On_Trigger == true)
                {
                    A_I.Turn_Reset();
                    if (Vector3.Distance(Left_Arm_Transform.position, this.gameObject.transform.position) < Distance_Between_Trigger)
                    {
                        C_M.Camera_Enabler("Main Camera");
                        Has_Event_Been_Triggered = true;
                        Disable_Or_Enable_Controls(true);
                        Is_Player_On_Trigger = false;
                    }
                }
            }
            else { Button_Text.enabled = false; Is_Player_On_Trigger = false; }

            if (Has_Event_Been_Triggered)
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
                Debug.Log("Timer: " + Timer);
                Timer += Time.deltaTime;
                if(Timer >= 5.0f)
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
                            // Emission_Color = Vector4.Normalize(Emission_Color);
                            Emission_Color = Color.yellow;

                            Renderer_Object.material.SetColor("_EmissionColor", Emission_Color);

                            if (Pressure_Plate_GO != null) { Pressure_Plate_GO.SetActive(true); }
                        }
                    }
                    Current_Event = null;
                    Timer = 0f;
                }
            }
        }

        if(Current_Event == "IK Demonstration")
        {
            int Maximum_Particles;

            ParticleSystem Particle_System;
            Particle_System = gameObject.GetComponentInChildren<ParticleSystem>();
            ParticleSystem.EmissionModule E_M = Particle_System.emission;
            ParticleSystem.MainModule M_M = Particle_System.main;

            if (!Has_Particle_System_Been_Setup)
            {
                Has_Particle_System_Been_Setup = true;
                M_M.simulationSpeed = Starting_Simulation_Speed;
                Particle_System.Play();
            }

            if (!Has_Max_Emission_Been_Met)
            {
                M_M.simulationSpeed += Time.deltaTime / 16;

                if(M_M.simulationSpeed >= Max_Simulation_Speed / 2) { IK_Demonstration_GO.SetActive(true); IK_Demonstration_Animator.enabled = true; }

                if (M_M.simulationSpeed > Max_Simulation_Speed)
                {
                    Has_Max_Emission_Been_Met = true;
                    M_M.simulationSpeed = Max_Simulation_Speed;
                }
            }
            else
            {
                if (M_M.simulationSpeed == Max_Simulation_Speed)
                {
                    M_M.maxParticles -= 1;
                    if (M_M.maxParticles <= 0) { M_M.maxParticles = 0; }
                }

                if (M_M.maxParticles == 0) { Particle_System.Stop(); }
            }
        }
        return;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Button_Interaction_Behavior))]
public class Button_Interaction_Behavior_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Button_Interaction_Behavior B_I_B = (Button_Interaction_Behavior)target;

        EditorGUILayout.LabelField("Particle Events");

        B_I_B.Execute_Order_66 = EditorGUILayout.Toggle("Execute Order 66", B_I_B.Execute_Order_66);
        if (B_I_B.Execute_Order_66)
        {
            B_I_B.Current_Event = "Execute Order 66";
            B_I_B.Distance_Between_Objects = EditorGUILayout.FloatField("Distance Between Objects", B_I_B.Distance_Between_Objects);
            B_I_B.Distance_Between_Trigger = EditorGUILayout.FloatField("Distance Between Trigger", B_I_B.Distance_Between_Trigger);
            B_I_B.Button_Text = EditorGUILayout.ObjectField("Button Text", B_I_B.Button_Text, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
            B_I_B.Left_Arm_Transform = EditorGUILayout.ObjectField("Left Arm Transform", B_I_B.Left_Arm_Transform, typeof(Transform), true) as Transform;
        }

        EditorGUILayout.LabelField("Trigger Events");

        B_I_B.Instantiate_Main_Stage = EditorGUILayout.Toggle("Instantiate Main Stage", B_I_B.Instantiate_Main_Stage);
        if (B_I_B.Instantiate_Main_Stage)
        {
            B_I_B.Current_Event = "Instantiate Main Stage";
            B_I_B.Distance_Between_Trigger = EditorGUILayout.FloatField("Distance Between Trigger", B_I_B.Distance_Between_Trigger);
            B_I_B.Max_Simulation_Speed = EditorGUILayout.FloatField("Maximum Simulation Speed: ", B_I_B.Max_Simulation_Speed);
        }

        B_I_B.IK_Minigame = EditorGUILayout.Toggle("IK Minigame", B_I_B.IK_Minigame);
        if (B_I_B.IK_Minigame) { B_I_B.Current_Event = "IK Minigame"; }
    }
}
#endif