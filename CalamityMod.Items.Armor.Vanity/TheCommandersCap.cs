using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class TheCommandersCap : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			ArmorIDs.Head.Sets.DrawFullHair[base.Item.headSlot] = false;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 20;
		base.Item.rare = 1;
		base.Item.vanity = true;
		base.Item.Calamity().donorItem = true;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<TheGrandGarment>())
		{
			return legs.type == ModContent.ItemType<TheFormalFootwear>();
		}
		return false;
	}

	public override void UpdateVanitySet(Player player)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		if (!player.CCed && (!Main.gamePaused || Main.gameMenu) && player.velocity.X != 0f && player.velocity.Y == 0f)
		{
			for (int k = 0; k < 2; k++)
			{
				int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)((player.gravDir == 1f) ? (player.height - 2) : (-4))), player.width, 6, 16, 0f, 0f, 100, default(Color), 0.1f);
				Main.dust[dust].fadeIn = 1f;
				Main.dust[dust].noGravity = true;
				Dust obj = Main.dust[dust];
				obj.velocity *= 0.2f;
				Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
			}
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(225, 5).AddRecipeGroup("AnyGoldBar").AddIngredient(1015)
			.AddTile(86)
			.Register();
	}
}
