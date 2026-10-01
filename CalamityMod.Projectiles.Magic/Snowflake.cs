using System;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(false)]
public class Snowflake : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 70;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 280;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.coldDamage = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 40;
	}

	public override void AI()
	{
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player == null)
		{
			return;
		}
		if (player.CantUseHoldout())
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.type != ModContent.ProjectileType<Snowflake>() || Main.player[base.Projectile.owner].HeldItem.type != ModContent.ItemType<SnowstormStaff>() || !Main.player[base.Projectile.owner].channel)
		{
			base.Projectile.Kill();
			return;
		}
		if (base.Projectile.ai[2] > 0f)
		{
			base.Projectile.ai[2]--;
		}
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		base.Projectile.rotation += 0.2f;
		if (base.Projectile.localAI[0] < 1f)
		{
			base.Projectile.localAI[0] += 0.002f;
		}
		else
		{
			base.Projectile.width = (base.Projectile.height = 50);
		}
		Vector2 projPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float projX = (float)Main.mouseX + Main.screenPosition.X - projPos.X;
		float projY = (float)Main.mouseY + Main.screenPosition.Y - projPos.Y;
		if (player.gravDir == -1f)
		{
			projY = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - projPos.Y;
		}
		if ((float.IsNaN(projX) && float.IsNaN(projY)) || (projX == 0f && projY == 0f))
		{
			projX = player.direction;
			projY = 0f;
		}
		projPos += new Vector2(projX, projY);
		float speed = 30f;
		float speedScale = 3f;
		Vector2 vectorPos = base.Projectile.Center;
		if (Vector2.Distance(projPos, vectorPos) < 90f)
		{
			speed = 10f;
			speedScale = 1f;
		}
		if (Vector2.Distance(projPos, vectorPos) < 30f)
		{
			speed = 3f;
			speedScale = 0.3f;
		}
		if (Vector2.Distance(projPos, vectorPos) < 10f)
		{
			speed = 1f;
			speedScale = 0.1f;
		}
		float projectileX = projPos.X - vectorPos.X;
		float projectileY = projPos.Y - vectorPos.Y;
		float projectileAdjust = (float)Math.Sqrt(projectileX * projectileX + projectileY * projectileY);
		projectileAdjust = speed / projectileAdjust;
		projectileX *= projectileAdjust;
		projectileY *= projectileAdjust;
		if (base.Projectile.velocity.X < projectileX)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X + speedScale;
			if (base.Projectile.velocity.X < 0f && projectileX > 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + speedScale;
			}
		}
		else if (base.Projectile.velocity.X > projectileX)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X - speedScale;
			if (base.Projectile.velocity.X > 0f && projectileX < 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - speedScale;
			}
		}
		if (base.Projectile.velocity.Y < projectileY)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + speedScale;
			if (base.Projectile.velocity.Y < 0f && projectileY > 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + speedScale;
			}
		}
		else if (base.Projectile.velocity.Y > projectileY)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - speedScale;
			if (base.Projectile.velocity.Y > 0f && projectileY < 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - speedScale;
			}
		}
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 67, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		float pushForce = 0.15f;
		for (int k = 0; k < Main.maxProjectiles; k++)
		{
			Projectile otherProj = Main.projectile[k];
			if (!otherProj.active || k == base.Projectile.whoAmI)
			{
				continue;
			}
			bool num = otherProj.type == base.Projectile.type;
			float taxicabDist = Vector2.Distance(base.Projectile.Center, otherProj.Center);
			float distancegate = 100f;
			if (num && taxicabDist < distancegate)
			{
				if (base.Projectile.position.X < otherProj.position.X)
				{
					base.Projectile.velocity.X -= pushForce;
				}
				else
				{
					base.Projectile.velocity.X += pushForce;
				}
				if (base.Projectile.position.Y < otherProj.position.Y)
				{
					base.Projectile.velocity.Y -= pushForce;
				}
				else
				{
					base.Projectile.velocity.Y += pushForce;
				}
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 67, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int x = 1; x <= 2; x++)
			{
				Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, base.Projectile.velocity * 0.5f + Main.rand.NextVector2Square(-4f, 4f), base.Mod.Find<ModGore>("CryoShieldGore" + Main.rand.Next(1, 5)).Type, 0.7f);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(324, 180);
		float randOffset = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.ai[2] <= 0f)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)i / 3f + randOffset).ToRotationVector2() * 4f;
				int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<SnowflakeIceStar>(), base.Projectile.damage / 2, base.Projectile.knockBack * 0.5f, base.Projectile.owner);
				Main.projectile[p].DamageType = DamageClass.Magic;
			}
			SoundEngine.PlaySound(in SoundID.Item30, base.Projectile.Center);
			base.Projectile.ai[2] = 240f;
		}
	}
}
