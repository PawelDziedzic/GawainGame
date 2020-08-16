using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class GameManagerScript : MonoBehaviour
    {
        public GameObject playerPrefab;
        public GameObject enemyDefenderPrefab;
        public GameObject allyDefenderPrefab;
        public GameObject raiderPrefab;
        static DateTime stageStart;
        public Canvas sectorMap;
        public Canvas messageCanvas;
        public RawImage[] enemySectorGraph;
        public RawImage[] alliedSectorGraph;
        public static GameManagerScript instance;
        public List<GameObject> wallPiecePrefabs;
        
        public static GameObject PlayerPrefab()
        {
            return instance.playerPrefab;
        }
        
        public static GameObject RandomWallPiece()
        {
            System.Random rnd = new System.Random();
            int raffle = rnd.Next(16);
            if (raffle <= 8)
                return instance.wallPiecePrefabs[0];
            if (raffle <= 12)
                return instance.wallPiecePrefabs[1];

            return instance.wallPiecePrefabs[2];
        }

        public static Canvas MessageCanvas()
        {
            return instance.messageCanvas;
        }

        public static GameObject EnemyDefenderPrefab()
        {
            return instance.enemyDefenderPrefab;
        }

        public static GameObject AllyDefenderPrefab()
        {
            return instance.allyDefenderPrefab;
        }

        public static GameObject RaiderPrefab()
        {
            return instance.raiderPrefab;
        }

        public static RawImage[] EnemySectorGraph()
        {
            return instance.enemySectorGraph;
        }

        public static RawImage[] AlliedSectorGraph()
        {
            return instance.alliedSectorGraph;
        }

        public static Canvas SectorMap()
        {
            return instance.sectorMap;
        }

        void OnEnable()
        {
            instance = this;
            stageStart = DateTime.Now;
        }

        public void MoveNAggressors(int n, GameObject startSector, GameObject endSector)
        {

        }

        public static int GetStagePhase()
        {
            int minutesPassed = ((DateTime.Now.Minute - stageStart.Minute) + 60)%60;

            if (minutesPassed < 3)
                return 2;
            if (minutesPassed < 5)
                return 3;
            if (minutesPassed < 10)
                return 4;

            return 5;
        }
    }
}
