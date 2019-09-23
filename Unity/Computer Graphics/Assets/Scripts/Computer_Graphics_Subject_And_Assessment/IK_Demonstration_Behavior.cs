using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AI_Agent
{
    #region Private
        #region Bool
        // Has area boundaries's verticies been grabbed
        private bool HABVBG = false;
        private bool Has_AI_Agent_Been_Initialized = false;
        private bool Has_Timer_Been_Setup = false;
        private bool Is_Current_Agent_Avaliable = false;
        #endregion

        #region Float
        private float Center;
        private float Current_Timer;
        private float Selected_Time;
        #endregion

        #region Int
        private int List_Counter = 0;
        private int Maximum_Agent_Counter = 10;
        #endregion

        #region List
        // Area Boundary Vertices
        private List<Vector3> AB_Vertices = new List<Vector3>();
        #endregion

        #region Miscellaneous
        private Button_Interaction_Behavior BIB = new Button_Interaction_Behavior();
        private Rigidbody RB;
        #endregion
    #endregion

    #region Public
        #region Bool
        public bool Avalability = true;
        public bool Has_AI_Agent_Been_Setup = false;
        public bool Has_AI_Agent_Reached_Goal = false;
        // Is within distance = IWD
        public bool IWD;
        #endregion

        #region GameObject
        public GameObject Agent_GameObject;
    #endregion

        #region Miscellaneous
        public Steering_Behaviors SB;
        #endregion
    #endregion

    public void AI_Reset_Position(GameObject _Area_Boundaries, int _Current_Index, List<AI_Agent> _Agents)
    {
        _Agents[_Current_Index].Agent_GameObject.transform.position = _Area_Boundaries.transform.position;
        _Agents[_Current_Index].Agent_GameObject.transform.position += new Vector3(Random.Range(-.5f, .5f), .0625f, Random.Range(-.125f, .125f));
    }

    public void AI_Set_Flee_And_Seek_Targets(GameObject _Flee_Target, GameObject _Seek_Target, int _Current_Index, List<AI_Agent> _Agents)
    {
        _Agents[_Current_Index].SB.FleeTarget = _Flee_Target;
        _Agents[_Current_Index].SB.SeekTarget = _Seek_Target;
    }

    public void AI_Spawn(float _Timer_Maximum, GameObject _Area_Boundaries, List<AI_Agent> _Agents)
    {
        if (!HABVBG)
        {
            for (int SJ = 0; SJ < _Area_Boundaries.GetComponent<MeshFilter>().mesh.vertices.Length; SJ++) { AB_Vertices.Add(_Area_Boundaries.GetComponent<MeshFilter>().mesh.vertices[SJ]); }

            float Center = _Area_Boundaries.GetComponent<MeshFilter>().mesh.bounds.center.x;

            HABVBG = true;
        }

        if (Has_AI_Agent_Been_Setup == false)
        {
            if (Has_Timer_Been_Setup == false) { Current_Timer = 0f; Is_Current_Agent_Avaliable = false; Has_Timer_Been_Setup = true; Selected_Time = Random.Range(0f, _Timer_Maximum); }
            Current_Timer += Time.deltaTime;

            if (Current_Timer >= Selected_Time)
            {
                if (List_Counter != 0) { _Agents.Add(new AI_Agent()); }

                if (!_Agents[List_Counter].Has_AI_Agent_Been_Initialized)
                {
                    _Agents[List_Counter].Agent_GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    _Agents[List_Counter].Agent_GameObject.name = "AI" + List_Counter;
                    _Agents[List_Counter].Agent_GameObject.AddComponent<Rigidbody>();
                    _Agents[List_Counter].Agent_GameObject.AddComponent<Steering_Behaviors>();
                    _Agents[List_Counter].Agent_GameObject.transform.localScale = new Vector3(_Agents[List_Counter].Agent_GameObject.transform.localScale.x / 12, _Agents[List_Counter].Agent_GameObject.transform.localScale.y / 12, _Agents[List_Counter].Agent_GameObject.transform.localScale.z / 12);
                    _Agents[List_Counter].Has_AI_Agent_Been_Initialized = true;
                    _Agents[List_Counter].SB = _Agents[List_Counter].Agent_GameObject.GetComponent<Steering_Behaviors>();
                    _Agents[List_Counter].SB.Setup(.5f, _Agents[List_Counter].Agent_GameObject);
                }
                _Agents[List_Counter].AI_Reset_Position(_Area_Boundaries, List_Counter, _Agents);
                _Agents[List_Counter].Set_Avalability(_Agents, List_Counter, false);

                if (_Agents[List_Counter] != null && !Is_Current_Agent_Avaliable)
                {
                    List_Counter += 1;
                }
                Has_Timer_Been_Setup = false;
            }
            if(List_Counter + 1 > Maximum_Agent_Counter ) { Has_AI_Agent_Been_Setup = true; List_Counter = 0; }
        }
        else
        {
            // Check to see if one is avaliable for use
            if (_Agents[List_Counter] != null)
            {
                if (_Agents[List_Counter].Avalability == true) { _Agents[List_Counter].Set_Avalability(_Agents, List_Counter, false); _Agents[List_Counter].AI_Reset_Position(_Area_Boundaries, List_Counter, _Agents); _Agents[List_Counter].Has_AI_Agent_Reached_Goal = false; }
                else { List_Counter++; }

                if (List_Counter > _Agents.Count - 1) { List_Counter = 0; }
            }
        }
    }

    public void Set_Avalability(bool _False_Or_Ture)
    {
        // In use
        if (!_False_Or_Ture) { this.Agent_GameObject.SetActive(true);  this.Avalability = false; }
        // Recycle to be reused
        else { this.Agent_GameObject.SetActive(false); this.Avalability = true; }
    }

    public void Set_Avalability(List<AI_Agent> _Agents, int _Current_Index, bool _False_Or_Ture)
    {
        // In use
        if (!_False_Or_Ture) { _Agents[_Current_Index].Agent_GameObject.SetActive(true); _Agents[_Current_Index].Avalability = false; }
        // Recycle to be reused
        else { _Agents[_Current_Index].Agent_GameObject.SetActive(false); _Agents[_Current_Index].Avalability = true; }
    }
}

