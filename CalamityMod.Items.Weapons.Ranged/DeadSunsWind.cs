using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "TheEmpyrean", "GodsBellows" })]
public class DeadSunsWind : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ShootSound = new SoundStyle("CalamityMod/Sounds/Item/DeadSunShot")
	{
		PitchVariance = 0.35f,
		Volume = 0.4f
	};

	public static readonly SoundStyle Ricochet = new SoundStyle("CalamityMod/Sounds/Item/DeadSunRicochet")
	{
		Volume = 0.35f
	};

	public static readonly SoundStyle Explosion = new SoundStyle("CalamityMod/Sounds/Item/DeadSunExplosion")
	{
		Volume = 0.5f
	};

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 70;
		base.Item.height = 24;
		base.Item.damage = 115;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 22);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.5f;
		base.Item.UseSound = ShootSound;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<CosmicFire>();
		base.Item.shootSpeed = 8f;
		base.Item.useAmmo = AmmoID.Gel;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MeldConstruct>(12).AddTile(412).Register();
	}
}
