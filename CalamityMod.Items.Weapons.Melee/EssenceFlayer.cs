using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class EssenceFlayer : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 100;
		base.Item.height = 78;
		base.Item.damage = 220;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTime = (base.Item.useAnimation = 19);
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.knockBack = 8f;
		base.Item.UseSound = SoundID.Item71;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<EssenceScythe>();
		base.Item.shootSpeed = 21f;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/EssenceFlayerGlow", (AssetRequestMode)2).Value);
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			int dust = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 173);
			Dust obj = Main.dust[dust];
			obj.velocity *= 0f;
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 300);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 300);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(12).AddTile<CosmicAnvil>().Register();
	}
}
