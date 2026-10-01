using CalamityMod.Items.Ammo;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class IceBarrage : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle CastSound = new SoundStyle("CalamityMod/Sounds/Item/IceBarrageCast");

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 60;
		base.Item.useStyle = 4;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 180;
		base.Item.noMelee = true;
		base.Item.UseSound = CastSound;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
		base.Item.damage = 2250;
		base.Item.knockBack = 6f;
		base.Item.useAnimation = (base.Item.useTime = 300);
		base.Item.reuseDelay = 60;
		base.Item.useLimitPerAnimation = 1;
		base.Item.shoot = ModContent.ProjectileType<IceBarrageMain>();
		base.Item.shootSpeed = 2f;
		base.Item.useAmmo = ModContent.ItemType<BloodRune>();
	}

	public override bool CanUseItem(Player player)
	{
		return CalamityGlobalItem.HasEnoughAmmo(player, base.Item, 2);
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		realPlayerPos.X = (float)Main.mouseX + Main.screenPosition.X;
		realPlayerPos.Y = (float)Main.mouseY + Main.screenPosition.Y;
		Projectile.NewProjectile(source, realPlayerPos, Vector2.Zero, type, damage, knockback, player.whoAmI);
		CalamityGlobalItem.ConsumeAdditionalAmmo(player, base.Item, 2);
		return false;
	}

	public override void UseStyle(Player player, Rectangle rectangle)
	{
		player.itemLocation.X -= 8f * (float)player.direction;
		player.itemRotation = (float)player.direction * MathHelper.ToRadians(-45f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1931).AddIngredient(496).AddIngredient<IcicleStaff>()
			.AddIngredient<CosmiliteBar>(8)
			.AddIngredient<EndothermicEnergy>(40)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
