using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        #if UNITY_EDITOR
        var gameView = GetMainGameView();
        if (gameView != null)
        {
            gameView.maximized = isFullscreen;
            Debug.Log("Editor Fullscreen (Maximize on Play): " + isFullscreen);
        }
        #else
        Screen.fullScreen = isFullscreen;
        #endif
    }

    #if UNITY_EDITOR
    // Helper untuk mencari jendela Game View di dalam Unity Editor
    private UnityEditor.EditorWindow GetMainGameView()
    {
        System.Type type = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        return UnityEditor.EditorWindow.GetWindow(type);
    }
    #endif
}
