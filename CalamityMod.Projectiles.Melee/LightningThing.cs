using System;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class LightningThing : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 90;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		int damage = (int)Main.player[base.Projectile.owner].GetTotalDamage<MeleeDamageClass>().ApplyTo(GaelsGreatsword.BaseDamage);
		for (int i = 0; i < 3; i++)
		{
			int idx = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + new Vector2(Main.rand.NextFloat(-35f, 35f), -1600f), Vector2.UnitY * 12f, 466, damage, 0f, base.Projectile.owner, (float)Math.PI / 2f, Main.rand.Next(100));
			if (idx.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[idx].usesLocalNPCImmunity = true;
				Main.projectile[idx].localNPCHitCooldown = GaelsGreatsword.ImmunityFrames;
				Main.projectile[idx].friendly = true;
				Main.projectile[idx].hostile = false;
				Main.projectile[idx].DamageType = DamageClass.Melee;
			}
		}
	}
}
