using System;
using CalamityMod.Events;
using CalamityMod.Utilities;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class EmpressofLightAI : VanillaAIOverride
{
	public static float EverlastingRainbowTrailDamageMult = 0.75f;

	public static float DashDamageMult = 1.5f;

	public static int PrismaticBoltDamage = 30;

	public static int EverlastingRainbowDamage = 30;

	public static int EtherealLanceDamage = 30;

	public static int SunDanceDamage = 35;

	public static int Phase2PrismaticBoltDamage = 35;

	public static int Phase2EverlastingRainbowDamage = 35;

	public static int Phase2EtherealLanceDamage = 35;

	public static int Phase2SunDanceDamage = 40;

	public static int ContactDamageCorrection = (Main.masterMode ? 248 : 110);

	public override bool AI(Mod mod)
	{
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_158b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1596: Unknown result type (might be due to invalid IL or missing references)
		//IL_2884: Unknown result type (might be due to invalid IL or missing references)
		//IL_288f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a23: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f76: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f80: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f85: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f44: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f65: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_32ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_32f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_32fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_32ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_32c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_32db: Unknown result type (might be due to invalid IL or missing references)
		//IL_32e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c41: Unknown result type (might be due to invalid IL or missing references)
		//IL_15db: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_223c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1636: Unknown result type (might be due to invalid IL or missing references)
		//IL_1629: Unknown result type (might be due to invalid IL or missing references)
		//IL_28c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c93: Unknown result type (might be due to invalid IL or missing references)
		//IL_377f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3784: Unknown result type (might be due to invalid IL or missing references)
		//IL_3794: Unknown result type (might be due to invalid IL or missing references)
		//IL_163b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1643: Unknown result type (might be due to invalid IL or missing references)
		//IL_1645: Unknown result type (might be due to invalid IL or missing references)
		//IL_1647: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_3380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1036: Unknown result type (might be due to invalid IL or missing references)
		//IL_1045: Unknown result type (might be due to invalid IL or missing references)
		//IL_104a: Unknown result type (might be due to invalid IL or missing references)
		//IL_107b: Unknown result type (might be due to invalid IL or missing references)
		//IL_108a: Unknown result type (might be due to invalid IL or missing references)
		//IL_108f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1661: Unknown result type (might be due to invalid IL or missing references)
		//IL_1663: Unknown result type (might be due to invalid IL or missing references)
		//IL_1665: Unknown result type (might be due to invalid IL or missing references)
		//IL_166a: Unknown result type (might be due to invalid IL or missing references)
		//IL_166f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1674: Unknown result type (might be due to invalid IL or missing references)
		//IL_167b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1685: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b23: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b16: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a79: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2abe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2acd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_076c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0776: Unknown result type (might be due to invalid IL or missing references)
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a08: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2956: Unknown result type (might be due to invalid IL or missing references)
		//IL_2965: Unknown result type (might be due to invalid IL or missing references)
		//IL_296a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b28: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b32: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b34: Unknown result type (might be due to invalid IL or missing references)
		//IL_110b: Unknown result type (might be due to invalid IL or missing references)
		//IL_110d: Unknown result type (might be due to invalid IL or missing references)
		//IL_110f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1114: Unknown result type (might be due to invalid IL or missing references)
		//IL_1119: Unknown result type (might be due to invalid IL or missing references)
		//IL_111e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1125: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ccd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b50: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b52: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b68: Unknown result type (might be due to invalid IL or missing references)
		//IL_3429: Unknown result type (might be due to invalid IL or missing references)
		//IL_342e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3436: Unknown result type (might be due to invalid IL or missing references)
		//IL_3441: Unknown result type (might be due to invalid IL or missing references)
		//IL_344b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3450: Unknown result type (might be due to invalid IL or missing references)
		//IL_347f: Unknown result type (might be due to invalid IL or missing references)
		//IL_349b: Unknown result type (might be due to invalid IL or missing references)
		//IL_34bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_34c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_34c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_34d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_34ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_34ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_3554: Unknown result type (might be due to invalid IL or missing references)
		//IL_3559: Unknown result type (might be due to invalid IL or missing references)
		//IL_3563: Unknown result type (might be due to invalid IL or missing references)
		//IL_3568: Unknown result type (might be due to invalid IL or missing references)
		//IL_356d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d01: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d17: Unknown result type (might be due to invalid IL or missing references)
		//IL_35cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_35d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a62: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a64: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a86: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aca: Unknown result type (might be due to invalid IL or missing references)
		//IL_1747: Unknown result type (might be due to invalid IL or missing references)
		//IL_1760: Unknown result type (might be due to invalid IL or missing references)
		//IL_1766: Unknown result type (might be due to invalid IL or missing references)
		//IL_1768: Unknown result type (might be due to invalid IL or missing references)
		//IL_176d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af6: Unknown result type (might be due to invalid IL or missing references)
		//IL_3084: Unknown result type (might be due to invalid IL or missing references)
		//IL_3094: Unknown result type (might be due to invalid IL or missing references)
		//IL_309a: Unknown result type (might be due to invalid IL or missing references)
		//IL_309c: Unknown result type (might be due to invalid IL or missing references)
		//IL_30a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_074f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2698: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2303: Unknown result type (might be due to invalid IL or missing references)
		//IL_230e: Unknown result type (might be due to invalid IL or missing references)
		//IL_30c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_30c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_30c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_30cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2400: Unknown result type (might be due to invalid IL or missing references)
		//IL_233a: Unknown result type (might be due to invalid IL or missing references)
		//IL_232d: Unknown result type (might be due to invalid IL or missing references)
		//IL_310b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3110: Unknown result type (might be due to invalid IL or missing references)
		//IL_3112: Unknown result type (might be due to invalid IL or missing references)
		//IL_3117: Unknown result type (might be due to invalid IL or missing references)
		//IL_3119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_261e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2623: Unknown result type (might be due to invalid IL or missing references)
		//IL_2627: Unknown result type (might be due to invalid IL or missing references)
		//IL_262c: Unknown result type (might be due to invalid IL or missing references)
		//IL_234b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2350: Unknown result type (might be due to invalid IL or missing references)
		//IL_2355: Unknown result type (might be due to invalid IL or missing references)
		//IL_2363: Unknown result type (might be due to invalid IL or missing references)
		//IL_2365: Unknown result type (might be due to invalid IL or missing references)
		//IL_236a: Unknown result type (might be due to invalid IL or missing references)
		//IL_236f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_264c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2656: Unknown result type (might be due to invalid IL or missing references)
		//IL_265b: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_23ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_23b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e60: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2467: Unknown result type (might be due to invalid IL or missing references)
		//IL_247f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2485: Unknown result type (might be due to invalid IL or missing references)
		//IL_2487: Unknown result type (might be due to invalid IL or missing references)
		//IL_248c: Unknown result type (might be due to invalid IL or missing references)
		//IL_31c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_31cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_31cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_31d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_181f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1821: Unknown result type (might be due to invalid IL or missing references)
		//IL_182e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1834: Unknown result type (might be due to invalid IL or missing references)
		//IL_1836: Unknown result type (might be due to invalid IL or missing references)
		//IL_1840: Unknown result type (might be due to invalid IL or missing references)
		//IL_1845: Unknown result type (might be due to invalid IL or missing references)
		//IL_184a: Unknown result type (might be due to invalid IL or missing references)
		//IL_184e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2be2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2be7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c03: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c21: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eee: Unknown result type (might be due to invalid IL or missing references)
		//IL_188e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1890: Unknown result type (might be due to invalid IL or missing references)
		//IL_189d: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_18af: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_272c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2731: Unknown result type (might be due to invalid IL or missing references)
		//IL_2733: Unknown result type (might be due to invalid IL or missing references)
		//IL_273f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2744: Unknown result type (might be due to invalid IL or missing references)
		//IL_2749: Unknown result type (might be due to invalid IL or missing references)
		//IL_2751: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2508: Unknown result type (might be due to invalid IL or missing references)
		//IL_250d: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_24be: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_24cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e82: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e89: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e93: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ecd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f05: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f13: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f46: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f61: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f68: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f72: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fad: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2006: Unknown result type (might be due to invalid IL or missing references)
		//IL_200b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2010: Unknown result type (might be due to invalid IL or missing references)
		//IL_2029: Unknown result type (might be due to invalid IL or missing references)
		//IL_202d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2032: Unknown result type (might be due to invalid IL or missing references)
		//IL_2037: Unknown result type (might be due to invalid IL or missing references)
		//IL_277f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2787: Unknown result type (might be due to invalid IL or missing references)
		//IL_278c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2791: Unknown result type (might be due to invalid IL or missing references)
		//IL_275f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2767: Unknown result type (might be due to invalid IL or missing references)
		//IL_2769: Unknown result type (might be due to invalid IL or missing references)
		//IL_2773: Unknown result type (might be due to invalid IL or missing references)
		//IL_2778: Unknown result type (might be due to invalid IL or missing references)
		//IL_277d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2529: Unknown result type (might be due to invalid IL or missing references)
		//IL_252e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c42: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11da: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c64: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c74: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c76: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c78: Unknown result type (might be due to invalid IL or missing references)
		//IL_121d: Unknown result type (might be due to invalid IL or missing references)
		//IL_121f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1224: Unknown result type (might be due to invalid IL or missing references)
		//IL_1229: Unknown result type (might be due to invalid IL or missing references)
		//IL_1234: Unknown result type (might be due to invalid IL or missing references)
		//IL_1239: Unknown result type (might be due to invalid IL or missing references)
		//IL_1241: Unknown result type (might be due to invalid IL or missing references)
		//IL_1201: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cce: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c83: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c85: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c90: Unknown result type (might be due to invalid IL or missing references)
		//IL_1254: Unknown result type (might be due to invalid IL or missing references)
		//IL_1259: Unknown result type (might be due to invalid IL or missing references)
		//IL_125e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1263: Unknown result type (might be due to invalid IL or missing references)
		//IL_2050: Unknown result type (might be due to invalid IL or missing references)
		//IL_2052: Unknown result type (might be due to invalid IL or missing references)
		//IL_205c: Unknown result type (might be due to invalid IL or missing references)
		//IL_27d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_27d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_27dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_27e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_27e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_27f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_27f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_27fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ce6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ca0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ca2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ca4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cba: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_127f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1283: Unknown result type (might be due to invalid IL or missing references)
		//IL_128d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1292: Unknown result type (might be due to invalid IL or missing references)
		//IL_1297: Unknown result type (might be due to invalid IL or missing references)
		//IL_1299: Unknown result type (might be due to invalid IL or missing references)
		//IL_129d: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1271: Unknown result type (might be due to invalid IL or missing references)
		//IL_1278: Unknown result type (might be due to invalid IL or missing references)
		//IL_127d: Unknown result type (might be due to invalid IL or missing references)
		//IL_25cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_25d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1310: Unknown result type (might be due to invalid IL or missing references)
		//IL_1312: Unknown result type (might be due to invalid IL or missing references)
		//IL_1317: Unknown result type (might be due to invalid IL or missing references)
		//IL_12cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2071: Unknown result type (might be due to invalid IL or missing references)
		//IL_2076: Unknown result type (might be due to invalid IL or missing references)
		//IL_207b: Unknown result type (might be due to invalid IL or missing references)
		//IL_207d: Unknown result type (might be due to invalid IL or missing references)
		//IL_207f: Unknown result type (might be due to invalid IL or missing references)
		//IL_132d: Unknown result type (might be due to invalid IL or missing references)
		//IL_132f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1342: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1303: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2087: Unknown result type (might be due to invalid IL or missing references)
		//IL_2091: Unknown result type (might be due to invalid IL or missing references)
		//IL_2098: Unknown result type (might be due to invalid IL or missing references)
		//IL_209d: Unknown result type (might be due to invalid IL or missing references)
		//IL_209f: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_20aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_20af: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_20bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_20c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_20c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d73: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d81: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d83: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d85: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d97: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2daf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2db4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2da6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1408: Unknown result type (might be due to invalid IL or missing references)
		//IL_140d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1412: Unknown result type (might be due to invalid IL or missing references)
		//IL_1414: Unknown result type (might be due to invalid IL or missing references)
		//IL_1416: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dce: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dda: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ddc: Unknown result type (might be due to invalid IL or missing references)
		//IL_145a: Unknown result type (might be due to invalid IL or missing references)
		//IL_145c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1461: Unknown result type (might be due to invalid IL or missing references)
		//IL_146f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1471: Unknown result type (might be due to invalid IL or missing references)
		//IL_1484: Unknown result type (might be due to invalid IL or missing references)
		//IL_1421: Unknown result type (might be due to invalid IL or missing references)
		//IL_1423: Unknown result type (might be due to invalid IL or missing references)
		//IL_1425: Unknown result type (might be due to invalid IL or missing references)
		//IL_142a: Unknown result type (might be due to invalid IL or missing references)
		//IL_142c: Unknown result type (might be due to invalid IL or missing references)
		//IL_142e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e20: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e22: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e27: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e35: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e37: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2de7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2de9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2deb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2df0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2df2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2df4: Unknown result type (might be due to invalid IL or missing references)
		//IL_143e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1440: Unknown result type (might be due to invalid IL or missing references)
		//IL_1442: Unknown result type (might be due to invalid IL or missing references)
		//IL_1447: Unknown result type (might be due to invalid IL or missing references)
		//IL_144e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1453: Unknown result type (might be due to invalid IL or missing references)
		//IL_1458: Unknown result type (might be due to invalid IL or missing references)
		//IL_143a: Unknown result type (might be due to invalid IL or missing references)
		//IL_143c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e04: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e06: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e08: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e14: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e19: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e00: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e02: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		base.NPC.rotation = base.NPC.velocity.X * 0.005f;
		calamityGlobalNPC.DR = 0.15f;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		float phase2LifeRatio = (death ? 0.7f : 0.6f);
		float phase3LifeRatio = (death ? 0.3f : 0.15f);
		bool phase2 = base.NPC.AI_120_HallowBoss_IsInPhase2();
		bool phase3 = lifeRatio <= phase3LifeRatio;
		int boltDamage = (phase2 ? Phase2PrismaticBoltDamage : PrismaticBoltDamage).CalculateDamageForEnrage();
		int rainbowDamage = (phase2 ? Phase2EverlastingRainbowDamage : EverlastingRainbowDamage).CalculateDamageForEnrage();
		int lanceDamage = (phase2 ? Phase2EtherealLanceDamage : EtherealLanceDamage).CalculateDamageForEnrage();
		int sunDanceDamage = (phase2 ? Phase2SunDanceDamage : SunDanceDamage).CalculateDamageForEnrage();
		bool shouldBeInPhase2ButIsStillInPhase1 = lifeRatio <= phase2LifeRatio && !phase2;
		if (shouldBeInPhase2ButIsStillInPhase1)
		{
			calamityGlobalNPC.DR = 0.99f;
		}
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = shouldBeInPhase2ButIsStillInPhase1 || base.NPC.ai[0] == 6f;
		bool dayTimeEnrage = NPC.ShouldEmpressBeEnraged();
		if (((base.NPC.life == base.NPC.lifeMax) & dayTimeEnrage) && !base.NPC.AI_120_HallowBoss_IsGenuinelyEnraged())
		{
			base.NPC.ai[3] += 2f;
		}
		base.NPC.Calamity().CurrentlyEnraged = !BossRushEvent.BossRushActive & dayTimeEnrage;
		Vector2 rainbowStreakDistance = default(Vector2);
		((Vector2)(ref rainbowStreakDistance))._002Ector(-150f, -250f);
		Vector2 everlastingRainbowDistance = default(Vector2);
		((Vector2)(ref everlastingRainbowDistance))._002Ector(0f, -350f);
		Vector2 etherealLanceDistance = default(Vector2);
		((Vector2)(ref etherealLanceDistance))._002Ector(0f, -350f);
		Vector2 sunDanceDistance = default(Vector2);
		((Vector2)(ref sunDanceDistance))._002Ector(-80f, -500f);
		float acceleration = (death ? 0.66f : 0.6f);
		float velocity = (death ? 16.5f : 15f);
		float movementDistanceGateValue = 40f;
		float despawnDistanceGateValue = 6400f;
		if (dayTimeEnrage)
		{
			float enragedDistanceMultiplier = 1.1f;
			rainbowStreakDistance *= enragedDistanceMultiplier;
			everlastingRainbowDistance *= enragedDistanceMultiplier;
			etherealLanceDistance *= enragedDistanceMultiplier;
			float enragedVelocityMultiplier = 1.2f;
			acceleration *= enragedVelocityMultiplier;
			velocity *= enragedVelocityMultiplier;
		}
		bool visible = true;
		bool takeDamage = true;
		float lessTimeSpentPerPhaseMultiplier = ((!phase2) ? (death ? 0.75f : 1f) : (death ? 0.375f : 0.5f));
		if (Main.getGoodWorld)
		{
			lessTimeSpentPerPhaseMultiplier *= 0.2f;
		}
		float playSpawnSoundTime = 10f;
		float stopSpawningDustTime = 150f;
		float spawnTime = 180f;
		float maxOpacity = (phase3 ? 0.7f : 1f);
		if (phase3)
		{
			if (calamityGlobalNPC.newAI[0] == playSpawnSoundTime)
			{
				SoundEngine.PlaySound(in SoundID.Item161, base.NPC.Center);
			}
			if (calamityGlobalNPC.newAI[0] > playSpawnSoundTime && calamityGlobalNPC.newAI[0] < stopSpawningDustTime)
			{
				CreateSpawnDust(base.NPC, useAI: false);
			}
			calamityGlobalNPC.newAI[0]++;
			if (calamityGlobalNPC.newAI[0] >= stopSpawningDustTime)
			{
				calamityGlobalNPC.newAI[0] = playSpawnSoundTime + 1f;
				base.NPC.SyncExtraAI();
			}
		}
		Vector2 val = default(Vector2);
		switch ((int)base.NPC.ai[0])
		{
		case 0:
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.velocity = new Vector2(0f, 5f);
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + new Vector2(0f, -80f), Vector2.Zero, 874, 0, 0f, Main.myPlayer);
				}
			}
			if (base.NPC.ai[1] == playSpawnSoundTime)
			{
				SoundEngine.PlaySound(in SoundID.Item161, base.NPC.Center);
			}
			NPC nPC3 = base.NPC;
			nPC3.velocity *= 0.95f;
			if (base.NPC.ai[1] > playSpawnSoundTime && base.NPC.ai[1] < stopSpawningDustTime)
			{
				CreateSpawnDust(base.NPC);
			}
			base.NPC.ai[1]++;
			visible = false;
			takeDamage = false;
			base.NPC.Opacity = MathHelper.Clamp(base.NPC.ai[1] / spawnTime, 0f, 1f);
			if (base.NPC.ai[1] >= spawnTime)
			{
				if (dayTimeEnrage && !base.NPC.AI_120_HallowBoss_IsGenuinelyEnraged())
				{
					base.NPC.ai[3] += 2f;
				}
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			}
			break;
		}
		case 1:
		{
			base.NPC.damage = 0;
			float idleTimer = ((!phase2) ? (death ? 20f : 30f) : (death ? 10f : 15f));
			if (Main.getGoodWorld)
			{
				idleTimer *= 0.5f;
			}
			if (idleTimer < 10f)
			{
				idleTimer = 10f;
			}
			if (base.NPC.ai[1] <= 10f)
			{
				if (base.NPC.ai[1] == 0f)
				{
					base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
				}
				NPCAimedTarget targetData17 = base.NPC.GetTargetData();
				if (targetData17.Invalid)
				{
					base.NPC.ai[0] = 13f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2]++;
					NPC nPC8 = base.NPC;
					nPC8.velocity /= 4f;
					base.NPC.netUpdate = true;
					break;
				}
				Vector2 center2 = targetData17.Center;
				center2 += new Vector2(0f, -400f);
				if (base.NPC.Distance(center2) > 200f)
				{
					center2 -= base.NPC.DirectionTo(center2) * 100f;
				}
				Vector2 targetDirection2 = center2 - base.NPC.Center;
				float lerpValue2 = Utils.GetLerpValue(100f, 600f, ((Vector2)(ref targetDirection2)).Length());
				float targetDistance2 = ((Vector2)(ref targetDirection2)).Length();
				float maxVelocity2 = (death ? 24f : 21f);
				if (targetDistance2 > maxVelocity2)
				{
					targetDistance2 = maxVelocity2;
				}
				base.NPC.velocity = Vector2.Lerp(targetDirection2.SafeNormalize(Vector2.Zero) * targetDistance2, targetDirection2 / 6f, lerpValue2);
				base.NPC.netUpdate = true;
			}
			NPC nPC9 = base.NPC;
			nPC9.velocity *= 0.92f;
			base.NPC.ai[1]++;
			if (!(base.NPC.ai[1] >= idleTimer))
			{
				break;
			}
			int attackPatternLength = (int)base.NPC.ai[2];
			int attackType = 2;
			int attackIncrement = 0;
			if (!phase2)
			{
				int phase1Attack1 = attackIncrement++;
				int phase1Attack2 = attackIncrement++;
				int phase1Attack3 = attackIncrement++;
				int phase1Attack4 = attackIncrement++;
				int phase1Attack5 = attackIncrement++;
				int phase1Attack6 = attackIncrement++;
				int phase1Attack7 = attackIncrement++;
				int phase1Attack8 = attackIncrement++;
				int phase1Attack9 = attackIncrement++;
				int phase1Attack10 = attackIncrement++;
				if (attackPatternLength % attackIncrement == phase1Attack1)
				{
					attackType = 2;
				}
				if (attackPatternLength % attackIncrement == phase1Attack2)
				{
					attackType = 6;
				}
				if (attackPatternLength % attackIncrement == phase1Attack3)
				{
					attackType = 8;
				}
				if (attackPatternLength % attackIncrement == phase1Attack4)
				{
					attackType = 4;
					calamityGlobalNPC.newAI[3] = Main.rand.Next(2);
					base.NPC.SyncExtraAI();
				}
				if (attackPatternLength % attackIncrement == phase1Attack5)
				{
					attackType = 5;
				}
				if (attackPatternLength % attackIncrement == phase1Attack6)
				{
					attackType = 8;
				}
				if (attackPatternLength % attackIncrement == phase1Attack7)
				{
					attackType = 2;
				}
				if (attackPatternLength % attackIncrement == phase1Attack8)
				{
					attackType = 4;
					calamityGlobalNPC.newAI[3] = Main.rand.Next(2);
					base.NPC.SyncExtraAI();
				}
				if (attackPatternLength % attackIncrement == phase1Attack9)
				{
					attackType = 8;
				}
				if (attackPatternLength % attackIncrement == phase1Attack10)
				{
					attackType = 5;
				}
				if (lifeRatio <= phase2LifeRatio)
				{
					attackType = 10;
				}
			}
			if (phase2)
			{
				int phase2Attack1 = attackIncrement++;
				int phase2Attack2 = attackIncrement++;
				int phase2Attack3 = attackIncrement++;
				int phase2Attack4 = attackIncrement++;
				int phase2Attack5 = attackIncrement++;
				int phase2Attack6 = attackIncrement++;
				int phase2Attack7 = attackIncrement++;
				int phase2Attack8 = attackIncrement++;
				int phase2Attack9 = attackIncrement++;
				int phase2Attack10 = attackIncrement++;
				if (attackPatternLength % attackIncrement == phase2Attack1)
				{
					attackType = 7;
					calamityGlobalNPC.newAI[2] = Main.rand.Next(2);
					base.NPC.SyncExtraAI();
				}
				if (attackPatternLength % attackIncrement == phase2Attack2)
				{
					attackType = (phase3 ? 8 : 2);
				}
				if (attackPatternLength % attackIncrement == phase2Attack3)
				{
					attackType = 8;
				}
				if (attackPatternLength % attackIncrement == phase2Attack5)
				{
					attackType = 5;
				}
				if (attackPatternLength % attackIncrement == phase2Attack6)
				{
					attackType = 2;
				}
				if (attackPatternLength % attackIncrement == phase2Attack7)
				{
					if (phase3)
					{
						attackType = 7;
						calamityGlobalNPC.newAI[2] = Main.rand.Next(2);
						base.NPC.SyncExtraAI();
					}
					else
					{
						attackType = 6;
					}
				}
				if (attackPatternLength % attackIncrement == phase2Attack7)
				{
					if (phase3)
					{
						attackType = 4;
						calamityGlobalNPC.newAI[3] = Main.rand.Next(2);
						base.NPC.SyncExtraAI();
					}
					else
					{
						attackType = 6;
					}
				}
				if (attackPatternLength % attackIncrement == phase2Attack8)
				{
					attackType = 4;
					calamityGlobalNPC.newAI[3] = Main.rand.Next(2);
					base.NPC.SyncExtraAI();
				}
				if (attackPatternLength % attackIncrement == phase2Attack9)
				{
					attackType = 8;
				}
				if (attackPatternLength % attackIncrement == phase2Attack4)
				{
					attackType = 11;
				}
				if (attackPatternLength % attackIncrement == phase2Attack10)
				{
					attackType = 12;
				}
			}
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			NPCAimedTarget targetData18 = base.NPC.GetTargetData();
			bool despawnFlag = false;
			if (base.NPC.AI_120_HallowBoss_IsGenuinelyEnraged() && !BossRushEvent.BossRushActive)
			{
				if (!Main.dayTime)
				{
					despawnFlag = true;
				}
				if (Main.dayTime && Main.time >= 53400.0)
				{
					despawnFlag = true;
				}
			}
			if ((targetData18.Invalid || base.NPC.Distance(targetData18.Center) > despawnDistanceGateValue) | despawnFlag)
			{
				attackType = 13;
			}
			if (attackType == 8 && targetData18.Center.X > base.NPC.Center.X)
			{
				attackType = 9;
			}
			if (attackType != 5 && attackType != 12)
			{
				NPC nPC10 = base.NPC;
				Vector2 spinningpoint6 = base.NPC.DirectionFrom(targetData18.Center).SafeNormalize(Vector2.Zero);
				double radians7 = (float)Math.PI / 2f * (float)(targetData18.Center.X > base.NPC.Center.X).ToDirectionInt();
				val = default(Vector2);
				nPC10.velocity = spinningpoint6.RotatedBy(radians7, val) * 24f;
			}
			base.NPC.ai[0] = attackType;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] += (float)Main.rand.Next(2) + 1f;
			base.NPC.netUpdate = true;
			break;
		}
		case 2:
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item164, base.NPC.Center);
			}
			Vector2 randomStreakOffset = default(Vector2);
			((Vector2)(ref randomStreakOffset))._002Ector(-55f, -30f);
			NPCAimedTarget targetData12 = base.NPC.GetTargetData();
			Vector2 targetCenter = (targetData12.Invalid ? base.NPC.Center : targetData12.Center);
			if (base.NPC.Distance(targetCenter + rainbowStreakDistance) > movementDistanceGateValue)
			{
				base.NPC.SimpleFlyMovement(base.NPC.DirectionTo(targetCenter + rainbowStreakDistance).SafeNormalize(Vector2.Zero) * velocity, acceleration);
			}
			if (base.NPC.ai[1] < 60f)
			{
				AI_120_HallowBoss_DoMagicEffect(base.NPC.Center + randomStreakOffset, 1, Utils.GetLerpValue(0f, 60f, base.NPC.ai[1], clamped: true), base.NPC);
			}
			int streakSpawnFrequency = (Main.getGoodWorld ? 1 : 2);
			if (phase3)
			{
				streakSpawnFrequency *= 2;
			}
			if ((int)base.NPC.ai[1] % streakSpawnFrequency == 0 && base.NPC.ai[1] < 60f)
			{
				int projectileType3 = 873;
				int projectileDamage3 = boltDamage;
				float ai3 = base.NPC.ai[1] / 60f;
				Vector2 spinningpoint = new Vector2(0f, death ? (-10f) : (-8f));
				double radians2 = (float)Math.PI / 2f * Main.rand.NextFloatDirection();
				val = default(Vector2);
				Vector2 rainbowStreakVelocity = Utils.RotatedBy(spinningpoint, radians2, val);
				if (phase2)
				{
					Vector2 spinningpoint2 = new Vector2(0f, death ? (-12f) : (-10f));
					double radians3 = (float)Math.PI * 2f * Main.rand.NextFloat();
					val = default(Vector2);
					rainbowStreakVelocity = Utils.RotatedBy(spinningpoint2, radians3, val);
				}
				if (dayTimeEnrage)
				{
					rainbowStreakVelocity *= MathHelper.Lerp(0.8f, 1.6f, ai3);
				}
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + randomStreakOffset, rainbowStreakVelocity, projectileType3, projectileDamage3, 0f, Main.myPlayer, base.NPC.target, ai3);
					if (phase3)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + randomStreakOffset, -rainbowStreakVelocity, projectileType3, projectileDamage3, 0f, Main.myPlayer, base.NPC.target, 1f - ai3);
					}
				}
				if (Main.netMode != 1)
				{
					int multiplayerStreakSpawnFrequency = (int)(base.NPC.ai[1] / (float)streakSpawnFrequency);
					for (int l = 0; l < 255; l++)
					{
						if (base.NPC.Boss_CanShootExtraAt(l, multiplayerStreakSpawnFrequency % 3, 3, 2400f))
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + randomStreakOffset, rainbowStreakVelocity, projectileType3, projectileDamage3, 0f, Main.myPlayer, l, ai3);
						}
					}
				}
			}
			base.NPC.ai[1]++;
			float extraPhaseTime = ((!dayTimeEnrage) ? (death ? 60f : 72f) : (death ? 30f : 36f)) + 30f * lessTimeSpentPerPhaseMultiplier;
			if (base.NPC.ai[1] >= 60f + extraPhaseTime)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 4:
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item162, base.NPC.Center);
			}
			float lanceGateValue = (death ? 75f : 100f);
			if (base.NPC.ai[1] >= 6f && base.NPC.ai[1] < 54f)
			{
				AI_120_HallowBoss_DoMagicEffect(base.NPC.Center + new Vector2(-55f, -20f), 2, Utils.GetLerpValue(0f, lanceGateValue, base.NPC.ai[1], clamped: true), base.NPC);
				AI_120_HallowBoss_DoMagicEffect(base.NPC.Center + new Vector2(55f, -20f), 4, Utils.GetLerpValue(0f, lanceGateValue, base.NPC.ai[1], clamped: true), base.NPC);
			}
			NPCAimedTarget targetData10 = base.NPC.GetTargetData();
			Vector2 targetCenter = (targetData10.Invalid ? base.NPC.Center : targetData10.Center);
			if (base.NPC.Distance(targetCenter + etherealLanceDistance) > movementDistanceGateValue)
			{
				base.NPC.SimpleFlyMovement(base.NPC.DirectionTo(targetCenter + etherealLanceDistance).SafeNormalize(Vector2.Zero) * velocity, acceleration);
			}
			int lanceRotation = (death ? 10 : 8);
			if (base.NPC.ai[1] % (dayTimeEnrage ? 2f : 3f) == 0f && base.NPC.ai[1] < lanceGateValue)
			{
				int lanceAmount = ((!phase3) ? 1 : 2);
				for (int j = 0; j < lanceAmount; j++)
				{
					int lanceFrequency = (int)(base.NPC.ai[1] / (dayTimeEnrage ? 2f : 3f));
					lanceRotation += (death ? 5 : 4) * j;
					Vector2 unitX = Vector2.UnitX;
					double radians = (float)Math.PI / (float)(lanceRotation * 2) + (float)lanceFrequency * ((float)Math.PI / (float)lanceRotation);
					val = default(Vector2);
					Vector2 lanceDirection = unitX.RotatedBy(radians, val);
					if (calamityGlobalNPC.newAI[3] == 0f)
					{
						lanceDirection.X += ((lanceDirection.X > 0f) ? 0.5f : (-0.5f));
					}
					lanceDirection = lanceDirection.SafeNormalize(Vector2.UnitY);
					float spawnDistance = 600f;
					Vector2 playerCenter = targetData10.Center;
					if (base.NPC.Distance(playerCenter) > 2400f)
					{
						continue;
					}
					if (Vector2.Dot(targetData10.Velocity.SafeNormalize(Vector2.UnitY), lanceDirection) > 0f)
					{
						lanceDirection *= -1f;
					}
					Vector2 val2 = playerCenter + targetData10.Velocity * 90f;
					Vector2 spawnLocation = playerCenter + lanceDirection * spawnDistance - targetData10.Velocity * 30f;
					if (spawnLocation.Distance(playerCenter) < spawnDistance)
					{
						Vector2 lanceSpawnDirection = playerCenter - spawnLocation;
						if (lanceSpawnDirection == Vector2.Zero)
						{
							lanceSpawnDirection = lanceDirection;
						}
						spawnLocation = playerCenter - lanceSpawnDirection.SafeNormalize(Vector2.UnitY) * spawnDistance;
					}
					int projectileType = 919;
					int projectileDamage = lanceDamage;
					Vector2 v3 = val2 - spawnLocation;
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnLocation, Vector2.Zero, projectileType, projectileDamage, 0f, Main.myPlayer, v3.ToRotation(), base.NPC.ai[1] / lanceGateValue);
					}
					if (Main.netMode == 1)
					{
						continue;
					}
					for (int k = 0; k < 255; k++)
					{
						if (!base.NPC.Boss_CanShootExtraAt(k, lanceFrequency % 3, 3, 2400f))
						{
							continue;
						}
						Player extraPlayer = Main.player[k];
						playerCenter = extraPlayer.Center;
						if (Vector2.Dot(extraPlayer.velocity.SafeNormalize(Vector2.UnitY), lanceDirection) > 0f)
						{
							lanceDirection *= -1f;
						}
						Vector2 val3 = playerCenter + extraPlayer.velocity * 90f;
						spawnLocation = playerCenter + lanceDirection * spawnDistance - extraPlayer.velocity * 30f;
						if (spawnLocation.Distance(playerCenter) < spawnDistance)
						{
							Vector2 extraPlayerSpawnDirection = playerCenter - spawnLocation;
							if (extraPlayerSpawnDirection == Vector2.Zero)
							{
								extraPlayerSpawnDirection = lanceDirection;
							}
							spawnLocation = playerCenter - extraPlayerSpawnDirection.SafeNormalize(Vector2.UnitY) * spawnDistance;
						}
						v3 = val3 - spawnLocation;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnLocation, Vector2.Zero, projectileType, projectileDamage, 0f, Main.myPlayer, v3.ToRotation(), base.NPC.ai[1] / lanceGateValue);
					}
				}
			}
			base.NPC.ai[1]++;
			float extraPhaseTime = (dayTimeEnrage ? 24f : 48f) + 20f * lessTimeSpentPerPhaseMultiplier;
			if (base.NPC.ai[1] >= lanceGateValue + extraPhaseTime)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				base.NPC.netUpdate = true;
				base.NPC.SyncExtraAI();
			}
			break;
		}
		case 5:
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item163, base.NPC.Center);
			}
			Vector2 magicSpawnOffset = default(Vector2);
			((Vector2)(ref magicSpawnOffset))._002Ector(55f, -30f);
			Vector2 everlastingRainbowSpawn = base.NPC.Center + magicSpawnOffset;
			if (base.NPC.ai[1] < 42f)
			{
				AI_120_HallowBoss_DoMagicEffect(base.NPC.Center + magicSpawnOffset, 3, Utils.GetLerpValue(0f, 42f, base.NPC.ai[1], clamped: true), base.NPC);
			}
			NPCAimedTarget targetData13 = base.NPC.GetTargetData();
			Vector2 targetCenter = (targetData13.Invalid ? base.NPC.Center : targetData13.Center);
			if (base.NPC.Distance(targetCenter + everlastingRainbowDistance) > movementDistanceGateValue)
			{
				base.NPC.SimpleFlyMovement(base.NPC.DirectionTo(targetCenter + everlastingRainbowDistance).SafeNormalize(Vector2.Zero) * velocity * 0.5f, acceleration * 0.75f);
			}
			if (base.NPC.ai[1] % 42f == 0f && base.NPC.ai[1] < 42f)
			{
				float projRotation2 = (float)Math.PI * 2f * Main.rand.NextFloat();
				float totalProjectiles = (Main.getGoodWorld ? 30f : ((!death) ? (dayTimeEnrage ? 18f : 13f) : (dayTimeEnrage ? 22f : 15f)));
				int projIndex = 0;
				bool inversePhase2SpreadPattern = Main.rand.NextBool();
				for (float i2 = 0f; i2 < 1f; i2 += 1f / totalProjectiles)
				{
					int projectileType4 = 872;
					int projectileDamage4 = rainbowDamage;
					int projectileType5 = 873;
					int projectileDamage5 = boltDamage;
					float projRotationMultiplier = i2;
					Vector2 unitY = Vector2.UnitY;
					double radians4 = (float)Math.PI / 2f + (float)Math.PI * 2f * projRotationMultiplier + projRotation2;
					val = default(Vector2);
					Vector2 spinningpoint3 = unitY.RotatedBy(radians4, val);
					float initialVelocity = (death ? 2f : 1.75f);
					if (dayTimeEnrage && projIndex % 2 == 0)
					{
						initialVelocity *= 2f;
					}
					if (Main.getGoodWorld)
					{
						initialVelocity *= 1.5f;
					}
					if (phase2)
					{
						float maxAddedVelocity = initialVelocity;
						float addedVelocity = (inversePhase2SpreadPattern ? Math.Abs(maxAddedVelocity - Math.Abs(MathHelper.Lerp(0f - maxAddedVelocity, maxAddedVelocity, Math.Abs(i2 - 0.5f) * 2f))) : Math.Abs(MathHelper.Lerp(0f - maxAddedVelocity, maxAddedVelocity, Math.Abs(i2 - 0.5f) * 2f)));
						initialVelocity += addedVelocity;
					}
					if (Main.netMode != 1)
					{
						IEntitySource source_FromAI = base.NPC.GetSource_FromAI();
						val = default(Vector2);
						Projectile.NewProjectile(source_FromAI, everlastingRainbowSpawn + spinningpoint3.RotatedBy(-1.5707963705062866, val) * 30f, spinningpoint3 * initialVelocity, projectileType4, projectileDamage4, 0f, Main.myPlayer, 0f, projRotationMultiplier);
						if (phase3 && Main.netMode != 1)
						{
							IEntitySource source_FromAI2 = base.NPC.GetSource_FromAI();
							val = default(Vector2);
							Projectile.NewProjectile(source_FromAI2, everlastingRainbowSpawn + spinningpoint3.RotatedBy(-1.5707963705062866, val) * 30f, spinningpoint3 * (death ? 3f : 2f) * initialVelocity, projectileType5, projectileDamage5, 0f, Main.myPlayer, base.NPC.target, projRotationMultiplier);
						}
					}
					projIndex++;
				}
			}
			base.NPC.ai[1]++;
			float extraPhaseTime = (dayTimeEnrage ? 36f : 72f) + 30f * lessTimeSpentPerPhaseMultiplier;
			if (base.NPC.ai[1] >= 72f + extraPhaseTime)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 6:
		{
			base.NPC.damage = 0;
			calamityGlobalNPC.DR = (shouldBeInPhase2ButIsStillInPhase1 ? 0.99f : 0.575f);
			int num = (phase2 ? 2 : 3);
			float sunDanceGateValue = (dayTimeEnrage ? 35f : (death ? 40f : 50f));
			float totalSunDancePhaseTime = (float)num * sunDanceGateValue;
			Vector2 sunDanceHoverOffset = default(Vector2);
			((Vector2)(ref sunDanceHoverOffset))._002Ector(0f, -100f);
			Vector2 position = base.NPC.Center + sunDanceHoverOffset;
			NPCAimedTarget targetData11 = base.NPC.GetTargetData();
			Vector2 targetCenter = (targetData11.Invalid ? base.NPC.Center : targetData11.Center);
			if (base.NPC.Distance(targetCenter + sunDanceDistance) > movementDistanceGateValue)
			{
				base.NPC.SimpleFlyMovement(base.NPC.DirectionTo(targetCenter + sunDanceDistance).SafeNormalize(Vector2.Zero) * velocity * 0.3f, acceleration * 0.7f);
			}
			if (base.NPC.ai[1] % sunDanceGateValue == 0f && base.NPC.ai[1] < totalSunDancePhaseTime)
			{
				int projectileType2 = 923;
				int projectileDamage2 = sunDanceDamage;
				int sunDanceExtension = (int)(base.NPC.ai[1] / sunDanceGateValue);
				int targetFloatDirection = ((targetData11.Center.X > base.NPC.Center.X) ? 1 : 0);
				float projAmount = (phase2 ? 8f : 6f);
				float projRotation = 1f / projAmount;
				for (float j2 = 0f; j2 < 1f; j2 += projRotation)
				{
					float projDirection = (j2 + projRotation * 0.5f + (float)sunDanceExtension * projRotation * 0.5f) % 1f;
					float ai = (float)Math.PI * 2f * (projDirection + (float)targetFloatDirection);
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), position, Vector2.Zero, projectileType2, projectileDamage2, 0f, Main.myPlayer, ai, base.NPC.whoAmI);
					}
				}
			}
			base.NPC.ai[1]++;
			float extraPhaseTime = ((!dayTimeEnrage) ? (death ? 140f : 150f) : (death ? 105f : 110f)) + 30f * lessTimeSpentPerPhaseMultiplier;
			if (base.NPC.ai[1] >= totalSunDancePhaseTime + extraPhaseTime)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 7:
		{
			base.NPC.damage = 0;
			bool expertAttack = calamityGlobalNPC.newAI[2] == 0f;
			int numLanceWalls = (expertAttack ? 6 : 4);
			float lanceWallSpawnGateValue = ((!expertAttack) ? 54f : (death ? 42f : 36f));
			if (dayTimeEnrage)
			{
				lanceWallSpawnGateValue -= (expertAttack ? 4f : 6f);
			}
			float lanceWallPhaseTime = lanceWallSpawnGateValue * (float)numLanceWalls;
			NPCAimedTarget targetData15 = base.NPC.GetTargetData();
			Vector2 destination = (targetData15.Invalid ? base.NPC.Center : targetData15.Center);
			if (base.NPC.Distance(destination + etherealLanceDistance) > movementDistanceGateValue)
			{
				base.NPC.SimpleFlyMovement(base.NPC.DirectionTo(destination + etherealLanceDistance).SafeNormalize(Vector2.Zero) * velocity * 0.4f, acceleration);
			}
			if ((float)(int)base.NPC.ai[1] % lanceWallSpawnGateValue == 0f && base.NPC.ai[1] < lanceWallPhaseTime)
			{
				SoundEngine.PlaySound(in SoundID.Item162, base.NPC.Center);
				float totalProjectiles2 = (death ? 18f : 15f);
				float lanceSpacing = (death ? 150f : 175f);
				float lanceWallSize = totalProjectiles2 * lanceSpacing;
				Vector2 lanceSpawnOffset = targetData15.Center;
				if (base.NPC.Distance(lanceSpawnOffset) <= 3200f)
				{
					Vector2 lanceWallStartingPosition = Vector2.Zero;
					Vector2 lanceWallDirection = Vector2.UnitY;
					float lanceWallConvergence = 0.4f;
					float lanceWallSizeMult = 1.4f;
					totalProjectiles2 += 5f;
					lanceSpacing += 50f;
					lanceWallSize *= (death ? 0.7f : 0.5f);
					float direction = 1f;
					int randomLanceWallType;
					do
					{
						randomLanceWallType = Main.rand.Next(numLanceWalls);
					}
					while ((float)randomLanceWallType == calamityGlobalNPC.newAI[3]);
					calamityGlobalNPC.newAI[3] = randomLanceWallType;
					calamityGlobalNPC.newAI[1]++;
					base.NPC.SyncExtraAI();
					switch (randomLanceWallType)
					{
					case 0:
						lanceSpawnOffset += new Vector2((0f - lanceWallSize) / 2f, 0f) * direction;
						((Vector2)(ref lanceWallStartingPosition))._002Ector(0f, lanceWallSize);
						lanceWallDirection = Vector2.UnitX;
						break;
					case 1:
						lanceSpawnOffset += new Vector2(lanceWallSize / 2f, lanceSpacing / 2f) * direction;
						((Vector2)(ref lanceWallStartingPosition))._002Ector(0f, lanceWallSize);
						lanceWallDirection = -Vector2.UnitX;
						break;
					case 2:
						lanceSpawnOffset += new Vector2(0f - lanceWallSize, 0f - lanceWallSize) * lanceWallConvergence * direction;
						((Vector2)(ref lanceWallStartingPosition))._002Ector(lanceWallSize * lanceWallSizeMult, 0f);
						((Vector2)(ref lanceWallDirection))._002Ector(1f, 1f);
						break;
					case 3:
						lanceSpawnOffset += new Vector2(lanceWallSize * lanceWallConvergence + lanceSpacing / 2f, (0f - lanceWallSize) * lanceWallConvergence) * direction;
						((Vector2)(ref lanceWallStartingPosition))._002Ector((0f - lanceWallSize) * lanceWallSizeMult, 0f);
						((Vector2)(ref lanceWallDirection))._002Ector(-1f, 1f);
						break;
					case 4:
						lanceSpawnOffset += new Vector2(0f - lanceWallSize, lanceWallSize) * lanceWallConvergence * direction;
						((Vector2)(ref lanceWallStartingPosition))._002Ector(lanceWallSize * lanceWallSizeMult, 0f);
						lanceWallDirection = lanceSpawnOffset.DirectionTo(targetData15.Center);
						break;
					case 5:
						lanceSpawnOffset += new Vector2(lanceWallSize * lanceWallConvergence + lanceSpacing / 2f, lanceWallSize * lanceWallConvergence) * direction;
						((Vector2)(ref lanceWallStartingPosition))._002Ector((0f - lanceWallSize) * lanceWallSizeMult, 0f);
						lanceWallDirection = lanceSpawnOffset.DirectionTo(targetData15.Center);
						break;
					}
					int projectileType7 = 919;
					int projectileDamage7 = lanceDamage;
					for (float i3 = 0f; i3 <= 1f; i3 += 1f / totalProjectiles2)
					{
						Vector2 spawnLocation2 = lanceSpawnOffset + lanceWallStartingPosition * (i3 - 0.5f) * (expertAttack ? 1f : 2f);
						Vector2 v5 = lanceWallDirection;
						if (expertAttack)
						{
							Vector2 lanceWallSpawnPredictiveness = targetData15.Velocity * 20f * i3;
							Vector2 lanceWallSpawnLocation = spawnLocation2.DirectionTo(targetData15.Center + lanceWallSpawnPredictiveness);
							v5 = Vector2.Lerp(lanceWallDirection, lanceWallSpawnLocation, 0.75f).SafeNormalize(Vector2.UnitY);
						}
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnLocation2, Vector2.Zero, projectileType7, projectileDamage7, 0f, Main.myPlayer, v5.ToRotation(), i3);
						}
					}
				}
				if (Main.rand.NextBool(5 - ((int)calamityGlobalNPC.newAI[1] - 2)) && calamityGlobalNPC.newAI[1] >= 2f)
				{
					base.NPC.ai[1] = lanceWallPhaseTime;
					base.NPC.netUpdate = true;
				}
			}
			base.NPC.ai[1]++;
			float extraPhaseTime = (dayTimeEnrage ? 24f : 48f) + 20f * lessTimeSpentPerPhaseMultiplier;
			if (base.NPC.ai[1] >= lanceWallPhaseTime + extraPhaseTime)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				base.NPC.SyncExtraAI();
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 8:
		case 9:
		{
			int chargeDirection = ((base.NPC.ai[0] != 8f) ? 1 : (-1));
			AI_120_HallowBoss_DoMagicEffect(base.NPC.Center, 5, Utils.GetLerpValue(40f, 90f, base.NPC.ai[1], clamped: true), base.NPC);
			float chargeGateValue = 40f;
			float playChargeSoundTime = 20f;
			float chargeDuration = (phase3 ? 40f : 50f);
			float totalPhaseTime = chargeGateValue + chargeDuration;
			float chargeStartDistance = (phase3 ? 1000f : 800f);
			float chargeVelocity = (phase3 ? 100f : 70f);
			float chargeAcceleration = (phase3 ? 0.1f : 0.07f);
			if (base.NPC.ai[1] <= chargeGateValue)
			{
				base.NPC.damage = 0;
				if (base.NPC.ai[1] == playChargeSoundTime)
				{
					SoundEngine.PlaySound(in SoundID.Item160, base.NPC.Center);
				}
				NPCAimedTarget targetData16 = base.NPC.GetTargetData();
				Vector2 destination = (targetData16.Invalid ? base.NPC.Center : targetData16.Center) + new Vector2((float)chargeDirection * (0f - chargeStartDistance), 0f);
				base.NPC.SimpleFlyMovement(base.NPC.DirectionTo(destination).SafeNormalize(Vector2.Zero) * velocity, acceleration * 2f);
				if (base.NPC.ai[1] == chargeGateValue)
				{
					NPC nPC4 = base.NPC;
					nPC4.velocity *= 0.3f;
				}
			}
			else if (base.NPC.ai[1] <= chargeGateValue + chargeDuration)
			{
				if (base.NPC.ai[1] == chargeGateValue + 1f)
				{
					SoundEngine.PlaySound(in SoundID.Item164, base.NPC.Center);
				}
				float rainbowStreakGateValue = 2f;
				if ((base.NPC.ai[1] - 1f) % rainbowStreakGateValue == 0f)
				{
					int projectileType8 = 873;
					int projectileDamage8 = boltDamage;
					float ai4 = (base.NPC.ai[1] - chargeGateValue - 1f) / chargeDuration;
					Vector2 spinningpoint4 = new Vector2(0f, death ? (-5f) : (-4f));
					double radians5 = (float)Math.PI / 2f * Main.rand.NextFloatDirection();
					val = default(Vector2);
					Vector2 rainbowStreakVelocity2 = Utils.RotatedBy(spinningpoint4, radians5, val);
					if (phase2)
					{
						Vector2 spinningpoint5 = new Vector2(0f, death ? (-6f) : (-5f));
						double radians6 = (float)Math.PI * 2f * Main.rand.NextFloat();
						val = default(Vector2);
						rainbowStreakVelocity2 = Utils.RotatedBy(spinningpoint5, radians6, val);
					}
					rainbowStreakVelocity2.X *= 2f;
					if (!phase2)
					{
						rainbowStreakVelocity2.Y *= 0.5f;
					}
					if (dayTimeEnrage)
					{
						rainbowStreakVelocity2 *= MathHelper.Lerp(0.8f, 1.6f, ai4);
					}
					if (Main.netMode != 1)
					{
						int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, rainbowStreakVelocity2, projectileType8, projectileDamage8, 0f, Main.myPlayer, base.NPC.target, ai4);
						if (Main.getGoodWorld)
						{
							Main.projectile[proj].extraUpdates++;
						}
					}
					if (Main.netMode != 1)
					{
						int multiplayerStreakSpawnFrequency2 = (int)((base.NPC.ai[1] - chargeGateValue - 1f) / rainbowStreakGateValue);
						for (int num2 = 0; num2 < 255; num2++)
						{
							if (base.NPC.Boss_CanShootExtraAt(num2, multiplayerStreakSpawnFrequency2 % 3, 3, 2400f))
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, rainbowStreakVelocity2, projectileType8, projectileDamage8, 0f, Main.myPlayer, num2, ai4);
							}
						}
					}
				}
				NPC nPC5 = base.NPC;
				((Vector2)(ref val))._002Ector((float)chargeDirection * chargeVelocity, 0f);
				nPC5.velocity = Vector2.Lerp(base.NPC.velocity, val, chargeAcceleration);
				if (base.NPC.ai[1] == chargeGateValue + chargeDuration)
				{
					NPC nPC6 = base.NPC;
					nPC6.velocity *= 0.45f;
				}
				base.NPC.damage = (int)Math.Round((float)ContactDamageCorrection.CalculateDamageForEnrage() * DashDamageMult);
			}
			else
			{
				base.NPC.damage = 0;
				NPC nPC7 = base.NPC;
				nPC7.velocity *= 0.92f;
			}
			base.NPC.ai[1]++;
			float extraPhaseTime = (dayTimeEnrage ? 60f : 120f) * ((lessTimeSpentPerPhaseMultiplier < 1f) ? (lessTimeSpentPerPhaseMultiplier * 1.5f) : lessTimeSpentPerPhaseMultiplier);
			if (base.NPC.ai[1] >= totalPhaseTime && base.NPC.ai[1] <= totalPhaseTime + 10f)
			{
				Vector2 center = base.NPC.GetTargetData().Center;
				center += new Vector2(0f, -200f);
				if (base.NPC.Distance(center) > 200f)
				{
					center -= base.NPC.DirectionTo(center) * 100f;
				}
				Vector2 targetDirection = center - base.NPC.Center;
				float lerpValue = Utils.GetLerpValue(100f, 600f, ((Vector2)(ref targetDirection)).Length());
				float targetDistance = ((Vector2)(ref targetDirection)).Length();
				float maxVelocity = (death ? 24f : 21f);
				if (targetDistance > maxVelocity)
				{
					targetDistance = maxVelocity;
				}
				base.NPC.velocity = Vector2.Lerp(targetDirection.SafeNormalize(Vector2.Zero) * targetDistance, targetDirection / 6f, lerpValue);
				base.NPC.netUpdate = true;
			}
			if (base.NPC.ai[1] >= totalPhaseTime + extraPhaseTime)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 10:
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item161, base.NPC.Center);
			}
			takeDamage = !(base.NPC.ai[1] >= 30f) || !(base.NPC.ai[1] <= 170f);
			NPC nPC2 = base.NPC;
			nPC2.velocity *= 0.95f;
			if (base.NPC.ai[1] == 90f)
			{
				if (base.NPC.ai[3] == 0f)
				{
					base.NPC.ai[3] = 1f;
				}
				if (base.NPC.ai[3] == 2f)
				{
					base.NPC.ai[3] = 3f;
				}
				base.NPC.Center = base.NPC.GetTargetData().Center + new Vector2(0f, -250f);
				base.NPC.netUpdate = true;
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 180f)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 11:
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item162, base.NPC.Center);
			}
			float lanceGateValue2 = (death ? 75f : 100f);
			if (base.NPC.ai[1] >= 6f && base.NPC.ai[1] < 54f)
			{
				AI_120_HallowBoss_DoMagicEffect(base.NPC.Center + new Vector2(-55f, -20f), 2, Utils.GetLerpValue(0f, lanceGateValue2, base.NPC.ai[1], clamped: true), base.NPC);
				AI_120_HallowBoss_DoMagicEffect(base.NPC.Center + new Vector2(55f, -20f), 4, Utils.GetLerpValue(0f, lanceGateValue2, base.NPC.ai[1], clamped: true), base.NPC);
			}
			NPCAimedTarget targetData14 = base.NPC.GetTargetData();
			Vector2 targetCenter = (targetData14.Invalid ? base.NPC.Center : targetData14.Center);
			if (base.NPC.Distance(targetCenter + etherealLanceDistance) > movementDistanceGateValue)
			{
				base.NPC.SimpleFlyMovement(base.NPC.DirectionTo(targetCenter + etherealLanceDistance).SafeNormalize(Vector2.Zero) * velocity, acceleration);
			}
			float etherealLanceGateValue = (death ? 5f : 6f);
			if (dayTimeEnrage)
			{
				etherealLanceGateValue--;
			}
			if (base.NPC.ai[1] % etherealLanceGateValue == 0f && base.NPC.ai[1] < lanceGateValue2)
			{
				int numLances = (phase3 ? 4 : 3);
				for (int m = 0; m < numLances; m++)
				{
					bool oppositeLance = m % 2 == 0;
					Vector2 inverseTargetVel = (oppositeLance ? targetData14.Velocity : (-targetData14.Velocity));
					inverseTargetVel.SafeNormalize(-Vector2.UnitY);
					float spawnDistance2 = 100f + (float)m * 100f;
					targetCenter = targetData14.Center;
					if (base.NPC.Distance(targetCenter) > 2400f)
					{
						continue;
					}
					Vector2 val4 = targetCenter + (oppositeLance ? (-targetData14.Velocity) : targetData14.Velocity) * 90f;
					Vector2 straightLanceSpawnDirection = targetCenter + inverseTargetVel * spawnDistance2;
					if (straightLanceSpawnDirection.Distance(targetCenter) < spawnDistance2)
					{
						Vector2 straightLanceSpawnLocation = targetCenter - straightLanceSpawnDirection;
						if (straightLanceSpawnLocation == Vector2.Zero)
						{
							straightLanceSpawnLocation = inverseTargetVel;
						}
						straightLanceSpawnDirection = targetCenter - straightLanceSpawnLocation.SafeNormalize(Vector2.UnitY) * spawnDistance2;
					}
					int projectileType6 = 919;
					int projectileDamage6 = lanceDamage;
					Vector2 v4 = val4 - straightLanceSpawnDirection;
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), straightLanceSpawnDirection, Vector2.Zero, projectileType6, projectileDamage6, 0f, Main.myPlayer, v4.ToRotation(), base.NPC.ai[1] / lanceGateValue2);
					}
					if (Main.netMode == 1)
					{
						continue;
					}
					int multiplayerExtraStraightLances = (int)(base.NPC.ai[1] / etherealLanceGateValue);
					for (int n = 0; n < 255; n++)
					{
						if (!base.NPC.Boss_CanShootExtraAt(n, multiplayerExtraStraightLances % 3, 3, 2400f))
						{
							continue;
						}
						Player player = Main.player[n];
						inverseTargetVel = (oppositeLance ? player.velocity : (-player.velocity));
						inverseTargetVel.SafeNormalize(-Vector2.UnitY);
						targetCenter = player.Center;
						Vector2 val5 = targetCenter + (oppositeLance ? (-player.velocity) : player.velocity) * 90f;
						straightLanceSpawnDirection = targetCenter + inverseTargetVel * spawnDistance2;
						if (straightLanceSpawnDirection.Distance(targetCenter) < spawnDistance2)
						{
							Vector2 extraPlayerLanceSpawnLocation = targetCenter - straightLanceSpawnDirection;
							if (extraPlayerLanceSpawnLocation == Vector2.Zero)
							{
								extraPlayerLanceSpawnLocation = inverseTargetVel;
							}
							straightLanceSpawnDirection = targetCenter - extraPlayerLanceSpawnLocation.SafeNormalize(Vector2.UnitY) * spawnDistance2;
						}
						v4 = val5 - straightLanceSpawnDirection;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), straightLanceSpawnDirection, Vector2.Zero, projectileType6, projectileDamage6, 0f, Main.myPlayer, v4.ToRotation(), base.NPC.ai[1] / lanceGateValue2);
					}
				}
			}
			base.NPC.ai[1]++;
			float extraPhaseTime = (dayTimeEnrage ? 24f : 48f) * lessTimeSpentPerPhaseMultiplier;
			if (base.NPC.ai[1] >= lanceGateValue2 + extraPhaseTime)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 12:
		{
			base.NPC.damage = 0;
			Vector2 projRandomOffset = default(Vector2);
			((Vector2)(ref projRandomOffset))._002Ector(-55f, -30f);
			if (base.NPC.ai[1] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item165, base.NPC.Center);
				base.NPC.velocity = new Vector2(0f, -12f);
			}
			NPC nPC11 = base.NPC;
			nPC11.velocity *= 0.95f;
			bool shouldSpawnStreaks = base.NPC.ai[1] < 60f && base.NPC.ai[1] >= 10f;
			if (shouldSpawnStreaks)
			{
				AI_120_HallowBoss_DoMagicEffect(base.NPC.Center + projRandomOffset, 1, Utils.GetLerpValue(0f, 60f, base.NPC.ai[1], clamped: true), base.NPC);
			}
			int stationaryStreakSpawnFrequency = 4;
			if (dayTimeEnrage)
			{
				stationaryStreakSpawnFrequency--;
			}
			if (phase3)
			{
				stationaryStreakSpawnFrequency *= 2;
			}
			float streakHomeTime = (base.NPC.ai[1] - 10f) / 50f;
			if (((int)base.NPC.ai[1] % stationaryStreakSpawnFrequency == 0) & shouldSpawnStreaks)
			{
				int projectileType9 = 873;
				int projectileDamage9 = boltDamage;
				Vector2 spinningpoint7 = new Vector2(0f, (death ? (-24f) : (-22f)) - (phase3 ? ((death ? 6f : 4f) * streakHomeTime) : 0f));
				double radians8 = (float)Math.PI * 2f * streakHomeTime;
				val = default(Vector2);
				Vector2 vector = Utils.RotatedBy(spinningpoint7, radians8, val);
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + projRandomOffset, vector, projectileType9, projectileDamage9, 0f, Main.myPlayer, base.NPC.target, streakHomeTime);
					if (phase3)
					{
						int proj2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + projRandomOffset, -vector, projectileType9, projectileDamage9, 0f, Main.myPlayer, base.NPC.target, 1f - streakHomeTime);
						if (Main.getGoodWorld)
						{
							Main.projectile[proj2].extraUpdates++;
							Main.projectile[proj2].netUpdate = true;
						}
					}
				}
				if (Main.netMode != 1)
				{
					int extraStationaryStreakSpawnFrequency = (int)(base.NPC.ai[1] % (float)stationaryStreakSpawnFrequency);
					for (int num3 = 0; num3 < 255; num3++)
					{
						if (base.NPC.Boss_CanShootExtraAt(num3, extraStationaryStreakSpawnFrequency % 3, 3, 2400f))
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + projRandomOffset, vector, projectileType9, projectileDamage9, 0f, Main.myPlayer, num3, streakHomeTime);
						}
					}
				}
			}
			base.NPC.ai[1]++;
			float extraPhaseTime = (dayTimeEnrage ? 36f : 72f) + 30f * lessTimeSpentPerPhaseMultiplier;
			if (base.NPC.ai[1] >= (death ? 105f : 120f) + extraPhaseTime)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 13:
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item165, base.NPC.Center);
				base.NPC.velocity = new Vector2(0f, -7f);
			}
			NPC nPC = base.NPC;
			nPC.velocity *= 0.95f;
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			NPCAimedTarget targetData = base.NPC.GetTargetData();
			visible = false;
			bool trueDespawnFlag = false;
			bool shouldDespawn = false;
			if (!trueDespawnFlag)
			{
				if (base.NPC.AI_120_HallowBoss_IsGenuinelyEnraged() && !BossRushEvent.BossRushActive)
				{
					if (!Main.dayTime)
					{
						shouldDespawn = true;
					}
					if (Main.dayTime && Main.time >= 53400.0)
					{
						shouldDespawn = true;
					}
				}
				trueDespawnFlag |= shouldDespawn;
			}
			if (!trueDespawnFlag)
			{
				bool hasNoTarget = targetData.Invalid || base.NPC.Distance(targetData.Center) > despawnDistanceGateValue;
				trueDespawnFlag |= hasNoTarget;
			}
			base.NPC.alpha = Utils.Clamp(base.NPC.alpha + trueDespawnFlag.ToDirectionInt() * 5, 0, 255);
			bool alphaExtreme = base.NPC.alpha == 0 || base.NPC.alpha == 255;
			int despawnDustAmt = 5;
			for (int i = 0; i < despawnDustAmt; i++)
			{
				float despawnDustOpacity = MathHelper.Lerp(1.3f, 0.7f, base.NPC.Opacity);
				Color newColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f);
				int despawnRainbowDust = Dust.NewDust(base.NPC.position - base.NPC.Size * 0.5f, base.NPC.width * 2, base.NPC.height * 2, 267, 0f, 0f, 0, newColor);
				Main.dust[despawnRainbowDust].position = base.NPC.Center + Main.rand.NextVector2Circular(base.NPC.width, base.NPC.height);
				Dust obj = Main.dust[despawnRainbowDust];
				obj.velocity *= Main.rand.NextFloat() * 0.8f;
				Main.dust[despawnRainbowDust].noGravity = true;
				Main.dust[despawnRainbowDust].scale = 0.9f + Main.rand.NextFloat() * 1.2f;
				Main.dust[despawnRainbowDust].fadeIn = 0.4f + Main.rand.NextFloat() * 1.2f * despawnDustOpacity;
				Dust obj2 = Main.dust[despawnRainbowDust];
				obj2.velocity += Vector2.UnitY * -2f;
				Main.dust[despawnRainbowDust].scale = 0.35f;
				if (despawnRainbowDust != 6000)
				{
					Dust dust = DustExtensions.BetterCloneDust(despawnRainbowDust);
					dust.scale /= 2f;
					dust.fadeIn *= 0.85f;
					dust.color = new Color(255, 255, 255, 255);
				}
			}
			base.NPC.ai[1]++;
			if (!((base.NPC.ai[1] >= 20f) & alphaExtreme))
			{
				break;
			}
			if (base.NPC.alpha == 255)
			{
				base.NPC.active = false;
				if (Main.netMode != 1)
				{
					NetMessage.SendData(23, -1, -1, null, base.NPC.whoAmI);
				}
				return false;
			}
			base.NPC.ai[0] = 1f;
			base.NPC.ai[1] = 0f;
			base.NPC.netUpdate = true;
			break;
		}
		}
		base.NPC.dontTakeDamage = !takeDamage;
		if (phase3)
		{
			base.NPC.defense = (int)Math.Round((double)base.NPC.defDefense * 0.8);
		}
		else if (phase2)
		{
			base.NPC.defense = (int)Math.Round((double)base.NPC.defDefense * 1.2);
		}
		else
		{
			base.NPC.defense = base.NPC.defDefense;
		}
		if (++base.NPC.localAI[0] >= 44f)
		{
			base.NPC.localAI[0] = 0f;
		}
		if (visible)
		{
			base.NPC.alpha = Utils.Clamp(base.NPC.alpha - 5, 0, 255);
		}
		Lighting.AddLight(base.NPC.Center, Vector3.One * base.NPC.Opacity);
		return false;
	}

	private static void CreateSpawnDust(NPC npc, bool useAI = true)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		int spawnDustAmount = 2;
		float timer = (useAI ? npc.ai[1] : npc.Calamity().newAI[0]);
		float spawnTime = 180f;
		for (int i = 0; i < spawnDustAmount; i++)
		{
			float fadeInScalar = MathHelper.Lerp(1.3f, 0.7f, npc.Opacity) * Utils.GetLerpValue(0f, 120f, timer, clamped: true);
			Color newColor = Main.hslToRgb(timer / spawnTime, 1f, 0.5f);
			int dust = Dust.NewDust(npc.position, npc.width, npc.height, 267, 0f, 0f, 0, newColor);
			Main.dust[dust].position = npc.Center + Main.rand.NextVector2Circular((float)npc.width * 3f, (float)npc.height * 3f) + new Vector2(0f, -150f);
			Dust obj = Main.dust[dust];
			obj.velocity *= Main.rand.NextFloat() * 0.8f;
			Main.dust[dust].noGravity = true;
			Main.dust[dust].fadeIn = 0.6f + Main.rand.NextFloat() * 0.7f * fadeInScalar;
			Dust obj2 = Main.dust[dust];
			obj2.velocity += Vector2.UnitY * 3f;
			Main.dust[dust].scale = 0.35f;
			if (dust != 6000)
			{
				Dust dust2 = DustExtensions.BetterCloneDust(dust);
				dust2.scale /= 2f;
				dust2.fadeIn *= 0.85f;
				dust2.color = new Color(255, 255, 255, 255);
			}
		}
	}

	private static void AI_120_HallowBoss_DoMagicEffect(Vector2 spot, int effectType, float progress, NPC npc)
	{
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		float magicDustSpawnArea = 4f;
		float magicDustColorMult = 1f;
		float fadeIn = 0f;
		float magicDustPosChange = 0.5f;
		int magicAmt = 2;
		int magicDustType = 267;
		switch (effectType)
		{
		case 1:
			magicDustColorMult = 0.5f;
			fadeIn = 2f;
			magicDustPosChange = 0f;
			break;
		case 2:
		case 4:
			magicDustSpawnArea = 50f;
			magicDustColorMult = 0.5f;
			fadeIn = 0f;
			magicDustPosChange = 0f;
			magicAmt = 4;
			break;
		case 3:
			magicDustSpawnArea = 30f;
			magicDustColorMult = 0.1f;
			fadeIn = 2.5f;
			magicDustPosChange = 0f;
			break;
		case 5:
			if (progress == 0f)
			{
				magicAmt = 0;
			}
			else
			{
				magicAmt = 5;
				magicDustType = Main.rand.Next(86, 92);
			}
			if (progress >= 1f)
			{
				magicAmt = 0;
			}
			break;
		}
		for (int i = 0; i < magicAmt; i++)
		{
			Dust dust = Dust.NewDustPerfect(spot, magicDustType, Main.rand.NextVector2CircularEdge(magicDustSpawnArea, magicDustSpawnArea) * (Main.rand.NextFloat() * (1f - magicDustPosChange) + magicDustPosChange), 0, Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f), (Main.rand.NextFloat() * 2f + 2f) * magicDustColorMult);
			dust.fadeIn = fadeIn;
			dust.noGravity = true;
			switch (effectType)
			{
			case 2:
			case 4:
			{
				dust.velocity *= 0.005f;
				dust.scale = 3f * Utils.GetLerpValue(0.7f, 0f, progress, clamped: true) * Utils.GetLerpValue(0f, 0.3f, progress, clamped: true);
				dust.velocity = ((float)Math.PI * 2f * ((float)i / 4f) + (float)Math.PI / 4f).ToRotationVector2() * 8f * Utils.GetLerpValue(1f, 0f, progress, clamped: true);
				dust.velocity += npc.velocity * 0.3f;
				float magicDustColorChange = 0f;
				if (effectType == 4)
				{
					magicDustColorChange = 0.5f;
				}
				dust.color = Main.hslToRgb(((float)i / 5f + magicDustColorChange + progress * 0.5f) % 1f, 1f, 0.5f);
				ref Color color = ref dust.color;
				((Color)(ref color)).A = (byte)(((Color)(ref color)).A / 2);
				dust.alpha = 127;
				break;
			}
			case 5:
				if (progress == 0f)
				{
					dust.customData = npc;
					dust.scale = 1.5f;
					dust.fadeIn = 0f;
					dust.velocity = new Vector2(0f, -1f) + Main.rand.NextVector2Circular(1f, 1f);
					dust.color = new Color(255, 255, 255, 80) * 0.3f;
				}
				else
				{
					dust.color = Main.hslToRgb(progress * 2f % 1f, 1f, 0.5f);
					dust.alpha = 0;
					dust.scale = 1f;
					dust.fadeIn = 1.3f;
					dust.velocity *= 3f;
					dust.velocity.X *= 0.1f;
					dust.velocity += npc.velocity * 1f;
				}
				break;
			}
		}
	}
}
