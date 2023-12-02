using PulsarModLoader;

namespace System_Damage_Notification
{
    public class Mod : PulsarMod
    {
        public override string Version => "0.0.2";

        public override string Author => "18107";

        public override string ShortDescription => "Displays a notification when any of the ship systems get damaged";

        public override string Name => "System Damage Notification";

        public override string ModID => "systemdamagenotification";

        public override string HarmonyIdentifier()
        {
            return "id107.systemdamagenotification";
        }
    }
}
