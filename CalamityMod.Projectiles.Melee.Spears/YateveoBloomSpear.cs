using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Terraria;
using Terraria.Localization;

namespace CalamityMod.Projectiles.Melee.Spears;

public class YateveoBloomSpear : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<YateveoBloom>();

	public override float InitialSpeed => 3f;

	public override float ReelbackSpeed => 2.4f;

	public override float ForwardSpeed => 0.8f;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
		base.Projectile.timeLeft = 90;
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void ExtraBehavior()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			int dustType = Main.rand.Next(5);
			switch (dustType)
			{
			case 0:
				dustType = 2;
				break;
			case 1:
				dustType = 44;
				break;
			case 2:
			case 3:
			case 4:
				dustType = 136;
				break;
			}
			int dust = Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, dustType);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].scale = 1.5f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(20, 180);
	}
}
