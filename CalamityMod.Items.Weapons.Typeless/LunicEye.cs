using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Typeless;

public class LunicEye : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/LunicShot", 2)
	{
		Volume = 0.8f,
		PitchVariance = 0.1f
	};

	public static readonly SoundStyle ImpactSound = new SoundStyle("CalamityMod/Sounds/Item/LunicImpact")
	{
		PitchVariance = 0.1f
	};

	public new string LocalizationCategory => "Items.Weapons.Typeless";

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 36;
		base.Item.damage = 32;
		base.Item.DamageType = AverageDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 22);
		base.Item.knockBack = 4.5f;
		base.Item.shoot = ModContent.ProjectileType<LunicBeam>();
		base.Item.shootSpeed = 12f;
		base.Item.useStyle = 5;
		base.Item.UseSound = UseSound;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)580;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		position += velocity.SafeNormalize(Vector2.Zero) * 48f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyCobaltBar", 10).AddIngredient<StarblightSoot>(20).AddTile(16)
			.Register();
	}
}
