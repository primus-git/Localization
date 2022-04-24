using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SimpleLocalization
{
    [ExecuteInEditMode]
    public class Translation : ScriptableObject
    {
        public static Translation instance;

        [System.Serializable]
        public class Convert
        {
            public int Id;
            public string Conversation;

            public Convert(string convt)
            {
                Id = instance.converts.Count + 1;
                Conversation = convt;
            }
        }
        public List<Convert> converts = new List<Convert>();

        private void OnEnable()
        {
            setReference();
        }
        void setReference()
        {
            instance = null;
            instance = this;
        }
        public void Add(string value)
        {
            setReference();
            Convert convert = new Convert(value);
            converts.Add(convert);
        }

    }
}
