using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class Gelpick : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 48;
		base.Item.damage = 19;
		base.Item.knockBack = 2.5f;
		base.Item.useTime = 9;
		base.Item.useAnimation = 12;
		base.Item.pick = 105;
		base.Item.tileBoost++;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PurifiedGel>(12).AddIngredient<BlightedGel>(12).AddTile(220)
			.Register();
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(4))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 20);
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(137, 180);
	}
}
