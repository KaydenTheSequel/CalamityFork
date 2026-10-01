using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class JewelSpike : ModProjectile, ILocalizedModType, IModType
{
	public const int MaxPenetrate = 2;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public ref float RealPenetrate => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 44;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 80;
		base.Projectile.DamageType = DamageClass.Generic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0f;
		if (Main.rand.NextBool(5) && base.Projectile.frame < 3)
		{
			int crystalDust = Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 87, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
			Main.dust[crystalDust].noGravity = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4 && base.Projectile.frame > 0)
		{
			base.Projectile.frame--;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame < 0)
		{
			base.Projectile.frame = 0;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		RealPenetrate++;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (!(RealPenetrate > 1f))
		{
			return null;
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int index = 0; index < 2; index++)
			{
				float SpeedX = (0f - base.Projectile.velocity.X) * Main.rand.NextFloat(0.4f, 0.7f) + Main.rand.NextFloat(-8f, 8f);
				float SpeedY = (0f - base.Projectile.velocity.Y) * Main.rand.NextFloat(0.4f, 0.7f) + Main.rand.NextFloat(-8f, 8f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X + SpeedX, base.Projectile.Center.Y + SpeedY, SpeedX, SpeedY, 90, base.Projectile.damage / 3, 0f, base.Projectile.owner);
			}
		}
	}
}
