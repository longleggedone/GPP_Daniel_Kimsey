using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {


	void Awake () {
        Services.Game_Manager = this;
        Services.EventManager = new EventManager();
        Services.TaskManager = new TaskManager();
        Services.Prefabs = Resources.Load<PrefabDB>("Prefabs/PrefabDB");
        Services.Scenes = new SceneManager<TransitionData>(gameObject, Services.Prefabs.Levels);

        Services.Scenes.PushScene<TitleScene>();
	}

    private void Update()
    {
        Services.TaskManager.Update();
    }

    public void StartGame()
    {
        Services.Scenes.PushScene<GameScene>();
    }

}
