using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Healing;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BloodsoakedCrashax : ModProjectile, ILocalizedModType, IModType
{
	private int bounce = 3;

	private int grind;

	private const float MaxSpeed = 14f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/BloodsoakedCrasher";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 6;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		float speed = ((Vector2)(ref base.Projectile.velocity)).Length();
		if (grind > 0)
		{
			grind--;
			base.Projectile.velocity.X *= 0.75f;
			base.Projectile.velocity.Y *= 0.75f;
		}
		else
		{
			base.Projectile.velocity.Y += 0.07f;
			speed = ((Vector2)(ref base.Projectile.velocity)).Length();
			if (speed > 14f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 14f / speed;
			}
		}
		float spinRate = ((grind > 0) ? 0.28f : 0.09f);
		if (grind <= 0)
		{
			spinRate += speed * 0.005f;
		}
		base.Projectile.rotation += spinRate * (float)base.Projectile.direction;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		bounce--;
		if (bounce <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
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
		target.AddBuff(ModContent.BuffType<Laceration>(), 120);
		if (target.lifeMax > 5)
		{
			OnHitEffects(hit.Damage);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Laceration>(), 120);
		OnHitEffects(info.Damage);
	}

	private void OnHitEffects(int damage)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		grind += 6;
		if (grind > 18)
		{
			grind = 18;
		}
		if (base.Projectile.Calamity().stealthStrike && base.Projectile.owner == Main.myPlayer)
		{
			int projID = ModContent.ProjectileType<Blood>();
			int stealth = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.UnitX.RotatedByRandom(3.1415927410125732) * 4f, projID, (int)((float)base.Projectile.damage * 0.5f), 1f, base.Projectile.owner, 1f, 0.85f + Main.rand.NextFloat() * 1.15f);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].DamageType = RogueDamageClass.Instance;
				Main.projectile[stealth].extraUpdates = 1;
			}
		}
		float orbAmount = (base.Projectile.Calamity().stealthStrike ? 3 : (base.Projectile.penetrate % 2));
		if (orbAmount > 0f)
		{
			float spreadAmount = MathHelper.ToRadians(360f);
			for (int i = 0; (float)i < orbAmount; i++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.One.RotatedByRandom(spreadAmount) * 2f * Main.rand.NextFloat(0.75f, 1.25f), ModContent.ProjectileType<BloodstoneHealOrb>(), 10, 0f, base.Projectile.owner);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
