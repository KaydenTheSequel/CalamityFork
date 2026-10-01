using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class AstralPickaxe : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 60;
		base.Item.damage = 65;
		base.Item.knockBack = 5f;
		base.Item.useTime = 6;
		base.Item.useAnimation = 10;
		base.Item.pick = 220;
		base.Item.tileBoost += 3;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.rare = 9;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 25f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralBar>(7).AddTile(412).Register();
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 300);
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		Dust d = CalamityUtils.MeleeDustHelper(player, Main.rand.NextBool() ? ModContent.DustType<AstralOrange>() : ModContent.DustType<AstralBlue>(), 0.56f, 40f, 65f, -0.13f, 0.13f);
		if (d != null)
		{
			d.customData = 0.02f;
		}
	}
}
