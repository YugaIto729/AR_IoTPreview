//-----------------------------------------------------------------------
// <copyright file="AugmentedImageExampleController.cs" company="Google LLC">
//
// Copyright 2018 Google LLC
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

namespace GoogleARCore.Examples.AugmentedImage
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Controller for AugmentedImage example.
    /// </summary>
    /// <remarks>
    /// In this sample, we assume all images are static or moving slowly with
    /// a large occupation of the screen. If the target is actively moving,
    /// we recommend to check <see cref="AugmentedImage.TrackingMethod"/> and
    /// render only when the tracking method equals to
    /// <see cref="AugmentedImageTrackingMethod"/>.<c>FullTracking</c>.
    /// See details in <a href="https://developers.google.com/ar/develop/c/augmented-images/">
    /// Recognize and Augment Images</a>
    /// </remarks>
    public class AugmentedImageController : MonoBehaviour
    {
        //AugmentedImageVisualizer は prefab にアタッチしているクラス

        /// <summary>
        /// A prefab for visualizing an AugmentedImage.表示するモデル
        /// </summary>
        public AugmentedImageVisualizer_Model AIV_Prefab;

        /// <summary>
        /// The overlay containing the fit to scan user guide.(ARのUI)
        /// </summary>
        public GameObject FitToScanOverlay;

        private Dictionary<int, AugmentedImageVisualizer_Model> _visualizers
            = new Dictionary<int, AugmentedImageVisualizer_Model>();

        private List<AugmentedImage> _tempAugmentedImages = new List<AugmentedImage>();

        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        public void Awake()
        {
            // Enable ARCore to target 60fps camera capture frame rate on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.

            //多分、フレームレート固定
            Application.targetFrameRate = 60;
        }

        public void Start()
        {
            if (AppController.instance.mode == AppController.Mode.Amateur)
            {
                AIV_Prefab = AppController.instance.models_amateur[0].GetComponent<AugmentedImageVisualizer_Model>();
            }
            else if (AppController.instance.mode == AppController.Mode.Expert)
            {
                AIV_Prefab = AppController.instance.models_Expert[0].GetComponent<AugmentedImageVisualizer_Model>();
            }


        }

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Only allow the screen to sleep when not tracking.
            // セッション トラッキングしていない場合はタイムアウトする。トラッキングしている場合はタイムアウトしない。
            //
            if (Session.Status != SessionStatus.Tracking)
            {
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            //多分、用意されている画像を取得する。(あるいは、読み込んだ画像)
            // Get updated augmented images for this frame.
            Session.GetTrackables<AugmentedImage>(
                _tempAugmentedImages, TrackableQueryFilter.Updated);

            // Create visualizers and anchors for updated augmented images that are tracking and do
            // not previously have a visualizer. Remove visualizers for stopped images.
            // 
            foreach (var image in _tempAugmentedImages)
            {
                AugmentedImageVisualizer_Model visualizer = null;

                //取得している画像がキーになっているvisualizerを取得
                _visualizers.TryGetValue(image.DatabaseIndex, out visualizer);

                //トラッキングされている場合(画面に映ってる) ＆ 辞書に登録されていない。
                if (image.TrackingState == TrackingState.Tracking && visualizer == null)
                {
                    // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                    Anchor anchor = image.CreateAnchor(image.CenterPose); //重り…？位置か？


                    //AugmentedImageVisualizerのオブジェクトを生成する。
                    visualizer = (AugmentedImageVisualizer_Model)Instantiate(AIV_Prefab, anchor.transform); 
                    //
                    visualizer.Image = image;

                    //ここでフレームを置いてるのかも

                    _visualizers.Add(image.DatabaseIndex, visualizer); //辞書に追加
                }
                else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
                {
                    _visualizers.Remove(image.DatabaseIndex);
                    GameObject.Destroy(visualizer.gameObject);
                }
            }

            // ひとつでもトラッキングされているならUIを出す。
            // Show the fit-to-scan overlay if there are no images that are Tracking.
            foreach (var visualizer in _visualizers.Values)
            {
                if (visualizer.Image.TrackingState == TrackingState.Tracking)
                {
                    FitToScanOverlay.SetActive(false);
                    return;
                }
            }

            FitToScanOverlay.SetActive(true);
        }
    }
}
