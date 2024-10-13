using UnityEngine;

namespace UltraEvents.Utils
{
    // Token: 0x02000005 RID: 5
    internal static class ModUtils
    {
        // Token: 0x06000066 RID: 102 RVA: 0x000059EC File Offset: 0x00003BEC
        public static NewMovement GetPlayerTransform()
        {
            return MonoSingleton<NewMovement>.Instance;
        }

        // Token: 0x06000067 RID: 103 RVA: 0x00005A08 File Offset: 0x00003C08
        public static void AttachWeapon(int tempSlot, string pPref, GameObject weapon, GunSetter gs)
        {
            bool flag = false;
            bool flag2 = pPref != "";
            if (flag2)
            {
                flag = (GameProgressSaver.CheckGear(pPref) != 0 && MonoSingleton<PrefsManager>.Instance.GetInt("weapon." + pPref, 0) != 0);
                bool flag3 = !flag;
                if (flag3)
                {
                    MonoSingleton<PrefsManager>.Instance.SetInt("weapon." + pPref, 1);
                    bool flag4 = !SceneHelper.IsPlayingCustom;
                    if (flag4)
                    {
                        GameProgressSaver.AddGear(pPref);
                    }
                }
            }
            MonoSingleton<GunControl>.Instance.noWeapons = false;
            bool flag5 = gs != null;
            if (flag5)
            {
                gs.enabled = true;
                gs.ResetWeapons(false);
            }
            bool flag6 = !flag;
            if (flag6)
            {
                for (int i = 0; i < MonoSingleton<GunControl>.Instance.slots[tempSlot].Count; i++)
                {
                    bool flag7 = MonoSingleton<GunControl>.Instance.slots[tempSlot][i].name == weapon.name + "(Clone)";
                    if (flag7)
                    {
                        flag = true;
                    }
                }
            }
            bool flag8 = !flag;
            if (flag8)
            {
                GameObject item = Object.Instantiate<GameObject>(weapon, MonoSingleton<GunControl>.Instance.transform);
                MonoSingleton<GunControl>.Instance.slots[tempSlot].Add(item);
                MonoSingleton<GunControl>.Instance.ForceWeapon(weapon, true);
                MonoSingleton<GunControl>.Instance.noWeapons = false;
                MonoSingleton<GunControl>.Instance.UpdateWeaponList(false);
            }
            else
            {
                bool isPlayingCustom = SceneHelper.IsPlayingCustom;
                if (isPlayingCustom)
                {
                    for (int j = 0; j < MonoSingleton<GunControl>.Instance.slots[tempSlot].Count; j++)
                    {
                        bool flag9 = MonoSingleton<GunControl>.Instance.slots[tempSlot][j].name == weapon.name + "(Clone)";
                        if (flag9)
                        {
                            MonoSingleton<GunControl>.Instance.ForceWeapon(weapon, true);
                            MonoSingleton<GunControl>.Instance.noWeapons = false;
                            MonoSingleton<GunControl>.Instance.UpdateWeaponList(false);
                        }
                    }
                }
            }
        }
    }
}
