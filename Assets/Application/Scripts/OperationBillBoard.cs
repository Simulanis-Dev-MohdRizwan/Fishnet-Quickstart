using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class OperationBillBoard : MonoBehaviour
    {
        public static Action<GameObject> SetReferences;
        [SerializeField] private bool DesktopBillBoard = false;
        [SerializeField] private bool vrBillBoard = false;
        [SerializeField] private GameObject vrHeadCamera;
        [SerializeField] private bool freezeY = true;
        private void OnEnable()
        {
            SetReferences += setCameraRef;
        }
        void setCameraRef(GameObject cam)
        {
            vrHeadCamera = cam;
        }
        void Update()
        {
            if (DesktopBillBoard) _DesktopBillBoard();
            else if (vrBillBoard) _xrBillBoard();
        }
        public void _DesktopBillBoard()  //for mobile // desktop
        {
            Vector3 rotationCam = Camera.main.transform.forward;
            if (freezeY)
                rotationCam.y = 0f;
            transform.rotation = Quaternion.LookRotation(rotationCam);
        }

        public void _xrBillBoard()  //for vr
        {
            if (vrHeadCamera != null)
            {
                Vector3 rotationCam = vrHeadCamera.transform.forward;
                if (freezeY)
                    rotationCam.y = 0f;
                transform.rotation = Quaternion.LookRotation(rotationCam);

            }
        }

}
