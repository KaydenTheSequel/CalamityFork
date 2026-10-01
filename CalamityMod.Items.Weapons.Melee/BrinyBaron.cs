using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class BrinyBaron : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 100;
		base.Item.height = 102;
		base.Item.damage = 182;
		base.Item.knockBack = 4f;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 4f;
		base.Item.shoot = ModContent.ProjectileType<Razorwind>();
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool? UseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.noMelee = true;
		}
		else
		{
			base.Item.noMelee = false;
		}
		return base.UseItem(player);
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.altFunctionUse == 2)
		{
			damage = (int)((double)damage * 0.3);
			type = ModContent.ProjectileType<Razorwind>();
		}
		else
		{
			type = 0;
		}
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 187, 0f, 0f, 100, new Color(53, Main.DiscoG, 255));
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
		IEntitySource source = player.GetSource_ItemUse(base.Item);
		if (player.ownedProjectileCounts[ModContent.ProjectileType<BrinySpout>()] == 0)
		{
			Projectile.NewProjectile(source, target.Center, Vector2.Zero, ModContent.ProjectileType<BrinyTyphoonBubble>(), base.Item.damage, base.Item.knockBack, player.whoAmI);
		}
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
		IEntitySource source = player.GetSource_ItemUse(base.Item);
		if (player.ownedProjectileCounts[ModContent.ProjectileType<BrinySpout>()] == 0)
		{
			Projectile.NewProjectile(source, target.Center, Vector2.Zero, ModContent.ProjectileType<BrinyTyphoonBubble>(), base.Item.damage, base.Item.knockBack, player.whoAmI);
		}
	}

	public override void UseAnimation(Player player)
	{
		base.Item.noUseGraphic = false;
		base.Item.UseSound = SoundID.Item1;
		if (player.altFunctionUse == 2)
		{
			base.Item.noUseGraphic = true;
			base.Item.UseSound = SoundID.Item84;
		}
	}
}
