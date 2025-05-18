using UnityEngine;
using UnityEngine.Events;

    public class GrabObjTriggerEnter : MonoBehaviour
    {
        
        public string objName;
        [HideInInspector]
        public bool objEntered;
        public UnityEvent ObjEnteredEvt;
        public UnityEvent ObjExitedEvt;
        private bool eventCall;
        void Start()
        { objEntered = false; eventCall = false; }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == objName)
                objEntered = true;
            if (objEntered && !eventCall)
            { ObjEnteredEvt?.Invoke(); eventCall = true; }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == objName)
                objEntered = false;
            if (!objEntered && eventCall)
            { ObjExitedEvt?.Invoke(); eventCall = false; }
        }
        public void ResetVariables()
        { objEntered = false; eventCall = false; }
    }

