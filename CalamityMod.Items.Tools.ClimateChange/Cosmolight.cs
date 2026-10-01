using CalamityMod.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Net;

namespace CalamityMod.Items.Tools.ClimateChange;

public class Cosmolight : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 4;
		base.Item.useAnimation = 9;
		base.Item.useTime = 9;
		base.Item.autoReuse = false;
		base.Item.useStyle = 4;
		base.Item.UseSound = SoundID.Item60;
		base.Item.consumable = false;
		base.Item.channel = true;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)820;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool? UseItem(Player player)
	{
		if (Main.netMode != 2 && player == Main.LocalPlayer && (player.altFunctionUse == 2 || CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().Enabled))
		{
			CreativePowers.FreezeTime power = CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>();
			NetPacket packet = NetCreativePowersModule.PreparePacket(power.PowerId, 1);
			packet.Writer.Write(!power.Enabled);
			NetManager.Instance.SendToServerOrLoopback(packet);
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Bakidon>().AddIngredient(3064).AddIngredient<AstralBar>(3)
			.AddIngredient(3458, 15)
			.AddTile(26)
			.Register();
		CreateRecipe().AddIngredient<Bakidon>().AddIngredient(5381).AddIngredient<AstralBar>(3)
			.AddIngredient(3458, 15)
			.AddTile(26)
			.Register();
	}
}
