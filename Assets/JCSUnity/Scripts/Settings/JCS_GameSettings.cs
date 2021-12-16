﻿/**
 * $File: JCS_GameSettings.cs $
 * $Date: $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *                   Copyright (c) 2016 by Shen, Jen-Chieh $
 */
using UnityEngine;

namespace JCSUnity
{
    /// <summary>
    /// Hold the general game setting.
    /// </summary>
    public class JCS_GameSettings : JCS_Settings<JCS_GameSettings>
    {
        /* Variables */

        public EmptyFunction SAVE_GAME_DATA_FUNC = null;
        public EmptyFunction LOAD_GAME_DATA_FUNC = null;  // NOT USED

        public static JCS_XMLGameData GAME_DATA = null;  // NOT USED

        [Header("** Check Variables (JCS_GameSettings) **")]

        [SerializeField]
        private string REAL_DATA_PATH = "";

        [SerializeField]
        private string REAL_SCREENSHOT_PATH = "";

        [SerializeField]
        private string REAL_WEBCAM_PATH = "";

        [SerializeField]
        private string REAL_STREAMING_CACHE_PATH = "";

        [Header("** Game Settings (JCS_GameSettings) **")]

        [Tooltip("Debug mode flag.")]
        public bool DEBUG_MODE = true;

        [Tooltip("Game scene flag.<")]
        public bool THIS_IS_GAME_SCENE = false;

        [Tooltip("Level design mode flag.")]
        public bool LEVEL_DESIGN_MODE = true;

        [Tooltip("Game type flag.")]
        public JCS_GameType GAME_TYPE = JCS_GameType.GAME_2D;

        [Tooltip("Gravity production. (For game that have gravity in it)")]
        public float GRAVITY_PRODUCT = 4.5f;

        [Header("- Camera")]

        [Tooltip("Type of the camera.")]
        public JCS_CameraType CAMERA_TYPE = JCS_CameraType.NONE;

        [Header("- Player")]

        [Tooltip("Game only allows control one player.")]
        public bool ACTIVE_ONE_PLAYER = true;

        [Header("- Collision")]

        [Tooltip("Do collusion happen with eacth other. (Player)")]
        public bool PLAYER_IGNORE_EACH_OTHER = true;

        [Tooltip("Can tribe damage each other?")]
        public bool TRIBE_DAMAGE_EACH_OTHER = false;

        [Tooltip("Careful, this will override player ignore options!")]
        public bool IGNORE_EACH_OTHER_CHARACTER_CONTROLLER = true;

        [Header("- Resources")]

        [Tooltip("Base URL for streaming assets, please point to a directory.")]
        public string STREAMING_BASE_URL = "https://wwww.example.com/";

        [Tooltip("Cache streaming assets' data path.")]
        public string STREAMING_CACHE_PATH = "/JCS_GameData/Cache_StreamingAssets/";

        [Header("- Save Load")]

        [Tooltip("Data folder path.")]
        public string DATA_PATH = "/JCS_GameData/";

        [Tooltip("Data file extension.")]
        public string DATA_EXTENSION = ".jcs";

        [Tooltip("Save when switching the scene.")]
        public bool SAVE_ON_SWITCH_SCENE = true;

        [Tooltip("Save when app exit.")]
        public bool SAVE_ON_EXIT_APP = true;

        [Header("- Screenshot")]

        [Tooltip("Screenshot folder path.")]
        public string SCREENSHOT_PATH = "/JCS_GameData/Screenshot/";

        [Tooltip("Screenshot file name.")]
        public string SCREENSHOT_FILENAME = "Screenshot_";

        [Tooltip("Screenshot image extension.")]
        public string SCREENSHOT_EXTENSION = ".png";

        [Header("- Webcam")]

        [Tooltip("Webcam image save path.")]
        public string WEBCAM_PATH = "/JCS_GameData/WebcamShot/";

        [Tooltip("Webcam file name.")]
        public string WEBCAM_FILENAME = "";

        [Tooltip("Webcam image extension.")]
        public string WEBCAM_EXTENSION = ".png";

        [Header("- Damage")]

        [Tooltip("Mininum damage can be in the game.")]
        public int MIN_DAMAGE = 1;

        [Tooltip("Maxinum damage can be in the game.")]
        public int MAX_DAMAGE = 999999;

        /* Setter & Getter */

        /* Functions */

        private void Awake()
        {
            instance = CheckSingleton(instance, this);

            REAL_DATA_PATH = JCS_GameData.SavePath();
            REAL_SCREENSHOT_PATH = JCS_Camera.SavePath();
            REAL_WEBCAM_PATH = JCS_Webcam.SavePath();
            REAL_STREAMING_CACHE_PATH = JCS_StreamingAssets.CachePath();

            JCS_IO.CreateDirectory(REAL_DATA_PATH);
            JCS_IO.CreateDirectory(REAL_SCREENSHOT_PATH);
            JCS_IO.CreateDirectory(REAL_WEBCAM_PATH);
            JCS_IO.CreateDirectory(REAL_STREAMING_CACHE_PATH);
        }

        private void Start()
        {
            JCS_GameWindowHandler gwh = JCS_GameWindowHandler.instance;

            // if this is the game scene, enable the game ui.
            if (THIS_IS_GAME_SCENE)
            {
                if (gwh != null)
                    gwh.ShowGameUI();
            }
            // if this is NOT the game scene, disable the game ui.
            else
            {
                if (gwh != null)
                    gwh.HideGameUI();
            }
        }

        private void Update()
        {
            JCS_Input.MouseDeltaPosition();
        }

        /// <summary>
        /// Make limit so not all the data override the by the new data!
        /// </summary>
        /// <param name="_old"> old data we copy from </param>
        /// <param name="_new"> new data we copy to </param>
        protected override void TransferData(JCS_GameSettings _old, JCS_GameSettings _new)
        {
            // Debug check
            _new.DEBUG_MODE = _old.DEBUG_MODE;

            _new.STREAMING_BASE_URL = _old.STREAMING_BASE_URL;
            _new.STREAMING_CACHE_PATH = _old.STREAMING_CACHE_PATH;

            _new.DATA_PATH = _old.DATA_PATH;
            _new.DATA_EXTENSION = _old.DATA_EXTENSION;
            _new.SAVE_ON_EXIT_APP = _old.SAVE_ON_EXIT_APP;
            _new.SAVE_ON_SWITCH_SCENE = _old.SAVE_ON_SWITCH_SCENE;

            _new.SCREENSHOT_PATH = _old.SCREENSHOT_PATH;
            _new.SCREENSHOT_FILENAME = _old.SCREENSHOT_FILENAME;
            _new.SCREENSHOT_EXTENSION = _old.SCREENSHOT_EXTENSION;

            _new.WEBCAM_PATH = _old.WEBCAM_PATH;
            _new.WEBCAM_FILENAME = _old.WEBCAM_FILENAME;
            _new.WEBCAM_EXTENSION = _old.WEBCAM_EXTENSION;
        }
    }
}
