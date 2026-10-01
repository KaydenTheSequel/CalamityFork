using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Typeless;

public class EyeofMagnus : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ImpactSound = new SoundStyle("CalamityMod/Sounds/Item/MagnusImpact")
	{
		PitchVariance = 0.1f
	};

	public new string LocalizationCategory => "Items.Weapons.Typeless";

	public override void SetDefaults()
	{
		base.Item.width = 80;
		base.Item.height = 50;
		base.Item.damage = 60;
		base.Item.DamageType = AverageDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 22);
		base.Item.knockBack = 5f;
		base.Item.shoot = ModContent.ProjectileType<MagnusBeam>();
		base.Item.shootSpeed = 12f;
		base.Item.useStyle = 5;
		base.Item.UseSound = LunicEye.UseSound;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)580;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-15f, 0f);
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
		position += velocity.SafeNormalize(Vector2.Zero) * 44f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LunicEye>().AddIngredient(3457, 12).AddTile(412)
			.Register();
	}
}
