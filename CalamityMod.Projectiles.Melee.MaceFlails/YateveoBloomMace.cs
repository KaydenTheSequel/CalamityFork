using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.Projectiles.BaseProjectiles;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.MaceFlails;

[PierceResistException(false)]
public class YateveoBloomMace : BaseMaceFlailProjectile
{
	public override int AssociatedItemID => ModContent.ItemType<YateveoBloom>();

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.SetDefaults();
	}

	public override bool ExtraBehavior()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			int dustType = Main.rand.Next(5);
			Dust dust = Dust.NewDustDirect(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, dustType switch
			{
				0 => 2, 
				1 => 44, 
				_ => 136, 
			});
			dust.noGravity = true;
			dust.scale = 1.5f;
		}
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(20, 180);
	}
}
