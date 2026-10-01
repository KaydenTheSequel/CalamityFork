using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "TerraDisk" })]
public class SamsaraSlicer : RogueWeapon
{
	public static float Speed = 25f;

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 64;
		base.Item.damage = 80;
		base.Item.knockBack = 4f;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.DD2_GoblinBomberThrow;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.shoot = ModContent.ProjectileType<SamsaraSlicerProjectile>();
		base.Item.shootSpeed = Speed;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		if (player.ownedProjectileCounts[type] > 4)
		{
			return false;
		}
		int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (player.Calamity().StealthStrikeAvailable() && proj.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[proj].Calamity().stealthStrike = true;
		}
		return false;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] > 4)
		{
			return false;
		}
		return base.CanUseItem(player);
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/SamsaraSlicerGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Equanimity>().AddIngredient<Brimblade>().AddIngredient<LivingShard>(12)
			.AddTile(134)
			.Register();
	}
}
