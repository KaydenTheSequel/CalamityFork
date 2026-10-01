using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class CleansingBlaze : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 104;
		base.Item.height = 62;
		base.Item.damage = 275;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 3;
		base.Item.useAnimation = 24;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item34;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<EssenceFire>();
		base.Item.shootSpeed = 11f;
		base.Item.useAmmo = AmmoID.Gel;
		base.Item.consumeAmmoOnFirstShotOnly = true;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 newPos = position + new Vector2(0f, 5f) + velocity.SafeNormalize(Vector2.UnitX) * 36f;
		for (int i = 0; i < 3; i++)
		{
			Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(5f));
			Projectile.NewProjectile(source, newPos, newVel, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-20f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(12).AddTile<CosmicAnvil>().Register();
	}
}
