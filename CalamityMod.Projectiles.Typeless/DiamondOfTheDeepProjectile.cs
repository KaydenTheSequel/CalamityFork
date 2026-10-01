using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
using CalamityMod.Tiles.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class DiamondOfTheDeepProjectile : ModProjectile, ILocalizedModType, IModType
{
	public Color bColor;

	public Color color1;

	public Color color2;

	public bool canDamage;

	public bool healing;

	public NPC targeted;

	public int hitCooldown;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool visuals => Owner.Calamity().dOfTheDeepVisual;

	public ref float time => ref base.Projectile.ai[0];

	public ref float energyNumber => ref base.Projectile.ai[1];

	public bool idle => base.Projectile.ai[2] == 0f;

	public ref float projType => ref base.Projectile.localAI[2];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.NoLiquidDistortion[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0983: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_0723: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_081f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_0826: Unknown result type (might be due to invalid IL or missing references)
		//IL_0851: Unknown result type (might be due to invalid IL or missing references)
		//IL_0864: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0876: Unknown result type (might be due to invalid IL or missing references)
		//IL_087b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0880: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Unknown result type (might be due to invalid IL or missing references)
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0904: Unknown result type (might be due to invalid IL or missing references)
		//IL_090e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_128c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ece: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0971: Unknown result type (might be due to invalid IL or missing references)
		//IL_0976: Unknown result type (might be due to invalid IL or missing references)
		//IL_0928: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0beb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_094f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0955: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_0961: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_155d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1568: Unknown result type (might be due to invalid IL or missing references)
		//IL_1572: Unknown result type (might be due to invalid IL or missing references)
		//IL_1582: Unknown result type (might be due to invalid IL or missing references)
		//IL_158c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1593: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1396: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f80: Unknown result type (might be due to invalid IL or missing references)
		//IL_1424: Unknown result type (might be due to invalid IL or missing references)
		//IL_106b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1070: Unknown result type (might be due to invalid IL or missing references)
		//IL_1072: Unknown result type (might be due to invalid IL or missing references)
		//IL_107c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1081: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_10de: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1100: Unknown result type (might be due to invalid IL or missing references)
		//IL_1446: Unknown result type (might be due to invalid IL or missing references)
		//IL_144c: Unknown result type (might be due to invalid IL or missing references)
		//IL_144e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1458: Unknown result type (might be due to invalid IL or missing references)
		//IL_145d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0faf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffa: Unknown result type (might be due to invalid IL or missing references)
		//IL_116d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1172: Unknown result type (might be due to invalid IL or missing references)
		//IL_1177: Unknown result type (might be due to invalid IL or missing references)
		//IL_1181: Unknown result type (might be due to invalid IL or missing references)
		//IL_118b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1191: Unknown result type (might be due to invalid IL or missing references)
		//IL_1193: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_11be: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1203: Unknown result type (might be due to invalid IL or missing references)
		//IL_1209: Unknown result type (might be due to invalid IL or missing references)
		//IL_122c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1231: Unknown result type (might be due to invalid IL or missing references)
		Vector2 velocity = base.Projectile.velocity;
		int startTime = ((projType == 0f) ? 70 : ((projType == 1f) ? 140 : 210)) + 15;
		int endTime = 320;
		if (time == 0f && idle)
		{
			float num = projType;
			if (num != 0f)
			{
				if (num != 1f)
				{
					if (num == 2f)
					{
						color1 = Color.MediumBlue;
						color2 = Color.DodgerBlue;
					}
				}
				else
				{
					color1 = Color.DarkRed;
					color2 = Color.OrangeRed;
				}
			}
			else
			{
				color1 = Color.MediumSeaGreen;
				color2 = Color.DarkSlateGray;
			}
		}
		float rate = Main.GlobalTimeWrappedHourly * 5f;
		List<Color> eColors = new List<Color> { color1, color2 };
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		bColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (idle || time < (float)startTime)
		{
			base.Projectile.timeLeft++;
		}
		if (Owner.dead || !Owner.Calamity().dOfTheDeep)
		{
			base.Projectile.Kill();
		}
		if (Owner.Center.Distance(base.Projectile.Center) > 1100f)
		{
			if (idle)
			{
				base.Projectile.Center = Owner.Center;
			}
			else if (targeted == null && !healing)
			{
				base.Projectile.Kill();
			}
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref bColor)).ToVector3() * 1.5f);
		float velLerp = Utils.GetLerpValue(-1.5f, 3f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
		if (projType == 0f && visuals && Main.rand.NextBool((int)(14f - 5f * velLerp)))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(6f, 6f) * base.Projectile.scale + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 15f, ModContent.DustType<SquashDustHollow>());
			dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 1f) * velLerp;
			dust.scale = Main.rand.NextFloat(1f, 1.3f) * velLerp;
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool() ? Color.Aquamarine : bColor);
			dust.noLightEmittence = true;
			dust.fadeIn = 1f;
			dust.alpha = 100;
		}
		if (projType == 1f && visuals && Main.rand.NextBool((int)(10f - 5f * velLerp)))
		{
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 15f, ModContent.DustType<SquashDust>());
			dust2.velocity = -base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.6f, 1.3f) * velLerp;
			dust2.scale = Main.rand.NextFloat(1.2f, 1.5f) * velLerp;
			dust2.noGravity = true;
			dust2.color = bColor;
			dust2.noLightEmittence = true;
			dust2.noGravity = !Main.rand.NextBool(3);
		}
		if (projType == 2f && visuals && Main.rand.NextBool((int)(3f - velLerp)))
		{
			Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(6f, 6f) * base.Projectile.scale + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 15f, ModContent.DustType<SquashDust>());
			dust3.velocity = -base.Projectile.velocity.RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(1.4f, 3.5f) * velLerp;
			dust3.scale = Main.rand.NextFloat(1.8f, 2.3f) * velLerp;
			dust3.noGravity = true;
			dust3.color = bColor;
			dust3.noLightEmittence = true;
			dust3.fadeIn = 2.3f;
		}
		if (idle)
		{
			if (time == 0f && visuals)
			{
				for (int i = 0; i <= 10; i++)
				{
					float variance = Main.rand.NextFloat(-0.5f, 0.5f);
					Vector2 vel = (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 7f).RotatedBy(variance) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance)) * 2f;
					float scale = (Main.rand.NextFloat(1.5f, 1.7f) - Math.Abs(variance)) * 0.35f;
					Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), vel);
					dust4.scale = scale * 3f;
					dust4.noGravity = true;
					dust4.color = bColor;
					dust4.noLightEmittence = true;
				}
			}
			if (time > 80f)
			{
				float homingSpeed = Utils.Remap(base.Projectile.Center.Distance(Owner.Center), 200f, 600f, 0.07f, 0.16f) + 0.009f * energyNumber;
				float offsetPower = Utils.GetLerpValue(1f, 5f, ((Vector2)(ref Owner.velocity)).Length(), clamped: true);
				float sine = (float)Math.Sin(time * 0.1f / (float)Math.PI);
				float sine2 = (float)Math.Sin(time * 0.04f / (float)Math.PI);
				Vector2 bonusMobility = ((offsetPower > 0f) ? ((base.Projectile.Center.DirectionTo(Owner.Center) * 90f * sine2).RotatedBy(0.8f * sine) * offsetPower) : Vector2.Zero);
				Vector2 goalPosition = Owner.MountedCenter + bonusMobility + ((float)Math.PI * 2f * energyNumber / (float)Math.Max(Owner.ownedProjectileCounts[ModContent.ProjectileType<AmuletEnergy>()], 1)).ToRotationVector2().RotatedBy(Main.GlobalTimeWrappedHourly * 0.4f) * 20f;
				bool outOfRange = base.Projectile.Center.Distance(goalPosition) > 120f;
				if ((((Vector2)(ref base.Projectile.velocity)).Length() < 6f) & outOfRange)
				{
					base.Projectile.velocity = base.Projectile.velocity * 0.995f + base.Projectile.Center.DirectionTo(goalPosition) * homingSpeed;
				}
				else if (outOfRange)
				{
					Projectile projectile = base.Projectile;
					projectile.velocity *= 0.985f;
				}
				if (!outOfRange)
				{
					base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.0065f * (float)((energyNumber % 2f != 0f) ? 1 : (-1))) * 1.004f;
				}
				velocity = base.Projectile.velocity;
			}
			else
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.99f;
			}
		}
		else
		{
			if (base.Projectile.ai[2] == 5f)
			{
				time = 0f;
				healing = (float)Owner.statLife < (float)Owner.statLifeMax2 * 0.5f;
				base.Projectile.ai[2]++;
			}
			if (healing)
			{
				startTime = 100;
			}
			if (time <= (float)startTime)
			{
				float timeLerp = Utils.GetLerpValue(0f, (float)startTime * 0.5f, time, clamped: true);
				int distFromPlayer = (int)(160f + (healing ? (240f * Utils.GetLerpValue(0f, (float)startTime * 0.8f, time, clamped: true)) : 0f));
				int moveSpeed = (int)(90f - 85f * timeLerp);
				if (projType == 0f)
				{
					Vector2 destination = Owner.Center + Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).RotatedBy(2.094395160675049) * (float)distFromPlayer;
					base.Projectile.velocity = (destination - base.Projectile.Center) / (float)moveSpeed;
					velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
				}
				if (projType == 1f)
				{
					Vector2 destination2 = Owner.Center + Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) * (float)distFromPlayer;
					base.Projectile.velocity = (destination2 - base.Projectile.Center) / (float)moveSpeed;
					velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
				}
				if (projType == 2f)
				{
					Vector2 destination3 = Owner.Center + Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).RotatedBy(-2.094395160675049) * (float)distFromPlayer;
					base.Projectile.velocity = (destination3 - base.Projectile.Center) / (float)moveSpeed;
					velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
				}
			}
			if (healing)
			{
				if (time > (float)startTime)
				{
					float sine3 = (float)Math.Sin(time * 0.3f / (float)Math.PI);
					base.Projectile.extraUpdates = 6;
					if (time > (float)startTime)
					{
						float homingSpeed2 = Utils.Remap(time, startTime, endTime, 0.01f, 0.1f);
						Vector2 goalPosition2 = Owner.Center;
						if (((Vector2)(ref base.Projectile.velocity)).Length() < 5f)
						{
							base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.02f * sine3) * 0.99f + base.Projectile.Center.DirectionTo(goalPosition2) * homingSpeed2;
						}
						else
						{
							Projectile projectile3 = base.Projectile;
							projectile3.velocity *= 0.985f;
						}
						if (goalPosition2.Distance(base.Projectile.Center) < 50f)
						{
							if (projType == 0f)
							{
								Owner.HealPlayer(8);
							}
							if (projType == 1f)
							{
								Owner.Calamity().dOfTheDeepDefenseBuffTimer = Owner.Calamity().dOfTheDeepDefenseBuffMax;
							}
							if (projType == 2f)
							{
								int healValue = (int)((float)Owner.statLifeMax2 * 0.05f);
								Owner.HealPlayer(healValue);
							}
							base.Projectile.Kill();
						}
					}
				}
			}
			else if (time > (float)startTime)
			{
				if (!canDamage)
				{
					if (visuals)
					{
						SoundStyle style = AstrumDeusHead.GodRaySound with
						{
							Volume = 0.3f,
							Pitch = Main.rand.NextFloat(-0.6f, -0.5f) + projType * 0.1f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						style = StatisVoidSash.VoidDash with
						{
							Volume = 0.3f,
							Pitch = Main.rand.NextFloat(0.2f, 0.4f) + projType * 0.1f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					Vector2 aimTo = base.Projectile.Center.ClosestNPCAt(1200f)?.Center ?? Owner.Calamity().mouseWorld;
					if (projType == 0f)
					{
						base.Projectile.velocity = base.Projectile.Center.DirectionTo(aimTo) * 2f;
						base.Projectile.extraUpdates = 3;
					}
					if (projType == 1f)
					{
						base.Projectile.velocity = base.Projectile.Center.DirectionTo(aimTo) * 11f;
						base.Projectile.extraUpdates = 18;
						if (visuals)
						{
							for (float i2 = 1f; i2 <= 1.4f; i2 += 0.4f)
							{
								GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 7f * i2, "CalamityMod/Particles/BloomRing", affectedByGravity: false, 18, 0.5f / i2, bColor, new Vector2(3f, 1f), useAddativeBlend: true, glowCenter: false, MathHelper.ToRadians(90f), fadeIn: false, affectedByLight: false, 0.9f));
							}
						}
					}
					if (projType == 2f)
					{
						base.Projectile.velocity = base.Projectile.Center.DirectionTo(aimTo) * 7f;
						base.Projectile.extraUpdates = 7;
						base.Projectile.localNPCHitCooldown = 5;
						base.Projectile.timeLeft = 600;
						if (visuals)
						{
							GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 5f, "CalamityMod/Particles/BloomRing", affectedByGravity: false, 14, 0.35f, bColor, new Vector2(3f, 1f), useAddativeBlend: true, glowCenter: false, MathHelper.ToRadians(90f), fadeIn: false, affectedByLight: false, 0.9f));
						}
					}
					if (visuals)
					{
						for (int j = 0; j <= 12; j++)
						{
							float variance2 = Main.rand.NextFloat(-0.7f, 0.7f);
							Vector2 vel2 = (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 24f).RotatedBy(variance2) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance2));
							float scale2 = (Main.rand.NextFloat(1.5f, 1.7f) - Math.Abs(variance2)) * 0.35f;
							Dust dust5 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), vel2);
							dust5.scale = scale2 * 4f;
							dust5.noGravity = true;
							dust5.color = bColor;
							dust5.noLightEmittence = true;
						}
					}
				}
				canDamage = true;
				float homingSpeed3 = Utils.Remap(time, startTime, endTime, 0.01f, 0.5f);
				if (projType == 0f)
				{
					targeted = Owner.Calamity().mouseWorld.ClosestNPCAt(2300f);
					CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, homingSpeed3, 25f, 0.99f, 0.95f, accelerate: true);
					if (targeted == null)
					{
						if (base.Projectile.velocity.Y > 5f)
						{
							base.Projectile.velocity.Y += 0.8f * homingSpeed3;
							base.Projectile.velocity.X *= 0.997f;
						}
					}
					else
					{
						base.Projectile.timeLeft++;
					}
				}
				if (projType == 2f)
				{
					if (hitCooldown > 0)
					{
						hitCooldown--;
					}
					float timeLerp2 = Utils.GetLerpValue(400f, 800f, base.Projectile.timeLeft, clamped: true);
					if (targeted == null && hitCooldown == 0)
					{
						CalamityUtils.HomeInOnSelectedNPC(base.Projectile, Owner.Calamity().mouseWorld.ClosestNPCAt(2300f), ignoreTiles: true, 0.5f, 10f, 0.99f, 0.95f, accelerate: true);
					}
					else if (hitCooldown == 0)
					{
						base.Projectile.timeLeft += 2;
						CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.6f * timeLerp2, 10f, 0.99f, 0.95f, accelerate: true);
					}
					else if (hitCooldown < 100)
					{
						base.Projectile.velocity = base.Projectile.velocity.RotatedBy((float)((base.Projectile.numHits % 2 == 0) ? 1 : (-1)) * 0.0363f) * 0.99f;
					}
					if (targeted != null && (targeted.life <= 0 || !targeted.CanBeChasedBy() || !targeted.active))
					{
						targeted = null;
					}
				}
			}
		}
		float squash = Utils.GetLerpValue(1f, 3f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (squash > 0.15f && visuals && targetDist < 1400f)
		{
			float scale3 = 0.55f * base.Projectile.scale * (float)((projType != 1f || !canDamage) ? 1 : 2);
			int lifetime = (int)(18f * ((projType == 1f && canDamage) ? 2.5f : 1f));
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.01f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, lifetime, scale3, bColor * 0.4f * squash, new Vector2(1f - 0.15f * squash, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.3f * squash, 0.45f, 0.5f));
		}
		base.Projectile.scale = MathHelper.Lerp(base.Projectile.scale, 0.7f, 0.1f);
		base.Projectile.rotation = velocity.ToRotation() + (float)Math.PI / 2f;
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		if (projType == 0f)
		{
			if (visuals)
			{
				SoundStyle style = DeusMine.ExplodeSound with
				{
					Volume = 0.7f,
					Pitch = Main.rand.NextFloat(0.3f, 0.4f)
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				style = StatisVoidSash.VoidDash with
				{
					Volume = 0.4f,
					Pitch = Main.rand.NextFloat(-0.2f, -0.3f)
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int i = 0; i < 15; i++)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>());
					dust.velocity = ((float)Math.PI * 2f * (float)i / 15f).ToRotationVector2() * 16f * ((i % 3 == 0) ? 0.8f : 1f);
					dust.scale = Main.rand.NextFloat(1.3f, 1.6f) * 0.9f * ((i % 3 == 0) ? 2.2f : 1.8f);
					dust.noGravity = true;
					dust.color = bColor;
					dust.noLightEmittence = true;
					dust.fadeIn = 0.9f;
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, bColor * 0.7f, "CalamityMod/Particles/BloomRing", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.5f, 1.8f, 13, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			float blastSize = 170f;
			float minMultiplier = 0.35f;
			int hitsToMinMult = 8;
			int debuff = ModContent.BuffType<CrushDepth>();
			int debuffTime = 240;
			target.AddBuff(debuff, debuffTime);
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurstExclusive>(), base.Projectile.damage, -11f, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult);
			projectile.timeLeft = 14;
			projectile.DamageType = base.Projectile.DamageType;
			projectile.localAI[0] = target.whoAmI;
			projectile.localAI[1] = debuff;
			projectile.localAI[2] = debuffTime;
			base.Projectile.Kill();
		}
		else
		{
			Vector2 launchVel = base.Projectile.Center.DirectionTo(target.Center) - Vector2.UnitY;
			float launchPower = 7f;
			target.MoveNPC(launchVel, launchPower);
			if (visuals)
			{
				for (int j = 0; j <= Math.Max(2, 8 - base.Projectile.numHits); j++)
				{
					float variance = Main.rand.NextFloat(-0.3f, 0.3f);
					Vector2 vel = (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 24f).RotatedBy(variance) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance));
					float scale = (Main.rand.NextFloat(1.5f, 1.7f) - Math.Abs(variance)) * 0.35f;
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), vel);
					dust2.scale = scale * 5f;
					dust2.noGravity = true;
					dust2.color = bColor;
					dust2.noLightEmittence = true;
					dust2.fadeIn = 1.5f;
				}
			}
		}
		if (projType == 1f)
		{
			if (visuals)
			{
				SoundStyle style = AuricOre.MineSound with
				{
					Volume = 0.5f,
					Pitch = -0.3f + (float)base.Projectile.numHits * 0.1f,
					MaxInstances = 6
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			target.AddBuff(323, 300);
			float minMult = 0.25f;
			int hitsToMinMult2 = 10;
			float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult2, 1f, minMult);
			modifiers.SourceDamage *= damageMult * 2.5f;
		}
		if (projType == 2f)
		{
			modifiers.SourceDamage *= 0.33f;
			if (visuals)
			{
				SoundStyle style = CommonCalamitySounds.VoidstoneMine with
				{
					Volume = 1f
				};
				style = style with
				{
					Volume = 0.6f,
					Pitch = -0.3f + (float)base.Projectile.numHits * 0.1f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 7f;
			target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 30);
			targeted = target;
			hitCooldown = 140;
			base.Projectile.timeLeft = 600;
			base.Projectile.extraUpdates += 3;
			if (base.Projectile.numHits >= 5)
			{
				base.Projectile.Kill();
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		if (healing && visuals)
		{
			for (int i = 0; i <= 4; i++)
			{
				float variance = Main.rand.NextFloat(-0.5f, 0.5f);
				Vector2 vel = (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 4f).RotatedBy(variance) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance)) * 4f;
				float scale = (Main.rand.NextFloat(1.5f, 1.7f) - Math.Abs(variance)) * 0.35f;
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), vel);
				dust.scale = scale * 2f;
				dust.noGravity = false;
				dust.color = bColor;
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0886: Unknown result type (might be due to invalid IL or missing references)
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_091d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0922: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_072a: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0769: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Unknown result type (might be due to invalid IL or missing references)
		//IL_078e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_093b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_094a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Unknown result type (might be due to invalid IL or missing references)
		//IL_0954: Unknown result type (might be due to invalid IL or missing references)
		//IL_0959: Unknown result type (might be due to invalid IL or missing references)
		//IL_095e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0960: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_097f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0986: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_099a: Unknown result type (might be due to invalid IL or missing references)
		//IL_099f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> orb = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		float sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 10f / (float)Math.PI);
		Vector2 squash = default(Vector2);
		((Vector2)(ref squash))._002Ector(Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 1f, 5f, 1f, 0.6f), Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 1f, 5f, 1f, 2f));
		Asset<Texture2D> block = TextureAssets.Item[ModContent.ItemType<AbyssGravel>()];
		float num = projType;
		if (num != 0f)
		{
			if (num != 1f)
			{
				if (num == 2f)
				{
					block = TextureAssets.Item[ModContent.ItemType<Voidstone>()];
				}
			}
			else
			{
				block = TextureAssets.Item[ModContent.ItemType<PyreMantle>()];
			}
		}
		else
		{
			block = TextureAssets.Item[ModContent.ItemType<AbyssGravel>()];
		}
		Color val;
		for (int i = 0; i < 6; i++)
		{
			val = Color.Lerp(bColor, color2, (float)((i + 1) / 6));
			((Color)(ref val)).A = 0;
			Color orbColor = val * 0.4f;
			Vector2 scale = base.Projectile.scale * squash * (0.05f + (float)i * 0.01f) * 4.3f;
			Texture2D value = orb.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition;
			val = Color.White;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, position, null, Color.Lerp(orbColor, val, 1f - (float)i * 0.5f) * (visuals ? 1f : 0.15f), base.Projectile.rotation, orb.Size() * 0.5f, scale, (SpriteEffects)0);
		}
		float velLerp = Utils.GetLerpValue(0.5f, 3f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
		if (projType == 0f && visuals)
		{
			Asset<Texture2D> ring = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomRing", (AssetRequestMode)2);
			int orbs = 9;
			for (int j = 1; j < orbs + 1; j++)
			{
				Math.Sin(time * ((j % 3 == 0) ? 0.2f : ((j % 2 == 0) ? 0.4f : 1f)) * 0.2f / (float)Math.PI);
				Vector2 placement = base.Projectile.Center + ((float)Math.PI * 2f * (float)j / (float)orbs + 7f * sine).ToRotationVector2() * (18f + Math.Abs(sine * (float)((j % 3 == 0) ? 19 : 11))) * velLerp;
				Color orbColor2 = Color.White * (visuals ? 1f : 0.1f) * velLerp;
				Vector2 scale2 = Vector2.One * base.Projectile.scale;
				for (int y = 0; y < 7; y++)
				{
					Vector2 drawOffset = ((float)Math.PI * 2f * (float)y / 7f).ToRotationVector2() * 4.5f;
					Texture2D value2 = block.Value;
					Vector2 position2 = placement - Main.screenPosition + drawOffset;
					val = bColor;
					((Color)(ref val)).A = 0;
					Main.EntitySpriteDraw(value2, position2, null, val * 0.7f * velLerp, base.Projectile.rotation + 0.02f * MathHelper.Lerp((float)j, 1f, 0.75f) + 0.4f, block.Size() * 0.5f, scale2, (SpriteEffects)(j % 2 == 0));
				}
				Main.EntitySpriteDraw(block.Value, placement - Main.screenPosition, null, orbColor2, base.Projectile.rotation + 0.02f * MathHelper.Lerp((float)j, 1f, 0.75f) + 0.4f, block.Size() * 0.5f, scale2, (SpriteEffects)(j % 2 == 0));
			}
			for (int k = 0; k < 3; k++)
			{
				Texture2D value3 = ring.Value;
				Vector2 position3 = base.Projectile.Center - Main.screenPosition;
				val = bColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value3, position3, null, val * 0.9f * velLerp, base.Projectile.rotation, ring.Size() * 0.5f, 0.2f * velLerp * base.Projectile.scale + 0.15f * Math.Abs(sine) + 0.02f * (float)k, (SpriteEffects)0);
			}
		}
		if (projType == 1f && visuals)
		{
			int orbs2 = 9;
			for (int l = 0; l < orbs2; l++)
			{
				bool outer = l > 2;
				bool outest = l > 5;
				float rotation = (float)Math.PI * 2f * (float)l / 3f + Main.GlobalTimeWrappedHourly * 6f;
				Vector2 placement2 = base.Projectile.Center + (rotation.ToRotationVector2() * (float)(outest ? 16 : (outer ? 12 : 8)) + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * (outest ? (-15f) : (outer ? 0f : 15f))) * velLerp;
				Color orbColor3 = Color.White * (visuals ? 1f : 0.1f) * velLerp;
				Vector2 scale3 = new Vector2(0.35f + (outest ? 0.1f : (outer ? 0.3f : 0.6f)), 2.3f) * base.Projectile.scale * (outest ? 0.8f : (outer ? 0.9f : 1f)) * 1.5f;
				for (int m = 0; m < 7; m++)
				{
					Vector2 drawOffset2 = ((float)Math.PI * 2f * (float)m / 7f).ToRotationVector2() * 2.5f;
					Texture2D value4 = block.Value;
					Vector2 position4 = placement2 - Main.screenPosition + drawOffset2;
					val = bColor;
					((Color)(ref val)).A = 0;
					Main.EntitySpriteDraw(value4, position4, null, val * velLerp, placement2.DirectionTo(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 65f).ToRotation() + (float)Math.PI / 2f, block.Size() * 0.5f, scale3, (SpriteEffects)(outer ? 1 : 0));
				}
				Main.EntitySpriteDraw(block.Value, placement2 - Main.screenPosition, null, orbColor3, placement2.DirectionTo(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 65f).ToRotation() + (float)Math.PI / 2f, block.Size() * 0.5f, scale3, (SpriteEffects)(outer ? 1 : 0));
			}
		}
		if (projType == 2f && visuals)
		{
			float scalingValue = Main.GlobalTimeWrappedHourly * (float)(90 + base.Projectile.numHits * 55);
			int orbs3 = 8;
			for (int n = 1; n < orbs3 + 1; n++)
			{
				float sine3 = (float)Math.Sin(scalingValue * ((n % 3 == 0) ? 0.2f : ((n % 2 == 0) ? 0.4f : 1f)) * 0.07f / (float)Math.PI);
				float sine4 = (float)Math.Sin(scalingValue * ((n % 3 == 0) ? 0.2f : ((n % 2 == 0) ? 0.4f : 1f)) * 0.2f / (float)Math.PI);
				Vector2 velocity = base.Projectile.Center + ((float)Math.PI * 2f * (float)n / (float)orbs3 + scalingValue * 0.04f).ToRotationVector2() * (23f + 8f * sine3) * Math.Abs(sine4) * velLerp;
				Color orbColor4 = Color.White * (visuals ? 1f : 0.1f) * velLerp;
				Vector2 scale4 = new Vector2((n % 2 == 0) ? 0.4f : 0.6f, (n % 2 == 0) ? 1f : 1.3f) * base.Projectile.scale * 1.3f;
				for (int num2 = 0; num2 < 7; num2++)
				{
					Vector2 drawOffset3 = ((float)Math.PI * 2f * (float)num2 / 7f).ToRotationVector2() * 4.5f;
					Texture2D value5 = block.Value;
					Vector2 position5 = velocity - Main.screenPosition + drawOffset3;
					val = bColor;
					((Color)(ref val)).A = 0;
					Main.EntitySpriteDraw(value5, position5, null, val * 0.7f * velLerp, velocity.DirectionTo(base.Projectile.Center).ToRotation() + (float)Math.PI / 2f * sine3, block.Size() * 0.5f, scale4, (SpriteEffects)(n % 2 == 0));
				}
				Main.EntitySpriteDraw(block.Value, velocity - Main.screenPosition, null, orbColor4, velocity.DirectionTo(base.Projectile.Center).ToRotation() + (float)Math.PI / 2f * sine3, block.Size() * 0.5f, scale4, (SpriteEffects)(n % 2 == 0));
			}
		}
		return false;
	}

	public override bool? CanDamage()
	{
		if (!canDamage)
		{
			return false;
		}
		return null;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (!target.CanBeChasedBy())
		{
			return false;
		}
		if (projType == 2f)
		{
			if (hitCooldown > 0 || (targeted != null && target != targeted))
			{
				return false;
			}
			return null;
		}
		return null;
	}

	public DiamondOfTheDeepProjectile()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		bColor = Color.White;
		color1 = Color.White;
		color2 = Color.White;
		base._002Ector();
	}
}
