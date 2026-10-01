using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TerrorBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 4;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 600;
		base.Projectile.light = 1f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	private void SpawnTerrorBlast()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		int projID = ModContent.ProjectileType<TerrorBlast>();
		int blastDamage = (int)((float)base.Projectile.damage * 0.3f);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, projID, blastDamage, base.Projectile.knockBack, base.Projectile.owner);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.penetrate--;
		if (base.Projectile.penetrate <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			if (base.Projectile.owner == Main.myPlayer)
			{
				SpawnTerrorBlast();
			}
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			SpawnTerrorBlast();
		}
	}

	public override void AI()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[1] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item60, base.Projectile.position);
			base.Projectile.localAI[1]++;
		}
		base.Projectile.alpha -= 40;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 595)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		if (base.Projectile.localAI[0] == 0f)
		{
			SpawnTerrorBlast();
		}
	}
}