public class IK_Demonstration_Behavior : MonoBehaviour {
    #region Private
        #region Animation
        private Animation_I A_I;
        private Animation_II A_II;
        #endregion

        #region Bool
        private bool Has_Game_Ended = false;
        // Has IK Demonstration Been Setup = HIDBS
        private bool HIDBS;
        #endregion

        #region Float
        private float Current_Timer = 0f;
        #endregion

        #region GameObject
        GameObject AI_Agent;
        GameObject Player_GameObject;
        #endregion

        #region Int
        private int Current_Health = 2;
        private int Current_Score = 0;
        private int Hit_Counter = 0;
        #endregion

        #region Miscellaneous
        private AI_Agent Agent = new global::AI_Agent();

        private Camera_Management C_M;
        #endregion
    #endregion

    #region Public
        #region Bool
        [HideInInspector]
        public bool Has_Demonstration_Message_Been_Received;
        [HideInInspector]
        public bool Is_Within_Distance;
        #endregion

        #region Float
        public float Float_Timer_Maximum;
        #endregion

        #region GameObject
        public GameObject Area_Boundaries;
        public GameObject FleeTarget;
        public GameObject SeekTarget;
        #endregion

        #region List
        private List<AI_Agent> Agents = new List<AI_Agent>();
        public List<GameObject> Collision_Objects = new List<GameObject>();
        public List<Image> Health_Images = new List<Image>();
        #endregion

        #region Text / TextMeshProUGUI
        public TextMeshProUGUI Defeat_Text;
        public TextMeshProUGUI Lose_Text;
        public TextMeshProUGUI Score_Text;
        public TextMeshProUGUI Timer_Text;
        public TextMeshProUGUI Win_Text;
        #endregion

    #region Transform
    public Transform Left_Arm_Object;
        #endregion
    #endregion

