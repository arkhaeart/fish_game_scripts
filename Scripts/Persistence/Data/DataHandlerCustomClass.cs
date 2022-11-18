using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Persistence
{
    // This class is filled with game-specific info and used to correctly initialize DataHandler class
    public static class DataHandlerCustomClass
    {
        static DataHandlerInfo info;
        public static DataHandlerInfo GetDataHandlerInfo()
        {
            info = new DataHandlerInfo();
            FillDataPathsDict();
            FillResourcesPathsDict();
            return info;
        }
        static void FillDataPathsDict()
        {
            info.dataPaths = new Dictionary<System.Type, string>
            {
                [typeof(PlayerData)] = "player_data.json"
            };
        }
        static void FillResourcesPathsDict()
        {
            info.jsonResourcesPaths = new Dictionary<System.Type, string>
            {
                [typeof(PlayerData)]= "DataSamples/player_data"
            };
        }
    }
    public class DataHandlerInfo
    {
        public Dictionary<System.Type, string> dataPaths;
        public Dictionary<System.Type, string> jsonResourcesPaths;
    }
}