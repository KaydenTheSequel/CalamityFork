using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Back })]
public class QuiverofNihility : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 36;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		player.GetCritChance<RangedDamageClass>() += 5f;
		player.Calamity().voidField = true;
		if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<VoidFieldGenerator>()] < 4)
		{
			IEntitySource source = player.GetSource_Accessory(base.Item);
			int count = player.ownedProjectileCounts[ModContent.ProjectileType<VoidFieldGenerator>()];
			do
			{
				Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<VoidFieldGenerator>(), 0, 0f, Main.myPlayer, count);
				count++;
			}
			while (count < 4);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyQuiver").AddIngredient<GalacticaSingularity>(5).AddIngredient<DarkPlasma>(3)
			.AddTile(134)
			.Register();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.55f, new Vector2(0f, 0f), (SpriteEffects)0);
		return false;
	}
}
