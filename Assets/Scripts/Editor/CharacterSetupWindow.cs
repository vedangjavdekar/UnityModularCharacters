using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterSetupWindow : EditorWindow
{
    private static bool characterStatus;
    private static string missingTransform;
    public static Transform hips;
    public static Transform leftUpLeg;
    public static Transform leftMidLeg;
    public static Transform leftFoot;
    public static Transform rightUpLeg;
    public static Transform rightMidLeg;
    public static Transform rightFoot;
    public static Transform abdomen;
    public static Transform chest;
    public static Transform head;
    public static Transform leftShoulder;
    public static Transform leftElbow;
    public static Transform leftWrist;
    public static Transform leftHand;
    public static Transform rightShoulder;
    public static Transform rightElbow;
    public static Transform rightWrist;
    public static Transform rightHand;



    private static bool lockEditor;
    private static GameObject currentSelection;
    private static bool alreadyModularCharacter;

    [MenuItem("GameObject/Create Modular Character", false, -1)]
    public static void ShowWindow()
    {
        GetWindow<CharacterSetupWindow>();
    }

    private void OnFocus()
    {
        OnSelectionChange();
    }

    private void OnGUI()
    {
        #region EDITOR LOCK
        lockEditor = EditorGUILayout.Toggle("Lock on current object", lockEditor);
        if (!lockEditor)
        {
            currentSelection = null;
        }
        #endregion

        #region CHECK_SLOTS
        characterStatus = true;
        hips = EditorGUILayout.ObjectField("Hips", hips, typeof(Transform), true) as Transform;
        if (!hips && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Hips";
        }

        leftUpLeg = EditorGUILayout.ObjectField("Left Up Leg", leftUpLeg, typeof(Transform), true) as Transform;
        if (!leftUpLeg && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Left Up Leg";
        }

        leftMidLeg = EditorGUILayout.ObjectField("Left Mid Leg", leftMidLeg, typeof(Transform), true) as Transform;
        if (!leftMidLeg && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Left Mid Leg";
        }

        leftFoot = EditorGUILayout.ObjectField("Left Foot", leftFoot, typeof(Transform), true) as Transform;
        if (!leftFoot && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Left Foot";
        }

        rightUpLeg = EditorGUILayout.ObjectField("Right Up Leg", rightUpLeg, typeof(Transform), true) as Transform;
        if (!rightUpLeg && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Right Up Leg";
        }

        rightMidLeg = EditorGUILayout.ObjectField("Right Mid Leg", rightMidLeg, typeof(Transform), true) as Transform;
        if (!rightMidLeg && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Right Mid Leg";
        }

        rightFoot = EditorGUILayout.ObjectField("Right Foot", rightFoot, typeof(Transform), true) as Transform;
        if (!rightFoot && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Right Foot";
        }

        abdomen = EditorGUILayout.ObjectField("Abdomen", abdomen, typeof(Transform), true) as Transform;
        if (!abdomen && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Abdomen";
        }

        chest = EditorGUILayout.ObjectField("Chest", chest, typeof(Transform), true) as Transform;
        if (!chest && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Chest";
        }

        head = EditorGUILayout.ObjectField("Head", head, typeof(Transform), true) as Transform;
        if (!head && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Head";
        }

        leftShoulder = EditorGUILayout.ObjectField("Left Shoulder", leftShoulder, typeof(Transform), true) as Transform;
        if (!leftShoulder && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Left Shoulder";
        }

        leftElbow = EditorGUILayout.ObjectField("Left Elbow", leftElbow, typeof(Transform), true) as Transform;
        if (!leftElbow && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Left Elbow";
        }

        leftWrist = EditorGUILayout.ObjectField("Left Wrist", leftWrist, typeof(Transform), true) as Transform;
        if (!leftWrist && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Left Wrist";
        }

        leftHand = EditorGUILayout.ObjectField("Left Hand", leftHand, typeof(Transform), true) as Transform;
        if (!leftHand && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Left Hand";
        }

        rightShoulder = EditorGUILayout.ObjectField("Right Shoulder", rightShoulder, typeof(Transform), true) as Transform;
        if (!rightShoulder && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Right Shoulder";
        }

        rightElbow = EditorGUILayout.ObjectField("Right Elbow", rightElbow, typeof(Transform), true) as Transform;
        if (!rightElbow && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Right Elbow";
        }

        rightWrist = EditorGUILayout.ObjectField("Right Wrist", rightWrist, typeof(Transform), true) as Transform;
        if (!rightWrist && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Right Wrist";
        }

        rightHand = EditorGUILayout.ObjectField("Right Hand", rightHand, typeof(Transform), true) as Transform;
        if (!rightHand && characterStatus)
        {
            characterStatus = false;
            missingTransform = "Right Hand";
        }

        if (!characterStatus)
        {
            EditorGUILayout.HelpBox($"'{missingTransform}' is missing. Please assign the missing transform.", MessageType.Error);
        }
        #endregion

        EditorGUILayout.Space(10);

        EditorGUI.BeginDisabledGroup(!characterStatus);
        GameObject selection;
        if (lockEditor)
        {
            if (!currentSelection && Selection.activeGameObject != null)
            {
                currentSelection = Selection.activeGameObject;
                selection = currentSelection;
            }
            else if (currentSelection)
            {
                selection = currentSelection;
            }
            else
            {
                selection = null;
            }
        }
        else
        {
            selection = Selection.activeGameObject;
        }
        ModularCharacterBase modularBase;
        if (GUILayout.Button($"{(alreadyModularCharacter ? "Update" : "Create")} Character"))
        {
            if (selection == null)
            {
                Transform parent = hips.parent;
                while (parent.parent != null)
                {
                    parent = parent.parent;
                }
                selection = parent.gameObject;
            }

            modularBase = selection.GetComponent<ModularCharacterBase>();
            if (!modularBase)
            {
                modularBase = selection.AddComponent<ModularCharacterBase>();
            }
            else
            {
                modularBase.characterBaseSlots.Clear();
            }

            /*
            // If we choose to use dictionary, uncomment this and comment the next part
            modularBase.characterBaseSlots.Add("Hips", new ModularCharacterSlot(hips.parent, hips.gameObject));

            modularBase.characterBaseSlots.Add("Left Up Leg", new ModularCharacterSlot(leftUpLeg.parent, leftUpLeg.gameObject));
            modularBase.characterBaseSlots.Add("Left Mid Leg", new ModularCharacterSlot(leftMidLeg.parent, leftMidLeg.gameObject));
            modularBase.characterBaseSlots.Add("Left Foot", new ModularCharacterSlot(leftFoot.parent, leftFoot.gameObject));

            modularBase.characterBaseSlots.Add("Right Up Leg", new ModularCharacterSlot(rightUpLeg.parent, rightUpLeg.gameObject));
            modularBase.characterBaseSlots.Add("Right Mid Leg", new ModularCharacterSlot(rightMidLeg.parent, rightMidLeg.gameObject));
            modularBase.characterBaseSlots.Add("Right Foot", new ModularCharacterSlot(rightFoot.parent, rightFoot.gameObject));

            modularBase.characterBaseSlots.Add("Abdomen", new ModularCharacterSlot(abdomen.parent, abdomen.gameObject));
            modularBase.characterBaseSlots.Add("Chest", new ModularCharacterSlot(chest.parent, chest.gameObject));
            modularBase.characterBaseSlots.Add("Head", new ModularCharacterSlot(head.parent, head.gameObject));

            modularBase.characterBaseSlots.Add("Left Shoulder", new ModularCharacterSlot(leftShoulder.parent, leftShoulder.gameObject));
            modularBase.characterBaseSlots.Add("Left Elbow", new ModularCharacterSlot(leftElbow.parent, leftElbow.gameObject));
            modularBase.characterBaseSlots.Add("Left Wrist", new ModularCharacterSlot(leftWrist.parent, leftWrist.gameObject));
            modularBase.characterBaseSlots.Add("Left Hand", new ModularCharacterSlot(leftHand.parent, leftHand.gameObject));

            modularBase.characterBaseSlots.Add("Right Shoulder", new ModularCharacterSlot(rightShoulder.parent, rightShoulder.gameObject));
            modularBase.characterBaseSlots.Add("Right Elbow", new ModularCharacterSlot(rightElbow.parent, rightElbow.gameObject));
            modularBase.characterBaseSlots.Add("Right Wrist", new ModularCharacterSlot(rightWrist.parent, rightWrist.gameObject));
            modularBase.characterBaseSlots.Add("Right Hand", new ModularCharacterSlot(rightHand.parent, rightHand.gameObject));
            */

            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Hips", hips.parent, hips.gameObject));

            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Left Up Leg", leftUpLeg.parent, leftUpLeg.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Left Mid Leg", leftMidLeg.parent, leftMidLeg.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Left Foot", leftFoot.parent, leftFoot.gameObject));

            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Right Up Leg", rightUpLeg.parent, rightUpLeg.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Right Mid Leg", rightMidLeg.parent, rightMidLeg.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Right Foot", rightFoot.parent, rightFoot.gameObject));

            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Abdomen", abdomen.parent, abdomen.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Chest", chest.parent, chest.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Head", head.parent, head.gameObject));

            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Left Shoulder", leftShoulder.parent, leftShoulder.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Left Elbow", leftElbow.parent, leftElbow.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Left Wrist", leftWrist.parent, leftWrist.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Left Hand", leftHand.parent, leftHand.gameObject));

            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Right Shoulder", rightShoulder.parent, rightShoulder.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Right Elbow", rightElbow.parent, rightElbow.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Right Wrist", rightWrist.parent, rightWrist.gameObject));
            modularBase.characterBaseSlots.Add(new ModularCharacterSlot("Right Hand", rightHand.parent, rightHand.gameObject));

            alreadyModularCharacter = true;
            Repaint();
        }
        EditorGUI.EndDisabledGroup();

        if (alreadyModularCharacter && selection != null)
        {
            modularBase = selection.GetComponent<ModularCharacterBase>();
            if (modularBase)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Additional Slots", EditorStyles.boldLabel);
                if (GUILayout.Button("+"))
                {
                    modularBase.additionalSlots.Add(new ModularCharacterSlot("New Slot", null, null));
                }
                EditorGUILayout.EndHorizontal();

                int removeIndex = -1;
                for (int i = 0; i < modularBase.additionalSlots.Count; i++)
                {
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.BeginHorizontal();
                    modularBase.additionalSlots[i].name = EditorGUILayout.TextField("Name", modularBase.additionalSlots[i].name);
                    if (GUILayout.Button("Remove") && removeIndex == -1)
                    {
                        removeIndex = i;
                    }
                    EditorGUILayout.EndHorizontal();

                    modularBase.additionalSlots[i].parent = EditorGUILayout.ObjectField("Parent", modularBase.additionalSlots[i].parent, typeof(Transform), true) as Transform;
                    modularBase.additionalSlots[i].item = EditorGUILayout.ObjectField("Item", modularBase.additionalSlots[i].item, typeof(GameObject), true) as GameObject;
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (modularBase.additionalSlots[i].parent != null
                            && modularBase.additionalSlots[i].item != null)
                        {
                            modularBase.additionalSlots[i].item.transform.position = modularBase.additionalSlots[i].parent.position;
                            modularBase.additionalSlots[i].item.transform.parent = modularBase.additionalSlots[i].parent;
                        }
                    }
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Align Transform"))
                    {
                        if (modularBase.additionalSlots[i].parent != null
                           && modularBase.additionalSlots[i].item != null)
                        {
                            modularBase.additionalSlots[i].item.transform.position = modularBase.additionalSlots[i].parent.position;
                            modularBase.additionalSlots[i].item.transform.rotation = modularBase.additionalSlots[i].parent.rotation;
                        }
                    }
                    if (GUILayout.Button("Align Position"))
                    {
                        if (modularBase.additionalSlots[i].parent != null
                           && modularBase.additionalSlots[i].item != null)
                        {
                            modularBase.additionalSlots[i].item.transform.position = modularBase.additionalSlots[i].parent.position;
                        }
                    }
                    if (GUILayout.Button("Align Rotation"))
                    {
                        if (modularBase.additionalSlots[i].parent != null
                           && modularBase.additionalSlots[i].item != null)
                        {
                            modularBase.additionalSlots[i].item.transform.rotation = modularBase.additionalSlots[i].parent.rotation;
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space(10);
                }

                if (removeIndex != -1)
                {
                    if (modularBase.additionalSlots[removeIndex].item)
                    {
                        modularBase.additionalSlots[removeIndex].item.transform.parent = null;
                    }
                    modularBase.additionalSlots.RemoveAt(removeIndex);
                }
            }
        }
    }

    private void OnSelectionChange()
    {
        if (lockEditor)
            return;
        if (Selection.activeGameObject != null)
        {
            ModularCharacterBase modularBase = Selection.activeGameObject.GetComponent<ModularCharacterBase>();
            if (modularBase != null)
            {
                if (modularBase.characterBaseSlots.Count > 0)
                {
                    /* 
                    // If we choose to use dictionary, uncomment this and comment the next part
                    hips = modularBase.characterBaseSlots["Hips"].item.transform;
                    leftUpLeg = modularBase.characterBaseSlots["Left Up Leg"].item.transform;
                    leftMidLeg = modularBase.characterBaseSlots["Left Mid Leg"].item.transform;
                    leftFoot = modularBase.characterBaseSlots["Left Foot"].item.transform;
                    rightUpLeg = modularBase.characterBaseSlots["Right Up Leg"].item.transform;
                    rightMidLeg = modularBase.characterBaseSlots["Right Mid Leg"].item.transform;
                    rightFoot = modularBase.characterBaseSlots["Right Foot"].item.transform;
                    abdomen = modularBase.characterBaseSlots["Abdomen"].item.transform;
                    chest = modularBase.characterBaseSlots["Chest"].item.transform;
                    head = modularBase.characterBaseSlots["Head"].item.transform;
                    leftShoulder = modularBase.characterBaseSlots["Left Shoulder"].item.transform;
                    leftElbow = modularBase.characterBaseSlots["Left Elbow"].item.transform;
                    leftWrist = modularBase.characterBaseSlots["Left Wrist"].item.transform;
                    leftHand = modularBase.characterBaseSlots["Left Hand"].item.transform;
                    rightShoulder = modularBase.characterBaseSlots["Right Shoulder"].item.transform;
                    rightElbow = modularBase.characterBaseSlots["Right Elbow"].item.transform;
                    rightWrist = modularBase.characterBaseSlots["Right Wrist"].item.transform;
                    rightHand = modularBase.characterBaseSlots["Right Hand"].item.transform;
                    */

                    hips = modularBase.characterBaseSlots[0].item.transform;
                    leftUpLeg = modularBase.characterBaseSlots[1].item.transform;
                    leftMidLeg = modularBase.characterBaseSlots[2].item.transform;
                    leftFoot = modularBase.characterBaseSlots[3].item.transform;
                    rightUpLeg = modularBase.characterBaseSlots[4].item.transform;
                    rightMidLeg = modularBase.characterBaseSlots[5].item.transform;
                    rightFoot = modularBase.characterBaseSlots[6].item.transform;
                    abdomen = modularBase.characterBaseSlots[7].item.transform;
                    chest = modularBase.characterBaseSlots[8].item.transform;
                    head = modularBase.characterBaseSlots[9].item.transform;
                    leftShoulder = modularBase.characterBaseSlots[10].item.transform;
                    leftElbow = modularBase.characterBaseSlots[11].item.transform;
                    leftWrist = modularBase.characterBaseSlots[12].item.transform;
                    leftHand = modularBase.characterBaseSlots[13].item.transform;
                    rightShoulder = modularBase.characterBaseSlots[14].item.transform;
                    rightElbow = modularBase.characterBaseSlots[15].item.transform;
                    rightWrist = modularBase.characterBaseSlots[16].item.transform;
                    rightHand = modularBase.characterBaseSlots[17].item.transform;

                    alreadyModularCharacter = true;
                    Repaint();
                    return;
                }
            }
            else
            {
                hips = null;
                leftUpLeg = null;
                leftMidLeg = null;
                leftFoot = null;
                rightUpLeg = null;
                rightMidLeg = null;
                rightFoot = null;
                abdomen = null;
                chest = null;
                head = null;
                leftShoulder = null;
                leftElbow = null;
                leftWrist = null;
                leftHand = null;
                rightShoulder = null;
                rightElbow = null;
                rightWrist = null;
                rightHand = null;
            }
        }
        else
        {
            hips = null;
            leftUpLeg = null;
            leftMidLeg = null;
            leftFoot = null;
            rightUpLeg = null;
            rightMidLeg = null;
            rightFoot = null;
            abdomen = null;
            chest = null;
            head = null;
            leftShoulder = null;
            leftElbow = null;
            leftWrist = null;
            leftHand = null;
            rightShoulder = null;
            rightElbow = null;
            rightWrist = null;
            rightHand = null;
        }
        alreadyModularCharacter = false;
        Repaint();
    }
}
