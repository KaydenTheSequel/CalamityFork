using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Wings;

[AutoloadEquip(new EquipType[] { EquipType.Wings })]
public class SilvaWings : BaseWings
{
	public override float BonusAscentWhileFalling => 0.95f;

	public override float BonusAscentWhileRising => 0.16f;

	public override float RisingSpeedThreshold => 1.1f;

	public override float MaxAscentSpeed => 3.2f;

	public override float BaseAscent => 0.145f;

	public override void SetStaticDefaults()
	{
		ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(270, 10.5f, 2.8f);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Item.width = 22;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
		{
			int dustXOffset = 4;
			if (player.direction == 1)
			{
				dustXOffset = -40;
			}
			int flightDust = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)dustXOffset, player.position.Y + (float)(player.height / 2) - 15f), 30, 30, 157, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103));
			Main.dust[flightDust].noGravity = true;
			Dust obj = Main.dust[flightDust];
			obj.velocity *= 0.3f;
			if (Main.rand.NextBool(10))
			{
				Main.dust[flightDust].fadeIn = 2f;
			}
			Main.dust[flightDust].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(575, 20).AddIngredient<PlantyMush>(3).AddIngredient<EffulgentFeather>(15)
			.AddIngredient<AscendantSpiritEssence>(2)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
