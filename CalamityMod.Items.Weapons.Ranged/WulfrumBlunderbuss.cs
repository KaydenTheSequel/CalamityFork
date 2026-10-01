using System;
using System.IO;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "WulfrumBow" })]
public class WulfrumBlunderbuss : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ShootSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumBlunderbussFire")
	{
		PitchVariance = 0.1f
	};

	public static readonly SoundStyle ShootAndReloadSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumBlunderbussFireAndReload")
	{
		PitchVariance = 0.1f
	};

	public static int ArmorPenetration = 3;

	public static float MinSpreadDistance = 460f;

	public static float MaxSpreadDistance = 60f;

	public static float MinSpread = 0.2f;

	public static float MaxSpread = 0.6f;

	public static float MaxDamageFalloff = 0.8f;

	public static int BulletCount = 6;

	public static int ShotsPerScrap = 30;

	public int storedScrap;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ArmorPenetration);

	public override void SetDefaults()
	{
		base.Item.width = 23;
		base.Item.height = 8;
		base.Item.damage = 11;
		base.Item.ArmorPenetration = ArmorPenetration;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 55;
		base.Item.useAnimation = 55;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.25f;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.UseSound = ShootSound;
		base.Item.autoReuse = false;
		base.Item.shoot = ModContent.ProjectileType<WulfrumScrapBullet>();
		base.Item.shootSpeed = 15f;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool CanUseItem(Player player)
	{
		if (storedScrap <= 0)
		{
			if (!player.HasItem(ModContent.ItemType<WulfrumMetalScrap>()))
			{
				return player.HasItem(72);
			}
			return true;
		}
		return true;
	}

	public override void UseAnimation(Player player)
	{
		base.Item.UseSound = ShootSound;
		if (storedScrap == 1 && (player.HasItem(ModContent.ItemType<WulfrumMetalScrap>()) || player.HasItem(72)))
		{
			base.Item.UseSound = ShootAndReloadSound;
		}
	}

	public override bool? UseItem(Player player)
	{
		storedScrap--;
		if (storedScrap <= 0)
		{
			bool ammoConsumed = false;
			if (player.HasItem(ModContent.ItemType<WulfrumMetalScrap>()))
			{
				player.ConsumeItem(ModContent.ItemType<WulfrumMetalScrap>());
				ammoConsumed = true;
			}
			else if (player.HasItem(72))
			{
				player.ConsumeItem(72);
				ammoConsumed = true;
			}
			if (ammoConsumed)
			{
				storedScrap = ShotsPerScrap;
			}
		}
		return base.UseItem(player);
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Main.MouseWorld - player.MountedCenter;
		float aimLength = ((Vector2)(ref val)).Length();
		float damageMult = MathHelper.Lerp(1f, MaxDamageFalloff, Math.Clamp(aimLength - MaxSpreadDistance, 0f, MinSpreadDistance - MaxSpreadDistance) / (MinSpreadDistance - MaxSpreadDistance));
		damage = (int)((float)damage * damageMult);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		player.SetScreenshake(3f);
		Vector2 val = Main.MouseWorld - player.MountedCenter;
		float spreadDistance = Math.Clamp(((Vector2)(ref val)).Length() - MaxSpreadDistance, 0f, MinSpreadDistance - MaxSpreadDistance) / (MinSpreadDistance - MaxSpreadDistance);
		float spread = MathHelper.Lerp(MaxSpread, MinSpread, spreadDistance);
		Vector2 nuzzleDir = velocity.SafeNormalize(Vector2.Zero);
		for (int i = 0; i < BulletCount; i++)
		{
			Vector2 direction = nuzzleDir.RotatedByRandom(spread);
			Vector2 nuzzlePos = player.MountedCenter + direction * 15f;
			Projectile.NewProjectile(player.GetSource_ItemUse_WithPotentialAmmo(base.Item, base.Item.useAmmo), nuzzlePos, direction * base.Item.shootSpeed * Main.rand.NextFloat(1.5f, 2f), type, damage, (int)base.Item.knockBack, player.whoAmI);
		}
		return false;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * 7f;
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector(46f, 16f);
		Vector2 itemOrigin = default(Vector2);
		((Vector2)(ref itemOrigin))._002Ector(-13f, 3f);
		CalamityUtils.CleanHoldStyle(player, itemRotation, itemPosition, itemSize, itemOrigin);
		base.UseStyle(player, heldItemFrame);
	}

	public override void UseItemFrame(Player player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float animProgress = 1f - (float)player.itemTime / (float)player.itemTimeMax;
		float rotation = (player.Center - player.Calamity().mouseWorld).ToRotation() * player.gravDir + (float)Math.PI / 2f;
		if (animProgress < 0.4f)
		{
			rotation += -0.45f * (float)Math.Pow((0.4f - animProgress) / 0.4f, 2.0) * (float)player.direction;
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
		if (animProgress > 0.5f)
		{
			float backArmRotation = rotation + 0.52f * (float)player.direction;
			Player.CompositeArmStretchAmount stretch = ((float)Math.Sin((float)Math.PI * (animProgress - 0.5f) / 0.36f)).ToStretchAmount();
			player.SetCompositeArmBack(enabled: true, stretch, backArmRotation);
		}
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		float barScale = 1.2f;
		Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
		Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
		Vector2 drawPos = position + Vector2.UnitY * ((float)(frame.Height - 2) + 6f) * scale + Vector2.UnitX * ((float)frame.Width - (float)barBG.Width * barScale) * scale * 0.5f;
		Rectangle frameCrop = default(Rectangle);
		((Rectangle)(ref frameCrop))._002Ector(0, 0, (int)((float)storedScrap / (float)ShotsPerScrap * (float)barFG.Width), barFG.Height);
		Color colorBG = Color.RoyalBlue;
		Color colorFG = Color.Lerp(Color.Teal, Color.YellowGreen, (float)storedScrap / (float)ShotsPerScrap);
		spriteBatch.Draw(barBG, drawPos, (Rectangle?)null, colorBG, 0f, origin, scale * barScale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(barFG, drawPos, (Rectangle?)frameCrop, colorFG * 0.8f, 0f, origin, scale * barScale, (SpriteEffects)0, 0f);
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, storedScrap.ToString(), drawPos + new Vector2(-30f, -3f) * scale, Color.GreenYellow, Color.Black, scale);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(10).AddTile(16).Register();
	}

	public override void OnCreated(ItemCreationContext context)
	{
		if (context is RecipeItemCreationContext)
		{
			storedScrap = ShotsPerScrap;
		}
	}

	public override ModItem Clone(Item item)
	{
		ModItem modItem = base.Clone(item);
		if (modItem is WulfrumBlunderbuss a && item.ModItem is WulfrumBlunderbuss a2)
		{
			a.storedScrap = a2.storedScrap;
		}
		return modItem;
	}

	public override void SaveData(TagCompound tag)
	{
		tag["ammoStored"] = storedScrap;
	}

	public override void LoadData(TagCompound tag)
	{
		storedScrap = tag.GetInt("ammoStored");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(storedScrap);
	}

	public override void NetReceive(BinaryReader reader)
	{
		storedScrap = reader.ReadInt32();
	}
}
