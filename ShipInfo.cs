using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace System_Damage_Notification
{
    [HarmonyPatch(typeof(PLShipInfo), "Update")]
    internal class ShipInfo
    {
        private static readonly float pingOffset = 0.6f;
        private static readonly int numSystems = 4;
        private static readonly string[] systemName = new string[] { "Engineering", "Weapons", "Life Support", "Science" };
        private static readonly float[] systemHealth = new float[numSystems];
        private static float lastMessageTime = 0f;

        static void Postfix(PLShipInfo __instance)
        {
            if (!__instance.GetIsPlayerShip() || Time.time - lastMessageTime < 5f || __instance.RepairableSystemInstances?.Length != numSystems ||
                PLNetworkManager.Instance?.LocalPlayer == null) return;

            for (int i = 0; i < numSystems; i++)
            {
                PLMainSystem system = __instance.RepairableSystemInstances[i]?.MySystem;
                if (system.Health + pingOffset < systemHealth[i])
                {
                    PulsarModLoader.Utilities.Messaging.Notification($"{systemName[system.SystemID]} system damaged");
                    lastMessageTime = Time.time;
                    systemHealth[i] = system.Health;
                }
                if (system.Health > systemHealth[i])
                {
                    systemHealth[i] = system.Health;
                }
            }
        }
    }
}
