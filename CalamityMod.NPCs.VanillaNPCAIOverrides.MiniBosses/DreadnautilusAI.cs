using System;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.MiniBosses;

public class DreadnautilusAI : VanillaAIOverride
{
	public override bool AI(Mod mod)
	{
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1580: Unknown result type (might be due to invalid IL or missing references)
		//IL_158c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Unknown result type (might be due to invalid IL or missing references)
		//IL_0880: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1640: Unknown result type (might be due to invalid IL or missing references)
		//IL_1645: Unknown result type (might be due to invalid IL or missing references)
		//IL_164c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1651: Unknown result type (might be due to invalid IL or missing references)
		//IL_165c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1661: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1788: Unknown result type (might be due to invalid IL or missing references)
		//IL_1793: Unknown result type (might be due to invalid IL or missing references)
		//IL_1798: Unknown result type (might be due to invalid IL or missing references)
		//IL_179d: Unknown result type (might be due to invalid IL or missing references)
		//IL_170e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1718: Unknown result type (might be due to invalid IL or missing references)
		//IL_171d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1733: Unknown result type (might be due to invalid IL or missing references)
		//IL_1744: Unknown result type (might be due to invalid IL or missing references)
		//IL_175d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1762: Unknown result type (might be due to invalid IL or missing references)
		//IL_1767: Unknown result type (might be due to invalid IL or missing references)
		//IL_176d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1777: Unknown result type (might be due to invalid IL or missing references)
		//IL_177c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0979: Unknown result type (might be due to invalid IL or missing references)
		//IL_0983: Unknown result type (might be due to invalid IL or missing references)
		//IL_0988: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09df: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0961: Unknown result type (might be due to invalid IL or missing references)
		//IL_096c: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1100: Unknown result type (might be due to invalid IL or missing references)
		//IL_1105: Unknown result type (might be due to invalid IL or missing references)
		//IL_1111: Unknown result type (might be due to invalid IL or missing references)
		//IL_111b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1120: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10be: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1008: Unknown result type (might be due to invalid IL or missing references)
		//IL_100a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1011: Unknown result type (might be due to invalid IL or missing references)
		//IL_1016: Unknown result type (might be due to invalid IL or missing references)
		//IL_1020: Unknown result type (might be due to invalid IL or missing references)
		//IL_1025: Unknown result type (might be due to invalid IL or missing references)
		//IL_1042: Unknown result type (might be due to invalid IL or missing references)
		//IL_1063: Unknown result type (might be due to invalid IL or missing references)
		//IL_1068: Unknown result type (might be due to invalid IL or missing references)
		//IL_1079: Unknown result type (might be due to invalid IL or missing references)
		//IL_107e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1083: Unknown result type (might be due to invalid IL or missing references)
		//IL_109a: Unknown result type (might be due to invalid IL or missing references)
		//IL_109f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_082d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0838: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1521: Unknown result type (might be due to invalid IL or missing references)
		//IL_152c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1531: Unknown result type (might be due to invalid IL or missing references)
		//IL_1536: Unknown result type (might be due to invalid IL or missing references)
		//IL_12cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1168: Unknown result type (might be due to invalid IL or missing references)
		//IL_116a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1171: Unknown result type (might be due to invalid IL or missing references)
		//IL_1176: Unknown result type (might be due to invalid IL or missing references)
		//IL_1180: Unknown result type (might be due to invalid IL or missing references)
		//IL_1185: Unknown result type (might be due to invalid IL or missing references)
		//IL_119a: Unknown result type (might be due to invalid IL or missing references)
		//IL_11af: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1203: Unknown result type (might be due to invalid IL or missing references)
		//IL_1208: Unknown result type (might be due to invalid IL or missing references)
		//IL_120a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1211: Unknown result type (might be due to invalid IL or missing references)
		//IL_1216: Unknown result type (might be due to invalid IL or missing references)
		//IL_1220: Unknown result type (might be due to invalid IL or missing references)
		//IL_1225: Unknown result type (might be due to invalid IL or missing references)
		//IL_123b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1250: Unknown result type (might be due to invalid IL or missing references)
		//IL_1255: Unknown result type (might be due to invalid IL or missing references)
		//IL_1266: Unknown result type (might be due to invalid IL or missing references)
		//IL_126b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1270: Unknown result type (might be due to invalid IL or missing references)
		//IL_1287: Unknown result type (might be due to invalid IL or missing references)
		//IL_128c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1294: Unknown result type (might be due to invalid IL or missing references)
		//IL_1299: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c36: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1307: Unknown result type (might be due to invalid IL or missing references)
		//IL_130c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1321: Unknown result type (might be due to invalid IL or missing references)
		//IL_1336: Unknown result type (might be due to invalid IL or missing references)
		//IL_133b: Unknown result type (might be due to invalid IL or missing references)
		//IL_134c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1351: Unknown result type (might be due to invalid IL or missing references)
		//IL_1356: Unknown result type (might be due to invalid IL or missing references)
		//IL_1367: Unknown result type (might be due to invalid IL or missing references)
		//IL_136c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1374: Unknown result type (might be due to invalid IL or missing references)
		//IL_1379: Unknown result type (might be due to invalid IL or missing references)
		//IL_1380: Unknown result type (might be due to invalid IL or missing references)
		//IL_1385: Unknown result type (might be due to invalid IL or missing references)
		//IL_138a: Unknown result type (might be due to invalid IL or missing references)
		//IL_138f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1391: Unknown result type (might be due to invalid IL or missing references)
		//IL_1398: Unknown result type (might be due to invalid IL or missing references)
		//IL_139d: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_140e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1413: Unknown result type (might be due to invalid IL or missing references)
		//IL_141b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1420: Unknown result type (might be due to invalid IL or missing references)
		//IL_1427: Unknown result type (might be due to invalid IL or missing references)
		//IL_142c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1431: Unknown result type (might be due to invalid IL or missing references)
		//IL_146a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1471: Unknown result type (might be due to invalid IL or missing references)
		//IL_1476: Unknown result type (might be due to invalid IL or missing references)
		//IL_1494: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14da: Unknown result type (might be due to invalid IL or missing references)
		//IL_14dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_14de: Unknown result type (might be due to invalid IL or missing references)
		bool death = CalamityWorld.death;
		float goToAttackPositionAcceleration = (death ? 0.2f : 0.15f);
		float goToAttackPositionVelocity = (death ? 10f : 7.5f);
		float phaseSwitchPhaseTime = (death ? 30f : 60f);
		float dashChargeUpPhaseTime = 120f;
		float dashPhaseTime = (death ? 150f : 180f);
		float bloodSpitChargeUpPhaseTime = 90f;
		float bloodSpitPhaseTime = (death ? 120f : 90f);
		int numBloodSpitVolleys = (death ? 3 : 2);
		float bloodSquidPhaseTime = 180f;
		int maxBloodSquids = (death ? 3 : 2);
		if (base.NPC.localAI[0] == 0f)
		{
			base.NPC.localAI[0] = 1f;
			base.NPC.alpha = 255;
			if (Main.netMode != 1)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.netUpdate = true;
			}
		}
		if (base.NPC.ai[0] != -1f && Main.rand.NextBool(4))
		{
			NPC nPC = base.NPC;
			nPC.position += base.NPC.netOffset;
			Dust dust = Dust.NewDustDirect(base.NPC.position + new Vector2(5f), base.NPC.width - 10, base.NPC.height - 10, 5);
			dust.velocity *= 0.5f;
			if (dust.velocity.Y < 0f)
			{
				dust.velocity.Y *= -1f;
			}
			dust.alpha = 120;
			dust.scale = 1f + Main.rand.NextFloat() * 0.4f;
			dust.velocity += base.NPC.velocity * 0.3f;
			NPC nPC2 = base.NPC;
			nPC2.position -= base.NPC.netOffset;
		}
		if (base.NPC.target == 255)
		{
			base.NPC.TargetClosest();
			base.NPC.ai[2] = base.NPC.direction;
		}
		if (Main.player[base.NPC.target].dead || Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 2000f)
		{
			base.NPC.TargetClosest();
		}
		NPCAimedTarget nPCAimedTarget = base.NPC.GetTargetData();
		if (Main.dayTime || !Main.bloodMoon)
		{
			nPCAimedTarget = default(NPCAimedTarget);
		}
		int attackType = -1;
		Vector2 mouthPosition = default(Vector2);
		switch ((int)base.NPC.ai[0])
		{
		case -1:
		{
			NPC nPC6 = base.NPC;
			nPC6.velocity *= 0.98f;
			int spawnFaceDirection = Math.Sign(nPCAimedTarget.Center.X - base.NPC.Center.X);
			if (spawnFaceDirection != 0)
			{
				base.NPC.direction = spawnFaceDirection;
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			if (base.NPC.localAI[1] == 0f && base.NPC.alpha < 100)
			{
				base.NPC.localAI[1] = 1f;
				int dustAmt = 36;
				for (int l = 0; l < dustAmt; l++)
				{
					NPC nPC7 = base.NPC;
					nPC7.position += base.NPC.netOffset;
					Vector2 spinningpoint = Vector2.Normalize(base.NPC.velocity) * new Vector2((float)base.NPC.width / 2f, (float)base.NPC.height) * 0.75f * 0.5f;
					double radians = (float)(l - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt;
					mouthPosition = default(Vector2);
					Vector2 val = spinningpoint.RotatedBy(radians, mouthPosition) + base.NPC.Center;
					Vector2 dustVelocity = val - base.NPC.Center;
					int spawnDustBlood = Dust.NewDust(val + dustVelocity, 0, 0, 5, dustVelocity.X * 2f, dustVelocity.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[spawnDustBlood].noGravity = true;
					Main.dust[spawnDustBlood].velocity = Vector2.Normalize(dustVelocity) * 3f;
					NPC nPC8 = base.NPC;
					nPC8.position -= base.NPC.netOffset;
				}
			}
			if (base.NPC.ai[2] > 5f)
			{
				base.NPC.velocity.Y = -2.5f;
				base.NPC.alpha -= 10;
				if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.alpha += 15;
					if (base.NPC.alpha > 150)
					{
						base.NPC.alpha = 150;
					}
				}
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= 50f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 0:
		{
			Vector2 destination = nPCAimedTarget.Center + new Vector2((0f - base.NPC.ai[2]) * 500f, -300f);
			if (base.NPC.Center.Distance(destination) > 50f)
			{
				Vector2 desiredVelocity = base.NPC.DirectionTo(destination) * goToAttackPositionVelocity;
				base.NPC.SimpleFlyMovement(desiredVelocity, goToAttackPositionAcceleration);
			}
			base.NPC.direction = ((base.NPC.Center.X < nPCAimedTarget.Center.X) ? 1 : (-1));
			float faceTargetDirection = base.NPC.Center.DirectionTo(nPCAimedTarget.Center).ToRotation() - 0.47123894f * (float)base.NPC.spriteDirection;
			if (base.NPC.spriteDirection == -1)
			{
				faceTargetDirection += (float)Math.PI;
			}
			if (base.NPC.spriteDirection != base.NPC.direction)
			{
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.rotation = 0f - base.NPC.rotation;
				faceTargetDirection = 0f - faceTargetDirection;
			}
			base.NPC.rotation = base.NPC.rotation.AngleTowards(faceTargetDirection, 0.02f);
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] > phaseSwitchPhaseTime)
			{
				int attackPicker = (int)base.NPC.ai[3];
				if (attackPicker % 7 == 3 && NPC.CountNPCS(619) < maxBloodSquids)
				{
					attackType = 3;
				}
				else if (attackPicker % 2 == 0)
				{
					SoundEngine.PlaySound(in SoundID.Item170, base.NPC.Center);
					attackType = 2;
				}
				else
				{
					SoundEngine.PlaySound(in SoundID.Item170, base.NPC.Center);
					attackType = 1;
				}
			}
			break;
		}
		case 1:
		{
			base.NPC.direction = ((!(base.NPC.Center.X < nPCAimedTarget.Center.X)) ? 1 : (-1));
			float chargeFaceDirection = base.NPC.Center.DirectionFrom(nPCAimedTarget.Center).ToRotation() - 0.47123894f * (float)base.NPC.spriteDirection;
			if (base.NPC.spriteDirection == -1)
			{
				chargeFaceDirection += (float)Math.PI;
			}
			bool shouldStartCharge = base.NPC.ai[1] < dashChargeUpPhaseTime;
			if ((base.NPC.spriteDirection != base.NPC.direction) & shouldStartCharge)
			{
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.rotation = 0f - base.NPC.rotation;
				chargeFaceDirection = 0f - chargeFaceDirection;
			}
			if (base.NPC.ai[1] < dashChargeUpPhaseTime)
			{
				if (base.NPC.ai[1] == dashChargeUpPhaseTime - 1f)
				{
					SoundEngine.PlaySound(in SoundID.Item172, base.NPC.Center);
				}
				NPC nPC16 = base.NPC;
				nPC16.velocity *= 0.95f;
				base.NPC.rotation = base.NPC.rotation.AngleLerp(chargeFaceDirection, 0.02f);
				NPC nPC17 = base.NPC;
				nPC17.position += base.NPC.netOffset;
				base.NPC.BloodNautilus_GetMouthPositionAndRotation(out var mouthPosition4, out var mouthDirection4);
				Dust chargeUpDust = Dust.NewDustDirect(mouthPosition4 + mouthDirection4 * 60f - new Vector2(40f), 80, 80, 16, 0f, 0f, 150, Color.Transparent, 0.6f);
				chargeUpDust.fadeIn = 1f;
				chargeUpDust.velocity = chargeUpDust.position.DirectionTo(mouthPosition4 + Main.rand.NextVector2Circular(15f, 15f)) * ((Vector2)(ref chargeUpDust.velocity)).Length();
				chargeUpDust.noGravity = true;
				chargeUpDust = Dust.NewDustDirect(mouthPosition4 + mouthDirection4 * 100f - new Vector2(30f), 60, 60, 16, 0f, 0f, 100, Color.Transparent, 0.9f);
				chargeUpDust.fadeIn = 1.5f;
				chargeUpDust.velocity = chargeUpDust.position.DirectionTo(mouthPosition4 + Main.rand.NextVector2Circular(15f, 15f)) * (((Vector2)(ref chargeUpDust.velocity)).Length() + 5f);
				chargeUpDust.noGravity = true;
				NPC nPC18 = base.NPC;
				nPC18.position -= base.NPC.netOffset;
			}
			else if (base.NPC.ai[1] < dashChargeUpPhaseTime + dashPhaseTime)
			{
				NPC nPC19 = base.NPC;
				nPC19.position += base.NPC.netOffset;
				base.NPC.rotation = base.NPC.rotation.AngleLerp(chargeFaceDirection, 0.07f);
				base.NPC.BloodNautilus_GetMouthPositionAndRotation(out var mouthPosition5, out var mouthDirection5);
				if (base.NPC.ai[1] < dashChargeUpPhaseTime + dashPhaseTime * 0.9f)
				{
					if (base.NPC.Center.Distance(nPCAimedTarget.Center) > 240f || base.NPC.ai[1] == dashChargeUpPhaseTime)
					{
						base.NPC.velocity = mouthDirection5 * (0f - (death ? 20f : 16f)) + base.NPC.Center.DirectionTo(nPCAimedTarget.Center) * 2f;
					}
					else
					{
						base.NPC.ai[1] = dashChargeUpPhaseTime + dashPhaseTime * 0.9f;
					}
				}
				for (int m = 0; m < 4; m++)
				{
					Dust chargeBloodDust = Dust.NewDustDirect(mouthPosition5 + mouthDirection5 * 60f - new Vector2(15f), 30, 30, 5, 0f, 0f, 0, Color.Transparent, 1.5f);
					chargeBloodDust.velocity = chargeBloodDust.position.DirectionFrom(mouthPosition5 + Main.rand.NextVector2Circular(5f, 5f)) * ((Vector2)(ref chargeBloodDust.velocity)).Length();
					Dust dust7 = chargeBloodDust;
					dust7.position -= mouthDirection5 * 60f;
					chargeBloodDust = Dust.NewDustDirect(mouthPosition5 + mouthDirection5 * 100f - new Vector2(20f), 40, 40, 5, 0f, 0f, 100, Color.Transparent, 1.5f);
					chargeBloodDust.velocity = chargeBloodDust.position.DirectionFrom(mouthPosition5 + Main.rand.NextVector2Circular(10f, 10f)) * (((Vector2)(ref chargeBloodDust.velocity)).Length() + 5f);
					Dust dust8 = chargeBloodDust;
					dust8.position -= mouthDirection5 * 100f;
				}
				NPC nPC20 = base.NPC;
				nPC20.position -= base.NPC.netOffset;
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= dashChargeUpPhaseTime + dashPhaseTime)
			{
				attackType = 0;
			}
			break;
		}
		case 2:
		{
			base.NPC.direction = ((base.NPC.Center.X < nPCAimedTarget.Center.X) ? 1 : (-1));
			float bloodProjFaceDirection = base.NPC.Center.DirectionTo(nPCAimedTarget.Center).ToRotation() - 0.47123894f * (float)base.NPC.spriteDirection;
			if (base.NPC.spriteDirection == -1)
			{
				bloodProjFaceDirection += (float)Math.PI;
			}
			if (base.NPC.spriteDirection != base.NPC.direction)
			{
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.rotation = 0f - base.NPC.rotation;
				bloodProjFaceDirection = 0f - bloodProjFaceDirection;
			}
			base.NPC.rotation = base.NPC.rotation.AngleLerp(bloodProjFaceDirection, 0.2f);
			if (base.NPC.ai[1] < bloodSpitChargeUpPhaseTime)
			{
				NPC nPC9 = base.NPC;
				nPC9.position += base.NPC.netOffset;
				NPC nPC10 = base.NPC;
				nPC10.velocity *= 0.95f;
				base.NPC.BloodNautilus_GetMouthPositionAndRotation(out var mouthPosition2, out var mouthDirection2);
				if (!Main.rand.NextBool(4))
				{
					Dust bloodProjChargeUpDust = Dust.NewDustDirect(mouthPosition2 + mouthDirection2 * 60f - new Vector2(60f), 120, 120, 16, 0f, 0f, 150, Color.Transparent, 0.6f);
					bloodProjChargeUpDust.fadeIn = 1f;
					bloodProjChargeUpDust.velocity = bloodProjChargeUpDust.position.DirectionTo(mouthPosition2 + Main.rand.NextVector2Circular(15f, 15f)) * (((Vector2)(ref bloodProjChargeUpDust.velocity)).Length() + 3f);
					bloodProjChargeUpDust.noGravity = true;
					bloodProjChargeUpDust = Dust.NewDustDirect(mouthPosition2 + mouthDirection2 * 100f - new Vector2(80f), 160, 160, 16, 0f, 0f, 100, Color.Transparent, 0.9f);
					bloodProjChargeUpDust.fadeIn = 1.5f;
					bloodProjChargeUpDust.velocity = bloodProjChargeUpDust.position.DirectionTo(mouthPosition2 + Main.rand.NextVector2Circular(15f, 15f)) * (((Vector2)(ref bloodProjChargeUpDust.velocity)).Length() + 5f);
					bloodProjChargeUpDust.noGravity = true;
				}
				NPC nPC11 = base.NPC;
				nPC11.position -= base.NPC.netOffset;
			}
			else if (base.NPC.ai[1] < bloodSpitChargeUpPhaseTime + bloodSpitPhaseTime)
			{
				NPC nPC12 = base.NPC;
				nPC12.position += base.NPC.netOffset;
				NPC nPC13 = base.NPC;
				nPC13.velocity *= 0.9f;
				float bloodProjShootTimer = (base.NPC.ai[1] - bloodSpitChargeUpPhaseTime) % (bloodSpitPhaseTime / (float)numBloodSpitVolleys);
				base.NPC.BloodNautilus_GetMouthPositionAndRotation(out var mouthPosition3, out var mouthDirection3);
				if (bloodProjShootTimer < bloodSpitPhaseTime / (float)numBloodSpitVolleys * 0.8f)
				{
					for (int i = 0; i < 5; i++)
					{
						Dust bloodProjShootDust = Dust.NewDustDirect(mouthPosition3 + mouthDirection3 * 50f - new Vector2(15f), 30, 30, 5, 0f, 0f, 0, Color.Transparent, 1.5f);
						bloodProjShootDust.velocity = bloodProjShootDust.position.DirectionFrom(mouthPosition3 + Main.rand.NextVector2Circular(5f, 5f)) * ((Vector2)(ref bloodProjShootDust.velocity)).Length();
						Dust dust3 = bloodProjShootDust;
						dust3.position -= mouthDirection3 * 60f;
						bloodProjShootDust = Dust.NewDustDirect(mouthPosition3 + mouthDirection3 * 90f - new Vector2(20f), 40, 40, 5, 0f, 0f, 100, Color.Transparent, 1.5f);
						bloodProjShootDust.velocity = bloodProjShootDust.position.DirectionFrom(mouthPosition3 + Main.rand.NextVector2Circular(10f, 10f)) * (((Vector2)(ref bloodProjShootDust.velocity)).Length() + 5f);
						Dust dust4 = bloodProjShootDust;
						dust4.position -= mouthDirection3 * 100f;
					}
				}
				if ((int)bloodProjShootTimer == 0)
				{
					NPC nPC14 = base.NPC;
					nPC14.velocity += mouthDirection3 * -8f;
					for (int j = 0; j < 20; j++)
					{
						Dust bloodProjShootDust2 = Dust.NewDustDirect(mouthPosition3 + mouthDirection3 * 60f - new Vector2(15f), 30, 30, 5, 0f, 0f, 0, Color.Transparent, 1.5f);
						bloodProjShootDust2.velocity = bloodProjShootDust2.position.DirectionFrom(mouthPosition3 + Main.rand.NextVector2Circular(5f, 5f)) * ((Vector2)(ref bloodProjShootDust2.velocity)).Length();
						Dust dust5 = bloodProjShootDust2;
						dust5.position -= mouthDirection3 * 60f;
						bloodProjShootDust2 = Dust.NewDustDirect(mouthPosition3 + mouthDirection3 * 100f - new Vector2(20f), 40, 40, 5, 0f, 0f, 100, Color.Transparent, 1.5f);
						bloodProjShootDust2.velocity = bloodProjShootDust2.position.DirectionFrom(mouthPosition3 + Main.rand.NextVector2Circular(10f, 10f)) * (((Vector2)(ref bloodProjShootDust2.velocity)).Length() + 5f);
						Dust dust6 = bloodProjShootDust2;
						dust6.position -= mouthDirection3 * 100f;
					}
					if (Main.netMode != 1)
					{
						int projectileAmt = (death ? 6 : 5);
						float rotation = MathHelper.ToRadians((float)(death ? 35 : 30));
						Vector2 initialProjectileVelocity = mouthDirection3 * 10f;
						int damage = base.NPC.GetAttackDamage_ForProjectiles(30f, 25f);
						for (int k = 0; k < projectileAmt + 1; k++)
						{
							double radians2 = MathHelper.Lerp(0f - rotation, rotation, (float)k / (float)(projectileAmt - 1));
							mouthPosition = default(Vector2);
							Vector2 perturbedSpeed = initialProjectileVelocity.RotatedBy(radians2, mouthPosition);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), mouthPosition3 - mouthDirection3 * 5f, initialProjectileVelocity + perturbedSpeed, 814, damage, 0f, Main.myPlayer);
						}
					}
				}
				NPC nPC15 = base.NPC;
				nPC15.position -= base.NPC.netOffset;
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= bloodSpitChargeUpPhaseTime + bloodSpitPhaseTime)
			{
				attackType = 0;
			}
			break;
		}
		case 3:
		{
			base.NPC.direction = ((base.NPC.Center.X < nPCAimedTarget.Center.X) ? 1 : (-1));
			float targetAngle = 0f;
			base.NPC.spriteDirection = base.NPC.direction;
			if (base.NPC.ai[1] < bloodSquidPhaseTime)
			{
				NPC nPC3 = base.NPC;
				nPC3.position += base.NPC.netOffset;
				float bloodSquidVelClamp = MathHelper.Clamp(1f - base.NPC.ai[1] / bloodSquidPhaseTime * 1.5f, 0f, 1f);
				NPC nPC4 = base.NPC;
				((Vector2)(ref mouthPosition))._002Ector(0f, bloodSquidVelClamp * -1.5f);
				nPC4.velocity = Vector2.Lerp(base.NPC.velocity, mouthPosition, 0.03f);
				base.NPC.velocity = Vector2.Zero;
				base.NPC.rotation = base.NPC.rotation.AngleLerp(targetAngle, 0.02f);
				base.NPC.BloodNautilus_GetMouthPositionAndRotation(out mouthPosition, out var _);
				float t = base.NPC.ai[1] / bloodSquidPhaseTime;
				float scaleFactor2 = Utils.GetLerpValue(0f, 0.5f, t) * Utils.GetLerpValue(1f, 0.5f, t);
				Lighting.AddLight(base.NPC.Center, new Vector3(1f, 0.5f, 0.5f) * scaleFactor2);
				if (!Main.rand.NextBool(3))
				{
					Dust dust2 = Dust.NewDustDirect(base.NPC.Center - new Vector2(6f), 12, 12, 5, 0f, 0f, 60, Color.Transparent, 1.4f);
					dust2.position += new Vector2((float)(base.NPC.spriteDirection * 12), 12f);
					dust2.velocity *= 0.1f;
				}
				NPC nPC5 = base.NPC;
				nPC5.position -= base.NPC.netOffset;
			}
			if (base.NPC.ai[1] == 10f || (death && base.NPC.ai[1] == 20f) || base.NPC.ai[1] == 30f)
			{
				BloodNautilus_CallForHelp(base.NPC);
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= bloodSquidPhaseTime)
			{
				attackType = 0;
			}
			break;
		}
		}
		if (attackType != -1)
		{
			base.NPC.ai[0] = attackType;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			base.NPC.netUpdate = true;
			base.NPC.TargetClosest();
			if (attackType == 0)
			{
				base.NPC.ai[2] = base.NPC.direction;
			}
			else
			{
				base.NPC.ai[3]++;
			}
		}
		base.NPC.reflectsProjectiles = false;
		return false;
	}

	private static void BloodNautilus_CallForHelp(NPC npc)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 1 || !Main.player[npc.target].active || Main.player[npc.target].dead || npc.Distance(Main.player[npc.target].Center) > 2000f)
		{
			return;
		}
		Point npcCenterTileCoords = npc.Center.ToTileCoordinates();
		Point npcCenterTileCoordsCopy = npcCenterTileCoords;
		int bloodTearRandSpawnOffset = 20;
		int npcCenterTileRadius = 3;
		int npcCenterCopyTileRadius = 8;
		int bloodTearSpawnTileRadius = 2;
		int attempts = 0;
		int bloodTearTileX;
		int bloodTearTileY;
		while (true)
		{
			if (attempts >= 100)
			{
				return;
			}
			attempts++;
			bloodTearTileX = Main.rand.Next(npcCenterTileCoordsCopy.X - bloodTearRandSpawnOffset, npcCenterTileCoordsCopy.X + bloodTearRandSpawnOffset + 1);
			bloodTearTileY = Main.rand.Next(npcCenterTileCoordsCopy.Y - bloodTearRandSpawnOffset, npcCenterTileCoordsCopy.Y + bloodTearRandSpawnOffset + 1);
			if ((bloodTearTileY < npcCenterTileCoordsCopy.Y - npcCenterCopyTileRadius || bloodTearTileY > npcCenterTileCoordsCopy.Y + npcCenterCopyTileRadius || bloodTearTileX < npcCenterTileCoordsCopy.X - npcCenterCopyTileRadius || bloodTearTileX > npcCenterTileCoordsCopy.X + npcCenterCopyTileRadius) && (bloodTearTileY < npcCenterTileCoords.Y - npcCenterTileRadius || bloodTearTileY > npcCenterTileCoords.Y + npcCenterTileRadius || bloodTearTileX < npcCenterTileCoords.X - npcCenterTileRadius || bloodTearTileX > npcCenterTileCoords.X + npcCenterTileRadius) && !Main.tile[bloodTearTileX, bloodTearTileY].HasUnactuatedTile)
			{
				bool spawnBloodTear = true;
				if (spawnBloodTear && Main.tile[bloodTearTileX, bloodTearTileY].LiquidType == 1)
				{
					spawnBloodTear = false;
				}
				if (spawnBloodTear && Collision.SolidTiles(bloodTearTileX - bloodTearSpawnTileRadius, bloodTearTileX + bloodTearSpawnTileRadius, bloodTearTileY - bloodTearSpawnTileRadius, bloodTearTileY + bloodTearSpawnTileRadius))
				{
					spawnBloodTear = false;
				}
				if (spawnBloodTear && !Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
				{
					spawnBloodTear = false;
				}
				if (spawnBloodTear)
				{
					break;
				}
			}
		}
		Projectile.NewProjectile(npc.GetSource_FromAI(), bloodTearTileX * 16 + 8, bloodTearTileY * 16 + 8, 0f, 0f, 813, 0, 0f, Main.myPlayer);
	}
}
