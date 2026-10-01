using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public class InvisibleDye : BaseDye
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static hook_DrawPlayer_21_Head _003C0_003E__StopHeadDrawing;

		public static hook_DrawPlayer_12_Skin _003C1_003E__StopBodyAndLegDrawing;

		public static hook_DrawPlayer_13_Leggings _003C2_003E__StopLegClothesDrawing;

		public static hook_DrawPlayer_17_Torso _003C3_003E__StopBodyClothesDrawing;

		public static hook_DrawPlayer_12_SkinComposite_BackArmShirt _003C4_003E__StopCompositeArmDrawing;

		public static hook_DrawPlayer_28_ArmOverItem _003C5_003E__StopBackArmAndUndershirtDrawing;
	}

	public override ArmorShaderData ShaderDataToBind => new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/InvisibleDyeShader"), "DyePass");

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = 1;
		base.Item.value = Item.sellPrice(0, 0, 20);
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient(126, 2).AddIngredient(38).AddTile(228)
			.Register();
	}

	public override void Load()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		object obj = _003C_003EO._003C0_003E__StopHeadDrawing;
		if (obj == null)
		{
			hook_DrawPlayer_21_Head val = StopHeadDrawing;
			_003C_003EO._003C0_003E__StopHeadDrawing = val;
			obj = (object)val;
		}
		On_PlayerDrawLayers.DrawPlayer_21_Head += (hook_DrawPlayer_21_Head)obj;
		object obj2 = _003C_003EO._003C1_003E__StopBodyAndLegDrawing;
		if (obj2 == null)
		{
			hook_DrawPlayer_12_Skin val2 = StopBodyAndLegDrawing;
			_003C_003EO._003C1_003E__StopBodyAndLegDrawing = val2;
			obj2 = (object)val2;
		}
		On_PlayerDrawLayers.DrawPlayer_12_Skin += (hook_DrawPlayer_12_Skin)obj2;
		object obj3 = _003C_003EO._003C2_003E__StopLegClothesDrawing;
		if (obj3 == null)
		{
			hook_DrawPlayer_13_Leggings val3 = StopLegClothesDrawing;
			_003C_003EO._003C2_003E__StopLegClothesDrawing = val3;
			obj3 = (object)val3;
		}
		On_PlayerDrawLayers.DrawPlayer_13_Leggings += (hook_DrawPlayer_13_Leggings)obj3;
		object obj4 = _003C_003EO._003C3_003E__StopBodyClothesDrawing;
		if (obj4 == null)
		{
			hook_DrawPlayer_17_Torso val4 = StopBodyClothesDrawing;
			_003C_003EO._003C3_003E__StopBodyClothesDrawing = val4;
			obj4 = (object)val4;
		}
		On_PlayerDrawLayers.DrawPlayer_17_Torso += (hook_DrawPlayer_17_Torso)obj4;
		object obj5 = _003C_003EO._003C4_003E__StopCompositeArmDrawing;
		if (obj5 == null)
		{
			hook_DrawPlayer_12_SkinComposite_BackArmShirt val5 = StopCompositeArmDrawing;
			_003C_003EO._003C4_003E__StopCompositeArmDrawing = val5;
			obj5 = (object)val5;
		}
		On_PlayerDrawLayers.DrawPlayer_12_SkinComposite_BackArmShirt += (hook_DrawPlayer_12_SkinComposite_BackArmShirt)obj5;
		object obj6 = _003C_003EO._003C5_003E__StopBackArmAndUndershirtDrawing;
		if (obj6 == null)
		{
			hook_DrawPlayer_28_ArmOverItem val6 = StopBackArmAndUndershirtDrawing;
			_003C_003EO._003C5_003E__StopBackArmAndUndershirtDrawing = val6;
			obj6 = (object)val6;
		}
		On_PlayerDrawLayers.DrawPlayer_28_ArmOverItem += (hook_DrawPlayer_28_ArmOverItem)obj6;
	}

	private static void StopHeadDrawing(orig_DrawPlayer_21_Head orig, ref PlayerDrawSet drawInfo)
	{
		if (drawInfo.drawPlayer.cHead != GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<InvisibleDye>()))
		{
			orig.Invoke(ref drawInfo);
		}
	}

	private static void StopBodyAndLegDrawing(orig_DrawPlayer_12_Skin orig, ref PlayerDrawSet drawInfo)
	{
		if (drawInfo.drawPlayer.cBody == GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<InvisibleDye>()))
		{
			drawInfo.hidesTopSkin = true;
		}
		if (drawInfo.drawPlayer.cLegs == GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<InvisibleDye>()))
		{
			drawInfo.hidesBottomSkin = true;
		}
		orig.Invoke(ref drawInfo);
	}

	private static void StopLegClothesDrawing(orig_DrawPlayer_13_Leggings orig, ref PlayerDrawSet drawInfo)
	{
		if (drawInfo.drawPlayer.cLegs != GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<InvisibleDye>()))
		{
			orig.Invoke(ref drawInfo);
		}
	}

	private static void StopBodyClothesDrawing(orig_DrawPlayer_17_Torso orig, ref PlayerDrawSet drawInfo)
	{
		if (drawInfo.drawPlayer.cBody != GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<InvisibleDye>()))
		{
			orig.Invoke(ref drawInfo);
		}
	}

	private static void StopCompositeArmDrawing(orig_DrawPlayer_12_SkinComposite_BackArmShirt orig, ref PlayerDrawSet drawInfo)
	{
		if (drawInfo.drawPlayer.cBody != GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<InvisibleDye>()))
		{
			orig.Invoke(ref drawInfo);
		}
	}

	private static void StopBackArmAndUndershirtDrawing(orig_DrawPlayer_28_ArmOverItem orig, ref PlayerDrawSet drawInfo)
	{
		if (drawInfo.drawPlayer.cBody != GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<InvisibleDye>()))
		{
			orig.Invoke(ref drawInfo);
		}
	}
}
