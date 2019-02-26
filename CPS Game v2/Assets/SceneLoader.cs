using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls loaded scene.
///
/// <Scene Order>
/// 0) TitleScene
/// 1) OptionScene
/// 2) GameScene 
/// 3) VictoryScene
/// </summary>
public class SceneLoader : MonoBehaviour {

	
	public void LoadNextScene()
    {
        int current_scene_index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene_index + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene(1);
    }
	
	public void LoadGameScene()
    {
        SceneManager.LoadScene(2);
    }
	
	public void LoadVictoryScene()
    {
        SceneManager.LoadScene(3);
    }
	
	public void LoadTutorialScene()
    {
        SceneManager.LoadScene(4);
    }
}
