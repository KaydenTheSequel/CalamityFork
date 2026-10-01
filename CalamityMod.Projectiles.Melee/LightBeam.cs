using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class LightBeam : ModProjectile, ILocalizedModType, IModType
{
	private const float MaxVelocity = 24f;

	private const int FadeOutTime = 85;

	private const int TimeLeft = 300;

	private const int Alpha = 100;

	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 36;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 24;
		base.Projectile.alpha = 100;
	}

	public override void AI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		float alphaLightScale = (float)base.Projectile.alpha / 100f;
		Lighting.AddLight(base.Projectile.Center, 1.2f * alphaLightScale, 0f, 0.4f * alphaLightScale);
		if (base.Projectile.timeLeft > 85)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 24f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.055f;
			}
		}
		else
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.925f;
		}
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = -1;
		}
		base.Projectile.rotation += (float)base.Projectile.direction * 0.05f;
		if (base.Projectile.timeLeft > 85)
		{
			base.Projectile.rotation += (float)base.Projectile.direction * 0.5f;
		}
		else
		{
			base.Projectile.rotation += (float)base.Projectile.direction * 0.5f * ((float)base.Projectile.timeLeft / 85f);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 85)
		{
			byte b2 = (byte)(base.Projectile.timeLeft * 3);
			byte a2 = (byte)(100f * ((float)(int)b2 / 255f));
			base.Projectile.alpha = a2;
			return new Color((int)b2, (int)b2, (int)b2, base.Projectile.alpha);
		}
		base.Projectile.alpha = 100;
		return new Color(255, 255, 255, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 85)
		{
			base.Projectile.velocity = base.Projectile.oldVelocity;
			base.Projectile.tileCollide = false;
			base.Projectile.timeLeft = 85;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(153, 120);
		if (base.Projectile.numHits >= 2)
		{
			base.Projectile.timeLeft = 85;
		}
		int slashCreatorID = ModContent.ProjectileType<DarklightGreatswordSlashCreator>();
		int damage = (int)((float)base.Projectile.damage * 0.4125f);
		float knockback = base.Projectile.knockBack * 0.4125f;
		if (Owner.ownedProjectileCounts[slashCreatorID] < 4)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, slashCreatorID, damage, knockback, base.Projectile.owner, target.whoAmI, base.Projectile.rotation, 1f);
			Owner.ownedProjectileCounts[slashCreatorID]++;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Shadowflame>(), 120);
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.timeLeft >= 85)
		{
			return null;
		}
		return false;
	}
}
