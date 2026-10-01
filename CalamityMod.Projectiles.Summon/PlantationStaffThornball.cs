using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PlantationStaffThornball : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center.MinionHoming(PlantationStaff.EnemyDistanceDetection, Owner);
		}
	}

	public ref float IsSticked => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.localNPCHitCooldown = 60;
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (IsSticked == 0f)
		{
			if (Target != null)
			{
				base.Projectile.velocity = (base.Projectile.velocity * 25f + base.Projectile.SafeDirectionTo(Target.Center) * PlantationStaff.ThornballSpeed) / 26f;
			}
			base.Projectile.rotation += MathHelper.ToRadians(base.Projectile.velocity.X);
		}
		else if (Target != null)
		{
			base.Projectile.velocity = Target.velocity;
		}
		else
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (IsSticked == 1f)
		{
			base.Projectile.Kill();
		}
		IsSticked = 1f;
		base.Projectile.netUpdate = true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		for (int dustIndex = 0; dustIndex < 10; dustIndex++)
		{
			Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 40, 0f, 0f, 0, Color.Pink);
		}
	}
}
