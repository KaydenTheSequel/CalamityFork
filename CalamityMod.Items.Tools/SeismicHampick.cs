using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

[LegacyName(new string[] { "FlamebeakHampick" })]
public class SeismicHampick : ModItem, ILocalizedModType, IModType
{
	private const int PickPower = 210;

	private const int HammerPower = 95;

	public new string LocalizationCategory => "Items.Tools";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 50;
		base.Item.damage = 58;
		base.Item.knockBack = 8f;
		base.Item.useTime = 6;
		base.Item.useAnimation = 15;
		base.Item.pick = 210;
		base.Item.hammer = 95;
		base.Item.tileBoost += 2;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.pick = 0;
			base.Item.hammer = 95;
		}
		else
		{
			base.Item.pick = 210;
			base.Item.hammer = 0;
		}
		return base.CanUseItem(player);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScoriaBar>(7).AddTile(134).Register();
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, Main.rand.NextBool(3) ? 16 : 127);
		}
		if (Main.rand.NextBool(5) && !Main.dedServ)
		{
			int smoke = Gore.NewGore(player.GetSource_ItemUse(base.Item), new Vector2((float)hitbox.X, (float)hitbox.Y), default(Vector2), Main.rand.Next(375, 378), 0.75f);
			Main.gore[smoke].behindTiles = true;
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 300);
	}
}
