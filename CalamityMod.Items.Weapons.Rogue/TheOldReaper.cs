using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "TheReaper" })]
public class TheOldReaper : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.65f;

	public override void SetDefaults()
	{
		base.Item.width = 106;
		base.Item.height = 104;
		base.Item.damage = 1520;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 50;
		base.Item.useTime = 50;
		base.Item.useStyle = 1;
		base.Item.knockBack = 4f;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ReaperProjectile>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int spread = 15;
			for (int i = 0; i < 1; i++)
			{
				Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(velocity.X + (float)Main.rand.Next(-2, 3), velocity.Y + (float)Main.rand.Next(-2, 3)), (double)MathHelper.ToRadians((float)spread), default(Vector2));
				int proj = Projectile.NewProjectile(source, position, perturbedspeed, type, (int)((double)damage * 0.45), knockback, player.whoAmI);
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].Calamity().stealthStrike = true;
				}
				spread -= Main.rand.Next(5, 8);
			}
			return false;
		}
		return true;
	}
}
