using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityEditor
{
    public class NestAnimationClipsInController : EditorWindow
    {

        [NonSerialized]
        UnityEditor.Animations.AnimatorController target;
        Dictionary<Object, AssetInfo> objects;

        class AssetInfo
        {
            public Object asset;
            public List<Object> states = new List<Object>();
            public bool selected;
        }


        [MenuItem("Assets/Nest AnimClips in Controller")]
        public static void nestAnimClips()
        {
            GetWindow<NestAnimationClipsInController>().Show();
        }


        private void OnEnable()
        {
            if (objects == null)
                objects = new Dictionary<Object, AssetInfo>();

            Selection.selectionChanged -= OnSelectedChanged;
            Selection.selectionChanged += OnSelectedChanged;
        }



        void OnSelectedChanged()
        {
            objects.Clear();
            target = null;
            UnityEditor.Animations.AnimatorController selected = Selection.activeObject as UnityEditor.Animations.AnimatorController;

            target = selected;
            if (target == null)
            {
                foreach (var obj in Selection.objects)
                {
                    AssetInfo s;
                    if (!objects.TryGetValue(obj, out s))
                    {
                        s = new AssetInfo();
                        s.asset = obj;
                        if (s.states == null)
                            s.states = new List<Object>();
                        objects[obj] = s;
                    }
                }

                return;
            }

            AssetDatabase.SaveAssets();


            AnimatorControllerLayer[] layers = target.layers;

            foreach (AnimatorControllerLayer layer in layers)
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion)
                    {
                        AssetInfo s;
                        if (!objects.TryGetValue(state.state.motion, out s))
                        {
                            s = new AssetInfo();
                            s.asset = state.state.motion;
                            if (s.states == null)
                                s.states = new List<Object>();
                            objects[state.state.motion] = s;
                        }
                        s.states.Add(state.state);
                    }
                }
            }

        }

        private void Update()
        {

        }


        private void AttachToAsset()
        {

            if (target != null)
            {
                if (objects.Count > 0)
                {
                    foreach (var item in objects.Where(o => o.Value.selected))
                    {
                        var motion = item.Key;
                        var newMotion = Object.Instantiate(motion);
                        newMotion.name = motion.name;
                        AssetDatabase.AddObjectToAsset(newMotion, target);
                        foreach (var state in item.Value.states.Select(o => (AnimatorState)o))
                        {
                            state.motion = (Motion)newMotion;
                        }
                    }
                }
            }

            AssetDatabase.SaveAssets();
            OnSelectedChanged();
        }


        public void DeleteFromAsset()
        {
            HashSet<string> assetPaths = new HashSet<string>();

            var objs = objects.Where(o => o.Value.selected).Select(o => o.Key).ToArray();

            for (int i = 0; i < objs.Length; i++)
            {
                var obj = objs[i];
                if (!obj)
                    continue;
                string assetPath = AssetDatabase.GetAssetPath(obj);

                AssetDatabase.RemoveObjectFromAsset(obj);
                if (!string.IsNullOrEmpty(assetPath))
                {
                    assetPaths.Add(assetPath);

                }
            }
            AssetDatabase.SaveAssets();
            foreach (var assetPath in assetPaths)
            {
                AssetDatabase.ImportAsset(assetPath);
            }
            OnSelectedChanged();
        }

        public void DetachFromAsset()
        {
            HashSet<string> assetPaths = new HashSet<string>();

            var objs = objects.Where(o => o.Value.selected).Select(o => o.Key).ToArray();
            int n = 1;
            for (int i = 0; i < objs.Length; i++)
            {
                var obj = objs[i];
                if (!obj)
                    continue;
                string assetPath = AssetDatabase.GetAssetPath(obj);

                AssetDatabase.RemoveObjectFromAsset(obj);
                if (!string.IsNullOrEmpty(assetPath))
                {
                    assetPaths.Add(assetPath);
                    string newPath = Path.GetDirectoryName(assetPath) + "/Detach " + (n++) + " " + obj.name + ".anim";
                    AssetDatabase.CreateAsset(obj, newPath);
                }
            }
            AssetDatabase.SaveAssets();
            foreach (var assetPath in assetPaths)
            {
                AssetDatabase.ImportAsset(assetPath);
            }
            OnSelectedChanged();
        }
        Vector2 scrollPos;

        private void OnGUI()
        {
            using (var sv = new GUILayout.ScrollViewScope(scrollPos))
            {
                scrollPos = sv.scrollPosition;
                using (new GUILayout.HorizontalScope())
                {

                    using (new EditorGUI.DisabledGroupScope(target == null))
                    {
                        if (GUILayout.Button("Attach"))
                        {
                            AttachToAsset();
                        }
                    }

                    if (GUILayout.Button("Delete"))
                    {
                        if (EditorUtility.DisplayDialog("Delete", "delete", "yes", "cancel"))
                        {
                            DeleteFromAsset();
                        }
                    }

                    //if (GUILayout.Button("Detach"))
                    //{
                    //    DetachFromAsset();
                    //}
                }


                EditorGUILayout.ObjectField("Target", target, typeof(Object), false);

                foreach (var item in objects.Values)
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        item.selected = EditorGUILayout.Toggle(item.selected, GUILayout.ExpandWidth(false), GUILayout.Width(30));
                        EditorGUILayout.ObjectField("Motion", item.asset, typeof(Motion), false);
                        GUILayout.Label(AssetDatabase.GetAssetPath(item.asset));
                    }
                }

                Motion[] motions = Selection.GetFiltered<Motion>(SelectionMode.TopLevel);
                using (new EditorGUI.DisabledGroupScope(motions.Length == 0))
                {
                    if (GUILayout.Button("Example: Create AnimatorController From Selected Motion"))
                    {
                        CreateAnimatorControllerExample(motions);
                    }
                }
            }

        }

        /// <summary>
        /// 创建状态机例子
        /// </summary>
        public void CreateAnimatorControllerExample(Motion[] motions)
        {

            AnimatorController controller;
            AnimatorStateMachine stateMachine;
            AnimatorControllerLayer layer;
              

            string assetPath = AssetDatabase.GetAssetPath(motions[0]);
            assetPath = Path.Combine(Path.GetDirectoryName(assetPath), "Test.controller");

            controller = AnimatorController.CreateAnimatorControllerAtPath(assetPath);

            layer = controller.layers[0];

            stateMachine = layer.stateMachine;
            //创建子状态
            foreach (var motion in motions)
            {
                //new AnimatorState() 不能序列化
                //layer.stateMachine.AddState(new AnimatorState() { name = motion.name, motion = motion }, Vector3.zero);
                var state = stateMachine.AddState(motion.name, Vector3.zero);
                state.motion = motion;

            }

            //创建混合树            
            foreach (var motion in Selection.GetFiltered<Motion>(SelectionMode.TopLevel))
            {
                BlendTree tree;
                var state = controller.CreateBlendTreeInController("BlendTree_" + motion.name, out tree);
                tree.blendType = BlendTreeType.Simple1D;
                tree.AddChild(motion);
            }

            //创建子状态机
            stateMachine = layer.stateMachine.AddStateMachine("SubStateMachine");
            foreach (var motion in Selection.GetFiltered<Motion>(SelectionMode.TopLevel))
            {
                var state = stateMachine.AddState(stateMachine.name + "_" + motion.name, Vector3.zero);
                state.motion = motion;
            }

            EditorUtility.SetDirty(controller);
            AssetDatabase.SaveAssets();
        }

    }

}