using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class NullShot : ModProjectile, ILocalizedModType, IModType
{
	public Color baseColor;

	public int sineDir;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 25;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 400;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 4;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (time == 0f)
		{
			base.Projectile.scale = ((base.Projectile.ai[1] == 5f) ? 2.2f : 1.5f);
			sineDir = (Main.rand.NextBool() ? 1 : (-1));
		}
		float rate = Main.GlobalTimeWrappedHourly * 5f;
		List<Color> eColors = new List<Color>
		{
			Color.Turquoise,
			Color.Orchid
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		if (!Main.zenithWorld)
		{
			baseColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		}
		if (base.Projectile.ai[1] == 5f && !Main.zenithWorld)
		{
			baseColor = Color.White;
		}
		if (time == 5f)
		{
			for (int i = 0; i < 4; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, (base.Projectile.ai[1] == 5f) ? ModContent.DustType<VoidDust>() : ModContent.DustType<LightDust>(), (base.Projectile.velocity * 4f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.2f, 1f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.15f, 1.35f);
				dust.color = baseColor;
			}
		}
		if (time > 20f)
		{
			if (base.Projectile.ai[1] == 5f)
			{
				float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.575f / (float)Math.PI);
				Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 16f;
				float scale = Main.rand.NextFloat(0.8f, 1.1f);
				if (Main.rand.NextBool(2))
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + offset * (float)sineDir, ModContent.DustType<VoidDust>(), -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
					dust2.noGravity = true;
					dust2.scale = scale;
					dust2.color = baseColor;
				}
				if (Main.rand.NextBool(2))
				{
					Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center - offset * (float)sineDir, ModContent.DustType<VoidDustInverted>(), -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
					dust3.noGravity = true;
					dust3.scale = scale;
					dust3.color = baseColor;
				}
			}
			else if (Main.rand.NextBool(13))
			{
				Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), (base.Projectile.ai[1] != 5f) ? ModContent.DustType<LightDust>() : (Main.rand.NextBool() ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>()), -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
				dust4.noGravity = true;
				dust4.scale = Main.rand.NextFloat(1.05f, 1.65f);
				dust4.color = baseColor;
			}
		}
		if (time > 13f && time < 34f && base.Projectile.ai[2] > 0f)
		{
			Projectile projectile = base.Projectile;
			projectile.Center += base.Projectile.velocity.RotatedBy((base.Projectile.ai[2] == 1f) ? ((float)Math.PI / 2f) : (-(float)Math.PI / 2f)) * 0.2f;
		}
		if (base.Projectile.ai[1] == 5f)
		{
			NPC targetedNPC = base.Projectile.Center.ClosestNPCAt(700f);
			if (targetedNPC != null && time > 30f && base.Projectile.numHits < 1 && Vector2.Distance(targetedNPC.Center, base.Projectile.Center) < 700f)
			{
				CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targetedNPC, ignoreTiles: true, 0.2f, 8f, 0.97f, 0.95f, accelerate: true);
			}
		}
		else
		{
			if (base.Projectile.timeLeft < 100)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.96f;
				base.Projectile.scale *= 0.98f;
			}
			base.Projectile.timeLeft--;
			if (base.Projectile.timeLeft <= 1)
			{
				for (int j = 0; j < 4; j++)
				{
					GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, (base.Projectile.velocity * 5f).RotatedByRandom(0.05000000074505806) * Main.rand.NextFloat(0.1f, 1f), affectedByGravity: false, Main.rand.Next(20, 29), Main.rand.NextFloat(0.6f, 1.3f), baseColor));
				}
			}
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 5f)
		{
			for (int i = 0; i < 8; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, (base.Projectile.ai[1] == 5f) ? (Main.rand.NextBool() ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>()) : ModContent.DustType<LightDust>(), (base.Projectile.velocity * 3f).RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.2f, 1f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.15f, 1.45f);
				dust.color = baseColor;
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.3f, 0.65f, 13, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/SmallBloomRingLayered", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.4f, 0.75f, 13, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/SmallBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.4f, 0.25f, 16, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		else
		{
			for (int j = 0; j < 3; j++)
			{
				GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, (base.Projectile.velocity * 2f).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(0.1f, 1f), affectedByGravity: false, Main.rand.Next(20, 29), Main.rand.NextFloat(0.6f, 1.3f), baseColor));
			}
		}
		SoundEngine.PlaySound(NullificationPistol.HitSound with
		{
			Volume = ((base.Projectile.ai[1] == 5f) ? 1f : 0.7f),
			Pitch = Main.rand.NextFloat(0f, 0.1f) * (float)((base.Projectile.ai[1] != 5f) ? 1 : 3)
		}, base.Projectile.Center);
		if (!Main.zenithWorld)
		{
			return;
		}
		if (base.Projectile.ai[1] == 5f)
		{
			if (Main.player[base.Projectile.owner].Center.Distance(target.Center) > 1000f)
			{
				target.velocity = target.Center.DirectionTo(Main.player[base.Projectile.owner].Center);
			}
			if (Main.rand.NextBool(4))
			{
				target.aiStyle = Main.rand.Next(2, 141);
				return;
			}
			switch (Main.rand.Next(4))
			{
			case 0:
				target.ai[0] = Main.rand.Next(0, 101);
				target.localAI[0] = Main.rand.Next(0, 101);
				break;
			case 1:
				target.ai[1] = Main.rand.Next(0, 101);
				target.localAI[1] = Main.rand.Next(0, 101);
				break;
			case 2:
				target.ai[2] = Main.rand.Next(0, 101);
				target.localAI[2] = Main.rand.Next(0, 101);
				break;
			case 3:
				target.ai[3] = Main.rand.Next(0, 101);
				target.localAI[3] = Main.rand.Next(0, 101);
				break;
			}
			return;
		}
		switch (Main.rand.Next(8))
		{
		case 0:
			if (target.type != ModContent.NPCType<SuperDummyNPC>())
			{
				target.damage += Main.rand.Next(5, 41);
			}
			break;
		case 1:
			target.damage -= Main.rand.Next(5, 41);
			break;
		case 2:
			target.knockBackResist = 0f;
			break;
		case 3:
			target.knockBackResist = Main.rand.Next(1, 4);
			break;
		case 4:
			target.defense += Main.rand.Next(5, 31);
			break;
		case 5:
			target.defense -= Main.rand.Next(5, 31);
			break;
		case 6:
			target.scale *= 2f;
			break;
		case 7:
			target.scale *= 0.5f;
			break;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.97f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		if (time < 18f)
		{
			return false;
		}
		Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Particles/DrainLineBloom", (AssetRequestMode)2);
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/DrainLine", (AssetRequestMode)2);
		Projectile projectile = base.Projectile;
		int mode = ProjectileID.Sets.TrailingMode[base.Type];
		Color val = baseColor;
		((Color)(ref val)).A = 0;
		CalamityUtils.DrawAfterimagesCentered(projectile, mode, val * 0.35f, 1, tex.Value);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], (base.Projectile.ai[1] == 5f) ? Color.Black : Color.Lerp(baseColor, Color.White, 0.5f), 1, tex2.Value, drawCentered: true, shrink: true);
		return false;
	}

	public NullShot()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		baseColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		sineDir = 1;
		base._002Ector();
	}
}
