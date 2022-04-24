using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using System.Linq;
using System;

public class Randomize : MonoBehaviour
{
    #region some other
    [Header("Varivale")]
    public List<int> ge = new List<int>();
    [System.Serializable]
    public class rand
    {
        public int _head;
        public int _leg;
        public int _hand;
        public int _chest;
    }


    public List<rand> randlist = new List<rand>();
    [ContextMenu("Random")]
    void _Start()
    {
        foreach (int a in ge)
        {

            for (int i = 0; i < a; i++)
            {

            }
        }
    }
    int getVal(int val)
    {

        for (int i = 0; i < val; i++)
        {
            return getVal(i);
        }
        return 0;
    }

    #endregion

    [Header("follow")]
    public GameObject first;
    public GameObject second;
    public float speed;
    public bool foloow = false;
    void Update()
    {
        if (foloow)
        {
            float step = speed * Time.deltaTime;
            first.transform.position = Vector3.MoveTowards(first.transform.position, second.transform.position, step);
        }
    }
}
