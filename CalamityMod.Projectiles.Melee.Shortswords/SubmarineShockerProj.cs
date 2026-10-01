using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Shortswords;

public class SubmarineShockerProj : BaseShortswordProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<SubmarineShocker>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/SubmarineShocker";

	public override void SetDefaults()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Size = new Vector2(16f);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.scale = 1f;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.timeLeft = 360;
		base.Projectile.hide = true;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void SetVisualOffsets()
	{
		int HalfProjWidth = base.Projectile.width / 2;
		int HalfProjHeight = base.Projectile.height / 2;
		base.DrawOriginOffsetX = 0f;
		base.DrawOffsetX = -(16 - HalfProjWidth);
		base.DrawOriginOffsetY = -(16 - HalfProjHeight);
	}

	public override void ExtraBehavior()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 226);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		SpawnSparks(target);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		SpawnSparks(target);
	}

	public void SpawnSparks(Entity target)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<GenericElectricSpark>(), (int)((float)base.Projectile.damage * 0.7f), base.Projectile.knockBack, Main.myPlayer).DamageType = DamageClass.Melee;
	}
}
