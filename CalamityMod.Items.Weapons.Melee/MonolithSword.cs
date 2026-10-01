using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class MonolithSword : ModItem, ILocalizedModType, IModType
{
	public static int ArmorPenetration = 15;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ArmorPenetration);

	public override void SetDefaults()
	{
		base.Item.damage = 30;
		base.Item.width = 40;
		base.Item.height = 46;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 7);
		base.Item.useStyle = 1;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.ArmorPenetration = ArmorPenetration;
	}

	public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		float scale = 2.5f;
		Vector2 newSize = Utils.ToVector2(new Point(hitbox.Width, hitbox.Height)) * scale;
		hitbox = new Rectangle(hitbox.X - (int)((newSize.X - (float)hitbox.Width) / 2f), hitbox.Y - (int)((newSize.Y - (float)hitbox.Height) / 2f), (int)newSize.X, (int)newSize.Y);
	}

	public override void UseAnimation(Player player)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		if (player.whoAmI == Main.myPlayer)
		{
			float Rot = ((player.direction == -1) ? 5.5f : (-5.5f)) * Main.rand.NextFloat(0.99f, 1.1f);
			GeneralParticleHandler.SpawnParticle(new SemiCircularSmearFade(player.Center, Vector2.Zero, (Main.rand.NextBool() ? Color.DarkTurquoise : Color.Coral) * 0.7f, Rot, Main.rand.NextFloat(1.48f, 1.53f), new Vector2(1f, 1f), 6, playerCentered: true));
		}
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, Main.rand.NextBool() ? ModContent.DustType<AstralOrange>() : ModContent.DustType<AstralBlue>());
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralMonolith>(7).AddTile(18).Register();
	}
}
