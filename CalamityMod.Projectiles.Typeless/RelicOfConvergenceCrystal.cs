using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Enums;
using CalamityMod.Items.Tools;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class RelicOfConvergenceCrystal : ModProjectile
{
	public int SoundInterval = 25;

	public int TotalCrystalsToDraw = 3;

	public int CrystalsDrawTime = 40;

	public float MaxCrystalOffsetRadius = 80f;

	public float MaxDustOffsetRadius = 70f;

	public List<bool> healList = new List<bool>(new bool[255]);

	public float completion;

	public float fade;

	public int killTimer;

	public Vector2 mousePos;

	public bool playSound = true;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<RelicOfConvergence>();

	private Player Owner => Main.player[base.Projectile.owner];

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 46;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 125;
	}

	public override void AI()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		completion = Utils.GetLerpValue(120f, 0f, base.Projectile.timeLeft, clamped: true);
		fade = MathHelper.Lerp(fade, 0f, 0.04f);
		if (base.Projectile.timeLeft >= 5)
		{
			mousePos = Owner.ClampedMouseWorld();
		}
		if (Owner.channel)
		{
			killTimer = 18;
		}
		if (killTimer <= 0)
		{
			base.Projectile.Kill();
			return;
		}
		if (Owner.Calamity().profanedSoulRelicBuff)
		{
			base.Projectile.extraUpdates = 1;
		}
		killTimer--;
		UpdatePlayerVisuals(Owner);
		if (base.Projectile.soundDelay <= 0)
		{
			SoundStyle style = SoundID.DD2_WitherBeastCrystalImpact with
			{
				Volume = 0.5f * (float)((time >= (float)CrystalsDrawTime) ? 1 : 2),
				Pitch = 0.5f * completion
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (time >= (float)CrystalsDrawTime)
			{
				style = new SoundStyle("CalamityMod/Sounds/Item/NullHit");
				style.Volume = 0.4f;
				style.Pitch = -0.3f + 0.7f * completion;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				float numberOfDusts = 10f;
				for (int i = 0; (float)i < numberOfDusts; i++)
				{
					GeneralParticleHandler.SpawnParticle(new VelChangingSpark(base.Projectile.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(9f, 18f), mousePos.DirectionFrom(base.Projectile.Center) * 35f, "CalamityMod/Particles/BloomCircle", 25, Main.rand.NextFloat(0.1f, 0.35f) * completion, Color.Lerp(Color.Orange, Color.Orchid, completion), new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.15f, 0.04f));
				}
			}
			base.Projectile.soundDelay = (int)((float)SoundInterval * ((time >= (float)CrystalsDrawTime) ? (1f - 0.9f * completion) : 0.5f));
			fade = 1f;
		}
		if (base.Projectile.timeLeft == 5)
		{
			for (int playerIndex = 0; playerIndex < 255; playerIndex++)
			{
				Player player = Main.player[playerIndex];
				if (player.Center.DistanceSQ(mousePos) < 19044f && player.team == Owner.team && !healList[playerIndex])
				{
					healList[playerIndex] = true;
					int trueHealValue = (int)((float)RelicOfConvergence.HealValue * ((player.whoAmI == Owner.whoAmI) ? 1f : 1.5f) * (Owner.Calamity().profanedSoulRelicBuff ? 1.25f : 1f));
					player.HealPlayer(trueHealValue, HealTextType.Local);
					if (playSound)
					{
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianHeal");
						style.Volume = 1f;
						style.MaxInstances = -1;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						playSound = false;
					}
					for (int j = 0; j < 5; j++)
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(player.Center + Main.rand.NextVector2Circular(15f, 15f), -Vector2.UnitY * Main.rand.NextFloat(0.2f, 3f), "CalamityMod/Particles/HealingPlus", affectedByGravity: false, Main.rand.Next(35, 51), Main.rand.NextFloat(1.1f, 1.9f), Color.Lerp(Color.Orchid, Color.White, (float)j * 0.1f), Vector2.One, useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.1f));
					}
				}
			}
		}
		if (time >= (float)CrystalsDrawTime)
		{
			GeneratePassiveDust(Owner);
			Vector2 center = base.Projectile.Center;
			Color val = Color.Lerp(Color.Orange, Color.Orchid, completion);
			Lighting.AddLight(center, ((Color)(ref val)).ToVector3() * (2.5f * (completion - 0.375f) + fade));
		}
		time++;
	}

	public void UpdatePlayerVisuals(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Vector2 vel = player.Center.DirectionTo(mousePos);
		float rot = vel.ToRotation() + ((player.direction == -1) ? MathHelper.ToRadians(270f) : MathHelper.ToRadians(-90f));
		player.ChangeDir(MathF.Sign(vel.X));
		base.Projectile.Center = player.Center + vel * 15f;
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rot);
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, rot);
	}

	public void GeneratePassiveDust(Player player)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		float radius = 45f;
		radius = MathHelper.Lerp(0f, 200f, completion - 0.375f);
		for (float angle = 0f; angle <= (float)Math.PI * 2f; angle += MathHelper.ToRadians(Main.rand.NextFloat(6f, 8f)))
		{
			Vector2 drawPos = mousePos + angle.ToRotationVector2() * radius;
			Color useColor = Color.Lerp(Color.Orange, Color.Orchid, completion) * (completion - 0.25f);
			float particleScale = 0.01f + fade * 0.08f + completion * 0.08f;
			GeneralParticleHandler.SpawnParticle(new CustomSpark(drawPos, mousePos.DirectionTo(drawPos), "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 4, particleScale, useColor, new Vector2(0.5f + completion, (2f - completion) * 7f - completion * 7f)));
			if (Main.rand.NextBool(70))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + angle.ToRotationVector2() * radius, ModContent.DustType<LightDust>());
				dust.position = mousePos + angle.ToRotationVector2() * radius;
				dust.scale = Main.rand.NextFloat(1.4f, 1.9f) * completion;
				dust.noGravity = false;
				dust.velocity = new Vector2(0f, Main.rand.NextFloat(1f, 5f));
				dust.color = useColor;
			}
			if (base.Projectile.timeLeft == 5)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + angle.ToRotationVector2() * radius, ModContent.DustType<LightDust>());
				dust2.position = drawPos;
				dust2.scale = Main.rand.NextFloat(1.6f, 1.9f);
				dust2.noGravity = !Main.rand.NextBool(5);
				dust2.velocity = mousePos.DirectionTo(drawPos) * Main.rand.NextFloat(2f, 4f);
				dust2.color = Color.Orchid;
				dust2.noLightEmittence = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		_ = time / (float)CrystalsDrawTime;
		Texture2D crystalTexture = TextureAssets.Projectile[base.Type].Value;
		for (int i = 0; i < TotalCrystalsToDraw; i++)
		{
			float f = (float)Math.PI * 2f / (float)TotalCrystalsToDraw * (float)i + time / 10f;
			float radius = MathHelper.Lerp(MaxCrystalOffsetRadius, 0f, time / (float)CrystalsDrawTime);
			Vector2 drawPositionOffset = f.ToRotationVector2() * radius;
			Vector2 drawPosition = ((time >= (float)CrystalsDrawTime) ? base.Projectile.Center : (base.Projectile.Center + drawPositionOffset + Main.rand.NextVector2Circular(12f, 12f)));
			Projectile projectile = base.Projectile;
			Color val = Color.Lerp(Color.Orchid, Color.Goldenrod, fade);
			((Color)(ref val)).A = 0;
			Color backglowColor = val * completion * 0.5f;
			Color white = Color.White;
			val = Color.White;
			((Color)(ref val)).A = 0;
			Color lightColor2 = Color.Lerp(white, val, fade * 0.5f) * MathHelper.Clamp(completion * 1.5f, (time >= (float)CrystalsDrawTime) ? 0.8f : 0f, 1f);
			float backglowArea = 4f * completion + fade * 3f;
			float x = drawPosition.X;
			float y = drawPosition.Y;
			projectile.DrawProjectileWithBackglow(backglowColor, lightColor2, backglowArea, crystalTexture, null, (SpriteEffects)0, x, y);
		}
		return false;
	}
}
