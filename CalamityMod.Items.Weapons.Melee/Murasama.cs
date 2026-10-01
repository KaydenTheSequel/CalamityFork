using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Murasama : ModItem, ILocalizedModType, IModType
{
	public int frameCounter;

	public int frame;

	public static readonly SoundStyle OrganicHit = new SoundStyle("CalamityMod/Sounds/Item/MurasamaHitOrganic")
	{
		Volume = 0.45f
	};

	public static readonly SoundStyle InorganicHit = new SoundStyle("CalamityMod/Sounds/Item/MurasamaHitInorganic")
	{
		Volume = 0.55f
	};

	public static readonly SoundStyle Swing = new SoundStyle("CalamityMod/Sounds/Item/MurasamaSwing")
	{
		Volume = 0.2f
	};

	public static readonly SoundStyle BigSwing = new SoundStyle("CalamityMod/Sounds/Item/MurasamaBigSwing")
	{
		Volume = 0.25f
	};

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public bool IDUnlocked(Player player)
	{
		return DownedBossSystem.downedDoG;
	}

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(2, 13));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 90;
		base.Item.height = 134;
		base.Item.damage = 2200;
		base.Item.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.useAnimation = 25;
		base.Item.useStyle = 5;
		base.Item.useTime = 5;
		base.Item.knockBack = 6.5f;
		base.Item.autoReuse = false;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<MurasamaSlash>();
		base.Item.shootSpeed = 24f;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 61f;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frameI, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (IDUnlocked(Main.LocalPlayer))
		{
			Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
			spriteBatch.Draw(texture, position, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 2, 13), Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		}
		else
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/MurasamaSheathed", (AssetRequestMode)2).Value;
			spriteBatch.Draw(texture, position, (Rectangle?)null, Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		}
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (IDUnlocked(Main.LocalPlayer))
		{
			Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
			spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 2, 13), lightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		else
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/MurasamaSheathed", (AssetRequestMode)2).Value;
			spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)null, lightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if (IDUnlocked(Main.LocalPlayer))
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/MurasamaGlow", (AssetRequestMode)2).Value;
			spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 2, 13, frameCounterUp: false), Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] > 0)
		{
			return false;
		}
		return IDUnlocked(player);
	}
}
