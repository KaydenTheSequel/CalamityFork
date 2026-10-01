using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ApoctosisArray : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 130;
		base.Item.height = 58;
		base.Item.damage = 40;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 14;
		base.Item.useAnimation = 5;
		base.Item.useTime = 5;
		base.Item.useStyle = 5;
		base.Item.knockBack = 6f;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<ApoctosisArrayHoldout>();
		base.Item.shootSpeed = 3f;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] == 0;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseRotationListener = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/ApoctosisArrayGlow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Projectile holdout = ((!player.Calamity().mouseRight || player.whoAmI != Main.myPlayer || Main.mapFullscreen || Main.blockMouse) ? Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, type, damage, knockback, player.whoAmI) : Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 0f, 5f));
		holdout.velocity = player.Calamity().mouseWorld - player.RotatedRelativePoint(player.MountedCenter);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<IonBlaster>().AddIngredient(3458, 12).AddTile(412)
			.Register();
	}
}
