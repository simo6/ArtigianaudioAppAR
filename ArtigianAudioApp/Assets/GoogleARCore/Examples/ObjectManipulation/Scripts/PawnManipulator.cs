//-----------------------------------------------------------------------
// <copyright file="PawnManipulator.cs" company="Google">
//
// Copyright 2019 Google LLC. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.ObjectManipulation
{
    using GoogleARCore;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// Controls the placement of objects via a tap gesture.
    /// </summary>
    public class PawnManipulator : Manipulator
    {
        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR
        /// background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A prefab to place when a raycast from a user touch hits a plane.
        /// </summary>
        public GameObject PawnPrefab;

        /// <summary>
        /// Manipulator prefab to attach placed objects to.
        /// </summary>
        public GameObject ManipulatorPrefab;

        /// <summary>
        /// List of Pawns placed within the scene
        /// </summary>
        private List<GameObject> PawnList = new List<GameObject>();

        /// <summary>
        /// Stacks representing the states of the game
        /// </summary>
        private Stack<GameObject> StackState = new Stack<GameObject>();

        /// <summary>
        /// Undo/Redo buttons
        /// </summary>
        public Button ButtonUndo;
        public Button ButtonRedo;

        /// <summary>
        /// Panel to close after the selection of a Pawn
        /// </summary>
        public GameObject PanelList;

        /// <summary>
        /// Returns true if the manipulation can be started for the given gesture.
        /// </summary>
        /// <param name="gesture">The current gesture.</param>
        /// <returns>True if the manipulation can be started.</returns>
        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {
            if (gesture.TargetObject == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Function called when the manipulation is ended.
        /// </summary>
        /// <param name="gesture">The current gesture.</param>
        protected override void OnEndManipulation(TapGesture gesture)
        {
            if (gesture.WasCancelled)
            {
                return;
            }

            // If gesture is targeting an existing object we are done.
            if (gesture.TargetObject != null)
            {
                return;
            }

            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon;

            if (Frame.Raycast(
                gesture.StartPosition.x, gesture.StartPosition.y, raycastFilter, out hit))
            {
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    if (!IsPointerOverUIObject())
                    {
                        // Instantiate game object at the hit pose.
                        var gameObject = Instantiate(PawnPrefab, hit.Pose.position, hit.Pose.rotation);

                        //Update Material
                        int idx = UpdateMaterial(gameObject);
                        Debug.Log("IDX: " + idx);
                        if (idx != -1)
                        {
                            //Material found
                            Debug.Log("Old Material: " + gameObject.GetComponent<Renderer>().materials[idx].name);
                            Material newMaterial = Resources.Load<Material>("Materials/" + ButtonsManager.Material);
                            Debug.Log("Material Loaded: " + newMaterial.name);
                            gameObject.GetComponent<Renderer>().materials[idx].CopyPropertiesFromMaterial(newMaterial);
                            Debug.Log("New Material: " + gameObject.GetComponent<Renderer>().materials[idx].name);
                        }
                        else
                        {
                            //Material not found
                        }

                        // Instantiate manipulator.
                        var manipulator =
                            Instantiate(ManipulatorPrefab, hit.Pose.position, hit.Pose.rotation);

                        // Make game object a child of the manipulator.
                        gameObject.transform.parent = manipulator.transform;

                        // Create an anchor to allow ARCore to track the hitpoint as understanding of
                        // the physical world evolves.
                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                        // Make manipulator a child of the anchor.
                        manipulator.transform.parent = anchor.transform;

                        // Select the placed object.
                        manipulator.GetComponent<Manipulator>().Select();

                        PawnList.Add(manipulator);

                        ClearStackRedo();

                        UpdateButtonUndo();
                        UpdateButtonRedo();
                    }
   
                }
            }
        }

        private void ClearStackRedo()
        {
            foreach (GameObject o in StackState)
            {
                //We destroy the parent of the Manipulator gameObject -> its Anchor
                Anchor.Destroy(o.transform.parent.gameObject);
            }
            StackState.Clear();
            return;
        }

        // Function to detect if we are pointing to a UI component
        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            return results.Count > 0;
        }

        // Function to hide all the Pawns from the scene
        public void HideShowAllPawns()
        {
            bool active;

            if (FirstPersonCamera.enabled) active = true;
            else active = false;

            foreach (GameObject o in PawnList)
            {
                o.SetActive(active);
            }

        }

        //Select the desired Pawn
        public void SelectPawn()
        {

            string type = ButtonsManager.Base;
            int index = ButtonsManager.Model;
           
            Debug.Log("Base: " + type);
            Debug.Log("Model: " + index);
            if (PawnsManager.GetPrefabFromIndex(type, index) != null)
            {
                PawnPrefab = PawnsManager.GetPrefabFromIndex(type, index);           

                //Maybe show a text message like "Base Normal HXXX Selected"
                //Maybe close the panel
                Debug.Log(PawnPrefab.name + " " + LanguageManager.StringBaseSelected + "!");
                SSTools.ShowMessage(PawnPrefab.name + " " + LanguageManager.StringBaseSelected + "!", SSTools.Position.bottom, SSTools.Time.twoSecond);
                
                
            }
            else
            {
                //Maybe show an error text message
                Debug.Log(LanguageManager.StringBaseNotFound);
                SSTools.ShowMessage(LanguageManager.StringBaseNotFound, SSTools.Position.bottom, SSTools.Time.twoSecond);
            }

            return;
        }

        private int UpdateMaterial(GameObject gameObject)
        {
            Material[] Materials = gameObject.GetComponent<Renderer>().sharedMaterials;
            int i = 0;
            bool found = false;
            while (i < Materials.Length && !found)
            {
                if (ButtonsManager.MaterialsAvailable.Contains(Materials[i].name))
                {
                    found = true;
                }
                else
                {
                    i++;
                }
            }
            if (found) return i;
            else return -1;
        }

        // Function to clear the scene
        public void ClearScene()
        {
            foreach (GameObject o in PawnList)
            {
                Anchor.Destroy(o);
            }
        }

        public void Undo()
        {
            int dim = PawnList.Count;
            if(dim > 0)
            {
                GameObject lastObject = PawnList.ToArray()[dim - 1];
                StackState.Push(lastObject);
                //We hide the parent of the Manipulator gameObject -> its Anchor
                lastObject.transform.parent.gameObject.SetActive(false);
                PawnList.Remove(PawnList.ToArray()[dim - 1]);
                Debug.Log("Disabled base: " + lastObject.name);
            }
            UpdateButtonUndo();
            UpdateButtonRedo();
            return;
        }

        public void Redo()
        {
            if(StackState.Count > 0)
            {
                GameObject lastElement = StackState.Pop();

                //We show the parent of the Manipulator gameObject -> its Anchor
                lastElement.transform.parent.gameObject.SetActive(true);

                PawnList.Add(lastElement);
            }

            UpdateButtonUndo();
            UpdateButtonRedo();
            return;
        }

        public void UpdateButtonUndo()
        {
            
            if (PawnList.Count > 0) ButtonUndo.interactable = true;
            else ButtonUndo.interactable = false;

            return;
        }

        public void UpdateButtonRedo()
        {
            if (StackState.Count > 0) ButtonRedo.interactable = true;
            else ButtonRedo.interactable = false;

            return;
        }

        /// <summary>
        /// <param name="message">Message string to show in the toast.</param>
        /// </summary>
        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }


}
