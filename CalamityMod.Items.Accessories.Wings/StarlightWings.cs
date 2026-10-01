using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Wings;

[AutoloadEquip(new EquipType[] { EquipType.Wings })]
public class StarlightWings : BaseWings
{
	public override void SetStaticDefaults()
	{
		ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(160, 7.5f, 1.5f);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Item.width = 22;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
		{
			int dustXOffset = 4;
			if (player.direction == 1)
			{
				dustXOffset = -40;
			}
			int flightDust = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)dustXOffset, player.position.Y + (float)(player.height / 2) - 15f), 30, 30, 173, 0f, 0f, 100, default(Color), 2.4f);
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
		CreateRecipe().AddIngredient(575, 20).AddIngredient<CryonicBar>(5).AddTile(134)
			.Register();
	}
}
