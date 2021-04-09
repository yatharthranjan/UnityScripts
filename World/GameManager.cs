using UnityEngine;
using System.Collections;

namespace Assets.Scripts.World
{
    public class GameManager : MonoBehaviour
    {
        public int TargetFPS = 40;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = TargetFPS;
        }
    }
}