using System;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Typeless;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

[PierceResistException(false)]
public class RelicOfDeliveranceSpear : ModProjectile
{
	public Vector2 idealVel;

	public int driftFrames;

	public bool flying;

	public int boostTimer;

	public int driftTimer;

	public int driftPower;

	public float driftPowerScaling;

	public float driftBadMult;

	public bool killed;

	public float iframeLevel;

	public float velX;

	public float velY;

	public Vector2 respawnPoint;

	public int respawnTimer;

	public bool inTiles;

	public float respawnMult;

	public Color bColor;

	public SlotId digSoundSlot;

	public int digFXCooldown;

	public float ramLerp;

	public float damageMult;

	public int hitCountDamageSource;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<RelicOfDeliverance>();

	public ref float time => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public bool boosted => boostTimer > 0;

	public bool drifting => Main.mouseLeft;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 68;
		base.Projectile.height = 32;
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 50 * base.Projectile.MaxUpdates;
		base.Projectile.extraUpdates = 3;
		base.Projectile.ContinuouslyUpdateDamageStats = true;
	}

	public override void AI()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_108b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1675: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1695: Unknown result type (might be due to invalid IL or missing references)
		//IL_169a: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		//IL_075c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0761: Unknown result type (might be due to invalid IL or missing references)
		//IL_076b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Unknown result type (might be due to invalid IL or missing references)
		//IL_078f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_079f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_110d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1118: Unknown result type (might be due to invalid IL or missing references)
		//IL_111d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_159a: Unknown result type (might be due to invalid IL or missing references)
		//IL_159f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1394: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1169: Unknown result type (might be due to invalid IL or missing references)
		//IL_1174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ced: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_087b: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_089d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_199f: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_19cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a05: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_15db: Unknown result type (might be due to invalid IL or missing references)
		//IL_1711: Unknown result type (might be due to invalid IL or missing references)
		//IL_171c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1721: Unknown result type (might be due to invalid IL or missing references)
		//IL_1726: Unknown result type (might be due to invalid IL or missing references)
		//IL_1734: Unknown result type (might be due to invalid IL or missing references)
		//IL_174d: Unknown result type (might be due to invalid IL or missing references)
		//IL_177a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1789: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1504: Unknown result type (might be due to invalid IL or missing references)
		//IL_1582: Unknown result type (might be due to invalid IL or missing references)
		//IL_158d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1188: Unknown result type (might be due to invalid IL or missing references)
		//IL_1193: Unknown result type (might be due to invalid IL or missing references)
		//IL_1198: Unknown result type (might be due to invalid IL or missing references)
		//IL_119d: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1950: Unknown result type (might be due to invalid IL or missing references)
		//IL_195b: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_180c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1811: Unknown result type (might be due to invalid IL or missing references)
		//IL_181c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1821: Unknown result type (might be due to invalid IL or missing references)
		//IL_1826: Unknown result type (might be due to invalid IL or missing references)
		//IL_1834: Unknown result type (might be due to invalid IL or missing references)
		//IL_184d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1852: Unknown result type (might be due to invalid IL or missing references)
		//IL_1857: Unknown result type (might be due to invalid IL or missing references)
		//IL_1600: Unknown result type (might be due to invalid IL or missing references)
		//IL_125c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1261: Unknown result type (might be due to invalid IL or missing references)
		//IL_126f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1288: Unknown result type (might be due to invalid IL or missing references)
		//IL_128d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1298: Unknown result type (might be due to invalid IL or missing references)
		//IL_129d: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f29: Unknown result type (might be due to invalid IL or missing references)
		//IL_1402: Unknown result type (might be due to invalid IL or missing references)
		//IL_141b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1420: Unknown result type (might be due to invalid IL or missing references)
		//IL_143d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1442: Unknown result type (might be due to invalid IL or missing references)
		//IL_144f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1455: Unknown result type (might be due to invalid IL or missing references)
		//IL_1457: Unknown result type (might be due to invalid IL or missing references)
		//IL_1461: Unknown result type (might be due to invalid IL or missing references)
		//IL_1466: Unknown result type (might be due to invalid IL or missing references)
		//IL_1473: Unknown result type (might be due to invalid IL or missing references)
		//IL_1479: Unknown result type (might be due to invalid IL or missing references)
		//IL_147b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1480: Unknown result type (might be due to invalid IL or missing references)
		//IL_148a: Unknown result type (might be due to invalid IL or missing references)
		//IL_149b: Unknown result type (might be due to invalid IL or missing references)
		//IL_14aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0955: Unknown result type (might be due to invalid IL or missing references)
		//IL_0965: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0971: Unknown result type (might be due to invalid IL or missing references)
		//IL_097d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0982: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0998: Unknown result type (might be due to invalid IL or missing references)
		//IL_099d: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09db: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0faa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1007: Unknown result type (might be due to invalid IL or missing references)
		//IL_100c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1023: Unknown result type (might be due to invalid IL or missing references)
		//IL_1029: Unknown result type (might be due to invalid IL or missing references)
		//IL_105a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1070: Unknown result type (might be due to invalid IL or missing references)
		//IL_1075: Unknown result type (might be due to invalid IL or missing references)
		//IL_107b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1afc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b62: Unknown result type (might be due to invalid IL or missing references)
		if (killed)
		{
			return;
		}
		ramLerp = Utils.GetLerpValue(120 * driftPower, 0f, boostTimer, clamped: true);
		Owner.Calamity().rOfDelivarenceRam = false;
		float rate = Main.GlobalTimeWrappedHourly * 7f;
		Color powerColor = Color.Khaki;
		List<Color> eColors = new List<Color>
		{
			Color.Goldenrod,
			Color.OrangeRed,
			Color.Orange,
			Color.Gold
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		bColor = Color.Lerp(Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f)), powerColor, Utils.Remap(ramLerp, iframeLevel, 0.17f, 0.5f, 0f) * (float)((driftPower != 1) ? 1 : 0));
		if (Owner.dead || Owner.Calamity().mouseRight || !WorldGen.InWorld(Owner.Center.ToTileCoordinates().X, Owner.Center.ToTileCoordinates().Y, 20))
		{
			KillProj();
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4 * base.Projectile.MaxUpdates)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (Owner.HeldItem == null)
		{
			KillProj();
			return;
		}
		if (Owner.HeldItem.type != ModContent.ItemType<RelicOfDeliverance>())
		{
			KillProj();
			return;
		}
		float speed = (CalamityPlayer.areThereAnyDamnBosses ? 3f : 6f) * driftBadMult * (inTiles ? 0.65f : 1f);
		Vector2 aimDir = Owner.Calamity().mouseWorld;
		idealVel = Owner.Center.DirectionTo(aimDir) * speed * (float)driftPower;
		driftPowerScaling = MathHelper.Lerp(driftPowerScaling, (float)driftPower * 0.7f, (driftPowerScaling > (float)driftPower * 0.7f) ? 0.005f : 0.04f);
		if (time == 0f)
		{
			base.Projectile.velocity = idealVel * 0.2f;
			velX = base.Projectile.velocity.X;
			velY = base.Projectile.velocity.Y;
		}
		float lerpPower = (drifting ? 0f : ((boosted && driftPower > 1) ? (0.01f * ramLerp) : 0.02f));
		velX = MathHelper.Lerp(base.Projectile.velocity.X, idealVel.X, lerpPower);
		velY = MathHelper.Lerp(base.Projectile.velocity.Y, idealVel.Y, lerpPower);
		base.Projectile.velocity = new Vector2(velX, velY);
		if (((Vector2)(ref base.Projectile.velocity)).Length() <= speed && !drifting)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.05f;
		}
		if (drifting)
		{
			if (driftTimer == 0)
			{
				driftPower = 1;
			}
			boostTimer = 0;
			if (time % (float)base.Projectile.MaxUpdates == 0f)
			{
				if (((Vector2)(ref base.Projectile.velocity)).Length() > 1f)
				{
					driftBadMult = 1f;
					driftTimer++;
					driftPower = ((driftTimer > 100) ? 3 : ((driftTimer <= 40) ? 1 : 2));
				}
				else
				{
					driftPower = 1;
					driftTimer = (int)MathHelper.Clamp((float)driftTimer * 0.94f, 1f, 300f);
				}
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 1f - 0.02f * (CalamityPlayer.areThereAnyDamnBosses ? 0.5f : 1f);
			}
			base.Projectile.extraUpdates = 3;
			float sparkPower = Utils.GetLerpValue(0f, 100f, driftTimer);
			if (time % (float)base.Projectile.MaxUpdates * 2f == 0f)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/NullImpact");
				style.Volume = 0.045f;
				style.Pitch = Main.rand.NextFloat(-0.2f, -0.4f) + sparkPower * 0.4f + ((driftPower == 2) ? 0.3f : ((driftPower == 3) ? 0.7f : 0f));
				style.MaxInstances = -1;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			for (int i = 0; i < 2; i++)
			{
				Color sparkColor = ((driftPower == 2) ? Color.Orange : ((driftPower == 3) ? Color.OrangeRed : Color.Khaki));
				Vector2 baseVel = Vector2.Lerp(base.Projectile.velocity, -idealVel, sparkPower);
				Vector2 vel = baseVel.RotatedByRandom(1.5f - sparkPower * 0.5f) * Main.rand.NextFloat(0.5f, 3f);
				GeneralParticleHandler.SpawnParticle(new VelChangingSpark(base.Projectile.Center + idealVel * 3f, vel + baseVel.SafeNormalize(Vector2.UnitX) * 15f, vel, "CalamityMod/Particles/BloomCircle", Main.rand.Next(6, 10), Main.rand.NextFloat(0.25f, 0.65f) * sparkPower, Color.Lerp(sparkColor, Color.Goldenrod, Main.rand.NextFloat(0f, 0.3f)), new Vector2(0.2f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.3f, 0.35f));
			}
		}
		Color newColor;
		if (!drifting)
		{
			if (driftPower > 1 && ramLerp < iframeLevel && !killed)
			{
				Owner.Calamity().rOfDelivarenceRam = true;
				if (Main.rand.NextBool() || driftPower == 3)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 30f + Main.rand.NextVector2Circular(30f, 30f) * driftPowerScaling, base.Projectile.velocity * Main.rand.NextFloat(0.2f, 5f), "CalamityMod/Particles/FullStar", affectedByGravity: false, Main.rand.Next(15, 26), Main.rand.NextFloat(0.6f, 1.2f), Color.Khaki, new Vector2(2f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.8f));
				}
			}
			if (driftPower > 2)
			{
				base.Projectile.extraUpdates = 5;
			}
			else
			{
				base.Projectile.extraUpdates = 3;
			}
			if (driftPower == 1 && driftBadMult == 1f && driftTimer > 0)
			{
				base.Projectile.velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) * (speed * 0.1f);
				driftBadMult = 0.1f;
			}
			if (time % (float)base.Projectile.MaxUpdates == 0f)
			{
				if (driftBadMult < 1f)
				{
					driftBadMult += 0.005f;
				}
				if (boostTimer == 0 && driftTimer > 0)
				{
					base.Projectile.netUpdate = true;
					driftBadMult = ((driftPower > 1) ? 1f : ((time < 60f) ? 0f : 0.35f));
					driftTimer = 0;
					base.Projectile.velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) * speed * (float)driftPower;
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 3.1f * (float)driftPower, Color.Goldenrod * 0.8f, "CalamityMod/Particles/BloomRing", new Vector2(0.4f, 1f), base.Projectile.velocity.ToRotation(), 0f, 1.33f * (float)driftPower, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 8.4f * (float)driftPower, Color.OrangeRed * 0.8f, "CalamityMod/Particles/BloomRing", new Vector2(0.4f, 1f), base.Projectile.velocity.ToRotation(), 0f, 0.92f * (float)driftPower, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					float volume = 0.1f + 0.3f * (float)driftPower;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HolyColliderSmallHit");
					style.Volume = volume;
					style.Pitch = -0.7f + 0.1f * (float)driftPower;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					if (driftPower > 1)
					{
						style = new SoundStyle("CalamityMod/Sounds/Item/LauncherHeavyShot");
						style.Volume = volume * 2f;
						style.Pitch = -0.3f - 0.1f * (float)driftPower;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					if (driftPower > 2)
					{
						SoundStyle sound3 = new SoundStyle("CalamityMod/Sounds/Item/OpalChargedFire");
						for (int j = 0; j < 3; j++)
						{
							SoundEngine.PlaySound(sound3 with
							{
								Volume = 0.7f,
								Pitch = 0.4f,
								MaxInstances = 3
							}, base.Projectile.Center);
						}
					}
					if (driftPower > 1)
					{
						Owner.SetScreenshake((driftPower == 2) ? 6 : 9);
					}
					else if (driftBadMult > 0.15f)
					{
						driftBadMult -= 0.15f;
					}
					boostTimer = 120 * driftPower;
					base.Projectile.numHits = 0;
					hitCountDamageSource = 0;
				}
				if (boostTimer > 0)
				{
					boostTimer--;
				}
				if (boostTimer == 0)
				{
					driftPower = 1;
				}
			}
			if (driftBadMult > 0.2f)
			{
				float sine = (float)Math.Sin(time * 0.085f / (float)Math.PI);
				float mult = driftBadMult * driftPowerScaling;
				Vector2 tipPos = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 125f * mult;
				Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 15.5f;
				for (int k = 0; k < 2; k++)
				{
					offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 15.5f * (float)((k != 0) ? 1 : (-1));
					GeneralParticleHandler.SpawnParticle(new VelChangingSpark(tipPos + offset, (-base.Projectile.velocity.SafeNormalize(Vector2.UnitX) + offset * 0.05f) * 20.5f * mult, -base.Projectile.velocity.SafeNormalize(Vector2.UnitX), "CalamityMod/Particles/SmallBloom", 15, 0.11f * mult, bColor * 0.65f, new Vector2(1f, 3.8f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.42f, 0.4f));
				}
				if (driftPower == 3)
				{
					Vector2 vel2 = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(1f, 10f);
					GeneralParticleHandler.SpawnParticle(new VelChangingSpark(base.Projectile.Center + vel2, -base.Projectile.velocity.SafeNormalize(Vector2.UnitX), -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) + vel2 * Main.rand.NextFloat(0.01f, 0.02f), "CalamityMod/Particles/BloomCircle", Main.rand.Next(12, 20), Main.rand.NextFloat(0.55f, 0.95f), Color.Lerp(Color.OrangeRed, Color.Goldenrod, Main.rand.NextFloat(0f, 1f)), new Vector2(0.2f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.35f, 0.25f));
				}
				if (time % (float)base.Projectile.MaxUpdates == 0f)
				{
					Vector2 vel3 = (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) + offset * (float)(Main.rand.NextBool() ? 1 : (-1)) * 0.03f) * 50.5f * mult;
					Vector2 position = base.Projectile.Center + (offset * (float)(Main.rand.NextBool() ? 1 : (-1))).RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.6f, 1.4f);
					int type = ModContent.DustType<LightDust>();
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(position, type, null, 0, newColor);
					dust.scale = Main.rand.NextFloat(0.7f, 1.2f) * mult;
					dust.noGravity = true;
					dust.velocity = vel3 * Main.rand.NextFloat(0.6f, 1.4f);
					dust.color = bColor;
				}
			}
		}
		if (Collision.SolidCollision(base.Projectile.Center, 20, 20))
		{
			inTiles = true;
		}
		else
		{
			inTiles = false;
		}
		if (inTiles)
		{
			if (respawnTimer == 0 && time % (float)base.Projectile.MaxUpdates == 0f)
			{
				SoundStyle digSound = new SoundStyle("CalamityMod/Sounds/Item/HeavyDig");
				SoundStyle style = digSound with
				{
					Volume = 0.7f,
					IsLooped = true
				};
				digSoundSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
				if (digFXCooldown == 0)
				{
					style = new SoundStyle("CalamityMod/Sounds/Item/MagicRockImpact");
					style.Volume = 0.4f;
					style.Pitch = Main.rand.NextFloat(0.3f, 0.7f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					for (int l = 0; l < 13; l++)
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(2f, 24f), "CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard6", affectedByGravity: true, Main.rand.Next(25, 36), Main.rand.NextFloat(0.85f, 2.3f), Color.White, new Vector2(1.1f, 0.8f), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-5f, 5f)));
						if (l % 2 == 0)
						{
							GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(1f, 7f), -base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(1f, 12f), Color.Peru, Color.Sienna, Main.rand.NextFloat(1.4f, 2.3f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
						}
					}
					digFXCooldown = 5;
				}
			}
			if (time % (float)base.Projectile.MaxUpdates == 0f)
			{
				respawnTimer++;
				base.Projectile.soundDelay--;
			}
			if (respawnTimer >= 300)
			{
				base.Projectile.netUpdate = true;
				base.Projectile.Center = respawnPoint;
				Owner.Center = respawnPoint;
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 0.01f;
				for (int m = 0; m < 15; m++)
				{
					Vector2 vel4 = ((float)Math.PI * 2f * (float)m / 15f).ToRotationVector2() * 15.5f * ((m % 4 == 0) ? 0.88f : 1f) * Main.rand.NextFloat(0.8f, 1f);
					Main.rand.NextFloat(1.3f, 1.6f);
					_ = m % 4;
					GeneralParticleHandler.SpawnParticle(new VelChangingSpark(respawnPoint, vel4.RotatedBy(0.019999999552965164) * 3f, -vel4.RotatedBy(-0.03999999910593033) * 4f, "CalamityMod/Particles/SmallBloom", 27, 0.2f, Color.Goldenrod, new Vector2(1.8f, 1.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.25f, 0.07f));
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(respawnPoint, Vector2.Zero, Color.Khaki, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 2f, 0.5f, 28, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianHeal");
				style.Volume = 0.6f;
				style.Pitch = Main.rand.NextFloat(-0.7f, -0.9f);
				SoundEngine.PlaySound(in style, respawnPoint);
				KillProj();
			}
			if (respawnPoint == Vector2.Zero)
			{
				respawnPoint = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 60f;
			}
			if (SoundEngine.TryGetActiveSound(digSoundSlot, out ActiveSound zound) && zound.IsPlaying)
			{
				zound.Position = base.Projectile.Center;
				zound.Pitch = 0.4f * respawnMult - (drifting ? 0.5f : 0f);
				zound.Volume = (drifting ? 0.6f : 1f);
			}
			respawnMult = Utils.GetLerpValue(60f, 300f, respawnTimer);
		}
		else
		{
			if (SoundEngine.TryGetActiveSound(digSoundSlot, out ActiveSound zound2))
			{
				zound2?.Stop();
			}
			respawnPoint = base.Projectile.Center;
			if (respawnTimer > 0 && digFXCooldown == 0)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MagicRockSound");
				style.Volume = 0.4f;
				style.Pitch = Main.rand.NextFloat(0.3f, 0.7f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int n = 0; n < 13; n++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(2f, 24f), "CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard6", affectedByGravity: true, Main.rand.Next(25, 36), Main.rand.NextFloat(0.85f, 2.3f), Color.White, new Vector2(1.1f, 0.8f), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-5f, 5f)));
					if (n % 2 == 0)
					{
						GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(1f, 7f), base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(1f, 12f), Color.Peru, Color.Sienna, Main.rand.NextFloat(1.4f, 2.3f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
					}
				}
				digFXCooldown = 5;
			}
			respawnTimer = 0;
			respawnMult = 0f;
			if (time % (float)base.Projectile.MaxUpdates * 2f == 0f && !drifting)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianDash");
				style.Volume = 0.065f * driftPowerScaling;
				style.Pitch = Main.rand.NextFloat(-0.4f, -0.9f) + ((driftPower == 3) ? 1.4f : 0f);
				style.MaxInstances = -1;
				SoundEngine.PlaySound(in style, Owner.Center);
			}
		}
		if (time % (float)base.Projectile.MaxUpdates == 0f && digFXCooldown > 0)
		{
			digFXCooldown--;
		}
		Owner.Center = base.Projectile.Center;
		Owner.velocity = base.Projectile.velocity * (float)base.Projectile.MaxUpdates;
		Owner.dashDelay = 0;
		base.Projectile.timeLeft++;
		Owner.RemoveAllGrapplingHooks();
		Vector2 center = base.Projectile.Center;
		newColor = Color.Gold;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * (driftPowerScaling + 0.5f));
		Owner.mount?.Dismount(Owner);
		Owner.ChangeDir((Math.Sign(drifting ? idealVel.X : base.Projectile.velocity.X) > 0) ? 1 : (-1));
		if (!killed)
		{
			float idealRot = (drifting ? idealVel : base.Projectile.velocity).ToRotation();
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(idealRot, (boostTimer > 0) ? 1f : 0.15f);
			Owner.fullRotationOrigin = Owner.Center - Owner.position;
			Owner.fullRotation = ((Owner.direction == -1) ? MathHelper.ToRadians(180f) : 0f) + base.Projectile.rotation + MathHelper.ToRadians(65f * (float)Owner.direction);
			float rot = (drifting ? idealVel : base.Projectile.velocity).ToRotation() + ((Owner.direction == -1) ? MathHelper.ToRadians(270f) : MathHelper.ToRadians(-90f));
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rot - Owner.fullRotation);
			Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, rot - Owner.fullRotation);
		}
		time++;
	}

	public void KillProj()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(digSoundSlot, out ActiveSound sound))
		{
			sound?.Stop();
		}
		if (inTiles && respawnPoint != Vector2.Zero)
		{
			base.Projectile.Center = respawnPoint;
			if (!Owner.dead)
			{
				Owner.Center = respawnPoint;
			}
		}
		killed = true;
		Owner.Calamity().rOfDelivarenceRam = false;
		base.Projectile.netUpdate = true;
		base.Projectile.Kill();
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer && SoundEngine.TryGetActiveSound(digSoundSlot, out ActiveSound sound))
		{
			sound.Stop();
		}
		Owner.fullRotationOrigin = Owner.Center - Owner.position;
		Owner.fullRotation = 0f;
		if (Main.netMode != 0)
		{
			NetMessage.SendData(4, -1, -1, null, Owner.whoAmI);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		if ((float)Main.rand.Next(0, 101) < Owner.GetTotalCritChance(Owner.GetBestClass()))
		{
			modifiers.SetCrit();
		}
		float minMult = 0.15f;
		int hitsToMinMult = 15;
		float damageMult = Utils.Remap(hitCountDamageSource, 0f, hitsToMinMult, 1f, minMult);
		float totalMult = ((driftPower == 1) ? 0.3f : ((driftPower == 2) ? 1.6f : 2.5f)) * damageMult * ((hitCountDamageSource == 0 && driftPower > 1) ? 1.9f : 1f) * (float)((!Owner.Calamity().profanedSoulRelicBuff) ? 1 : 8);
		modifiers.SourceDamage *= totalMult;
		if (base.Projectile.numHits == 0 && driftPower > 1)
		{
			for (int i = 0; i < 5; i++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 18, 0.05f * (float)driftPower, Color.Lerp(Color.White, Color.Orange, (float)i * 0.2f) * 0.85f, new Vector2(4f + (float)i * 0.55f, 0.4f + (float)i * 0.1f), quickShrink: true, glow: false));
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/FinalDawnSlash");
			style.Volume = 1f;
			style.Pitch = Main.rand.NextFloat(0.2f, 0.4f) * damageMult;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (base.Projectile.soundDelay <= 0)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HolyColliderBigHit");
			style.Volume = 1f;
			style.Pitch = Main.rand.NextFloat(-0.2f, -0.4f) * damageMult;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.soundDelay = 25;
		}
	}

	public override bool? CanDamage()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (!(base.Projectile.velocity == Vector2.Zero) && !drifting)
		{
			return null;
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 30f * driftPowerScaling, 60f * driftPowerScaling, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPos = Owner.MountedCenter + (drifting ? idealVel : base.Projectile.velocity).SafeNormalize(Vector2.UnitX) * (55f - 35f * Utils.GetLerpValue(0f, 25f, driftTimer, clamped: true));
		Projectile projectile = base.Projectile;
		Color val = Color.Goldenrod;
		((Color)(ref val)).A = 0;
		Color backglowColor = val;
		Color white = Color.White;
		float backglowArea = 3f * driftPowerScaling;
		SpriteEffects effects = (SpriteEffects)((Math.Sign(drifting ? idealVel.X : base.Projectile.velocity.X) == -1) ? 2 : 0);
		float x = drawPos.X;
		float y = drawPos.Y;
		projectile.DrawProjectileWithBackglow(backglowColor, white, backglowArea, null, null, effects, x, y);
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomRing", (AssetRequestMode)2).Value;
		Texture2D texture2 = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2).Value;
		Texture2D texture3 = ModContent.Request<Texture2D>("CalamityMod/Particles/HalfStar", (AssetRequestMode)2).Value;
		if (inTiles)
		{
			float randSize = Main.rand.NextFloat(0.9f, 1.1f);
			Vector2 vel = base.Projectile.Center.DirectionTo(respawnPoint);
			for (int i = 0; i < 4; i++)
			{
				Vector2 position = base.Projectile.Center - Main.screenPosition + vel * 80f * respawnMult;
				val = Color.Goldenrod;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(texture2, position, null, val * respawnMult, vel.ToRotation() + MathHelper.ToRadians(90f), texture2.Size() * 0.5f, new Vector2(1.2f - (float)i * 0.2f, (1.2f + (float)i * 0.3f) * respawnMult) * (0.7f + (float)i * 0.07f) * 0.05f, (SpriteEffects)0);
			}
			for (int j = 0; j < 3; j++)
			{
				Vector2 position2 = base.Projectile.Center - Main.screenPosition;
				val = ((j == 0) ? Color.White : Color.Goldenrod);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(texture, position2, null, val * respawnMult, vel.ToRotation(), texture.Size() * 0.5f, (0.43f + (float)j * 0.08f) * respawnMult, (SpriteEffects)0);
			}
			for (int k = 0; k < 2; k++)
			{
				Vector2 position3 = respawnPoint - Main.screenPosition;
				val = bColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(texture, position3, null, val * MathHelper.Clamp(respawnMult + 0.5f, 0.5f, 1f), vel.ToRotation(), texture.Size() * 0.5f, 0.15f + respawnMult * 0.5f, (SpriteEffects)0);
				Vector2 position4 = respawnPoint - Main.screenPosition;
				val = bColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(texture3, position4, null, val, (k == 0) ? MathHelper.ToRadians(90f) : 0f, texture3.Size() * 0.5f, new Vector2(0.8f, 1.5f) * 1.65f * randSize, (SpriteEffects)0);
				Vector2 position5 = respawnPoint - Main.screenPosition;
				val = Color.White;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(texture3, position5, null, val * 0.65f, (k == 0) ? MathHelper.ToRadians(90f) : 0f, texture3.Size() * 0.5f, new Vector2(0.8f, 1.5f) * 1.25f * randSize, (SpriteEffects)0);
			}
		}
		if (!drifting)
		{
			Vector2 placement = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * (-20f + 80f * ((driftPowerScaling <= 1f) ? driftPowerScaling : 1f));
			Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Particles/VerticalSmear", (AssetRequestMode)2);
			ModContent.Request<Texture2D>("CalamityMod/Particles/SemiCircularSmearSwipe", (AssetRequestMode)2);
			for (int l = 0; l < 6; l++)
			{
				Vector2 scale = new Vector2(0.5f - (float)l * 0.1f, (1.5f + (float)l * 0.15f) * driftBadMult) * (0.75f * driftPowerScaling * Main.rand.NextFloat(0.9f, 1.1f) + 0.25f);
				Texture2D value = tex.Value;
				Vector2 position6 = placement - Main.screenPosition;
				val = bColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value, position6, null, val * 0.5f * driftBadMult, base.Projectile.rotation + MathHelper.ToRadians(90f), tex.Size() * 0.5f, scale, (SpriteEffects)0);
			}
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (target.life > 0 || target.realLife != -1)
		{
			hitCountDamageSource++;
		}
		else
		{
			hitCountDamageSource -= 3;
		}
		if (hitCountDamageSource < 0)
		{
			hitCountDamageSource = 0;
		}
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overWiresUI.Add(index);
	}

	public RelicOfDeliveranceSpear()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		driftPower = 1;
		driftPowerScaling = 1f;
		driftBadMult = 1f;
		iframeLevel = 0.2f;
		bColor = Color.White;
		damageMult = 1f;
		base._002Ector();
	}
}
