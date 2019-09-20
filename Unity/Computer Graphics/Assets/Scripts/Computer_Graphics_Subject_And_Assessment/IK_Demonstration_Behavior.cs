using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AI_Agent
{
    #region Private
        #region Bool
        private bool Is_Current_Agent_Avaliable = false;
        private bool Has_AI_Agent_Been_Setup = false;
        private bool Has_Timer_Been_Setup = false;
        #endregion

        #region Float
        private float Current_Timer;
        private float Selected_Time;
    #endregion

        #region Int
        private int List_Counter = 0;
        private int Maximum_Agent_Counter = 10;
        #endregion
    #endregion

    #region Public
        #region Bool
        public bool Avalability = true;
        #endregion

        #region GameObject
        public GameObject Agent_GameObject;
        #endregion
    #endregion

    public void AI_Spawn(float _Timer_Maximum, GameObject _Area_Boundaries, List<AI_Agent> _Agents)
    {
        if(Has_AI_Agent_Been_Setup == false)
        {
            if (Has_Timer_Been_Setup == false) { Current_Timer = 0f; Is_Current_Agent_Avaliable = false; Has_Timer_Been_Setup = true; Selected_Time = Random.Range(0f, _Timer_Maximum); }
            Current_Timer += Time.deltaTime;

            if (Current_Timer >= Selected_Time)
            {
                
                _Agents.Add(new AI_Agent());
                _Agents[List_Counter].Set_Avalability(_Agents, List_Counter, true);

                // Else check to see if one is avaliable for use
                if (_Agents[List_Counter] != null)
                {
                    if (_Agents[List_Counter].Avalability == true) { _Agents[List_Counter].Set_Avalability(_Agents, List_Counter, true); }
                    else { Is_Current_Agent_Avaliable = false; List_Counter++; }
                }

                if (_Agents[List_Counter] != null && !Is_Current_Agent_Avaliable)
                {
                    Renderer Area_Renderer = _Area_Boundaries.GetComponent<Renderer>();

                    float xRange = Area_Renderer.bounds.center.x / 8;
                    float zRange = Area_Renderer.bounds.center.z / 8;

                    // Set Agent's GameObject position
                    // _Agents[_Current_Index].Agent_GameObject = new GameObject("AI");
                    _Agents[List_Counter].Agent_GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    _Agents[List_Counter].Agent_GameObject.name = "AI" + List_Counter;
                    _Agents[List_Counter].Agent_GameObject.transform.localScale = new Vector3(_Agents[List_Counter].Agent_GameObject.transform.localScale.x / 8, _Agents[List_Counter].Agent_GameObject.transform.localScale.y / 8, _Agents[List_Counter].Agent_GameObject.transform.localScale.z / 8);
                    _Agents[List_Counter].Agent_GameObject.transform.position = _Area_Boundaries.transform.position;
                    _Agents[List_Counter].Agent_GameObject.transform.position += new Vector3(Random.Range(-xRange, xRange), 1.0f, Random.Range(-zRange, zRange));

                    Has_Timer_Been_Setup = false;
                    List_Counter += 1;
                }
            }
            if(_Agents.Count > Maximum_Agent_Counter ) { Has_AI_Agent_Been_Setup = true; }
        }
    }

    public void Set_Avalability(List<AI_Agent> _Agents, int _Current_Index, bool _False_Or_Ture)
    {
        // In use
        if (!_False_Or_Ture) { /* _Agents[_Current_Index].Agent_GameObject.SetActive(true); */ _Agents[_Current_Index].Avalability = false; }
        // Recycle to be reused
        else { /* _Agents[_Current_Index].Agent_GameObject.SetActive(false); */ _Agents[_Current_Index].Avalability = true; }
    }
}

public class IK_Demonstration_Behavior : MonoBehaviour {
    #region Private
        #region Animation
        private Animation_I A_I;
        #endregion

        #region Bool
        // Has IK Demonstration Been Setup = HIDBS
        bool HIDBS;
        #endregion

        #region Float
        private float Current_Timer = 0f;
        #endregion

        #region GameObject
        GameObject AI_Agent;
        GameObject Player_GameObject;
        #endregion

        #region Int
        private int Current_Score = 0;
    #endregion

        #region Miscellaneous
        AI_Agent Agent = new global::AI_Agent();
        #endregion
    #endregion

    #region Public
        #region Bool
        //[HideInInspector]
        //public bool Has_Demonstration_Message_Been_Received = false;

        public bool Has_Demonstration_Message_Been_Received;
        #endregion

        #region Float
        public float Float_Timer_Maximum;
        #endregion

        #region GameObject
        public GameObject Area_Boundaries;
        #endregion

        #region List
        private List<AI_Agent> Agents = new List<AI_Agent>();
        public List<Image> Health_Images = new List<Image>();
        #endregion

        #region Text / TextMeshProUGUI
        public TextMeshProUGUI Score_Text;
        public TextMeshProUGUI Timer_Text;
        #endregion
    #endregion

    void Start () {
        A_I = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_I>();

        Agents.Add(Agent);

        Current_Timer = Float_Timer_Maximum;

        // List_Counter += 1;

        Player_GameObject = GameObject.FindGameObjectWithTag("Player");

        Score_Text.text = Current_Score.ToString();
        Timer_Text.text = Current_Timer.ToString();
    }

    void Update () {
        if (Has_Demonstration_Message_Been_Received) { HIDBS = IK_Demonstration_Setup(true); }

        if (HIDBS == true) {
            Agent.AI_Spawn(1f, Area_Boundaries, Agents);

            Current_Timer -= Time.deltaTime;
            Timer_Text.text = Current_Timer.ToString("f0");
        }
    }

    private bool IK_Demonstration_Setup(bool _Has_IK_Demonstration_Been_Setup)
    {
        if (!_Has_IK_Demonstration_Been_Setup)
        {
            A_I.Are_Player_Controls_Enabled = false;
            GameObject.Find("Scene Manager").GetComponent<Camera_Management>().Camera_Enabler("IK Demonstration Camera");

            Player_GameObject.transform.eulerAngles = Vector3.zero;
            Player_GameObject.transform.position = new Vector3(0.09375f, Player_GameObject.transform.position.y, 7.375f);

            _Has_IK_Demonstration_Been_Setup = true;
        }
        return _Has_IK_Demonstration_Been_Setup;
    }
}
