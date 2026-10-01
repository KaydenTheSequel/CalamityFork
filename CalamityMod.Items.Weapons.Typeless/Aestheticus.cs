using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Typeless;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Typeless;

public class Aestheticus : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Typeless";

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 58;
		base.Item.DamageType = AverageDamageClass.Instance;
		base.Item.damage = 8;
		base.Item.useAnimation = 25;
		base.Item.useTime = 25;
		base.Item.useStyle = 1;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item109;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<CursorProj>();
		base.Item.shootSpeed = 5f;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)580;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Vaporfied>(), 120);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(ModContent.BuffType<Vaporfied>(), 120);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AerialiteBar>(5).AddIngredient<SeaPrism>(10).AddIngredient(75, 5)
			.AddTile(16)
			.Register();
	}
}
