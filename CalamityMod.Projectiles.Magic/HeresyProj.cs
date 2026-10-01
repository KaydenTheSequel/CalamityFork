using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.Projectiles.Magic;

public class HeresyProj : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Heresy>();

	public Player Owner => Main.player[base.Projectile.owner];

	public float ShootIntensity => MathHelper.SmoothStep(0f, 1f, Utils.GetLerpValue(0f, 275f, Time, clamped: true));

	public ref float Time => ref base.Projectile.ai[0];

	public ref float AttackTimer => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
			return;
		}
		if (AttackTimer >= (float)(Main.rand.Next(20, 28) - (int)MathHelper.Lerp(0f, 16f, Utils.GetLerpValue(0f, 120f, Time, clamped: true))))
		{
			ReleaseThings();
		}
		base.Projectile.localAI[0] += Utils.Remap(Time, 0f, 180f, 1f, 5f);
		base.Projectile.frame = (int)Math.Round(base.Projectile.localAI[0] / 10f) % Main.projFrames[base.Type];
		if (base.Projectile.localAI[0] >= (float)Main.projFrames[base.Type] * 10f)
		{
			base.Projectile.localAI[0] = 0f;
		}
		AdjustPlayerValues();
		base.Projectile.Center = Owner.Center + (Owner.compositeFrontArm.rotation + (float)Math.PI / 2f).ToRotationVector2() * 14f;
		base.Projectile.timeLeft = 2;
		AttackTimer++;
		Time++;
	}

	public void ReleaseThings()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.DD2_EtherianPortalSpawnEnemy);
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		if (!Owner.CheckMana(Owner.HeldItem.mana, pay: true))
		{
			base.Projectile.Kill();
			return;
		}
		WeightedRandom<int> typeDecider = new WeightedRandom<int>();
		typeDecider.Add(ModContent.ProjectileType<RedirectingFire>(), 1.5);
		typeDecider.Add(ModContent.ProjectileType<RedirectingLostSoul>(), ShootIntensity * 0.75f);
		typeDecider.Add(ModContent.ProjectileType<RedirectingVengefulSoul>(), ShootIntensity * 0.4f);
		typeDecider.Add(ModContent.ProjectileType<RedirectingGildedSoul>(), ShootIntensity * 0.2f);
		Vector2 spawnPosition = base.Projectile.Top + Main.rand.NextVector2CircularEdge(4f, 4f);
		Vector2 shootVelocity = -Vector2.UnitY.RotatedBy(Main.rand.NextFloat(-0.13f, 0.23f) * (float)Owner.direction) * Owner.gravDir;
		shootVelocity *= Main.rand.NextFloat(5f, 8f);
		if (Owner.velocity.Y < 0f)
		{
			shootVelocity.Y += Owner.velocity.Y;
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, shootVelocity, typeDecider.Get(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, ShootIntensity);
		AttackTimer = 0f;
		base.Projectile.netUpdate = true;
	}

	public void AdjustPlayerValues()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = (base.Projectile.direction = Owner.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = ((float)base.Projectile.direction * base.Projectile.velocity).ToRotation();
		float frontArmRotation = 1.1107963f * (float)(-Owner.direction);
		float backArmRotation = frontArmRotation + MathHelper.Lerp(0.23f, 0.97f, CalamityUtils.Convert01To010(base.Projectile.localAI[0] / (float)Main.projFrames[base.Type] / 10f)) * (float)(-Owner.direction);
		Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, backArmRotation);
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, frontArmRotation);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		float glowOutwardness = MathHelper.SmoothStep(0f, 4f, Utils.GetLerpValue(90f, 270f, Time, clamped: true));
		Texture2D bookTexture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = bookTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Color glowColor = Color.Lerp(Color.Pink, Color.Red, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 5f) * 0.5f + 0.5f);
		((Color)(ref glowColor)).A = 0;
		Vector2 drawPosition;
		for (int i = 0; i < 8; i++)
		{
			drawPosition = base.Projectile.Center + ((float)Math.PI * 2f * (float)i / 8f + Main.GlobalTimeWrappedHourly * 4f).ToRotationVector2() * glowOutwardness - Main.screenPosition;
			Main.EntitySpriteDraw(bookTexture, drawPosition, frame, base.Projectile.GetAlpha(glowColor), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		}
		drawPosition = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(bookTexture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
