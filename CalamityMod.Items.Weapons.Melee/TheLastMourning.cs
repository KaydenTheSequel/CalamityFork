using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TheLastMourning : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 94;
		base.Item.height = 94;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.damage = 550;
		base.Item.knockBack = 8.5f;
		base.Item.useAnimation = 20;
		base.Item.useTime = 20;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.Calamity().donorItem = true;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (target.type != ModContent.NPCType<DevourerofGodsBody>() || Main.rand.NextBool(3))
		{
			CalamityPlayer.HorsemansBladeOnHit(player, target.whoAmI, base.Item.damage, base.Item.knockBack, 0, ModContent.ProjectileType<MourningSkull>());
			CalamityPlayer.HorsemansBladeOnHit(player, target.whoAmI, base.Item.damage, base.Item.knockBack, 1);
		}
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		CalamityPlayer.HorsemansBladeOnHit(player, -1, base.Item.damage, base.Item.knockBack, 0, ModContent.ProjectileType<MourningSkull>());
		CalamityPlayer.HorsemansBladeOnHit(player, -1, base.Item.damage, base.Item.knockBack, 1);
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			int dustType = 5;
			switch (Main.rand.Next(3))
			{
			case 0:
				dustType = 5;
				break;
			case 1:
				dustType = 6;
				break;
			case 2:
				dustType = 174;
				break;
			}
			int dust = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, dustType, player.direction * 2, 0f, 150, default(Color), 1.3f);
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.2f;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1826).AddIngredient(521, 30).AddIngredient<ReaperTooth>(5)
			.AddIngredient<RuinousSoul>(3)
			.AddTile(134)
			.Register();
	}
}
