using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Terratomere : ModItem, ILocalizedModType, IModType
{
	public const int SwingTime = 54;

	public const int SlashLifetime = 135;

	public const int SmallSlashCreationRate = 9;

	public const int TrueMeleeHitHeal = 4;

	public const int TrueMeleeGlacialStateTime = 30;

	public const float SmallSlashDamageFactor = 0.4f;

	public const float ExplosionExpandFactor = 1.013f;

	public const float TrailOffsetCompletionRatio = 0.2f;

	public static readonly Color TerraColor1;

	public static readonly Color TerraColor2;

	public static readonly SoundStyle SwingSound;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 66;
		base.Item.damage = 145;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 21;
		base.Item.useTime = 21;
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.knockBack = 7f;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<TerratomereHoldoutProj>();
		base.Item.shootSpeed = 60f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(757).AddIngredient<UelibloomBar>(7).AddIngredient<LivingShard>(9)
			.AddTile(134)
			.Register();
	}

	static Terratomere()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		TerraColor1 = new Color(141, 203, 50);
		TerraColor2 = new Color(83, 163, 136);
		SwingSound = new SoundStyle("CalamityMod/Sounds/Item/TerratomereSwing");
	}
}
