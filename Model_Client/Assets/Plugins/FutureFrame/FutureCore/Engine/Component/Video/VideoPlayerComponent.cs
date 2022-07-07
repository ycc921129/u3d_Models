/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace FutureCore
{
    public class VideoPlayerComponent : MonoBehaviour
    {
        private VideoPlayer videoPlayer;
        private Action onEndFunc;

        public void Init(Camera camera, Action onEndFunc)
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
            videoPlayer.source = VideoSource.Url;
            videoPlayer.playOnAwake = false;
            videoPlayer.waitForFirstFrame = false;
            videoPlayer.isLooping = false;
            videoPlayer.playbackSpeed = 1.6f;
            videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
            videoPlayer.aspectRatio = VideoAspectRatio.FitInside;
            videoPlayer.audioOutputMode = VideoAudioOutputMode.None;

            videoPlayer.targetCamera = camera;
            videoPlayer.targetCameraAlpha = 1;
            this.onEndFunc = onEndFunc;
        }

        public void Play(string videoClipName)
        {
            if (Application.isEditor)
            {
                EndEvent();
            }
            else
            {
                videoPlayer.url = PathUtil.StreamingAssetsPath + "video/" + videoClipName;
                StartCoroutine(OnPlayVideo());
            }
        }

        private IEnumerator OnPlayVideo()
        {
            videoPlayer.errorReceived += OnErrorReceived;
            videoPlayer.Prepare();
            while (!videoPlayer.isPrepared)
            {
                yield return null;
            }

            videoPlayer.Play();
            while (videoPlayer.isPlaying)
            {
                yield return null;
            }

            yield return null;
            EndEvent();
        }

        private void OnErrorReceived(VideoPlayer source, string message)
        {
            LogUtil.LogFormat("[VideoPlayerComponent]Error:", message);
            EndEvent();
        }

        private void EndEvent()
        {
            videoPlayer.Stop();
            onEndFunc();
            Destroy(gameObject);
        }
    }
}