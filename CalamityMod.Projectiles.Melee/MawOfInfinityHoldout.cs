using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MawOfInfinityHoldout : BaseSwordHoldoutProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override bool useMeleeSpeed => true;

	public override bool useMeleeSize => true;

	public override int swingWidth => 270;

	public override Item BaseItem => ModContent.GetModItem(ModContent.ItemType<MawOfInfinity>()).Item;

	public override int AfterImageLength => 0;

	public override int StartupTime { get; set; }

	public override int CooldownTime { get; set; }

	public override string Texture => ModContent.GetModItem(BaseItem.type).Texture;

	public override float lineCollisionLength => 232f;

	public override bool AlternateSwings => true;

	public override void Defaults()
	{
		base.Projectile.width = 78;
		base.Projectile.height = 94;
		base.Projectile.extraUpdates = 5;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void Spawn()
	{
		BaseSwordHoldoutPlayer modplayer = Main.player[base.Projectile.owner].GetModPlayer<BaseSwordHoldoutPlayer>();
		StartupTime = 10;
		CooldownTime = 10;
		swingTime -= StartupTime + CooldownTime;
		if (Main.myPlayer == base.Projectile.owner)
		{
			modplayer.swingNum = modplayer.swingNum++ % 3;
		}
		OffsetDistance = 70;
		RotateInStartup = 0.2f;
		RotateInCooldown = 0f;
		UseSound = SoundID.DD2_MonkStaffSwing;
	}

	public override void AdditionalAI()
	{
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		switch (player.GetModPlayer<BaseSwordHoldoutPlayer>().swingNum)
		{
		case 1:
			if (base.inSwing)
			{
				Vector2 veloc = base.oldPlayerOffset - (base.Projectile.Center - Main.player[base.Projectile.owner].Center);
				((Vector2)(ref veloc)).Normalize();
				int sparkLifetime = Main.rand.Next(15, 23);
				Vector2 spinningpoint = Vector2.UnitY * -9f;
				float maxRotationDeviance = 0.4f;
				float rotationAngle = Main.rand.NextFloat(0f - maxRotationDeviance, maxRotationDeviance);
				_ = spinningpoint.RotatedBy(rotationAngle) * Main.rand.NextFloat(0.3f, 1f);
				float sparkScale = Main.rand.NextFloat(0.007f, 0.015f);
				Vector2 compensatedSparkVel = veloc.RotatedBy((float)Math.PI / 8f * (float)base.Projectile.spriteDirection) * Main.rand.NextFloat(2f, 5f);
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + Utils.RotatedBy(new Vector2((float)(-base.angle.X.DirectionalSign()), Main.rand.NextFloat(-0.05f, 0.05f)), (double)(base.Projectile.rotation - 0.7f * (float)base.Projectile.spriteDirection), default(Vector2)) * Main.rand.NextFloat(-20f, -108f) * base.Projectile.scale, compensatedSparkVel, affectedByGravity: false, sparkLifetime, sparkScale, Main.rand.NextBool() ? Color.Fuchsia : Color.HotPink, new Vector2(0.5f, 1.3f)));
			}
			break;
		case 2:
			if (swingTimer == (int)((float)swingTime * 0.5f) && Main.myPlayer == base.Projectile.owner)
			{
				for (int i = -1; i < 2; i++)
				{
					int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), player.Center, -base.angle.RotatedBy(0.2f * (float)i) * 16f, ModContent.ProjectileType<DoGFire>(), base.Projectile.damage, base.Projectile.knockBack, player.whoAmI, 2f);
					if (Main.projectile.IndexInRange(p))
					{
						Main.projectile[p].hostile = false;
						Main.projectile[p].friendly = true;
						Main.projectile[p].DamageType = DamageClass.Melee;
						Main.projectile[p].timeLeft = 120;
						Main.projectile[p].netUpdate = true;
					}
				}
			}
			if (base.inSwing)
			{
				Vector2 veloc2 = base.oldPlayerOffset - (base.Projectile.Center - Main.player[base.Projectile.owner].Center);
				((Vector2)(ref veloc2)).Normalize();
				int sparkLifetime2 = Main.rand.Next(15, 23);
				Vector2 spinningpoint2 = Vector2.UnitY * -9f;
				float maxRotationDeviance2 = 0.4f;
				float rotationAngle2 = Main.rand.NextFloat(0f - maxRotationDeviance2, maxRotationDeviance2);
				_ = spinningpoint2.RotatedBy(rotationAngle2) * Main.rand.NextFloat(0.3f, 1f);
				float sparkScale2 = Main.rand.NextFloat(0.007f, 0.015f);
				Vector2 compensatedSparkVel2 = veloc2.RotatedBy((float)Math.PI / 8f * (float)base.Projectile.spriteDirection) * Main.rand.NextFloat(2f, 5f);
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + Utils.RotatedBy(new Vector2((float)(-base.angle.X.DirectionalSign()), Main.rand.NextFloat(-0.05f, 0.05f)), (double)(base.Projectile.rotation - 0.7f * (float)base.Projectile.spriteDirection), default(Vector2)) * Main.rand.NextFloat(20f, 108f) * base.Projectile.scale, compensatedSparkVel2, affectedByGravity: false, sparkLifetime2, sparkScale2, Main.rand.NextBool() ? Color.Cyan : Color.Aqua, new Vector2(0.5f, 1.3f)));
			}
			break;
		case 0:
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), player.Center, -base.angle * 3f, ModContent.ProjectileType<MawOfInfinityJaws>(), base.Projectile.damage * 2, base.Projectile.knockBack, player.whoAmI);
			}
			player.itemAnimation = BaseItem.useAnimation;
			player.itemTime = BaseItem.useTime;
			base.Projectile.Kill();
			break;
		}
	}

	public override float SwingFunction()
	{
		if (base.inStartup)
		{
			return MathHelper.ToRadians(MathHelper.SmoothStep((float)(-swingWidth) * 0.6f, (float)(-swingWidth) * 0.5f, MathF.Pow(base.StartupCompletion, 2f)));
		}
		if (base.inCooldown)
		{
			return MathHelper.ToRadians(MathHelper.Lerp((float)swingWidth * 0.5f, (float)swingWidth * 0.6f, 1f - MathF.Pow(1f - base.CooldownCompletion, 2f)));
		}
		return MathHelper.ToRadians(MathHelper.SmoothStep((float)(-swingWidth) * 0.5f, (float)swingWidth * 0.5f, base.SwingCompletion));
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		BaseSwordHoldoutPlayer modplayer = Main.player[base.Projectile.owner].GetModPlayer<BaseSwordHoldoutPlayer>();
		if (modplayer.swingNum != 0)
		{
			Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/Jaws", (AssetRequestMode)2).Value;
			float jawScaleMult = 1f;
			if (base.inStartup)
			{
				jawScaleMult = base.StartupCompletion;
			}
			if (base.inCooldown)
			{
				jawScaleMult = 1f - base.CooldownCompletion;
			}
			jawScaleMult = MathF.Pow(jawScaleMult, 3f);
			float rotation = base.Projectile.rotation - ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI / 2f) : 0f);
			Vector2 DrawPos = base.Projectile.Center + base.Projectile.scale * Utils.RotatedBy(new Vector2(50f, (float)(10 * base.Projectile.spriteDirection)), (double)(base.Projectile.rotation - (float)Math.PI / 2f + ((base.Projectile.spriteDirection == -1) ? (-(float)Math.PI / 4f) : ((float)Math.PI / 4f))), default(Vector2));
			Main.spriteBatch.SetBlendState(BlendState.Additive);
			Main.spriteBatch.Draw(tex, DrawPos - Main.screenPosition, (Rectangle?)tex.Frame(2), (modplayer.swingNum == 1) ? Color.Fuchsia : Color.Cyan, rotation + (float)Math.PI / 4f, new Vector2((float)tex.Width * 0.25f, (float)tex.Height * 0.5f), base.Projectile.scale * jawScaleMult, (SpriteEffects)(base.Projectile.spriteDirection == -1), 0f);
			Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<WhisperingDeath>(), 240);
	}
}
