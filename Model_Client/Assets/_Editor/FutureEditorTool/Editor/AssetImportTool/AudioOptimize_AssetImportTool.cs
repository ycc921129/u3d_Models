/*
    [音频加载方式]
    Decompress On Load
    音频文件以压缩形式存储在磁盘上，加载时直接解压放到内存中。这种方式在内存占用上没有优势，但在后续播放时对CPU的计算需求是最小的。一般来讲这种方式更适用于短小的音频。
    
    Compressed In Memory
    音频文件以压缩形式存放在内存里，使用时再解压。这意味着会有更多的CPU开销，但是在加载速度和内存占用上具有优势。这种情况适用于大型音频文件。

    Streaming
    音频文件存放在磁盘中，加载时循环以下操作：“从磁盘读取一部分，解压到内存中，播放，卸载”。这种方式在内存占用上相较而言是最小的，但在CPU的消耗上是最不具优势的。
    而在音频的实际运用中，背景音乐的使用是普遍存在的。几乎所有游戏都会使用BGM以增强对游戏氛围和环境的渲染。结合以上关于音频加载方式的描述可以看出，采用Streaming方式去加载背景音乐，可以有效减少内存占用和加载时间，从而降低Audio资源对项目总体内存和加载时间的影响。
*/

/*
    [音频压缩编码]
    PCM
    全称是Pulse-Code Modulation。属于脉冲调制编码，它将模拟信号转换为数字信号，实质上没有经过编码，没有进行压缩，所以在音质上是属于完全无损的原始音频。而且相较于原生的模拟信号，它的抗干扰能力更强，保真效果更好。
    未使用 PCM 格式的音频可能存在音质问题，所以我们将这些音频筛选出来，以供开发团队进行进一步的检查，去考虑音频格式和原音频的质量是否符合预期的使用需求。

    Vorbis
    应该叫做OGG Vorbis。类似mp3格式，但这是一种免费开发的非商业压缩格式。属于有损压缩。
     
    ADPCM
    Adaptive Differential Pulse Code Modulation，自适应差分脉冲编码调制。是一种基于PCM的优化压缩方式，但也属于有损压缩。

    单声道
    勾选Force To Mono的情况下，Unity会将音频合并成单声道，从而节约内存。
*/

using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    /// <summary>
    /// mp3:失真小，适合音质要求高的文件，例如BGM
    /// wav:资源大，不推荐
    /// ogg:压缩比高，适合人声、音效等
    /// 
    /// 通过设置改善其延迟的问题
    ///  1.Edit → Project Settings → Audio → 设置DSP Buffer size为Best latency（设置dsp缓冲区大小以优化延迟或性能，设置一个不合适的值会导致安卓设备的电流音）
    ///  2.音频文件的Load Type为Decompress On Load（让音频提前读取到缓存中）
    ///  3.让音频文件越小越好
    /// </summary>
    public class AudioOptimize_AssetImportTool : AssetPostprocessor
    {
        public void OnPreprocessAudio()
        {
            if (assetPath.IndexOf("_Res/Resources/Audio") != -1)
            {
                AudioImporterSampleSettings audioSetting = new AudioImporterSampleSettings();
                //加载方式选择
                audioSetting.loadType = AudioClipLoadType.DecompressOnLoad;
                //压缩方式选择
                audioSetting.compressionFormat = AudioCompressionFormat.PCM;
                //设置播放质量
                audioSetting.quality = 0.85f;
                //优化采样率
                audioSetting.sampleRateSetting = AudioSampleRateSetting.OptimizeSampleRate;

                AudioImporter audioImporter = assetImporter as AudioImporter;
                //开启单声道 
                audioImporter.forceToMono = true;
                audioImporter.loadInBackground = false;
                audioImporter.ambisonic = false;

                audioImporter.preloadAudioData = true;
                audioImporter.defaultSampleSettings = audioSetting;

                // iOS
                //bool successfullOverride = audioImporter.SetOverrideSampleSettings("IOS", audioImporterSampleSettingsIOS);
            }
        }
    }
}