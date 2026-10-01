using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "DivineHatchet" })]
public class SeekingScorcher : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ThrowSound = new SoundStyle("CalamityMod/Sounds/Item/SwingMid")
	{
		Volume = 0.5f,
		Pitch = -0.35f,
		PitchVariance = 0.1f
	};

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastImpact")
	{
		Volume = 0.5f,
		Pitch = 0.2f,
		PitchVariance = 0.2f
	};

	public static readonly SoundStyle ShatterSound = new SoundStyle("CalamityMod/Sounds/Item/BlazingCoreParry")
	{
		Volume = 0.4f,
		PitchVariance = 0.2f
	};

	public static readonly SoundStyle LightShatterSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/CrownJewelShatter")
	{
		Pitch = 0.4f,
		PitchVariance = 0.3f
	};

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 86;
		base.Item.height = 64;
		base.Item.damage = 1170;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useAnimation = (base.Item.useTime = 55);
		base.Item.knockBack = 8.5f;
		base.Item.shoot = ModContent.ProjectileType<SeekingScorcherProj>();
		base.Item.shootSpeed = 12f;
		base.Item.useStyle = 1;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1122).AddIngredient<DivineGeode>(5).AddIngredient<UnholyEssence>(8)
			.AddTile(134)
			.Register();
	}
}
