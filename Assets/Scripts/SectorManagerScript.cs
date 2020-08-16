using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SectorManagerScript : MonoBehaviour
    {
        static Dictionary<string,BasicSectorScript> allSectors = new Dictionary<string, BasicSectorScript>();

        public static void AddSector(string name, BasicSectorScript script)
        {
            script.index = allSectors.Count;
            allSectors.Add(name, script);
        }

        public static void SectorGained()
        {
            if (AllAllied())
                YouWin();
        }

        public static void SectorLost()
        {
            if (AllEnemy())
                GameOver();
        }

        static bool AllAllied()
        {
            foreach(KeyValuePair<string,BasicSectorScript> sector in allSectors)
            {
                if (!sector.Value.isAllied.Value)
                    return false;
            }

            return true;
        }

        static bool AllEnemy()
        {
            foreach (KeyValuePair<string, BasicSectorScript> sector in allSectors)
            {
                if (sector.Value.isAllied.Value)
                    return false;
            }

            return true;
        }

        static void YouWin()
        {

        }

        static void GameOver()
        {

        }
    }
}
