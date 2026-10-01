using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes.HairDye;

public class WingTimeHairDye : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Dyes";

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			GameShaders.Hair.BindShader(base.Type, new LegacyHairShaderData().UseLegacyMethod(UpdateHairDye));
		}
	}

	private static Color UpdateHairDye(Player player, Color newColor, ref bool lighting)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		float wingP = player.wingTime / (float)player.wingTimeMax;
		if (player.mount.Active)
		{
			wingP = 1f;
		}
		else if (float.IsInfinity(wingP) || float.IsNaN(wingP))
		{
			wingP = 0f;
		}
		return Color.Lerp(player.hairColor, new Color(139, 205, 255), wingP);
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 26;
		base.Item.useAnimation = (base.Item.useTime = 17);
		base.Item.UseSound = SoundID.Item3;
		base.Item.useStyle = 9;
		base.Item.useTurn = true;
		base.Item.consumable = true;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.buyPrice(0, 5);
		base.Item.rare = 2;
	}
}
