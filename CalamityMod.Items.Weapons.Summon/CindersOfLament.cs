using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class CindersOfLament : ExhumedItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<EntropysVigil>();
	}

	public override void SetDefaults()
	{
		base.Item.width = 86;
		base.Item.height = 92;
		base.Item.damage = 1666;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 80);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<CataclysmSummon>();
		base.Item.shootSpeed = 10f;
		base.Item.UseSound = SoundID.DD2_BetsySummon;
		base.Item.useStyle = 1;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 2)
		{
			if (Main.rand.NextBool())
			{
				type = ModContent.ProjectileType<CatastropheSummon>();
			}
			int p = Projectile.NewProjectile(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = base.Item.damage;
			}
		}
		return false;
	}
}
