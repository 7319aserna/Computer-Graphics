using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AI_Agent
{
    #region Private
        #region Bool
        private bool Has_AI_Agent_Been_Setup = false;
        private bool Has_Timer_Been_Setup = false;
        #endregion

        #region Float
        private float Current_Timer;
        private float Selected_Time;
        #endregion

        #region Miscellaneous
        private AI_Agent Agent;
        #endregion
    #endregion

    #region Public
        #region Bool
        public bool Avalability;
        #endregion

        #region GameObject
        public GameObject Agent_GameObject;
        #endregion
    #endregion

    public void AI_Spawn(List<AI_Agent> _Agents, float _Timer_Maximum, GameObject _Area_Boundaries, int _Current_Index)
    {
        if(Has_Timer_Been_Setup == false) { Current_Timer = 0f; Has_Timer_Been_Setup = true; Selected_Time = Random.Range(0f, _Timer_Maximum); }

        if(Has_AI_Agent_Been_Setup == false)
        {
            Current_Timer += Time.deltaTime;
            if (Current_Timer == _Timer_Maximum)
            {
                for (int SJ = 0; SJ < _Agents.Count; SJ++)
                {
                    // If null, create a new Agent
                    if (_Agents[_Current_Index] == null) { Agent = new AI_Agent(); _Agents.Add(Agent); }
                    // Else, check to see if one is avaliable for use
                    else
                    {
                        if (_Agents[SJ].Avalability == true)
                        {
                            _Agents[SJ].Set_Avalability(_Agents, SJ, true);
                        }
                    }

                    if (_Agents[SJ] != null)
                    {
                        Renderer Area_Renderer = _Area_Boundaries.GetComponent<Renderer>();

                        float xRange = Area_Renderer.bounds.size.x / 2;
                        float zRange = Area_Renderer.bounds.size.z / 2;

                        // Set Agent's GameObject position
                        _Agents[SJ].Agent_GameObject.transform.position = new Vector3(Random.Range(-xRange, xRange), 1.0f, Random.Range(-zRange, zRange));
                        _Agents[SJ].Agent_GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                        Has_AI_Agent_Been_Setup = true;
                    }
                }
            }
        }
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
        #endregion

        #region GameObject
        GameObject AI_Agent;
        GameObject Player_GameObject;
        #endregion

        #region Float
        private float Current_Timer = 0f;
        #endregion

        #region Int
        private int Current_Score = 0;
    #endregion

        #region List
        private List<AI_Agent> Agents = new List<AI_Agent>();
        #endregion
    #endregion

    #region Public
    #region Bool
    [HideInInspector]
        public bool Has_Demonstration_Message_Been_Received = false;
        #endregion

        #region Float
        public float Float_Timer_Maximum;
        #endregion

        #region GameObject
        public GameObject Area_Boundaries;
        #endregion

        #region List
        public List<Image> Health_Images = new List<Image>();
        #endregion

        #region Text / TextMeshProUGUI
        public TextMeshProUGUI Score_Text;
        public TextMeshProUGUI Timer_Text;
        #endregion
    #endregion

    void Start () {
        A_I = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_I>();

        Current_Timer = Float_Timer_Maximum;

        Player_GameObject = GameObject.FindGameObjectWithTag("Player");

        Score_Text.text = Current_Score.ToString();
        Timer_Text.text = Current_Timer.ToString();
    }

    void Update () {
        if (Has_Demonstration_Message_Been_Received) { IK_Demonstration_Setup(true); }
        
        for(int SJ = 0; SJ < Agents.Count; SJ++)
        {
            Agents[SJ].AI_Spawn(Agents, Float_Timer_Maximum, Area_Boundaries);
        }

        Current_Timer -= Time.deltaTime;
        Debug.Log("Current Timer: " + Current_Timer);
        Timer_Text.text = Current_Timer.ToString("f0");
    }

    private void IK_Demonstration_Setup(bool _Has_IK_Demonstration_Been_Setup)
    {
        if (_Has_IK_Demonstration_Been_Setup)
        {
            A_I.Are_Player_Controls_Enabled = false;
            GameObject.Find("Scene Manager").GetComponent<Camera_Management>().Camera_Enabler("IK Demonstration Camera");

            Player_GameObject.transform.eulerAngles = Vector3.zero;
            Player_GameObject.transform.position = new Vector3(0.09375f, Player_GameObject.transform.position.y, 7.375f);

            _Has_IK_Demonstration_Been_Setup = false;
        }
    }
}
