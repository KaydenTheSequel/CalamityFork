using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Packets;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class IgneousExaltation : ModItem, ILocalizedModType, IModType
{
	private static Texture2D BladeOutline;

	public static int ChargeDuration => 25;

	public static int ChargeCooldown => 120;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public static Texture2D GetBladeOutlineTex()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		if (BladeOutline == null)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/IgneousBlade", (AssetRequestMode)2).Value;
			BladeOutline = new Texture2D(Main.graphics.GraphicsDevice, texture.Width, texture.Height);
			Color[] BaseArray = (Color[])(object)new Color[BladeOutline.Width * BladeOutline.Height];
			Color[] ColorArray = (Color[])(object)new Color[BladeOutline.Width * BladeOutline.Height];
			texture.GetData<Color>(BaseArray);
			for (int i = 0; i < BaseArray.Length; i++)
			{
				ColorArray[i] = new Color(255, 255, 255) * ((float)(int)((Color)(ref BaseArray[i])).A / 255f);
			}
			BladeOutline.SetData<Color>(ColorArray);
		}
		return BladeOutline;
	}

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 50;
		base.Item.damage = 30;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4.5f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item71;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<IgneousExaltationBuff>();
		base.Item.shoot = ModContent.ProjectileType<IgneousBlade>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool CanRightClick()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.keyState.PressingShift())
		{
			return false;
		}
		return true;
	}

	public override void RightClick(Player player)
	{
		Main.LocalPlayer.Calamity().InvertExaltationLineRotationDirections = !Main.LocalPlayer.Calamity().InvertExaltationLineRotationDirections;
		if (Main.netMode != 0)
		{
			ExaltationDirectionSyncPacket.Send(Main.LocalPlayer.Calamity());
		}
	}

	public override bool ConsumeItem(Player player)
	{
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		float totalSlots = 0f;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.minion && p.owner == player.whoAmI)
			{
				totalSlots += p.minionSlots;
			}
		}
		if (totalSlots >= (float)player.maxMinions)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Projectile pro = enumerator2.Current;
				if (pro.type == type && pro.owner == player.whoAmI && pro.ai[1] >= 0f && pro.ai[0] == 0f)
				{
					pro.ModProjectile<IgneousBlade>().CurrentState = IgneousBlade.AIState.TransitionToLaunch;
					pro.netUpdate = true;
				}
			}
		}
		else
		{
			player.AddBuff(base.Item.buffType, 2);
			Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 1f).originalDamage = base.Item.damage;
			int bladeIndex = 0;
			ActiveEntityIterator<Projectile>.Enumerator enumerator3 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				Projectile pro2 = enumerator3.Current;
				if (pro2.type == type && pro2.owner == player.whoAmI)
				{
					pro2.ModProjectile<IgneousBlade>().BladeIndex = bladeIndex++;
					pro2.ModProjectile<IgneousBlade>().AITimer = -ChargeCooldown;
					pro2.ModProjectile<IgneousBlade>().DistanceTimer = -ChargeCooldown;
					pro2.ModProjectile<IgneousBlade>().CurrentState = IgneousBlade.AIState.CircleOwner;
					pro2.netUpdate = true;
				}
			}
		}
		return false;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Item[base.Type].Value;
		SpriteEffects spriteEffects = (SpriteEffects)(Main.LocalPlayer.Calamity().InvertExaltationLineRotationDirections ? 1 : 0);
		float rotation = (Main.LocalPlayer.Calamity().InvertExaltationLineRotationDirections ? ((float)Math.PI / 2f) : 0f);
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, tex, position, frame, drawColor, itemColor, origin, scale, 0.75f, default(Vector2), spriteEffects, rotation);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UnholyCore>(10).AddIngredient<EssenceofHavoc>(5).AddTile(134)
			.Register();
	}
}