    void Start () {
        A_I = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_I>();
        A_II = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_II>();

        Agents.Add(Agent);

        C_M = GameObject.FindGameObjectWithTag("Scene Manager").GetComponent<Camera_Management>();

        Current_Timer = Float_Timer_Maximum;

        Player_GameObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update () {
        if (!Has_Game_Ended)
        {
            if (Has_Demonstration_Message_Been_Received)
            {
                Health_Images[0].transform.parent.gameObject.SetActive(true);
                HIDBS = IK_Demonstration_Setup(false);
                Score_Text.transform.parent.gameObject.SetActive(true);
                Score_Text.text = Current_Score.ToString();
                Timer_Text.transform.parent.gameObject.SetActive(true);
                Timer_Text.text = Current_Timer.ToString();
            }

            if (HIDBS == true)
            {
                A_II.Is_IK_Active = true;
                A_II.C = C_M.Camera_Select("IK Demonstration Camera");
                A_II.Left_Hand_Object = Left_Arm_Object;

                Agent.AI_Spawn(1f, Area_Boundaries, Agents);

                Current_Timer -= Time.deltaTime;

                if(Current_Timer <= 0)
                {
                    Lose_Text.enabled = true;
                    Current_Timer = 0;
                    Has_Game_Ended = true;
                }

                Player_GameObject.transform.position = new Vector3(0.09375f, -.05f, 7.375f);

                Timer_Text.text = Current_Timer.ToString("f0");
            }

            if (Agents != null)
            {
                for (int SJ = 0; SJ < Agents.Count; SJ++)
                {
                    if (Agents[SJ].Agent_GameObject != null)
                    {
                        Agents[SJ].AI_Set_Flee_And_Seek_Targets(FleeTarget, SeekTarget, SJ, Agents);
                        Agents[SJ].SB.Check_For_Collision(Collision_Objects);
                        if (Agents[SJ].SB.Has_Object_Collided)
                        {
                            if (!Agents[SJ].Has_AI_Agent_Reached_Goal) { Agents[SJ].Has_AI_Agent_Reached_Goal = true; Current_Score += 1; Score_Text.text = Current_Score.ToString();
                            }
                            Agents[SJ].SB.Has_Object_Collided = false;
                            Agents[SJ].Set_Avalability(true);
                            
                            if(Current_Score >= 50)
                            {
                                Win_Text.enabled = true;
                                Has_Game_Ended = true;
                            }
                        }
                        if (Vector3.Distance(Agents[SJ].Agent_GameObject.transform.position, SeekTarget.transform.position) < .25f)
                        {
                            Agents[SJ].Set_Avalability(true);

                            if (!Agents[SJ].Has_AI_Agent_Reached_Goal) { Agents[SJ].Has_AI_Agent_Reached_Goal = true; Hit_Counter += 1; }
                            if(Hit_Counter == 10)
                            {
                                Health_Images[Current_Health].enabled = false;
                                // Health_Images.Remove(Health_Images[Current_Health]);
                                Hit_Counter = 0;
                                Current_Health -= 1;
                            }

                            if (Current_Health < 0) { Has_Game_Ended = true; }
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.U)) { Agent.Set_Avalability(Agents, Random.Range(0, Agents.Count - 1), true); }
            }
        }
        else
        {
            Defeat_Text.enabled = true;
            if (Input.GetKeyDown(KeyCode.E)) { On_Reset(); }
        }
    }

    private bool IK_Demonstration_Setup(bool _Has_IK_Demonstration_Been_Setup)
    {
        if (!_Has_IK_Demonstration_Been_Setup)
        {
            A_I.Are_Player_Controls_Enabled = false;
            GameObject.Find("Scene Manager").GetComponent<Camera_Management>().Camera_Enabler("IK Demonstration Camera");

            Has_Demonstration_Message_Been_Received = false;
            _Has_IK_Demonstration_Been_Setup = true;

            Player_GameObject.transform.eulerAngles = Vector3.zero;
        }
        return _Has_IK_Demonstration_Been_Setup;
    }

    private void On_Reset()
    {
        Current_Health = 2;
        for(int SJ = 0; SJ < Health_Images.Count; SJ++) { Health_Images[SJ].enabled = true; }
        Current_Score = 0;
        Current_Timer = Float_Timer_Maximum;
        
        Defeat_Text.enabled = false;
        Lose_Text.enabled = false;
        Win_Text.enabled = false;

        Has_Game_Ended = false;
    }
}
