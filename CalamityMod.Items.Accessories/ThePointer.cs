using System.Collections.Generic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ThePointer : ModItem, ILocalizedModType, IModType
{
	public static Asset<Texture2D> ActiveTexture;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.accessory = true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.ThePointerLock);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		LockOnHelper.ForceUsability = true;
		if (!CalamityKeybinds.ThePointerLock.JustPressed)
		{
			return;
		}
		LockOnHelper.Toggle();
		if (LockOnHelper.AimedTarget == null)
		{
			return;
		}
		Vector2 dir = player.DirectionTo(LockOnHelper.AimedTarget.Center);
		int loops = (int)(player.Center.Distance(LockOnHelper.AimedTarget.Center) / 30f) + 1;
		for (int i = 1; i < loops; i++)
		{
			Vector2 pos = player.Center + dir * (float)i * 30f;
			if (i >= loops - 1)
			{
				pos = LockOnHelper.AimedTarget.Center - dir * 30f;
			}
			GeneralParticleHandler.SpawnParticle(new LineParticle(pos, dir, affectedByGravity: false, 10, 1f, new Color(238, 2, 52)));
		}
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		if (LockOnHelper.AimedTarget == null)
		{
			return true;
		}
		Asset<Texture2D> tex = ActiveTexture ?? (ActiveTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Accessories/ThePointer_Active", (AssetRequestMode)2));
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, tex.Value, position, tex.Frame(), drawColor, itemColor, tex.Size() * 0.5f, 0.1f, 0.75f, default(Vector2), (SpriteEffects)0);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(170).AddRecipeGroup("AnyCopperBar", 2).AddRecipeGroup("IronBar", 3)
			.AddIngredient(109)
			.AddTile(18)
			.Register();
	}
}
