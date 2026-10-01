using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BatholithBangleProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public int damageTime;

	public int Soundtime1;

	public int Soundtime2;

	public int Soundtime3;

	public int Soundtime4;

	public SlotId SoundSlot;

	public Color clr1;

	public Color clr2;

	public Color clr3;

	public int spinDir;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public bool invalidTarget
	{
		get
		{
			if (!(base.Projectile.ai[0] < 0f) && !(base.Projectile.ai[0] > 199f) && Main.npc[(int)base.Projectile.ai[0]].active)
			{
				return Main.npc[(int)base.Projectile.ai[0]].life <= 0;
			}
			return true;
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public bool visual => Owner.Calamity().batholithBangleVisual;

	public override void SetDefaults()
	{
		base.Projectile.width = 120;
		base.Projectile.height = 120;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = damageTime + 2;
		base.Projectile.ArmorPenetration = 35;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.scale = 0.8f;
	}

	public override void AI()
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0 && visual)
		{
			spinDir = (Main.rand.NextBool() ? 1 : (-1));
			SoundStyle sound = new SoundStyle("CalamityMod/Sounds/Item/BatholithBangleSound");
			SoundSlot = SoundEngine.PlaySound(sound with
			{
				Volume = 1f,
				MaxInstances = -1
			}, base.Projectile.Center);
		}
		if (!invalidTarget)
		{
			base.Projectile.Center = Main.npc[(int)base.Projectile.ai[0]].Center;
		}
		else if (time < Soundtime2)
		{
			NPC target = base.Projectile.Center.ClosestNPCAt(400f, ignoreTiles: true, bossPriority: true);
			base.Projectile.ai[0] = target?.whoAmI ?? (-1);
		}
		if (time > Soundtime1)
		{
			base.Projectile.rotation += 0.23f * (float)spinDir * (float)Math.Pow(Utils.GetLerpValue(damageTime, Soundtime2, time, clamped: true), 1.0);
		}
		float stabLerp = (float)Math.Pow(Utils.GetLerpValue(Soundtime4, damageTime, time, clamped: true), 8.0);
		Vector2 triPlace = Vector2.UnitY * (230f - 150f * stabLerp) * base.Projectile.scale;
		if (time == Soundtime2)
		{
			MakePusle(base.Projectile.Center + triPlace.RotatedBy(base.Projectile.rotation));
		}
		if (time == Soundtime3)
		{
			MakePusle(base.Projectile.Center + triPlace.RotatedBy(base.Projectile.rotation + (float)Math.PI * 2f / 3f));
		}
		if (time == Soundtime4)
		{
			MakePusle(base.Projectile.Center + triPlace.RotatedBy(base.Projectile.rotation - (float)Math.PI * 2f / 3f));
		}
		if (time == damageTime)
		{
			float visMult = (visual ? 1f : 0.6f);
			if (visual)
			{
				Owner.SetScreenshake(5f);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 12, 1.35f, clr3, base.Projectile.scale * new Vector2(2.5f, 1.3f), useAddativeBlend: true, glowCenter: true, (float)Math.PI / 2f, fadeIn: false, affectedByLight: false, 1.25f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 10, 1.05f, clr3, base.Projectile.scale * new Vector2(2f, 1.3f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.7f));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, clr1, "CalamityMod/Particles/BloomRing", base.Projectile.scale * new Vector2(0.6f, 1.4f), 0f, 0.3f, 1.35f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, clr1, "CalamityMod/Particles/BloomRing", base.Projectile.scale * new Vector2(1.3f, 0.7f), 0f, 0.25f, 1f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int i = 0; i < 12; i++)
			{
				Vector2 vel = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(5f, 10f);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, vel, "CalamityMod/Particles/GlowTriangle", affectedByGravity: true, Main.rand.Next(30, 56), base.Projectile.scale * Main.rand.NextFloat(0.12f, 0.18f), (Main.rand.NextBool() ? clr1 : clr3) * visMult, Vector2.One, useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-4f, 4f), fadeIn: false, affectedByLight: false, 0f, 1f, 1f, flipHorizontal: false, noShrink: false, Main.rand.NextFloat(-0.5f, 0.5f)));
			}
		}
		if (SoundEngine.TryGetActiveSound(SoundSlot, out ActiveSound Sound) && Sound.IsPlaying)
		{
			Sound.Position = base.Projectile.Center;
		}
		time++;
	}

	public void MakePusle(Vector2 position)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if (visual)
		{
			for (int i = 0; i < 8; i++)
			{
				Vector2 outerVel = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(1f, 5f);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(position + outerVel * 5f, outerVel, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, Main.rand.Next(18, 26), base.Projectile.scale * Main.rand.NextFloat(0.3f, 0.4f), Main.rand.NextBool() ? clr1 : clr3, Vector2.One, useAddativeBlend: true, glowCenter: true, Main.rand.NextFloat(-4f, 4f), fadeIn: false, affectedByLight: false, 0f, 1f, 0.8f, flipHorizontal: false, noShrink: false, Main.rand.NextFloat(-0.3f, 0.3f)));
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		modifiers.SetCrit();
		float critDamage = Math.Min(Owner.GetTotalCritChance(AverageDamageClass.Instance) * 0.01f, 1f);
		modifiers.SourceDamage *= 1f + critDamage;
		Vector2 launchVel = Owner.Center.DirectionTo(target.Center) - Vector2.UnitY;
		float launchPower = 9f;
		target.MoveNPC(launchVel, launchPower, ignoreKBImmune: true);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * 0.5f, targetHitbox);
	}

	public override bool? CanDamage()
	{
		if (time >= damageTime && base.Projectile.numHits <= 0)
		{
			return null;
		}
		return false;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if ((invalidTarget || base.Projectile.ai[0] == (float)target.whoAmI) && base.Projectile.numHits <= 0)
		{
			return null;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		if (!visual)
		{
			return false;
		}
		Asset<Texture2D> tri = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowTriangle", (AssetRequestMode)2);
		Asset<Texture2D> shine = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		float stabLerp = (float)Math.Pow(Utils.GetLerpValue(Soundtime4, damageTime, time, clamped: true), 8.0);
		Vector2 triPlace = Vector2.UnitY * (230f - 150f * stabLerp) * base.Projectile.scale;
		Color color;
		for (int i = 0; i < 6; i++)
		{
			float scale = base.Projectile.scale * (0.3f - 0.015f * (float)i);
			float rotation = base.Projectile.rotation;
			float spawnScaleBonus = 0.3f;
			float squashPower = 0.4f;
			Color triColor = Color.Lerp(clr1, clr3, (float)i * 0.14f);
			if (time >= Soundtime2)
			{
				float lerp = (float)Math.Pow(Utils.GetLerpValue(Soundtime2 + 13, Soundtime2, time, clamped: true), 2.0);
				Texture2D value = tri.Value;
				Vector2 position = base.Projectile.Center + triPlace.RotatedBy(rotation) - Main.screenPosition;
				color = triColor;
				((Color)(ref color)).A = 0;
				Main.EntitySpriteDraw(value, position, null, color, rotation + (float)Math.PI * lerp, tri.Size() * 0.5f, base.Projectile.scale * new Vector2(0.05f * (float)i + 1f - squashPower * (1f - lerp), -0.05f * (float)i + 1f + squashPower * (1f - lerp)) * (scale + spawnScaleBonus * lerp), (SpriteEffects)0);
			}
			if (time >= Soundtime3)
			{
				rotation += (float)Math.PI * 2f / 3f;
				float lerp2 = (float)Math.Pow(Utils.GetLerpValue(Soundtime3 + 13, Soundtime3, time, clamped: true), 2.0);
				Texture2D value2 = tri.Value;
				Vector2 position2 = base.Projectile.Center + triPlace.RotatedBy(rotation) - Main.screenPosition;
				color = triColor;
				((Color)(ref color)).A = 0;
				Main.EntitySpriteDraw(value2, position2, null, color, rotation + (float)Math.PI * lerp2, tri.Size() * 0.5f, base.Projectile.scale * new Vector2(0.05f * (float)i + 1f - squashPower * (1f - lerp2), -0.05f * (float)i + 1f + squashPower * (1f - lerp2)) * (scale + spawnScaleBonus * lerp2), (SpriteEffects)0);
			}
			if (time >= Soundtime4)
			{
				rotation += (float)Math.PI * 2f / 3f;
				float lerp3 = (float)Math.Pow(Utils.GetLerpValue(Soundtime4 + 13, Soundtime4, time, clamped: true), 2.0);
				Texture2D value3 = tri.Value;
				Vector2 position3 = base.Projectile.Center + triPlace.RotatedBy(rotation) - Main.screenPosition;
				color = triColor;
				((Color)(ref color)).A = 0;
				Main.EntitySpriteDraw(value3, position3, null, color, rotation + (float)Math.PI * lerp3, tri.Size() * 0.5f, base.Projectile.scale * new Vector2(0.05f * (float)i + 1f - squashPower * (1f - lerp3), -0.05f * (float)i + 1f + squashPower * (1f - lerp3)) * (scale + spawnScaleBonus * lerp3), (SpriteEffects)0);
			}
		}
		for (int j = 0; j < 4; j++)
		{
			float shineLerp = (float)Math.Pow(Utils.GetLerpValue(0f, Soundtime4, time, clamped: true), 3.0);
			Color shineColor = Color.Lerp(Color.Lerp(clr1, clr3, (float)j * 0.3f), Color.White, (float)j * 0.2f) * shineLerp;
			float shineScale = base.Projectile.scale * 0.4f + 0.3f * shineLerp;
			Texture2D value4 = shine.Value;
			Vector2 position4 = base.Projectile.Center - Main.screenPosition;
			color = shineColor;
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(value4, position4, null, color, (float)Math.PI / 2f, shine.Size() * 0.5f, new Vector2(0.5f - (float)j * 0.15f, 1f + (float)j * 0.2f) * (1f - (float)j * 0.2f) * shineScale, (SpriteEffects)0);
			Texture2D value5 = shine.Value;
			Vector2 position5 = base.Projectile.Center - Main.screenPosition;
			color = shineColor;
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(value5, position5, null, color, 0f, shine.Size() * 0.5f, new Vector2(0.5f - (float)j * 0.15f, 1f + (float)j * 0.2f) * (1f - (float)j * 0.1f) * shineScale, (SpriteEffects)0);
		}
		return false;
	}

	public BatholithBangleProjectile()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		damageTime = 120;
		Soundtime1 = 84;
		Soundtime2 = 90;
		Soundtime3 = 96;
		Soundtime4 = 107;
		clr1 = new Color(59, 28, 136);
		clr2 = new Color(16, 14, 36);
		clr3 = new Color(23, 186, 218);
		spinDir = 1;
		base._002Ector();
	}
}
