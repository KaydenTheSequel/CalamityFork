using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TheMutilator : BaseSwordHoldoutItem, ILocalizedModType, IModType
{
	public static int MaximumCharge = 7;

	public int Charge;

	public int DecayTimer;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override int ProjectileType => ModContent.ProjectileType<MutilatorSwordProj>();

	public override void SetDefaults()
	{
		base.Item.width = 90;
		base.Item.height = 90;
		base.Item.damage = 1005;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 1;
		base.Item.useTime = 30;
		base.Item.knockBack = 8f;
		base.Item.shootSpeed = 10f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.SetDefaults();
	}

	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		damage *= 1f + (float)Charge / 7f;
	}

	public override void UpdateInventory(Player player)
	{
		if (DecayTimer > 0)
		{
			DecayTimer--;
		}
		else if (Charge > 0)
		{
			Charge--;
			DecayTimer = 60;
		}
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		float fill = (float)Charge / (float)MaximumCharge;
		if (!(fill <= 0f))
		{
			float barScale = 3f;
			Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
			Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
			Vector2 barOrigin = barBG.Size() * 0.5f;
			float yOffset = 44f;
			Vector2 drawPos = position + Vector2.UnitY * scale * ((float)frame.Height - yOffset);
			Rectangle frameCrop = default(Rectangle);
			((Rectangle)(ref frameCrop))._002Ector(0, 0, (int)(fill * (float)barFG.Width), barFG.Height);
			Color colorBG = Color.Crimson;
			Color colorFG = Color.Lerp(Color.OrangeRed, Color.DarkOrange, fill);
			spriteBatch.Draw(barBG, drawPos, (Rectangle?)null, colorBG, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(barFG, drawPos, (Rectangle?)frameCrop, colorFG, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(5).AddTile(134).Register();
	}
}
