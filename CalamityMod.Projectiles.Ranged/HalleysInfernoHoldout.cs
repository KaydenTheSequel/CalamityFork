using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HalleysInfernoHoldout : BaseGunHoldoutProjectile
{
	public override int AssociatedItemID => ModContent.ItemType<HalleysInferno>();

	public override float MaxOffsetLengthFromArm => 24f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -5f;

	public override float OffsetYDownwards => 5f;

	public override float WeaponTurnSpeed => 20f;

	public ref float ShotTimer => ref base.Projectile.ai[0];

	public ref float AccuracyCounter => ref Owner.Calamity().HalleyAccuracyCounter;

	public ref float ShotCounter => ref base.Projectile.ai[2];

	public ref float RecoilAmount => ref base.Projectile.localAI[0];

	public new Player Owner => Main.player[base.Projectile.owner];

	public Item Halley => Owner.HeldItem;

	public override void KillHoldoutLogic()
	{
		if (Owner.CantUseHoldout(needsToHold: false) || Halley.type != ModContent.ItemType<HalleysInferno>())
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		base.SetUsage = false;
		if (Halley.type != ModContent.ItemType<HalleysInferno>())
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.timeLeft = 60;
		if (Owner.HasAmmo(Halley) && (Owner.controlUseItem || ShotCounter > 0f))
		{
			if (ShotTimer <= 0f)
			{
				ShotCounter++;
				ShotTimer = 5f;
				if (ShotCounter >= 5f)
				{
					ShotCounter = 0f;
					ShotTimer += 35f;
				}
				Vector2 spawnpos = base.Projectile.Center;
				Projectile.NewProjectile(Owner.GetSource_ItemUse_WithPotentialAmmo(Halley, AmmoID.Gel), spawnpos, spawnpos.DirectionTo(Main.MouseWorld) * Halley.shootSpeed, ModContent.ProjectileType<HalleysComet>(), (int)Owner.GetDamage(DamageClass.Ranged).ApplyTo(Halley.damage), Halley.knockBack, base.Projectile.owner);
				RecoilAmount = 8f;
				SoundEngine.PlaySound(in HalleysInferno.ShootSound);
				Owner.PickAmmo(Halley, out var _, out var _, out var _, out var _, out var _);
			}
		}
		else if (Owner.controlUseTile && ShotTimer <= 0f && Owner.Calamity().AvaliableStarburst > 1)
		{
			ShotTimer = 4f;
			Vector2 spawnpos2 = base.Projectile.Center;
			Vector2 dir = spawnpos2.DirectionTo(Main.MouseWorld);
			int color = Main.rand.Next(1, 7);
			Color drawColor = Color.White;
			switch (color)
			{
			case 1:
				drawColor = Color.HotPink;
				break;
			case 2:
				drawColor = Color.Yellow;
				break;
			case 3:
				drawColor = Color.LimeGreen;
				break;
			case 4:
				drawColor = Color.SkyBlue;
				break;
			case 5:
				drawColor = Color.Lavender;
				break;
			}
			for (int i = 0; i < 5; i++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(spawnpos2 + dir * 36f, (dir * Halley.shootSpeed).RotatedByRandom(0.75) * Main.rand.NextFloat(0.5f, 1.5f), affectedByGravity: false, 7, 0.02f, drawColor, new Vector2(0.5f, 1f)));
			}
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(Owner.GetSource_ItemUse_WithPotentialAmmo(Halley, AmmoID.Gel), spawnpos2 + dir * 16f, dir * Halley.shootSpeed * HalleysInferno.StarburstVelMult, ModContent.ProjectileType<HalleysStarburst>(), (int)(Owner.GetDamage(DamageClass.Ranged).ApplyTo(Halley.damage) * HalleysInferno.StarburstDmgMult), Halley.knockBack, base.Projectile.owner, color);
			}
			Owner.Calamity().StratusStarburst -= 2;
			SoundEngine.PlaySound(in HalleysInferno.ShootSound);
			RecoilAmount = 4f;
		}
		RecoilAmount *= 0.75f;
		if (ShotTimer > 0f)
		{
			ShotTimer--;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == Owner.whoAmI)
		{
			float completion = AccuracyCounter / HalleysInferno.MaxAccuracy;
			Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
			Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
			Vector2 drawPos = Owner.Center - Main.screenPosition + new Vector2(0f, -36f) - barBG.Size() / 2f;
			Rectangle frame = default(Rectangle);
			((Rectangle)(ref frame))._002Ector(0, 0, (int)(completion * (float)barFG.Width), barFG.Height);
			float opacity = 1f;
			Color color = Color.Lerp(Color.DarkSlateBlue, Color.SkyBlue, completion);
			if (completion >= 1f)
			{
				color = Color.DeepSkyBlue;
			}
			Main.spriteBatch.Draw(barBG, drawPos, color * opacity);
			Main.spriteBatch.Draw(barFG, drawPos, (Rectangle?)frame, color * opacity * 0.8f);
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * Owner.gravDir == -1f);
		for (int i = 1; i <= 24; i++)
		{
			float mult = MathHelper.Max(Utils.GetLerpValue(7f, 0f, i), Utils.GetLerpValue(17f, 24f, i));
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 24f).ToRotationVector2().RotatedBy(base.Projectile.rotation) * RecoilAmount + Main.rand.NextVector2Circular(2f, 2f);
			Color chartreuse = Color.Chartreuse;
			((Color)(ref chartreuse)).A = 0;
			Color auraColor = chartreuse * mult * 0.4f * Utils.GetLerpValue(90f, 135f, RecoilAmount, clamped: true);
			float aimAngle = drawRotation;
			Main.EntitySpriteDraw(texture, drawPosition + drawOffset, null, auraColor, aimAngle, rotationPoint, base.Projectile.scale * Owner.gravDir, flipSprite);
		}
		Main.EntitySpriteDraw(texture, drawPosition + Utils.RotatedBy(new Vector2(0f - RecoilAmount, 0f), (double)base.Projectile.rotation, default(Vector2)), null, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale * Owner.gravDir, flipSprite);
		return false;
	}
}
