using System.Collections.Generic;
using CalamityMod.Buffs.Pets;
using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Pets;

public class PrimroseKeepsake : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Pets";

	public override void SetDefaults()
	{
		base.Item.DefaultToVanitypet(ModContent.ProjectileType<PrimroseKeepsakeDisplay>(), ModContent.BuffType<FurtasticDuoBuff>());
		base.Item.UseSound = SoundID.Item44;
		base.Item.value = Item.sellPrice(1);
		base.Item.rare = 11;
		base.Item.Calamity().devItem = true;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(base.Item.buffType, 15);
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		foreach (int petProjID in new List<int>
		{
			ModContent.ProjectileType<Bear>(),
			ModContent.ProjectileType<KendraPet>()
		})
		{
			Projectile.NewProjectile(source, position, velocity, petProjID, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BearsEye>().AddIngredient<RomajedaOrchid>().AddIngredient(2352)
			.AddTile(114)
			.Register();
	}
}
