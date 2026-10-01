using CalamityMod.Items.Materials;
using CalamityMod.Items.Potions;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Auralis : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle HeavyShotSound;

	public static readonly Color blueColor;

	public static readonly Color greenColor;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 96;
		base.Item.height = 34;
		base.Item.damage = 695;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.knockBack = 10f;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AuralisBullet>();
		base.Item.shootSpeed = 7.5f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.UseSound = HeavyShotSound with
		{
			Volume = 0.8f
		};
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
	}

	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		float damageMult = MathHelper.Lerp(0f, 0.25f, player.Calamity().auralisStealthCounter / 300f);
		damage += damageMult;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, base.Item.shoot, damage, knockback, player.whoAmI);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/AuralisGlow", (AssetRequestMode)2).Value);
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override void HoldItem(Player player)
	{
		player.scope = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1254).AddIngredient<UelibloomBar>(5).AddIngredient<AureusCell>(5)
			.AddIngredient<StarblightSoot>(50)
			.AddTile(134)
			.Register();
	}

	static Auralis()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		HeavyShotSound = new SoundStyle("CalamityMod/Sounds/Item/PlasmaRifleMain");
		blueColor = new Color(0, 77, 255);
		greenColor = new Color(0, 255, 77);
	}
}
