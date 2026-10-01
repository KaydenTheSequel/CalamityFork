using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class IceCluster : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 90;
		base.Projectile.height = 90;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 100;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.coldDamage = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += 0.5f;
		if (base.Projectile.localAI[1] == 0f)
		{
			base.Projectile.localAI[1] = 1f;
			SoundEngine.PlaySound(in SoundID.Item120, base.Projectile.Center);
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[1] == 1f)
		{
			if (base.Projectile.ai[0] % 30f == 0f && Main.netMode != 1)
			{
				Vector2 vector80 = base.Projectile.rotation.ToRotationVector2();
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vector80, ModContent.ProjectileType<IceCluster>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
			Lighting.AddLight(base.Projectile.Center, 0.3f, 0.75f, 0.9f);
		}
		if (base.Projectile.ai[0] >= 90f)
		{
			base.Projectile.alpha += 25;
		}
		else
		{
			base.Projectile.alpha -= 15;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.alpha > 255)
		{
			base.Projectile.alpha = 255;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
		if (base.Projectile.damage <= 10)
		{
			return;
		}
		Vector2 vector80 = base.Projectile.rotation.ToRotationVector2();
		if (base.Projectile.owner == Main.myPlayer)
		{
			int newDamage = base.Projectile.damage / 2;
			if (newDamage < 1)
			{
				newDamage = 1;
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vector80, ModContent.ProjectileType<IceCluster>(), newDamage, base.Projectile.knockBack, base.Projectile.owner);
		}
	}
}
